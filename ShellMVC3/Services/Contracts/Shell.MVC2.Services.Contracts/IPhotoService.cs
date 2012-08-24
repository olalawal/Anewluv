using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dating.Server.Data.Models;
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
        List<photo> GetAllMyPhotos(string username);

        [WebGet]
        [OperationContract]
         EditProfilePhotoModel GetSingleProfilePhotobyphotoID(Guid photoid);
        
        [WebGet]
        [OperationContract]
         EditProfilePhotosViewModel GetEditPhotoModel(string UserName, string ApprovedYes, string NotApprovedNo, int photoStatusID, int page, int pagesize);
       
        [WebInvoke]
        [OperationContract]
         void DeletedUserPhoto(Guid PhotoID);
        [WebInvoke]
        [OperationContract]
         void MakeUserPhoto_Private(Guid PhotoID);
        [WebGet]
        [OperationContract]
         void MakeUserPhoto_Public(Guid PhotoID);
       
        [WebInvoke]
        [OperationContract]

         bool AddPhoto(photo model);
        [WebGet]
        [OperationContract]
         bool CheckValidJPGGIF(byte[] image);
             

      
        [WebGet]
        [OperationContract]
         photo UploadProfileImage(string _imageUrl, string caption);

        // added by Deshola on 5/17/2011   
        [OperationContract]
        byte[] GetGalleryImagebyPhotoId(Guid id);
        [WebGet]
        [OperationContract]
        byte[] GetGalleryPhotobyScreenName(string id);
        [WebGet]
        [OperationContract]
        byte[] GetGalleryImagebyNormalizedScreenName(string id);

        [WebGet]
        [OperationContract]
         byte[] GetGalleryPhotobyProfileID(string strProfileID);
        [WebInvoke]
        [OperationContract]
         bool InsertPhotoCustom(photo newphoto);
        [WebGet]
        [OperationContract]
         bool CheckIfPhotoCaptionAlreadyExists(string strProfileID, string strPhotoCaption);
        [WebGet]
        [OperationContract]
         bool CheckForGalleryPhotobyProfileID(string strProfileID);
        [WebGet]
        [OperationContract]
         bool CheckForUploadedPhotobyProfileID(string strProfileID);



        //  [WebGet]
        // [OperationContract]
        // IEnumerable<EditProfileViewPhotoModel> GetApprovedPhotos(IQueryable<photo> MyPhotos, string approved, int page, int pagesize);

        //gets all approved non prviate photos athat are not gallery 
        //[WebGet]
        // [OperationContract]
        //  IEnumerable<EditProfileViewPhotoModel> GetApprovedPhotosMinusGallery(IQueryable<photo> MyPhotos, string approved, int page, int pagesize);

        //[WebGet]
        // [OperationContract]
        //  IEnumerable<EditProfileViewPhotoModel> GetPhotosbyStatusID(IQueryable<photo> MyPhotos, int photoStatusID, int page, int pagesize);

        //  [WebGet]
        // [OperationContract]
        //  EditProfilePhotosViewModel GetPhotoViewModel(IEnumerable<EditProfileViewPhotoModel> Approved, IEnumerable<EditProfileViewPhotoModel> NotApproved,
        //                                                     IEnumerable<EditProfileViewPhotoModel> Private, IQueryable<photo> model);

    }
}
