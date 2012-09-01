using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface IPhotoRepository
    {

        List<photo> GetAllPhotos(string username);

        //List<EditProfileViewPhotoModel> GetAllMApproved(IQueryable<photo> MyPhotos, string approved, int page, int pagesize);
  
        //gets all approved non prviate photos athat are not gallery 
      // List<EditProfileViewPhotoModel> GetApprovedMinusGallery(IQueryable<photo> MyPhotos, string approved,int page, int pagesize);

      // List<EditProfileViewPhotoModel> GetPhotoByStatusID(IQueryable<photo> MyPhotos, int photoStatusID, int page, int pagesize);

      // EditProfilePhotosViewModel GetPhotoViewModel(List<EditProfileViewPhotoModel> Approved, List<EditProfileViewPhotoModel> NotApproved,
        //                                                    List<EditProfileViewPhotoModel> Private, IQueryable<photo> model);        
         
       EditProfilePhotoModel GetSingleProfilePhotobyphotoID(Guid photoid);

       EditProfilePhotosViewModel GetEditPhotoViewModel(string UserName, string ApprovedYes, string NotApprovedNo, int photoStatusID, int page, int pagesize);
      
       void DeletedUserPhoto(Guid PhotoID);
       
       void MakeUserPhoto_Private(Guid PhotoID);
        
       void MakeUserPhoto_Public(Guid PhotoID);        

       bool AddPhoto(photo model);

       bool CheckValidJPGGIF(byte[] image);

       byte[] GetGalleryImagebyPhotoId(Guid id);      

       byte[] GetGalleryPhotobyScreenName(string id);

       byte[] GetGalleryImagebyNormalizedScreenName(string id);

        photo UploadProfileImage(string _imageUrl, string caption);

       // added by Deshola on 5/17/2011     

        byte[] GetGalleryPhotobyProfileID(string strProfileID);

        bool InsertPhotoCustom(photo newphoto);

        bool CheckIfPhotoCaptionAlreadyExists(string strProfileID, string strPhotoCaption);
      
        bool CheckForGalleryPhotobyProfileID(string strProfileID);
       
        bool CheckForUploadedPhotobyProfileID(string strProfileID);
       
       
      
    }
}
