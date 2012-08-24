using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using System.ComponentModel.DataAnnotations;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;


using System.Security.Principal;

using System.Web.Routing;
using System.Web.Security;
using System.IO;


using Shell.MVC2.Models;
using Omu.Awesome.Core;

using System.Data;
using System.Data.Entity;
using ActionMailer.Net.Mvc;
using ActionMailer.Net;



using Shell.MVC2.AppFabric;

using Shell.MVC2.Filters;

namespace Shell.MVC2.Controllers
{
    public class AdminController : Controller
    {
        private AnewLuvFTSEntities db ;
        private AdminPhotoModel adminPhoto;

        private readonly IList<Hobby> dataHobby = new List<Hobby>();

      
           //TO DO convert the other services to do this as well
        public AdminController(AnewLuvFTSEntities _db, AdminPhotoModel _adminPhoto)
       {
           db = _db;
           adminPhoto= _adminPhoto; 
       }


        //
        // GET: /Admin/

        //  [Authorize(Roles = "admin")]
        
        [GetChatUsersData]
        [Authorize(Users = "Shola, dar, case")]
        public ActionResult Home(int? page)
        {

            const int pageSize = 10;
            var pageIndex = page ?? 1;
            return View(new Pageable<Hobby>
            {
                PageIndex = pageIndex,
                PageCount = GetPageCount(pageSize, dataHobby.Count),
                Page = dataHobby.Skip(--pageIndex * pageSize).Take(pageSize)
            });
        }

        static int GetPageCount(int pageSize, int count)
        {
            var pages = count / pageSize;
            if (count % pageSize > 0) pages++;
            return pages;
        }

          [GetChatUsersData]
        [Authorize(Users = "Shola, dar, case")]
        public ActionResult AdminHome()
        {
            var ActiveProfiles = new AdminViewModel();

            ActiveProfiles.UserProfileDatas = db.ProfileDatas.ToList();
            return View(ActiveProfiles);
        }

        [Authorize(Users = "Shola, dar, case")]
        public ActionResult Admin21()
        {
            return View(db.photos.ToList());
        }

        [Authorize(Users = "Shola, dar, case")]
        public ActionResult Admin()
        {
            return View();
        }
        [Authorize(Users = "Shola, dar, case")]
        [GetChatUsersData]
        public ActionResult AdminPhotos(string approve)
        {

            //Yes/No

            if (approve == "Yes")
            {
                // Go to partial view
            }

            var Query = db.photos
                .Where(p => p.Aproved == approve).OrderBy(p => p.PhotoDate).ToList();
            return View(Query);
        }

        //// 
        //// GET: /Admin/PhotEdit/5 
        //public ActionResult PhotoEdit(Guid id)
        //{

        //    AdminPhotoModel Photo = (from p in db.photos.Where(p => p.PhotoID == id)
        //                            select new AdminPhotoModel
        //                            {  
        //                                PhotoID  = p.PhotoID,
        //                                ProfileID = p.ProfileID,
        //                                Aproved = p.Aproved,
        //                                ProfileImageType = p.ProfileImageType,
        //                                PhotoReviewDate = p.PhotoReviewDate,
        //                                PhotoReviewerID = p.PhotoReviewerID    
        //                            }).Single();

        //    Photo.checkedAproved = Photo.BoolAproved(Photo.Aproved.ToString());

        //    //// can't get view bag to work.
        //    //var Approvedlist = new List<SelectListItem>();
        //    //Approvedlist.Add(new SelectListItem(){Text = "Yes", Value="Yes"});
        //    //Approvedlist.Add(new SelectListItem(){Text = "No", Value="No"});

        //    //ViewBag.Aproved = new SelectList(Approvedlist, "Aproved", "Name", Photo.Aproved);




        //    return View(Photo);
        //}

        //// 
        //// POST: /Admin/PhotEdit/5 
        //[Authorize(Users = "Shola, dar, case")]
        //[HttpPost]
        //public ActionResult PhotoEdit(AdminPhotoModel Photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Retrieve single value from photos table
        //        photo PhotoModify = db.photos.Single(p => p.PhotoID == Photo.PhotoID);

        //        // Update photo.Aproved value based on the value of the checkbox
        //        if (Photo.checkedAproved == true)
        //        { Photo.Aproved = "Yes";} 
        //        else { Photo.Aproved = "No"; }

        //#region "Verify Approved Gallery Exists"


        //        /// <summary>
        //        /// Determines whether an approved gallery image exists and changes the value of ProfileImageType accordingly.
        //        /// </summary>
        //        /// 
        //        //initialize access to the datating context / service
        //        var datingService = new DatingService().Initialize();

        //        //call to the data model to verify approved galleryExists
        //        bool galleryExists = datingService.CheckForGalleryPhotoByProfileID(Photo.ProfileID.ToString());


        //        if (galleryExists == false)
        //        {
        //            if (Photo.Aproved == "No") 
        //            { 
        //                Photo.ProfileImageType = "NoStatus";
        //            }
        //            else
        //            {
        //                Photo.ProfileImageType = "Gallery"; // Changes ProfileImageType from "Nostatus" to "Gallery" 
        //            }
        //        }
        //        else { Photo.ProfileImageType = "NoStatus"; } // Keeps ProfileImageType = "NoStatus"

        //#endregion

        //        // Update or attach values from AdminPhoto to PhotoModify model
        //        PhotoModify.Aproved = Photo.Aproved;
        //        PhotoModify.ProfileImageType = Photo.ProfileImageType; 
        //        PhotoModify.PhotoReviewDate = DateTime.Now;
        //        PhotoModify.PhotoReviewerID = Photo.PhotoReviewerID;  //Add user.name


        //       // db.photos.Attach(PhotoModify); already attached
        //       // Update database
        //        db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);                
        //        db.SaveChanges();

        //        ////// Attach Send email Here so the user knows that the photo has being approved.
        //        ///////

        //        return RedirectToAction("Admin");
        //    }

        //    return View(Photo);
        //}

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// /////
        /// </summary>
        /// <returns></returns>

        [Authorize(Users = "Shola, dar, case")]
        [ChildActionOnly]
        public ActionResult PhotoMenu()
        {
            // var menu = GetMenuFromSomewhere();
            return PartialView();
        }
        //
        // GET: /ShoppingCart/CartSummary

        //[ChildActionOnly]
        [Authorize(Users = "Shola, dar, case")]
        [HttpPost]
        public ActionResult PhotoList(int page, string PhotoView)
        {

            int pagesize = 20;

            //Assume  NotReviewed is Selected, that is PhotoView == "Not Reviewed"
            //if (PhotoView == "Not Reviewed")
            //{
            var Photos = db.photos
             .Where(p => p.Aproved == "No" && p.PhotoReviewDate == null);
            //}
            if (PhotoView == "Approved")
            {
                Photos = db.photos
               .Where(p => p.Aproved == "Yes"); //Display Gallery, Nostatus & Private, etc.
            }
            else if (PhotoView == "Disapproved")
            {
                Photos = db.photos
               .Where(p => p.Aproved == "No" && p.PhotoReviewDate != null);
            }


            //var model = Photos.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize)
            //    .OrderBy(u => u.PhotoDate).ToList();
            var model = (from p in Photos
                         select new AdminPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ProfileID = p.ProfileID,
                             Aproved = p.Aproved,
                             ProfileImageType = p.ProfileImageType,
                             PhotoDate = p.PhotoDate,
                             PhotoReviewDate = p.PhotoReviewDate,
                             PhotoReviewerID = p.PhotoReviewerID
                         }).OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize)
                            .OrderBy(u => u.ProfileID).ToList();

            //Display Items for viewing
            var Fromdate = Photos.OrderBy(o => o.PhotoDate).Select(p => p.PhotoDate).FirstOrDefault();
            var Todate = Photos.OrderByDescending(o => o.PhotoDate).Select(p => p.PhotoDate).FirstOrDefault();
            var ModelCount = Photos.Count();

            ViewData["Message"] = PhotoView;
            ViewData["ModelCount"] = ModelCount;
            ViewData["DateRange"] = Fromdate + " - " + Todate;
            ViewData["PageIndex"] = page;

            return PartialView("PhotoList", model);

        }
        [Authorize(Users = "Shola, dar, case")]
        [HttpGet]
        public ActionResult PhotoApprovalView(string photoid)
        {
            Guid pid = Guid.Parse(photoid);
            // Retrieve updated single photo profile by PhotoId
            AdminPhotoModel Photo = (from p in db.photos.Where(p => p.PhotoID == pid)
                                     select new AdminPhotoModel
                                     {
                                         PhotoID = p.PhotoID,
                                         ProfileID = p.ProfileID,
                                         Aproved = p.Aproved,
                                         ProfileImageType = p.ProfileImageType,
                                         PhotoDate = p.PhotoDate,
                                         PhotoReviewDate = p.PhotoReviewDate,
                                         PhotoReviewerID = p.PhotoReviewerID
                                     }).Single();

            Photo.checkedAproved = Photo.BoolAproved(Photo.Aproved.ToString());


            //var AprovedList = (from x in db.photos
            //                   select x.Aproved).Distinct();          

            List<SelectListItem> AprovedList = new List<SelectListItem>();
            AprovedList.Add(new SelectListItem { Text = "Approve", Value = "Yes" });
            AprovedList.Add(new SelectListItem { Text = "Disapprove", Value = "No" });
            Photo.AprovedList = new SelectList(AprovedList, "value", "text");

            var RejectionReasonList = (from x in db.PhotoRejectionReasons select x);
            Photo.PhotoRejectionReasonList = new SelectList(RejectionReasonList.ToList(), "PhotoRejectionReasonID", "Description");



            return PartialView("PhotoApprovalView", Photo);
        }

        [Authorize(Users = "Shola, dar, case")]
        [HttpGet]
        public ActionResult _PhotoEdit(Guid photoid)
        {
            // Retrieve updated single photo profile by PhotoId
            AdminPhotoModel Photo = (from p in db.photos.Where(p => p.PhotoID == photoid)
                                     select new AdminPhotoModel
                                     {
                                         PhotoID = p.PhotoID,
                                         ProfileID = p.ProfileID,
                                         Aproved = p.Aproved,
                                         ProfileImageType = p.ProfileImageType,
                                         PhotoDate = p.PhotoDate,
                                         PhotoReviewDate = p.PhotoReviewDate,
                                         PhotoReviewerID = p.PhotoReviewerID
                                     }).Single();

            Photo.checkedAproved = Photo.BoolAproved(Photo.Aproved.ToString());


            //var AprovedList = (from x in db.photos
            //                   select x.Aproved).Distinct();          

            List<SelectListItem> AprovedList = new List<SelectListItem>();
            AprovedList.Add(new SelectListItem { Text = "Approve", Value = "Yes" });
            AprovedList.Add(new SelectListItem { Text = "Disapprove", Value = "No" });
            Photo.AprovedList = new SelectList(AprovedList, "value", "text");

            var RejectionReasonList = (from x in db.PhotoRejectionReasons select x);
            Photo.PhotoRejectionReasonList = new SelectList(RejectionReasonList.ToList(), "PhotoRejectionReasonID", "Description");



            return View(Photo);
        }


        // 
        // POST: /Admin/PhotoApprovalView/5 
        [Authorize(Users = "Shola, dar, case")]
        [HttpPost]
        public ActionResult _PhotoEdit(AdminPhotoModel Photo)
        {
            // Display error message if photo is rejected and no rejection reason is given.
            if ((Photo.PhotoRejectionReasonID == 0) && (Photo.Aproved == "No"))
            {
                ModelState.Clear();
                ModelState.AddModelError("PhotoRejectionReasonID", "Please select a rejection reason to continue.");


                List<SelectListItem> AprovedList = new List<SelectListItem>();
                AprovedList.Add(new SelectListItem { Text = "Approve", Value = "Yes" });
                AprovedList.Add(new SelectListItem { Text = "Disapprove", Value = "No" });
                Photo.AprovedList = new SelectList(AprovedList, "value", "text");

                var RejectionReasonList = (from x in db.PhotoRejectionReasons select x);
                Photo.PhotoRejectionReasonList = new SelectList(RejectionReasonList.ToList(), "PhotoRejectionReasonID", "Description");

                // return this.RedirectToAction(c => c.PhotoEditQuick(_model));

                return View(Photo);

            }

            // Model is valid.

            //string name = this.HttpContext.User.Identity.Name;
            string UserName = (from x in db.profiles.Where(z => z.ProfileID  ==Photo.ProfileID )
                                    select x.ProfileID).SingleOrDefault();
           // string user = ProfileID;

            //Added 1-9-2012 ola lawal
            //Only Send an Email if photo was rejected
            if (Photo.Aproved == "No")
            {
                //send the email to admin and the user
                var Email = new EmailModel();

                Email.ProfileID = Photo.ProfileID    ;//Photo.ProfileID;
                Email.UserName = UserName;   // MembershipService.GetUserNamebyProfileID(Photo.ProfileID); (can't get to work)
                Email.MiscelleaneousData = (from x in db.PhotoRejectionReasons.Where(z => z.PhotoRejectionReasonID == Photo.PhotoRejectionReasonID)
                                            select x.UserMessage).SingleOrDefault();
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email = LocalEmailService.CreateEmails(Email, EMailtype.PhotoRejected); // fixed 1-3-2011need to change
                localEmailService.SendEmailMessage(Email);

            }


            // return PartialView("PhotoApprovalView", Photo);
            //  return RedirectToAction("AdminHome");


            // Retrieve single value from photos table
            photo PhotoModify = db.photos.Single(p => p.PhotoID == Photo.PhotoID);

            // Update photo.Aproved value based on the value of the checkbox
            //if (Photo.checkedAproved == true)
            //{ Photo.Aproved = "Yes"; }
            //else { Photo.Aproved = "No"; }

            #region "Verify Approved Gallery Exists"


            /// <summary>
            /// Determines whether an approved gallery image exists and changes the value of ProfileImageType accordingly.
            /// </summary>
            /// 
            //initialize access to the datating context / service
            var datingService = new DatingService().Initialize();

            //call to the data model to verify approved galleryExists
            bool galleryExists = datingService.CheckForGalleryPhotoByProfileID(Photo.ProfileID.ToString());


            if (galleryExists == false)
            {
                if (Photo.Aproved == "No")
                {
                    Photo.ProfileImageType = "NoStatus";
                }
                else
                {
                    Photo.ProfileImageType = "Gallery"; // Changes ProfileImageType from "Nostatus" to "Gallery" 
                }
            }
            else { Photo.ProfileImageType = "NoStatus"; } // Keeps ProfileImageType = "NoStatus"

            #endregion

            // Update or attach values from AdminPhoto to PhotoModify model    
            MembersViewModel model = new MembersViewModel();
            model= CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));


            PhotoModify.Aproved = Photo.Aproved;
            PhotoModify.ProfileImageType = Photo.ProfileImageType;
            PhotoModify.PhotoReviewDate = DateTime.Now;
            PhotoModify.PhotoReviewerID = model.Profile.ProfileID ;
            PhotoModify.PhotoReviewStatusID = 1; //1 = PhotoReviewed
            if ((Photo.PhotoRejectionReasonID != 0) && (Photo.Aproved == "No"))
            {
                PhotoModify.PhotoRejectionReasonID = Photo.PhotoRejectionReasonID;
                PhotoModify.PhotoStatusID = 5; //deletebyadmin
            }

            // db.photos.Attach(PhotoModify); already attached
            // Update database
            db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            db.SaveChanges();

            ////// Attach Send email Here so the user knows that the photo has being approved.
            ///////

            // Guid id = (Guid)Photo.PhotoID;
            //return RedirectToAction(_PhotoEdit);
            return RedirectToAction("_PhotoEdit", "Admin", new System.Web.Routing.RouteValueDictionary(
            new { photoid = new Guid(Photo.PhotoID.ToString()) }));



            //this.RedirectToAction(c => c.PhotoEditQuick(_model));













            // ModelState.AddModelError("UploadPhotoError", "No photos were selected! , please select a photo to continue");

            //if (ModelState.IsValid)
            //{

            //}

            //return View(Photo);
            // return View();

        }

        
        [HttpPost]
        public void TestEmailMessaging()
        {

            new ActionMailController().VerificationEmail(new EmailModel { ProfileID = "Ola_lawal@yahoo.com", MessageSubject = "test of spam system formammted email" }).DeliverAsync();
          //  return View();
        }

        [HttpGet]
        public ActionResult SendEmailMatches()
        {

            new ActionMailController().WeeklyMatches(new EmailModel { ProfileID = "olen33@live.com", MessageSubject = "Here are you matches" }).DeliverAsync();


            return PartialView();

        }

          


        [HttpPost]
        public void SendEmailToALLMembers (EmailModel model)
        {//change code to get emails and do asynch drops 

            //get members in a good status to send messages to

            var validmembers = from profile in db.profiles.Where(p=>p.ProfileID != null && p.ProfileStatusID < 3 )
                 
                 select profile ;

         
            //loop through and send emails use a job timer probbaly to send a few evyer hour
            foreach (profile p in validmembers)
            {
            
            }
               

              


                
            //test of send
            new ActionMailController().VerificationEmail(new EmailModel { ProfileID = "Ola_lawal@yahoo.com", 
                MessageSubject = model.MessageSubject ,MessageBody = model.MessageBody  }).DeliverAsync();
            //return View();
        }

        [GetChatUsersData]
        public ActionResult ViewMembersOnline()
        {
            return View();
        }














    }
}
