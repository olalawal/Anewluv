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

        #region "private Data Context methods that are not to be serialzied"
        
        private List<photo> getallphotosbyusername(string username)
            {
                // Retrieve All User's Photo's that have not deleted by Admin and User.
                var query = _datingcontext.photos.Where(o => o.profilemetadata.profile.username == username
                                    && o.photostatus.id != 4 && o.photostatus.id != 5).ToList();

                return query;
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
        private List<photoconversion> addphotoconverions(photo photo, PhotoUploadModel photouploaded)
        {
            //TemporaryImageUpload tempImageUpload = new TemporaryImageUpload();             
            // tempImageUpload = _service.GetImageData(id) ?? null;
            List<photoconversion> convertedphotos = new List<photoconversion>();

            if (photouploaded.image != null)
            {

                try
                {

                    foreach (lu_photoformat currentformat in _datingcontext.lu_photoformat.ToList())
                    {
                        byte[] byteArray = photouploaded.image; //tempImageUpload.TempImageData; 
                        using (var outStream = new MemoryStream())
                        {
                            using (var inStream = new MemoryStream(byteArray))
                            {
                                //var settings1 = new ResizeSettings("maxwidth=200&maxheight=200");
                                var settings = new ResizeSettings(currentformat.imageresizerformat.description);
                                ImageResizer.ImageBuilder.Current.Build(inStream, outStream, settings);
                                var outBytes = outStream.ToArray();
                                //double check that there is no conversion with matching size and the name
                                var test = _datingcontext.photoconversions.Where(p => p.photo.profile_id == photo.profile_id).Any(p => p.size == photo.size);

                                convertedphotos.Add(new photoconversion
                                {
                                    creationdate = DateTime.Now,
                                    description = currentformat.description,
                                    formattype = currentformat,
                                    image = outBytes,
                                    size = outBytes.Length,
                                    photo_id = photo.id
                                });



                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    var dd = ex.ToString();
                }
            }
            return convertedphotos;


        }

        #endregion

        #region "View Photo models"

        public PhotoModel getphotomodelbyphotoid(Guid photoid,photoformatEnum format)
        {
            PhotoModel model = (from p in _datingcontext.photoconversions.Where(a => a.formattype.id == (int)format && ( a.photo.id == photoid))
                                    select new PhotoModel
                                    {
                                        photoid = p.photo.id,
                                        profileid = p.photo.profile_id,
                                        screenname = p.photo.profilemetadata.profile.screenname,
                                        photo = p.image,
                                        photoformat = p.formattype,
                                        convertedsize = p.size,
                                        orginalsize = p.photo.size,
                                        imagecaption = p.photo.imagecaption,
                                        creationdate = p.photo.creationdate,
                                        
                                    }).Single();

            // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



            return (model);


        }

        public List<PhotoModel> getphotomodelsbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status,photoformatEnum format)
        {
            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in _datingcontext.photoconversions.Where(a => a.formattype.id == (int)format && a.photo.profile_id == profile_id && 
                ((a.photo.approvalstatus != null && a.photo.approvalstatus.id == (int)status)))
                         select new PhotoModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             photo = p.image,
                             photoformat = p.formattype,
                             convertedsize = p.size,
                             orginalsize = p.photo.size,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,                         
                         });





            return (model.OrderByDescending(u => u.creationdate).ToList());



        }

        public List<PhotoModel> getpagedphotomodelbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status,photoformatEnum format,
                                                                   int page, int pagesize)
        {

            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            try
            {
                var model = (from p in _datingcontext.photoconversions.Where(a => a.formattype.id == (int)format && a.photo.profile_id == profile_id && a.photo.profile_id == profile_id && (
                                                     a.photo.approvalstatus != null && a.photo.approvalstatus.id == (int)status))
                             select new PhotoModel
                             {
                                 photoid = p.photo.id,
                                 profileid = p.photo.profile_id,
                                 screenname = p.photo.profilemetadata.profile.screenname,
                                 photo = p.image,
                                 photoformat = p.formattype ,
                                 convertedsize = p.size,
                                 orginalsize = p.photo.size,
                                 imagecaption = p.photo.imagecaption,
                                 creationdate = p.photo.creationdate,
                             });

                if (model.Count() > pagesize) { pagesize = model.Count(); }


                return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();
            }
            catch (Exception ex)
            {

            }

            return null;

        }

        //TO DO get photo albums as well ?
        public PhotoViewModel getpagedphotoviewmodelbyprofileid(int profileid, photoformatEnum format,int page, int pagesize)
        {
            //var myPhotos = _datingcontext.photoconversions.Where(z => z.profile_id == profileid).ToList();
            //var ApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, page, pagesize);
            //var NotApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, page, pagesize);
            ////TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
            ////since this is edit mode that is fine
            //var PrivatePhotos = filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, page, pagesize);
            ////var model = getphotoviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

            //// return (model);
            return null;

        }


        //Private filtering methods :
        private PhotoViewModel getphotoviewmodel(IEnumerable<PhotoModel> Approved,
                                                           IEnumerable<PhotoModel> NotApproved,
                                                           IEnumerable<PhotoModel> Private,
                                                           List<photoconversion> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoModel src = new PhotoModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.photo.id equals x.photoid
                       select new PhotoModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           photoformat = p.formattype,
                              convertedsize = p.size ,
                           //   approved = (p.approvalstatus != null && p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           //   profileimagetype = p.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,                          
                           //   checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                             .OrderBy(o => o.creationdate)
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new PhotoModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           //   approved = (p.approvalstatus != null && p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           //  profileimagetype = p.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                        //   photostatusid = p.photostatus.id,
                           //  checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                               .OrderByDescending(o => o.creationdate)
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

            // PhotoUploadViewModel UploadPhotos = new PhotoUploadViewModel();

            PhotoViewModel ViewModel = new PhotoViewModel
            {
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),

            };


            return (ViewModel);


        }

     
        #endregion
        
        #region "Edititable Photo models 
        
        public PhotoEditModel getphotoeditmodelbyphotoid(Guid photoid, photoformatEnum format)
        {
            PhotoEditModel model = (from p in _datingcontext.photoconversions.Where(p =>p.formattype.id == (int)format && p.photo.id == photoid)
                                    select new PhotoEditModel
                                    {
                                        photoid = p.photo.id,
                                        profileid = p.photo.profile_id,
                                        screenname = p.photo.profilemetadata.profile.screenname,
                                        approved = (p.photo.approvalstatus != null && p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                        profileimagetype = p.photo.imagetype.description,
                                        imagecaption = p.photo.imagecaption,
                                        creationdate = p.photo.creationdate,
                                        photostatusid = p.photo.photostatus.id,
                                        checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                                    }).Single();

            // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



            return (model);


        }

        public List<PhotoEditModel> getphotoeditmodelsbyprofileidandstatus(int profile_id, photoapprovalstatusEnum status, photoformatEnum format)
        {
            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in _datingcontext.photoconversions.Where(a => a.formattype.id == (int)format
                && a.photo.approvalstatus != null && a.photo.approvalstatus.id == (int)status)
                         select new PhotoEditModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.photo.imagetype.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus.id,
                             checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                         });





            return (model.OrderByDescending(u => u.creationdate).ToList());



        }

        public List<PhotoEditModel> getpagedphotoeditmodelsbyprofileidstatus(int profile_id, photoapprovalstatusEnum status,photoformatEnum format,
                                                              int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            //var photos = MyPhotos.Where(a => a.approvalstatus.id  == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in _datingcontext.photoconversions.Where(a =>a.formattype.id == (int)format && a.photo.approvalstatus != null 
                && a.photo.approvalstatus.id == (int)status)
                         select new PhotoEditModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.photo.imagetype.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus.id,
                             checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }


            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();



        }
         

        ////TO DO get photo albums as well ?
        //public PhotoEditViewModel getpagededitphotoviewmodelbyprofileid(int profileid,photoformatEnum format, int page, int pagesize)
        //{
        //    var myPhotos = _datingcontext.photos.Where(z => z.profile_id == profileid).ToList();
        //    var ApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, page, pagesize);
        //    var NotApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, page, pagesize);
        //    //TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
        //    //since this is edit mode that is fine
        //    var PrivatePhotos = filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, page, pagesize);
        //    var model = getphotoeditviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

        //    return (model);

        //}

        //TO DO get photo albums as well ?
        //12-10-2012 this also filters the format
        public PhotoEditViewModel getpagededitphotoviewmodelbyprofileidandformat(int profileid,photoformatEnum format, int page, int pagesize)
        {
            var myPhotos = _datingcontext.photoconversions.Where(z => z.formattype.id == (int)format && z.photo.profile_id == profileid).ToList();
            var ApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.Approved, page, pagesize);
            var NotApprovedPhotos = filterandpagephotosbystatus(myPhotos, photoapprovalstatusEnum.NotReviewed, page, pagesize);
            //TO DO need to discuss this all photos should be filtered by security level for other users not for your self so 
            //since this is edit mode that is fine
            var PrivatePhotos = filterphotosbysecuitylevel(myPhotos, securityleveltypeEnum.Private, page, pagesize);
            var model = getphotoeditviewmodel(ApprovedPhotos, NotApprovedPhotos, PrivatePhotos, myPhotos);

            return (model);

        }

        //Filter methods for edit photo models
        private IEnumerable<PhotoEditModel> filterphotosapprovedminusgallery(IQueryable<photoconversion> MyPhotos, 
                                                        photoapprovalstatusEnum status,
                                                            int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.photostatus.id == (int)status);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //TO DO modifiy this code to filter out the private photos and photos that are part of private albums 
            if (status == photoapprovalstatusEnum.Approved)
            {
                photos = photos.Where(a => a.photo.imagetype.id != (int)photostatusEnum.Gallery);
            }

            var model = (from p in photos
                         select new PhotoEditModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus != null && p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.photo.imagetype.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus.id,
                             checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }

            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize));

        }

        private IEnumerable<PhotoEditModel> filterphotosbysecuitylevel(List<photoconversion> MyPhotos, securityleveltypeEnum status,
                                                                        int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.photo.photosecuritylevels.Any(d => d.securityleveltype.id == (int)status))
                         select new PhotoEditModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus != null && p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.photo.imagetype.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus.id,
                             checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }
            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize));

        }

        //format should be known by down
        private List<PhotoEditModel> filterandpagephotosbystatus(List<photoconversion> MyPhotos, photoapprovalstatusEnum approvalstatus,
                                                               int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.approvalstatus != null && a.photo.approvalstatus.id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in photos
                         select new PhotoEditModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.photo.imagetype.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus.id,
                             checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }


            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize)).ToList();



        }

        private PhotoEditViewModel getphotoeditviewmodel(IEnumerable<PhotoEditModel> Approved,
                                                            IEnumerable<PhotoEditModel> NotApproved,
                                                            IEnumerable<PhotoEditModel> Private,
                                                            List<photoconversion> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoEditModel src = new PhotoEditModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.photo.id equals x.photoid
                       select new PhotoEditModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           approved = (p.photo.approvalstatus != null && p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           profileimagetype = p.photo.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus.id,
                           checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                             .OrderBy(o => o.profileimagetype).ThenByDescending(o => o.creationdate)
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new PhotoEditModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           approved = (p.photo.approvalstatus != null && p.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           profileimagetype = p.photo.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus.id,
                           checkedprimary = (p.photo.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                               .OrderByDescending(o => o.creationdate)
                               .FirstOrDefault();
            }


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
               

      
        //general shared methods for photo actions
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
           if (PhotoModify.photosecuritylevels.Any(z=>z.id  != (int)securityleveltypeEnum.Private ))
           {
               PhotoModify.photosecuritylevels.Add(new photo_securitylevel { photo_id = PhotoID ,
                   securityleveltype = _datingcontext.lu_securityleveltype.Where(p=>p.id == (int)securityleveltypeEnum.Private).FirstOrDefault()  });
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
                //System.Console.WriteLine(i);
                try
                {
                    //only add photos that are not dupes 
                    if (!_datingcontext.photos.Where(p => p.profile_id == model.profileid).Any(p => p.size == item.size && p.imagename == item.imagename))
                    {
                        photo NewPhoto = new photo();
                        Guid identifier = Guid.NewGuid();                       
                        NewPhoto.id = identifier;
                        NewPhoto.profile_id = model.profileid; //model.ProfileImage.Length;
                        // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                        NewPhoto.creationdate = item.creationdate;
                        NewPhoto.imagecaption = item.caption;
                        NewPhoto.imagename = item.imagename; //11-26-2012 olawal added the name for comparisons 
                        NewPhoto.size = item.size.GetValueOrDefault();
                        //set the rest of the information as needed i.e approval status refecttion etc
                        NewPhoto.imagetype = (item.imagetypeid != null) ? _datingcontext.lu_photoimagetype.Where(p => p.id == item.imagetypeid).FirstOrDefault() : null; // : null; item.imagetypeid;
                        NewPhoto.approvalstatus = (item.approvalstatusid  != null) ? _datingcontext.lu_photoapprovalstatus.Where(p => p.id == item.approvalstatusid ).FirstOrDefault() : null;
                        NewPhoto.rejectionreason = (item.rejectionreasonid  != null) ? _datingcontext.lu_photorejectionreason.Where(p => p.id == item.rejectionreasonid ).FirstOrDefault() : null;
                        NewPhoto.photostatus = (item.photostatusid  != null) ? _datingcontext.lu_photostatus .Where(p => p.id == item.photostatusid).FirstOrDefault() : null;
                       
                        //profile ID was passed with when this instance of the dispatacher was created
                        // NewPhoto.ProfileImageType = "NoStatus";
                        //NewPhoto.photostatus.id = 1;
                        //NewPhoto.ProfileID = model.ProfileID;

                        //TO DO move this out of RIA services to rest service

                        //get the existing photos for a user to compare the size so we do not import dupes
                        // existing
                        _datingcontext.photos.Add(NewPhoto);
                        //save the  photo here since we are somewhat checking before we try to crreate the conversions in the surrounding if
                        //olawal 12-26-2012
                        _datingcontext.SaveChanges();


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

                }

                catch (Exception ex)
                {    //log error
                    string dd = ex.ToString();
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
                //only add photos that are not dupes 
                if (!_datingcontext.photos.Where(p => p.profile_id == profileid).Any(p => p.size == newphoto.size && p.imagename == newphoto.imagename ))
                {
                    photo NewPhoto = new photo();
                    Guid identifier = Guid.NewGuid();
                    NewPhoto.id = identifier;
                    NewPhoto.profile_id = profileid; //model.ProfileImage.Length;
                    // NewPhoto.reviewstatus = "No"; not sure what to do with review status 
                    NewPhoto.creationdate = newphoto.creationdate;
                    NewPhoto.imagecaption = newphoto.caption;
                    NewPhoto.imagename = newphoto.imagename; //11-26-2012 olawal added the name for comparisons 
                    NewPhoto.size = newphoto.size.GetValueOrDefault();
                    //set the rest of the information as needed i.e approval status refecttion etc
                    NewPhoto.imagetype = (newphoto.imagetypeid != null) ? _datingcontext.lu_photoimagetype.Where(p => p.id == newphoto.imagetypeid).FirstOrDefault() : null; // : null; newphoto.imagetypeid;
                    NewPhoto.approvalstatus = (newphoto.approvalstatusid != null) ? _datingcontext.lu_photoapprovalstatus.Where(p => p.id == newphoto.approvalstatusid).FirstOrDefault() : null;
                    NewPhoto.rejectionreason = (newphoto.rejectionreasonid != null) ? _datingcontext.lu_photorejectionreason.Where(p => p.id == newphoto.rejectionreasonid).FirstOrDefault() : null;
                    NewPhoto.photostatus = (newphoto.photostatusid != null) ? _datingcontext.lu_photostatus.Where(p => p.id == newphoto.photostatusid).FirstOrDefault() : null;
                       

                    //TO DO move this out of RIA services to rest service

                    //get the existing photos for a user to compare the size so we do not import dupes
                    // existing
                    _datingcontext.photos.Add(NewPhoto);
                    //save the  photo here since we are somewhat checking before we try to crreate the conversions in the surrounding if
                    //olawal 12-26-2012
                    _datingcontext.SaveChanges();
                    

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

            }

            catch (Exception  ex)
            {    //log error
                string dd   = ex.ToString();
            }

            return true;
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


        public byte[] getgalleryphotobyscreenname(string strScreenName, photoformatEnum format)
        {
            //IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
          //var  GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.screenname  == strScreenName)
          //                  join f in _datingcontext.photos on p.id  equals f.profile_id
          //                     where (f.approvalstatus != null && f.approvalstatus.id == (int)photoapprovalstatusEnum.Approved  
          //                  && f.imagetype.id  == (int)photostatusEnum.Gallery  )
          //                  select f).FirstOrDefault();


          var GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.screenname == strScreenName)
                              join f in _datingcontext.photoconversions on p.id equals f.photo.profile_id
                              where (f.formattype.id == (int)format && f.photo.approvalstatus != null &&
                              f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                              f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                              select f).FirstOrDefault();



            try
            {
                //new code to only get the gallery conversion copy
              //  return GalleryPhoto.conversions
              // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image ;
                return GalleryPhoto.image;

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }

        public byte[] getgalleryimagebyphotoid(Guid strPhotoID, photoformatEnum format)
        {
           // IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
         //  var GalleryPhoto = this._datingcontext.photos.Where(p =>( p.id  == strPhotoID)
         //      && p.approvalstatus != null && p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved & p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault();

           var GalleryPhoto = ( from f in _datingcontext.photoconversions.Where(f=> f.photo.id == strPhotoID &&  
               f.formattype.id == (int)format && f.photo.approvalstatus != null &&
               f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
               f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                               select f).FirstOrDefault();


            try
            {
                //new code to only get the gallery conversion copy
               //return GalleryPhoto.conversions
              // .Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                return GalleryPhoto.image;

            }
            catch
            {

            }
            return null;



            // Return CInt(myQuery.First.ScreenName)

        }

        public byte[] getgalleryphotobyprofileid(int intProfileID, photoformatEnum format)
        {
           // IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
          // var GalleryPhoto = this._datingcontext.photos
          //.Where(p => (p.profile_id == intProfileID) && (p.approvalstatus != null) &&
          //p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved & p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault();

           var GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.id == intProfileID)
                               join f in _datingcontext.photoconversions on p.id equals f.photo.profile_id
                               where (f.formattype.id == (int)format && f.photo.approvalstatus != null &&
                               f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                               f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                               select f).FirstOrDefault();

            try
            {
                //new code to only get the gallery conversion copy
               // return GalleryPhoto.conversions
               //.Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                return GalleryPhoto.image;
            }
            catch
            {

            }
            return null;

        }

        public byte[] getgalleryimagebynormalizedscreenname(string strScreenName, photoformatEnum format)
        {
            //IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
            //Dim ctx As New Entities()
            //

            // string strProfileID = this.getprofileidbyscreenname(strScreenName);
          var  GalleryPhoto = (from p in _datingcontext.profiles.Where(p => p.screenname.Replace(" ", "") == strScreenName)
                            join f in _datingcontext.photoconversions  on p.id equals f.photo.profile_id
                               where (f.formattype.id  == (int)format && f.photo.approvalstatus != null &&
                               f.photo.approvalstatus.id == (int)photoapprovalstatusEnum.Approved &&
                               f.photo.imagetype.id == (int)photostatusEnum.Gallery)
                            select f).FirstOrDefault();


             try
            {
                //new code to only get the gallery conversion copy
                //return GalleryPhoto.conversions
               //.Where(p => p.photo_id == GalleryPhoto.id && p.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image;
                return GalleryPhoto.image;

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
                p.approvalstatus != null &&   p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved && p.imagetype.id == (int)photostatusEnum.Gallery).FirstOrDefault(); 

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
