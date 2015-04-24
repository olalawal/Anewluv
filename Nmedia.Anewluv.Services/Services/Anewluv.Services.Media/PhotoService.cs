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
using Nmedia.Infrastructure;
using Nmedia.Infrastructure.Domain.Data.Notification;

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
                                        creationdate =photo.creationdate,
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

    

        #region "main Query methods"

       //This should be the only method used to get photos
       public async Task<PhotoSearchResultsViewModel> getfilteredphotospaged(PhotoModel model)
       {
           {
               try
               {
                   var task = Task.Factory.StartNew(() =>
                   {
                       var repo = _unitOfWorkAsync.Repository<photoconversion>();
                       var dd = mediaextentionmethods.getfilteredphotospaged
                           (repo,model);

                       return dd;
 

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
       public async Task<PhotoViewModel> getfilteredphoto(PhotoModel model)
       {
           {
               try
               {
                   var task = Task.Factory.StartNew(() =>
                   {
                       var repo = _unitOfWorkAsync.Repository<photoconversion>();
                       var dd = mediaextentionmethods.getfilteredphoto
                           (repo,model);

                       return dd;


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
       //this is the method used to get albums and the ids      
       public async Task<List<PhotoAlbumViewModel>> getphotoalbumlistbyprofileid(PhotoModel model)
       {

           {
               try
               {

                   var task = Task.Factory.StartNew(() =>
                   {
                       
                       // string strProfileID = this.getprofileidbyscreenname(strScreenName);
                       var albums = (from p in _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.id == model.profileid).ToList()
                                     join f in _unitOfWorkAsync.Repository<photoalbum>().Queryable() on p.id equals f.profile_id
                                     select new PhotoAlbumViewModel
                                     {
                                         active = true,
                                         description = f.description,
                                         id = f.id

                                     }).ToList();


                       return albums;
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
     
       
       #endregion

        #region "Edit User methods"

       //For now only caption can be edited by user 
        public async Task<AnewluvMessages> edituserphoto(PhotoModel model)
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
                                                         

                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == model.photoid && u.profile_id == model.profileid).FirstOrDefault();

                                //remove all secruity otptions if its public
                                if (model.photocaption != null & model.photocaption != PhotoModify.imagecaption)
                                {

                                    PhotoModify.imagecaption = model.photocaption;
                                    //TO DO add modify date
                                    _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                    var i = _unitOfWorkAsync.SaveChanges();
                                    AnewluvMessages.messages.Add("Photo caption changed!");
                                }
                                else
                                {
                                    // transaction.Commit();
                                    AnewluvMessages.errormessages.Add("Only the caption of a photo can be changed please select a new caption");                                
                                }
                           
                              


                            
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
       public async Task<AnewluvMessages> deleteuserphotos(PhotoModel model)
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
                            foreach (Guid photoid in model.photoids)
                            {
                                // var  model.photoformat = model. model.photoformat);
                                //var photoid = model.photoid;
                                //var convertedprofileid = Convert.ToInt32(model.profileid);
                                // var convertedstatus =  model.photostatus);

                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == model.photoid && u.profile_id == model.profileid).FirstOrDefault();
                                PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                // Update database
                                // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                                var i = _unitOfWorkAsync.SaveChanges();
                                // transaction.Commit();
                                AnewluvMessages.messages.Add("photo deleted successfully");
                               
                            
                            }
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
        public async Task<AnewluvMessages> deleteuserphoto(PhotoModel model)
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
                            // var  model.photoformat = model. model.photoformat);
                            //var photoid = model.photoid;
                            //var convertedprofileid = Convert.ToInt32(model.profileid);
                            // var convertedstatus =  model.photostatus);

                            // Retrieve single value from photos table
                            photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == model.photoid).FirstOrDefault();
                            PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                            _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                            // Update database
                            // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                            var i = _unitOfWorkAsync.SaveChanges();
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
        public async Task<AnewluvMessages> updatephotossecuritylevel(PhotoModel model)
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
                            //var  model.photoformat = model. model.photoformat);
                            //  var photoid = Guid.Parse(photoid);
                            // var convertedprofileid = Convert.ToInt32(model.profileid);
                            // var convertedstatus =  model.photostatus);
                            foreach (Guid photoid in model.photoids)
                            {
                                // Retrieve single value from photos table
                                photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Query(u => u.id == photoid && u.profile_id ==model.profileid)
                                    .Include(z=>z.photo_securitylevel.Select(f=>f.lu_securityleveltype)).Select()                                    
                                    .FirstOrDefault();                             


                               //remove all secruity otptions if its public
                                if (model.makepublic!=null & model.makepublic == true)
                                {
                                    foreach (photo_securitylevel securitylevel in PhotoModify.photo_securitylevel.ToList())
                                    {
                                        var secruityleveltodelete = _unitOfWorkAsync.Repository<photo_securitylevel>().Query(p => p.id == securitylevel.id).Select().FirstOrDefault();
                                        _unitOfWorkAsync.Repository<photo_securitylevel>().Delete(secruityleveltodelete);
                                        // newsecurity.id = (int)securityleveltypeEnum.Private;
                                    }
                                    _unitOfWorkAsync.SaveChanges();
                                    AnewluvMessages.messages.Add("photo security removed for selected photos");
                                }
                                else if (model.photosecuritylevelid != null)
                                {
                                    foreach (photo_securitylevel securitylevel in PhotoModify.photo_securitylevel.ToList())
                                    {
                                        var existingsecuritylevel = _unitOfWorkAsync.Repository<photo_securitylevel>().Query(p => p.id == securitylevel.id).Select().FirstOrDefault();

                                        if (existingsecuritylevel == null)
                                        {
                                            //add it only if it exists

                                            var newsecuritylevel = new photo_securitylevel
                                            {
                                                photo_id = model.photoid.Value,
                                                securityleveltype_id = model.photosecuritylevelid,
                                                lu_securityleveltype = _unitOfWorkAsync.Repository<lu_securityleveltype>().Queryable().Where(p => p.id == model.photosecuritylevelid).FirstOrDefault()
                                            };
                                            _unitOfWorkAsync.Repository<photo_securitylevel>().Insert(newsecuritylevel);
                                        }

                                    }

                                    _unitOfWorkAsync.SaveChanges();
                                    AnewluvMessages.messages.Add("photo scurity added removed for selected photos");
                                }
                                else
                                {
                                    AnewluvMessages.errormessages.Add("photosecuritylevelid or makepublic or photoids are empty , cannot modify security level");
                                }

                            
                            }
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
               
        #endregion

        #region "Edit Methods for Admin role"

        public async Task<AnewluvMessages> editphotostatus(PhotoModel model)
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
                            bool updated = false;
                            //get the profile role
                           var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = model.profileid.Value });
                                
                            //if user is in admin role
                            if (profile !=null && profile.membersinroles.Any(z=>z.role_id == (int)roleEnum.Admin && z.active != false && z.roleexpiredate > DateTime.Now))
                            {
                                // Retrieve single value from photos table
                            photo PhotoModify = _unitOfWorkAsync.Repository<photo>().Queryable().Where(u => u.id == model.photoid).FirstOrDefault();

                            //test to see if photo was rejected
                             if (model.photorejectionreasonid != null && model.photoapproved == false | model.photoapproved == null)
                             {
                                 updated = true;
                                 PhotoModify.approvalstatus_id = (int)photoapprovalstatusEnum.Rejected;
                                 PhotoModify.rejectionreason_id = model.photorejectionreasonid.Value;                                
                                //TO DO add modify date
                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);                                   
                                    
                                //add the action as well to trck the admins work 
                                var newaction = new action();
                                var newnote = new note();

                                newaction.creator_profile_id = model.profileid.Value;
                                newaction.target_profile_id =  PhotoModify.profile_id;
                                newaction.actiontype_id = (int)actiontypeEnum.PhotoReject;
                                //TO DO add notes if posible
                                if (model.photoapprovalrejectnote !="")
                                {
                                    //To do change this to photo rejecttion note for filtering
                                    newaction.notes.Add(new note { action_id = newaction.id, notedetail = model.photoapprovalrejectnote, creationdate = DateTime.Now, notetype_id = (int)notetypeEnum.AdminPhotoRejectionNote });
                                }
                                newaction.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                                _unitOfWorkAsync.Repository<action>().Insert(newaction);
                                AnewluvMessages.errormessages.Add("Photo Rejected");

                                //member 
                                var EmailViewModel = new EmailViewModel
                                {
                                    memberEmailViewModel = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberPhotoRejectedMemberNotification,
                                        messagetypeid = (int)messagetypeenum.UserUpdate,
                                        addresstypeid = (int)addresstypeenum.SiteUser,
                                        emailaddress =PhotoModify.profilemetadata.profile.emailaddress,
                                        screename = PhotoModify.profilemetadata.profile.screenname,
                                        //subject = templatesubjectenum.MemberPhotoRejectedMemberNotification.ToDescription()
                                    },
                                    adminEmailViewModel = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberPhotoRejectedAdminNotification,
                                        messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                        addresstypeid = (int)addresstypeenum.SystemAdmin,
                                      //  subject = templatesubjectenum.MemberRecivedEmailMessageAdminNotification.ToDescription()
                                    }
                                };

                                //this sends both admin and user emails  
                                Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);

                                    
                                }
                             else if (model.photoapproved == true)
                            {
                                updated = true;
                                PhotoModify.approvalstatus_id = (int)photoapprovalstatusEnum.Approved;
                                //TO DO add modify date
                                _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);

                                //add the action as well to trck the admins work 
                                var newaction = new action();
                                var newnote = new note();

                                newaction.creator_profile_id = model.profileid.Value;
                                newaction.target_profile_id = PhotoModify.profile_id;
                                newaction.actiontype_id = (int)actiontypeEnum.PhotoAprove;
                                //TO DO add notes if posible
                                if (model.photoapprovalrejectnote != "")
                                {
                                    //To do change this to photo rejecttion note for filtering
                                    newaction.notes.Add(new note { action_id = newaction.id, notedetail = model.photoapprovalrejectnote, creationdate = DateTime.Now, notetype_id = (int)notetypeEnum.AdminPhotoRejectionNote });
                                }
                                newaction.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                                _unitOfWorkAsync.Repository<action>().Insert(newaction);
                                AnewluvMessages.errormessages.Add("Photo Approved");

                                //member 
                                var EmailViewModel = new EmailViewModel
                                {
                                   
                                    adminEmailViewModel = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberPhotoApprovedAdminNotification,
                                        messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                        addresstypeid = (int)addresstypeenum.SystemAdmin,
                                       // subject = templatesubjectenum.MemberPhotoApprovedAdminNotification.ToDescription()
                                    }
                                };

                                Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);
                            }

                            else
                            {
                                // transaction.Commit();
                                AnewluvMessages.errormessages.Add("Only the caption of a photo can be changed please select a new caption");
                            }


                            var i = _unitOfWorkAsync.SaveChanges();
                            AnewluvMessages.messages.Add("Photo caption changed!");
                            
                             }
                             else
                            {
                                // transaction.Commit();
                                AnewluvMessages.errormessages.Add("You must be an adminstrator to modify member photos !");
                            }





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

        /// <summary>
        /// allows for getting photos that are not approved yet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PhotoSearchResultsViewModel> getadminfilteredphotospaged(PhotoModel model)
        { 

        var task = Task.Factory.StartNew(() =>
                            {
                                return new PhotoSearchResultsViewModel();
                            });
        return await task.ConfigureAwait(false);
       
        
        }

        #endregion

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        public async Task<AnewluvMessages> addphotos(PhotoModel model)
        {

            AnewluvResponse response = new AnewluvResponse();
            AnewluvMessages AnewluvMessage = new AnewluvMessages();
            var errormessages = new List<string>();
            ResponseMessage responsemessage = new ResponseMessage();
            int photosaddedcount = 0;

                       
            {

                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                            {

                                foreach (PhotoUploadModel item in model.photostoupload)
                                {
                                    #region "inner code"

                                    photo NewPhoto = new photo();
                                    Guid identifier = Guid.NewGuid();
                                    NewPhoto.id = identifier;
                                    NewPhoto.size = (long)item.legacysize;
                                    NewPhoto.profile_id = model.profileid.Value; //model.ProfileImage.Length;
                                    // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                                    NewPhoto.creationdate = item.creationdate;
                                    NewPhoto.imagecaption = item.caption;
                                    NewPhoto.imagename = item.imagename; //11-26-2012 olawal added the name for comparisons 
                                    // NewPhoto.size = item.size.GetValueOrDefault();                        
                                    //set the rest of the information as needed i.e approval status refecttion etc

                                    NewPhoto.lu_photoimagetype = (item.imagetypedescription != "") ?
                                    _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.description.ToUpper().Contains(item.imagetypedescription.ToUpper())).FirstOrDefault() :
                                    _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.id == (int)photoimagetypeEnum.other).FirstOrDefault(); 

                                    NewPhoto.imagetype_id = NewPhoto.lu_photoimagetype != null ? NewPhoto.lu_photoimagetype.id : (int?)null;

                                    NewPhoto.approvalstatus_id = (int)photoapprovalstatusEnum.NotReviewed;
                                    NewPhoto.rejectionreason_id = null;
                                    NewPhoto.photostatus_id = (int)photostatusEnum.Nostatus;

                                    var temp = addphotoconverionsb64string(NewPhoto, item);
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
                                    //update admins 
                                    //member 
                                    var EmailViewModel = new EmailViewModel
                                    {
                                       
                                        adminEmailViewModel = new EmailModel
                                        {
                                            templateid = (int)templateenum.MemberPhotoUploadedAdminNotification,
                                            messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                            addresstypeid = (int)addresstypeenum.SystemAdmin//,
                                          //  subject = templatesubjectenum.MemberPhotoUploadedAdminNotification.ToDescription()
                                        }
                                    };
                                    Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);
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
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }



                    // responsemessage = new ResponseMessage("", message.message, "");
                    // response.ResponseMessages.Add(responsemessage);
                    return AnewluvMessage;
               }
                 
 }

        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public async Task<AnewluvMessages> addsinglephoto(PhotoModel model)
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

                            var newphoto = model.singlephototoupload;

                            //var  model.photoformat = model. model.photoformat);
                           // var photoid = Guid.Parse(photoid);
                          //  var convertedprofileid = Convert.ToInt32(model.profileid);
                            //var convertedstatus =  model.photostatus);

                            AnewluvResponse response = new AnewluvResponse();
                            AnewluvMessages message = new AnewluvMessages();
                            ResponseMessage responsemessage = new ResponseMessage();

                            photo NewPhoto = new photo();
                            Guid identifier = Guid.NewGuid();
                            NewPhoto.id = identifier;
                            NewPhoto.profile_id =  model.profileid.Value; //model.ProfileImage.Length;
                            // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                            NewPhoto.creationdate = newphoto.creationdate;
                            NewPhoto.imagecaption = newphoto.caption;
                            NewPhoto.imagename = newphoto.imagename; //11-26-2012 olawal added the name for comparisons 
                            NewPhoto.size = newphoto.legacysize.GetValueOrDefault();
                            //set the rest of the information as needed i.e approval status refecttion etc


                            NewPhoto.lu_photoimagetype = (newphoto.imagetypedescription != "") ?
                            _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.description.ToUpper().Contains(newphoto.imagetypedescription.ToUpper())).FirstOrDefault() :
                            _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.id == (int)photoimagetypeEnum.other).FirstOrDefault();

                            NewPhoto.imagetype_id = NewPhoto.lu_photoimagetype != null ? NewPhoto.lu_photoimagetype.id : (int?)null;

                            NewPhoto.approvalstatus_id = (int)photoapprovalstatusEnum.NotReviewed;
                            NewPhoto.rejectionreason_id = null;
                            NewPhoto.photostatus_id = (int)photostatusEnum.Nostatus;

                            var temp = addphotoconverionsb64string(NewPhoto, newphoto);
                            if (temp.Count > 0)
                            {
                                //existing conversions to compare with new one : 
                                var existingthumbnailconversion = _unitOfWorkAsync.Repository<photoconversion>().Queryable().Where(z => z.photo.profile_id == model.profileid & z.formattype_id == (int)photoformatEnum.Thumbnail).ToList();
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

                                //update admins 
                                //member 
                                var EmailViewModel = new EmailViewModel
                                {

                                     adminEmailViewModel  = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberPhotoUploadedAdminNotification,
                                        messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                        addresstypeid = (int)addresstypeenum.SystemAdmin
                                       // subject = templatesubjectenum.MemberPhotoUploadedAdminNotification.ToDescription()
                                    }
                                };
                                Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);

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
        public async Task<bool> checkvalidjpggif(PhotoModel model)
        {          
         
            {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            using (MemoryStream ms = new MemoryStream(model.image))
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
        //TO DO fix this code     
        public async Task<bool> checkifphotocaptionalreadyexists(PhotoModel model)
        {
            {
                    try
                    {
                        
                    var task = Task.Factory.StartNew(() =>
                    {
                                var convertedprofileid = Convert.ToInt32(model.profileid);
                                var myPhotoList = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == convertedprofileid && p.imagecaption == model.photocaption).FirstOrDefault();
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
        public async Task<bool> checkforgalleryphotobyprofileid(PhotoModel model)
        {           
         
            {               
                      
                            try
                            {

                                var task = Task.Factory.StartNew(() =>
                                {
                                    var convertedprofileid = Convert.ToInt32(model.profileid);
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
        public async Task<bool> checkforuploadedphotobyprofileid(PhotoModel model)
        {           
         
            {
                                    
                        try
                        {

                            var task = Task.Factory.StartNew(() =>
                            {
                                var convertedprofileid = Convert.ToInt32(model.profileid);
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
        public async Task<string> getimageb64stringfromurl(PhotoModel model)
        {
           
         
            {
                try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            byte[] imageBytes;
                            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(model.imageUrl);
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

        //get full profile stuff
        //*****************************************************
        public string getgenderbyphotoid(PhotoModel model)
        {

           
            {
                try
                {
                   
                    return  _unitOfWorkAsync.Repository<profiledata>().Query(p => p.profile.profilemetadata.photos.ToList().Any(z => z.id == model.photoid)).Select().FirstOrDefault().lu_gender.description;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }


        }

    }
}
