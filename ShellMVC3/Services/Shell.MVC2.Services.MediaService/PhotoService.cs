using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.Web;
using Shell.MVC2.Interfaces ;
using Shell.MVC2.Services.Contracts;

using Shell.MVC2.Domain.Entities.Anewluv;
using System.Net;
using System.ServiceModel.Activation;
using Shell.MVC2.Services.Contracts.ServiceResponse;

namespace Shell.MVC2.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class PhotoService : IPhotoService
    {


        private IPhotoRepository  _photorepo;
        //private string _apikey;

        public PhotoService(IPhotoRepository photorepo)
        {
                _photorepo = photorepo;
             //   _apikey  = HttpContext.Current.Request.QueryString["apikey"];
             //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
        }







        #region "View Photo models"




        public PhotoModel getphotomodelbyphotoid(string photoid, string format)
        {
            return _photorepo.getphotomodelbyphotoid(Guid.Parse(photoid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }

        public PhotoModel getphotomodelbyprofileid(string profileid, string format)
        {
            return _photorepo.getphotomodelbyphotoid(Guid.Parse(profileid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));      

        }

        public List<PhotoModel> getphotomodelsbyprofileidandstatus(string profile_id, string status, string format)
        {
            

            return _photorepo.getphotomodelsbyprofileidandstatus(Convert.ToInt32(profile_id),
                      ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)) ,
                       ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));
        }

        public List<PhotoModel> getpagedphotomodelbyprofileidandstatus(string profile_id, string status, string format, string page, string pagesize)
        {
            return _photorepo.getpagedphotomodelbyprofileidandstatus(Convert.ToInt32(profile_id),
                      ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                       ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));

        }

        //TO DO get photo albums as well ?
        public PhotoViewModel getpagedphotoviewmodelbyprofileid(string profileid, string format, string page, string pagesize)
        {
            return _photorepo.getpagedphotoviewmodelbyprofileid(Convert.ToInt32(profileid),
                        ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }



        #endregion

        #region "Edititable Photo models

        public photoeditmodel getphotoeditmodelbyphotoid(string photoid, string format)
        {
            return _photorepo.getphotoeditmodelbyphotoid(Guid.Parse(photoid), (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }

        public List<photoeditmodel> getphotoeditmodelsbyprofileidandstatus(string profile_id, string status, string format)
        {
            return _photorepo.getphotoeditmodelsbyprofileidandstatus(Convert.ToInt32(profile_id),
                   ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                    ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));

        }

        public List<photoeditmodel> getpagedphotoeditmodelsbyprofileidstatus(string profile_id, string status, string format,
                                                              string page, string pagesize)
        {
            return _photorepo.getpagedphotoeditmodelsbyprofileidstatus(Convert.ToInt32(profile_id),
                    ((photoapprovalstatusEnum)Enum.Parse(typeof(photoapprovalstatusEnum), status)),
                     ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }

        //12-10-2012 this also filters the format
        public PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(string profileid, string format, string page, string pagesize)
        {
            return _photorepo.getpagededitphotoviewmodelbyprofileidandformat(Convert.ToInt32(profileid),
                          ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }



        #endregion


        public AnewluvResponse deleteuserphoto(string photoid)
        {
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
            try
            {

                var message =  _photorepo.addsinglephoto(newphoto, Convert.ToInt32(profileid));
                AnewluvResponse response = new AnewluvResponse();
                ResponseMessage responsemessage = new ResponseMessage();
                responsemessage = new ResponseMessage("", message.message  , "");
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

        //http://stackoverflow.com/questions/10484295/image-resizing-from-sql-database-on-the-fly-with-mvc2
    
        public bool checkvalidjpggif(byte[] image)
        {

            return _photorepo.checkvalidjpggif(image);
        }
        
        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

         public string getgalleryphotobyscreenname(string strScreenName,string format)
        {
            return _photorepo.getgalleryimagebynormalizedscreenname(strScreenName, ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));
        }

         public string getgalleryimagebyphotoid(string photoid, string format)
        {
            return _photorepo.getgalleryimagebyphotoid(Guid.Parse(photoid),(photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }
        //TO DO normalize name
         public string getgalleryphotobyprofileid(string profileid, string format)
        {
            return _photorepo.getgalleryphotobyprofileid(Convert.ToInt32(profileid), ((photoformatEnum)Enum.Parse(typeof(photoformatEnum), format)));
        }

         public string getgalleryimagebynormalizedscreenname(string strScreenName, string format)
        {
            return _photorepo.getgalleryimagebynormalizedscreenname(strScreenName, (photoformatEnum)Enum.Parse(typeof(photoformatEnum), format));
        }

        public bool checkifphotocaptionalreadyexists(string profileid, string strPhotoCaption)
        {
            return _photorepo.checkifphotocaptionalreadyexists(Convert.ToInt32(profileid), strPhotoCaption);
        }

        public bool checkforgalleryphotobyprofileid(string profileid)
        {
            return _photorepo.checkforgalleryphotobyprofileid(Convert.ToInt32(profileid));
        }

        public bool checkforuploadedphotobyprofileid(string profileid)
        {
            return _photorepo.checkforuploadedphotobyprofileid(Convert.ToInt32(profileid));
        }


            public string getimagebytesfromurl(string _imageUrl, string source)
        {

            return _photorepo.getimageb64stringfromurl(_imageUrl, source);

        }


        /// <summary>
        /// dont think this is used
        /// </summary>
        /// <param name="_imageUrl"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        //public photo uploadprofileimage(string _imageUrl, string caption)
        //{
        //    return _photorepo.uploadprofileimage(_imageUrl, caption);
           
        //}         
        


    }
}
