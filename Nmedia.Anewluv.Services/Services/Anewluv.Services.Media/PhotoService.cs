using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;
using System.Web;
using System.Net;

using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using System.ServiceModel.Activation;
using LoggingLibrary;

using Anewluv.Services.Contracts.ServiceResponse;
using Nmedia.Infrastructure.Helpers;
using System.IO;
using ImageResizer;
//using Nmedia.DataAccess.Interfaces;
using System.Drawing;

using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.ExceptionHandling;
using System.Threading.Tasks;
using Anewluv.Domain;
using Repository.Pattern.UnitOfWork;

namespace Anewluv.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
    public class PhotoService : IPhotoService
    {


        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public PhotoService(IUnitOfWorkAsync unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

            //promotionrepository = _promotionrepository;
            _unitOfWorkAsync = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWorkAsync.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

        //BEfore unit of work contrcutor
        //public MemberActionsService(IMemberActionsRepository memberactionsrepository)
        //    {
        //        _memberactionsrepository = memberactionsrepository;
        //       // _apikey  = HttpContext.Current.Request.QueryString["apikey"];
        //      //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        //    }




        #region "private Data Context methods that are not to be serialzied"
       
        //TO DO move to extention methods 
        /// <summary>
        /// No using on this one since it is called internally
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="photouploaded"></param>
        /// <returns></returns>
        private List<photoconversion> addphotoconverionsb64string(photo photo, PhotoUploadModel photouploaded,AnewluvContext db)
        {
            //TemporaryImageUpload tempImageUpload = new TemporaryImageUpload();             
            // tempImageUpload = _service.GetImageData(id) ?? null;
            List<photoconversion> convertedphotos = new List<photoconversion>();

            if (photouploaded.imageb64string != null)
            {

                try
                {
                   //var db = _unitOfWorkAsync;
                    var test = _unitOfWorkAsync.Repository<lu_photoformat>().Queryable().OfType<lu_photoformat>().ToList();
                    foreach (lu_photoformat currentformat in _unitOfWorkAsync.Repository<lu_photoformat>().Query().Include(p=>p.lu_photoImagersizerformat).Select())
                    {


                        byte[] byteArray = b64Converters.b64stringtoByteArray(photouploaded.imageb64string);  //tempImageUpload.TempImageData; 
                        using (var outStream = new MemoryStream())
                        {
                            using (var inStream = new MemoryStream(byteArray))
                            {
                                try
                                {
                                    //var settings1 = new ResizeSettings("maxwidth=200&maxheight=200");

                                    var settings = new ResizeSettings(currentformat.lu_photoImagersizerformat.description);
                                    ImageResizer.ImageBuilder.Current.Build(inStream, outStream, settings);
                                    var outBytes = outStream.ToArray();
                                    //double check that there is no conversion with matching size and the name
                                    // var test = _datingcontext.photoconversions.Where(p => p.photo.profile_id == photo.profile_id).Any(p => p.size == photo.size);

                                    convertedphotos.Add(new photoconversion
                                    {
                                        creationdate = DateTime.Now,
                                        description = currentformat.description,
                                         lu_photoformat = currentformat,formattype_id =currentformat.id, ObjectState= Repository.Pattern.Infrastructure.ObjectState.Added,
                                        image = outBytes,
                                        size = outBytes.Length,
                                        photo_id = photo.id
                                    });

                                }
                                catch (ImageResizer.ImageMissingException missing)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, missing.Message, missing);
                                    new  Logging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning,globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageCorruptedException cr)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, cr.Message, cr);
                                    new  Logging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageProcessingException ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new  Logging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (Exception ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new  Logging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                            }
                        }
                    }
                }



                catch (Exception ex)
                {
                    //logg this from the caller ? so we dont log twice
                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), "", ex.Message, ex.InnerException);
                    //new  Logging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption, null, null);
                    throw convertedexcption;
                }

            }
            return convertedphotos;


        }
        #endregion

        #region "View Photo models"  

        public async Task<PhotoModel> getphotomodelbyphotoid(string photoid, string format)
        {
           
         
            {
             
                    try
                    {
                        
                    var task = Task.Factory.StartNew(() =>
                    {
                        var convertedformat = Convert.ToInt16(format);
                        var convertedphotoid = Guid.Parse(photoid);

                        PhotoModel model = (from p in
                                                (from r in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(a => a.formattype_id == convertedformat && (a.photo.id == convertedphotoid)).ToList()
                                                 select new
                                                 {
                                                     photoid = r.photo.id,
                                                     profileid = r.photo.profile_id,
                                                     screenname = r.photo.profilemetadata.profile.screenname,
                                                     photo = r.image,
                                                     photoformat = r.lu_photoformat,
                                                     convertedsize = r.size,
                                                     orginalsize = r.photo.size,
                                                     imagecaption = r.photo.imagecaption,
                                                     creationdate = r.photo.creationdate,

                                                 }).ToList()
                                            select new PhotoModel
                                            {
                                                photoid = p.photoid,
                                                profileid = p.profileid,
                                                screenname = p.screenname,
                                                photo = b64Converters.ByteArraytob64string(p.photo),
                                                photoformat = p.photoformat,
                                                convertedsize = p.convertedsize,
                                                orginalsize = p.orginalsize,
                                                imagecaption = p.imagecaption,
                                                creationdate = p.creationdate,

                                            }).FirstOrDefault();



                        // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

                        //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



                        return (model);
                           });
                    return await task.ConfigureAwait(false);

                     
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }


                
              
            }

            
        }

        public async Task<PhotoModel> getgalleryphotomodelbyprofileid(string profileid, string format)
        {

         
         
            {
              
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            return _unitOfWorkAsync.Repository<photoconversion>().getgalleryphotomodelbyprofileid(profileid, format);
                        });
                        return await task.ConfigureAwait(false);

                      
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
       


               
            }
          
        }

        public async Task<List<PhotoModel>> getphotomodelsbyprofileidandstatus(string profileid, string status, string format)
        {

           
         
            {

             
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            var convertedprofileid = Convert.ToInt32(profileid);
                            var convertedstatus = Convert.ToInt16(status);

                            //var convertedphotoid = Guid.Parse(photoid);
                            // Retrieve All User's Photos that are not approved.
                            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

                            // Retrieve All User's Approved Photo's that are not Private and approved.
                            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

                            var model = (from p in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(a => a.formattype_id == convertedformat && a.photo.profile_id == convertedprofileid &&
                                ((a.photo.lu_photoapprovalstatus != null && a.photo.approvalstatus_id == convertedstatus))).ToList()
                                         select new PhotoModel
                                         {
                                             photoid = p.photo.id,
                                             profileid = p.photo.profile_id,
                                             screenname = p.photo.profilemetadata.profile.screenname,
                                             photo = b64Converters.ByteArraytob64string(p.image),
                                             photoformat = p.lu_photoformat,
                                             convertedsize = p.size,
                                             orginalsize = p.photo.size,
                                             imagecaption = p.photo.imagecaption,
                                             creationdate = p.photo.creationdate,
                                         });

                            return (model.OrderByDescending(u => u.creationdate).ToList());
                        });
                        return await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
              


              
            }

           
        }
      
       public async Task<List<PhotoModel>> getpagedphotomodelbyprofileidandstatus(string profileid, string status, string format, string page, string pagesize)
        {

         
         
            {
              
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            return _unitOfWorkAsync.Repository<photoconversion>().getpagedphotomodelbyprofileidandstatus(profileid, status, format, page, pagesize);
                        });
                        return await task.ConfigureAwait(false);
                       
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
              


               
            }
       

        }

        //TO DO get photo albums as well ?
        //TO DO not implemented
        public async Task <PhotoViewModel> getpagedphotoviewmodelbyprofileid(string profileid, string format, string page, string pagesize)
        {
           
         
            {

               
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            return new PhotoViewModel();
                        });
                        return await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                    return new PhotoViewModel();
             

              
            }
      
        }
              
        #endregion

        #region "Edititable Photo models

        public async Task<photoeditmodel> getphotoeditmodelbyphotoid(string photoid, string format)
        {
           
         
            {

             
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            var convertedphotoid = Guid.Parse(photoid);
                            // var convertedprofileid = Convert.ToInt32(profileid);
                            // var convertedstatus = Convert.ToInt16(status);

                            photoeditmodel model = (from p in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(p => p.formattype_id == convertedformat && p.photo.id == convertedphotoid).ToList()
                                                    select new photoeditmodel
                                                    {
                                                        photoid = p.photo.id,
                                                        profileid = p.photo.profile_id,
                                                        screenname = p.photo.profilemetadata.profile.screenname,
                                                        approved = (p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                                        profileimagetype = p.photo.lu_photoimagetype.description,
                                                        imagecaption = p.photo.imagecaption,
                                                        creationdate = p.photo.creationdate,
                                                        photostatusid = p.photo.lu_photostatus.id,
                                                        checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                                                    }).Single();

                            // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());
                            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);
                            return (model);
                        });
                        return await task.ConfigureAwait(false);

                      
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
              


            }
       }

        public async Task<List<photoeditmodel>> getphotoeditmodelsbyprofileidandstatus(string profile_id, string status, string format)
        {
           
         
            {
              
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            var convertedstatus = Convert.ToInt16(status);

                            var model = (from p in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(a => a.formattype_id == convertedformat
                                && a.photo.lu_photoapprovalstatus != null && a.photo.approvalstatus_id == convertedstatus).ToList()
                                         select new photoeditmodel
                                         {
                                             photoid = p.photo.id,
                                             profileid = p.photo.profile_id,
                                             screenname = p.photo.profilemetadata.profile.screenname,
                                             approved = (p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                             profileimagetype = p.photo.lu_photoimagetype.description,
                                             imagecaption = p.photo.imagecaption,
                                             creationdate = p.photo.creationdate,
                                             photostatusid = p.photo.lu_photostatus.id,
                                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                                         });
                            return (model.OrderByDescending(u => u.creationdate).ToList());
                        });
                        return await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
             


               
            }
           

        }

        public async Task<List<photoeditmodel>> getpagedphotoeditmodelsbyprofileidstatus(string profile_id, string status, string format,
                                                              string page, string pagesize)
        {

           
         
            {
              
                    try
                    {
                        
                    var task = Task.Factory.StartNew(() =>
                    {
                        var convertedformat = Convert.ToInt16(format);
                        //var convertedphotoid = Guid.Parse(photoid);
                        //var convertedprofileid = Convert.ToInt32(profileid);
                        var convertedstatus = Convert.ToInt16(status);
                        // Retrieve All User's Photos that are not approved.
                        //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

                        // Retrieve All User's Approved Photo's that are not Private and approved.
                        //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

                        var model = (from p in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(a => a.formattype_id == convertedformat && a.photo.lu_photoapprovalstatus != null
                            && a.photo.approvalstatus_id == convertedstatus).ToList()
                                     select new photoeditmodel
                                     {
                                         photoid = p.photo.id,
                                         profileid = p.photo.profile_id,
                                         screenname = p.photo.profilemetadata.profile.screenname,
                                         approved = (p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                         profileimagetype = p.photo.lu_photoimagetype.description,
                                         imagecaption = p.photo.imagecaption,
                                         creationdate = p.photo.creationdate,
                                         photostatusid = p.photo.lu_photostatus.id,
                                         checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                                     });


                        if (model.Count() > Convert.ToInt32(pagesize)) { pagesize = model.Count().ToString(); }

                        return (model.OrderByDescending(u => u.creationdate).Skip((Convert.ToInt16(page) - 1) * Convert.ToInt16(pagesize)).Take(Convert.ToInt16(pagesize))).ToList();
                           });
                    return await task.ConfigureAwait(false);

                    
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
             


     
            }
 
        }
        //12-10-2012 this also filters the format
        public  async Task<PhotoEditViewModel> getpagededitphotoviewmodelbyprofileidandformat(string profileid, string format, string page, string pagesize)
        {
            
         
            {
             
                try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            var convertedformat = Convert.ToInt16(format);
                            // var convertedphotoid = Guid.Parse(photoid);
                            var convertedprofileid = Convert.ToInt32(profileid);
                            //  var convertedstatus = Convert.ToInt16(status);

                            var myPhotos = _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(z => z.formattype_id == convertedformat && z.photo.profile_id == convertedprofileid).ToList();
                            var ApprovedPhotos = mediaextentionmethods.filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, Convert.ToInt16(page), Convert.ToInt16(pagesize)).ToList();
                            var NotApprovedPhotos = mediaextentionmethods.filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, Convert.ToInt16(page), Convert.ToInt16(pagesize));
                            //TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
                            //since this is edit mode that is fine
                            var PrivatePhotos = mediaextentionmethods.filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, Convert.ToInt16(page), Convert.ToInt16(pagesize));
                            var model = mediaextentionmethods.getphotoeditviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

                            return (model);

                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
               
             
            }
 
        }
       
        #endregion

        public async Task<AnewluvMessages> deleteuserphoto(string photoid)
        {

         
            {
               //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                
                        try
                        {


                            var task = Task.Factory.StartNew(() =>
                            {
                                // var convertedformat = Convert.ToInt16(format);
                                var convertedphotoid = Guid.Parse(photoid);
                                //var convertedprofileid = Convert.ToInt32(profileid);
                                // var convertedstatus = Convert.ToInt16(status);

                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == convertedphotoid).FirstOrDefault();
                                PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                // Update database
                                // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                              var i  =_unitOfWorkAsync.SaveChanges();
                               // transaction.Commit();
                                AnewluvMessages.messages.Add("photo deleted successfully");
                                return AnewluvMessages;
                            });
                            return await task.ConfigureAwait(false);

                        
                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                           // transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }

              


                 
                }
            }

        }

        public async Task<AnewluvMessages> makeuserphoto_private(string photoid)
        {
         
            {
               //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                      try
                        {

                          
                    var task = Task.Factory.StartNew(() =>
                    {
                                //var convertedformat = Convert.ToInt16(format);
                                var convertedphotoid = Guid.Parse(photoid);
                                // var convertedprofileid = Convert.ToInt32(profileid);
                                // var convertedstatus = Convert.ToInt16(status);

                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == convertedphotoid).FirstOrDefault();
                                PhotoModify.lu_photostatus.id = 1; //public values:1 or 2 are public values

                                if (PhotoModify.photo_securitylevel.Any(z => z.id != (int)securityleveltypeEnum.Private))
                                {
                                    PhotoModify.photo_securitylevel.Add(new photo_securitylevel
                                    {
                                        photo_id = convertedphotoid,
                                        lu_securityleveltype = _unitOfWorkAsync.Repository<lu_securityleveltype>().Queryable().Where(p => p.id == (int)securityleveltypeEnum.Private).FirstOrDefault()
                                    });
                                    // newsecurity.id = (int)securityleveltypeEnum.Private;
                                }

                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                // Update database
                                // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                                var i  =_unitOfWorkAsync.SaveChanges();
                               // transaction.Commit();
                                AnewluvMessages.messages.Add("photo privacy added");
                                return AnewluvMessages;
                           });
                    return await task.ConfigureAwait(false);

                         
                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                           // transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }
                  
                }
            }

        }

        public async Task<AnewluvMessages> makeuserphoto_public(string photoid)
        {
         
            {
               //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                 
                        try
                        {

                            var task = Task.Factory.StartNew(() =>
                            {
                                //var convertedformat = Convert.ToInt16(format);
                                var convertedphotoid = Guid.Parse(photoid);
                                // var convertedprofileid = Convert.ToInt32(profileid);
                                // var convertedstatus = Convert.ToInt16(status);

                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == convertedphotoid).FirstOrDefault();
                                PhotoModify.lu_photostatus.id = 1; //public values:1 or 2 are public values
                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                // Update database
                                // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                              var i  =_unitOfWorkAsync.SaveChanges();
                               // transaction.Commit();
                               AnewluvMessages.messages.Add("photo privacy removed");
                               return AnewluvMessages;

                            });
                            return await task.ConfigureAwait(false);

                            
                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                           // transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }

                   


                  
                }
            }


         
          
        }

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        public async Task<AnewluvMessages> addphotos(PhotoUploadViewModel model)
        {




            AnewluvResponse response = new AnewluvResponse();
            AnewluvMessages AnewluvMessage = new AnewluvMessages();
            var errormessages = new List<string>();
            ResponseMessage responsemessage = new ResponseMessage();
            int photosaddedcount = 0;

            //update method code
            //Test this with unit oof work as ut for now...
           using (var db = new AnewluvContext())  
          //  using (var db = _unitOfWorkAsync)  
            {
               //do not audit on adds
               
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                            {
                                
                                foreach (PhotoUploadModel item in model.photosuploaded)
                                {
                                    #region "inner code"

                                    photo NewPhoto = new photo();
                                    Guid identifier = Guid.NewGuid();
                                    NewPhoto.id = identifier;
                                    NewPhoto.size = (long)item.legacysize;
                                    NewPhoto.profile_id = model.profileid; //model.ProfileImage.Length;
                                    // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                                    NewPhoto.creationdate = item.creationdate;
                                    NewPhoto.imagecaption = item.caption;
                                    NewPhoto.imagename = item.imagename; //11-26-2012 olawal added the name for comparisons 
                                    // NewPhoto.size = item.size.GetValueOrDefault();                        
                                    //set the rest of the information as needed i.e approval status refecttion etc
                                    NewPhoto.lu_photoimagetype = (item.imagetypeid != null) ? _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.id == item.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                                    NewPhoto.imagetype_id = NewPhoto.lu_photoimagetype != null ? NewPhoto.lu_photoimagetype.id : (int?)null; 
                                   
                                    NewPhoto.lu_photoapprovalstatus = (item.approvalstatusid != null) ? _unitOfWorkAsync.Repository<lu_photoapprovalstatus>().Queryable().ToList().Where(p => p.id == item.approvalstatusid).FirstOrDefault() : null;
                                    NewPhoto.approvalstatus_id = NewPhoto.lu_photoapprovalstatus != null ? NewPhoto.lu_photoapprovalstatus.id : (int?)null; 
                                  
                                    NewPhoto.lu_photorejectionreason = (item.rejectionreasonid != null) ? _unitOfWorkAsync.Repository<lu_photorejectionreason>().Queryable().ToList().Where(p => p.id == item.rejectionreasonid).FirstOrDefault() : null;
                                    NewPhoto.rejectionreason_id = NewPhoto.lu_photorejectionreason != null ? NewPhoto.lu_photorejectionreason.id : (int?)null; 

                                    NewPhoto.lu_photostatus = (item.photostatusid != null) ? _unitOfWorkAsync.Repository<lu_photostatus>().Queryable().ToList().Where(p => p.id == item.photostatusid).FirstOrDefault() : null;
                                    NewPhoto.photostatus_id = NewPhoto.lu_photostatus != null ? NewPhoto.lu_photostatus.id : (int?)null; 

                                    var temp = addphotoconverionsb64string(NewPhoto, item, db);
                                    if (temp.Count > 0)
                                    {
                                        //existing conversions to compare with new one : 
                                        var existingthumbnailconversion = _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(z => z.photo.profile_id == model.profileid & z.lu_photoformat.id == (int)photoformatEnum.Thumbnail).ToList();
                                        var newphotothumbnailconversion = temp.Where(p => p.lu_photoformat.id == (int)photoformatEnum.Thumbnail).FirstOrDefault();
                                        if (existingthumbnailconversion.Any(p => p.size == newphotothumbnailconversion.size & p.image == newphotothumbnailconversion.image))
                                        {
                                            AnewluvMessage.messages.Add( "<br/>" + "This photo has already been uploaded:" + NewPhoto.imagename) ;
                                        }
                                        else
                                        {
                                            AnewluvMessage.messages.Add("<br/>" + "photo with name " + NewPhoto.imagecaption + "Has been uploaded");
                                            //allow saving of new photo 
                                            _unitOfWorkAsync.Repository<photo>().Insert(NewPhoto);
                                            int i2 =_unitOfWorkAsync.SaveChanges();
                                            photosaddedcount = +1;

                                            foreach (photoconversion convertedphoto in temp)
                                            {
                                                //if this does not recognise the photo object we might need to save that and delete it later
                                                _unitOfWorkAsync.Repository<photoconversion>().Insert(convertedphoto);
                                                //commit if no errors                               
                                              var i  =_unitOfWorkAsync.SaveChanges();      
                                            }
                                        }
                                    }
                                
                                                                                                 

                                    #endregion
                                }
                                
                                // responsemessage = new ResponseMessage("", message.message, "");
                                // response.ResponseMessages.Add(responsemessage);

                                if (photosaddedcount > 0)
                                {
                                   // transaction.Commit();
                                }

                                return AnewluvMessage;
                            });
                            return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)  //internal excetion for the indivual item
                    {

                        //add the error to message object
                        AnewluvMessage.errormessages.Add(ex.Message);
                        //just log and continue
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex);
                        //no need to throw heer wince we build the eror thing for them
                    }



                    // responsemessage = new ResponseMessage("", message.message, "");
                    // response.ResponseMessages.Add(responsemessage);
                    return AnewluvMessage;

                }
                 
            }

           
        
        }
        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public async Task<AnewluvMessages> addsinglephoto(PhotoUploadModel newphoto, string profileid)
        {

            //update method code
            using (var db = new AnewluvContext())
            {
               //do not audit on adds
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        #region "Inner Code"
                        try
                        {



                            //var convertedformat = Convert.ToInt16(format);
                           // var convertedphotoid = Guid.Parse(photoid);
                            var convertedprofileid = Convert.ToInt32(profileid);
                            //var convertedstatus = Convert.ToInt16(status);

                            AnewluvResponse response = new AnewluvResponse();
                            AnewluvMessages message = new AnewluvMessages();
                            ResponseMessage responsemessage = new ResponseMessage();

                            photo NewPhoto = new photo();
                            Guid identifier = Guid.NewGuid();
                            NewPhoto.id = identifier;
                            NewPhoto.profile_id = convertedprofileid; //model.ProfileImage.Length;
                            // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                            NewPhoto.creationdate = newphoto.creationdate;
                            NewPhoto.imagecaption = newphoto.caption;
                            NewPhoto.imagename = newphoto.imagename; //11-26-2012 olawal added the name for comparisons 
                            NewPhoto.size = newphoto.legacysize.GetValueOrDefault();
                            //set the rest of the information as needed i.e approval status refecttion etc
                            NewPhoto.lu_photoimagetype = (newphoto.imagetypeid != null) ? _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.id == newphoto.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                            NewPhoto.lu_photoapprovalstatus = (newphoto.approvalstatusid != null) ? _unitOfWorkAsync.Repository<lu_photoapprovalstatus>().Queryable().ToList().Where(p => p.id == newphoto.approvalstatusid).FirstOrDefault() : null;
                            NewPhoto.lu_photorejectionreason = (newphoto.rejectionreasonid != null) ? _unitOfWorkAsync.Repository<lu_photorejectionreason>().Queryable().ToList().Where(p => p.id == newphoto.rejectionreasonid).FirstOrDefault() : null;
                            NewPhoto.lu_photostatus = (newphoto.photostatusid != null) ? _unitOfWorkAsync.Repository<lu_photostatus>().Queryable().ToList().Where(p => p.id == newphoto.photostatusid).FirstOrDefault() : null;

                            var temp = addphotoconverionsb64string(NewPhoto, newphoto,db);
                            if (temp.Count > 0)
                            {
                                //existing conversions to compare with new one : 
                                var existingthumbnailconversion = _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(z => z.photo.profile_id == convertedprofileid & z.formattype_id == (int)photoformatEnum.Thumbnail).ToList();
                                var newphotothumbnailconversion = temp.Where(p => p.formattype_id == (int)photoformatEnum.Thumbnail).FirstOrDefault();
                                if (existingthumbnailconversion.Any(p => p.size == newphotothumbnailconversion.size & p.image == newphotothumbnailconversion.image))
                                {
                                    message.messages.Add("This photo has already been uploaded");
                                }
                                else
                                {
                                    //allow saving of new photo 
                                    _unitOfWorkAsync.Repository<photo>().Insert(NewPhoto);
                                    int i2 =_unitOfWorkAsync.SaveChanges();
                                    // _datingcontext.SaveChanges();
                                    foreach (photoconversion convertedphoto in temp)
                                    {
                                        //if this does not recognise the photo object we might need to save that and delete it later
                                        _unitOfWorkAsync.Repository<photoconversion>().Insert(convertedphoto);
                                    }
                                  var i  =_unitOfWorkAsync.SaveChanges();
                                   // transaction.Commit();
                                }

                                message.messages.Add("photo added succesfully ");
                            }

                            //return the response
                          //  responsemessage = new ResponseMessage("", message.message, "");
                           // response.ResponseMessages.Add(responsemessage);
                            return message;

                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                           // transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new  Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }

                        #endregion
                    });
                    return await task.ConfigureAwait(false);
                }


                     
            }
                    
        }

        //http://stackoverflow.com/questions/10484295/image-resizing-from-sql-database-on-the-fly-with-mvc2

        public async Task<bool> checkvalidjpggif(byte[] image)
        {

           
         
            {

               
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            using (MemoryStream ms = new MemoryStream(image))
                                Image.FromStream(ms);
                            return true;

                        });
                        return await task.ConfigureAwait(false);
                       
                    }
                    catch (ArgumentException)
                    {
                        return false;
                    }
                      
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
            }

          
          

        }
        
        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

        public async Task<string> getgalleryphotobyscreenname(string strscreenname, string format)
        {

           
         
            {

                 try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            //  var convertedphotoid = Guid.Parse(photoid);
                            //  var convertedprofileid = Convert.ToInt32(profileid);
                            // var convertedstatus = Convert.ToInt16(status);
                            var GalleryPhoto = (from p in _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.screenname == strscreenname).ToList()
                                                join f in _unitOfWorkAsync.Repository<photoconversion>().Queryable() on p.id equals f.photo.profile_id
                                                where (f.formattype_id == (int)convertedformat && f.photo.lu_photoapprovalstatus != null &&
                                                f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                                f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                                select f).FirstOrDefault();

                            //new code to only get the gallery conversion copy
                            //  return GalleryPhoto.conversions
                            // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image ;
                            return Convert.ToBase64String(GalleryPhoto.image);
                        });
                        return await task.ConfigureAwait(false);

                    
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
            
             
            }

         
        }

        public async Task<string> getgalleryimagebyphotoid(string photoid, string format)
        {

           
         
            {

              
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            var convertedphotoid = Guid.Parse(photoid);
                            //var convertedprofileid = Convert.ToInt32(profileid);
                            // var convertedstatus = Convert.ToInt16(status);

                            var GalleryPhoto = (from f in _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(f => f.photo.id == (convertedphotoid) &&
                             f.formattype_id == (int)convertedformat && f.photo.lu_photoapprovalstatus != null &&
                             f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                             f.photo.imagetype_id == (int)photostatusEnum.Gallery).ToList()
                                                select f).FirstOrDefault();

                            //new code to only get the gallery conversion copy
                            //return GalleryPhoto.conversions
                            // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                            return Convert.ToBase64String(GalleryPhoto.image);

                        });
                        return await task.ConfigureAwait(false);

                      
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
             

            }

           
        }
        //TO DO normalize name
       
       public async Task<string> getgalleryphotobyprofileid(string profileid, string format)
        {
           
         
            {

              
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            var convertedformat = Convert.ToInt16(format);
                            var convertedprofileid = Convert.ToInt32(profileid);


                            var GalleryPhoto = (from p in _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.id == convertedprofileid).ToList()
                                                join f in _unitOfWorkAsync.Repository<photoconversion>().Queryable() on p.id equals f.photo.profile_id
                                                where (f.formattype_id == (int)Convert.ToInt32(format) && f.photo.lu_photoapprovalstatus != null &&
                                                f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                                f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                                select f).FirstOrDefault();

                            if (GalleryPhoto != null)
                                return Convert.ToBase64String(GalleryPhoto.image);

                            return null;
                        });
                        return await task.ConfigureAwait(false);

                        
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
               

                
            }

     
           
        }

        //TO DO fix this code
        public async Task<string> getgalleryimagebynormalizedscreenname(string strScreenName, string format)
        {

           
         
            {

             
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            var test = "";
                            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
                            var GalleryPhoto = (from p in _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.screenname.Replace(" ", "") == strScreenName).ToList()
                                                join f in _unitOfWorkAsync.Repository<photoconversion>().Queryable() on p.id equals f.photo.profile_id
                                                where (f.formattype_id == (int)Convert.ToInt32(format) && f.photo.lu_photoapprovalstatus != null &&
                                                    f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                                    f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                                select f).FirstOrDefault();


                            //new code to only get the gallery conversion copy
                            //return GalleryPhoto.conversions
                            //.Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                            if (GalleryPhoto != null)
                                return Convert.ToBase64String(GalleryPhoto.image);
                            return null;
                        });
                        return await task.ConfigureAwait(false);

                     
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
              

               

                return null;
            }
           
         
        
        }

        public async Task<bool> checkifphotocaptionalreadyexists(string profileid, string strPhotoCaption)
        {

           
         
            {

               
                    try
                    {
                        
                    var task = Task.Factory.StartNew(() =>
                    {
                                var convertedprofileid = Convert.ToInt32(profileid);
                                var myPhotoList = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == convertedprofileid && p.imagecaption == strPhotoCaption).FirstOrDefault();
                                if (myPhotoList != null)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                           });
                    return await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
     


              
            }
        
          //  return _photorepo.checkifphotocaptionalreadyexists(convertedprofileid, strPhotoCaption);


           
        }

        public async Task<bool> checkforgalleryphotobyprofileid(string profileid)
        {

           
         
            {                
                      
                            try
                            {

                                var task = Task.Factory.StartNew(() =>
                                {
                                    var convertedprofileid = Convert.ToInt32(profileid);
                                    var GalleryPhoto = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == convertedprofileid &&
                                    p.lu_photoapprovalstatus != null && p.approvalstatus_id == (int)photoapprovalstatusEnum.Approved && p.imagetype_id == (int)photostatusEnum.Gallery).FirstOrDefault();

                                    if (GalleryPhoto != null)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                });
                                return await task.ConfigureAwait(false);

                                
                            }
                            catch (Exception ex)
                            {
                                //instantiate logger here so it does not break anything else.
                                logger = new  Logging(applicationEnum.MediaService);
                                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                                //can parse the error to build a more custom error mssage and populate fualt faultreason
                                FaultReason faultreason = new FaultReason("Error in photo service");
                                string ErrorMessage = "";
                                string ErrorDetail = "ErrorMessage: " + ex.Message;
                                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                            }
                   
            }


        }

        public async Task<bool> checkforuploadedphotobyprofileid(string profileid)
        {

           
         
            {
                                    
                        try
                        {

                            var task = Task.Factory.StartNew(() =>
                            {
                                var convertedprofileid = Convert.ToInt32(profileid);
                                IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
                                //Dim ctx As New Entities()
                                GalleryPhoto = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == convertedprofileid);

                                if (GalleryPhoto.Count() > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            });
                            return await task.ConfigureAwait(false);

                           
                        }
                        catch (Exception ex)
                        {
                            //instantiate logger here so it does not break anything else.
                            logger = new  Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }
                  
            }

          
        }

        //TO DO move to media extention methods or generic extentions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public async Task<string> getimageb64stringfromurl(string imageUrl, string source)
        {
           
         
            {
                try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            byte[] imageBytes;
                            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                            WebResponse imageResponse = imageRequest.GetResponse();
                            Stream responseStream = imageResponse.GetResponseStream();
                            using (BinaryReader br = new BinaryReader(responseStream))
                            {
                                imageBytes = br.ReadBytes(500000);
                                br.Close();
                            }
                            responseStream.Close();
                            imageResponse.Close();
                            return Convert.ToBase64String(imageBytes);
                        });
                        return await task.ConfigureAwait(false);

                        

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
              

               
            }
          
          
              


           

        }


    }
}
