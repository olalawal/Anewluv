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

        #region "View Photo models"

        PhotoModel getphotomodelbyphotoid(Guid photoid, photoformatEnum format);

        PhotoModel getphotomodelbyprofileid(int profileid, photoformatEnum format);
      
        List<PhotoModel> getphotomodelsbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status, photoformatEnum format);

        List<PhotoModel> getpagedphotomodelbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status, photoformatEnum format, int page, int pagesize);               

        //TO DO get photo albums as well ?
        PhotoViewModel getpagedphotoviewmodelbyprofileid(int profileid, photoformatEnum format, int page, int pagesize);

        #endregion

        #region "Edititable Photo models

        photoeditmodel getphotoeditmodelbyphotoid(Guid photoid, photoformatEnum format);
       
        List<photoeditmodel> getphotoeditmodelsbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status, photoformatEnum format);
       
        List<photoeditmodel> getpagedphotoeditmodelsbyprofileidstatus(int profile_id, photoapprovalstatusEnum status, photoformatEnum format,
                                                              int page, int pagesize);    

        //12-10-2012 this also filters the format
        PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(int profileid, photoformatEnum format, int page, int pagesize);
       
               
        #endregion

        //general shared methods for photo actions
        AnewluvMessages deleteuserphoto(Guid photoid);

        AnewluvMessages makeuserphoto_private(Guid PhotoID);

        AnewluvMessages makeuserphoto_public(Guid PhotoID);
        
        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        AnewluvMessages addphotos(PhotoUploadViewModel model);

        AnewluvMessages addsinglephoto(PhotoUploadModel newphoto, int profileid);    

        bool checkvalidjpggif(byte[] image);
       
       // byte[] getgalleryphotobyscreenname(string strScreenName, photoformatEnum format);

        string getgalleryphotobyscreenname(string strScreenName, photoformatEnum format);   
        
        //byte[] getgalleryimagebyphotoid(Guid strPhotoID, photoformatEnum format);

        string getgalleryimagebyphotoid(Guid strPhotoID, photoformatEnum format);
       
       // byte[] getgalleryphotobyprofileid(int intProfileID, photoformatEnum format);

        string getgalleryphotobyprofileid(int intProfileID, photoformatEnum format);
       
       // byte[] getgalleryimagebynormalizedscreenname(string strScreenName, photoformatEnum format);

        string getgalleryimagebynormalizedscreenname(string strScreenName, photoformatEnum format);
        
        bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption);
      
        bool checkforgalleryphotobyprofileid(int intProfileID);
       
        bool checkforuploadedphotobyprofileid(int intProfileID);
      
       // byte[] getimagebytesfromurl(string _imageUrl, string source);

       string getimageb64stringfromurl(string _imageUrl, string source);   

           
      
    }
}
