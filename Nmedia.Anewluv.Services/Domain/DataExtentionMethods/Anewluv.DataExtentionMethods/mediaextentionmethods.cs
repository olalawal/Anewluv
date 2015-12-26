using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;
//using Nmedia.DataAccess.Interfaces;
using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Helpers;
using Repository.Pattern.Repositories;
using Nmedia.Infrastructure;


namespace Anewluv.DataExtentionMethods
{
    public static class mediaextentionmethods
    {

        public static List<photo> getallphotosbyusername(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Query(o => o.profilemetadata.profile.username == model.username
                                    && o.lu_photoapprovalstatus.id != (int)photoapprovalstatusEnum.Rejected && o.lu_photoapprovalstatus.id
                                    != (int)photoapprovalstatusEnum.RequiresFurtherInformation).Select().ToList();

        }


        public static int getapprovedphotocountbyprofileid(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Query(o => o.profilemetadata.profile.id == model.profileid
                                    && o.lu_photoapprovalstatus.id == (int)photoapprovalstatusEnum.Approved).Select().Count();

        }

        public static PhotosSortedCountsViewModel getphotossortedcountsbyprofileid(this IRepository<photo> repo, PhotoModel model)
        {

            var returnmodel = new PhotosSortedCountsViewModel();
            try
            {
                IQueryable<photo> photomodel = repo.Query(z => z.profile_id == model.profileid.Value &
                        (z.approvalstatus_id != (int)photoapprovalstatusEnum.Rejected) &  //filter not approved
                        (z.photostatus_id != (int)photostatusEnum.deletedbyadmin | z.photostatus_id != (int)photostatusEnum.deletedbyuser))  //filter deleted                   
                        .Include(p => p.profilemetadata)
                        .Include(p => p.photo_securitylevel.Select(z => z.lu_securityleveltype))
                        .Include(p => p.profilemetadata.profile.membersinroles.Select(z => z.lu_role))
                        .Select().AsQueryable();
                



                //public photos
                returnmodel.PublicPhotoCount = photomodel.Where(a => a.photostatus_id == (int)photoapprovalstatusEnum.Approved && a.photo_securitylevel.Count() == 0).Count();
                //not reviewed
                returnmodel.NotApprovedPhotoCount = photomodel.Where(a => a.photostatus_id == (int)photoapprovalstatusEnum.NotReviewed && a.photo_securitylevel.Count() == 0).Count();
                //rejected
                returnmodel.RejectedPhotoCount = photomodel.Where(a => a.photostatus_id == (int)photoapprovalstatusEnum.Rejected && a.photo_securitylevel.Count() == 0).Count();
                //interests only view
                returnmodel.PeeksOnlyPhotoCount = photomodel.Where(a => a.photo_securitylevel.Count() > 0 && a.photo_securitylevel.Any(d => d.securityleveltype_id == (int)securityleveltypeEnum.Peeks)).Count();
                //likes only view
                returnmodel.InterestsOnlyPhotoCount = photomodel.Where(a => a.photo_securitylevel.Count() > 0 && a.photo_securitylevel.Any(d => d.securityleveltype_id == (int)securityleveltypeEnum.Intrests)).Count();
                //likes only view
                returnmodel.LikesOnlyPhotoCount = photomodel.Where(a => a.photo_securitylevel.Count() > 0 && a.photo_securitylevel.Any(d => d.securityleveltype_id == (int)securityleveltypeEnum.Likes)).Count();
                //private 
                returnmodel.PrivatePhotoCount = photomodel.Where(a => a.photo_securitylevel.Count() > 0 && a.photo_securitylevel.Any(d => d.securityleveltype_id == (int)securityleveltypeEnum.NoOne)).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnmodel;
        }


        public static photo getphotobyphotoid(this IRepository<photo> repo, PhotoModel model)
        {
            return repo.Query(o => o.id == model.photoid.GetValueOrDefault()).Select().FirstOrDefault();
                                    
         
        }


        public static PhotoViewModel getgalleryphotomodelbyprofileid(this IRepository<photoconversion> repo,int profileid, int photoformatid)
        {
           
              
                try
                {
                    //var convertedprofileid = Convert.ToInt32(profileid);
                   // var converedtedphotoformat = Convert.ToInt16(format);
                    //var format = 

                    var dd = (from p in
                                (from r in repo.Query(a => a.lu_photoformat.id == photoformatid && (a.photo.profile_id == profileid & a.photo.lu_photostatus.id == (int)photostatusEnum.Gallery))
                                 select new
                                 {
                                     photoid = r.photo.id,
                                     profileid = r.photo.profile_id,
                                     screenname = r.photo.profilemetadata.profile.screenname,
                                     photo = r.image,
                                     photoformat = r.lu_photoformat,
                                     convertedsize = r.size,
                                     orginalsize = r.photo.size,
                                     imagecaption = r.photo.imagecaption,
                                     creationdate = r.photo.creationdate,
                                     photodetail = r.photo

                                 }).ToList()
                            select new PhotoViewModel
                            {
                                photoid = p.photoid,
                                profileid = p.profileid,
                                screenname = p.screenname,
                                photo = b64Converters.ByteArraytob64string(p.photo),
                                photoformat = p.photoformat,
                                convertedsize = p.convertedsize,
                                orginalsize = p.orginalsize,
                                imagecaption = p.imagecaption,
                                creationdate = p.creationdate,
                                photostatusid = p.photodetail.photostatus_id.GetValueOrDefault()

                            }).FirstOrDefault();

                    return dd;
                    // model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

                    //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



                    //return (model);
                }
                catch (Exception ex)
                {
            
                throw ex;
                
                }
            

        }



        

       

        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static IQueryable<photoconversion> filterphotos(this IRepository<photoconversion> repo, PhotoModel model)
        {
            try
            {

                //added roles
                //filterer out photos that are not approved
                IQueryable<photoconversion> photomodel = repo.Query(z => z.photo.profile_id == model.profileid.Value &
                   // (z.photo.approvalstatus_id != (int)photoapprovalstatusEnum.Rejected) &  //filter not approved
                    (z.photo.photostatus_id != (int)photostatusEnum.deletedbyadmin | z.photo.photostatus_id != (int)photostatusEnum.deletedbyuser))  //filter deleted 
                     .Include(p=>p.photo)
                     .Include(p => p.lu_photoformat)
                    .Include(p => p.photo.profilemetadata)
                    .Include(p => p.photo.photo_securitylevel.Select(z => z.lu_securityleveltype))
                    .Include(p => p.photo.profilemetadata.profile.membersinroles.Select(z => z.lu_role))
                    .Select().AsQueryable();

              
                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //photo id
                if (model.photoid != null)
                    photomodel = photomodel.Where(a => a.photo_id == model.photoid).AsQueryable();
                               
                //also assumes the viewer is the same as the profile ID
                //only allow the user who owns them get photos by security level   
                if (model.photosecuritylevelid != null)
                    photomodel = photomodel.Where(a => a.photo.photo_securitylevel.Count() > 0 && a.photo.photo_securitylevel.Any(d => d.securityleveltype_id == model.photosecuritylevelid));               
                if (model.photosecuritylevelid == null) //only grab photos with no status 
                    photomodel = photomodel.Where(a => a.photo.photo_securitylevel.Count() == 0).AsQueryable();             
               
                //status
                if (model.phototstatusid != null)
                    photomodel = photomodel.Where(a => a.photo.photostatus_id == model.phototstatusid);                
               

                //format
                if (model.photoformatid != null)
                    photomodel = photomodel.Where(a => a.formattype_id == model.photoformatid.Value);
                if (model.photoformatid == null) //default is medium quality
                    photomodel = photomodel.Where(a => a.formattype_id ==  (int)photoformatEnum.Medium);

                //only allow admins or the user to view un reviewed photos 
                if (model.phototapprovalstatusid != null )
                     photomodel = photomodel.Where(z => z.photo.approvalstatus_id == model.phototapprovalstatusid.Value);     
                //if no status is requested just show all that are reviewed and approved
                if (model.phototapprovalstatusid == null)
                    photomodel = photomodel.Where(z => z.photo.approvalstatus_id != (int)photoapprovalstatusEnum.NotReviewed |
                        z.photo.approvalstatus_id != (int)photoapprovalstatusEnum.Rejected |
                        z.photo.approvalstatus_id != (int)photoapprovalstatusEnum.RequiresFurtherInformation);
             

               


                return photomodel;



            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }


        //THis function is for list other users photos
        //TO DO in another function for viewer others photos we will need to grab the actions of the current user to the photo profileid (i.e viewer profileid) 
        //if user is in correect relation ship to the photo profileid then allow the photo to be downloaded.
        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static IQueryable<photoconversion> filterothersphotos(this IRepository<photoconversion> repo, PhotoModel model)
        {

            try
            {
                //added roles
                // only approved photos are allowed
                IQueryable<photoconversion> photomodel = repo.Query(z => z.photo.profile_id == model.viewingprofileid &
                    (z.photo.approvalstatus_id  == (int)photoapprovalstatusEnum.Approved ) &  //filter not approved
                    (z.photo.photostatus_id != (int)photostatusEnum.deletedbyadmin | z.photo.photostatus_id != (int)photostatusEnum.deletedbyuser))  //filter deleted 
                     .Include(p => p.lu_photoformat)
                    .Include(p => p.photo.profilemetadata)
                    .Include(p => p.photo.photo_securitylevel.Select(z => z.lu_securityleveltype))
                    .Include(p => p.photo.profilemetadata.profile.membersinroles.Select(z => z.lu_role))
                    .Select().AsQueryable();



                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //photo id
                if (model.photoid != null)
                    photomodel = photomodel.Where(a => a.photo_id == model.photoid).AsQueryable();

                //TO DO
                //security level check if the viewer has access here 
              //  if (model.photosecuritylevelid != null)    //if phop
              //      photomodel = photomodel.Where(a => a.photo.photo_securitylevel.Count() > 0 && a.photo.photo_securitylevel.Any(d => d.securityleveltype_id == model.photosecuritylevelid));
              //  if (model.photosecuritylevelid == null) //only grab photos with no status 
                    photomodel = photomodel.Where(a => a.photo.photo_securitylevel.Count() == 0).AsQueryable();



                //status
                //if (model.phototstatusid != null)
                //    photomodel = photomodel.Where(a => a.photo.photostatus_id == model.phototstatusid);



                //format
                if (model.photoformatid != null)
                    photomodel = photomodel.Where(a => a.formattype_id == model.photoformatid.Value);
                if (model.photoformatid == null) //default is medium quality
                    photomodel = photomodel.Where(a => a.formattype_id == (int)photoformatEnum.Medium);

               





                return photomodel;



            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static PhotoSearchResultsViewModel getfilteredphotospaged(this IRepository<photoconversion> repo, PhotoModel model)
        {
        
            try
            {
                var dd = filterphotos(repo, model);
                //TO DO test the ordering we want the order by date and then the gallery photo first
                 return pagephotos(dd.OrderBy(z=>z.creationdate).OrderBy(z=>z.photo.photostatus_id).ToList(), model.page, model.numberperpage);


            }
             catch (Exception ex)
            {
//eath the eception
               // throw ex;
            }

            return null;
        }


       //filtering for other memebers photos i.e profile that is not your own
        public static PhotoSearchResultsViewModel getothersfilteredphotospaged(this IRepository<photoconversion> repo, PhotoModel model)
        {

            try
            {
                var dd = filterothersphotos(repo, model);
                //TO DO test the ordering we want the order by date and then the gallery photo first
                return pagephotos(dd.OrderBy(z => z.creationdate).OrderBy(z => z.photo.photostatus_id).ToList(), model.page, model.numberperpage);


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static PhotoViewModel getfilteredphoto(this IRepository<photoconversion> repo, PhotoModel model)
        {

            try
            {

                var results = filterphotos(repo, model).Select(p => new PhotoViewModel
                {
                    photoid = p.photo.id,
                    profileid = p.photo.profile_id,
                    screenname = p.photo.profilemetadata.profile.screenname,
                   approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                    profileimagetype = p.lu_photoformat.description,
                    imagecaption = p.photo.imagecaption,
                    orginalsize = p.photo.size,
                    creationdate = p.photo.creationdate,
                    photostatusid = p.photo.photostatus_id.Value,
                    checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                }).FirstOrDefault();

                return results;
               


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }


        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static List<PhotoViewModel> getpagedphotomodelbyprofileid(this IRepository<photoconversion> repo, int profileid, int format, int page, int pagesize)
        {

            try
            {

                var model = (from p in
                                 (from r in repo.Query(a => a.lu_photoformat.id == format && (a.photo.profile_id == profileid &
                                     a.photo.photostatus_id != null &&
                                     (a.photo.photostatus_id == (int)photostatusEnum.Gallery |
                                      a.photo.lu_photostatus.id == (int)photostatusEnum.NotSet |
                                       a.photo.lu_photostatus.id == (int)photostatusEnum.Nostatus)))
                                  select new
                                  {
                                      photoid = r.photo.id,
                                      profileid = r.photo.profile_id,
                                      screenname = r.photo.profilemetadata.profile.screenname,
                                      photo = r.image,
                                      photoformat = r.lu_photoformat,
                                      convertedsize = r.size,
                                      orginalsize = r.photo.size,
                                      imagecaption = r.photo.imagecaption,
                                      creationdate = r.photo.creationdate,
                                      photodetail = r.photo

                                  }).ToList()
                             select new PhotoViewModel
                             {
                                 photoid = p.photoid,
                                 profileid = p.profileid,
                                 screenname = p.screenname,
                                 photo = b64Converters.ByteArraytob64string(p.photo),
                                 photoformat = p.photoformat,
                                 convertedsize = p.convertedsize,
                                 orginalsize = p.orginalsize,
                                 imagecaption = p.imagecaption,
                                 creationdate = p.creationdate,
                                 photostatusid = p.photodetail.photostatus_id.GetValueOrDefault()

                             }).ToList();


               // if (page == null || page == 0) page = 1;
               // if (numberperpage == null || numberperpage == 0) numberperpage = 4;
             //   bool allowpaging = (photomodel.Count() >= (page * numberperpage) ? true : false);
            //    var pageData = page > 1 & allowpaging ?
            //        new PaginatedList<PhotoModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
          //      return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }


        //Private filtering methods :
        public static PhotoEditViewModel getphotoviewmodel(IEnumerable<PhotoViewModel> Approved,
                                                           IEnumerable<PhotoViewModel> NotApproved,
                                                           IEnumerable<PhotoViewModel> Private,
                                                           List<photoconversion> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoViewModel src = new PhotoViewModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.photo.id equals x.photoid
                       select new PhotoViewModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           photoformat = p.lu_photoformat,
                           convertedsize = p.size,
                           //   approved = (p.approvalstatus != null && p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           //   profileimagetype = p.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus_id.GetValueOrDefault()
                           //   checkedprimary = (p.photostatus.id == (int)photostatusEnum.Gallery)
                       })
                             .OrderBy(o => o.creationdate)
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new PhotoViewModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           //   approved = (p.approvalstatus != null && p.approvalstatus.id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           //  profileimagetype = p.imagetype.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus_id.GetValueOrDefault()
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

            // PhotosUploadModel UploadPhotos = new PhotosUploadModel();

            PhotoEditViewModel ViewModel = new PhotoEditViewModel
            {
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),

            };


            return (ViewModel);


        }


        public static PhotoEditViewModel getphotoeditviewmodel(IEnumerable<PhotoViewModel> Approved,
                                                            IEnumerable<PhotoViewModel> NotApproved,
                                                            IEnumerable<PhotoViewModel> Private,
                                                            List<photoconversion> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoViewModel src = new PhotoViewModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.photo.id equals x.photoid
                       select new PhotoViewModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                          approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                           profileimagetype = p.lu_photoformat.description,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                           checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                       })
                             .OrderBy(o => o.profileimagetype).ThenByDescending(o => o.creationdate)
                             .FirstOrDefault();
            }
            else
            {
                src = (from p in model
                       select new PhotoViewModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                          approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                           checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                       })
                               .OrderByDescending(o => o.creationdate)
                               .FirstOrDefault();
            }


          //  PhotosUploadModel UploadPhotos = new PhotosUploadModel();

            PhotoEditViewModel ViewModel = new PhotoEditViewModel
            {
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),
              //  PhotosUploading = UploadPhotos
            };


            return (ViewModel);


        }


        //format should be known by down
        public static PhotoSearchResultsViewModel filterandpagephotosbystatus(List<photoconversion> MyPhotos, photoapprovalstatusEnum approvalstatus,
                                                               int? page, int? numberperpage)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.approvalstatus_id != null && a.photo.approvalstatus_id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var photomodel = (from p in photos
                         select new PhotoViewModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.lu_photoformat.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                         });


            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (photomodel.Count() > numberperpage ? true : false);
            var pageData = page >= 1 & allowpaging ?
                new PaginatedList<PhotoViewModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
            return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };



        }

        //Filter methods for edit photo models
        public static PhotoSearchResultsViewModel filterandpagephotosapprovedminusgallery(IQueryable<photoconversion> MyPhotos,
                                                        photoapprovalstatusEnum status,
                                                            int? page, int? numberperpage)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.photostatus_id == (int)status);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //TO DO modifiy this code to filter out the private photos and photos that are part of private albums 
            if (status == photoapprovalstatusEnum.Approved)
            {
                photos = photos.Where(a => a.photo.imagetype_id != (int)photostatusEnum.Gallery);
            }

            var photomodel = (from p in photos
                         select new PhotoViewModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                            approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                             profileimagetype = p.lu_photoformat.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                         });

            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (photomodel.Count() > numberperpage ? true : false);
            var pageData = page >= 1 & allowpaging ?
                new PaginatedList<PhotoViewModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
            return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };

        }

        public static PhotoSearchResultsViewModel filterandpagephotosbysecuitylevel(List<photoconversion> MyPhotos, securityleveltypeEnum status,
                                                                        int? page, int? numberperpage)
        {
            var photomodel = (from p in MyPhotos.Where(a => a.photo.photo_securitylevel.Any(d => d.securityleveltype_id == (int)status))
                         select new PhotoViewModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                            approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                             profileimagetype = p.lu_photoformat.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                         });


            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (photomodel.Count() >  numberperpage ? true : false);
            var pageData = page >= 1 & allowpaging ?
                new PaginatedList<PhotoViewModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
            return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };

        }

        public static PhotoSearchResultsViewModel pagephotos(List<photoconversion> source,
                                                                   int? page, int? numberperpage)
        {

            
           // int? totalrecordcount = MemberSearchViewmodels.Count;
            //handle zero and null paging values
            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (source.Count() >=  numberperpage ? true : false);
            var pageData = page >= 1 & allowpaging ?
                new PaginatedList<photoconversion>().GetCurrentPages(source.ToList(), page ?? 1, numberperpage ?? 20) : source.Take(numberperpage.GetValueOrDefault());


            var results = pageData.Select(p => new PhotoViewModel                             
                              {
                                  photoid = p.photo.id,
                                  photo = b64Converters.ByteArraytob64string(p.image),
                                  profileid = p.photo.profile_id,
                                  screenname = p.photo.profilemetadata.profile.screenname,
                                  approved = p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved ? true : false,
                                  profileimagetype = p.lu_photoformat.description,
                                  imagecaption = p.photo.imagecaption,
                                  orginalsize = p.photo.size,
                                  creationdate = p.photo.creationdate,
                                  photostatusid = p.photo.photostatus_id.Value,
                                  checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                              }).ToList();

            return new PhotoSearchResultsViewModel { results = results, totalresults = source.Count() };

        }

    }
}
