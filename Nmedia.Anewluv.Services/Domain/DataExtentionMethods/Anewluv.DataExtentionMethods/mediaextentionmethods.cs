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

        public static photo getphotobyphotoid(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Query(o => o.id == model.photoid.GetValueOrDefault()).Select().FirstOrDefault();
                                    
         
        }


        public static PhotoModel getgalleryphotomodelbyprofileid(this IRepository<photoconversion> repo,int profileid, int photoformatid)
        {
           
              
                try
                {
                    //var convertedprofileid = Convert.ToInt32(profileid);
                   // var converedtedphotoformat = Convert.ToInt16(format);
                    //var format = 

                    return (from p in
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
                            select new PhotoModel
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
        public static PhotoSearchResultsViewModel getpagedphotomodelbyprofileidandstatusandalbumid (this IRepository<photoconversion> repo, int profileid, int? format, int? status, int? securitylevel, int? albumid, int? page, int? numberperpage)
        {
        
            try
            {
                //added roles
                IQueryable<photoconversion> photomodel = repo.Query(z=>z.photo.profile_id == profileid)
                    .Include(p=>p.photo.profilemetadata).Include(p=>p.photo.photo_securitylevel).Include(p=>p.photo.profilemetadata.profile.membersinroles.Select(z=>z.profile_id == profileid))
                    .Select().AsQueryable();

                if (securitylevel  !=null)    //if phop
                       photomodel = photomodel.Where(a => a.photo.photo_securitylevel!=null && a.photo.photo_securitylevel.Any(d =>  d.securityleveltype_id == (int)securitylevel));
                if (securitylevel  ==null) //only grab photos with no status 
                       photomodel = photomodel.Where(a => a.photo.photo_securitylevel!=null && a.photo.photo_securitylevel.Any(d => (d.securityleveltype_id ==  (int)securityleveltypeEnum.Public) | (d.securityleveltype_id ==  (int)securityleveltypeEnum.Public)));
                if (status != null)
                       photomodel = photomodel.Where(a =>  a.photo.photostatus_id == status );
                if (status ==null) //grab all photos with good status
                       photomodel = photomodel.Where(a =>   a.photo.photostatus_id != null &&
                                     (a.photo.photostatus_id == (int)photostatusEnum.Gallery |
                                      a.photo.lu_photostatus.id == (int)photostatusEnum.NotSet |
                                       a.photo.lu_photostatus.id == (int)photostatusEnum.Nostatus));
                if (format != null)
                    photomodel = photomodel.Where(a => a.photo.photoconversions.Any(z=>z.formattype_id == format.Value ));
                //if no photo status passed get call (premmium members only 
                //TO DO
                // if (format == null)
                //     photomodel = photomodel.Where(a =>    a.photo.photoconversions.Any(z => z.formattype_id == format.Value));
                //   return pagephotos(photomodel.ToList(), page, numberperpage);


            }
             catch (Exception ex)
            {
//eath the eception
               // throw ex;
            }

            return null;
        }

        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static List<PhotoModel> getpagedphotomodelbyprofileid(this IRepository<photoconversion> repo, int profileid, int format, int page, int pagesize)
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
                             select new PhotoModel
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
        public static PhotoViewModel getphotoviewmodel(IEnumerable<PhotoModel> Approved,
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
                       select new PhotoModel
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



      

        public static PhotoEditViewModel getphotoeditviewmodel(IEnumerable<PhotoModel> Approved,
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
                           approved = (p.photo.photostatus_id != null && p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
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
                       select new PhotoModel
                       {
                           photoid = p.photo.id,
                           profileid = p.photo.profile_id,
                           screenname = p.photo.profilemetadata.profile.screenname,
                           approved = (p.photo.photostatus_id != null && p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                           imagecaption = p.photo.imagecaption,
                           creationdate = p.photo.creationdate,
                           photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                           checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
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



        //format should be known by down
        public static PhotoSearchResultsViewModel filterandpagephotosbystatus(List<photoconversion> MyPhotos, photoapprovalstatusEnum approvalstatus,
                                                               int? page, int? numberperpage)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.approvalstatus_id != null && a.photo.approvalstatus_id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var photomodel = (from p in photos
                         select new PhotoModel
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
            bool allowpaging = (photomodel.Count() >= (page * numberperpage) ? true : false);
            var pageData = page > 1 & allowpaging ?
                new PaginatedList<PhotoModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
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
                         select new PhotoModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.photostatus_id != null && p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.lu_photoformat.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                         });

            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (photomodel.Count() >= (page * numberperpage) ? true : false);
            var pageData = page > 1 & allowpaging ?
                new PaginatedList<PhotoModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
            return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };

        }

        public static PhotoSearchResultsViewModel filterandpagephotosbysecuitylevel(List<photoconversion> MyPhotos, securityleveltypeEnum status,
                                                                        int? page, int? numberperpage)
        {
            var photomodel = (from p in MyPhotos.Where(a => a.photo.photo_securitylevel.Any(d => d.securityleveltype_id == (int)status))
                         select new PhotoModel
                         {
                             photoid = p.photo.id,
                             profileid = p.photo.profile_id,
                             screenname = p.photo.profilemetadata.profile.screenname,
                             approved = (p.photo.photostatus_id != null && p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                             profileimagetype = p.lu_photoformat.description,
                             imagecaption = p.photo.imagecaption,
                             creationdate = p.photo.creationdate,
                             photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                             checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                         });


            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (photomodel.Count() >= (page * numberperpage) ? true : false);
            var pageData = page > 1 & allowpaging ?
                new PaginatedList<PhotoModel>().GetCurrentPages(photomodel.ToList(), page ?? 1, numberperpage ?? 20) : photomodel.ToList().Take(numberperpage.Value);
            return new PhotoSearchResultsViewModel { results = pageData.ToList(), totalresults = pageData.Count() };

        }

        public static PhotoSearchResultsViewModel pagephotos(List<photoconversion> source,
                                                                   int? page, int? numberperpage)
        {

            
           // int? totalrecordcount = MemberSearchViewmodels.Count;
            //handle zero and null paging values
            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (source.Count() >= (page * numberperpage) ? true : false);
            var pageData = page > 1 & allowpaging ?
                new PaginatedList<photoconversion>().GetCurrentPages(source.ToList(), page ?? 1, numberperpage ?? 20) : source.Take(numberperpage.GetValueOrDefault());


            var results = pageData.Select(p => new PhotoModel                             
                              {
                                  photoid = p.photo.id,
                                  profileid = p.photo.profile_id,
                                  screenname = p.photo.profilemetadata.profile.screenname,
                                  approved = (p.photo.photostatus_id != null && p.photo.approvalstatus_id == (int)photoapprovalstatusEnum.Approved) ? true : false,
                                  profileimagetype = p.lu_photoformat.description,
                                  imagecaption = p.photo.imagecaption,
                                  orginalsize = p.photo.size,
                                  creationdate = p.photo.creationdate,
                                  photostatusid = p.photo.photostatus_id.GetValueOrDefault(),
                                  checkedprimary = (p.photo.photostatus_id == (int)photostatusEnum.Gallery)
                              }).ToList();

            return new PhotoSearchResultsViewModel { results = results, totalresults = source.Count() };

        }

    }
}
