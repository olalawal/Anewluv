using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.Web;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Services.Contracts;
using System.Net;

namespace Shell.MVC2.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PhotoService : IPhotoService
    {


        private IPhotoRepository  _photorepo;
        private string _apikey;

        public PhotoService(IPhotoRepository photorepo)
            {
                _photorepo = photorepo;
                _apikey  = HttpContext.Current.Request.QueryString["apikey"];

                throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }






       /// <summary>
       /// TO DO we should not be using the bare photo object I think ?
       /// </summary>
       /// <param name="username"></param>
       /// <returns></returns>
        public List<photo> GetAllMyPhotos(string username)
        {
           return _photorepo.GetAllPhotos(username);

        }

        public PhotoEditViewModel GetSingleProfilePhotobyphotoID(Guid photoid)
        {
            return _photorepo.GetSingleProfilePhotobyphotoID(photoid);
        }


        public EditProfilePhotosViewModel GetEditPhotoModel(string UserName, string ApprovedYes, string NotApprovedNo, int photoStatusID, int page, int pagesize)
        {
            return _photorepo.GetEditPhotoViewModel(UserName, ApprovedYes, NotApprovedNo, photoStatusID, page, pagesize);
        }

        public void DeletedUserPhoto(Guid PhotoID)
        {
            _photorepo.DeletedUserPhoto(PhotoID);
        }

        public void MakeUserPhoto_Private(Guid PhotoID)
        {
            _photorepo.MakeUserPhoto_Private(PhotoID);
        }

        public void MakeUserPhoto_Public(Guid PhotoID)
        {
            _photorepo.MakeUserPhoto_Public(PhotoID);
        }


        public bool AddPhoto(photo model)
        {

             return _photorepo.AddPhoto(model);
        }

        public bool CheckValidJPGGIF(byte[] Image)
        {
            return _photorepo.CheckValidJPGGIF(Image);
        }

        //mobe this to a repository ,, done 7/9/2012
      
        public photo UploadProfileImage(string _imageUrl, string caption)
        {
            return _photorepo.UploadProfileImage(_imageUrl,caption);

        }

        // added by Deshola on 5/17/2011    

        // added by Deshola on 5/17/2011   
       
       public  byte[] GetGalleryImagebyPhotoId(Guid id)
        {

            return _photorepo.GetGalleryImagebyPhotoId(id);
        }


       public byte[] GetGalleryPhotobyScreenName(string screenname)
        {
            return _photorepo.GetGalleryPhotobyScreenName( screenname);
        }

       public byte[] GetGalleryImagebyNormalizedScreenName(string screenname)
        {
            return _photorepo.GetGalleryImagebyNormalizedScreenName(screenname);

        }
        
       public  byte[] GetGalleryPhotobyProfileID(string strProfileID)
        {
            return _photorepo.GetGalleryPhotobyProfileID(strProfileID);
        }

       
        public bool InsertPhotoCustom(photo newphoto)
        {
            return _photorepo.InsertPhotoCustom(newphoto);
        }
        
        public bool CheckIfPhotoCaptionAlreadyExists(string strProfileID, string strPhotoCaption)
        {
            return _photorepo.CheckIfPhotoCaptionAlreadyExists(strProfileID, strPhotoCaption);
        }
       
        public bool CheckForGalleryPhotobyProfileID(string strProfileID)
        {
            return _photorepo.CheckForGalleryPhotobyProfileID(strProfileID);
        }
       
        public bool CheckForUploadedPhotobyProfileID(string strProfileID)
        {
            return _photorepo.CheckForUploadedPhotobyProfileID(strProfileID);
        }

        


    }
}
