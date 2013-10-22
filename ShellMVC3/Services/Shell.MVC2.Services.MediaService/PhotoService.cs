using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using System.Web;
using System.Net;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;
using Anewluv.DataAccess.Interfaces;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities;
using Anewluv.DataAccess.ExtentionMethods;
using Shell.MVC2.Services.Contracts.ServiceResponse;
using Shell.MVC2.Infrastructure.Helpers;
using System.IO;
using ImageResizer;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.Drawing;

namespace Shell.MVC2.Services.Media
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
                                    var settings = new ResizeSettings(currentformat.imageresizerformat.description);
                                    ImageResizer.ImageBuilder.Current.Build(inStream, outStream, settings);
                                    var outBytes = outStream.ToArray();
                                    //double check that there is no conversion with matching size and the name
                                    // var test = _datingcontext.photoconversions.Where(p => p.photo.profile_id == photo.profile_id).Any(p => p.size == photo.size);

                                    convertedphotos.Add(new photoconversion
                                    {
                                        creationdate = DateTime.Now,
                                        description = currentformat.description,
                                        formattype = currentformat,
                                        image = outBytes,
                                        size = outBytes.Length,
                                        photo_id = photo.id
                                    });

                                }
                                catch (ImageResizer.ImageMissingException missing)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, missing.Message, missing);
                                    new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageCorruptedException cr)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, cr.Message, cr);
                                    new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, convertedexcption,  photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (ImageResizer.ImageProcessingException ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, convertedexcption, photo.profile_id, null);
                                    //domt throw just log and move on
                                }
                                catch (Exception ex)
                                {
                                    Exception convertedexcption = new CustomExceptionTypes.MediaException(photo.profile_id.ToString(), photo.imagename, ex.Message, ex);
                                    new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.Warning, convertedexcption, photo.profile_id, null);
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
                    //new ErroLogging(applicationEnum.MediaService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
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
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MediaService);                      
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in photo service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
                }

            return _photorepo.getphotomodelbyphotoid(Guid.Parse(photoid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }

        public PhotoModel getgalleryphotomodelbyprofileid(string profileid, string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getgalleryphotomodelbyprofileid(int.Parse(profileid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));      

        }

        public List<PhotoModel> getphotomodelsbyprofileidandstatus(string profile_id, string status, string format)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            return _photorepo.getphotomodelsbyprofileidandstatus(Convert.ToInt32(profile_id),
                      ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)) ,
                       ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));
        }

        public List<PhotoModel> getpagedphotomodelbyprofileidandstatus(string profile_id, string status, string format, string page, string pagesize)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getpagedphotomodelbyprofileidandstatus(Convert.ToInt32(profile_id),
                      ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                       ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));

        }

        //TO DO get photo albums as well ?
        public PhotoViewModel getpagedphotoviewmodelbyprofileid(string profileid, string format, string page, string pagesize)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getpagedphotoviewmodelbyprofileid(Convert.ToInt32(profileid),
                        ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }



        #endregion

        #region "Edititable Photo models

        public photoeditmodel getphotoeditmodelbyphotoid(string photoid, string format)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getphotoeditmodelbyphotoid(Guid.Parse(photoid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }

        public List<photoeditmodel> getphotoeditmodelsbyprofileidandstatus(string profile_id, string status, string format)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getphotoeditmodelsbyprofileidandstatus(Convert.ToInt32(profile_id),
                   ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                    ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));

        }

        public List<photoeditmodel> getpagedphotoeditmodelsbyprofileidstatus(string profile_id, string status, string format,
                                                              string page, string pagesize)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getpagedphotoeditmodelsbyprofileidstatus(Convert.ToInt32(profile_id),
                    ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                     ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }

        //12-10-2012 this also filters the format
        public PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(string profileid, string format, string page, string pagesize)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            return _photorepo.getpagededitphotoviewmodelbyprofileidandformat(Convert.ToInt32(profileid),
                          ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }



        #endregion


        public AnewluvResponse deleteuserphoto(string photoid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            try
            {
               
               var message = _photorepo.deleteuserphoto(Guid.Parse(photoid));
                AnewluvResponse response = new AnewluvResponse();
                ResponseMessage responsemessage = new ResponseMessage();
                responsemessage = new ResponseMessage("", message.message , "");
                response.ResponseMessages.Add(responsemessage);
                return response;         
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in photo service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }
        public AnewluvResponse makeuserphoto_private(string photoid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            try
            {

                var message =  _photorepo.makeuserphoto_private(Guid.Parse(photoid));
                AnewluvResponse response = new AnewluvResponse();
                ResponseMessage responsemessage = new ResponseMessage();
                responsemessage = new ResponseMessage("", message.message ,"");
                response.ResponseMessages.Add(responsemessage);
                return response;
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in photo service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }
        public AnewluvResponse makeuserphoto_public(string photoid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
          
            try
            {

              var message =   _photorepo.makeuserphoto_private(Guid.Parse(photoid));

                AnewluvResponse response = new AnewluvResponse();
                ResponseMessage responsemessage = new ResponseMessage();
                responsemessage = new ResponseMessage("", message.message , "");
                response.ResponseMessages.Add(responsemessage);
                return response;
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in photo service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        public AnewluvResponse addphotos(PhotoUploadViewModel model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            try
            {

               var message =    _photorepo.addphotos(model);
                AnewluvResponse response = new AnewluvResponse();
                ResponseMessage responsemessage = new ResponseMessage();
                responsemessage = new ResponseMessage("", message.message , "");
                response.ResponseMessages.Add(responsemessage);
                return response;
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in photo service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public AnewluvResponse addsinglephoto(PhotoUploadModel newphoto, string profileid)
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
                    NewPhoto.profile_id = Convert.ToInt32( profileid); //model.ProfileImage.Length;
                    // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                    NewPhoto.creationdate = newphoto.creationdate;
                    NewPhoto.imagecaption = newphoto.caption;
                    NewPhoto.imagename = newphoto.imagename; //11-26-2012 olawal added the name for comparisons 
                    NewPhoto.size = newphoto.legacysize.GetValueOrDefault();
                    //set the rest of the information as needed i.e approval status refecttion etc
                    NewPhoto.imagetype = (newphoto.imagetypeid != null) ? db.GetRepository<lu_photoimagetype>().Find().Where(p => p.id == newphoto.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                    NewPhoto.approvalstatus = (newphoto.approvalstatusid != null) ? db.GetRepository<lu_photoapprovalstatus>().Find().Where(p => p.id == newphoto.approvalstatusid).FirstOrDefault() : null;
                    NewPhoto.rejectionreason = (newphoto.rejectionreasonid != null) ? db.GetRepository<lu_photorejectionreason>().Find().Where(p => p.id == newphoto.rejectionreasonid).FirstOrDefault() : null;
                    NewPhoto.photostatus = (newphoto.photostatusid != null) ? db.GetRepository<lu_photostatus>().Find().Where(p => p.id == newphoto.photostatusid).FirstOrDefault() : null;

                    var temp = addphotoconverionsb64string(NewPhoto, newphoto);
                    if (temp.Count > 0)
                    {

                        //existing conversions to compare with new one : 
                        var existingthumbnailconversion = db.GetRepository<photoconversion>().Find().Where(z => z.photo.profile_id == Convert.ToInt32(profileid) & z.formattype.id == (int)photoformatEnum.Thumbnail);
                        var newphotothumbnailconversion = temp.Where(p => p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault();
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

                         message.message  = "photo added succesfully " ;
                    }

                   //return the response
                    responsemessage = new ResponseMessage("", message.message, "");
                    response.ResponseMessages.Add(responsemessage);
                    return response;

                }
                catch (Exception ex)
                {
                    //TO DO track the transaction types only rollback on DB connections
                    //rollback transaction
                    transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
                    var GalleryPhoto = (from p in db.GetRepository<profile>().Find().Where(p => p.screenname == strscreenname)
                                        join f in db.GetRepository<photoconversion>().Find() on p.id equals f.photo.profile_id
                                        where (f.formattype.id == (int)Convert.ToInt16(format) && f.photo.approvalstatus != null &&
                                        f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                                        f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();

                    //new code to only get the gallery conversion copy
                    //  return GalleryPhoto.conversions
                    // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image ;
                    return Convert.ToBase64String(GalleryPhoto.image);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

            var GalleryPhoto = (from f in db.GetRepository<photoconversion>().Find().Where(f => f.photo.id == (Guid.Parse(photoid)) &&
             f.formattype.id == (int) Convert.ToInt16(format) && f.photo.approvalstatus != null &&
             f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
             f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();

                    //new code to only get the gallery conversion copy
                    //return GalleryPhoto.conversions
                    // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                    return Convert.ToBase64String(GalleryPhoto.image);

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    var GalleryPhoto = (from p in db.GetRepository<profile>().Find().Where(p => p.id == Convert.ToInt32(profileid))
                                        join f in db.GetRepository<photoconversion>().Find() on p.id equals f.photo.profile_id
                                        where (f.formattype.id == (int)Convert.ToInt32(format) && f.photo.approvalstatus != null &&
                                        f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                                        f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                                        select f).FirstOrDefault();

                    if (GalleryPhoto != null)
                        return Convert.ToBase64String(GalleryPhoto.image);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
                                        where (f.formattype.id == (int) Convert.ToInt32(format) && f.photo.approvalstatus != null &&
                                        f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                                        f.photo.imagetype.id == (int)photostatusEnum.Gallery)
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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        
            return _photorepo.checkifphotocaptionalreadyexists(Convert.ToInt32(profileid), strPhotoCaption);


           
        }

        public bool checkforgalleryphotobyprofileid(string profileid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var GalleryPhoto = db.GetRepository<photo>().Find().Where(p => p.profile_id == Convert.ToInt32(profileid) &&
                p.approvalstatus != null &&   p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved && p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault();

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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
                    logger = new ErroLogging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex);
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
