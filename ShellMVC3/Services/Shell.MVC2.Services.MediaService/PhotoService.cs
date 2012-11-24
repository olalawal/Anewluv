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

namespace Shell.MVC2.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
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



        public List<photo> getallphotosbyusername(string username)
        {
            return _photorepo.getallphotosbyusername(username);
        }

        public List<PhotoEditModel> getphotosbyprofileidandstatus(string profileid, photoapprovalstatusEnum status)
        {

            return _photorepo.getphotosbyprofileidandstatus(Convert.ToInt32(profileid), status);
        }

        public List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profileid, photoapprovalstatusEnum approvalstatus,
                                                                    string page, string pagesize)
        {
            return _photorepo.getpagedphotosbyprofileidstatus(Convert.ToInt32(profileid), approvalstatus, Convert.ToInt32(page), Convert.ToInt32(pagesize));

        }

        public PhotoEditModel getsingleprofilephotobyphotoid(string photoid)
        {

            return _photorepo.getsingleprofilephotobyphotoid(Guid.Parse(photoid));

        }

        //TO DO get photo albums as well ?
        //TO DO look at this code
        public PhotoEditViewModel getpagededitphotoviewmodelbyprofileid(string profileid, string page, string pagesize)        {

            return _photorepo.getpagededitphotoviewmodelbyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(pagesize));
        }

        public void deleteduserphoto(string photoid)
        {
            _photorepo.deleteduserphoto(Guid.Parse(photoid));
        }
        public void makeuserphoto_private(string photoid)
        {
            _photorepo.makeuserphoto_private(Guid.Parse(photoid));
        }
        public void makeuserphoto_public(string photoid)
        {
            _photorepo.makeuserphoto_private(Guid.Parse(photoid));
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
        public bool addsinglephoto(PhotoUploadModel newphoto, string profileid)
        {
            return _photorepo.addsinglephoto(newphoto, Convert.ToInt32(profileid));
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
        //public List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded)
        //{

        //    return _photorepo.addphotoconverions(photo, photouploaded);

        //}

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

        public byte[] getgalleryimagebyphotoid(string photoid)
        {

            return _photorepo.getgalleryimagebyphotoid(Guid.Parse(photoid));
        }
        //TO DO normalize name
        public byte[] getgalleryphotobyprofileid(string profileid)
        {
            return _photorepo.getgalleryphotobyprofileid(Convert.ToInt32(profileid));

        }

        public byte[] getgalleryimagebynormalizedscreenname(string strScreenName)
        {

            return _photorepo.getgalleryimagebynormalizedscreenname(strScreenName);
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
