using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;
using Nmedia.DataAccess.Interfaces;
using Anewluv.Domain.Data.ViewModels;
using Shell.MVC2.Infrastructure.Helpers;

namespace Anewluv.DataExtentionMethods
{
    public static class mediaextentionmethods
    {

        public static List<photo> getallphotosbyusername(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Find().OfType<photo>().Where(o => o.profilemetadata.profile.username == model.username
                                    && o.lu_photoapprovalstatus.id != (int)photoapprovalstatusEnum.Rejected && o.lu_photoapprovalstatus.id
                                    != (int)photoapprovalstatusEnum.RequiresFurtherInformation).ToList();

        }

        public static photo getphotobyphotoid(this IRepository<photo> repo, ProfileModel model)
        {
            return repo.Find().OfType<photo>().Single(o => o.id == model.photoid.GetValueOrDefault());
                                    

            //If above does not work
        //   _gender = (from x in (_datingcontext.photos.Where(f => f.id == model.photoid))
          //             join f in _datingcontext.profiledata on x.profile_id equals f.profile_id
           //            select f.gender).FirstOrDefault();

        }


        public static PhotoModel getgalleryphotomodelbyprofileid(this IRepository<photoconversion> repo,string profileid, string format)
        {

           
              
                try
                {
                    var convertedprofileid = Convert.ToInt32(profileid);
                    var converedtedphotoformat = Convert.ToInt16(format);
                    //var format = 

                  return  (from p in
                               (from r in repo.Find().Where(a => a.lu_photoformat.id ==converedtedphotoformat && (a.photo.profile_id == convertedprofileid & a.photo.lu_photostatus.id == (int)photostatusEnum.Gallery)).ToList()
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


        public static List<PhotoModel> getpagedphotomodelbyprofileidandstatus(this IRepository<photoconversion> repo, string profileid, string status, string format, string page, string pagesize)
        {
        
            try
            {
             var model = (from p in repo.Find().Where(a => a.formattype_id ==  Convert.ToInt16(format) && a.photo.profile_id ==  Convert.ToInt16(profileid) && a.photo.profile_id ==  Convert.ToInt32(profileid) && (
                                                     a.photo.photostatus_id  != null && a.photo.photostatus_id == Convert.ToInt16(status))).ToList()
                                 select new PhotoModel
                                 {
                                     photoid = p.photo.id,
                                     profileid = p.photo.profile_id,
                                     screenname = p.photo.profilemetadata.profile.screenname,
                                     photo = b64Converters.ByteArraytob64string(p.image),
                                     photoformat = p.lu_photoformat,
                                     convertedsize = p.size,
                                     orginalsize = p.photo.size,
                                     imagecaption = p.photo.imagecaption,
                                     creationdate = p.photo.creationdate,
                                 });

                    if (model.Count() > Convert.ToInt32(pagesize)) { pagesize = model.Count().ToString(); }

                    return (model.OrderByDescending(u => u.creationdate).Skip((Convert.ToInt16(page) - 1) * Convert.ToInt16(pagesize)).Take(Convert.ToInt16(pagesize))).ToList();
            }
             catch (Exception ex)
            {
            
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
            var photos = MyPhotos.Where(a => a.photo.approvalstatus_id != null && a.photo.approvalstatus_id == (int)approvalstatus);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //  if (approvalstatus == "Yes") { photos = photos.Where(a => a.photostatus.id  != 3); }

            var model = (from p in photos
                         select new photoeditmodel
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
                       select new photoeditmodel
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


        //Filter methods for edit photo models
        public static IEnumerable<photoeditmodel> filterphotosapprovedminusgallery(IQueryable<photoconversion> MyPhotos,
                                                        photoapprovalstatusEnum status,
                                                            int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.photo.photostatus_id == (int)status);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            //TO DO modifiy this code to filter out the private photos and photos that are part of private albums 
            if (status == photoapprovalstatusEnum.Approved)
            {
                photos = photos.Where(a => a.photo.imagetype_id != (int)photostatusEnum.Gallery);
            }

            var model = (from p in photos
                         select new photoeditmodel
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


            if (model.Count() > pagesize) { pagesize = model.Count(); }

            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize));

        }

        public static IEnumerable<photoeditmodel> filterphotosbysecuitylevel(List<photoconversion> MyPhotos, securityleveltypeEnum status,
                                                                        int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.photo.photo_securitylevel.Any(d => d.securityleveltype_id == (int)status))
                         select new photoeditmodel
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


            if (model.Count() > pagesize) { pagesize = model.Count(); }
            return (model.OrderByDescending(u => u.creationdate).Skip((page - 1) * pagesize).Take(pagesize));

        }
     


    }
}
