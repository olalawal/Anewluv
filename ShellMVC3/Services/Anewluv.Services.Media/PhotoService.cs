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
using Anewluv.DataExtentionMethods;
using Anewluv.Services.Contracts.ServiceResponse;
using Shell.MVC2.Infrastructure.Helpers;
using System.IO;
using ImageResizer;
using Nmedia.DataAccess.Interfaces;
using System.Drawing;

using Nmedia.Infrastructure.Domain.Data.errorlog;
using Anewluv.Lib;

namespace Anewluv.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
    public class PhotoService : IPhotoService
    {


        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public PhotoService(IUnitOfWork unitOfWork)
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
            _unitOfWork = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
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
        private List<photoconversion> addphotoconverionsb64string(photo photo, PhotoUploadModel photouploaded)
        {
            //TemporaryImageUpload tempImageUpload = new TemporaryImageUpload();             
            // tempImageUpload = _service.GetImageData(id) ?? null;
            List<photoconversion> convertedphotos = new List<photoconversion>();

            if (photouploaded.imageb64string != null)
            {

                try
                {
                    var db = _unitOfWork;
                    var test = db.GetRepository<lu_photoformat>().Find().OfType<lu_photoformat>().ToList();
                    foreach (lu_photoformat currentformat in db.GetRepository<lu_photoformat>().Find().OfType<lu_photoformat>().ToList())
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
                                         lu_photoformat = currentformat,
                                        image = outBytes,
                                        size = outBytes.Length,
                                        photo_id = photo.id
                                    });

                                }
                                catch (ImageResizer.ImageMissingException missing)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, missing.Message, missing);
                                    new ErroLogging(logapplicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning,globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageCorruptedException cr)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, cr.Message, cr);
                                    new ErroLogging(logapplicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageProcessingException ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new ErroLogging(logapplicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (Exception ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new ErroLogging(logapplicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, convertedexcption, photo.profile_id, null);
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
                    //new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption, null, null);
                    throw convertedexcption;
                }

            }
            return convertedphotos;


        }
        #endregion



        #region "View Photo models"
        


        public PhotoModel getphotomodelbyphotoid(string photoid, string format)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                
                    try
                    {
                        PhotoModel model = (from p in
                                                (from r in db.GetRepository<photoconversion>().Find().Where(a => a.formattype_id == Convert.ToInt16(format) && (a.photo.id == Guid.Parse(photoid))).ToList()
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
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
                }

            
        }



        public PhotoModel getgalleryphotomodelbyprofileid(string profileid, string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                  return   db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(profileid, format);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
          
        }
        public List<PhotoModel> getphotomodelsbyprofileidandstatus(string profileid, string status, string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    // Retrieve All User's Photos that are not approved.
                    //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

                    // Retrieve All User's Approved Photo's that are not Private and approved.
                    //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

                    var model = (from p in db.GetRepository<photoconversion>().Find().Where(a => a.formattype_id == Convert.ToInt16(format) && a.photo.profile_id == Convert.ToInt32(profileid) &&
                        ((a.photo.lu_photoapprovalstatus != null && a.photo.approvalstatus_id == Convert.ToInt16(status)))).ToList()
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
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

           
        }
        public List<PhotoModel> getpagedphotomodelbyprofileidandstatus(string profileid, string status, string format, string page, string pagesize)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                  return   db.GetRepository<photoconversion>().getpagedphotomodelbyprofileidandstatus(profileid, status,  format,  page,  pagesize);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
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
        public PhotoViewModel getpagedphotoviewmodelbyprofileid(string profileid, string format, string page, string pagesize)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return null;
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
      
        }



        #endregion

        #region "Edititable Photo models

        public photoeditmodel getphotoeditmodelbyphotoid(string photoid, string format)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {
                    photoeditmodel model = (from p in db.GetRepository<photoconversion>().Find().Where(p => p.formattype_id == Convert.ToInt16(format) && p.photo.id == Guid.Parse(photoid)).ToList()
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
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
       }

        public List<photoeditmodel> getphotoeditmodelsbyprofileidandstatus(string profile_id, string status, string format)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    var model = (from p in db.GetRepository<photoconversion>().Find().Where(a => a.formattype_id == Convert.ToInt16( format)
                        && a.photo.lu_photoapprovalstatus != null && a.photo.approvalstatus_id ==  Convert.ToInt16(status)).ToList()
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

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
           

        }

        public List<photoeditmodel> getpagedphotoeditmodelsbyprofileidstatus(string profile_id, string status, string format,
                                                              string page, string pagesize)
        {


            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {
                    // Retrieve All User's Photos that are not approved.
                    //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

                    // Retrieve All User's Approved Photo's that are not Private and approved.
                    //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

                    var model = (from p in db.GetRepository<photoconversion>().Find().Where(a => a.formattype_id == Convert.ToInt16(format) && a.photo.lu_photoapprovalstatus != null
                        && a.photo.approvalstatus_id == Convert.ToInt16(status)).ToList()
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


                    if (model.Count() > Convert.ToInt32(pagesize)) {pagesize = model.Count().ToString(); }

                    return (model.OrderByDescending(u => u.creationdate).Skip((Convert.ToInt16(page) - 1) * Convert.ToInt16(pagesize)).Take(Convert.ToInt16(pagesize))).ToList();
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
 
        }

        //12-10-2012 this also filters the format
        public PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(string profileid, string format, string page, string pagesize)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var myPhotos = db.GetRepository<photoconversion>().Find().Where(z => z.formattype_id == Convert.ToInt16(format) && z.photo.profile_id == Convert.ToInt32(profileid)).ToList();
                    var ApprovedPhotos = mediaextentionmethods.filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, Convert.ToInt16(page), Convert.ToInt16(pagesize)).ToList();
                    var NotApprovedPhotos = mediaextentionmethods.filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, Convert.ToInt16(page), Convert.ToInt16(pagesize));
                    //TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
                    //since this is edit mode that is fine
                    var PrivatePhotos = mediaextentionmethods.filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, Convert.ToInt16(page), Convert.ToInt16(pagesize));
                    var model = mediaextentionmethods.getphotoeditviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

                    return (model);


                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
 
        }



        #endregion


        public AnewluvMessages deleteuserphoto(string photoid)
        {

            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Retrieve single value from photos table
                        photo PhotoModify = db.GetRepository<photo>().FindSingle(u => u.id == Guid.Parse(photoid));
                        PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                        db.Update(PhotoModify);
                        // Update database
                        // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                        int i = db.Commit();
                        transaction.Commit();
                        return new AnewluvMessages { message = "photo deleted successfully" };
                    }
                    catch (Exception ex)
                    {
                        //TO DO track the transaction types only rollback on DB connections
                        //rollback transaction
                        transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        }
        public AnewluvMessages makeuserphoto_private(string photoid)
        {


            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Retrieve single value from photos table
                        photo PhotoModify = db.GetRepository<photo>().FindSingle(u => u.id == Guid.Parse(photoid));
                        PhotoModify.lu_photostatus.id = 1; //public values:1 or 2 are public values

                        if (PhotoModify.photo_securitylevel.Any(z => z.id != (int)securityleveltypeEnum.Private))
                        {
                            PhotoModify.photo_securitylevel.Add(new photo_securitylevel
                            {
                                photo_id = Guid.Parse(photoid),
                                lu_securityleveltype = db.GetRepository<lu_securityleveltype>().FindSingle(p => p.id == (int)securityleveltypeEnum.Private)
                            });
                            // newsecurity.id = (int)securityleveltypeEnum.Private;
                        }

                        db.Update(PhotoModify);
                        // Update database
                        // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                        int i = db.Commit();
                        transaction.Commit();
                        return new AnewluvMessages { message = "photo privacy added" };
                    }
                    catch (Exception ex)
                    {
                        //TO DO track the transaction types only rollback on DB connections
                        //rollback transaction
                        transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        }
        public AnewluvMessages makeuserphoto_public(string photoid)
        {


            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // Retrieve single value from photos table
                        photo PhotoModify = db.GetRepository<photo>().FindSingle(u => u.id ==  Guid.Parse(photoid));
                        PhotoModify.lu_photostatus.id = 1; //public values:1 or 2 are public values
                        db.Update(PhotoModify);
                        // Update database
                        // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                        int i = db.Commit();
                        transaction.Commit();
                        return new AnewluvMessages { message = "photo privacy removed" };
                    }
                          catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                            transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new ErroLogging(logapplicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
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
        public AnewluvMessages addphotos(PhotoUploadViewModel model)
        {

            AnewluvResponse response = new AnewluvResponse();
            AnewluvMessages AnewluvMessage = new AnewluvMessages();
            var errormessages = new List<string>();
            ResponseMessage responsemessage = new ResponseMessage();

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                db.DisableProxyCreation = true;
                using (var transaction = db.BeginTransaction())
                {

                    foreach (PhotoUploadModel item in model.photosuploaded)
                    {
                        try
                        {

                            photo NewPhoto = new photo();
                            Guid identifier = Guid.NewGuid();
                            NewPhoto.id = identifier;
                            NewPhoto.profile_id = model.profileid; //model.ProfileImage.Length;
                            // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                            NewPhoto.creationdate = item.creationdate;
                            NewPhoto.imagecaption = item.caption;
                            NewPhoto.imagename = item.imagename; //11-26-2012 olawal added the name for comparisons 
                            // NewPhoto.size = item.size.GetValueOrDefault();                        
                            //set the rest of the information as needed i.e approval status refecttion etc
                            NewPhoto.lu_photoimagetype = (item.imagetypeid != null) ? db.GetRepository<lu_photoimagetype>().Find().ToList().Where(p => p.id == item.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                            NewPhoto.lu_photoapprovalstatus = (item.approvalstatusid != null) ? db.GetRepository<lu_photoapprovalstatus>().Find().ToList().Where(p => p.id == item.approvalstatusid).FirstOrDefault() : null;
                            NewPhoto.lu_photorejectionreason = (item.rejectionreasonid != null) ? db.GetRepository<lu_photorejectionreason>().Find().ToList().Where(p => p.id == item.rejectionreasonid).FirstOrDefault() : null;
                            NewPhoto.lu_photostatus = (item.photostatusid != null) ? db.GetRepository<lu_photostatus>().Find().ToList().Where(p => p.id == item.photostatusid).FirstOrDefault() : null;

                            var temp = addphotoconverionsb64string(NewPhoto, item);
                            if (temp.Count > 0)
                            {

                                //existing conversions to compare with new one : 
                                var existingthumbnailconversion = db.GetRepository<photoconversion>().Find().Where(z => z.photo.profile_id == model.profileid & z.lu_photoformat.id == (int)photoformatEnum.Thumbnail).ToList();
                                var newphotothumbnailconversion = temp.Where(p => p.lu_photoformat.id == (int)photoformatEnum.Thumbnail).FirstOrDefault();
                                if (existingthumbnailconversion.Any(p => p.size == newphotothumbnailconversion.size & p.image == newphotothumbnailconversion.image))
                                {
                                    AnewluvMessage.message =    AnewluvMessage.message + "<br/>" + "This photo has already been uploaded" ;
                                }
                                else
                                {
                                        AnewluvMessage.message =    AnewluvMessage.message + "<br/>" + "photo with name " + NewPhoto.imagecaption + "Has been uploaded" ;
                                    //allow saving of new photo 
                                    db.Add(NewPhoto);
                                    int i2 = db.Commit();

                                    foreach (photoconversion convertedphoto in temp)
                                    {
                                        //if this does not recognise the photo object we might need to save that and delete it later
                                        db.Add(convertedphoto);
                                    }

                                }
                            }
                        }
                        catch (Exception ex)  //internal excetion for the indivual item
                        { 
                         
                            //add the error to message object
                            AnewluvMessage.errormessages.Add(ex.Message );
                            //just log and continue
                            //instantiate logger here so it does not break anything else.
                            logger = new ErroLogging(logapplicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex);
                          //no need to throw heer wince we build the eror thing for them
                        }

                        //commit if no errors 
                        try
                        {
                            int i = db.Commit();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                            transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new ErroLogging(logapplicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in photo service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }
                    }



                }
            }

           // responsemessage = new ResponseMessage("", message.message, "");
           // response.ResponseMessages.Add(responsemessage);
            return AnewluvMessage;
        
        }
        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public AnewluvMessages addsinglephoto(PhotoUploadModel newphoto, string profileid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        AnewluvResponse response = new AnewluvResponse();
                        AnewluvMessages message = new AnewluvMessages();
                        ResponseMessage responsemessage = new ResponseMessage();

                        photo NewPhoto = new photo();
                        Guid identifier = Guid.NewGuid();
                        NewPhoto.id = identifier;
                        NewPhoto.profile_id = Convert.ToInt32(profileid); //model.ProfileImage.Length;
                        // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                        NewPhoto.creationdate = newphoto.creationdate;
                        NewPhoto.imagecaption = newphoto.caption;
                        NewPhoto.imagename = newphoto.imagename; //11-26-2012 olawal added the name for comparisons 
                        NewPhoto.size = newphoto.legacysize.GetValueOrDefault();
                        //set the rest of the information as needed i.e approval status refecttion etc
                        NewPhoto.lu_photoimagetype = (newphoto.imagetypeid != null) ? db.GetRepository<lu_photoimagetype>().Find().ToList().Where(p => p.id == newphoto.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                        NewPhoto.lu_photoapprovalstatus = (newphoto.approvalstatusid != null) ? db.GetRepository<lu_photoapprovalstatus>().Find().ToList().Where(p => p.id == newphoto.approvalstatusid).FirstOrDefault() : null;
                        NewPhoto.lu_photorejectionreason = (newphoto.rejectionreasonid != null) ? db.GetRepository<lu_photorejectionreason>().Find().ToList().Where(p => p.id == newphoto.rejectionreasonid).FirstOrDefault() : null;
                        NewPhoto.lu_photostatus = (newphoto.photostatusid != null) ? db.GetRepository<lu_photostatus>().Find().ToList().Where(p => p.id == newphoto.photostatusid).FirstOrDefault() : null;

                        var temp = addphotoconverionsb64string(NewPhoto, newphoto);
                        if (temp.Count > 0)
                        {

                            //existing conversions to compare with new one : 
                            var existingthumbnailconversion = db.GetRepository<photoconversion>().Find().Where(z => z.photo.profile_id == Convert.ToInt32(profileid) & z.formattype_id == (int)photoformatEnum.Thumbnail).ToList();
                            var newphotothumbnailconversion = temp.Where(p => p.formattype_id == (int)photoformatEnum.Thumbnail).FirstOrDefault();
                            if (existingthumbnailconversion.Any(p => p.size == newphotothumbnailconversion.size & p.image == newphotothumbnailconversion.image))
                            {

                                message.message = "This photo has already been uploaded";
                            }
                            else
                            {

                                //allow saving of new photo 
                                db.Add(NewPhoto);
                                int i2 = db.Commit();

                                // _datingcontext.SaveChanges();

                                foreach (photoconversion convertedphoto in temp)
                                {
                                    //if this does not recognise the photo object we might need to save that and delete it later
                                    db.Add(convertedphoto);
                                }

                                int i = db.Commit();
                                transaction.Commit();


                            }

                            message.message = "photo added succesfully ";
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
                        transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
                }



            }
        }

        //http://stackoverflow.com/questions/10484295/image-resizing-from-sql-database-on-the-fly-with-mvc2
    
        public bool checkvalidjpggif(byte[] image)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(image))
                        Image.FromStream(ms);
                }
                catch (ArgumentException)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

          
            return true;

        }
        
        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

         public string getgalleryphotobyscreenname(string strscreenname,string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var GalleryPhoto = (from p in db.GetRepository<profile>().Find().Where(p => p.screenname == strscreenname).ToList()
                                        join f in db.GetRepository<photoconversion>().Find() on p.id equals f.photo.profile_id
                                        where (f.formattype_id == (int)Convert.ToInt16(format) && f.photo.lu_photoapprovalstatus != null &&
                                        f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                        f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();

                    //new code to only get the gallery conversion copy
                    //  return GalleryPhoto.conversions
                    // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image ;
                    return Convert.ToBase64String(GalleryPhoto.image);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

         
        }

         public string getgalleryimagebyphotoid(string photoid, string format)
        {

            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {

            var GalleryPhoto = (from f in db.GetRepository<photoconversion>().Find().Where(f => f.photo.id == (Guid.Parse(photoid)) &&
             f.formattype_id == (int) Convert.ToInt16(format) && f.photo.lu_photoapprovalstatus != null &&
             f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
             f.photo.imagetype_id == (int)photostatusEnum.Gallery).ToList()
                                        select f).FirstOrDefault();

                    //new code to only get the gallery conversion copy
                    //return GalleryPhoto.conversions
                    // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                    return Convert.ToBase64String(GalleryPhoto.image);

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

           
        }
        //TO DO normalize name
         public string getgalleryphotobyprofileid(string profileid, string format)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {

                    var GalleryPhoto = (from p in db.GetRepository<profile>().Find().Where(p => p.id == Convert.ToInt32(profileid)).ToList()
                                        join f in db.GetRepository<photoconversion>().Find() on p.id equals f.photo.profile_id
                                        where (f.formattype_id == (int)Convert.ToInt32(format) && f.photo.lu_photoapprovalstatus != null &&
                                        f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                        f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();

                    if (GalleryPhoto != null)
                        return Convert.ToBase64String(GalleryPhoto.image);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            return null;
           
        }

        //TO DO fix this code
         public string getgalleryimagebynormalizedscreenname(string strScreenName, string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var test = "";
                    // string strProfileID = this.getprofileidbyscreenname(strScreenName);
                    var GalleryPhoto = (from p in db.GetRepository<profile>().Find().Where(p => p.screenname.Replace(" ", "") == strScreenName).ToList()
                                        join f in db.GetRepository<photoconversion>().Find() on p.id equals f.photo.profile_id
                                        where (f.formattype_id == (int)Convert.ToInt32(format) && f.photo.lu_photoapprovalstatus != null &&
                                            f.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved &&
                                            f.photo.imagetype_id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();


                    //new code to only get the gallery conversion copy
                    //return GalleryPhoto.conversions
                    //.Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                    if (GalleryPhoto != null)
                        return Convert.ToBase64String(GalleryPhoto.image);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

                return null;
            }
           
         
        
        }

        public bool checkifphotocaptionalreadyexists(string profileid, string strPhotoCaption)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var myPhotoList = db.GetRepository<photo>().Find().Where(p => p.profile_id == Convert.ToInt32(profileid) && p.imagecaption == strPhotoCaption).FirstOrDefault();

                    if (myPhotoList != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        
          //  return _photorepo.checkifphotocaptionalreadyexists(Convert.ToInt32(profileid), strPhotoCaption);


           
        }

        public bool checkforgalleryphotobyprofileid(string profileid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var GalleryPhoto = db.GetRepository<photo>().Find().Where(p => p.profile_id == Convert.ToInt32(profileid) &&
                p.lu_photoapprovalstatus != null &&   p.approvalstatus_id == (int)photoapprovalstatusEnum.Approved && p.imagetype_id == (int)photostatusEnum.Gallery).FirstOrDefault();

                    if (GalleryPhoto != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }


        }

        public bool checkforuploadedphotobyprofileid(string profileid)
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
                    //Dim ctx As New Entities()
                    GalleryPhoto = db.GetRepository<photo>().Find().Where(p => p.profile_id == Convert.ToInt32(profileid));

                    if (GalleryPhoto.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
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
        public string getimageb64stringfromurl(string imageUrl, string source)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
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

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex);
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
