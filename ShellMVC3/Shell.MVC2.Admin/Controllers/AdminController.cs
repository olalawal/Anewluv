using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Shell.MVC2.Models;

using Shell.MVC2.Helpers;

using System.Security.Principal;


using System.IO;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

//using MvcContrib.Filters;
//using MvcContrib;

//For mango chat
//using Newtonsoft.Json;
//using Ninject;

//using SignalR.Infrastructure;
//using SignalR.Ninject;

//using SignalR;
//using Ninject.Web.Mvc;
//using Ninject.Modules;

//using Shell.MVC2.Models.Chat;
//using Shell.MVC2.Services.Chat;
//using Shell.MVC2.Repositories.Chat;


//using Shell.MVC2.AppFabric;

//using Shell.MVC2.Infastructure.JanRainAuthentication;
//using Shell.MVC2.Filters;



namespace Shell.MVC2.Controllers
{
    public class AccountController : Controller
    {


        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        // public DatingService datingservicecontext;
        private MembersRepository _membersrepository;
        private PostalDataService _postalcodeservice;



        private readonly IChatRepository _repository;
        private readonly IChatService _chatservice;
        //  private readonly IResourceProcessor _resourceProcessor;
        //private readonly IApplicationSettings _settings;

        //TO DO convert the other services to do this as well
        public AccountController(IChatService service, IChatRepository repository, MembersRepository membersrepositiory, PostalDataService postalcodeservice)
        {
            _chatservice = service;
            _repository = repository;
            _membersrepository = membersrepositiory;
            _postalcodeservice = postalcodeservice;

        }


        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            //initialize the dating service here
            // datingservicecontext = new DatingService().Initialize();

            base.Initialize(requestContext);
        }


        #region "JainRain SSSO stuff"

        public ActionResult Authenticate(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {

                var rpx = new Rpx("2589ec8e50274d3e35e594a0d29e9799ec536527", "https://anewluv.rpxnow.com/");
                var response = rpx.AuthInfo(token); var parser = new RpxResponseParser(response);
                if (parser.Status == RpxReponseStatus.Ok)
                {
                    var responseUser = parser.BuildUser();
                    return LogOnDetail(responseUser, "", false, false, responseUser.ProfileID);
                }
            }
            ViewBag.Message = "There was a problem signing you in." + "Verify your credentials and try again.";
            return RedirectToAction("create");
        }

        //ActionResult AuthAndRedirect(string friendlyName)
        //{
        //    string returnUrl = Request["ReturnUrl"];
        //    //SetCookies(userName, friendlyName);

        //    if (!String.IsNullOrEmpty(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //} 


        #endregion


        //TO DO convert to persisitant connection
        #region "Chat configuration cookie and Signal IR setup"


        //Called after authentication and Login is successftul via whatever method
        //4-28-2012 added true screenname field to populate identity feild to store the real screen name so we have it 
        // for server size queries
        public void ConfigureChat(string screenname)
        {
            var chatusername = Extensions.NormalizeScreenName(screenname);
            // var myObject =
            //System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ICoolObject)); 


            //var repository = Bootstrapper.Kernel.Get<IChatRepository>();
            // var chatService = Bootstrapper.Kernel.Get<IChatService>();


            // Try to get the user by identity
            ChatUser user = _repository.GetUserByName(chatusername);

            // No user with this identity
            if (user == null)
            {

                var clientState = new ClientState();
                //In test mode don't do this so we can see how multiple 
                //wndows work 
#if !DISCONECTED  && !DEBUG
                // See if the user is already logged in (via cookie)              
                 clientState = GetClientState(this.HttpContext);
#endif

                user = _repository.GetUserById(clientState.UserId);

                if (user != null)
                {
                    // If they are logged in then assocate the identity
                    user.ScreenName = screenname;
                    user.Name = chatusername;
                    // user.Email = email;
                    //if (!String.IsNullOrEmpty(email))
                    //{
                    //    user.Hash = email.ToMD5();
                    //}
                    _repository.CommitChanges();
                    //context.Response.Redirect("~/", false);
                    this.HttpContext.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    // There's no logged in user so create a new user with the associated credentials
                    user = _chatservice.AddUser(chatusername, screenname, "");
                }
            }
            else
            {

                // Update email and gravatar
                //user.Email = email;
                //if (!String.IsNullOrEmpty(email))
                //{
                //    user.Hash = email.ToMD5();
                //}
                _repository.CommitChanges();
            }

            // Save the cokie state
            var state = JsonConvert.SerializeObject(new { userId = user.Id });
            var cookie = new HttpCookie("jabbr.state", state);
            cookie.Expires = DateTime.Now.AddDays(30);
            this.HttpContext.Response.Cookies.Add(cookie);
            //this.HttpContext.Response.Redirect("~/", false);
            this.HttpContext.ApplicationInstance.CompleteRequest();




        }


        private ClientState GetClientState(HttpContextBase context)
        {
            // New client state
            var jabbrState = GetCookieValue(context, "jabbr.state");

            ClientState clientState = null;

            if (String.IsNullOrEmpty(jabbrState))
            {
                clientState = new ClientState();
            }
            else
            {
                clientState = JsonConvert.DeserializeObject<ClientState>(jabbrState);
            }

            return clientState;
        }

        private string GetCookieValue(HttpContextBase context, string key)
        {
            HttpCookie cookie = context.Request.Cookies[key];
            return cookie != null ? HttpUtility.UrlDecode(cookie.Value) : null;
        }


        #endregion

        #region "Acount Management"

        // **************************************
        // URL: /Account/ Activation
        // **************************************

        //this handles the intial data load , not postback so validation is not done, p.s the string value is a 
        //dummy variable to make the get method difftrent in signatuutr from the POST method - to do fix this hack with and 
        //overload later.
        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [Authorize]
        public virtual ActionResult Account(string profileID)
        {
            //AccountModel model = new AccountModel();
            ////populate values

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //populate default  values
            ViewData["AccountStatus"] = "Change Your Account Settings Here";

            //create a model to store the selected country and other persient values as well
            MembersViewModel model = new MembersViewModel();
            //map the Register default values
            //var  cf = new CachingFactory.MembersViewModelHelper();
            model = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);

            //catch a page refresh
            if (model.Account != null)
            {
                return View(model);
            }

            AccountModel _AccountModel = new AccountModel();

            //get updated profile i.e maybe it changed
            //maybe check DB value first but no
            var myProfile = model.Profile;
            var ProfileData = model.ProfileData;


            //TO DO figure out why the model is being cleared out for some reason
            //or find a way to only update the values not the collections with the exetiontions update
            // SharedRepository sharedrepository = new SharedRepository();
            PostalDataService postaldataservicecontext = new PostalDataService().Initialize();

            //change this to use cache and test later
            //rebuild the values that were not passed using the shared repo
            //_AccountModel.Genders = sharedrepository.GendersSelectList;
            //_AccountModel.Countries = sharedrepository.CountrySelectList();
            //_AccountModel.SecurityQuestions = sharedrepository.SecurityQuestionSelectList;


            // DatingService datingservicecontext = new DatingService().Initialize();

            //var selectList1 = datingservicecontext.GetGenders().Select(x => new SelectListItem
            //     {
            //         Text = x.GenderName,
            //         Value = x.GenderID.ToString()
            //     }).ToList();

            var Viewmodel = new AccountModel
            {
                //ScreenName = myProfile.ScreenName,
                Password = myProfile.Password,
                ConfirmPassword = myProfile.Password,
                BirthDate = myProfile.ProfileData.Birthdate,
                //Gender = myProfile.ProfileData.GenderID.ToString(),

                City = myProfile.ProfileData.City,
                ZipOrPostalCode = myProfile.ProfileData.PostalCode,
                // Country = model

                SecurityAnswer = myProfile.SecurityAnswer,

                // Categories = selectList1,
                // Genders = sharedrepository.GendersSelectList,
                // this is what sets the selected value
                Gender = myProfile.ProfileData.GenderID.ToString(),

                //SecurityQuestions = sharedrepository.SecurityQuestionSelectList,
                SecurityQuestion = myProfile.SecurityQuestionID.ToString(),

                //Countries = sharedrepository.CountrySelectList(),
                Country = model.MyCountryName

            };


            Viewmodel.PostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(model.MyCountryName) == 1) ? true : false;



            //populate values
            model.Account = Viewmodel;
            //not sure this is needed unless cache value is valid 
            //model.ProfileData = myProfile.ProfileData;
            //model.Profile = myProfile
            // ;
            CachingFactory.MembersViewModelHelper.UpdateMemberData(model, _ProfileID);

            //return View(model);
            return View(model);
        }

        // when this action result runs we need to attempt to access the user's profile to see if they have a photo uploaded using the ID
        //that was passed as well as filling out the values in the text box's
        //change this to accept query string values similar to 
        [HttpPost]
        [PassParametersDuringRedirect]
        [ModelStateToTempData]
        [Authorize]
        public virtual ActionResult Account(AccountModel model)
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //create a model to store the selected country and other persient values as well
            MembersViewModel Returnmodel = new MembersViewModel();
            //map the Register default values             
            Returnmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);
            //update the account peice of members view model wih the account data posted back
            Returnmodel.Account = model;

            //TO DO figure out why the model is being cleared out for some reason
            //or find a way to only update the values not the collections with the exetiontions update
            SharedRepository sharedrepository = new SharedRepository();
            PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
            //rebuild the values that were not passed using the shared repo
            // model.Genders = sharedrepository.GendersSelectList;
            //  model.Countries = sharedrepository.CountrySelectList();
            //model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList;
            //we also have to make sure the the postal colde value is correct as well
            //fix this so it is not lost i guess as well
            model.PostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(model.Country) == 1) ? true : false;



            if (ModelState.IsValid)
            {
                //save the code to the model
                //get geocode if postal code status is false
                if (model.PostalCodeStatus == false)
                    model.ZipOrPostalCode = postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(model.Country, model.City);


                //MembershipService.UpdateUser(                
                //  model.Password,model.SecurityQuestion,model.SecurityAnswer ,model.BirthDate,model.Gender ,model.Country ,model.City,model.ZipOrPostalCode);

                //build the membership user thing
                //MembershipUser user = new MembershipUser("AnewLuvMembershipProvider", null, null, null, null, null, true, true, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now, System.DateTime.Now);
                // AnewLuvMembershipProvider dd = new AnewLuvMembershipProvider();
                MembershipUser user = MembershipService.GetUser(Returnmodel.Profile.UserName);

                AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
                //u.securityanswer = model.SecurityAnswer;
                u.birthdate = model.BirthDate;
                u.password = model.Password;
                //u.securityquestion = model.SecurityQuestion;
                u.gender = model.Gender;
                u.country = model.Country;
                u.city = model.City;
                u.ziporpostalcode = model.ZipOrPostalCode;





                MembershipService.UpdateUser(u);

                //restore the membersviewmodel into cache as well
                // ;

                //***
                //also update the model stored in cache with city gender and county before saving
                Returnmodel.MyCountryName = model.Country;
                Returnmodel.MyCity = model.City;
                Returnmodel.MyGenderID = Convert.ToInt32(model.Gender);
                Returnmodel.MyPostalCode = model.ZipOrPostalCode;

                CachingFactory.MembersViewModelHelper.UpdateMemberData(Returnmodel, _ProfileID);

                ViewData["AccountStatus"] = "Your Account settings have been updated !";

                return View(Returnmodel);


            }
            else
            {
                ModelState.AddModelError("", "Please fix your errors");
                return View(Returnmodel);
            }


            //return View(Returnmodel);
        }

        [HttpPost]
        [PassParametersDuringRedirect]
        [ModelStateToTempData]
        [Authorize]
        public virtual ActionResult DeactivateAccount()
        {

            string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
            //create a model to store the selected country and other persient values as well
            MembersViewModel Returnmodel = new MembersViewModel();
            //map the Register default values             
            Returnmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);
            //update the account peice of members view model wih the account data posted back

            return View();
        }


        #endregion






        // **************************************
        // URL: /Account/ Activation
        // **************************************

        //this handles the intial data load , not postback so validation is not done, p.s the string value is a 
        //dummy variable to make the get method difftrent in signatuutr from the POST method - to do fix this hack with and 
        //overload later.
        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult ActivateProfile(string ProfileID, string ActivationCode, bool? PhotoStatus)
        {
            //abandon session if one exists becease we dont want them to clash
            Session.Abandon();


            //re populate the profile ID as if theer is another request it will be lost
            TempData["ProfileID"] = ProfileID;
            TempData["ActivationCode"] = ActivationCode;


            //initialize the viewmodel container and its models
            var activateProfileModel = new ActivateProfileModel();
            var photoViewModel = new PhotoViewModel();

            var model = new ActivateProfileContainerViewModel
            {
                ActivateProfileModel = activateProfileModel,
                ActivateProfilePhotos = photoViewModel,

            };

            if (TempData["ActivateProfileModel"] != null)
            {

                model = (ActivateProfileContainerViewModel)(TempData["ActivateProfileModel"]);
            }
            else
            {
                //populate any values that cane across the wire
                model.ActivateProfileModel.ProfileId = ProfileID;
                model.ActivateProfileModel.ActivationCode = ActivationCode;
            }

            //now if we have blank values 
            // valyes must be in session, remmeber must be in guest since no actciate means no login then get the values from the membersviewmodel     in session  
            if (model.ActivateProfileModel.ProfileId == null || model.ActivateProfileModel.ActivationCode == null)
            {
                //get session ID


                //get the model from session 
                var membersmodel = new MembersViewModel();
                membersmodel = CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);
                //populate the values from here
                activateProfileModel.ProfileId = membersmodel.Register.Email;
                activateProfileModel.ActivationCode = membersmodel.Register.ActivationCode;
            }



            if (ModelState.IsValid)
            {
                //test code here 
                // model.ActivateProfileModel.ProfileId = "ola_lawal@yahoo.comrrtrter";
                //model.ActivateProfileModel.ActivationCode = "/O23CifQTU/o2VsYqc2aIm6vbTQ=";
                // end of debug code


                // end of debug code



                //temporarily set the photo status to true so we do not see the photo upload on the initial load
                //model.ActivateProfileModel.PhotoStatus = datingservicecontext.CheckForGalleryPhotoByProfileID(model.ProfileId);            

                ////test code here , in prod we use the line above 
                //if (PhotoStatus == true)
                //{
                //    ModelState.Clear();  // clear the model state , i.e removes prevalidation

                //    //show the header message 
                //    ViewData["ActivateProfileStatus"] = "Your photo as been uploaded , please click the submit button to complete your activation";
                //}
                if (PhotoStatus == null)
                {
                    model.ActivateProfileModel.PhotoStatus = true;

                }
                else
                {
                    model.ActivateProfileModel.PhotoStatus = false;
                }

                ModelState.Clear();  // clear the model state , i.e removes prevalidation

                //show the header message 
                ViewData["ActivateProfileStatus"] = " Once your profile has been activated you can log on and start viewing other members profiles in detail as well as sending messages to Members that you are interested in.";

                return View(model);
            }
            else
            {
                return View(model);
            }


        }

        // when this action result runs we need to attempt to access the user's profile to see if they have a photo uploaded using the ID
        //that was passed as well as filling out the values in the text box's
        //change this to accept query string values similar to 
        [HttpPost]
        [PassParametersDuringRedirect]
        public virtual ActionResult ActivateProfile(ActivateProfileContainerViewModel model)
        {
            //reset model state
            ModelState.Clear();

            //also create a members view model to store pertinent data i.e persist photos profile ID etc
            var membersmodel = new MembersViewModel();

            //get the macthcing member data using the profile ID/email entered
            membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(model.ActivateProfileModel.ProfileId);

            //verify that user entered correct email before doing anything
            //TO DO add these error messages to resource files
            if (membersmodel == null)
            {
                ModelState.AddModelError("", "There is no registered account with the email address: " + model.ActivateProfileModel.ProfileId + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                //hide the photo view in thsi case
                model.ActivateProfileModel.PhotoStatus = true;
                return View(model);
            }

            //11-1-2011
            //store the valid profileID in appfarbic cache
            CachingFactory.MembersViewModelHelper.SaveProfileIDBySessionID(model.ActivateProfileModel.ProfileId, this.HttpContext);


            //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
            PhotoViewModel photoviewmodel = new PhotoViewModel();
            RegisterModel registerviewmodel = new RegisterModel();
            registerviewmodel.Email = model.ActivateProfileModel.ProfileId;
            registerviewmodel.ActivationCode = model.ActivateProfileModel.ActivationCode;
            photoviewmodel.ProfileID = model.ActivateProfileModel.ProfileId;  //store the profileID i.e email addy into photo viewmodel
            registerviewmodel.RegistrationPhotos = photoviewmodel;  //map it to the empty photo view model
            //add the registermodel to the activate model          
            membersmodel.Register = registerviewmodel;
            //store the members viewmodel
            CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, model.ActivateProfileModel.ProfileId);



            //populate the values of all the form feilds from model if they are empty



            //verify that the modelstate is good beforeing even starting, this so that in case it was a redirecect 
            // from action we display any preivous errors from another associated partial view
            if (ModelState.IsValid != true)
            {
                //show the photo partial view as well since any previous errors will be from here
                model.ActivateProfileModel.PhotoStatus = true;
                //ModelState.Clear();
                return View(model);
            }


            // create temprary instances of both models since the partial view only passes one or the other not both
            //depending on which partial view made the request
            var activateProfileModel = new ActivateProfileModel();
            var photoModel = new PhotoViewModel();

            //5/11/2011
            // add photo view model stuff
            model.ActivateProfilePhotos = photoModel;


            //store temporary variables for this request
            TempData["ProfileID"] = model.ActivateProfileModel.ProfileId;
            TempData["ActivationCode"] = model.ActivateProfileModel.ActivationCode;

            //no need for this sicne we have a route for it
            // HttpRequestBase rd = this.Request;
            // model.ProfileId =  rd.QueryString.Get("ProfileId");
            //model.ActivationCode = rd.QueryString.Get("ActivationCode");

            //after a photo has been uploaded or if no photo needed to be uploaded, attempt to validate the user's information           

            //Server side validation begins here  activation code is validated though the data model , but we still need to validate that the user has photo 
            //*****************************************************************************

            //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error
            if (MembershipService.CheckIfEmailAlreadyExists(model.ActivateProfileModel.ProfileId) == false)
            {
                ModelState.AddModelError("", "Invalid Activation Code or Email Address");
                //hide the photo view in thsi case
                model.ActivateProfileModel.PhotoStatus = true;
                return View(model);
            }
            //else
            //{
            //    ModelState.Clear();  // clear the model state , i.e removes prevalidation
            //}



            //since we got here we can now check if the user has a photo
            //first check to see if there is an email address for the given user on the server add it to the data anotaions validation                 
            //get a value for photo status so we know weather to display uplodad phot dialog or not
            //if the photo status is TRUE then hide the upload photo div
            model.ActivateProfileModel.PhotoStatus = MembershipService.CheckForUploadedPhotoByProfileID(model.ActivateProfileModel.ProfileId);
            if (model.ActivateProfileModel.PhotoStatus == false)
            {
                ModelState.AddModelError("", "Please upload at least one profile photo using the browser below");
                return View(model);

            }
            //else
            //{
            //    ModelState.Clear();  // clear the model state , i.e removes prevalidation
            //}
            //add the error to the model if 


            if (ModelState.IsValid)
            {
                //get username here
                string UserName = MembershipService.GetUserNamebyProfileID(model.ActivateProfileModel.ProfileId);
                string ScreenName = MembershipService.GetScreenNamebyProfileID(model.ActivateProfileModel.ProfileId);
                //build log on model
                //create a new login model
                var logonmodel = new LogOnModel();
                var lostaccountinfomodel = new LostAccountInfoModel();
                var lostActivationcodemodel = new LostActivationCodeModel();
                var _logonmodel = new LogonViewModel
                {
                    LogOnModel = logonmodel,
                    LostAccountInfoModel = lostaccountinfomodel,
                    LostActivationCodeModel = lostActivationcodemodel
                };

                //popualate values
                _logonmodel.LogOnModel.UserName = UserName;
                _logonmodel.LogOnModel.Password = "";  //we do not sent password over wire

                //Check here if the profile was alrady activated, if it is add the error and return the view, 
                //If the profile was actived then the next check is if the mailbox folders were created, if they are not then create them here as well
                //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error



                if (MembershipService.CheckIfProfileisActivated(model.ActivateProfileModel.ProfileId) == true)
                {
                    ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case

                    //ViewData["ActivateProfileStatus"]=
                    return View("LogOn", _logonmodel);
                }
                //activaate profile here
                else
                {
                    MembershipService.ActivateProfile(model.ActivateProfileModel.ProfileId);
                }

                //check if mailbox folders exist, if they dont create em , don't add any error status
                if (MembershipService.CheckIfMailBoxFoldersAreCreated(model.ActivateProfileModel.ProfileId) == true)
                {
                    //ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case                  
                }
                //create the mailbox folders if they do not exist
                else
                {
                    MembershipService.CreateMailBoxFolders(model.ActivateProfileModel.ProfileId);
                }

                //update UI and use pass through validation to send user to thier home ?
                // ViewData["ActivateProfileStatus"] = "Your Profile has been Succesfully Activated, logging you in now";

                ViewData["LoginStatus"] = "Your Profile has been Succesfully Activated, please log in using your password and username";
                //do pass through aViewData["LoginStatus"]uth and redirect to homepage here I think
                //send the email to admin and the user
                var Email = new EmailModel();
                Email.ScreenName = ScreenName;
                Email.ProfileID = model.ActivateProfileModel.ProfileId;
                Email.UserName = UserName;
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileActivated);
                localEmailService.SendEmailMessage(Email);

                //redirect to login
                //create a new login model   
                //no redirect to action so we can save the viewdata for login status update
                return View("LogOn", _logonmodel);

                //return this.RedirectToAction(c => c.LogOn(_logonmodel,false,false)); 

                // return View(model);



            }
            else
            {

                ModelState.AddModelError("", "Please fix your errors");

                return View(model);
            }



        }

        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult ActivateProfileCompleted(ActivateProfileContainerViewModel model)
        {

            //re populate the profile ID as if theer is another request it will be lost
            TempData["ProfileID"] = model.ActivateProfileModel.ProfileId;
            TempData["ActivationCode"] = model.ActivateProfileModel.ActivationCode;
            ////populate the values of all the form feilds from model if they are empty
            ////initialize the viewmodel container and its models
            //var activateProfileModel = new ActivateProfileModel();
            //var photoModel = new PhotoModel();

            //var model = new ActivateProfileContainerViewModel
            //{
            //    ActivateProfileModel = activateProfileModel,
            //    PhotoModel = photoModel,

            //};


            //if (TempData["Model"] != null)
            //{
            //    model = (ActivateProfileContainerViewModel)(TempData["Model"]);
            //}
            //else {

            //    return this.RedirectToAction("ActivateProfile");

            //}

            //temporary solution to fix issue with modelstate not being copied over 
            //******************************************************************
            if (TempData["ModelErrors"] == "True")
            {
                return View(model);
            }



            ModelState.Equals(TempData["ModelState"]);
            ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
            //verify that the modelstate is good beforeing even starting, this so that in case it was a redirecect 
            // from action we display any preivous errors from another associated partial view
            if (ModelState.IsValid != true)
            {
                //restore the model state


                //show the photo partial view as well since any previous errors will be from here
                //  TempData["ModelState"] = ModelState; 
                //  model.ActivateProfileModel.PhotoStatus = true;
                // return this.RedirectToAction(c => c.ActivateProfile(model.ActivateProfileModel.ProfileId,model.ActivateProfileModel.ActivationCode,model));
                return View(model);
            }



            //no need for this sicne we have a route for it
            // HttpRequestBase rd = this.Request;
            // model.ProfileId =  rd.QueryString.Get("ProfileId");
            //model.ActivationCode = rd.QueryString.Get("ActivationCode");

            //after a photo has been uploaded or if no photo needed to be uploaded, attempt to validate the user's information           

            //Server side validation begins here  activation code is validated though the data model , but we still need to validate that the user has photo 
            //*****************************************************************************

            //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error
            if (MembershipService.CheckIfEmailAlreadyExists(model.ActivateProfileModel.ProfileId) == false)
            {
                ModelState.AddModelError("", "Invalid Activation Code or Email Address");
                //hide the photo view in thsi case
                model.ActivateProfileModel.PhotoStatus = true;
                return this.RedirectToAction("ActivateProfile");
            }
            else
            {
                ModelState.Clear();  // clear the model state , i.e removes prevalidation
            }



            //since we got here we can now check if the user has a photo
            //first check to see if there is an email address for the given user on the server add it to the data anotaions validation                 
            //get a value for photo status so we know weather to display uplodad phot dialog or not
            //if the photo status is TRUE then hide the upload photo div
            model.ActivateProfileModel.PhotoStatus = MembershipService.CheckForUploadedPhotoByProfileID(model.ActivateProfileModel.ProfileId);
            if (model.ActivateProfileModel.PhotoStatus == false)
            {
                ModelState.AddModelError("", "Please upload at least one profile photo using the browser below");
                TempData["ModelState"] = ModelState;
                return this.RedirectToAction(c => c.ActivateProfile(model.ActivateProfileModel.ProfileId, model.ActivateProfileModel.ActivationCode, false));

                //return View(model);
            }
            else
            {
                ModelState.Clear();  // clear the model state , i.e removes prevalidation
            }
            //add the error to the model if 


            if (ModelState.IsValid)
            {
                //Check here if the profile was alrady activated, if it is add the error and return the view, 
                //If the profile was actived then the next check is if the mailbox folders were created, if they are not then create them here as well
                //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error
                if (MembershipService.CheckIfProfileisActivated(model.ActivateProfileModel.ProfileId) == true)
                {
                    ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case
                    return this.RedirectToAction(c => c.ActivateProfile(model.ActivateProfileModel.ProfileId, model.ActivateProfileModel.ActivationCode, true));
                    //ViewData["ActivateProfileStatus"]=

                }
                //activate the profile here
                else
                {
                    MembershipService.ActivateProfile(model.ActivateProfileModel.ProfileId);
                }

                //check if mailbox folders exist, if they dont create em , don't add any error status
                if (MembershipService.CheckIfMailBoxFoldersAreCreated(model.ActivateProfileModel.ProfileId) == true)
                {
                    //ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case                  
                }
                //create the mailbox folders if they do not exist
                else
                {
                    MembershipService.CreateMailBoxFolders(model.ActivateProfileModel.ProfileId);
                }

                //update UI and use pass through validation to send user to thier home ?
                ViewData["ActivateProfileStatus"] = "Your Profile has been Succesfully created, logging you in now";
                //do pass through auth and redirect to homepage here I think
                //send the email to admin and the user
                var Email = new EmailModel();

                Email.ProfileID = model.ActivateProfileModel.ProfileId;
                Email.UserName = MembershipService.GetUserNamebyProfileID(model.ActivateProfileModel.ProfileId);
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileActivated);
                localEmailService.SendEmailMessage(Email);


                //return to login page here 
                return this.RedirectToAction("LogOn");

            }
            else
            {
                ModelState.AddModelError("", "Please fix your errors");
                return this.RedirectToAction("ActivateProfile");
            }



        }

        //new code for using telerik file uploader

        //public virtual ActionResult UploadPhoto(ActivateProfileContainerViewModel model, IEnumerable<HttpPostedFileBase> attachments)


        #region "UPload photo actions for edit, register and and activate"

        //This Action should work for logged in and non logged in users
        [ActionName("UploadPhoto")]
        [HttpPost]
        [AcceptParameter(Name = "button", Value = "cancel")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult UploadRegistrationPhoto_Cancel()
        {
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            // var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
            //     CachingFactory.GetProfileIDBySessionId(this.HttpContext);

            //get the model from session 
            //    var membersmodel = new MembersViewModel();           
            //   membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //" Your account was succesfully created , please upload a photo to your account to complete your profile Once your photo has been uploaded please check your email for your activation code."
            ViewData["RegisterStatus"] = "Your Profile has been created however you did not upload a photo. Please check your email for your activation link.. You will be required to upload a photo when you activate your profile";

            PhotoViewModel model = new PhotoViewModel();


            //when  we know the user did hit cancel so we know we are handled in any case.
            //  membersmodel.Register.RegistrationPhotos.PhotoStatus = true;
            model.PhotoStatus = true;

            //This model error thing is to handle the   
            // TempData["ModelErrors"] = "True";
            return View("RegisterSuccess", model);
            // return RedirectToAction("Index", "Home");
        }

        [ActionName("UploadPhoto")]
        [ModelStateToTempData]
        [HttpPost]
        [PassParametersDuringRedirect]
        [AcceptParameter(Name = "button", Value = "submit")]
        public virtual ActionResult UploadRegistrationPhoto(PhotoViewModel model)
        {
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
                CachingFactory.GetProfileIDBySessionId(this.HttpContext);

            //get the model from session 
            var membersmodel = new MembersViewModel();
            membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //check if any photo's exist in either the photoviewmodel or the membersmodel or register model
            //if (model.Photos == null & membersmodel.MyPhotos == null & membersmodel.Register.RegistrationPhotos.Photos == null)
            //no need to verify register model anymore using new method I think
            if (model.Photos == null & membersmodel.MyPhotos == null)
            {
                ModelState.AddModelError("", "No photos were selected! , please select a photo to continue");
            }



            //if there were files to upload handle that case first 
            if (ModelState.IsValid)
            {
                // If we have empty data raise the error and return to the source
                if (model.ProfileID == "")
                {
                    // ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                    ModelState.AddModelError("", "You must enter an email address to Upload a Photo");
                    return View(model);
                }
                else
                {
                    ModelState.Clear();
                }



                if (ModelState.IsValid)
                {

                    PhotoViewModel PhotoVM = new PhotoViewModel();

                    foreach (Photo photo in membersmodel.MyPhotos)
                    {
                        //verify that the file type is an image here

                        //HttpPostedFileBase file = Request.Files["OriginalLocation"];
                        // HttpPostedFileBase hpf = model.PhotoModel.ImageUploaded;
                        // var photomodel = new PhotoModel();
                        //byte[] fileContent = new byte[hpf.ContentLength];
                        // hpf.InputStream.Read(fileContent, 0, hpf.ContentLength);
                        if (PhotoVM.AddPhoto(photo, membersmodel.Profile.ProfileID))
                        {
                            model.PhotoStatus = true;
                        }
                        //if we were unable to upload photo don't update the photo status for registration photo viewmodel
                        else
                        {
                            //do nothing since defualt is null or false
                        }
                        //get other values from form         
                        //Save file here
                        //if the photo was 

                    }

                    //send  the emails for photos uploaded out
                    var Email = new EmailModel();

                    Email.ProfileID = membersmodel.Profile.ProfileID;
                    Email.UserName = membersmodel.Profile.UserName;
                    //declae a new instance of LocalEmailService
                    var localEmailService = new LocalEmailService();
                    Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfilePhotoUploaded);
                    localEmailService.SendEmailMessage(Email);
                    //emails are sent yay


                    // return this.RedirectToAction(c => c.ActivateProfileCompleted(modelRebuilt));
                    ModelState.Clear();
                    //" Your account was succesfully created , please upload a photo to your account to complete your profile Once your photo has been uploaded please check your email for your activation code."
                    ViewData["RegisterStatus"] = "Your Profile has been created, Please check your email for your activation link..";
                    //This model error thing is to handle the   
                    // TempData["ModelErrors"] = "True";
                    return View("RegisterSuccess", model);
                    // return RedirectToAction("Index", "Home");

                }
                else
                {
                    return Content("");

                    // return this.RedirectToAction("Account", "ActivateProfileCompleted");
                    // return this.View("AddTasks", modelRebuilt); 
                }
            }
            else
            {
                //This model error thing is to handle the   
                TempData["ModelErrors"] = "True";
                //  return this.RedirectToAction(c => c.ActivateProfileCompleted(modelRebuilt));

                return this.RedirectToAction(c => c.RegisterSuccess(model));
            }








        }

        //Modified code to work for both logged in and non logged in members
        //10-9-2011
        [ActionName("UploadPhotoActivate")]
        [HttpPost]
        [AcceptParameter(Name = "button", Value = "cancel")]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public ActionResult UploadActivationPhoto_Cancel()
        {
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
                CachingFactory.GetProfileIDBySessionId(this.HttpContext);

            //get the model from session 
            var model = new MembersViewModel();
            var photoviewmodel = new PhotoViewModel();

            model = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;


            //rebuild the activate profile model so we can repopulate values lost on postback
            //initialize the viewmodel container and its models
            var activateProfileModel = new ActivateProfileModel();

            //restore the values from tempdata
            //store temporary variables for this request
            activateProfileModel.ProfileId = model.Register.Email;
            activateProfileModel.ActivationCode = model.Register.ActivationCode;
            var modelRebuilt = new ActivateProfileContainerViewModel
            {
                ActivateProfileModel = activateProfileModel,
                ActivateProfilePhotos = photoviewmodel,
            };


            //" Your account was succesfully created , please upload a photo to your account to complete your profile Once your photo has been uploaded please check your email for your activation code."
            ViewData["ActivateProfileStatus"] = "You must upload a photo to activate your profile!";

            //when  we know the user did hit cancel so we know we are handled in any case.
            activateProfileModel.PhotoStatus = false;

            //store model in tempdata for now, redirect to action not passing model for some reason
            TempData["ActivateProfileModel"] = modelRebuilt;


            return this.RedirectToAction(c => c.ActivateProfile(modelRebuilt));
        }

        //Modified code to work for both logged in and non logged in members
        //10-9-2011
        [ActionName("UploadPhotoActivate")]
        [ModelStateToTempData]
        [HttpPost]
        [PassParametersDuringRedirect]
        [AcceptParameter(Name = "button", Value = "submit")]
        public virtual ActionResult UploadActivationPhoto(PhotoViewModel model)
        {
            //11-1-2011 this is an out of logged in action as well so using session to tie 
            var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
                CachingFactory.GetProfileIDBySessionId(this.HttpContext);
            //get the model from session 
            var membersmodel = new MembersViewModel();
            //updated 11-1-2011 only members can active so no point on using guest stuff here
            membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //rebuild the activate profile model so we can repopulate values lost on postback
            //initialize the viewmodel container and its models
            var activateProfileModel = new ActivateProfileModel();

            //restore the values from tempdata
            //store temporary variables for this request
            activateProfileModel.ProfileId = membersmodel.Profile.ProfileID;
            activateProfileModel.ActivationCode = membersmodel.Profile.ActivationCode;


            var modelRebuilt = new ActivateProfileContainerViewModel
            {
                ActivateProfileModel = activateProfileModel,
                ActivateProfilePhotos = model,
            };

            //store model in tempdata for now, redirect to action not passing model for some reason
            TempData["ActivateProfileModel"] = modelRebuilt;


            //check if any photo's exist in either the photoviewmodel or the membersmodel or register model



            if (model.Photos == null & membersmodel.MyPhotos == null)
            {
                ModelState.AddModelError("", "No photos were selected! , please select a photo to continue");
            }






            //if there were files to upload handle that case first 
            if (ModelState.IsValid)
            {
                // If we have empty data raise the error and return to the source
                if (model.ProfileID == "")
                {
                    // ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                    ModelState.AddModelError("", "You must enter an email address to Upload a Photo");
                    return View(model);
                }
                else
                {
                    ModelState.Clear();
                }



                //now if we are here and no errors continue other return errors to view
                if (ModelState.IsValid)
                {

                    //TO DO move this add photo to its own repostiorey 
                    PhotoViewModel PhotoVM = new PhotoViewModel();

                    foreach (Photo photo in membersmodel.MyPhotos)
                    {
                        //verify that the file type is an image here

                        //HttpPostedFileBase file = Request.Files["OriginalLocation"];
                        // HttpPostedFileBase hpf = model.PhotoModel.ImageUploaded;
                        // var photomodel = new PhotoModel();
                        //byte[] fileContent = new byte[hpf.ContentLength];
                        // hpf.InputStream.Read(fileContent, 0, hpf.ContentLength);
                        if (PhotoVM.AddPhoto(photo, activateProfileModel.ProfileId))
                        {
                            //   membersmodel.Register.RegistrationPhotos.PhotoStatus = true;
                            modelRebuilt.ActivateProfilePhotos.PhotoStatus = true;

                        }
                        //if we were unable to upload photo don't update the photo status for registration photo viewmodel
                        else
                        {
                            //do nothing since defualt is null or false
                        }
                        //get other values from form         
                        //Save file here
                        //if the photo was 

                    }



                    //send  the emails for photos uploaded out
                    var Email = new EmailModel();

                    Email.ProfileID = membersmodel.Profile.ProfileID;
                    Email.UserName = membersmodel.Profile.UserName;
                    //declae a new instance of LocalEmailService
                    var localEmailService = new LocalEmailService();
                    Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfilePhotoUploaded);
                    localEmailService.SendEmailMessage(Email);
                    //emails are sent yay



                    ModelState.Clear();
                    //" Your account was succesfully created , please upload a photo to your account to complete your profile Once your photo has been uploaded please check your email for your activation code."
                    // ViewData["RegisterStatus"] = "Your Profile has been created, Please check your email for your activation link..";
                    //This model error thing is to handle the   
                    // TempData["ModelErrors"] = "True";


                    return this.RedirectToAction(c => c.ActivateProfile(modelRebuilt));
                    // return this.View("AddTasks", modelRebuilt); 

                }
                else
                {
                    return Content("");

                    // return this.RedirectToAction("Account", "ActivateProfileCompleted");
                    // return this.View("AddTasks", modelRebuilt); 
                }
            }
            else
            {
                //  //This model error thing is to handle the   
                //  TempData["ModelErrors"] = "True";
                //    //  return this.RedirectToAction(c => c.ActivateProfileCompleted(modelRebuilt));

                //  return this.RedirectToAction(c => c.ActivateProfile(modelRebuilt));



                //This model error thing is to handle the   
                TempData["ModelErrors"] = "True";
                return this.RedirectToAction(c => c.ActivateProfile(modelRebuilt));

            }








        }




        #endregion

        //[HttpPost]
        //  public FileUploadJsonResult AjaxUpload(HttpPostedFileBase file)          
        //  {              // TODO: Add your business logic here and/or save the file             
        //      //System.Threading.Thread.Sleep(2000);
        //      // Simulate a long running upload               
        //      // Return JSON             
        //      return new FileUploadJsonResult
        //      { Data = new { message = string.Format("{0} uploaded successfully.", System.IO.Path.GetFileName(file.FileName)) } };     
        //  }  




        //we can later add added user name and password for single sign on purposes
        //else the only reason we have code here is to handle a direct lost password case
        //i.e sonone lost there pass word and they call logon with action result model and password


        //overload for this logon metho that just allows members to login from anywhere
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [HttpGet]
        public virtual ActionResult EmptyLogOn()
        {

            //create a new empty model
            LogonViewModel model = new LogonViewModel();

            //initialize the viewmodel container and its models
            var logOnModel = new LogOnModel();
            var lostAccountInfoModel = new LostAccountInfoModel();
            //bad thing here the view crashes if the lostpassword is null, fix this later on the model and view
            //TO DO fix this bug          
            lostAccountInfoModel.LostPassword = false;
            if (model.LogOnModel == null)
            {
                model.LostAccountInfoModel = lostAccountInfoModel;
                model.LogOnModel = logOnModel;
            }


            return this.RedirectToAction(c => c.LogOn(model, false, false, false, ""));
        }


        //
        // GET: /Account/LogOn
        //this logon is for redirection from a URL or email etc



        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        [HttpGet]
        public virtual ActionResult LogOn(LogonViewModel model, bool? LostPassword, bool? LostActivationCode, bool? LostAccountInfoSent, string ProfileID)
        {


            //initialize the viewmodel container and its models
            var logOnModel = new LogOnModel();
            var lostAccountInfoModel = new LostAccountInfoModel();
            var lostActivationCodeModel = new LostActivationCodeModel();

            //create reference to the shared repository or dating service context that buils select lists
            SharedRepository sharedrepository = new SharedRepository();

            if (model.LogOnModel == null)
            {
                model.LostAccountInfoModel = lostAccountInfoModel;
                model.LostActivationCodeModel = lostActivationCodeModel;
                model.LogOnModel = logOnModel;
            }


            if (TempData["Model"] != null)
            {
                model = (LogonViewModel)(TempData["Model"]);
                TempData["Model"] = model;
            }
            else
            {
                //store the full or empty model in tempdata regardless
                TempData["Model"] = model;
            }



            if (ModelState.IsValid)
            {

                //default case 
                if (LostPassword == null && LostActivationCode == null)
                {
                    //set the value of username to profileID if its not empty
                    if (ProfileID != null) model.LogOnModel.UserName = ProfileID;
                    model.LostAccountInfoModel.LostPassword = false; // hide the lost password view  
                    model.LostActivationCodeModel.LostActivationCode = false;
                    //show the header message 
                    ViewData["LoginStatus"] = "Please Log in to your account";
                    ModelState.Clear();
                    return View(model);
                }

                else  //handle cases wehere user is tryig to recover info
                {
                    if (LostPassword == true && LostAccountInfoSent == null)
                    {
                        //populate the select list for secuity questions from the Data service

                        // model.LostAccountInfoModel.SecurityQuestions =  sharedrepository.SecurityQuestionSelectList;

                        model.LostAccountInfoModel.LostPassword = true;  //show the recover pasword view
                        model.LostActivationCodeModel.LostActivationCode = false;  //hide activation code recovery
                        ViewData["LoginStatus"] = "Please Enter your valid registration email , then hit submit. An email will be sent that will assist you in reseting your account";
                        // ModelState.AddModelError("","Please Enter your email address and you will be emailed a link that you can use to reset your password");
                        ModelState.Clear();
                        return View(model);
                    }
                    if (LostPassword == true && LostAccountInfoSent == true)
                    {
                        model.LostAccountInfoModel.LostPassword = false;  //show  the login view
                        model.LostActivationCodeModel.LostActivationCode = false;  //hide activation code recovery
                        ViewData["LoginStatus"] = "Your account has been succesfully reset, Please Check your email account for an email from AnewLuv.com, then click the 'recover account' link";
                        // ModelState.AddModelError("", "An email has been sent to your registration email account , please click check your email and click on the link provided to reset your password");
                        ModelState.Clear();
                        return View(model);
                    }
                    if (LostPassword == true && LostAccountInfoSent == false)
                    {
                        model.LostAccountInfoModel.LostPassword = false;  //show  the login view
                        model.LostActivationCodeModel.LostActivationCode = false;  //hide activation code recovery
                        ViewData["LoginStatus"] = "Your account recovery information was invalid ! , please verify via email that you have created an account on AnewLuv.com";
                        // ModelState.AddModelError("", "An email has been sent to your registration email account , please click check your email and click on the link provided to reset your password");
                        // ModelState.Clear();
                        model.LogOnModel = null;
                        return View(model);
                    }
                    //checks for activaiton code reset as well
                    //*************************************************************************************
                    if (LostActivationCode == true && LostAccountInfoSent == null)
                    {

                        model.LostActivationCodeModel.LostActivationCode = true;  //show the recover pasword view  
                        model.LostAccountInfoModel.LostPassword = false;  //hide login view
                        ViewData["LoginStatus"] = "Please Enter your valid registration email , then hit submit. Your activation code will then be resent to your email address";
                        // ModelState.AddModelError("","Please Enter your email address and you will be emailed a link that you can use to reset your password");
                        ModelState.Clear();
                        return View(model);
                    }
                    if (LostActivationCode == true && LostAccountInfoSent == true)
                    {
                        //set both activation resend and lost password model to false so only login screen shows
                        model.LostActivationCodeModel.LostActivationCode = false;
                        model.LostAccountInfoModel.LostPassword = false;
                        ViewData["LoginStatus"] = "Your Activation code has been resent to your email address, Please Check your email account for an email from AnewLuv.com, then click the 'Activate Profile' link";
                        // ModelState.AddModelError("", "An email has been sent to your registration email account , please click check your email and click on the link provided to reset your password");
                        ModelState.Clear();
                        return View(model);

                    }
                    if (LostActivationCode == true && LostAccountInfoSent == false)
                    {
                        model.LostActivationCodeModel.LostActivationCode = false;  //show  the login view
                        model.LostAccountInfoModel.LostPassword = false; //hide lost password view as well
                        ViewData["LoginStatus"] = "Your Activation code recovery information was invalid ! , please verify via email that you have created an account on AnewLuv.com";
                        // ModelState.AddModelError("", "An email has been sent to your registration email account , please click check your email and click on the link provided to reset your password");
                        // ModelState.Clear();
                        model.LogOnModel = null;
                        return View(model);

                    }



                }

                ModelState.Clear();  // clear the model state , i.e removes prevalidation
            }
            else
            {
                return View(model);
            }


            return View(model);
        }

        //Actually handles the logon or the other stuff for recovery
        [HttpPost]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult LogOnDetail(LogOnModel model, string returnUrl, bool? LostPassword, bool? LostActivationCode, string ProfileID)
        {

            var _model = new LogonViewModel();
            //var lostAccountInfoModel = new LostAccountInfoModel();


            if (TempData["Model"] != null)
            {
                _model = (LogonViewModel)(TempData["Model"]);
                TempData["Model"] = _model;
            }


            //redirect if password was lost , not sure if this was needed at all
            if (LostPassword == true)
            {
                return this.RedirectToAction(c => c.LogOn(_model, true, false, true, ""));
            }

            if (LostActivationCode == true)
            {
                return this.RedirectToAction(c => c.LogOn(_model, false, true, true, ""));
            }


            //build the portion of the LostPassword and lost activation code model  in case we need to re-direct
            var lostAccountInfoModel = new LostAccountInfoModel();
            var lostActivationCodeModel = new LostActivationCodeModel();
            lostAccountInfoModel.LostPassword = false; //set lost password to false so that the right partial view will be displayed, if login is uncessful

            if (_model.LostAccountInfoModel == null)
            {
                _model.LostAccountInfoModel = lostAccountInfoModel;
            }
            if (_model.LostActivationCodeModel == null)
            {
                _model.LostActivationCodeModel = lostActivationCodeModel;
            }



            if (ModelState.IsValid)
            {

                //check if the user has activated thier account ?


                // ... use a diffeent validatione method for debug ide no need for password
                //othere replase types we would need full password and SHA1 validation.
                // ... use a diffeent validatione method for debug ide no need for password
                //othere replase types we would need full password and SHA1 validation.
#if (DEBUG || DEVELOPMENT)
                Console.WriteLine("Debug version");
                if (MembershipService.ValidateUser(model.UserName))
                {
                    //added 3/7/2012
                    //setup the chat thing to test in disc mode and normal mode
                    //3-28-2012 configure chat using screen name 
                    //4-28-2012 confiigured the chat deal to trim out leading spaces in the screen name
                    //adding a new feild to members model to hold the cleaned up screen name and that is what is displayed to chat
                    ConfigureChat(MembershipService.GetScreenNamebyUserName((model.UserName)));
#elif RELEASE
                if (MembershipService.ValidateUser(model.UserName, model.Password.Trim())) {
                  //added 3/7/2012
                     //setup the chat thing to test in disc mode and normal mode
                     ConfigureChat(MembershipService.GetScreenNamebyUserName(model.UserName));
#elif DISCONECTED
                    //no database connection for disconected testing, username still is needed 
                  if (model.UserName != null) {
                  //added 3/7/2012
                     //setup the chat thing to test in disc mode and normal mode
                     ConfigureChat(model.UserName);
                              
#endif


                    //close out previous session
                    Session.Abandon();

                    //create the ticket
                    int timeout = model.RememberMe ? 525600 : 30;
                    // Timeout in minutes, 525600 = 365 days. 
                    var ticket = new FormsAuthenticationTicket(model.UserName, model.RememberMe, timeout);

                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                    // Not my line 
                    Response.Cookies.Add(cookie);
                    //My Line 
                    // HttpContext.Current.Response.Cookies.Add(cookie); 
                    //sign the user in here
                    FormsService.SignIn(model.UserName, model.RememberMe);




                    //9-29-2011 added after we switched to app fabric since we need a key better than user name to
                    // to use to key each person's data

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("MembersHome", "Members");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect, Have you activated your account? if you have activated please use the Recover Account link on this page to create a new password");
                }
            }

            // If we got this far, something failed, redisplay form
            return View("LogOn", _model);
        }


        [HttpPost]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult LostAccountInfo(LostAccountInfoModel model, string var)
        {


            //temporaril store the lostaccountinfo model stuff
            LogonViewModel _model = new LogonViewModel();

            //get the previous tempdata model data from tempdata
            if (TempData["Model"] != null)
            {
                _model = (LogonViewModel)(TempData["Model"]);
                // TempData["Model"] = model;
            }

            //add the lostAccountInfo model stuff to the perssiatant view model  
            LostActivationCodeModel lostActivationCodeModel = new LostActivationCodeModel();
            if (_model != null)
            {
                _model.LostAccountInfoModel = model;
                _model.LostActivationCodeModel = lostActivationCodeModel;

            }

            //repopulate security questions in case we need it
            //create reference to the shared repository or dating service context that buils select lists
            SharedRepository sharedrepository = new SharedRepository();
            // _model.LostAccountInfoModel.SecurityQuestions   = sharedrepository.SecurityQuestionSelectList; 


            //server side validations since profile ID is needed and is not in the model perse
            //using (DatingService dbContext = new DatingService())
            //{

            //    if (dbContext.ValidateSecurityAnswerIsCorrect(model.Email, Int32.Parse(model.SecurityQuestion),model.SecurityAnswer) == "")
            //     ModelState.AddModelError("You Security Question or Answer is invalid, please note after 3 tries your account will be locked out","")  
            //        ;
            //    //row = _repo.GetAdminLoginByUsername(valueAsString);
            //}



            //add the email model code here too            
            //open up the change passw

            if (ModelState.IsValid)
            {

                string newpassword = MembershipService.ResetPassword(model.Email);

                if (newpassword == "")
                {
                    ModelState.AddModelError("", "You Email address is invalid, please note after 3 tries you will be locked out of the system");
                    //build the model here ?
                    //before this reset the the value of the lostaccountinfo lostpassword boolean   so it is not showing email view on postbacks from the logonview
                    _model.LostAccountInfoModel.LostPassword = true;



                    //return View("LogOn", model,{true,false});
                    return View("LogOn", _model);


                }
                else
                {
                    //send the email as well

                    var Email = new EmailModel();

                    //set pasword
                    Email.Password = newpassword;

                    using (DatingService context = new DatingService())
                    {
                        ProfileData tempprofiledata = new ProfileData();
                        tempprofiledata = context.GetProfileDataByProfileID(model.Email);
                        Email.ScreenName = tempprofiledata.profile.ScreenName;
                        Email.ProfileID = model.Email;
                        Email.UserName = tempprofiledata.profile.UserName;

                    }


                    //declae a new instance of LocalEmailService
                    var localEmailService = new LocalEmailService();
                    Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileRecovered);
                    localEmailService.SendEmailMessage(Email);

                    //before this reset the the value of the lostaccountinfo lostpassword boolean   so it is not showing email view on postbacks from the logonview
                    ViewData["LoginStatus"] = "Your password has been succesfully reset, Please Check your email account for an email from AnewLuv.com, then click the 'recover account' link";
                    _model.LostAccountInfoModel.LostPassword = false;
                    return View("LogOn", _model);
                }

            }

            ViewData["LoginStatus"] = "Your account recovery information was invalid ! , please verify via email that you have created an account on AnewLuv.com";
            //before this reset the the value of the lostaccountinfo lostpassword boolean   so it is not showing email view on postbacks from the logonview
            _model.LostAccountInfoModel.LostPassword = true;
            //reset temp data with the new updated model 
            TempData["Model"] = _model;
            return View("LogOn", _model);
            //return this.RedirectToAction(c => c.LogOn(model,true, false,""));

        }

        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult LostAccountInfo(LogonViewModel model)
        {

            //build the model here ?
            if (TempData["Model"] != null)
            {
                model = (LogonViewModel)(TempData["Model"]);
                TempData["Model"] = model;
            }



            return View();

        }


        [HttpPost]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult LostActivationCode(LostActivationCodeModel model, string var)
        {


            //temporaril store the LostActivationCode model stuff
            LogonViewModel _model = new LogonViewModel();

            //get the previous tempdata model data from tempdata
            if (TempData["Model"] != null)
            {
                _model = (LogonViewModel)(TempData["Model"]);
                // TempData["Model"] = model;
            }

            //add the LostActivationCode model and logon model stuff to the perssiatant view model  
            LostAccountInfoModel lostAccountInfoModel = new LostAccountInfoModel();
            if (_model != null)
            {
                _model.LostActivationCodeModel = model;
                _model.LostAccountInfoModel = lostAccountInfoModel;
            }



            //add the email model code here too            
            //open up the change passw

            if (ModelState.IsValid)
            {
                ProfileData profiledata = new ProfileData();
                profiledata = _membersrepository.GetProfileDataByProfileID(model.Email);


                if (profiledata == null)
                {
                    ModelState.AddModelError("", "You Email address is invalid, please note after 3 tries you will be locked out of the system");
                    //build the model here ?
                    //before this reset the the value of the LostActivationCode lostpassword boolean   so it is not showing email view on postbacks from the logonview
                    _model.LostActivationCodeModel.LostActivationCode = true;


                    //return View("LogOn", model,{true,false});
                    return View("LogOn", _model);


                }
                else
                {
                    //send the email as well

                    var Email = new EmailModel();
                    Email.ScreenName = profiledata.profile.ScreenName;
                    Email.ProfileID = model.Email;
                    Email.UserName = profiledata.profile.UserName;
                    Email.ActivationCode = profiledata.profile.ActivationCode;



                    //declae a new instance of LocalEmailService
                    var localEmailService = new LocalEmailService();
                    Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileActivationCodeRecoverd);
                    localEmailService.SendEmailMessage(Email);

                    //before this reset the the value of the LostActivationCode lostpassword boolean   so it is not showing email view on postbacks from the logonview
                    ViewData["LoginStatus"] = "Your Activation Code has been resent to your the email address you provided, Please Check your email account for an email from AnewLuv.com, then click the 'Activate Profile' link";
                    _model.LostActivationCodeModel.LostActivationCode = false;
                    _model.LostAccountInfoModel.LostPassword = false;

                    return View("LogOn", _model);
                }

            }

            ViewData["LoginStatus"] = "Your ActivationCode recovery information was invalid ! , please verify via email that you have created an account on AnewLuv.com";
            //before this reset the the value of the LostActivationCode lostpassword boolean   so it is not showing email view on postbacks from the logonview
            _model.LostActivationCodeModel.LostActivationCode = true;
            //reset temp data with the new updated model 
            TempData["Model"] = _model;
            return View("LogOn", _model);
            //return this.RedirectToAction(c => c.LogOn(model,true, false,""));

        }

        [HttpGet]
        [ModelStateToTempData]
        [PassParametersDuringRedirect]
        public virtual ActionResult LostActivationCode(LogonViewModel model)
        {

            //build the model here ?
            if (TempData["Model"] != null)
            {
                model = (LogonViewModel)(TempData["Model"]);
                TempData["Model"] = model;
            }



            return View();

        }



        //
        // GET: /Account/LogOff

        //updayted 10-9-2011 to handle the clearing of Appfabric cache.
        //TO DO thing about using a composite key of ProfielID and current session to perisist data so if the 
        //Session ID peice matches when a user logs in you can retive the currnt sessions data if session was not accidentaly killed
        public ActionResult LogOff()
        {

            //update user log time here
            using (DatingService context = new DatingService())
            {
                string _ProfileID = CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name);
                // var  contextsession = this.HttpContext;              
                //clear all models from appfabric cahce
                CachingFactory.ClearCurrentSessionCache(_ProfileID, this.HttpContext);
                //get the profile ID i.e email from the user context username
                // string profileID = context.GetProfileIdbyUsername(contextsession.User.Identity.Name);
                //now you can log it in database
                context.UpdateUserLogoutTime(this.HttpContext.User.Identity.Name, this.HttpContext.Session.SessionID);
            }


            FormsAuthentication.SignOut();



            return RedirectToAction("SplashPage", "Home");
        }

        //
        // GET: /Account/Register

        [HttpGet]
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        public ActionResult Register()
        {
            // ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            // return RedirectToAction("Registration","Account");



            //catch a page refresh
            //if (registermodel != null)
            // {
            //     return View(registermodel);
            //}

            //generate  the Register model
            // RegisterModel RegisterModel = new RegisterModel();
            //clear current session
            // Session.Abandon();




            //create a model to store the selected country and other persient values as well
            MembersViewModel model = new MembersViewModel();
            //map the Register default values
            ;
            //9/29/2011 uppdated to use guest mapper always
            //Only guests can register keep that in mind so use geuset cache always
            model = CachingFactory.MembersViewModelHelper.GetGuestData(this.HttpContext);


            //SharedRepository sharedrepository = new SharedRepository();
            // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();


            //rebuild the values that were not passed using the shared repo
            // model.Register.Genders = sharedrepository.GendersSelectList;
            //  model.Register.Countries  = sharedrepository.CountrySelectList();
            //  model.Register.SecurityQuestions  = sharedrepository.SecurityQuestionSelectList();



            //TO DO set based on IP
            //set the default country based on IP later for now set to USA
            model.Register.Country = model.MyQuickSearch.MySelectedCountryName;

            if (model.MyQuickSearch.geocodeddata == true)
            {
                model.Register.City = model.MyQuickSearch.MySelectedCity;
                model.Register.PostalCodeStatus = model.MyQuickSearch.MySelectedPostalCodeStatus;
                //TO DO build a new sproc to get city by region and one by lattitude long
                //    model.Register.ZipOrPostalCode = (model.MyQuickSearch.MySelectedPostalCodeStatus == true) ?
                //       _postalcodeservice.GetGeoPostalCodebyCountryNameAndCity(model.MyQuickSearch.MySelectedCountryName, model.MyQuickSearch.MySelectedCity) : "";
                model.Register.Gender = Extensions.ConvertGenderID(model.MyQuickSearch.MySelectedIamGenderID);
            }



            //add the selected country to the members viewmodel , since this is not a search


            //add model to the session here if it does not exist

            CachingFactory.MembersViewModelHelper.UpdateGuestData(model, this.HttpContext);


            return View(model);
        }

        //
        // POST: /Account/Register

        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {



            //merge the new model data with the old since we lose all our collections
            //ie coutnries list etc


            if (ModelState.IsValid)
            {

                //generate the activation code here
                //guid for the activation code
                Guid g;
                // Create and display the value of two GUIDs.
                g = Guid.NewGuid();
                string activationcode = g.ToString();

                MembershipCreateStatus createStatus = MembershipService.CreateUser(
                model.UserName,
                model.Password,
                model.Email,
                    // model.SecurityQuestion,
                    // model.SecurityAnswer,
                model.BirthDate,
                model.Gender,
                model.Country,
                model.City,
                model.ScreenName,
                model.ZipOrPostalCode, activationcode);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //send the mails out 
                    var Email = new EmailModel();

                    Email.ScreenName = model.ScreenName;
                    Email.ProfileID = model.Email;
                    Email.UserName = model.UserName;
                    Email.ActivationCode = activationcode;
                    //declae a new instance of LocalEmailService
                    var localEmailService = new LocalEmailService();
                    Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileCreated);
                    localEmailService.SendEmailMessage(Email);
                    //emails are sent yay

                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);

                    //create a model to store the selected country and other persient values as well
                    MembersViewModel membersmodel = new MembersViewModel();

                    //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
                    PhotoViewModel photoviewmodel = new PhotoViewModel();
                    photoviewmodel.ProfileID = model.Email;  //store the profileID i.e email addy into photo viewmodel
                    model.RegistrationPhotos = photoviewmodel;  //map it to the empty photo view model

                    //map the membersviewmodel to default values or what is stored in  stored in session
                    membersmodel = _membersrepository.GetMemberDataByProfileID(model.Email);
                    //replace the registration viewmodel in memory with the one passed to this model
                    membersmodel.Register = model;

                    //restore the guest cache membersviewmodel into session
                    //att this point we don't need the guest cache anymore since they are registered ? removed
                    CachingFactory.MembersViewModelHelper.RemoveGuestData(this.HttpContext);
                    //CachingFactory.MembersViewModelHelper.UpdateGuestData(membersmodel, this.HttpContext);

                    //11-1-2011
                    //store the valid profileID in appfarbic cache
                    //so it can be rtrived latrer if we do not have access to user itendity.name
                    CachingFactory.MembersViewModelHelper.SaveProfileIDBySessionID(membersmodel.ProfileData.ProfileID, this.HttpContext);

                    return View("RegisterSuccess", membersmodel.Register.RegistrationPhotos);

                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;

            //since the register page excpects a Membersviewmodel make sure it gets one
            MembersViewModel Returnmodel = new MembersViewModel();

            //TO DO figure out why the model is being cleared out for some reason
            //or find a way to only update the values not the collections with the exetiontions update
            // SharedRepository sharedrepository = new SharedRepository();
            PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
            model.PostalCodeStatus = (postaldataservicecontext.GetCountry_PostalCodeStatusByCountryName(model.Country) == 1) ? true : false;


            //rebuild the values that were not passed using the shared repo
            //model.Genders = sharedrepository.GendersSelectList;
            // model.Countries = sharedrepository.CountrySelectList();
            // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList;
            //we also have to make sure the the postal colde value is correct as well
            //fix this so it is not lost i guess as well         
            //map the registration default values
            //model = membersrepository.GetMembersViewModel(this.HttpContext);

            Returnmodel.Register = model;



            return View(Returnmodel);


        }


        //handles photo upload and completion of user account
        [ModelStateToTempData]
        [TempDataToViewData]
        [PassParametersDuringRedirect]
        [HttpGet]
        public ActionResult RegisterSuccess(PhotoViewModel model)
        {

            //CachingFactory.MembersViewModelHelper.Add (model, this.HttpContext);
            Session.Abandon();
            return View(model);


        }


        //



        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
