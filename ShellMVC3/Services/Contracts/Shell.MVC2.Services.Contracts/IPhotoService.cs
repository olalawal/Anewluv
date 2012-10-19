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



namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
     public  interface IPhotoService
    {
        [WebGet]
        [OperationContract]
        List<photo> getallphotosbyusername(string username);
        [WebInvoke]
        [OperationContract]
        List<PhotoEditModel> getphotosbyprofileidandstatus(string profile_id, photoapprovalstatusEnum status);
        [WebInvoke]
        [OperationContract]
        List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profile_id, photoapprovalstatusEnum status,
                                                                    int page, int pagesize);
        [WebGet]
        [OperationContract]
        PhotoEditModel getsingleprofilephotobyphotoid(Guid photoid);
        [WebInvoke]
        [OperationContract]
        PhotoEditViewModel getpagededitphotoviewmodel(string username, string ApprovedYes, string NotApprovedNo,
                                                           photoapprovalstatusEnum approvalstatus, int page, int pagesize);
        [WebGet]
        [OperationContract]
        void deleteduserphoto(Guid photoid);
        [WebGet]
        [OperationContract]
        void makeuserphoto_private(Guid PhotoID);
        [WebGet]
        [OperationContract]
        void makeuserphoto_public(Guid PhotoID);

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists  
        [WebInvoke]
        [OperationContract]
        bool addphotos(PhotoUploadViewModel model);
        [WebInvoke]
        [OperationContract]
        bool addsinglephoto(PhotoUploadModel newphoto, int profileid);
        [WebInvoke]
        [OperationContract]
        List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded);
        [WebInvoke]
        [OperationContract]
        bool checkvalidjpggif(byte[] image);

        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011   
        [WebGet]
        [OperationContract]
        byte[] getgalleryphotobyscreenname(string strScreenName);
        [WebGet]
        [OperationContract]
        byte[] getgalleryimagebyphotoid(Guid strPhotoID);
        [WebGet]
        [OperationContract]
        byte[] getgalleryphotobyprofileid(int intProfileID);
        [WebGet]
        [OperationContract]
        byte[] getgalleryimagebynormalizedscreenname(string strScreenName);
        [WebGet]
        [OperationContract]
        bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption);
        [WebGet]
        [OperationContract]
        bool checkforgalleryphotobyprofileid(int intProfileID);
        [WebGet]
        [OperationContract]
        bool checkforuploadedphotobyprofileid(int intProfileID);
        [WebGet]
        [OperationContract]
        byte[] getimagebytesfromurl(string _imageUrl, string source); 
     

    }
}
