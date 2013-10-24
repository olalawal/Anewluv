using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;
using Anewluv.DataAccess.Interfaces;
using Anewluv.Domain.Data.ViewModels;

namespace Anewluv.DataExtentionMethods
{
    public static class mediaextentionmethods
    {

        public static List<photo> getallphotosbyusername(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Find().OfType<photo>().Where(o => o.profilemetadata.profile.username == model.username 
                                    && o.photostatus.id != 4 && o.photostatus.id != 5).ToList();

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
                           photoformat = p.formattype,
                           convertedsize = p.size,
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



        //format should be known by down
        public static List<photoeditmodel> filterandpagephotosbystatus(List<photoconversion> MyPhotos, photoapprovalstatusEnum approvalstatus,
                                                               int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.approvalstatus != null && a.photo.approvalstatus.id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in photos
                         select new photoeditmodel
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

        public static PhotoEditViewModel getphotoeditviewmodel(IEnumerable<photoeditmodel> Approved,
                                                            IEnumerable<photoeditmodel> NotApproved,
                                                            IEnumerable<photoeditmodel> Private,
                                                            List<photoconversion> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            photoeditmodel src = new photoeditmodel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                       join x in Approved
                       on p.photo.id equals x.photoid
                       select new photoeditmodel
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
                       select new photoeditmodel
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


        //Filter methods for edit photo models
        public static IEnumerable<photoeditmodel> filterphotosapprovedminusgallery(IQueryable<photoconversion> MyPhotos,
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
                         select new photoeditmodel
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

        public static IEnumerable<photoeditmodel> filterphotosbysecuitylevel(List<photoconversion> MyPhotos, securityleveltypeEnum status,
                                                                        int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.photo.photosecuritylevels.Any(d => d.securityleveltype.id == (int)status))
                         select new photoeditmodel
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
     


    }
}
