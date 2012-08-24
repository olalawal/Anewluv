using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using System.Web.Routing;
using Shell.MVC2.Models;

using MvcContrib.Filters;
using Shell.MVC2.Filters;




using Shell.MVC2.AppFabric;

using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;
using Shell.MVC2.Infrastructure;


namespace Shell.MVC2.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public partial class MembersController : Controller
    {
        private MembersRepository _membersrepository; 
       

        //ninject contructor
        public MembersController(IChatService chatservice, IChatRepository chatrepository, MembersRepository membersrepositiory )
        {
         //_chatservice = chatservice;
        // _chatrepository = chatrepository;
         _membersrepository = membersrepositiory;

         //TO DO comment this out if not needed , this is only needed on pages that have the random chat box thing
       //  chatUsersOnline = _service.GetOnlineUsers();
                
        }



        protected override void Initialize(RequestContext requestContext)
        {
           
            // PagerQuickMyMatches = new PaginatedList<MemberSearchViewModel>();
            // PagerProfileBrowse = new PaginatedList<ProfileBrowseModel>();
            base.Initialize(requestContext);
        }


        // <HttpGet()> _
        // This members home is how we get to the members home page from the silverlight shell
        //since the user is being redirected with all thier authentication details intact we do not 
        //need a normal post/get login type implementation just need to verify the user context against the database and load the members page


        //6/1/2001 - added handling for paging, if page is not empty we need to remodel the partial view
        [Authorize]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpGet]
        [OutputCache(Duration = 0)] 
        [GetChatUsersData]
        public ActionResult MembersHome(int? page)
        {

            
// Throw a test error so that we can see that it is handled by Elmah
 // To test go to the ~/elmah.axd page to see if the error is being logged correctly
 //throw new Exception("A test exception for ELMAH");

            MembersViewModel model = new MembersViewModel();
             #if DISCONECTED
                    //no database connection for disconected testing, username still is needed 
                    var guestMapper = new ViewModelMapper();
                    model = guestMapper.MapGuest();
            // Disconnected mode needs a mock of the profile
                    profile MockProfile = new profile();
                    model.Profile = MockProfile;
                    model.Profile.UserName = this.HttpContext.User.Identity.Name;

                    return View(model);
              #endif


            //housekeeping get profileID
            string profileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
          
            //2-12-2011 changed the code to allways update the data of the members page on each refresh, tedious but is the saftest way to update the
            //members data on a consitent basis
            var mm = new ViewModelMapper();
            model = mm.MapMember(profileID);
            //now update whats in cache with the most current stuff we just loaded
            CachingFactory.MembersViewModelHelper.UpdateMemberData(model,profileID);
            //gets most current copy , or populates the sub models in the viewmodel
            //var  cf = new CachingFactory.MembersViewModelHelper();
           // model = CachingFactory.MembersViewModelHelper.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));
            //decpuple matches and browsemodel from the profile data 
            //build quick matches in the view.
        
        //store the username as a test for now
        
            if (ModelState.IsValid)
            {

                //if we are in a paging sitiuation handle the update to the model here
                if (page != null)
                {
                    //var pp = new PaginatedList<MemberSearchViewModel>();
                    //sets up the inital paging
                    //Store the page value in viewdata
                    ViewData["Page"] = page;
                   // var productPagedList = pp.GetPageableList(model.MyMatches, (int?)ViewData["Page"] ?? 1, 4);
                   //update the model yay;
                  //  model.MyMatchesPaged = productPagedList;  // set quick matches
                   
                
                }

                //rebuild the matches list                  
                  List<MemberSearchViewModel> myMatches =  _membersrepository.GetQuickMatches(model); 
                  //cant do paging here due to serialization
                  ViewBag.MyMatches= myMatches;
               
       
                               

              return View(model);

            }
         
                //return a diffrent view if the user is not logged in correctly
                //redirect them to the login page actually
                return RedirectToAction("LogOn", "Account");

        }

        //Updated with hack for ethincity , move this to repository
        //new json to chech members status etc.
        public JsonResult CheckMemberStatus(string bal) 
        {
                #if  DISCONECTED
            return Json(new { Status = true, PercentComplete = "NA" }); 
#else

            string profileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //bool ProfileNotEdited = false; //default to the profile being edited
            //load the person's model thing
            string editprofileUrl = "/EditProfile/EditProfileBasicSettings";
            int profileUpdatedPercent = 10;
            string message =  "Your profile is not yet complete ! Click  <b> OK </b>to edit your profile <b> Now </b> or edit it later using the EDIT link on this page (next to your picture)" +
	   "</p>	<p>		Currently Your Account is <b>{0}% complete</b>.</p>";

            //return Json(dataContext.Balances.ToList().Contains(bal, new BalanceEquality()));
            MembersViewModel model = new MembersViewModel();
            //gets most current copy
            //get profile ID also from 
            model = CachingFactory.MembersViewModelHelper.GetMemberData(profileID);

            
            //TO DO update this to use the calculation thing to detrmine what page to go to on the profile eidt maybe
            //or at least what percentage is updated
            //TO DO maybe get it from database since it could have changed since last refresh or session dead etc

              //if (model == null)              
              //model = _membersrepository.GetMembersViewModelAndReMapSettings(this.HttpContext);
                 
              //check basic settings edited
            //added check for height an other stuff
              if (model.profiledata.AboutMe == "Hello" | model.profiledata.MyCatchyIntroLine == "" )  
                  
                  profileUpdatedPercent =+ 10 ;
                  //tempoeary hack to fick members whose hights are broken
              else if (model.profiledata.Height == 0 | model.profiledata.Height == null)
              {
                  message = "There was a problem updating your profile please update your profile settings using the Edit Button";
                  editprofileUrl = "/EditProfile/EditProfileAppearanceSettings";
              }

              else if
                  //check appearance setttings
                  //TO DO Add more values 
             (model.profiledata.SearchSettings.FirstOrDefault().HeightMax == null | model.profiledata.HairColorID == null)
              {
                  profileUpdatedPercent = +10;
                  //also update the navigate URL then
                  editprofileUrl = "/EditProfile/EditProfileAppearanceSettings";
              }
              else
              {
                  profileUpdatedPercent = 100;

              }

            //so if they have not edited at all and they have not been shown the message show it
            if ( profileUpdatedPercent < 100 && model.ProfileStatusMessageShown == false)
            {
               
                //change  status of wether it can be shown
                model.ProfileStatusMessageShown = true;
                CachingFactory.MembersViewModelHelper.UpdateMemberData(model, profileID);
                return Json(new { Status = false,url= editprofileUrl , Message = string.Format(message,profileUpdatedPercent.ToString())});
            }

            //this will enure the modal is not shown
            CachingFactory.MembersViewModelHelper.UpdateMemberData(model, profileID );
            return Json(new { Status = true, PercentComplete = "NA" }); 

        
            #endif

            return null;
        } 


        //Does nothing
        [Authorize]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        [NoCache]
        public virtual ActionResult MembersHome(MembersViewModel Model)
        {
            //update the model in session here 
            //TO DO
            //we also have a remapper in the Viewmodel mapper, that might be a better pattern
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

           
           // CachingFactory.MembersViewModelHelper.UpdateMemberData(Model, _ProfileID);
            ModelState.Clear();
            
            // Model. = "a new value"; 
            if (ModelState.IsValid)
            
            {
                return View(Model);
            }

            MembersViewModel model = new MembersViewModel();
            //gets most current copy
          //  CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID); 

            return View(Model);
            
        }

        //shows the quick profile for members I guess
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpGet]
        [Authorize]
        public virtual ActionResult QuickProfileMembers(int? page, string returnUrl)
        {
            string ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //TO DO do away with stoting browsemodel in session
            //kill the browsemodel
            //housekeeping for previous searches clear the profile browsemodel i think
            //CachingFactory.ProfileBrowseModelsHelper.RemoveMemberResults(ProfileID);

            //get the model and pass the matches into the quicksearch global session variable that
            //holds the current search on the home controller and all controllers

            MembersViewModel model = new MembersViewModel();
           
            model =  CachingFactory.MembersViewModelHelper.GetMemberData(ProfileID);
           
            

            //pass the model in my matches i guess

            //rebuild matches and store in viewbag

            List<MemberSearchViewModel> MyMatches = _membersrepository.GetQuickMatches(model);
            //cant do paging here due to serialization
            //Re do this later to pass model
            Session["QuickSearch"] = MyMatches;
               

           // Session["QuickSearch"] = Model.MyMatches;


            

            if (ModelState.IsValid) 
            {
                return RedirectToAction("QuickProfile", "Home", new { ProfileID, returnUrl });          
            }


            return View("MembersHome");
          

        }

        //shows the quick profile for members I guess
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpGet]
        [Authorize]
        public virtual ActionResult QuickProfileMine(int? page, string returnUrl)
        {
            string ViewingProfileId = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //TO DO do away with stoting browsemodel in session
            //kill the browsemodel
            //housekeeping for previous searches clear the profile browsemodel i think
            //CachingFactory.ProfileBrowseModelsHelper.RemoveMemberResults(ProfileID);

            //get the model and pass the matches into the quicksearch global session variable that
            //holds the current search on the home controller and all controllers

         //   MembersViewModel model = new MembersViewModel();

        //    model = CachingFactory.MembersViewModelHelper.GetMemberData(ProfileID);

            //rebuild the matches list                  
           // List<MemberSearchViewModel> MyMatches = _membersrepository.GetQuickMatches(model);
            //cant do paging here due to serialization
           // ViewBag.MyMatches = MyMatches;


            //TODO apply this to members home, move this to an extention method or a MemberSearchViewModelMapper
            //also add a one value search profile thing so they can view thier own profile, do this also on the members home
          //  MemberSearchViewModel MyProfileView = new  MemberSearchViewModel(model); //= membersrepository.GetQuickMatches(model);

            //var MyProfileViewSingle = new MemberSearchViewModel
            //{
              
            //    ProfileID = model.profiledata.ProfileID,
            //    State_Province = model.profiledata.State_Province,
            //    PostalCode = model.profiledata.PostalCode,
            //    CountryID = model.profiledata.CountryID,
            //    GenderID = model.profiledata.GenderID,
            //    Birthdate = model.profiledata.Birthdate,
            //    profile = model.Profile,
            //    //Longitude = (double)model.profiledata.Longitude,
            //    //Latitude = (double)model.profiledata.Latitude,
            //    // HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
            //    CreationDate = model.Profile.CreationDate,
            //    City = Extensions.Chop(model.profiledata.City, 11),
            //    lastloggedonString = DateTime.Now.ToShortDateString(),
            //    LastLoginDate = model.Profile.LoginDate,
            //    Online = true,
            //    DistanceFromMe = 0

            //};
            //add it to the list
           // MyProfileView.Add(MyProfileViewSingle);
            //add it to the model
           // model.MyCurrentSearchList = MyProfileView;



            //save it in quick search so we can browse it
           // Session["QuickSearch"] = MyProfileView;





            if (ModelState.IsValid)
            {
                return RedirectToAction("QuickProfile", "Home", new {ViewingProfileId, returnUrl });
            }


            return View("MembersHome");


        }

        #region "peeks and view related actions"

           [Authorize]
        public virtual  ActionResult WhoViewedMe(int? Page, int? NumberPerPage)
        {

            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            //var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);
            
          //  NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;           

            //get the list of peeks 
        //   MemberSearchViewModels =  _membersrepository.GetWhoPeekedAtMe(ProfileID);
           //store to session since thats how QuickProfileWill access it sicne we are not passing it 
         //  Session["QuickSearch"] = MemberSearchViewModels;
         //  var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());





           // return View("WhoViewedMe", PagedList);
            return View();

        }

           [Authorize]
        public virtual ActionResult WhoIViewed(int? Page, int? NumberPerPage)
        {

            

           //return RedirectToAction("Underconstruction", "Members");
          //  List<MemberSearchViewModel> MemberSearchViewModels;
          //  var pp = new PaginatedList<MemberSearchViewModel>();


           string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);
      
           //set the default number of items per page if it is null
           NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;

            
            

           

            //get the list of peeks 
         return View ("WhoIViewed",_membersrepository.GetWhoIHavePeekedAt(ProfileID,Page,NumberPerPage));

           //store to session since thats how QuickProfileWill access it sicne we are not passing it 
         //  Session["QuickSearch"] = MemberSearchViewModels;

        //   var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());






       //     return View("WhoIViewed", PagedList);
           // return View();
        }

           [Authorize]
        public virtual ActionResult WhoIViewedNew(int? Page, int? NumberPerPage)
        {
            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID =CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;






            //get the list of peeks 
            MemberSearchViewModels = _membersrepository.GetWhoPeekedAtMeNew(ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());


            return View("WhoViewedMeNew", PagedList);
        }

        
           public JsonResult UpdateWhoViewedMe(string ScreenName)
           {               //MembersViewModel model = new MembersViewModel();
               //gets most current copy , or populates the sub models in the viewmodel
               //var  cf = new CachingFactory.MembersViewModelHelper();
               string ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
               string TargetProfileID = CachingFactory.getprofileidbyscreenname(ScreenName);

               _membersrepository.UpdatePeekViewStatus(ProfileID, TargetProfileID);

               return null;
           }

        #endregion

        #region "Interests and view related actions"


        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult Interests(int? Page, int? NumberPerPage)
        {

            //update the users online
             
             //ViewBag.OnlineChatUsers = chatUsersOnline;

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID =CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Interests 
            MemberSearchViewModels = _membersrepository.GetWhoIsInterestedInMe(ProfileID);
            

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
             Session["QuickSearch"] = MemberSearchViewModels;

             var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());


          //  ProfileRepository profileRepo = new ProfileRepository();
           // List<ProfileBrowseModel> browsemodel = new List<ProfileBrowseModel>();
            //string _profileID = _membersrepository.getProfileID(HttpContext.User.Identity.Name);

            //TO DO possilbly move this to cache and get it from the members viewmodel data
            //profiledata YourProfile = _membersrepository.GetProfileDataByProfileID(ProfileID);
          //  profiledata YourProfile = CachingFactory.MembersViewModelHelper.GetMemberData(ProfileID).profiledata;
            //browsemodel = profileRepo.GetQuickProfileWithUsername(MemberSearchViewModels, YourProfile).ToList();

          // CachingFactory.ProfileBrowseModelsHelper.RemoveMemberResults (ProfileID);

            //save the model as a profilebrowsemodel session things
         // CachingFactory.ProfileBrowseModelsHelper.AddMemberResults(browsemodel, ProfileID);





             return View("Interests", PagedList);

        }

        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult InterestsNew(int? Page, int? NumberPerPage)
        {

            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Interests 
            MemberSearchViewModels = _membersrepository.GetWhoIsInterestedInMeNew(ProfileID);
            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;


            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());


       //     ProfileRepository profileRepo = new ProfileRepository();
        //    List<ProfileBrowseModel> browsemodel = new List<ProfileBrowseModel>();
            //string _profileID = _membersrepository.getProfileID(HttpContext.User.Identity.Name);
            //Get from members viewmodel
          //  profiledata YourProfile = CachingFactory.MembersViewModelHelper.GetMemberData(ProfileID).profiledata;
          //  browsemodel = profileRepo.GetQuickProfileWithUsername(MemberSearchViewModels, YourProfile).ToList();


        //   CachingFactory.ProfileBrowseModelsHelper.RemoveMemberResults (ProfileID);
            //save the model as a profilebrowsemodel session things
         // CachingFactory.ProfileBrowseModelsHelper.AddMemberResults(browsemodel, ProfileID);





            return View("InterestsNew", PagedList);

        }

        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult InterestsSent(int? Page, int? NumberPerPage)
        {

            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();

            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;

            //get the list of Interests 
            MemberSearchViewModels = _membersrepository.GetWhoIamInterestedIn(ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());

            return View("InterestsSent", PagedList);

        }

   
        /// <summary>
        ///update the Interest viewed status 
        /// </summary>
        /// <param name="ScreenName"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Authorize]
          public JsonResult UpdateInterestViewed(string ScreenName)
           {               //MembersViewModel model = new MembersViewModel();
               //gets most current copy , or populates the sub models in the viewmodel
               //var  cf = new CachingFactory.MembersViewModelHelper();
              string ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
               string TargetProfileID =    CachingFactory.getprofileidbyscreenname(ScreenName);

               _membersrepository.UpdateIntrestViewStatus(ProfileID,TargetProfileID);

               return null;

           }


          /// <summary>
          ///remove intrests  
          /// </summary>
          /// <param name="ScreenName"></param>
          /// <returns></returns>
          /// 
          [HttpPost]
        [Authorize]
          public JsonResult RemoveInterests(List<string> json)
          {               //MembersViewModel model = new MembersViewModel();
              //gets most current copy , or populates the sub models in the viewmodel
              //var  cf = new CachingFactory.MembersViewModelHelper();
             // string ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
             // string TargetProfileID = CachingFactory.getprofileidbyscreenname(ScreenName);

            //  _membersrepository.UpdateIntrestViewStatus(ProfileID, TargetProfileID);
             bool  result = false;
             return Json(new { result = result });

          }

        #endregion

        #region "Likess and view related actions"


           [Authorize]
           [GetChatUsersData]
        public virtual ActionResult Likes(int? Page, int? NumberPerPage)
        {

            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Likess 
            MemberSearchViewModels = _membersrepository.GetWhoLikesMe (ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());


       
            return View("Likes", PagedList);

        }


           [Authorize]
           [GetChatUsersData]
        public virtual ActionResult LikesNew(int? Page, int? NumberPerPage)
        {
            


            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Likess 
            MemberSearchViewModels = _membersrepository.GetWhoLikesMeNew(ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());

            return View("LikesNew", PagedList);

        }

           [Authorize]
           [GetChatUsersData]
        public virtual ActionResult WhoIlike(int? Page, int? NumberPerPage)
        {

            

            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Likess 
            MemberSearchViewModels = _membersrepository.GetWhoILike(ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());


    

            return View("WhoILike", PagedList);

        }


           public JsonResult UpdateLikeViewed(string ScreenName)
           {               //MembersViewModel model = new MembersViewModel();
               //gets most current copy , or populates the sub models in the viewmodel
               //var  cf = new CachingFactory.MembersViewModelHelper();
               string ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
               string TargetProfileID = CachingFactory.getprofileidbyscreenname(ScreenName);

               _membersrepository.UpdateLikeViewStatus(ProfileID, TargetProfileID);

               return null;
           }

        #endregion

        #region "Block related actions"

           [Authorize]
           [GetChatUsersData]
        public virtual ActionResult WhoIBlocked(int? Page, int? NumberPerPage)
        {



            //return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            //get the list of Likess 
            MemberSearchViewModels = _membersrepository.GetWhoIHaveBlocked(ProfileID);

            //store to session since thats how QuickProfileWill access it sicne we are not passing it 
            Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());



            return View("WhoIBlocked", PagedList);

        }

        //not implemented
           [Authorize]
        public virtual ActionResult Reported(int? Page, int? NumberPerPage)
        {



            ////return RedirectToAction("Underconstruction", "Members");
            List<MemberSearchViewModel> MemberSearchViewModels;
            var pp = new PaginatedList<MemberSearchViewModel>();


            string ProfileID = CachingFactory.GetProfileIDByUserName(HttpContext.User.Identity.Name);

            //NumberPerPage = (NumberPerPage != null) ? NumberPerPage : 4;



            ////get the list of Likess 
            MemberSearchViewModels = _membersrepository.GetWhoLikesMeNew(ProfileID);

            ////store to session since thats how QuickProfileWill access it sicne we are not passing it 
            //Session["QuickSearch"] = MemberSearchViewModels;

            var PagedList = pp.GetPageableList(MemberSearchViewModels, Page ?? 1, NumberPerPage.GetValueOrDefault());

            return View("Reported", PagedList);

        }

      

        #endregion

         

        public virtual ActionResult Underconstruction()
        {
            return View();
        }

        //end of interests methods

        //Search actions

           [Authorize]
        public ActionResult CustomSearch()
        {
            string _ProfileId = AppFabric.CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //TO DO change this to a new query against user name (DONE)
            MembersViewModel membersmodel = new MembersViewModel();

            membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileId);
         

            //setup the searchModel part
           // membersmodel.profiledata.SearchSettings.Add(_membersrepository.GetPerFectMatchSearchSettingsByProfileID(membersmodel.profiledata.ProfileID));
            CustomSearchViewModel model = new CustomSearchViewModel();
         

               //load the My matches seach settings as default
           model.BasicSearchSettings =  new  SearchModelBasicSettings(membersmodel.profiledata.SearchSettings.Where(p=>p.MyPerfectMatch==true).FirstOrDefault());
            


         
       
            // if (model.IsUploading == true)
            //    return View(model);
           
        
          

            return View(model );
        }

        [HttpPost]
        [Authorize]
        public ActionResult CustomSearch(SearchProfilesViewModel model)
        {
            return View();
        }

           [Authorize]
        public ActionResult SavedSearches()
        {
            return View();
        }

          [HttpPost]
          [Authorize]
        public ActionResult SavedSearches(SearchProfilesViewModel model)
        {
            return View();
        }

        //TO DO send index as well
        //*** for all these get the index in the profile browsemodel as well
        //so we can update the actual value as well until we can come up with a more effiecnt query for the whole set of results
         
        //TO DO this action is in members repop since it is not a real action
        //peek is not an action just a method      
        [NonAction]
        public void SendPeek(ProfileBrowseModel model, int Page, string returnUrl)
        {
            
           

        }

        //We are passing Page so we can update the profile browsemodel
        //[Authorize]
        //
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [Authorize]
        public virtual ActionResult SendInterest(string SenderScreenName, string TargetScreenName, int Page, string returnUrl)
        {
            string ProfileID, TargetProfileID, UserName;

            //get the info
            using (DatingService  context = new DatingService())
            {
                ProfileID = context.getprofileidbyscreenname(SenderScreenName);
                TargetProfileID = context.getprofileidbyscreenname(TargetScreenName);
                
                //attaempt to add the intrest, if we get a false it means either intrest existed or you were trying to interest yourslef.
                if (context.AddIntrest(ProfileID, TargetProfileID) == false) return Redirect(returnUrl);
                //get the username for the email message
                UserName = context.GetUserNamebyProfileID(TargetProfileID);
            
            }



            // string TargetProfileID; string ProfileID; string UserName;
            var Email = new EmailModel();
            Email.ScreenName = TargetScreenName;
            Email.ProfileID = TargetProfileID;
            Email.UserName = UserName;
            Email.SenderScreenName =SenderScreenName;
            Email.SenderProfileID = ProfileID;

            var localEmailService = new LocalEmailService();
            Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileInterestRecieved );
             localEmailService.SendEmailMessage(Email);

        

            return Redirect(returnUrl);

        }

        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [Authorize]
        public virtual ActionResult SendLike(string SenderScreenName, string TargetScreenName, int Page, string returnUrl)
        {
            string ProfileID, TargetProfileID, UserName;

            //get the info
            using (DatingService context = new DatingService())
            {
                ProfileID = context.getprofileidbyscreenname(SenderScreenName);
                TargetProfileID = context.getprofileidbyscreenname(TargetScreenName);

                //attaempt to add the intrest, if we get a false it means either intrest existed or you were trying to Like yourslef.
                if (context.AddLike(ProfileID, TargetProfileID) == false) return Redirect(returnUrl);
                //get the username for the email message
                UserName = context.GetUserNamebyProfileID(TargetProfileID);

            }



            // string TargetProfileID; string ProfileID; string UserName;
            var Email = new EmailModel();
            Email.ScreenName = TargetScreenName;
            Email.ProfileID = TargetProfileID;
            Email.UserName = UserName;
            Email.SenderScreenName = SenderScreenName;
            Email.SenderProfileID = ProfileID;

            var localEmailService = new LocalEmailService();
            Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileLikeRecived);
            localEmailService.SendEmailMessage(Email);



            return Redirect(returnUrl);

        }


        [Authorize]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult ReportAbuse(string SenderScreenName,string TargetScreenName,int Page, string returnUrl)
        {

            return Redirect(returnUrl);



        }

        [Authorize]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult SendBlock(string SenderScreenName,string TargetScreenName,int Page, string returnUrl)
        {

            string ProfileID, TargetProfileID, UserName;

            //get the info
            using (DatingService context = new DatingService())
            {
                ProfileID = context.getprofileidbyscreenname(SenderScreenName);
                TargetProfileID = context.getprofileidbyscreenname(TargetScreenName);

                //attaempt to add the intrest, if we get a false it means either intrest existed or you were trying to interest yourslef.
                if (context.Addblock(ProfileID, TargetProfileID) == false) return Redirect(returnUrl); 
                //get the username for the email message
                UserName = context.GetUserNamebyProfileID(TargetProfileID);

            }



            // string TargetProfileID; string ProfileID; string UserName;
            var Email = new EmailModel();
            Email.ScreenName = TargetScreenName;
            Email.ProfileID = TargetProfileID;
            Email.UserName = UserName;
            Email.SenderScreenName = SenderScreenName;
            Email.SenderProfileID = ProfileID;

            var localEmailService = new LocalEmailService();
            Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileBlockSent);
            localEmailService.SendEmailMessage(Email);



            return Redirect(returnUrl);

        }

        [Authorize]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult UnblockBlock(string SenderScreenName,string TargetScreenName,int Page, string returnUrl)
        {



            string ProfileID, TargetProfileID, UserName;

            //get the info
            using (DatingService context = new DatingService())
            {
                ProfileID = context.getprofileidbyscreenname(SenderScreenName);
                TargetProfileID = context.getprofileidbyscreenname(TargetScreenName);

                //attaempt to add the intrest, if we get a false it means either intrest existed or you were trying to interest yourslef.
                if (context.Removeblock(ProfileID, TargetProfileID) == false) return Redirect(returnUrl);
                //get the username for the email message
                UserName = context.GetUserNamebyProfileID(TargetProfileID);

            }


            //NO email for blocks removed 
            // string TargetProfileID; string ProfileID; string UserName;
            //var Email = new EmailModel();
            //Email.ScreenName = TargetScreenName;
            //Email.ProfileID = ProfileID;
            //Email.UserName = UserName;
            //Email.SenderScreenName = SenderScreenName;
            //Email.SenderProfileID = ProfileID;

            //var localEmailService = new LocalEmailService();
            //Email = LocalEmailService.CreateEmails(Email, EMailtype.);
            //localEmailService.SendEmailMessage(Email);



            return Redirect(returnUrl);

        }


        

                   [Authorize]      
        public virtual ActionResult TestLinksBug()
        {






            return View();

        }

        //


       


        


    }
}
