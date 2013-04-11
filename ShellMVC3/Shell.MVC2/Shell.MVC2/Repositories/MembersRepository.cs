using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;



using System.Text;

using System.Web.Mvc;

using Common;


using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using MvcContrib.Pagination;

using Omu.Awesome.Core;


using Shell.MVC2.AppFabric;

using System.Security.Principal;
using Ninject.Web.Mvc;
using Ninject;

using Shell.MVC2.Infastructure;
using System.Net;
using System.IO;
using Dating.Server.Data.ViewModels;


namespace Shell.MVC2.Models
{

    public partial class MembersRepository
    {


        private  DatingService datingservicecontext;  //= new DatingService().Initialize();  
        private  AnewLuvFTSEntities db; // = new AnewLuvFTSEntities();
        private  PostalData2Entities postaldb; //= new PostalData2Entities();
        private PostalDataService postaldataservicecontext;  //= new PostalDataService().Initialize();

        //TO DO
      

       public MembersRepository()
       {
           IKernel kernel = new StandardKernel();
          //Get these initalized
           datingservicecontext = kernel.Get<DatingService>(); 
           postaldataservicecontext = kernel.Get<PostalDataService>();
           db =  kernel.Get<AnewLuvFTSEntities>();
           postaldb = kernel.Get<PostalData2Entities>();
        }

   

        //session Key is username for now until we implement state server
        #region "Cookie and session strong handlers here for persisting the members data"


//        // We're using HttpContextBase to allow access to cookies. 
//        public static string GetMembersViewModelID(HttpContextBase context)
//        {
//            //means a new login here
//            if (context.Session[MemberSessionKey] == null)
//            {
//                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
//                {

//                    context.Session[MemberSessionKey] = context.User.Identity.Name;

//                    //this might not be needed but we get username and rolles from cookie
//                    if (context.Request.Cookies[".SHell_ASPXAUTH"] != null)
//                    {
//                        // why is it empty
//                        NameValueCollection authCookie = context.Request.Cookies[".SHell_ASPXAUTH"].Values;
//                    //   context.Session["UserName"] = authCookie["UserName"];
//                     //  context.Session["Roles"] = authCookie["Roles"];
//                    }
                   
                    
//                }
//               else
//                {
//                    //this is for GUESTS
//                    // Generate a new random GUID using System.Guid class 
//                    Guid tempMembersViewModelId = Guid.NewGuid();
//                    // Send tempCartId back to client as a cookie 
//                    context.Session[MemberSessionKey] = tempMembersViewModelId.ToString();
//                 }
//        }
//      return context.Session[MemberSessionKey].ToString();
//}


//        //TO DO  use velocity
//        //session key is guid for guests and username for users
//        public  MembersViewModel GetMembersViewModel(HttpContextBase context )
//        {
//                var model = new MembersViewModel();

//                //check if the members viewmodel already exists if it does nothn
//                //TO DO  we  will be checking the thisMemberSessionKey instead when we use a state server.
//                // ie code will be if context.Session["thisMemberSessionKey"] == null then this runs , otherwise  retrive model from state DB
//                if (context.Session["MembersViewModel"] == null)
//                {

//                    //different for logged on users and not logged on users 
//                    if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
//                    {
//                        //build the members viewmodel and rturn it

//                         // var viewModelBuilder = new MembersViewModelBuilder();
//                        //TO store the model to the data
//                        
//                        membersmodel = cf.GetMemberData(CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));
//                        var mm = new ViewModelMapper();
//                        model = mm.MapMember(context.User.Identity.Name);

//                        CachingFactory.MembersViewModelHelper.Add(model, context);
//                                              return model;

                                            
//                    }
//                    else
//                    {
//                        //not sure what needs to be initialized for guests , if anything initlize it here
//                        // map the registration thing since 
//                       // GuestRepository guestrepo = new GuestRepository();
//                       // model = guestrepo.MapGuest();
//                        //also map the guest members viewmodel
//                        var mm = new ViewModelMapper();
//                        model = mm.MapGuest();

//                        //poulate the model from the view model mapper
//                        ViewModelMapper Mapper = new ViewModelMapper();
//                        #if DEBUG
//                        Console.WriteLine("Debug version");
//                        model.Register = Mapper.MapRegistrationTest();
//                        #else
//                               model.Register = Mapper.MapRegistration();
//                        #endif

//                        //store the model if null
//                        CachingFactory.MembersViewModelHelper.Add(model, context);
//                        return model;
//                    }

//                } 
              
//            //generate the model and return it since its not null         
//                model = (MembersViewModel)(context.Session["MembersViewModel"]);
           


//             return  model;
//        }

//        //TO DO  use velocity
//        //session key is guid for guests and username for users
//        //Ola LAwal this also remaps as well use this only on members home page since we might need to reload user settings
//        //i.e the come back home after doing stuff elsewhere on the site
//        public MembersViewModel GetMembersViewModelAndReMapSettings(HttpContextBase context)
//        {
//            var model = new MembersViewModel();

//            //check if the members viewmodel already exists if it does nothn
//            //TO DO  we  will be checking the thisMemberSessionKey instead when we use a state server.
//            // ie code will be if context.Session["thisMemberSessionKey"] == null then this runs , otherwise  retrive model from state DB
//            if (context.Session["MembersViewModel"] == null)
//            {

//                //different for logged on users and not logged on users 
//                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
//                {
//                    //build the members viewmodel and rturn it

//                    // var viewModelBuilder = new MembersViewModelBuilder();
//                    //TO store the model to the data
//                    var mm = new ViewModelMapper();
//                    model = mm.MapMember(context.User.Identity.Name);
//                    return model;

//                }
//                else
//                {
//                    //not sure what needs to be initialized for guests , if anything initlize it here
//                    // map the registration thing since 
//                    // GuestRepository guestrepo = new GuestRepository();
//                    // model = guestrepo.MapGuest();
//                    //also map the guest members viewmodel
//                    var mm = new ViewModelMapper();
//                    model = mm.MapGuest();

//                    //poulate the model from the view model mapper
//                    // ViewModelMapper Mapper = new ViewModelMapper();
//                    // model.Register = Mapper.MapRegistration();

//                    return model;
//                }

//            }

//            //generate the model and return it since its not null         
//            model = (MembersViewModel)(context.Session["MembersViewModel"]);
//            //6/15/2011 - ola lawal added code to remap the model quickly first
//            //instead just loading it from session since user could have changed some data
//            var RemappedMember = new ViewModelMapper();
//            model = RemappedMember.ReMapLoggedInMember(model);


//            return model;
//        }


//        //generates down the members data if it does not exist and gets or sets it using the MemberSessionKey
//        //not doing anything i think
//        public  MembersViewModel GetMemberData(HttpContextBase context)
//        {
            
//            var model = new MembersViewModel();
//            //we use MemberSessionKey the seeion key, this function gets or creates a new key

//            string MemberSessionKey = GetMembersViewModelID(context);
//            //get the current model
//            model = GetMembersViewModel(context); 
//            //we have the ID now get fill in the rest of member data

//            return model;
//        }

        //Depreciated method we do it by profileID now
        //public MembersViewModel  GetMemberDataByUserName(string username)
        //{

        //    var model = new MembersViewModel();
          
        //    //get user name by profileID

         

        //            // var viewModelBuilder = new MembersViewModelBuilder();
        //            //TO store the model to the data
        //            var mm = new ViewModelMapper();
        //            model = mm.MapMember(username);
        //            return model;
           

           
        //}

        //added method to get by profileID
        //1-11-2011
        public MembersViewModel GetMemberDataByProfileID(string ProfileId)
        {

            var model = new MembersViewModel();

            //get user name by profileID



            // var viewModelBuilder = new MembersViewModelBuilder();
            //TO store the model to the data
            var mm = new ViewModelMapper();
            model = mm.MapMember(ProfileId);
            return model;



        } 

        // Helper method to simplify shopping cart calls 
        //public  MembersViewModel GetMemberData(Controller controller)
        //{
        //    return GetMemberData(controller.HttpContext);
        //}

        #endregion

        public string getProfileIDbyUserName(string User)
        {

            return (from p in db.profiles
                    where p.UserName == User
                    select p.ProfileID).FirstOrDefault();
        }


        public profile getProfileByProfileID(string ProfileId)
        {

            return (from p in db.profiles
                    where p.ProfileID == ProfileId
                    select p).FirstOrDefault();
        }

        public bool  checkifusernamealreadyexists(string profileID)
        {

            return datingservicecontext.checkifusernamealreadyexists(profileID);
        }    
        public string getGenderByScreenName(string ScreenName)
        {

            return (from p in db.profiles where p.ScreenName  == ScreenName 
                      join f in  db.ProfileDatas on p.ProfileID equals f.ProfileID
                    select f.gender).FirstOrDefault().GenderName ;
        }
        public string GetProfileIDByScreenName(string ScreenName)
        {

            return datingservicecontext.GetProfileIdbyScreenName(ScreenName);

        }                
        public profile GetProfileByUserName(string username)
        {
            return datingservicecontext.GetProfileByUsername(username);
        }
              
        public  ProfileData GetProfileDataByProfileID(string profileid)
        {

            return datingservicecontext.GetProfileDataByProfileID(profileid);
        }

        public SearchSetting GetPerFectMatchSearchSettingsByProfileID(string profileid)
        {

            return datingservicecontext.GetPerFectMatchSearchSettingsByProfileID(profileid);
        }

        public SearchSetting CreateMyPerFectMatchSearchSettingsByProfileID(string profileid)
        {

            return datingservicecontext.CreateMyPerFectMatchSearchSettingsByProfileID(profileid);
        }

        public bool CreateMailBoxFolders(string profileid)
        {

            return datingservicecontext.CreateMailBoxFolders(profileid);
        }

        public bool DeactivateProfile(string profileid)
        {

            return datingservicecontext.DeActivateProfile(profileid);

        }

        public ProfileVisiblitySetting GetProfileVisibilitySettingsByProfileID(string ProfileID)
        {

            return (from p in db .ProfileVisiblitySettings
                    where p.ProfileID == ProfileID  select p).FirstOrDefault();
        }

        #region "geolocation methods should be moved"
        public string GetMyCountry(ProfileData UserProfileData)
        {

            return (from p in postaldb.Country_PostalCode_List
                    where p.CountryID == UserProfileData.CountryID
                    select p.CountryName).FirstOrDefault();
            //return postaldataservicecontext.GetCountryNameByCountryID(ProfileData.CountryID);
        }

        public registermodel  VerifyOrUpdateRegistrationGeoData(registermodel  model,MembersViewModel membersmodel)
        {
            //4-24-2012 fixed code to hanlde if we did not have a postcal code
            GpsData gpsData = new GpsData();
            string[] tempCityAndStateProvince = model.City.Split(',');
            //int countryID;

            //attmept to get postal postalcode if it is empty
            model.ZipOrPostalCode = (model.ZipOrPostalCode == null) ? postaldataservicecontext.GetGeoPostalCodebyCountryNameAndCity(model.Country, tempCityAndStateProvince[0]) : model.ZipOrPostalCode;
            model.Stateprovince = ((tempCityAndStateProvince.Count() > 1)) ? tempCityAndStateProvince[1] : "NA";
            //countryID = postaldataservicecontext.GetCountryIdByCountryName(model.Country);

            //check if the  city and country match
            if (model.Country == membersmodel.MyQuickSearch.MySelectedCountryName && tempCityAndStateProvince[0] == membersmodel.MyQuickSearch.MySelectedCity )               
            {
                if (model.lattitude != null | model.lattitude == 0 ) 
                return model;

            }

            //get GPS data here
            //conver the unquiqe coountry Name to an ID
            //store country ID for use later
                //get the longidtue and latttude 
                //1-11-2011 postal code and city are flipped by the way not this function should be renamed
                //TO DO rename this function.                  
            gpsData = postaldataservicecontext.GetGpsDataSingleByCityCountryAndPostalCode(model.Country, model.ZipOrPostalCode, tempCityAndStateProvince[0]);


            model.lattitude = (gpsData != null)?  gpsData.Latitude : 0;
            model.longitude = (gpsData != null)? gpsData.Longitude : 0;

            return model;
            
        }

        #endregion


        #region "photo meothds maybe should be moved if we get more"

        //mobe this to a repository
        public Photo UploadProfileImage(string _imageUrl, string caption)
        {

            //get current profileID from cache
            //11-1-2011 this is an out of logged in action as well so using session to tie 
           // var _ProfileID = (this.HttpContext.User.Identity.Name != "") ? CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name) :
           //     CachingFactory.GetProfileIDBySessionId(this.HttpContext);
           // var membersmodel = new MembersViewModel();
            //guests cannot upload photos
           // membersmodel = (_ProfileID != null) ? CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID) : null;

            //add the files to a temporary photo model and store it in session for now?
           var photo = new Photo();

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
                photo.ActualImage = imageBytes;
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
        #endregion

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> GetQuickMatches(MembersViewModel model)
        {

            //get search sttings from DB
            SearchSetting perfectmatchsearchsettings = model.ProfileData.SearchSettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.DistanceFromMe  == null | perfectmatchsearchsettings.DistanceFromMe  == 0)
                model.MaxDistanceFromMe = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.Latitude,p.Longitude,UserProfile.Lattitude,UserProfile.Longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.ProfileData.SearchSettings.FirstOrDefault().SearchSettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.AgeMax  != null ? perfectmatchsearchsettings.AgeMax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.AgeMin  != null ? perfectmatchsearchsettings.AgeMin.GetValueOrDefault() : 18;
            //Height
            int intHeightMin = perfectmatchsearchsettings.HeightMin != null ? perfectmatchsearchsettings.HeightMin.GetValueOrDefault() : 0;
            int intHeightMax = perfectmatchsearchsettings.HeightMax != null ? perfectmatchsearchsettings.HeightMax.GetValueOrDefault() : 100;
            bool blEvaluateHeights = intHeightMin >0 ? true : false;
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.MyLongitude != "")?  Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLatitude  != "")?  Convert.ToDouble(model.MyLatitude):0;
            //get the rest of the values if they are needed in calculations

         
            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);
            


            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues =(perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_Genders.Select(c => c.GenderID.GetValueOrDefault())) : LookingForGenderValues;
            //Appearacnce seache settings values         

            //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?
         
            HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
            LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_BodyTypes.Select(c => c.BodyTypesID.GetValueOrDefault())) : LookingForBodyTypesValues;

            HashSet<int> LookingForEthnicityValues = new HashSet<int>();
            LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_Ethnicity.Select(c => c.EthicityID.GetValueOrDefault())) : LookingForEthnicityValues;

            HashSet<int> LookingForEyeColorValues = new HashSet<int>();
            LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_EyeColor.Select(c => c.EyeColorID.GetValueOrDefault())) : LookingForEyeColorValues;

            HashSet<int> LookingForHairColorValues = new HashSet<int>();
            LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_HairColor.Select(c => c.HairColorID.GetValueOrDefault())) : LookingForHairColorValues;

            HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
            LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_HotFeature.Select(c => c.HotFeatureID.GetValueOrDefault())) : LookingForHotFeatureValues;

        
            //******** visiblitysettings test code ************************
            
            // test all the values you are pulling here
            // var TestModel =   (from x in db.ProfileDatas.Where(x => x.profile.UserName  == "case")
           //                      select x).FirstOrDefault();
          //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault() + 1));
           // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.ProfileData.GenderID) ? true : false;

            //  var testmodel2 = (from x in db.ProfileDatas.Where(x => x.profile.UserName  == "case" &&  db.fnCheckIfBirthDateIsInRange(x.Birthdate, 19, 20) == true  )
           //                     select x).FirstOrDefault();

    
           //****** end of visiblity test settings *****************************************

            MemberSearchViewmodels = (from x in db.ProfileDatas.Where(p=> p.Birthdate > min && p.Birthdate <= max)
                            
                               //** visiblity settings still needs testing           
                             //5-8-2012 add profile visiblity code here
                            // .Where(x => x.profile.UserName == "case")
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.ProfileVisiblity == true)
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.AgeMaxVisibility != null && model.ProfileData.Birthdate > today.AddYears(-(x.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault() + 1)))
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.AgeMaxVisibility != null && model.ProfileData.Birthdate < today.AddYears(-x.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault()))
                           // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.CountryID != null && x.ProfileVisiblitySetting.CountryID == model.ProfileData.CountryID  )
                           // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.GenderID != null && x.ProfileVisiblitySetting.GenderID ==  model.ProfileData.GenderID )
                          //** end of visiblity settings ***
                          
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.GenderID)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z=>z.GenderID == model.LookingForGendersID.FirstOrDefault())    
                            //TO DO add the rest of the filitering here 
                            //Appearance filtering                         
                            .WhereIf(blEvaluateHeights, z=> z.Height > intHeightMin && z.Height <= intHeightMax) //Only evealuate if the user searching actually has height values they look for                         
                                      join f in db.profiles on x.ProfileID equals f.ProfileID                                    
                                      select new MemberSearchViewModel
                                      {
                                         // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          ProfileID = x.ProfileID,
                                          State_Province = x.State_Province,
                                          PostalCode = x.PostalCode,
                                          CountryID = x.CountryID,
                                          GenderID = x.GenderID,
                                          Birthdate = x.Birthdate,
                                          profile = f,
                                            ScreenName = f.ScreenName ,
                                          Longitude = (double)x.Longitude,
                                          Latitude = (double)x.Latitude,
                                          HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                                          CreationDate = f.CreationDate,
                                          City = db.fnTruncateString(x.City, 11),
                                          lastloggedonString = db.fnGetLastLoggedOnTime(f.LoginDate),
                                          LastLoginDate = f.LoginDate,
                                          Online = db.fnGetUserOlineStatus(x.ProfileID),
                                          DistanceFromMe = db.fnGetDistance((double)x.Latitude, (double)x.Longitude,myLattitude.Value  , myLongitude.Value   , "Miles") 
                                      }).ToList();


          //  var temp = MemberSearchViewmodels;
            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (model.MaxDistanceFromMe > 0) ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= model.MaxDistanceFromMe) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.CountryID  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.LastLoginDate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.HasGalleryPhoto == "Gallery").ThenByDescending(p => p.CreationDate).ThenByDescending(p => p.DistanceFromMe);

            //11/20/2011 handle case where  no profiles were found
            if (Profiles.Count() == 0 )
            return GetQuickMatchesWhenQuickMatchesEmpty(model);


            return Profiles.ToList();


        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> GetEmailMatches(MembersViewModel model)
        {

            //get search sttings from DB
            SearchSetting perfectmatchsearchsettings = model.ProfileData.SearchSettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.DistanceFromMe == null | perfectmatchsearchsettings.DistanceFromMe == 0)
                model.MaxDistanceFromMe = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.Latitude,p.Longitude,UserProfile.Lattitude,UserProfile.Longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.ProfileData.SearchSettings.FirstOrDefault().SearchSettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.AgeMax != null ? perfectmatchsearchsettings.AgeMax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.AgeMin != null ? perfectmatchsearchsettings.AgeMin.GetValueOrDefault() : 18;
            //Height
            int intHeightMin = perfectmatchsearchsettings.HeightMin != null ? perfectmatchsearchsettings.HeightMin.GetValueOrDefault() : 0;
            int intHeightMax = perfectmatchsearchsettings.HeightMax != null ? perfectmatchsearchsettings.HeightMax.GetValueOrDefault() : 100;
            bool blEvaluateHeights = intHeightMin > 0 ? true : false;
            //get the rest of the values if they are needed in calculations
            //convert lattitudes from string (needed for JSON) to bool           
            double? myLongitude = (model.MyLongitude != "") ? Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLatitude != "") ? Convert.ToDouble(model.MyLatitude) : 0;


            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_Genders.Select(c => c.GenderID.GetValueOrDefault())) : LookingForGenderValues;
            //Appearacnce seache settings values         

            //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

            HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
            LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_BodyTypes.Select(c => c.BodyTypesID.GetValueOrDefault())) : LookingForBodyTypesValues;

            HashSet<int> LookingForEthnicityValues = new HashSet<int>();
            LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_Ethnicity.Select(c => c.EthicityID.GetValueOrDefault())) : LookingForEthnicityValues;

            HashSet<int> LookingForEyeColorValues = new HashSet<int>();
            LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_EyeColor.Select(c => c.EyeColorID.GetValueOrDefault())) : LookingForEyeColorValues;

            HashSet<int> LookingForHairColorValues = new HashSet<int>();
            LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_HairColor.Select(c => c.HairColorID.GetValueOrDefault())) : LookingForHairColorValues;

            HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
            LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_HotFeature.Select(c => c.HotFeatureID.GetValueOrDefault())) : LookingForHotFeatureValues;



            MemberSearchViewmodels = (from x in db.ProfileDatas.Where(p => p.Birthdate > min && p.Birthdate <= max)
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.GenderID)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => z.GenderID == model.LookingForGendersID.FirstOrDefault())
                                          //Appearance filtering not implemented yet                        
                            .WhereIf(blEvaluateHeights, z => z.Height > intHeightMin && z.Height <= intHeightMax) //Only evealuate if the user searching actually has height values they look for 
                            .WhereIf(model.MaxDistanceFromMe > 0, a => db.fnGetDistance((double)a.Latitude, (double)a.Longitude, Convert.ToDouble(model.MyLatitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.MaxDistanceFromMe)
                                      join f in db.profiles on x.ProfileID equals f.ProfileID
                                      select new MemberSearchViewModel
                                      {
                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          ProfileID = x.ProfileID,
                                          State_Province = x.State_Province,
                                          PostalCode = x.PostalCode,
                                          CountryID = x.CountryID,
                                          GenderID = x.GenderID,
                                          Birthdate = x.Birthdate,
                                          profile = f,
                                          ScreenName = f.ScreenName,
                                          Longitude = x.Longitude ?? 0,
                                          Latitude = x.Latitude ?? 0,
                                          HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                                          CreationDate = f.CreationDate,
                                          City = db.fnTruncateString(x.City, 11),
                                          lastloggedonString = db.fnGetLastLoggedOnTime(f.LoginDate),
                                          LastLoginDate = f.LoginDate,
                                          Online = db.fnGetUserOlineStatus(x.ProfileID),
                                         DistanceFromMe = db.fnGetDistance((double)x.Latitude, (double)x.Longitude,myLattitude.Value  , myLongitude.Value   , "Miles")


                                      }).ToList();



            //these could be added to where if as well, also limits values if they did selected all
           // var Profiles = (model.MaxDistanceFromMe > 0) ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= model.MaxDistanceFromMe) select q) : MemberSearchViewmodels;
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.CountryID  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.LastLoginDate)
            //final ordering 
            var Profiles = MemberSearchViewmodels.OrderByDescending(p => p.HasGalleryPhoto == "Gallery").ThenByDescending(p => p.CreationDate).ThenByDescending(p => p.DistanceFromMe).Take(4);

            //11/20/2011 handle case where  no profiles were found
           // if (Profiles.Count() == 0)
            //    return GetQuickMatchesWhenQuickMatchesEmpty(model);


            return Profiles.ToList();


        }


        public List<MemberSearchViewModel> GetQuickMatchesWhenQuickMatchesEmpty(MembersViewModel model)
        {

            //get search sttings from DB
            SearchSetting perfectmatchsearchsettings = model.ProfileData.SearchSettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.DistanceFromMe == null | perfectmatchsearchsettings.DistanceFromMe == 0)
                model.MaxDistanceFromMe = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.Latitude,p.Longitude,UserProfile.Lattitude,UserProfile.Longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.ProfileData.SearchSettings.FirstOrDefault().SearchSettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.AgeMax != null ? perfectmatchsearchsettings.AgeMax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.AgeMin != null ? perfectmatchsearchsettings.AgeMin.GetValueOrDefault() : 18;
           
            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.MyLongitude != "") ? Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLatitude != "") ? Convert.ToDouble(model.MyLatitude) : 0;



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.SearchSettings_Genders.Select(c => c.GenderID.GetValueOrDefault())) : LookingForGenderValues;
                         

            //  where (LookingForGenderValues.Count !=null || LookingForGenderValues.Contains(x.GenderID)) 
            //  where (LookingForGenderValues.Count == null || x.GenderID == UserProfile.MyQuickSearch.MySelectedSeekingGenderID )   //this should not run if we have no gender in searchsettings
            MemberSearchViewmodels = (from x in db.ProfileDatas.Where(p => p.Birthdate > min && p.Birthdate <= max)
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.GenderID)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => z.GenderID == model.LookingForGendersID.FirstOrDefault())                            

                                      join f in db.profiles on x.ProfileID equals f.ProfileID
                                      select new MemberSearchViewModel
                                      {
                                         // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          ProfileID = x.ProfileID,
                                          State_Province = x.State_Province,
                                          PostalCode = x.PostalCode,
                                          CountryID = x.CountryID,
                                          GenderID = x.GenderID,
                                          Birthdate = x.Birthdate,
                                          profile = f,
                                          ScreenName = f.ScreenName,
                                          Longitude = x.Longitude ?? 0,
                                          Latitude =  x.Latitude ?? 0,
                                          HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                                          CreationDate = f.CreationDate,
                                          City = db.fnTruncateString(x.City, 11),
                                          lastloggedonString = db.fnGetLastLoggedOnTime(f.LoginDate),
                                          LastLoginDate = f.LoginDate,
                                          Online = db.fnGetUserOlineStatus(x.ProfileID),
                                          DistanceFromMe = db.fnGetDistance((double)x.Latitude, (double)x.Longitude,myLattitude.Value  , myLongitude.Value   , "Miles")

                                      }).ToList();


            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (model.MaxDistanceFromMe > 0) ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= model.MaxDistanceFromMe) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.CountryID  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.LastLoginDate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.HasGalleryPhoto == "Gallery").ThenByDescending(p => p.CreationDate).ThenByDescending(p => p.DistanceFromMe);


            return Profiles.ToList();


        }


        public IQueryable<MemberSearchViewModel> GetQuickMatchesQueryable(ProfileData UserProfile)
        {
            List<MemberSearchViewModel> model;
            {
                model = (from x in db.ProfileDatas
                 .Where(p => p.GenderID == UserProfile.GenderID
                     && p.CountryID == UserProfile.CountryID)
                         join f in db.profiles
                         on x.ProfileID equals f.ProfileID
                         select new MemberSearchViewModel
                         {

                             ProfileID = x.ProfileID,
                             State_Province = x.State_Province,
                             PostalCode = x.PostalCode,
                             CountryID = x.CountryID,
                             City = x.City,
                             GenderID = x.GenderID,
                             Birthdate = x.Birthdate,                            
                             profile = f,
                              ScreenName = f.ScreenName ,
                         }).Take(200).ToList();
            }

            //other limiting queries here
            // final query: query against required age range
            // var Profiles = from q in model.Where(a => a.Age <= intAgeTo && a.Age >= intAgeFrom) select q;
            var Profiles = model;
            return Profiles.AsQueryable();

        }

         #region "model mapper functions"

         //gets search settings
         //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
         public MembersViewModel GetDefaultQuickSearchSettingsMembers(MembersViewModel Model)
         {
             QuickSearchModel QuickSearchModel = new QuickSearchModel();
              PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
             //set deafult paging or pull from DB
             QuickSearchModel.mySelectedPageSize = 4;
             QuickSearchModel.mySelectedCurrentPage = 1;
             //added state province with comma 

             QuickSearchModel.MySelectedCity = Model.ProfileData.City;
             QuickSearchModel.MySelectedMaxDistanceFromMe = Model.ProfileData.SearchSettings.FirstOrDefault().DistanceFromMe  != null ? Model.MaxDistanceFromMe : 1000;

             QuickSearchModel.MySelectedFromAge = Model.ProfileData.SearchSettings.FirstOrDefault().AgeMin  != null ? Model.ProfileData.SearchSettings.FirstOrDefault().AgeMin.GetValueOrDefault() : 18;
             QuickSearchModel.MyselectedToAge = Model.ProfileData.SearchSettings.FirstOrDefault().AgeMax  != null ? Model.ProfileData.SearchSettings.FirstOrDefault().AgeMax.GetValueOrDefault() : 99; ;


             QuickSearchModel.MySelectedIamGenderID = Model.ProfileData.GenderID;
             QuickSearchModel.MySelectedCityStateProvince = Model.ProfileData.City + "," + Model.ProfileData.State_Province;;
             //TO DO convert genders to a list of genders 
             QuickSearchModel.MySelectedSeekingGenderID = Extensions.GetLookingForGenderID(Model.ProfileData.GenderID);
             QuickSearchModel.MySelectedCountryName = Model.MyCountryName; //use same country for now
             //add the postal code status here as well
             QuickSearchModel.MySelectedPostalCodeStatus = (georepository.GetCountry_PostalCodeStatusByCountryName(Model.MyCountryName) == 1) ? true : false;

             //TO do get this from search settings
             //default for has photos only get this from the 
             QuickSearchModel.MySelectedPhotoStatus = true;

             Model.MyQuickSearch = QuickSearchModel;  //save it

             return Model;
         }
         //populate search settings for guests 
         public MembersViewModel GetDefaultSearchSettingsGuest(MembersViewModel Model)
         {
             //set defualt values for guests
             Model.MyQuickSearch.mySelectedPageSize = 4;
             Model.MyQuickSearch.mySelectedCurrentPage = 1;
             Model.MyQuickSearch.MySelectedCity = "";
             Model.MyPostalCodeStatus = false;
             Model.MyQuickSearch.MySelectedMaxDistanceFromMe = 2000;
             Model.MyQuickSearch.MySelectedFromAge = 18;
             Model.MyQuickSearch.MyselectedToAge = 99;
             Model.MyQuickSearch.MySelectedIamGenderID = 1;
             Model.MyQuickSearch.MySelectedCityStateProvince = "ALL";
             Model.MyQuickSearch.MySelectedSeekingGenderID = Extensions.GetLookingForGenderID(1);
             Model.MyQuickSearch.MySelectedCountryName = "United States"; //use same country for now

             Model.MyQuickSearch.MySelectedPhotoStatus = true;

             return Model;
         }

        //builds the profile browse model ie profile Deatil for Members logged in
        //added exrea info 6/8/2011 
        //public IEnumerable<ProfileBrowseModel> GetMyMatchesQuickProfile(List<MemberSearchViewModel> searchModel,MembersViewModel  UserProfile)
        //{

        //    List<ProfileBrowseModel> model;
        //    {


        //        model = (from x in searchModel
        //                 join f in db.ProfileDatas
        //                 on x.ProfileID equals f.ProfileID
        //                 select new ProfileBrowseModel
        //                 {

        //                     SearchProfileData = f,
        //                     quickSearch = x,
        //                     MyCatchyIntroLineQuickSearch = Extensions.Chop(f.AboutMe.ToString(), 10),
        //                     MyprofileData = UserProfile.ProfileData,
        //                     SearchCriteria = new ProfileCriteriaModel(f),
        //                     MyCriteria = new ProfileCriteriaModel(UserProfile.ProfileData)
        //                 }).ToList();

        //    }

        //   // datingservicecontext.GetProfileStatuses();

        //   //update other things like blockstatus,email status etc
        //    //only check this for registered members
        //    if(UserProfile.Profile.ProfileStatusID  >= 2)
        //    {
        //        foreach (ProfileBrowseModel p in model)
        //        {

        //            //update values needed.
        //            //these are thier views of me
        //         //   p.PeekedAtMe = PeekStatus(UserProfile.Profile.ProfileID, p.SearchProfileData.ProfileID);
        //         //   p.IntrestSentToMe = IntrestStatus(p.SearchProfileData.ProfileID,UserProfile.Profile.ProfileID);
        //         //   p.LikesMe = LikeStatus(p.SearchProfileData.ProfileID,UserProfile.Profile.ProfileID);
        //         //   p.BlockedMe = BlockStatus(p.SearchProfileData.ProfileID, UserProfile.Profile.ProfileID);
        //            //status of my views of them
        //        //    p.PeekedAtThisMember = PeekStatus(p.SearchProfileData.ProfileID, UserProfile.Profile.ProfileID);
        //            p.IntrestSentToThisMember = IntrestStatus(UserProfile.Profile.ProfileID, p.SearchProfileData.ProfileID);
        //        //    p.LikedThisMemer = LikeStatus(UserProfile.Profile.ProfileID, p.SearchProfileData.ProfileID);
        //        //    p.BlockedThisMember  = BlockStatus(UserProfile.Profile.ProfileID,p.SearchProfileData.ProfileID);
        //        }
        //    }
        //    return model.ToList();


        //}
        #endregion

        

        /*** prfile actions , i.e inrests, likes peeks etc handled here **/

        #region " these fucntions check Profile status values here for noew"

        //verify that user is not alreayd liked by the viewer
        public bool PeekStatus(string ProfileID, string ProfileViewerID)
        {
            return datingservicecontext.CheckPeek(ProfileID,ProfileViewerID);
        }      
        public bool IntrestStatus(string ProfileID, string TargetProfileID)
        {
            // return db.profiles.Any(r => r.ProfileID  == ProfileID  && (r.ProfileStatusID !=1 ));
            return datingservicecontext.CheckIntrest(ProfileID, TargetProfileID);
        }
        //verify that an block has not been already sent
        public bool BlockStatus(string ProfileID, string TargetProfileID)
        {
            return datingservicecontext.Checkblock(ProfileID, TargetProfileID);
        }
        //verify that an Likehas not been already sent
        public bool LikeStatus(string ProfileID, string TargetProfileID)
        {
            // return db.profiles.Any(r => r.ProfileID  == ProfileID  && (r.ProfileStatusID !=1 ));
            return datingservicecontext.CheckLike(ProfileID,TargetProfileID);
        }
        //verify that an intrest has not been already sent
   
        #endregion

        #region "profile actions Update and retriveal methods"


        //counter functions here covereted to strings
        public string GetMyInterestCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoIamInterestedInCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoisInterestedinMeCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoisInterestedinmeCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoisInterestedinMeNewCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoisInteredinmeNewCount (UserProfileData.ProfileID).ToString();
        }

        public string GetMypeekCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoIPeekedAtCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoPeekedatmeCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoPeekedatmeCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoPeekedatmeNewCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoPeekedatmeNewCount(UserProfileData.ProfileID).ToString();
        }
      
        public string GetWhoLikesMeCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoLikesMeCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoLIkesMeNewCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoLikesMeNewCount(UserProfileData.ProfileID).ToString();
        }
        public string GetWhoIlikeCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoILikeCount (UserProfileData.ProfileID).ToString();
        }

        public string GetMyblockCount(ProfileData UserProfileData)
        {
            return datingservicecontext.WhoIblockedCount(UserProfileData.ProfileID).ToString();
        }

      

        //end of counter
          
#endregion

        #region "Collection Returning Profile Actions , ie likes,peeks,interests,blocks etc"
        //collection functions here as needed
        //Intrests
        [Authorize]
        public List<MemberSearchViewModel> GetWhoIamInterestedIn(string ProfileID)
        {


            IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            searchmodellist = from p in datingservicecontext.intrests (ProfileID)
                              select new MemberSearchViewModel
                              {
                                  Age = p.Age,
                                  Birthdate = p.BirthDate,
                                  ProfileID = p.ProfileId,
                                  State_Province = p.State_Province,
                                  PostalCode = p.PostalCode,
                                  CountryID = p.CountryID,
                                  City = p.City,
                                  GenderID = p.GenderID,
                                  Longitude = (double)p.Longitude,
                                  Latitude = (double)p.Latitude,
                                  lastloggedonString = p.LastLoggedInString,
                                  LastLoggedInTime = p.LastLoggedInTime,
                                  ScreenName = p.ScreenName,
                                  MyCatchyIntroLine = p.MyCatchyIntroLine,
                                  AboutMe = p.AboutMe,
                                  PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                              };

            //order by date right ? no its already ordered
            //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();


    


        }
        [Authorize]      
        public List<MemberSearchViewModel> GetWhoIsInterestedInMe(string ProfileID)
        {

            IEnumerable<MemberSearchViewModel> searchmodellist;
           //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

             searchmodellist = from p in datingservicecontext.WhoIsInterestedInMe(ProfileID)
                       select new MemberSearchViewModel
                       {
                           Age = p.Age,
                           Birthdate = p.BirthDate ,
                           ProfileID = p.ProfileId,
                           State_Province = p.State_Province,
                           PostalCode = p.PostalCode,
                           CountryID = p.CountryID ,
                           City = p.City,
                           GenderID = p.GenderID,                         
                           Longitude = (double)p.Longitude,
                           Latitude = (double)p.Latitude,
                           lastloggedonString = p.LastLoggedInString ,
                           LastLoggedInTime = p.LastLoggedInTime ,
                           ScreenName = p.ScreenName ,
                           MyCatchyIntroLine = p.MyCatchyIntroLine ,
                           AboutMe = p.AboutMe ,
                           PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)
                        
                          
                       };
          
            //order by date right ? no its already ordered
          //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();

            //now filter and sort , filter out blocked and banned members
            //trim out the messages of banned members added 6/2/2011   
            //sets up the inital paging for your matches    

            
        }
        [Authorize]
        public List<MemberSearchViewModel> GetWhoIsInterestedInMeNew(string ProfileID)
        {

            IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            searchmodellist = from p in datingservicecontext.WhoIsInterestedInMeNew (ProfileID)
                              select new MemberSearchViewModel
                              {
                                  Age = p.Age,
                                  Birthdate = p.BirthDate,
                                  ProfileID = p.ProfileId,
                                  State_Province = p.State_Province,
                                  PostalCode = p.PostalCode,
                                  CountryID = p.CountryID,
                                  City = p.City,
                                  GenderID = p.GenderID,
                                  Longitude = (double)p.Longitude,
                                  Latitude = (double)p.Latitude,
                                  lastloggedonString = p.LastLoggedInString,
                                  LastLoggedInTime = p.LastLoggedInTime,
                                  ScreenName = p.ScreenName,
                                  MyCatchyIntroLine = p.MyCatchyIntroLine,
                                  AboutMe = p.AboutMe,
                                    PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)
                              };

            //order by date right ? no its already ordered
            //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();
 


        }
        [Authorize]
        public IPageable<MemberSearchViewModel> GetWhoIHavePeekedAt(string ProfileID, int? page, int? numberPerPage)
        {

           // IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            return datingservicecontext.Peeks(ProfileID, page, numberPerPage);
                              


        }
        //Peeks
        [Authorize]
        public List<MemberSearchViewModel> GetWhoPeekedAtMe(string ProfileID)
        {
            
             IEnumerable<MemberSearchViewModel> searchmodellist;
           //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

             searchmodellist = from p in datingservicecontext.WhoPeekedAtMe(ProfileID)
                       select new MemberSearchViewModel
                       {
                           Age = p.Age,
                           Birthdate = p.BirthDate ,
                           ProfileID = p.ProfileId,
                           State_Province = p.State_Province,
                           PostalCode = p.PostalCode,
                           CountryID = p.CountryID ,
                           City = p.City,
                           GenderID = p.GenderID,                         
                           Longitude = (double)p.Longitude,
                           Latitude = (double)p.Latitude,
                           lastloggedonString = p.LastLoggedInString,
                           LastLoggedInTime = p.LastLoggedInTime,
                           ScreenName = p.ScreenName,
                           MyCatchyIntroLine = p.MyCatchyIntroLine,
                           AboutMe = p.AboutMe,
                           PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                       };
          
            //order by date right ? no its already ordered
          //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

        return searchmodellist.ToList();
      
         
        }
        [Authorize]
        public List<MemberSearchViewModel> GetWhoPeekedAtMeNew(string ProfileID)
         {

             IEnumerable<MemberSearchViewModel> searchmodellist;
             //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

             searchmodellist = from p in datingservicecontext.WhoPeekedAtMeNew(ProfileID)
                               select new MemberSearchViewModel
                               {
                                   Age = p.Age,
                                   Birthdate = p.BirthDate,
                                   ProfileID = p.ProfileId,
                                   State_Province = p.State_Province,
                                   PostalCode = p.PostalCode,
                                   CountryID = p.CountryID,
                                   City = p.City,
                                   GenderID = p.GenderID,
                                   Longitude = (double)p.Longitude,
                                   Latitude = (double)p.Latitude,
                                   lastloggedonString = p.LastLoggedInString,
                                   LastLoggedInTime = p.LastLoggedInTime,
                                   ScreenName = p.ScreenName,
                                   MyCatchyIntroLine = p.MyCatchyIntroLine,
                                   AboutMe = p.AboutMe,
                                   PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                               };

             //order by date right ? no its already ordered
             //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

             return searchmodellist.ToList();



         }
         [Authorize]      
        public List<MemberSearchViewModel> GetWhoIHaveBlocked(string ProfileID)
        {
            IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            searchmodellist = from p in datingservicecontext.WhoIBlocked(ProfileID)
                              select new MemberSearchViewModel
                              {
                                  Age = p.Age,
                                  Birthdate = p.BirthDate,
                                  ProfileID = p.ProfileId,
                                  State_Province = p.State_Province,
                                  PostalCode = p.PostalCode,
                                  CountryID = p.CountryID,
                                  City = p.City,
                                  GenderID = p.GenderID,
                                  Longitude = (double)p.Longitude,
                                  Latitude = (double)p.Latitude,
                                  lastloggedonString = p.LastLoggedInString,
                                  LastLoggedInTime = p.LastLoggedInTime,
                                  ScreenName = p.ScreenName,
                                  MyCatchyIntroLine = p.MyCatchyIntroLine,
                                  AboutMe = p.AboutMe,
                                  PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                              };

            //order by date right ? no its already ordered
            //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();



        }
        //Likes
        [Authorize]      
        public List<MemberSearchViewModel> GetWhoILike(string ProfileID)
        {

            IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            searchmodellist = from p in datingservicecontext.WhoILike(ProfileID)
                              select new MemberSearchViewModel
                              {
                                  Age = p.Age,
                                  Birthdate = p.BirthDate,
                                  ProfileID = p.ProfileId,
                                  State_Province = p.State_Province,
                                  PostalCode = p.PostalCode,
                                  CountryID = p.CountryID,
                                  City = p.City,
                                  GenderID = p.GenderID,
                                  Longitude = (double)p.Longitude,
                                  Latitude = (double)p.Latitude,
                                  lastloggedonString = p.LastLoggedInString,
                                  LastLoggedInTime = p.LastLoggedInTime,
                                  ScreenName = p.ScreenName,
                                  MyCatchyIntroLine = p.MyCatchyIntroLine,
                                  AboutMe = p.AboutMe,
                                  PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                              };

            //order by date right ? no its already ordered
            //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();


           
        }
         [Authorize] 
        public List <MemberSearchViewModel> GetWhoLikesMe(string ProfileID)
        {

            IEnumerable<MemberSearchViewModel> searchmodellist;
            //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

            searchmodellist = from p in datingservicecontext.WhoLikesMe (ProfileID)
                              select new MemberSearchViewModel
                              {
                                  Age = p.Age,
                                  Birthdate = p.BirthDate,
                                  ProfileID = p.ProfileId,
                                  State_Province = p.State_Province,
                                  PostalCode = p.PostalCode,
                                  CountryID = p.CountryID,
                                  City = p.City,
                                  GenderID = p.GenderID,
                                  Longitude = (double)p.Longitude,
                                  Latitude = (double)p.Latitude,
                                  lastloggedonString = p.LastLoggedInString,
                                  LastLoggedInTime = p.LastLoggedInTime,
                                  ScreenName = p.ScreenName,
                                  MyCatchyIntroLine = p.MyCatchyIntroLine,
                                  AboutMe = p.AboutMe,
                                  PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                              };

            //order by date right ? no its already ordered
            //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

            return searchmodellist.ToList();


        }
         [Authorize]
         public List<MemberSearchViewModel> GetWhoLikesMeNew(string ProfileID)
         {

             IEnumerable<MemberSearchViewModel> searchmodellist;
             //profiledatas = datingservicecontext.WhoIsInterestedInMe(ProfileID) ;

             searchmodellist = from p in datingservicecontext.WhoLikesMeNew(ProfileID)
                               select new MemberSearchViewModel
                               {
                                   Age = p.Age,
                                   Birthdate = p.BirthDate,
                                   ProfileID = p.ProfileId,
                                   State_Province = p.State_Province,
                                   PostalCode = p.PostalCode,
                                   CountryID = p.CountryID,
                                   City = p.City,
                                   GenderID = p.GenderID,
                                   Longitude = (double)p.Longitude,
                                   Latitude = (double)p.Latitude,
                                   lastloggedonString = p.LastLoggedInString,
                                   LastLoggedInTime = p.LastLoggedInTime,
                                   ScreenName = p.ScreenName,
                                   MyCatchyIntroLine = p.MyCatchyIntroLine,
                                   AboutMe = p.AboutMe,
                                   PerfectMatchSettings = GetPerFectMatchSearchSettingsByProfileID(p.ProfileId)

                               };

             //order by date right ? no its already ordered
             //  searchmodellist.OrderBy(p => p.LastLoggedInTime);

             return searchmodellist.ToList();


         }
        
        #endregion

       //end of collection functions
        //update functions here , ie add intrests and update intrest view status
       #region "update functions here , ie add intrests and update intrest view status"
       [Authorize]      
       public bool AddInterest(string ProfileID, string TargetProfileID)
       {
           return datingservicecontext.AddIntrest(ProfileID, TargetProfileID);          
       }
       [Authorize]
       public bool AddPeek(string ProfileID, string TargetProfileID)
       {
          



           return datingservicecontext.AddPeek(ProfileID, TargetProfileID);      
       }

        //overload that acceps a profile browsemodel
       [Authorize]
       public bool AddPeekFromBrowseModel(ProfileBrowseModel model)
       {
           
          

          // string TargetProfileID; string ProfileID; string UserName;
           var Email = new EmailModel();
           Email.ScreenName = model.ProfileDetails.profile.ScreenName;
           Email.ProfileID = model.ProfileDetails.ProfileID;
           Email.UserName = model.ProfileDetails.profile.UserName;
           Email.SenderScreenName = model.ViewerProfileDetails.profile.ScreenName;
           Email.SenderProfileID = model.ViewerProfileDetails.profile.ProfileID;
        
           //get the current profile browsemodel since we want to update it 
          // List<ProfileBrowseModel> modelInSession = new List<ProfileBrowseModel>();
           //modelInSession = CachingFactory.ProfileBrowseModelsHelper.Get(this.HttpContext);

           //add a peek here if needed
           //code makes sure that you are peeking for first time and also if you have never peeked before  



           if (datingservicecontext.AddPeek(model.ViewerProfileDetails.profile.ProfileID,model.ProfileDetails.ProfileID))
           {
               
               var localEmailService = new LocalEmailService();
               Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfilePeekReceived);
               localEmailService.SendEmailMessage(Email);

               return true;
           }

           return false;
       }

       [Authorize]
       public bool AddBlock(string ProfileID, string TargetProfileID)
       {
           return datingservicecontext.Addblock(ProfileID, TargetProfileID);
       }
       [Authorize]
       public bool AddLike(string ProfileID, string TargetProfileID)
       {
           return datingservicecontext.AddLike(ProfileID, TargetProfileID);
       }      

        //methods to uopdate if type was looked or viewd
       [Authorize]
       public bool UpdateIntrestViewStatus(string ProfileID, string IntrestSenderID)
       {
           return datingservicecontext.UpdateInterestViewStatus(ProfileID, IntrestSenderID);

       }
       [Authorize]
       public bool UpdatePeekViewStatus(string ProfileID, string ProfileViewerID)
       {
           return datingservicecontext.UpdatePeekViewStatus(ProfileID, ProfileViewerID);

       }       
       [Authorize]
       public bool UpdateLikeViewStatus(string ProfileID, string ProfileLikerID)
       {

           return datingservicecontext.UpdateLikeViewStatus(ProfileID, ProfileLikerID);
       }      

        //Removals done here
       [Authorize]
       public bool RemoveOneOfMyIntrest(string ProfileID, string InterestID )
       {
           return datingservicecontext.RemoveInterest(ProfileID,InterestID);
       }     
       [Authorize]
       public bool RemoveOneOfMyLikes(string ProfileID, string _LikeID)
       {

           return datingservicecontext.RemoveLike(ProfileID,_LikeID);
       }
       [Authorize]
       public bool RemoveOneOfMyBlocks(string ProfileID, string targetprofileID)
       {

           return datingservicecontext.Removeblock(ProfileID,targetprofileID);
       }      

        #endregion

    }
}