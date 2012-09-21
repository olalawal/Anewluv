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

        [WebGet]
        [OperationContract]   
         List<PhotoEditModel> getphotosbyprofileidandstatus(string profile_id, photoapprovalstatusEnum status);

        [WebGet]
        [OperationContract]
        List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profile_id, photoapprovalstatusEnum status,
                                                                   int page, int pagesize);

        [WebGet]
        [OperationContract]   
         PhotoEditModel getsingleprofilephotobyphotoid(Guid photoid);


        [WebGet]
        [OperationContract]   
        PhotoEditViewModel getpagededitphotoviewmodel(string username, string ApprovedYes, string NotApprovedNo,
                                                           photoapprovalstatusEnum approvalstatus, int page, int pagesize);

        [WebInvoke]
        [OperationContract]   
        void deleteduserphoto(Guid photoid);


        [WebInvoke]
        [OperationContract]   
         void makeuserphoto_private(Guid PhotoID);


          [WebInvoke]
        [OperationContract]   
         void makeuserphoto_public(Guid PhotoID);
       

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
         [WebInvoke]
        [OperationContract]   
        bool addphotos(PhotoUploadViewModel model);
        
        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
         [WebInvoke]
         [OperationContract]  
        bool addsinglephoto(PhotoUploadModel newphoto, int profileid);
        
        //http://stackoverflow.com/questions/10484295/image-resizing-from-sql-database-on-the-fly-with-mvc2
        /// <summary>
        /// this function creates and stores converted photos on the fly and returns them as byte array.
        /// if we have enough horsepower this might be faster than storing.
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="photouploaded"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
         [WebInvoke]
         [OperationContract]   
         List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded);

        [WebGet]
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
         byte[] getgalleryimagebynormalizedscreenname(string strScreenName)  ;

        [WebGet]
        [OperationContract]   
         bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption)   ;

        [WebGet]
        [OperationContract]   
         bool checkforgalleryphotobyprofileid(int intProfileID);

        [WebGet]
        [OperationContract]   
         bool checkforuploadedphotobyprofileid(int intProfileID);
        
        /// <summary>
        /// dont think this is used
        /// </summary>
        /// <param name="_imageUrl"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
  
        [WebGet]
        [OperationContract]   
         photo uploadprofileimage(string _imageUrl, string caption);
     

    }
}
