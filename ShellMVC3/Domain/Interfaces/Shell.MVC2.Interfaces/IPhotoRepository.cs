using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface IPhotoRepository
    {       
       
        List<photo> getallphotosbyusername(string username);       
       
        List<PhotoEditModel> getphotosbyprofileidandstatus(string profile_id, photoapprovalstatusEnum status);

        List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profile_id, photoapprovalstatusEnum status,
                                                                    int page, int pagesize);
       
        PhotoEditModel getsingleprofilephotobyphotoid(Guid photoid);       
       
        PhotoEditViewModel getpagededitphotoviewmodel(string username, string ApprovedYes, string NotApprovedNo,
                                                           photoapprovalstatusEnum approvalstatus, int page, int pagesize);       
       
        void deleteduserphoto(Guid photoid);       
       
        void makeuserphoto_private(Guid PhotoID);       
       
        void makeuserphoto_public(Guid PhotoID);

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists  
        bool addphotos(PhotoUploadViewModel model);         
       
        bool addsinglephoto(PhotoUploadModel newphoto, int profileid);             
       
        List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded);       
       
        bool checkvalidjpggif(byte[] image);

        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011      
        byte[] getgalleryphotobyscreenname(string strScreenName);       
       
        byte[] getgalleryimagebyphotoid(Guid strPhotoID);       
       
        byte[] getgalleryphotobyprofileid(int intProfileID);       
       
        byte[] getgalleryimagebynormalizedscreenname(string strScreenName);       
       
        bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption);       
       
        bool checkforgalleryphotobyprofileid(int intProfileID);       
       
        bool checkforuploadedphotobyprofileid(int intProfileID);   
       
        photo uploadprofileimage(string _imageUrl, string caption); 
      
    }
}
