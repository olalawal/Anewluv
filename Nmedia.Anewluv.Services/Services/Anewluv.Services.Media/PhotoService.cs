﻿using System;
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
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Repository.Pattern.Infrastructure;
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
              //photoImageresizerformatEnum
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

       /// <summary>
       /// sorts the types of photos for a user and returns the counts which is nice for things like lists of photo types
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public async Task<PhotosSortedCountsViewModel> getphotosortedcounts(PhotoModel model)
        {
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                       return  _unitOfWorkAsync.Repository<photo>().getphotossortedcountsbyprofileid(model);
                      
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
        //****TO DO pass apikey/token and make sure the token matches the profileid whose photos are being acccess and the token is active as well
       //get the api key from the wcf session tho
       //when accessing private photos or photo albums
       //This should be the only method used to get photos for the user who ownes the account
       public async Task<PhotoSearchResultsViewModel> getfilteredphotospaged(PhotoModel model)
       {
           {
               try
               {
                
                       //check the api key matches the profile id pased 
                       var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                       ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;

                       if (profileidmatchesapikey)
                       {
                           //get the profile details needed for some stuff
                           var profileresult = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.id == model.profileid.Value &&
                             (p.status_id != (int)profilestatusEnum.Banned | p.status_id != (int)profilestatusEnum.Inactive | p.status_id != (int)profilestatusEnum.ResetingPassword)
                              ).Include(z => z.membersinroles).SelectAsync();

                           var profile = profileresult.FirstOrDefault();
                           
                          
                           var repo = _unitOfWorkAsync.Repository<photoconversion>();
                           var dd = mediaextentionmethods.getfilteredphotospaged
                               (repo, model,profile,model.isadmin.GetValueOrDefault());

                           return dd;
                       }
                       return null;

                  


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


       //****TO DO use the profileid to determine if the viewer has access to what based on security settings in the filter
       public async Task<PhotoSearchResultsViewModel> getothersfilteredphotospaged(PhotoModel model)
       {
           {
               try
               {
                   var task = Task.Factory.StartNew(() =>
                   {
                    

                       var repo = _unitOfWorkAsync.Repository<photoconversion>();
                       var dd = mediaextentionmethods.getothersfilteredphotospaged
                           (repo, model);

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

                       //check the api key matches the profile id pased 
                       var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                       ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value,useridentifier = model.profileid.Value }).Result;
                       if (profileidmatchesapikey)
                       {
                           var repo = _unitOfWorkAsync.Repository<photoconversion>();
                           var dd = mediaextentionmethods.getfilteredphoto
                               (repo, model);

                           return dd;

                       }
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
                bool updated = false;
                
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {


                      

                            //check the api key matches the profile id pased 
                            var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                            ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;

                            if (profileidmatchesapikey)
                            {

                                // Retrieve single value from photos table
                                IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == model.photoid && u.profile_id == model.profileid).SelectAsync();
                            
                                var PhotoModify = result.FirstOrDefault();

                                //remove all secruity otptions if its public
                                if (model.photocaption != null  && model.photocaption != PhotoModify.imagecaption)
                                {

                                    PhotoModify.imagecaption = model.photocaption;
                                    PhotoModify.ObjectState = ObjectState.Modified;
                                    //TO DO add modify date
                                    _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);                                   
                                    AnewluvMessages.messages.Add("Photo caption changed!");
                                    updated = true;

                                }
                                else if (model.phototstatusid != null && model.phototstatusid == (int)photostatusEnum.Gallery)
                                {
                                    //first check if this photo status is not aleary gallery 
                                    if (PhotoModify.photostatus_id != (int)photostatusEnum.Gallery)
                                    {
                                        //first check grab the exisitng gallery and nuke it 
                                         // Retrieve single value from photos table
                                           IEnumerable<photo> existingallery = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == model.photoid && u.profile_id == model.profileid).SelectAsync();                            
                                           if (existingallery.FirstOrDefault() !=null)
                                           {
                                               var PhotoModifyGallery = existingallery.FirstOrDefault();
                                               PhotoModifyGallery.ObjectState = ObjectState.Modified;
                                               PhotoModifyGallery.photostatus_id = (int)photostatusEnum.Nostatus;
                                               AnewluvMessages.messages.Add("the photo : " + PhotoModifyGallery.imagecaption + " has been removed from primary");
                                           }

                                        PhotoModify.ObjectState = ObjectState.Modified;
                                        PhotoModify.photostatus_id = (int)photostatusEnum.Gallery;
                                        updated = true;
                                        AnewluvMessages.messages.Add("the photo : " + PhotoModify.imagecaption + " has been made primary");
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    // transaction.Commit();
                                    AnewluvMessages.errormessages.Add("you can either make a photo your primary photo or change its caption !");
                                }


                            }
                            else
                            {
                                AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                                //TO DO log this 
                            }

                            if (updated) await _unitOfWorkAsync.SaveChangesAsync();                       
                          
                           return AnewluvMessages;
                  


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
                bool updated = false;
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        //check the api key matches the profile id pased 
                        var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                        ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;

                        if (profileidmatchesapikey)
                        {

                            foreach (Guid photoid in model.photoids)
                            {
                                // var  model.photoformat = model. model.photoformat);
                                //var photoid = model.photoid;
                                //var convertedprofileid = Convert.ToInt32(model.profileid);
                                // var convertedstatus =  model.photostatus);

                                // Retrieve single value from photos table
                                IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == photoid && u.profile_id == model.profileid).SelectAsync();


                                if (result.FirstOrDefault() != null)
                                {
                                    photo PhotoModify = result.FirstOrDefault();
                                    PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                                    PhotoModify.ObjectState = ObjectState.Modified;
                                    _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                    // Update database
                                    // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                                    updated = true;
                                    // transaction.Commit();
                                    AnewluvMessages.messages.Add("photo deleted successfully");
                                }

                            }


                            if (updated) await _unitOfWorkAsync.SaveChangesAsync();
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                            //TO DO log this 
                        }


                     

                            return AnewluvMessages;
                       


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


            
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
                bool updated = false;
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        //check the api key matches the profile id pased 
                        var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                        ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;

                        if (profileidmatchesapikey)
                        {

                              // var  model.photoformat = model. model.photoformat);
                                //var photoid = model.photoid;
                                //var convertedprofileid = Convert.ToInt32(model.profileid);
                                // var convertedstatus =  model.photostatus);

                                // Retrieve single value from photos table
                                IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == model.photoid && u.profile_id == model.profileid).SelectAsync();


                                if (result.FirstOrDefault() != null)
                                {
                                    photo PhotoModify = result.FirstOrDefault();
                                    PhotoModify.photostatus_id = (int)photostatusEnum.deletedbyuser;
                                    PhotoModify.ObjectState = ObjectState.Modified;
                                    _unitOfWorkAsync.Repository<photo>().Update(PhotoModify);
                                    // Update database
                                    // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                                    updated = true;
                                    // transaction.Commit();
                                    AnewluvMessages.messages.Add("photo deleted successfully");
                                }

                                if (updated)
                                {
                                    _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();
                                }
                                else
                                {
                                    AnewluvMessages.errormessages.Add("invalid data no changes made!");
                                }
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                            //TO DO log this 
                        }
                        
                        return AnewluvMessages;

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
        public async Task<AnewluvMessages> addphotossecuritylevel(PhotoModel model)
        {

            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
                bool updated = false;
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        //check the api key matches the profile id pased 
                        var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                        ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;


                        if (profileidmatchesapikey)
                        {
                            if (model.photosecuritylevelid != null | model.photoids == null)
                            {
                                foreach (Guid photoid in model.photoids)
                                {
                                    // Retrieve single value from photos table
                                    IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == photoid && u.profile_id == model.profileid)
                                        .Include(z => z.photo_securitylevel.Select(f => f.lu_securityleveltype)).SelectAsync();


                                    if (result.FirstOrDefault() != null)
                                    {
                                        var PhotoModify = result.FirstOrDefault();

                                            var existingsecuritylevel = PhotoModify.photo_securitylevel.Where(z => z.securityleveltype_id == model.photosecuritylevelid).FirstOrDefault();

                                            if (existingsecuritylevel == null)
                                            {
                                                //add since we dont have this current security level type
                                                
                                                var newsecuritylevel = new photo_securitylevel
                                                {
                                                    photo_id = PhotoModify.id,                                                   
                                                    securityleveltype_id  = model.photosecuritylevelid, ObjectState = ObjectState.Added  

                                                };
                                                                                               
                                                _unitOfWorkAsync.RepositoryAsync<photo_securitylevel>().Insert(newsecuritylevel);
                                                AnewluvMessages.messages.Add("photo scurity added for the photo :" + PhotoModify.imagecaption);
                                                updated = true;
                                            }
                                            else
                                            {
                                                AnewluvMessages.errormessages.Add("the photo: " + PhotoModify.imagecaption + " already has the security setting : " + ((securityleveltypeEnum)model.photosecuritylevelid.Value).ToDescription());
                                         
                                            }
                                    }
                                    else
                                    {
                                        AnewluvMessages.errormessages.Add("the photoid : " + photoid + " is invalid for this profile , cannot modify security level");
                                    }
                                }
                            }

                            else
                            {
                                AnewluvMessages.errormessages.Add("no photoids or  photosecuritylevelid passed, cannot modify security level");
                            }


                            if (updated)
                            {
                                _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();
                            }
                            else
                            {
                                AnewluvMessages.errormessages.Add("no changes made");
                            }
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                            //TO DO log this 
                        }




                        return AnewluvMessages;



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
        public async Task<AnewluvMessages> removephotossecuritylevel(PhotoModel model)
        {

            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
                bool updated = false;
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        //check the api key matches the profile id pased 
                        var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                        ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;


                        if (profileidmatchesapikey)
                        {
                            if (model.photosecuritylevelid != null | model.photoids == null)
                            {
                                foreach (Guid photoid in model.photoids)
                                {
                                    // Retrieve single value from photos table
                                    IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == photoid && u.profile_id == model.profileid)
                                        .Include(z => z.photo_securitylevel.Select(f => f.lu_securityleveltype)).SelectAsync();

                                
                                    if (result.FirstOrDefault() != null)
                                    {
                                        var PhotoModify = result.FirstOrDefault();
                                        if (PhotoModify.photo_securitylevel.Count() > 0)
                                        {
                                            var securitytoremove = PhotoModify.photo_securitylevel.Where(z => z.securityleveltype_id == model.photosecuritylevelid).FirstOrDefault();

                                            if (securitytoremove != null)
                                            {
                                                securitytoremove.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                                                _unitOfWorkAsync.Repository<photo_securitylevel>().Delete(securitytoremove);
                                                updated = true;
                                                AnewluvMessages.messages.Add("photo scurity added removed for the photo :" + PhotoModify.imagecaption);
                                            }
                                            else
                                            {
                                                AnewluvMessages.errormessages.Add("the photo: " + PhotoModify.imagecaption + " does not have the security setting : " + ((securityleveltypeEnum)model.photosecuritylevelid.Value).ToDescription());
                                            }
                                        }
                                        else
                                        {
                                            AnewluvMessages.errormessages.Add("the photo : " + PhotoModify.imagecaption + "  has no current security settings to remove! !");
                                        }
                                    }
                                    else
                                    {
                                        AnewluvMessages.errormessages.Add("the photoid : " +  photoid + " is invalid for this profile , cannot modify security level");
                                    }
                                }                            }

                            else
                            {
                                AnewluvMessages.errormessages.Add("no photoids or  photosecuritylevelid passed, cannot modify security level");
                            }


                            if (updated)
                            {

                                _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();
                              

                            }
                            else
                            {
                                AnewluvMessages.errormessages.Add("photosecuritylevelid to remove not passed or photoids are empty , cannot modify security level");
                            }
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                            //TO DO log this 
                        }


                           

                            return AnewluvMessages;
                       


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
        public async Task<AnewluvMessages> makephotospublic(PhotoModel model)
        {

            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    bool updated = false;
                    try
                    {

                          //check the api key matches the profile id pased 
                        var profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                                        ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value }).Result;

                        if (profileidmatchesapikey)
                        {

                           
                                //var  model.photoformat = model. model.photoformat);
                                //  var photoid = Guid.Parse(photoid);
                                // var convertedprofileid = Convert.ToInt32(model.profileid);
                                // var convertedstatus =  model.photostatus);
                                foreach (Guid photoid in model.photoids)
                                {

                                    IEnumerable<photo> result = await _unitOfWorkAsync.RepositoryAsync<photo>().Query(u => u.id == photoid && u.profile_id == model.profileid)
                                           .Include(z => z.photo_securitylevel.Select(f => f.lu_securityleveltype)).SelectAsync();



                                    if (result.FirstOrDefault() != null)
                                    {
                                        var PhotoModify = result.FirstOrDefault();

                                        //remove all secruity otptions if its public

                                        foreach (photo_securitylevel securitylevel in PhotoModify.photo_securitylevel.ToList())
                                        {
                                            var secruityleveltodelete = await _unitOfWorkAsync.RepositoryAsync<photo_securitylevel>().Query(p => p.id == securitylevel.id).SelectAsync();
                                            secruityleveltodelete.FirstOrDefault().ObjectState = Repository.Pattern.Infrastructure.ObjectState.Deleted;
                                            _unitOfWorkAsync.Repository<photo_securitylevel>().Delete(secruityleveltodelete);
                                            // newsecurity.id = (int)securityleveltypeEnum.Private;
                                            updated = true;
                                        }
                                        AnewluvMessages.messages.Add("photo security removed for : " + PhotoModify.imagecaption);

                                    }
                                    else
                                    {
                                        AnewluvMessages.errormessages.Add("the photoid : " + photoid + " is invalid for this profile , cannot modify security level");
                                    }
                                }
                            }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");
                            //TO DO log this 
                        }


                        if (updated)
                        {
                            _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("invalid data no changes made!");
                        }
                        
                        return AnewluvMessages;



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

                           
                                var EmailModels = new List<EmailModel>();

                                EmailModels.Add(new EmailModel
                                {
                                    templateid = (int)templateenum.MemberPhotoRejectedMemberNotification,
                                    messagetypeid = (int)messagetypeenum.UserUpdate,
                                    addresstypeid = (int)addresstypeenum.SiteUser,
                                    emailaddress = PhotoModify.profilemetadata.profile.emailaddress,
                                    screenname = PhotoModify.profilemetadata.profile.screenname,
                                });


                                EmailModels.Add(new EmailModel
                                {
                                    templateid = (int)templateenum.MemberPhotoRejectedAdminNotification,
                                    messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                    addresstypeid = (int)addresstypeenum.SystemAdmin,
                                    emailaddress = PhotoModify.profilemetadata.profile.emailaddress,
                                    screenname = PhotoModify.profilemetadata.profile.screenname,
                                    username = PhotoModify.profilemetadata.profile.username,

                                });


                                //this sends both admin and user emails  
                                Api.AsyncCalls.sendmessagesbytemplate(EmailModels);   

                                    
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
                                var EmailModels = new List<EmailModel>();

                                EmailModels.Add(new EmailModel
                                {
                                    templateid = (int)templateenum.MemberPhotoApprovedAdminNotification,
                                    messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                    addresstypeid = (int)addresstypeenum.SystemAdmin,
                                    emailaddress = PhotoModify.profilemetadata.profile.emailaddress,
                                    screenname = PhotoModify.profilemetadata.profile.screenname,
                                    username = PhotoModify.profilemetadata.profile.username
                                    // subject = templatesubjectenum.MemberPhotoApprovedAdminNotification.ToDescription()
                                });


                                Api.AsyncCalls.sendmessagesbytemplate(EmailModels);
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

        ///// <summary>
        ///// allows for getting photos that are not approved yet
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public async Task<PhotoSearchResultsViewModel> getadminfilteredphotospaged(PhotoModel model)
        //{


        //    //check the api key matches the profile id pased 
        //    var profileidmatchesapikey =  await Api.AsyncCalls.validateapikeybyuseridentifierasync(new
        //                    ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = model.apikey.Value, useridentifier = model.profileid.Value });
            
             

        //        if (profileidmatchesapikey)
        //        {
        //           //check roles 


        //            var repo = _unitOfWorkAsync.Repository<photoconversion>();
        //            var dd = mediaextentionmethods.getfilteredphotospaged(repo, model,true);

        //            return dd;
        //        }
        //        return null;

               

            
       
        
        //}

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
                                    //set the rest of the information as needed i.e approval status refection etc

                                    NewPhoto.lu_photoimagetype = (!String.IsNullOrEmpty(item.imagetypedescription)) ? 
                                    _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.description.ToUpper().Contains(item.imagetypedescription.ToUpper())).FirstOrDefault() :
                                    _unitOfWorkAsync.Repository<lu_photoimagetype>().Queryable().ToList().Where(p => p.id == (int)photoimagetypeEnum.other).FirstOrDefault(); 

                                    NewPhoto.imagetype_id = NewPhoto.lu_photoimagetype != null ? NewPhoto.lu_photoimagetype.id : (int?)null;

                                    //code to handle open id uploaded photo, might need to still verify these at some point
                                    NewPhoto.approvalstatus_id = !String.IsNullOrEmpty(item.openidprovider) ? (int)photoapprovalstatusEnum.Approved :(int)photoapprovalstatusEnum.NotReviewed;
                                    NewPhoto.rejectionreason_id = null;
                                    //auto set photos to gallery for open id
                                    NewPhoto.photostatus_id = !String.IsNullOrEmpty(item.openidprovider) && (item.photostatusid == (int)photostatusEnum.Gallery) ?  (int)photostatusEnum.Gallery : (int)photostatusEnum.Nostatus;

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
                                     var EmailModels = new List<EmailModel>();

                                        EmailModels.Add ( new EmailModel {
                                              templateid = (int)templateenum.MemberPhotoUploadedAdminNotification,
                                            messagetypeid = (int)messagetypeenum.SysAdminUpdate, 
                                            addresstypeid = (int)addresstypeenum.SystemAdmin//,
                                           });

                                        Api.AsyncCalls.sendmessagesbytemplate(EmailModels);
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

                                var EmailModels = new List<EmailModel>();

                                EmailModels.Add(new EmailModel
                                {
                                    templateid = (int)templateenum.MemberPhotoUploadedAdminNotification,
                                    messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                    addresstypeid = (int)addresstypeenum.SystemAdmin
                                });


                                Api.AsyncCalls.sendmessagesbytemplate(EmailModels);

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
                                //var convertedprofileid = model.profileid.Value;
                                IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
                                //Dim ctx As New Entities()
                                GalleryPhoto = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == model.profileid.Value);

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
