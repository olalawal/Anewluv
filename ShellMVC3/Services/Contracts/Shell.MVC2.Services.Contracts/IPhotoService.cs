using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;
using System.Web;
using System.ServiceModel.Web;
using System.IO;



namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
     public  interface IPhotoService
    {


        //[OperationContract]
        //public Stream GetLargeFile()
        //{
        //    return new FileStream(path, FileMode.Open, FileAccess.Read);
        //}

        //[OperationContract]
        //public void SendLargeFile(Stream stream)
        //{
        //    // Handle stream here - e.g. save to disk    
        //    ProcessStream(stream);

        //    // Close the stream when done processing it
        //    stream.Close();
        //}



        #region "View Photo models"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getphotomodelbyphotoid/{photoid}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         PhotoModel getphotomodelbyphotoid(string photoid, string format);

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getphotomodelsbyprofileidandstatus/{profileid}/{status}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         List<PhotoModel> getphotomodelsbyprofileidandstatus(string profileid, string status, string format);

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getpagedphotomodelbyprofileidandstatus/{profileid}/{status}/{format}/{page}/{pagesize}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         List<PhotoModel> getpagedphotomodelbyprofileidandstatus(string profileid, string status, string format, string page, string pagesize);
        
        //TO DO get photo albums as well ?
         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getpagedphotoviewmodelbyprofileid/{profileid}/{format}/{page}/{pagesize}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         PhotoViewModel getpagedphotoviewmodelbyprofileid(string  profileid, string format, string page, string  pagesize);
        
        #endregion

        #region "Edititable Photo models

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getphotoeditmodelbyphotoid/{photoid}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         photoeditmodel getphotoeditmodelbyphotoid(string photoid, string format);

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getphotoeditmodelsbyprofileidandstatus/{profileid}/{status}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         List<photoeditmodel> getphotoeditmodelsbyprofileidandstatus(string profileid, string status, string format);

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getpagedphotoeditmodelsbyprofileidstatus/{profileid}/{status}/{format}/{page}/{pagesize}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         List<photoeditmodel> getpagedphotoeditmodelsbyprofileidstatus(string profileid, string status, string format, string page, string pagesize);
        
        //12-10-2012 this also filters the format
         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/getpagededitphotoviewmodelbyprofileidandformat/{profileid}/{format}/{page}/{pagesize}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
         PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(string profileid, string format, string page, string pagesize);
       

        #endregion

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/deleteduserphoto/{photoid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
        void deleteduserphoto(string photoid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/makeuserphoto_private/{photoid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
        void makeuserphoto_private(string photoid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/makeuserphoto_public/{photoid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
        void makeuserphoto_public(string photoid);

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists  
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/addphotos", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addphotos(PhotoUploadViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/addsinglephoto/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addsinglephoto(PhotoUploadModel newphoto, string profileid);

        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[WebInvoke(UriTemplate = "/getgenericerroremail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/checkvalidjpggif", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
        bool checkvalidjpggif(byte[] image);

        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011   
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgalleryphotobyscreenname/{screenname}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
        byte[] getgalleryphotobyscreenname(string screenname,string format);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgalleryimagebyphotoid/{photoid}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        byte[] getgalleryimagebyphotoid(string photoid,string format);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgalleryphotobyprofileid/{profileid}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        byte[] getgalleryphotobyprofileid(string profileid,string format);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgalleryimagebynormalizedscreenname/{screenname}/{format}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        byte[] getgalleryimagebynormalizedscreenname(string screenname,string format);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifphotocaptionalreadyexists/{profileid}/{photocaption}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkifphotocaptionalreadyexists(string profileid, string photocaption);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkforgalleryphotobyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkforgalleryphotobyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkforuploadedphotobyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool checkforuploadedphotobyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getimagebytesfromurl/{imageUrl}/{source}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        byte[] getimagebytesfromurl(string imageUrl, string source); 
     

    }
}
