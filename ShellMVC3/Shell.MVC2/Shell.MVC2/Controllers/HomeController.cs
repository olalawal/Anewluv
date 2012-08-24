using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;


using Dating.Server.Data.Services;
using Dating.Server.Data.Models;



using System.Web.Routing;
using MvcContrib.Filters;
using Shell.MVC2.Models;
using Shell.MVC2.AppFabric;
using Shell.MVC2.Filters;
using Newtonsoft.Json;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Controllers
{
  

    //[HandleError]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public partial class HomeController : Controller
    {
        //TO DO use autofac or some sort of IOC
       // private AnewLuvFTSEntities db;
      //  private PostalDataService _postaldataservice; 
        private GeoRepository _geoRepo;
        private ProfileRepository profileRepo;
     //  private EditProfileRepository editProfileRepository;
        private MembersRepository membersRepo;
        private PaginatedList<MemberSearchViewModel> PagerQuickSearch;
        private PaginatedList<ProfileBrowseModel> PagerProfileBrowse;
        const int QuickSearchPageSize = 25;
        const int DetailSearchPageSize = 25;
        private List<MemberSearchViewModel> quickSearch;




        protected override void Initialize(RequestContext requestContext)
        {
            //if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            //if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            //initialize the dating service here
            //datingservicecontext = new DatingService().Initialize();

           
       


            base.Initialize(requestContext);
        }


         //ninject contructor
        public HomeController(MembersRepository _membersrepo,PostalDataService postaldataservice,GeoRepository georepo)
        {
            PagerQuickSearch = new PaginatedList<MemberSearchViewModel>();
            PagerProfileBrowse = new PaginatedList<ProfileBrowseModel>();
            profileRepo = new ProfileRepository();
            membersRepo = _membersrepo;
            editProfileRepository = new EditProfileRepository();
           // _postaldataservice = postaldataservice;
            _geoRepo = georepo ;
        }


      
        //COntact us model
        public ActionResult ContactUs()
        {
            ViewData["ContactUsStatus"] = "Do you have questions or comments about AnewLuv.com ? Please fill out this form below";
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsModel model)
        {
            //send the pyspical message
            if (ModelState.IsValid)
            {
                //build the email packege as well as validate the user
                var Email = new EmailModel();
               // using (DatingService context = new DatingService())
              //  {
                   Email.UserName  = model.Name ;
                   Email.ProfileID = model.Email;
              //  }
                //send the email
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email.ContactUsBody  = model.Message;
                Email.ContactUsSubject   = model.Subject;
                Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileContactUs);

                localEmailService.SendEmailMessage(Email);

                //retrurn to view with view data
                ViewData["ContactUsStatus"] = " Your Message has been Successfully sent";
                //clear the model out, ie reset
                //TO DO
                //find a way to reset the form and hide send button using Jquery
            }

            return View(model);
        }


        //Added code to add geolocation data to temproary members model
        [HttpPost]
        public JsonResult UpdateGuestGeoData(MembersViewModel  json)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
           
            //retrive and save the country i think        
           
            var model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);
          

            //save synch up with the data from geo data 
#if !DISCONECTED
          
            //do not execute the update if we have already updated the geodatate
            //since this data gets updated by geocoding anyways, so only do this once perssesion
            //or if we are dealing wit ha registered user
            if (model.MyQuickSearch.geocodeddata == true & model.Profile !=null)
                return Json(new { model = JsonConvert.SerializeObject(model, Formatting.Indented) , success = model.MyQuickSearch.geocodeddata });
            else if (model.Profile != null)
            {                
                return Json(new { model = JsonConvert.SerializeObject(model, Formatting.Indented), success = model.MyQuickSearch.geocodeddata });
            }
            //otherwise we are in a new session
            model = _geoRepo.PopulateQuickSearchWithGeoDemoData(json, model);  
              if (model.MyQuickSearch == null || model.MyQuickSearch.geocodeddata == false )
            {
                var Email = new EmailModel();
                //admin side
                Email.EmailAdmins = Resources.EmailMessageResources.AdminEmail; //to for admins
                Email.FromAdmin = Resources.EmailMessageResources.DefaultEmailSender;
                Email.AdminMessageBody = String.Format("Missing country {0}", json.MyCountryName);
                Email.AdminMessageSubject = "A user with a country not in the database visited the site";

                //set default values
                model = membersRepo.GetDefaultSearchSettingsGuest(model);
            }
#else 
          model.MyQuickSearch = _geoRepo.PopulateQuickSearchWithGeoDemoDataDisconnected(json, model.MyQuickSearch);  
    
#endif

#if !DISCONECTED
//update guest data             
            CachingFactory.MembersViewModelHelper.UpdateGuestData(model, this.HttpContext);
#endif

          //get the most current version of the profile/
            //var myProfile = membersrepo.GetProfileDataByProfileID(model.Profile.ProfileID);
            //if (model.profiledata != null)
            //    return Json(new { MyCity = model.profiledata.City });

            
            var serializedObject = JsonConvert.SerializeObject(model, Formatting.Indented);
            return Json(new { model = JsonConvert.SerializeObject(model, Formatting.Indented), success = model.MyQuickSearch.geocodeddata });



        }

        //Added code to add geolocation data to temproary members model
        [HttpPost]
        public JsonResult LogGuestGeoData(MembersViewModel json)
        {
            
#if !DISCONECTED
//update guest data 
           
             return Json(new { success =  _geoRepo.LogGuestGeoData(json) });
#endif

            //get the most current version of the profile/
            //var myProfile = membersrepo.GetProfileDataByProfileID(model.Profile.ProfileID);
            //if (model.profiledata != null)
            //    return Json(new { MyCity = model.profiledata.City });
            return null;

           



        }

        //index basically

        [OutputCache(Duration = 0)] 
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [HttpGet]
        public virtual ActionResult SplashPage()
        {
            //reset session
           //  Session.Abandon();
          
              #if DEBUG
              //   Console.WriteLine("Clear out persistant cache for Debug version");               
              //  CachingFactory.SharedObjectHelper.RemoveAllLists();    
              #endif

            
            

            return View();
        }



        //index for silverligh users 
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult ShellTestPage()
        {



            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            //   return View(photo);
            //  Return View(dn);
            // Return View(Gender);
            return View();
        }



        //replace this routine with model passing like in the account controller instead of storing it all in session eachtime
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        private void GetQuickSearch()
        {
            if (Session["QuickSearch"] != null) quickSearch = (List<MemberSearchViewModel>)Session["QuickSearch"];
            else quickSearch = new List<MemberSearchViewModel>();
        }

        [ModelStateToTempData]
        [PassParametersDuringRedirect] 
        public virtual ActionResult Index()
        {
           // var datingService = new DatingService().Initialize();
            return View();

        }


        //sends email from profile detila i.e quick profile
        [HttpPost]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult SendEmail(string RecipientName, string account)
        {

            if (account == "Create An Account")
            {// User wants to create an account 
                return View("Index");
            }
            else
            {
                return RedirectToAction("SendMsg", "Mail", new { RecipientID = RecipientName });
                //return RedirectToAction("SendMsg", "Mail");
                //return RedirectToAction("MailHome", "Mail");

                //return View("Index");
            }
        }

        
        [ModelStateToTempData]
        [GetChatUsersData]
        [PassParametersDuringRedirect]
        public virtual ActionResult QuickProfile(string ScreenName, string ReturnURL,string ViewingProfileId)
        {

            //TO do use roles or something to figure this , use a custom authorzie
            //hack code for not allowing guests to go past page 2
            if (HttpContext.User.Identity.IsAuthenticated == false)
               return RedirectToAction("Register", "Account");




            //update peeks here i.e soemone peeked at this profile

            ProfileBrowseModel model = new ProfileBrowseModel();
             MembersViewModel membersmodel = new MembersViewModel();
            //12/18/2011 create a single list item of seach model so we can use the same methods
            //that worked for list of seachmodels to build the browsemodel
           //  List<MemberSearchViewModel> SearchModel = new List<MemberSearchViewModel>();
          
               

            //get profile ID only  of the viewer once from cache
            string _ProfileID =  CachingFactory.GetProfileIDByUserName(User.Identity.Name);
            //get profileID of the person being viewd by screen nmae
            string ProfileId = (ScreenName != null ) ? CachingFactory.getprofileidbyscreenname(ScreenName) : ViewingProfileId;
                     
              
                // Check if the user is not authenticated or is a guest user
                if (_ProfileID == null)
                {

                    //build the profileBrowsmodel here
                    model = profileRepo.GetProfileBrowseModelGuest(ProfileId);     
               
                    //store the profile criteria in a viewbag I guess so we only do it once way more efficent
                    ViewBag.MyProfileCriteria = new ProfileCriteriaModel();
                    return View("QuickProfile",model);
                }
                else
                {

                    //10-11-2011 changed code to get profile data from cache
                    membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);

                    //add the single searchmodel value so we can get browsemodel
                  //  SearchModel.Add(profileRepo.GetMemberSearchModelByProfileID(ProfileId, membersmodel));
                    model = profileRepo.GetProfileBrowseModelMembers(ProfileId, membersmodel);

                 
                    //add the profilecriteria for the viewer
                    //10.25-2011
                    //store the profile criteria in a viewbag I guess so we only do it once way more efficent
                   // ViewBag.MyProfileCriteria = new ProfileCriteriaModel(membersmodel.profiledata);

                }
            
         
            //TO do use notification service to send emails down the line
                 if  ( membersRepo.AddPeekFromBrowseModel(model)){
                 
                      // string TargetProfileID; string ProfileID; string UserName;
            var Email = new EmailModel();
            Email.ScreenName = model.ProfileDetails.profile.ScreenName;
            Email.ProfileID = model.ProfileDetails.ProfileID;
            Email.UserName = model.ProfileDetails.profile.UserName;
            Email.SenderScreenName = model.ViewerProfileDetails.profile.ScreenName;
            Email.SenderProfileID = model.ViewerProfileDetails.profile.ProfileID;

            var localEmailService = new LocalEmailService();
            Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfilePeekReceived);
            localEmailService.SendEmailMessage(Email);

                 }


            using (AnewLuvFTSEntities db = new AnewLuvFTSEntities())
            {
               model.IntrestSentToThisMember = db.Interests.Any(r => r.ProfileID == model.ViewerProfileDetails.ProfileID && r.InterestID == model.ProfileDetails.profile.ProfileID);
                model.BlockedThisMember = db.Mailboxblocks.Any(r => r.ProfileID == model.ViewerProfileDetails.ProfileID && r.BlockID == model.ProfileDetails.profile.ProfileID);
                //to do add that members block status to u i guess to depending on speed of these queries
                model.LikedThisMember = db.Likes.Any(r => r.ProfileID == model.ViewerProfileDetails.ProfileID && r.LikeID == model.ProfileDetails.profile.ProfileID);
                //send a peek udpate as well since we have looked at profile
               
            }



            return View("QuickProfile", model);

        }

        [HttpGet]
        public ActionResult QuickProfileSwitchPhoto(Guid photoid)
        {

             // Retrieve updated single photo profile by PhotoId
            EditProfilePhotoModel Photo = editProfileRepository.GetSingleProfilePhotoByphotoID(photoid);
            var src = Photo;

            return View("QuickProfileMainPhoto", src);
            //return PartialView("PhotoEditView", src);
            // return Json(src);
        }



        
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        public virtual ActionResult QuickSearchMembers(MembersViewModel model)
        {
            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);

                  

            //housekeeping for previous searches clear the profile browsemodel i think
            //clear the profile browsemodel 
            CachingFactory.ProfileBrowseModelsHelper.RemoveMemberResults(_ProfileID);



            //variables
            string strCity = "";
            string strStateProvince = "";
            GpsData GpsDataBestMatch = null;


            //pouplates quicksearch if its empty
            GetQuickSearch();


            //update the model here, with the posted search data 
            //CachingFactory.MembersViewModelHelper.Update(Model, this.HttpContext);
           

             //now u should have thenow  most current model so get it 
             //9-21-2011 this will run a remap procedure only remapping items that do not exists in this model
            //get current model and update it 
            MembersViewModel _model = new MembersViewModel();
            _model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);
                  


            ModelState.Clear();
            //this is for kusers logged in

            //custom code added 2/25 for this\
            //PostalDataService postaldataservicecontext = new PostalDataService().Initialize(); //TO DO change this to add the countrY ID to the the selected value and use the
            //country name as display later 

            //update 3/17/2011 to fix bug blank selected values caused errors , use a conidaltional statement here 
            string[] cityAndProvince = (model.MyQuickSearch.MySelectedCityStateProvince == null) ? null : model.MyQuickSearch.MySelectedCityStateProvince.Split(',');

            //string[] cityAndProvince = Model.MyQuickSearch.MySelectedCityStateProvince.Split(',');

            //update values fo quick search function
            //TO DO fix the search box on to actually use conplex property and presplit this
            if (cityAndProvince != null)
            {
                strCity = cityAndProvince[0];
                if (cityAndProvince.Count() > 1)
                {
                    strStateProvince = cityAndProvince[1];
                }

                if (model.MyQuickSearch.MySelectedPostalCode == null &&  strStateProvince !=null)
                {                    
                    
                    //build up the gps data since they chose a specific city
                    //11-1-2011 handling case where they do not enter in a state province
                    GpsDataBestMatch = (strStateProvince !="")? postaldataservicecontext.GetGpsDataByCountryAndCity(model.MyQuickSearch.MySelectedCountryName, strCity).Where(p =>  p.State_Province == strStateProvince).FirstOrDefault():
                        postaldataservicecontext.GetGpsDataByCountryAndCity(model.MyQuickSearch.MySelectedCountryName, strCity).FirstOrDefault();

                    //TO DO get these from the user's IP adress going for ward for now just validate to zero
                    model.MyQuickSearch.MySelectedLatitude = GpsDataBestMatch.Latitude;
                    model.MyQuickSearch.MySelectedLongitude = GpsDataBestMatch.Longitude;
                }
                
                else if (model.MyQuickSearch.MySelectedPostalCode != null)
                {
                  GpsDataBestMatch =postaldataservicecontext.GetGpsDataByCountryPostalCodeandCity(model.MyQuickSearch.MySelectedCountryName,model.MyQuickSearch.MySelectedPostalCode , strCity).FirstOrDefault();
                //TO DO get these from the user's IP adress going for ward for now just validate to zero
                    model.MyQuickSearch.MySelectedLatitude = GpsDataBestMatch.Latitude;
                    model.MyQuickSearch.MySelectedLongitude = GpsDataBestMatch.Longitude;
                
                }

            //otherwise just use city i guess
               
            }



            string lookingForGender = Extensions.ConvertGenderID(model.MyQuickSearch.MySelectedSeekingGenderID);
            int SelectedCountryId = postaldataservicecontext.GetCountryIdByCountryName(model.MyQuickSearch.MySelectedCountryName);
            double DistanceFromMe = (model.MyQuickSearch.MySelectedMaxDistanceFromMe.Value  == 0) ? 1000 : model.MyQuickSearch.MySelectedMaxDistanceFromMe.Value;

            //update the model just for housekeeping and so we can make this whole thing reusable
            _model.MyQuickSearch = model.MyQuickSearch;
            //update Cache
            //depeneding on guest or member 
            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData(_model, _ProfileID) : CachingFactory.MembersViewModelHelper.UpdateGuestData(_model, this.HttpContext);
            
           




            //execute the search
            var Profiles = profileRepo.GetQuickSearchMembers(model.MyQuickSearch.MySelectedFromAge, model.MyQuickSearch.MyselectedToAge
                                                      , lookingForGender,
                                                    SelectedCountryId, strCity, strStateProvince, DistanceFromMe,model.MyQuickSearch.MySelectedPhotoStatus, model);
            // Save QuickSearch for global use

            quickSearch = Profiles.ToList();
            //store search results into session
            Session["QuickSearch"] = quickSearch;


            var paginatedSearch = PagerQuickSearch.GetPageableList(quickSearch, model.MyQuickSearch.mySelectedCurrentPage ?? 0, model.MyQuickSearch.mySelectedPageSize);


            // Return to one location to minimize redundant coding, although it is possible to return to QuickSearch.aspx 
            //for redundancy sake  // we will return only to QuickSearchPager.aspx

            //return RedirectToAction("ViewQuickSearch");
            return RedirectToAction("ViewQuickSearch", new { model });
        }

        //quick search for the guests , nothing is loaded from session
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [HttpPost]
        public virtual ActionResult QuickSearch(MembersViewModel model)
        {
            //variables
            string strCity = "";
            string strStateProvince = "";       
            GpsData GpsDataBestMatch = null;
           // bool CurrentPostalCodeStatus = false;


            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //retrive and save the country i think
           
            //save quick search for later use
           var modeltosave = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);
           modeltosave.MyQuickSearch = model.MyQuickSearch;
           model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData(modeltosave, _ProfileID) : CachingFactory.MembersViewModelHelper.UpdateGuestData(modeltosave, this.HttpContext);
            
       

            //custom code added 2/25 for this\
            //PostalDataService postaldataservicecontext = new PostalDataService().Initialize(); //TO DO change this to add the countrY ID to the the selected value and use the
       
            //update 3/17/2011 to fix bug blank selected values caused errors , use a conidaltional statement here 
            string[] cityAndProvince = (model.MyQuickSearch.MySelectedCityStateProvince == null) ? null: model.MyQuickSearch.MySelectedCityStateProvince.Split(',');

            //string[] cityAndProvince = Model.MyQuickSearch.MySelectedCityStateProvince.Split(',');

            //update values fo quick search function
            //TO DO fix the search box on to actually use conplex property and presplit this
            if (cityAndProvince != null)
            {
                strCity = cityAndProvince[0];
                if (cityAndProvince.Count() > 1)
                {
                    strStateProvince = cityAndProvince[1];
                }
                
                //build up the gps data since they chose a specific city
                GpsDataBestMatch = postaldataservicecontext.GetGpsDataByCountryAndCity(model.MyQuickSearch.MySelectedCountryName, strCity).Where(p => p.State_Province == strStateProvince).FirstOrDefault();

                //TO DO get these from the user's IP adress going for ward for now just validate to zero
                
                model.MyQuickSearch.MySelectedLatitude = (GpsDataBestMatch != null) ? GpsDataBestMatch.Latitude: 0 ;
                model.MyQuickSearch.MySelectedLongitude =(GpsDataBestMatch != null) ? GpsDataBestMatch.Longitude: 0;
                


                //ZipOrPostal = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(Model.MyQuickSearch.MySelectedCountryName, strStateProvince);
              //  SelectedCityGpsData = postaldataservicecontext.GetGpsDataSingleByCityCountryAndPostalCode(Model.MyQuickSearch.MySelectedCountryName, strStateProvince, ZipOrPostal);
        
               
            }
            else {
                strCity= "ALL";
            }

          
            string lookingForGender = Extensions.ConvertGenderID(model.MyQuickSearch.MySelectedSeekingGenderID);
            int SelectedCountryId = postaldataservicecontext.GetCountryIdByCountryName(model.MyQuickSearch.MySelectedCountryName);
            double? DistanceFromMe = (model.MyQuickSearch.MySelectedMaxDistanceFromMe.HasValue == true || model.MyQuickSearch.MySelectedMaxDistanceFromMe.GetValueOrDefault() != 0) ? model.MyQuickSearch.MySelectedMaxDistanceFromMe.GetValueOrDefault() : 500;

            //update the model just for housekeeping and so we can make this whole thing reusable
            model.MyQuickSearch.MySelectedCity = strCity;
            model.MyQuickSearch.MySelectedCityStateProvince = strStateProvince;
            model.MyQuickSearch.MySelectedMaxDistanceFromMe = DistanceFromMe;


            //No saving for guests 
            //update Cache
            //depeneding on guest or member 
            //model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID) : CachingFactory.MembersViewModelHelper.UpdateGuestData(model, this.HttpContext);


            //execute the search
            var Profiles = profileRepo.GetQuickSearchMembers(model.MyQuickSearch.MySelectedFromAge, model.MyQuickSearch.MyselectedToAge
                                                      , lookingForGender,
                                                    SelectedCountryId, strCity, strStateProvince, DistanceFromMe.GetValueOrDefault(),model.MyQuickSearch.MySelectedPhotoStatus, model);


            // var Profiles = profileRepo.GetQuickSearch(intAgeFrom, intAgeTo, lookingForGender, CountrID, strCity, strPostalCode);

            // Save QuickSearch 


           // quickSearch = Profiles.ToList();

            Session["QuickSearch"] = Profiles;
            

            //var paginatedSearch = PagerQuickSearch.GetPageableList(quickSearch, Model.MyQuickSearch.mySelectedCurrentPage ?? 1, 25);


            // Return to one location to minimize redundant coding, although it is possible to return to QuickSearch.aspx 
            //for redundancy sake  // we will return only to QuickSearchPager.aspx

            //return RedirectToAction("ViewQuickSearch");
            return RedirectToAction("ViewQuickSearch",  model);
        }


        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [GetChatUsersData]
        public virtual ActionResult ViewQuickSearch(int? page, MembersViewModel Model)
        {

            //TO do use roles or something to figure this
            //hack code for not allowing guests to go past page 2
            if (page > 1 && HttpContext.User.Identity.IsAuthenticated == false)
                return RedirectToAction("Register", "Account");


            //check if we have an active search model if we do use the page and page size that that one lists

            GetQuickSearch();



            var paginatedSearch = PagerQuickSearch.GetPageableList(quickSearch, page ?? 1, QuickSearchPageSize);

            return View("Gallery", paginatedSearch);
        }




        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult QuickSearchContrib(int? page)
        {
            GetQuickSearch();


            return View(quickSearch.ToList());

            //const int pageSize = 2;

            //var paginatedSearch = PagerQuickSearch.GetPageableList(Profiles.ToList(), page ?? 0, pageSize);
            // return View(paginatedSearch);
        }






        [HttpPost]
        [Authorize]
        public JsonResult _AddPeek(string currentitem)
        {

          //  DatingService datingservicecontext = new DatingService().Initialize();
            if (datingservicecontext.AddPeek("", currentitem))
            {
                //send an email updating the peeked at user.
                return Json(true);
            }



            return Json(new object());

        }

        #region "links to other pages and sites"

        public virtual ActionResult AboutUs()
        {

            //    var datingService = new Dating.Server.Data.Services.DatingService().Initialize();
            //var gender = datingService.GetGenders();

            return View();
        }



        public virtual ActionResult AnewluvTwitter()
        {

            
            return Redirect("http://www.twitter.com/anewluvcom"); 
        }

        public virtual ActionResult AnewluvMyspace()
        {

            //    var datingService = new Dating.Server.Data.Services.DatingService().Initialize();
            //var gender = datingService.GetGenders();
            return Redirect("http://wwww.Myspace.com"); 
            
        }

        //change it to likes later
        public virtual ActionResult AnewluvFacebook()
        {

            //    var datingService = new Dating.Server.Data.Services.DatingService().Initialize();
            //var gender = datingService.GetGenders();

            return Redirect("http://www.facebook.com/pages/anewluvcom/206769516012405?sk=wall"); 
        }


        public ActionResult CommunityConnections()
        {
            return View();
        }

#endregion

    }
}
