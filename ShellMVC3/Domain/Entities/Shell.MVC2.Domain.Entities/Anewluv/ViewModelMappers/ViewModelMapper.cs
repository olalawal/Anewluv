using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using MvcContrib.Pagination;

using Omu.Awesome.Core;
//2-15-2012 added so we can build fake models for guests


using Ninject.Web.Mvc;
using Ninject;
using Shell.MVC2.Models;

using Shell.MVC2.Infastructure.JanRainAuthentication;


namespace Shell.MVC2.Models
{

    //maps viewmodels to thier repository methods
    public class ViewModelMapper
    {



        private MembersRepository membersrepository;
      

      /// <summary>
      /// maps user profile data to members viewmdel, create searchsettings table if it is empty
      /// </summary>
      /// <param name="ProfileID"></param>
      /// <returns></returns>
      public MembersViewModel MapMember(string ProfileID)
        {




            IKernel kernel = new StandardKernel();
           

            membersrepository = kernel.Get<MembersRepository>();
            MembersViewModel model = new MembersViewModel();            
            
            // IEnumerable<CityStateProvince> CityStateProvince ;
            MailModelRepository mailrepository = new MailModelRepository();
            //var myProfile = membersrepository.GetProfileDataByProfileID(ProfileID).profile;
           // var perfectmatchsearchsettings = membersrepository.GetPerFectMatchSearchSettingsByProfileID(ProfileID);

           // model.Profile = myProfile;
          //Profile data will be on the include
            model.profiledata =membersrepository.GetProfileDataByProfileID(ProfileID);
           
          //TO DO this should be a try cacth with exception handling


            try
            {
                //TO DO do away with this since we already have the profile via include from the profile DATA
                model.Profile = model.profiledata.profile;

                //   model.profiledata.SearchSettings(perfectmatchsearchsettings);
                //4-28-2012 added mapping for profile visiblity
                model.ProfileVisiblity = model.profiledata.ProfileVisiblitySetting;

                //on first load this should always be false
                model.ProfileStatusMessageShown = false;

                model.MyGenderID = model.profiledata.GenderID;
                //this should come from search settings eventually on the full blown model of this.
                //create hase list of genders they are looking for, if it is null add the default
                model.LookingForGendersID = (model.profiledata.SearchSettings.FirstOrDefault() != null) ?
                new HashSet<int>(model.profiledata.SearchSettings.FirstOrDefault().SearchSettings_Genders.Select(c => c.GenderID.GetValueOrDefault())) : null;
                if (model.LookingForGendersID.Count  == 0)
                {
                    model.LookingForGendersID.Add(Extensions.GetLookingForGenderID(model.profiledata.GenderID));

                }

                //set selected value
                //model.Countries. = model.profiledata.CountryID;

                //geographical data poulated here
                model.MyCountryName = membersrepository.GetMyCountry(model.profiledata);
                model.MyCountryID = model.profiledata.CountryID;
                model.MyCity = model.profiledata.City;




                //TO DO items need to be populated with real values, in this case change model to double for latt
                model.MyLatitude = model.profiledata.Latitude.ToString() ; //model.Lattitude
                model.MyLongitude = model.profiledata.Longitude.ToString();
                //update 9-21-2011 get fro search settings
                model.MaxDistanceFromMe = model.profiledata.SearchSettings.FirstOrDefault() != null ? model.profiledata.SearchSettings.FirstOrDefault().DistanceFromMe.GetValueOrDefault() : 500;


                //mail counters
                model.MyMailCount = mailrepository.MailCount("Sent", model.Profile.ProfileID).ToString();
                model.WhoMailedMe = mailrepository.MailCount("Inbox", model.Profile.ProfileID).ToString();
                model.WhoMailedMeNewCount = mailrepository.NewMailCount("Inbox", model.Profile.ProfileID).ToString();

                //model.WhoMailedMeNewCount =  
                //interests
                model.MyIntrestCount = membersrepository.GetMyInterestCount(model.profiledata);
                model.WhoisInterestedInMeCount = membersrepository.GetWhoisInterestedinMeCount(model.profiledata);
                model.WhoisInterestedInMeNewCount = membersrepository.GetWhoisInterestedinMeNewCount(model.profiledata);
                //peeks
                model.MyPeeksCount = membersrepository.GetMypeekCount(model.profiledata);
                model.WhopeekededatMeCount = membersrepository.GetWhoPeekedatmeCount(model.profiledata); ;
                model.WhopeekedatMeNewCount = membersrepository.GetWhoPeekedatmeNewCount(model.profiledata);
                //likes
                model.WhoLikesMeNewCount =membersrepository.GetWhoLikesMeCount(model.profiledata);
                model.WholikesMeCount = membersrepository.GetWhoLikesMeCount(model.profiledata);
                model.WhoIlikeCount = membersrepository.GetWhoIlikeCount(model.profiledata);

                //blocks
                model.MyBlockCount = membersrepository.GetMyblockCount(model.profiledata);

                //instantiate models for city state province and quick search
                // get users search setttings
                //model.MyQuickSearch = QuickSearchModel;



                // now instantiate city state province
                // model.MyQuickSearch.MySelectedCityStateProvince = CityStateProvince();
                model = membersrepository.GetDefaultQuickSearchSettingsMembers(model);

                //added 5-10-2012
                //we dont want to add search setttings to the members model?
                //TO do remove profiledata at some point
                //check if the user has a profile search settings value in stored DB if not add one and save it
                if (model.profiledata.SearchSettings.Count == 0)
                {
                    membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(ProfileID);
                    //update the profile data with the updated value
                    //TO DO stop storing profileData
                    model.profiledata = membersrepository.GetProfileDataByProfileID(ProfileID);
                    
                }

                //*** start binding collections here ******
                //do this last since we need values populated first


                // var pp = new PaginatedList<MemberSearchViewModel>();

                //sets up the inital paging for your matches
                //  var productPagedList = pp.GetPageableList(model.MyMatches, 1,4);
                //   MyMatchesPaged.AsPagination(1, 4);

                // model.MyMatches = productPagedList;  // set quick matches

                return model;
            }
            //TO DO log the error
            catch
            {


            }
            

            return null;


        }

      //public MembersViewModel ReMapLoggedInMember(MembersViewModel OldModel)
      //{

      //    MembersRepository membersrepository = new MembersRepository();


      //    MembersViewModel model = new MembersViewModel();
      //    List<MemberSearchViewModel> MyMatchesPaged; //list to hold current quick matches
      //    QuickSearchModel QuickSearchModel = new QuickSearchModel();
      //    // IEnumerable<CityStateProvince> CityStateProvince ;
      //    MailModelRepository mailrepository = new MailModelRepository();

      //    //get updated profile i.e maybe it changed
      //    var myProfile = membersrepository.GetProfileDataByProfileID(OldModel.Profile.ProfileID).profile;

      //    //reset UI values that should not change        
      //    model.LookingForAgeFrom = OldModel.LookingForAgeFrom;
      //    model.LookingForAgeTo = OldModel.LookingForAgeTo;
      //    model.LookingForGenderID = OldModel.LookingForGenderID;
      //    model.MaxDistanceFromMe = OldModel.MaxDistanceFromMe;     
      //    model.MyCountryID = OldModel.MyCountryID;
      //    model.MyCountryName = OldModel.MyCountryName;
      //    model.MyPostalCode = OldModel.MyPostalCode;
      //    model.MyPostalCodeStatus = OldModel.MyPostalCodeStatus;
      //    model.MyCity = OldModel.profiledata.City;
      //    // get users former  search setttings
      //    model.MyQuickSearch = OldModel.MyQuickSearch;


      //    //update the rest of the model from dataabase since it could have changed
      //    model.Profile = myProfile;            
      //    model.profiledata = membersrepository.GetProfileDataByProfileID(model.Profile.ProfileID);
      //    model.MyGenderID = model.profiledata.GenderID;
      //    //this should come from search settings eventually on the full blown model of this.
      //   //model.LookingForGenderID = Extensions.GetLookingForGenderID(model.profiledata.GenderID);
      //    //set selected value
      //    //model.Countries. = model.profiledata.CountryID;

      //    //geographical data poulated here
      //   // model.MyCountryName = membersrepository.GetMyCountry(model.profiledata);

      //   // model.MyCountryID = model.profiledata.CountryID;
      //    //model.MyCity = model.profiledata.City;
      //    // model.MaxDistanceFromMe = 2000;


      //    //TO DO items need to be populated with real values, in this case change model to double for latt
      //    model.MyLatitude = (double)model.profiledata.Latitude; //model.Lattitude
      //    model.MyLongitude = (double)model.profiledata.Longitude;
       


      //    //mail counters
      //    model.MyMailCount = mailrepository.MailCount("Sent", model.Profile.ProfileID).ToString();
      //    model.WhoMailedMe = mailrepository.MailCount("Inbox", model.Profile.ProfileID).ToString();
      //    model.WhoMailedMeNewCount = mailrepository.NewMailCount("Inbox", model.Profile.ProfileID).ToString();

      //    //model.WhoMailedMeNewCount =  
      //    //interests
      //    model.MyIntrestCount = membersrepository.GetMyInterestCount(model.profiledata);
      //    model.WhoisInterestedInMeCount = membersrepository.GetWhoisInterestedinMeCount(model.profiledata);
      //    model.WhoisInterestedInMeNewCount = membersrepository.GetWhoisInterestedinMeNewCount(model.profiledata);
      //    //peeks
      //    model.MyPeeksCount = membersrepository.GetMypeekCount(model.profiledata);
      //    model.WhopeekededatMeCount = membersrepository.GetWhoPeekedatmeCount(model.profiledata); ;
      //    model.WhopeekedatMeNewCount = membersrepository.GetWhoPeekedatmeNewCount(model.profiledata);
      //    //blocks
      //    model.MyBlockCount = membersrepository.GetMyblockCount(model.profiledata);
      //    //likes
      //    model.WholikesMeCount = membersrepository.GetWhoLikesMeCount(model.profiledata);
      //    model.WhoLikesMeNewCount = membersrepository.GetWhoLIkesMeNewCount(model.profiledata);
        


      //    // now instantiate city state province
      //    // model.MyQuickSearch.MySelectedCityStateProvince = CityStateProvince();
      //    model = membersrepository.GetDefaultSearchSettingsMembers(model);
          
      //    //*** start binding collections here ******
      //    //do this last since we need values populated first

      //   model.Ages = sharedrepository.AgesSelectList;
      //   model.Genders = sharedrepository.GendersSelectList;
      //   model.Countries = sharedrepository.CountrySelectList(model);

      //    //build quick matches
      //    var Profiles = membersrepository.GetQuickMatches(model);
      //    //save this as the cleant un paged list
      //   // model.MyMatches = Profiles.ToList();


      //    // PaginatedList<ProfileBrowseModel> PagerProfileBrowse;
      //    //get the user's matches
      //    //user cannot change page size on this 
      //    MyMatchesPaged = Profiles.ToList();
      //    var pp = new PaginatedList<MemberSearchViewModel>();

      //    //sets up the inital paging for your matches
      //  //  var productPagedList = pp.GetPageableList(model.MyMatches, 1, 4);
      //    //   MyMatchesPaged.AsPagination(1, 4);

      //  //  model.MyMatches = productPagedList;  // set quick matches

      //    return model;


      //}

      public MembersViewModel MapGuest()
      {
          MembersViewModel model = new MembersViewModel();

          MembersRepository membersrepository = new MembersRepository();

          //TO DO create a function to pupulate these if needed
          //add fake profile and fake profiledata to this code
         // profile fakeprofile = new profile();
         // profiledata fakeprofiledata = new profiledata();
          //populate fake data
          //fakeprofiledata.City = "ALL";
         // fakeprofiledata.CountryID = 43;
          


        //  model.profiledata = fakeprofiledata;
        //  model.Profile = fakeprofile;
    
         // List<MemberSearchViewModel> MyMatchesPaged; //list to hold current quick matches
          QuickSearchModel QuickSearchModel = new QuickSearchModel();
          // IEnumerable<CityStateProvince> CityStateProvince ;

           

        //  model.Profile = myProfile;
       //   model.profiledata = membersrepository.GetProfileDataByProfileID(model.Profile.ProfileID);



        //  model.MyInterstCount = membersrepository.GetMyInterestCount(model.profiledata);
        //  model.MyIntrestNewCount = membersrepository.GetMyInterestNewCount(model.profiledata);


        //  model.MyGenderID = model.profiledata.GenderID;
          //this should come from search settings eventually on the full blown model of this.
       //   model.LookingForGenderID = Extensions.GetLookingForGenderID(model.profiledata.GenderID);
          //set selected value
          //model.Countries. = model.profiledata.CountryID;

          //geographical data poulated here
          //TO DO get this from IP address i think or cookie
          //model.MyCountryName = membersrepository.GetMyCountry(model.profiledata);

         // model.MyCountryID = model.profiledata.CountryID;
         // model.MyCity = model.profiledata.City;



          //TO DO items need to be populated with real values, in this case change model to double for latt
         // model.MyLatitude = (double)model.profiledata.Latitude; //model.Lattitude
          //model.MyLongitude = (double)model.profiledata.Longitude;
        // model.MaxDistanceFromMe = 2000;
          


          //instantiate models for city state province and quick search
          // get users search setttings
           model.MyQuickSearch = QuickSearchModel;


          // now instantiate city state province
          // model.MyQuickSearch.MySelectedCityStateProvince = CityStateProvince();
        //  model = membersrepository.GetDefaultSearchSettingsGuest(model);


          //*** start binding collections here ******
          //do this last since we need values populated first

          //model.Ages = sharedrepository.AgesSelectList;
          //model.Genders = sharedrepository.GendersSelectList;
          //model.Countries = sharedrepository.CountrySelectList(model);

          //build quick matches
         // var Profiles = membersrepository.GetQuickMatches(model);
          //save this as the cleant un paged list
        //  model.MyMatches = Profiles.ToList();



          //get the user's matches
          //user cannot change page size on this 
         // MyMatchesPaged = Profiles.ToList();
       //   var productPagedList = MyMatchesPaged
        //       .AsPagination(1, 4);

        //  model.MyMatchesPaged = productPagedList;  // set quick matches

          return model;



       //   model.MyMatchesPaged = null;
        //  model.MyMatches = null;
         // return model;
      }

      public RegisterModel MapRegistration(MembersViewModel membersmodel)
      {

          // MembersRepository membersrepository = new MembersRepository();


          RegisterModel model = new RegisterModel();
          //QuickSearchModel QuickSearchModel = new QuickSearchModel();
          // IEnumerable<CityStateProvince> CityStateProvince ;
          model.City = membersmodel.MyQuickSearch.MySelectedCity;
          model.Country = membersmodel.MyQuickSearch.MySelectedCountryName;
          model.longitude = membersmodel.MyQuickSearch.MySelectedLongitude;
          model.lattitude = membersmodel.MyQuickSearch.MySelectedLongitude;
          model.PostalCodeStatus = membersmodel.MyQuickSearch.MySelectedPostalCodeStatus;

          // model.SecurityAnswer = "moma";
          //5/8/2011  set other defualt values here
          //model.RegistrationPhotos.PhotoStatus = "";
          // model.PostalCodeStatus = false;
          return model;

      }

      public RegisterModel MapJainRainRegistration(RpxProfile profile,MembersViewModel membersmodel)
      {

         // MembersRepository membersrepository = new MembersRepository();
          IKernel kernel = new StandardKernel();
          membersrepository = kernel.Get<MembersRepository>();

          RegisterModel model = new RegisterModel();
          //QuickSearchModel QuickSearchModel = new QuickSearchModel();
          // IEnumerable<CityStateProvince> CityStateProvince ;
          model.openidIdentifer = profile.Identifier;
          model.openidProvider = profile.ProviderName;


          //model.Ages = sharedrepository.AgesSelectList();
         // model.Genders = sharedrepository.GendersSelectList();
         // model.Countries = sharedrepository.CountrySelectList();
         // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
          //test values
          model.BirthDate = DateTime.Parse(profile.birthday);
          
          model.Email = profile.verifiedEmail;
          model.ConfirmEmail = profile.verifiedEmail;
          model.Gender  =  Extensions.ConvertGenderName(profile.gender).ToString() ;


         // model.Password = "kayode02";
          //model.ConfirmPassword = "kayode02";
          model.ScreenName = profile.DisplayName;
          model.UserName =  profile.PreferredUsername;
          model.City =  membersmodel.MyCityStateProvince ;
       
          
          model.Country = membersmodel.MyCountryName;
          model.longitude = Convert.ToDouble(membersmodel.MyLongitude) ;
          model.lattitude = Convert.ToDouble(membersmodel.MyLatitude );
          model.PostalCodeStatus =  membersmodel.MyPostalCodeStatus ;
          model.ZipOrPostalCode =  membersmodel.MyPostalCode  ;
          
     
          //added passwords temporary hack
          model.Password = "ssoUser";
          
          //5/29/2012
         
          //get the photo info
         // model.SecurityAnswer = "moma";
          //5/8/2011  set other defualt values here
          //model.RegistrationPhotos.PhotoStatus = "";
          // model.PostalCodeStatus = false;
          PhotoViewModel photoVM = new PhotoViewModel();
          //initlaize photos object
          photoVM.Photos = new List<Photo>();
          Shell.MVC2.Models.Photo photo = new Shell.MVC2.Models.Photo();
          //right now we are only uploading one photo 
          if (profile.photo != "")
          photo = (membersrepository.UploadProfileImage(profile.photo, profile.PreferredUsername));
          
          if (photo.ActualImage.Length > 0 )  
          {            
          photoVM.Photos.Add(photo);
           model.RegistrationPhotos = photoVM;
          }

          
         

          //make sure photos is not empty
        //  if (membersmodel.MyPhotos == null)
         // { //add new photo model to members model
          //    var photolist = new List<Photo>();
          //    membersmodel.MyPhotos = photolist;
         // }



          return model;

      }

      

      public RegisterModel MapRegistrationTest()
      {

          // MembersRepository membersrepository = new MembersRepository();


          RegisterModel model = new RegisterModel();
          //QuickSearchModel QuickSearchModel = new QuickSearchModel();
          // IEnumerable<CityStateProvince> CityStateProvince ;



          //model.Ages = sharedrepository.AgesSelectList();
         // model.Genders = sharedrepository.GendersSelectList();
         // model.Countries = sharedrepository.CountrySelectList();
         // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
          //test values
          model.BirthDate = DateTime.Parse("1/1/1983");
          
          model.Email = "ola_lawal@lyahoo.com";
          model.ConfirmEmail = "ola_lawal@lyahoo.com";
         // model.Gender = "Male";
          model.Password = "kayode02";
          model.ConfirmPassword = "kayode02";
          model.ScreenName = "test1";
          model.UserName = "olalaw";
         
         // model.SecurityAnswer = "moma";

          //5/8/2011  set other defualt values here
          //model.RegistrationPhotos.PhotoStatus = "";

          // model.PostalCodeStatus = false;


          return model;


      }


    }

    //maps guest viewmodels to thier repository methods
    public partial class GuestRepository
    {

        //private ProfileRepository profilerepository;
       // private int myMatchesPageSize = 4;
       // private int? MyMatchesCurrentPage = 1;

      




    }



}