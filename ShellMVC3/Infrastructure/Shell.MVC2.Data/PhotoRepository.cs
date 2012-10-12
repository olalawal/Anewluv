using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

//TO DO move these services to a new WCF service for now use it from the domain service
using Dating.Server.Data.Services;

using System.Data;
using System.Data.Entity;
using System.Web;

using Shell.MVC2.Interfaces;
using System.Net;
using System.IO;

using System.Drawing;
using ImageResizer;



namespace Shell.MVC2.Data
{
    public class PhotoRepository :MemberRepositoryBase, IPhotoRepository 
    {
       // private AnewluvContext _db;
        //TO DO move from ria servives
        private IMemberRepository  _memberepository;


        public PhotoRepository(AnewluvContext datingcontext, IMemberRepository memberepository)
            : base(datingcontext)
        {          
            _memberepository = memberepository;
        }


        public List<photo> getallphotosbyusername(string username)
        {
            // Retrieve All User's Photo's that have not deleted by Admin and User.
            var query = _datingcontext.photos.Where(o => o.profilemetadata.profile.username == username
                                && o.photostatus.id  != 4 && o.photostatus.id  != 5).ToList();

            return query;
        }

        public List<PhotoEditModel> getphotosbyprofileidandstatus(string profile_id, photoapprovalstatusEnum status)
        {
            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in _datingcontext.photos.Where(a => a.approvalstatus.id == (int)status)
                         select new PhotoEditModel
                         {
                             photoid = p.id,
                             profileid = p.profile_id,
                             screenname = p.profilemetadata.profile.screenname,
                             aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.imagetype.description,
                             imagecaption = p.imagecaption,
                             photodate = p.creationdate,
                             photostatusid = p.photostatus.id,
                             checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                         });


           


            return (model.OrderByDescending(u => u.photodate).ToList();



        }

        public List<PhotoEditModel> getpagedphotosbyprofileidstatus(string profile_id, photoapprovalstatusEnum status,
                                                                    int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in _datingcontext.photos.Where(a => a.approvalstatus.id == (int)status)
                         select new PhotoEditModel
                         {
                             photoid = p.id,
                             profileid = p.profile_id,
                             screenname = p.profilemetadata.profile.screenname,
                             aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.imagetype.description,
                             imagecaption = p.imagecaption,
                             photodate = p.creationdate,
                             photostatusid = p.photostatus.id,
                             checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }


            return (model.OrderByDescending(u => u.photodate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();



        }
   
       
        public PhotoEditModel getsingleprofilephotobyphotoid(Guid photoid)
        {
            PhotoEditModel model = (from p in _datingcontext.photos.Where(p => p.id  == photoid)
                                          select new PhotoEditModel
                                           {
                                               photoid = p.id ,
                                               profileid = p.profile_id,
                                               screenname = p.profilemetadata.profile.screenname ,
                                               aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true:false ,
                                               profileimagetype = p.imagetype.description ,
                                                imagecaption  = p.imagecaption ,
                                               photodate   = p.creationdate ,
                                              photostatusid   = p.photostatus.id ,
                                              checkedprimary = (p.photostatus.id == (int) photostatusEnum.Gallery)                                             
                                           }).Single();
           
           // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



            return (model);


        }    

        //TO DO get photo albums as well ?
        public PhotoEditViewModel getpagededitphotoviewmodel(string username, string ApprovedYes, string NotApprovedNo,
                                                           photoapprovalstatusEnum  approvalstatus , int page, int pagesize)
        {
            var myPhotos = getallphotosbyusername(username);
            var ApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, page, pagesize);
            var NotApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, page, pagesize);
            //TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
            //since this is edit mode that is fine
            var PrivatePhotos = filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, page, pagesize);
            var model = getphotoeditviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

            return (model);
            
        }

        public void deleteduserphoto(Guid photoid)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.id == photoid);
           // PhotoModify.photostatus.id  = 4; //deletebyuser
            PhotoModify.photostatus.id  = (int)photostatusEnum.deletedbyuser ;
            // Update database
            

            //_datingcontext  //.PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }
        public void makeuserphoto_private(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.id == PhotoID);
            //check if this phot is already set private if not set it 
           // PhotoModify.photostatus.id  = 3; //private
           if (PhotoModify.securitylevels.Any(z=>z.id  != (int)securityleveltypeEnum.Private ))
           {
               PhotoModify.securitylevels.Add(new lu_securityleveltype { id = (int)securityleveltypeEnum.Private });
              // newsecurity.id = (int)securityleveltypeEnum.Private;
           }
           // PhotoModify.ProfileImageType = "NoStatus";
            // Update database
          //  _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }
        public void makeuserphoto_public(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.id  == PhotoID);
            PhotoModify.photostatus.id  = 1; //public values:1 or 2 are public values
            // Update database
           // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }

        //9-18-2012 olawal when this is uploaded now we want to do the image conversions as well for the large photo and the thumbnail
        //since photo is only a row no big deal if duplicates but since conversion is required we must roll back if the photo already exists
        public bool addphotos(PhotoUploadViewModel model)
        {

             List<photoconversion> convertedphotos = new List<photoconversion>();
             

            foreach (PhotoUploadModel  item in model.photosuploaded)
            {
                //System.Console.WriteLine(i);
                try
                {
                    photo NewPhoto = new photo();
                    Guid identifier = Guid.NewGuid();
                    NewPhoto.imagetype = item.imagetype;
                    NewPhoto.id = identifier;
                    NewPhoto.profile_id = model.profileid; //model.ProfileImage.Length;
                    // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                    NewPhoto.creationdate  = item.creationdate;
                    NewPhoto.imagecaption  =  item.caption ;
                    //profile ID was passed with when this instance of the dispatacher was created
                    // NewPhoto.ProfileImageType = "NoStatus";
                    //NewPhoto.photostatus.id = 1;
                    //NewPhoto.ProfileID = model.ProfileID;
                    
                    //TO DO move this out of RIA services to rest service
                    _datingcontext.photos.Add(NewPhoto);
                   //dont save changes yet we can possibly remove or detach photos if they are dupes
                  var temp = addphotoconverions(NewPhoto, item);
                  if (temp.Count > 0)
                  {
                       foreach (photoconversion convertedphoto in temp)
                       {
                           //if this does not recognise the photo object we might need to save that and delete it later
                           _datingcontext.photoconversions.Add(convertedphoto);
                       }                      
                      _datingcontext.SaveChanges();                    
                  }
                  else
                  {
                      _datingcontext.Dispose();

                  }
                }
               


                catch
                {    //log error
                    return false;
                }

             
             

            }


            return true;

         

        }

        /// <summary>
        /// for adding as single photo withoute VM 
        /// replaces InseartPhotoCustom , maybe add the profileID but i dont want to
        /// </summary>
        /// <param name="newphoto"></param>
        /// <returns></returns>
        public bool addsinglephoto(PhotoUploadModel newphoto,int profileid)
        {

            //System.Console.WriteLine(i);
            try
            {
                photo NewPhoto = new photo();
                Guid identifier = Guid.NewGuid();
                NewPhoto.imagetype = newphoto.imagetype;
                NewPhoto.id = identifier;
                NewPhoto.profile_id = profileid; //model.ProfileImage.Length;
                // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                NewPhoto.creationdate = newphoto.creationdate;
                NewPhoto.imagecaption = newphoto.caption;
                //profile ID was passed with when this instance of the dispatacher was created
                // NewPhoto.ProfileImageType = "NoStatus";
                //NewPhoto.photostatus.id = 1;
                //NewPhoto.ProfileID = model.ProfileID;

                //TO DO move this out of RIA services to rest service
                _datingcontext.photos.Add(NewPhoto);
                //dont save changes yet we can possibly remove or detach photos if they are dupes
                var temp = addphotoconverions(NewPhoto, newphoto);
                if (temp.Count > 0)
                {
                    foreach (photoconversion convertedphoto in temp)
                    {
                        //if this does not recognise the photo object we might need to save that and delete it later
                        _datingcontext.photoconversions.Add(convertedphoto);
                    }
                    _datingcontext.SaveChanges();
                }
                else
                {
                    _datingcontext.Dispose();

                }
            }



            catch
            {    //log error
                return false;
            }

            return true;
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
            //TemporaryImageUpload tempImageUpload = new TemporaryImageUpload();             
            // tempImageUpload = _service.GetImageData(id) ?? null;
            List<photoconversion> convertedphotos = new List<photoconversion>();

            if (photouploaded.image != null)
            {

                try
                {

                    foreach (lu_photoformat currentformat in _datingcontext.lu_photoformat.ToList() )
                    {
                        byte[] byteArray = photouploaded.image; //tempImageUpload.TempImageData; 
                        using (var outStream = new MemoryStream())
                        {
                            using (var inStream = new MemoryStream(byteArray))
                            {
                                //var settings = new ResizeSettings("maxwidth=200&maxheight=200");
                                var settings = new ResizeSettings(currentformat.imageresizerformat.description);
                                ImageResizer.ImageBuilder.Current.Build(inStream, outStream, settings);
                                var outBytes = outStream.ToArray();

                                //handle the check to see if this user has photos of the same size allredy which means a duplicate
                                if (!_datingcontext.photoconversions.Where(p => p.photo.profile_id == photo.profile_id).Any(p => p.size == outBytes.Length))
                                {
                                    convertedphotos.Add(new photoconversion
                                    {
                                        creationdate = DateTime.Now,
                                        description = currentformat.description,
                                        image = outBytes,
                                        size = outBytes.Length,
                                        photo = photo
                                    });
                                }

                                
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            return convertedphotos;


        }

        public bool checkvalidjpggif(byte[] image)
        {

            try
            {
                using (MemoryStream ms = new MemoryStream(image))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;  


            //if (file.ContentType.ToLower() == "image/gif" || file.ContentType.ToLower() == "image/jpeg" ||
            //    file.ContentType.ToLower() == "image/pjpeg" || file.ContentType.ToLower() == "image/x-png" ||
            //    file.ContentType.ToLower() == "image/jpg" || file.ContentType.ToLower() == "image/bmp")
            //{

            //    return true;
            //}

            //else
            //{
            //    return false;

            //}
        }

        //Private functions btw not exposed 
        //gets all approved non prviate photos athat are not gallery 
        #region "private functions"
      
        private IEnumerable<PhotoEditModel> filterphotosapprovedminusgallery(IQueryable<photo> MyPhotos, photoapprovalstatusEnum status,
                                                                int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photostatus.id == (int)status);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //TO DO modifiy this code to filter out the private photos and photos that are part of private albums 
            if (status == photoapprovalstatusEnum.Approved ) 
            {
                photos = photos.Where(a => a.imagetype.id  !=  (int)photostatusEnum.Gallery ); 
            }

            var model = (from p in photos
                         select new PhotoEditModel
                                            {
                                                photoid = p.id,
                                                profileid = p.profile_id,
                                                screenname = p.profilemetadata.profile.screenname,
                                                aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                                profileimagetype = p.imagetype.description,
                                                imagecaption = p.imagecaption,
                                                photodate = p.creationdate,
                                                photostatusid = p.photostatus.id,
                                                checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                                            });


            if (model.Count() > pagesize) { pagesize = model.Count(); }

            return (model.OrderByDescending(u => u.photodate ).Skip((page - 1) * pagesize).Take(pagesize));

        }

        private IEnumerable<PhotoEditModel> filterphotosbysecuitylevel(List<photo> MyPhotos, securityleveltypeEnum status,
                                                                   int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.securitylevels.Any(d => d.id == (int)status))
                         select new PhotoEditModel
                         {
                             photoid = p.id,
                             profileid = p.profile_id,
                             screenname = p.profilemetadata.profile.screenname,
                             aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.imagetype.description,
                             imagecaption = p.imagecaption,
                             photodate = p.creationdate,
                             photostatusid = p.photostatus.id,
                             checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }
            return (model.OrderByDescending(u => u.photodate ).Skip((page - 1) * pagesize).Take(pagesize));

        }

        private List<PhotoEditModel> filterandpagephotosbystatus(List<photo> MyPhotos, photoapprovalstatusEnum approvalstatus,
                                                               int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.approvalstatus.id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in photos
                         select new PhotoEditModel
                         {
                             photoid = p.id,
                             profileid = p.profile_id,
                             screenname = p.profilemetadata.profile.screenname,
                             aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.imagetype.description,
                             imagecaption = p.imagecaption,
                             photodate = p.creationdate,
                             photostatusid = p.photostatus.id,
                             checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }


            return (model.OrderByDescending(u => u.photodate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();



        }

        private PhotoEditViewModel getphotoeditviewmodel(IEnumerable<PhotoEditModel> Approved,
                                                            IEnumerable<PhotoEditModel> NotApproved,
                                                            IEnumerable<PhotoEditModel> Private,
                                                            List<photo> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoEditModel src = new PhotoEditModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.id  equals x.photoid
                       select new PhotoEditModel
                       {
                           photoid = p.id,
                           profileid = p.profile_id,
                           screenname = p.profilemetadata.profile.screenname,
                           aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           profileimagetype = p.imagetype.description,
                           imagecaption = p.imagecaption,
                           photodate = p.creationdate,
                           photostatusid = p.photostatus.id,
                           checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                             .OrderBy(o => o.profileimagetype ).ThenByDescending(o => o.photodate )
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new PhotoEditModel
                       {
                           photoid = p.id,
                           profileid = p.profile_id,
                           screenname = p.profilemetadata.profile.screenname,
                           aproved = (p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           profileimagetype = p.imagetype.description,
                           imagecaption = p.imagecaption,
                           photodate = p.creationdate,
                           photostatusid = p.photostatus.id,
                           checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                               .OrderByDescending(o => o.photodate )
                               .FirstOrDefault();
            }


            //handled in the query
            //try
            //{
            //    src.checkedPrimary = src.BoolImageType(src.ProfileImageType.ToString());
            //}
            //catch
            //{


            //}

            PhotoUploadViewModel UploadPhotos = new PhotoUploadViewModel();

            PhotoEditViewModel ViewModel = new PhotoEditViewModel
            {
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),
                 PhotosUploading = UploadPhotos
            };


            return (ViewModel);


        }

        #endregion
        //end of private stuff

        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

        public byte[] getgalleryphotobyscreenname(string strScreenName)
        {
            //IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
          var  GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.screenname  == strScreenName)
                            join f in _datingcontext.photos on p.id  equals f.profile_id 
                            where (f.approvalstatus.id   ==  (int)photoapprovalstatusEnum.Approved  
                            && f.imagetype.id  == (int)photostatusEnum.Gallery  )
                            select f).FirstOrDefault();

            try
            {
                //new code to only get the gallery conversion copy
                return GalleryPhoto.conversions
               .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image ;

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }       

        public byte[] getgalleryimagebyphotoid(Guid strPhotoID)
        {
           // IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
           var GalleryPhoto = this._datingcontext.photos.Where(p => p.id  == strPhotoID 
               &&  p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved & p.imagetype.id  == (int)photostatusEnum.Gallery ).FirstOrDefault();

            try
            {
                //new code to only get the gallery conversion copy
                return GalleryPhoto.conversions
               .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }

        public byte[] getgalleryphotobyprofileid(int intProfileID)
        {
           // IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
           var GalleryPhoto = this._datingcontext.photos
          .Where(p => p.profile_id == intProfileID &&
          p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved & p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault();


            try
            {
                //new code to only get the gallery conversion copy
                return GalleryPhoto.conversions
               .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;

            }
            catch
            {

            }
            return null;

        }

        public byte[] getgalleryimagebynormalizedscreenname(string strScreenName)
        {
            //IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            //

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
          var  GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.screenname.Replace(" ", "") == strScreenName)
                            join f in _datingcontext.photos on p.id equals f.profile_id
                               where (f.approvalstatus.id == (int)photoapprovalstatusEnum.Approved && f.imagetype.id == (int)photostatusEnum.Gallery)
                            select f).FirstOrDefault();


             try
            {
                //new code to only get the gallery conversion copy
                return GalleryPhoto.conversions
               .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }     

        public bool checkifphotocaptionalreadyexists(int intProfileID, string strPhotoCaption)
        {
            //IQueryable<photo> myPhotoList = default(IQueryable<photo>);
            //Dim ctx As New Entities()


            var myPhotoList = this._datingcontext.photos.Where(p => p.profile_id  == intProfileID && p.imagecaption  == strPhotoCaption).FirstOrDefault();

            if (myPhotoList != null)
            {
                return true;
            }
            else
            {
                return false;

            }

            // Return CInt(myQuery.First.ScreenName)
        }

        public bool checkforgalleryphotobyprofileid(int intProfileID)
        {
           // IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            var GalleryPhoto = this._datingcontext.photos.Where(p => p.profile_id  == intProfileID &&
                p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved && p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault(); 

            if (GalleryPhoto != null)
            {
                return true;
            }
            else
            {
                return false;

            }
            // Return CInt(myQuery.First.ScreenName)
        }

        public bool checkforuploadedphotobyprofileid(int intProfileID)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            GalleryPhoto = this._datingcontext.photos.Where(p => p.profile_id   == intProfileID);

            if (GalleryPhoto.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;

            }
            // Return CInt(myQuery.First.ScreenName)
        }


        /// <summary>
        /// gets the bytes of an image from a URL, useeful in grabbing images from 3rd parties such as FB
        /// </summary>
        /// <param name="_imageUrl"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public byte[] getimagebytesfromurl(string _imageUrl, string source)
        {

            //get current profileID from cache
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            // var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByusername(this.HttpContext.User.Identity.Name) :
            //     CachingFactory.GetProfileIDBySessionId(this.HttpContext);
            // var membersmodel = new MembersViewModel();
            //guests cannot upload photos
            // membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //add the files to a temporary photo model and store it in session for now?
           // var photo = new photo();

            string imageUrl = _imageUrl;
            byte[] imageBytes;
            // string saveLocation = @"C:\someImage.jpg";

            try
            {
               
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                WebResponse imageResponse = imageRequest.GetResponse();
                Stream responseStream = imageResponse.GetResponseStream();

                using (BinaryReader br = new BinaryReader(responseStream))
                {
                    imageBytes = br.ReadBytes(500000);
                    br.Close();
                }
                responseStream.Close();
                imageResponse.Close();

                return imageBytes;
                //photo.imagecaption = caption;
                //photo.ImageUploaded = file;
                // photo.ProfileImage = imageBytes;
            }
            catch (WebException webEx)
            {
                // Now you can access webEx.Response object that contains more info on the server response               
                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)webEx.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)webEx.Response).StatusDescription);
                }
            }


            return null;
        }         

    }
}
