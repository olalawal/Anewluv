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
using Microsoft.VisualBasic; 



namespace Shell.MVC2.Data
{
    public class PhotoRepository :MemberRepositoryBase, IPhotoRepository 
    {
       // private AnewluvContext _db;
        //TO DO move from ria servives
        private AnewluvContext _datingservice;


        public PhotoRepository(AnewluvContext datingcontext, AnewluvContext datingservice)
            : base(datingcontext)
        {          
            _datingservice = datingservice;
        }

        public List<photo> GetAllPhotos(string username)
        {
            // Retrieve All User's Photo's that have not deleted by Admin and User.
            var query = _datingcontext.photos.Where(o => o.profiledata.profile.username == username
                                && o.photostatus.id  != 4 && o.photostatus.id  != 5).ToList();

            return query;
        }
        public List<EditProfileViewPhotoModel> GetApproved(List<photo> MyPhotos, photoaprovalstatusEnum approvalstatus,
                                                                    int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.approvalstatus.id == (int)photoaprovalstatusEnum.Rejected);
          
            // Retrieve All User's Approved Photo's that are not Private and approved.
            if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in photos
                         select new EditProfileViewPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ScreenName = p.profiledata.profile.ScreenName,
                             CheckedPhoto = false,
                             PhotoDate = p.PhotoDate
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }


            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();



        }
       
        public EditProfilePhotoModel GetSingleProfilePhotobyphotoID(Guid photoid)
        {
            EditProfilePhotoModel model = (from p in _datingcontext.photos.Where(p => p.PhotoID == photoid)
                                           select new EditProfilePhotoModel
                                           {
                                               PhotoID = p.PhotoID,
                                               ProfileID = p.ProfileID,
                                               ScreenName = p.profiledata.profile.ScreenName,
                                               Aproved = p.Aproved,
                                               ProfileImageType = p.ProfileImageType,
                                               ImageCaption = p.ImageCaption,
                                               PhotoDate = p.PhotoDate,
                                               photostatus.id  = p.photostatus.id 
                                           }).Single();
            model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



            return (model);


        }
        public EditProfilePhotosViewModel GetEditPhotoViewModel(string username, string ApprovedYes, string NotApprovedNo,
                                                            int photostatus.id , int page, int pagesize)
        {
            var myPhotos = GetAllPhotos(username);
            var Approved = GetApproved(myPhotos, ApprovedYes, page, pagesize);
            var NotApproved = GetApproved(myPhotos, NotApprovedNo, page, pagesize);
            var PhotoStatus = GetPhotobyStatusID(myPhotos, photostatus.id , page, pagesize);
            var model = GetPhotoViewModel(Approved, NotApproved, PhotoStatus, myPhotos);

            return (model);
            
        }

        public void DeletedUserPhoto(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.photostatus.id  = 4; //deletebyuser
            PhotoModify.ProfileImageType = "NoStatus";
            // Update database
            

            _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }
        public void MakeUserPhoto_Private(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.photostatus.id  = 3; //private
            PhotoModify.ProfileImageType = "NoStatus";
            // Update database
            _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }
        public void MakeUserPhoto_Public(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = _datingcontext.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.photostatus.id  = 1; //public values:1 or 2 are public values
            // Update database
            _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            _datingcontext.SaveChanges();
        }

        public bool AddPhoto(photo model)
        {

            //initialize the dating service here
            //var datingservicecontext = new DatingService().Initialize();
            photo objProfilePhotos = new photo();
            
            //Here's where the ContentType column comes in handy.  By saving
            //  this to the database, it makes it infinitely easier to get it back
            //  later when trying to show the image.
            //ObjProfilePhotos.ContentType = file.ContentType;

            // Int32 length = file.ContentLength;
            //This may seem odd, but the fun part is that if
            //  I didn't have a temp image to read into, I would
            //  get memory issues for some reason.  Something to do
            //  with reading straight into the object's ActualImage property.

            try
            {

                Guid identifier = Guid.NewGuid();
                objProfilePhotos.ProfileImage = model.ProfileImage;
                objProfilePhotos.PhotoID = identifier;
                objProfilePhotos.PhotoSize = model.ProfileImage.Length;
                objProfilePhotos.Aproved = "No";
                objProfilePhotos.PhotoDate = System.DateTime.Now;
                objProfilePhotos.ImageCaption = model.ImageCaption;
                //profile ID was passed with when this instance of the dispatacher was created
                objProfilePhotos.ProfileImageType = "NoStatus";
                objProfilePhotos.photostatus.id  = 1;
                objProfilePhotos.ProfileID = model.ProfileID ;

                //TO DO move this out of RIA services to rest service
                _datingcontext.photos.AddObject(objProfilePhotos);
                _datingcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CheckValidJPGGIF(byte[] image)
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
        private IEnumerable<EditProfileViewPhotoModel> GetApprovedMinusGallery(IQueryable<photo> MyPhotos, string approved,
                                                                int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.Aproved == approved);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            if (approved == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3 && a.ProfileImageType != "Gallery"); }

            var model = (from p in photos
                         select new EditProfileViewPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ScreenName = p.profiledata.profile.ScreenName,
                             CheckedPhoto = false,
                             PhotoDate = p.PhotoDate
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }

            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize));

        }

        private IEnumerable<EditProfileViewPhotoModel> GetPhotobyStatusID(List<photo> MyPhotos, int photostatus.id ,
                                                                   int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.photostatus.id  == photostatus.id )
                         select new EditProfileViewPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ScreenName = p.profiledata.profile.ScreenName,
                             CheckedPhoto = false,
                             PhotoDate = p.PhotoDate
                         });

            if (model.Count() > pagesize) { pagesize = model.Count(); }
            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize));

        }

        private EditProfilePhotosViewModel GetPhotoViewModel(IEnumerable<EditProfileViewPhotoModel> Approved,
                                                            IEnumerable<EditProfileViewPhotoModel> NotApproved,
                                                            IEnumerable<EditProfileViewPhotoModel> Private,
                                                            List<photo> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            EditProfilePhotoModel src = new EditProfilePhotoModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.PhotoID equals x.PhotoID
                       select new EditProfilePhotoModel
                       {
                           PhotoID = p.PhotoID,
                           ScreenName = p.profiledata.profile.ScreenName,
                           ProfileID = p.ProfileID,
                           Aproved = p.Aproved,
                           ProfileImageType = p.ProfileImageType,
                           ImageCaption = p.ImageCaption,
                           PhotoDate = p.PhotoDate,
                           photostatus.id  = p.photostatus.id 
                       })
                             .OrderBy(o => o.ProfileImageType).ThenByDescending(o => o.PhotoDate)
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new EditProfilePhotoModel
                       {
                           PhotoID = p.PhotoID,
                           ScreenName = p.profiledata.profile.ScreenName,
                           ProfileID = p.ProfileID,
                           Aproved = p.Aproved,
                           ProfileImageType = p.ProfileImageType,
                           ImageCaption = p.ImageCaption,
                           PhotoDate = p.PhotoDate,
                           photostatus.id  = p.photostatus.id 
                       })
                               .OrderByDescending(o => o.PhotoDate)
                               .FirstOrDefault();
            }

            try
            {
                src.checkedPrimary = src.BoolImageType(src.ProfileImageType.ToString());
            }
            catch
            {


            }

            PhotoViewModel UploadPhotos = new PhotoViewModel();

            EditProfilePhotosViewModel ViewModel = new EditProfilePhotosViewModel
            {
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),
                Photo = UploadPhotos
            };


            return (ViewModel);


        }

        //end of private stuff

        //Stuff pulled from dating service regular
        // added by Deshola on 5/17/2011

        public byte[] GetGalleryPhotobyScreenName(string strScreenName)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
            GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.ScreenName == strScreenName)
                            join f in _datingcontext.photos on p.ProfileID equals f.ProfileID
                            where (f.Aproved == "Yes" & f.ProfileImageType == "Gallery")
                            select f);

            try
            {
                //End If
                if (GalleryPhoto.Count() > 0) return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }

       

        public byte[] GetGalleryImagebyPhotoId(Guid strPhotoID)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            GalleryPhoto = this._datingcontext.photos.Where(p => p.PhotoID == strPhotoID);

            //End If
            if (GalleryPhoto.Count() > 0)
            {
                return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();
            }
            else
            {
                return null;

            }


            // Return CInt(myQuery.First.ScreenName)

        }

        public byte[] GetGalleryPhotobyProfileID(string strProfileID)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            GalleryPhoto = this._datingcontext.photos.Where(p => p.ProfileID == strProfileID & p.Aproved == "Yes" & p.ProfileImageType == "Gallery");

            //End If
            if (GalleryPhoto.Count() > 0)
            {
                return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();
            }
            else
            {
                return null;

            }
        }

        public byte[] GetGalleryImagebyNormalizedScreenName(string strScreenName)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            //

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
            GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.ScreenName.Replace(" ", "") == strScreenName)
                            join f in _datingcontext.photos on p.ProfileID equals f.ProfileID
                            where (f.Aproved == "Yes" & f.ProfileImageType == "Gallery")
                            select f);


            try
            {
                //End If
                if (GalleryPhoto.Count() > 0) return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }

        //mobe this to a repository
        public photo UploadProfileImage(string _imageUrl, string caption)
        {

            //get current profileID from cache
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            // var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByusername(this.HttpContext.User.Identity.Name) :
            //     CachingFactory.GetProfileIDBySessionId(this.HttpContext);
            // var membersmodel = new MembersViewModel();
            //guests cannot upload photos
            // membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //add the files to a temporary photo model and store it in session for now?
            var photo = new photo();

            string imageUrl = _imageUrl;
            // string saveLocation = @"C:\someImage.jpg";

            try
            {
                byte[] imageBytes;
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

                photo.ImageCaption = caption;
                //photo.ImageUploaded = file;
                photo.ProfileImage = imageBytes;
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




            //stiore the photo in the model in this version not into server yet and store that model into session I guess
            //membersmodel.MyPhotos.Add(photo);

            //update the model in session, maybe latter just have it upload on the fly
            //Conditianl update i.e add to corrent Cache gues ot member
            //11-1-2001 removed guest stuff since guests cannot upload photos doh !!
            //  membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID) : null; //


            //FileStream fs = new FileStream(saveLocation, FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);
            //try
            //{
            //    bw.Write(imageBytes);
            //}
            //finally
            //{
            //    fs.Close();
            //    bw.Close();
            //}

            return photo;
        } 

        public bool InsertPhotoCustom(photo newphoto)
        {

            try
            {
                this._datingcontext.photos.AddObject(newphoto);

                this._datingcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckIfPhotoCaptionAlreadyExists(string strProfileID, string strPhotoCaption)
        {
            IQueryable<photo> myPhotoList = default(IQueryable<photo>);
            //Dim ctx As New Entities()


            myPhotoList = this._datingcontext.photos.Where(p => p.ProfileID == strProfileID && p.ImageCaption == strPhotoCaption);

            if (myPhotoList.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;

            }

            // Return CInt(myQuery.First.ScreenName)
        }

        public bool CheckForGalleryPhotobyProfileID(string strProfileID)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            GalleryPhoto = this._datingcontext.photos.Where(p => p.ProfileID == strProfileID & p.Aproved == "Yes" & p.ProfileImageType == "Gallery");

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

        public bool CheckForUploadedPhotobyProfileID(string strProfileID)
        {
            IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            GalleryPhoto = this._datingcontext.photos.Where(p => p.ProfileID == strProfileID);

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

    }
}
