using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.Web;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;
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



        public List<photo> getallphotosbyusername(string username)
        {
            return _photorepo.getallphotosbyusername(username);
        }

        public List<PhotoEditModel> getphotosbyprofileidandstatus(string profile_id, photoapprovalstatusEnum status)
        {

            return _photorepo.getphotosbyprofileidandstatus(profile_id,  status);
        }

        public List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profile_id, photoapprovalstatusEnum approvalstatus,
                                                                    int page, int pagesize)
        {
            return _photorepo.getpagedphotosbyprofileidstatus(profile_id, approvalstatus, page, pagesize);

        }

        public PhotoEditModel getsingleprofilephotobyphotoid(Guid photoid)
        {

            return _photorepo.getsingleprofilephotobyphotoid(photoid);

        }

        //TO DO get photo albums as well ?
        //TO DO look at this code
        public PhotoEditViewModel getpagededitphotoviewmodel(string username, string ApprovedYes, string NotApprovedNo,
                                                           photoapprovalstatusEnum approvalstatus, int page, int pagesize)
        {

            return _photorepo.getpagededitphotoviewmodel(username, ApprovedYes, NotApprovedNo, approvalstatus, page, pagesize);
        }

        public void deleteduserphoto(Guid photoid)
        {
           _photorepo.deleteduserphoto(photoid);
        }
        public void makeuserphoto_private(Guid photoid)
        {
            _photorepo.makeuserphoto_private(photoid);
        }
        public void makeuserphoto_public(Guid photoid)
        {
            _photorepo.makeuserphoto_private(photoid);
        }

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        public bool addphotos(PhotoUploadViewModel model)
        {
            return _photorepo.addphotos(model);
        }

        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public bool addsinglephoto(PhotoUploadModel newphoto, int profileid)
        {

            return _photorepo.addsinglephoto(newphoto, profileid);
        }

        //http://stackoverflow.com/questions/10484295/image-resizing-from-sql-database-on-the-fly-with-mvc2
        /// <summary>
        /// this function creates and stores converted photos on the fly and returns them as byte array.
        /// if we have enough horsepower this might be faster than storing.
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="photouploaded"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded)
        {

            return _photorepo.addphotoconverions(photo, photouploaded);

        }

        public bool checkvalidjpggif(byte[] image)
        {

            return _photorepo.checkvalidjpggif(image);
        }


        
        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

        public byte[] getgalleryphotobyscreenname(string strScreenName)
        {
            return _photorepo.getgalleryimagebynormalizedscreenname(strScreenName);
        }

        public byte[] getgalleryimagebyphotoid(Guid strPhotoID)
        {

            return _photorepo.getgalleryimagebyphotoid(strPhotoID);
        }
        //TO DO normalize name
        public byte[] getgalleryphotobyprofileid(int intProfileID)
        {
            return _photorepo.getgalleryphotobyprofileid(intProfileID);

        }

        public byte[] getgalleryimagebynormalizedscreenname(string strScreenName)
        {

            return _photorepo.getgalleryimagebynormalizedscreenname(strScreenName);
        }

        public bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption)
        {
            return _photorepo.checkifphotocaptionalreadyexists(intProfileID, strPhotoCaption);
        }

        public bool checkforgalleryphotobyprofileid(int intProfileID)
        {
            return _photorepo.checkforgalleryphotobyprofileid(intProfileID);
        }

        public bool checkforuploadedphotobyprofileid(int intProfileID)
        {
            return _photorepo.checkforuploadedphotobyprofileid(intProfileID);
        }


           public byte[] getimagebytesfromurl(string _imageUrl, string source)
        {

            return _photorepo.getimagebytesfromurl(_imageUrl, source);

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
