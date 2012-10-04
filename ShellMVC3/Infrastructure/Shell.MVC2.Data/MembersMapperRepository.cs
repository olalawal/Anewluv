using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Runtime.Serialization;
using System.Text;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Infrastructure;
using Shell.MVC2.Interfaces;




namespace Shell.MVC2.Data
{
    public class MembersMapperRepository : IMembersMapperRepository
    {
        //TO DO do this a different way I think
        private IGeoRepository georepository;
        private IPhotoRepository photorepository;
        private IMemberRepository membersrepository;
        // private AnewluvContext _db;
        //TO DO move from ria servives
        private AnewluvContext  datingcontext;

       public MembersMapperRepository(IGeoRepository _georepository, IPhotoRepository _photorepository, IMemberRepository  _membersrepository,AnewluvContext _datingcontext )
        {
            georepository = _georepository;
            photorepository = _photorepository;
            membersrepository = _membersrepository;
            datingcontext = _datingcontext;
        }

        // constructor
        public MemberSearchViewModel GetMemberSearchViewModel(int profileId)
        {
            if (profileId != null)
            {


                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profiledata CurrentProfileData =membersrepository.getprofiledatabyprofileid(profileId); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                //  EditProfileRepository editProfileRepository = new EditProfileRepository();



                model.id = CurrentProfileData.id ;
                model.profiledata = CurrentProfileData;
                model.profile = CurrentProfileData.profile;
                model.stateprovince  = CurrentProfileData.stateprovince;
                model.postalcode  = CurrentProfileData.postalcode;
                model.countryid  = CurrentProfileData.countryid;
                model.genderid  = CurrentProfileData.gender.id ;
                model.birthdate = CurrentProfileData.birthdate;
                // modelprofile = CurrentProfileData.profile;
                model.longitude = (double)CurrentProfileData.longitude;
                model.latitude = (double)CurrentProfileData.latitude;
                //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.creationdate  = CurrentProfileData.profile.creationdate;
                model.city = Extensions.ReduceStringLength(CurrentProfileData.city, 11);
                model.lastlogindate =  CurrentProfileData.profile.logindate ;
                model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                model.online  = membersrepository.getuseronlinestatus(CurrentProfileData.id);
                // PerfectMatchSettings = CurrentProfileData.SearchSettings.First();
                //DistanceFromMe = 0  get distance from somwhere else
                //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                //or instead just have the photo the select zoom up
                int page = 1;
                int ps = 12;
               // var MyPhotos = editProfileRepository.MyPhotos(model.profile.username);
               // var Approved = editProfileRepository.GetApproved(MyPhotos, "Yes", page, ps);
               // var NotApproved = editProfileRepository.GetApproved(MyPhotos, "No", page, ps);
               // var Private = editProfileRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                model.profilephotos = photorepository.GetEditPhotoViewModel(model.profile.username, "Yes", "No", 3, page, ps);   //editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);

               return model;
            }
            return null;
        }
        public List<MemberSearchViewModel> GetMemberSearchViewModels(List<int> profileIds)
        {

            List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
            foreach (var item in profileIds)
            {

                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profiledata CurrentProfileData = membersrepository.getprofiledatabyprofileid(item); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                //  EditProfileRepository editProfileRepository = new EditProfileRepository();



                model.id = CurrentProfileData.id;
                model.profiledata = CurrentProfileData;
                model.profile = CurrentProfileData.profile;
                model.stateprovince = CurrentProfileData.stateprovince;
                model.postalcode = CurrentProfileData.postalcode;
                model.countryid = CurrentProfileData.countryid;
                model.genderid = CurrentProfileData.gender.id;
                model.birthdate = CurrentProfileData.birthdate;
                // modelprofile = CurrentProfileData.profile;
                model.longitude = (double)CurrentProfileData.longitude;
                model.latitude = (double)CurrentProfileData.latitude;
                //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.creationdate = CurrentProfileData.profile.creationdate;
                model.city = Extensions.ReduceStringLength(CurrentProfileData.city, 11);
                model.lastlogindate = CurrentProfileData.profile.logindate;
                model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                model.online = membersrepository.getuseronlinestatus(CurrentProfileData.id);
                // PerfectMatchSettings = CurrentProfileData.SearchSettings.First();
                //DistanceFromMe = 0  get distance from somwhere else
                //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                //or instead just have the photo the select zoom up
                int page = 1;
                int ps = 12;
                // var MyPhotos = editProfileRepository.MyPhotos(model.profile.username);
                // var Approved = editProfileRepository.GetApproved(MyPhotos, "Yes", page, ps);
                // var NotApproved = editProfileRepository.GetApproved(MyPhotos, "No", page, ps);
                // var Private = editProfileRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                model.profilephotos = photorepository.GetEditPhotoViewModel(model.profile.username, "Yes", "No", 3, page, ps);   //editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
             
            }
            return models;
        }
        public ProfileBrowseModel GetProfileBrowseModel(int viewerprofileId, int profileId)
        {



            var NewProfileBrowseModel = new ProfileBrowseModel
            {
                //TO Do user a mapper instead of a contructur and map it from the service
                //Move all this to a service
                ViewerProfileDetails = GetMemberSearchViewModel(viewerprofileId),
                ProfileDetails = GetMemberSearchViewModel(profileId)
            };

            //add in the ProfileCritera
            NewProfileBrowseModel.ProfileCriteria = GetProfileCriteriaModel(NewProfileBrowseModel.ProfileDetails);
            NewProfileBrowseModel.ViewerProfileCriteria = GetProfileCriteriaModel(NewProfileBrowseModel.ViewerProfileDetails);




            return NewProfileBrowseModel;
        }
        public List<ProfileBrowseModel> GetProfileBrowseModels(string viewerprofileId, List<int> profileIds)
        {
            List<ProfileBrowseModel> BrowseModels = new List<ProfileBrowseModel>();

            foreach (var item in profileIds)
            {
                var NewProfileBrowseModel = new ProfileBrowseModel
                {
                    //TO Do user a mapper instead of a contructur and map it from the service
                    //Move all this to a service
                    ViewerProfileDetails = GetMemberSearchViewModel(viewerprofileId),
                    ProfileDetails = GetMemberSearchViewModel(item)
                };

                //add in the ProfileCritera
                NewProfileBrowseModel.ProfileCriteria = GetProfileCriteriaModel(NewProfileBrowseModel.ProfileDetails);
                NewProfileBrowseModel.ViewerProfileCriteria =GetProfileCriteriaModel(NewProfileBrowseModel.ViewerProfileDetails);

                BrowseModels.Add(NewProfileBrowseModel);
            }

            return BrowseModels;
        }
        // constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public ProfileCriteriaModel GetProfileCriteriaModel(MemberSearchViewModel p)
        {
            //load postaldata context
            ProfileCriteriaModel CriteriaModel = new ProfileCriteriaModel();
            if (p != null)
            {

               // IKernel kernel = new StandardKernel();

                //load the geo service for quic user
               // IGeoRepository georepository = kernel.Get<IGeoRepository>();





                //instantiate models
                CriteriaModel.BasicSearchSettings = new SearchBasicSettings();
                CriteriaModel.AppearanceSearchSettings = new SearchAppearanceSettings();
                CriteriaModel.LifeStyleSearchSettings = new SearchLifeStyleSettings();
                CriteriaModel.CharacterSearchSettings = new SearchCharacterSettings();
                // IKernel kernel = new StandardKernel();
                //Get these initalized
                //  MembersRepository membersrepo = kernel.Get<MembersRepository>(); 

                //TO DO populate these values corrrectly
                //run a query h ere to populate these values 
                //Ethnicity =      p.ProfileData_Ethnicity.Where(
                //find a way to populate hoby, looking for and ethnicuty from the ProfileData_Ethncity and etc

                CriteriaModel.ScreenName = (p.screenname == null) ? Extensions.ReduceStringLength(p.profile.screenname, 10) : Extensions.ReduceStringLength(p.screenname, 10);
                CriteriaModel.AboutMe  = (p.profiledata.aboutme  == null || p.profiledata.aboutme == "Hello") ? "This is the description of the type of person I am looking for.. comming soon. For Now Email Me to find out more about me" : p.profiledata.aboutme;
                //  MyCatchyIntroLine = p.prMyCatchyIntroLine == null ? "Hi There!" : p.MyCatchyIntroLine;
                CriteriaModel.BodyType = (p.profiledata.bodytype == null | p.profiledata.bodytype.id   == null) ? "Ask Me" : p.profiledata.bodytype.description;
                CriteriaModel.EyeColor = (p.profiledata.eyecolor  == null | p.profiledata.eyecolor.id == null) ? "Ask Me" : p.profiledata.eyecolor.description ;
                // Ethnicity = p.CriteriaAppearance_Ethnicity == null ? "Ask Me" : p.CriteriaAppearance_Ethnicity.EthnicityName;
                CriteriaModel.HairColor = (p.profiledata.haircolor  == null | p.profiledata.haircolor.id  == null) ? "Ask Me" : p.profiledata.haircolor.description ;
                //TO DO determine weather metic or us sustem user //added 11-17-2011
                CriteriaModel.HeightByCulture = (p.profiledata.height  == null | p.profiledata.height  == 0) ? "Ask Me" : Extensions.ToFeetInches((double)p.profiledata.height );

                CriteriaModel.Exercise = (p.profiledata.exercise == null | p.profiledata.exercise.id == null) ? "Ask Me" : p.profiledata.exercise.description;
                CriteriaModel.Religion = (p.profiledata.religion  == null | p.profiledata.religion.id  == null) ? "Ask Me" : p.profiledata.religion.description;
                CriteriaModel.ReligiousAttendance = (p.profiledata.religiousattendance == null | p.profiledata.religiousattendance.id == null) ? "Ask Me" : p.profiledata.religiousattendance.description;
                CriteriaModel.Drinks = (p.profiledata.drinking == null | p.profiledata.drinking.id  == null) ? "Ask Me" : p.profiledata.drinking.description;
                CriteriaModel.Smokes = (p.profiledata.smoking  == null | p.profiledata.smoking.id  == null) ? "Ask Me" : p.profiledata.smoking.description  ;
                CriteriaModel.Humor = (p.profiledata.humor == null | p.profiledata.humor.id  == null) ? "Ask Me" : p.profiledata.humor.description ;
                // HotFeature = p.profiledata.CriteriaCharacter_HotFeature == null ? "Ask Me" : p.profiledata.CriteriaCharacter_HotFeature.HotFeatureName; 
                //   Hobby =  p.profiledata.CriteriaCharacter_Hobby == null ? "Ask Me" : p.profiledata.CriteriaCharacter_Hobby.HobbyName;
                CriteriaModel.PoliticalView =( p.profiledata.politicalview  == null | p.profiledata.politicalview  == null) ? "Ask Me" : p.profiledata.politicalview.description ;
                CriteriaModel.Diet = (p.profiledata.diet == null | p.profiledata.diet.id  == null) ? "Ask Me" : p.profiledata.diet.description ;
                //TO DO calculate this by bdate
                CriteriaModel.Sign = (p.profiledata.sign  == null | p.profiledata.sign.id == null) ? "Ask Me" : p.profiledata.sign.description ;
                CriteriaModel.IncomeLevel = (p.profiledata.incomelevel  == null | p.profiledata.incomelevel  == null) ? "Ask Me" : p.profiledata.incomelevel.description ;
                CriteriaModel.HaveKids = (p.profiledata.kidstatus  == null | p.profiledata.kidstatus.id  == null) ? "Ask Me" : p.profiledata.kidstatus.description ;
                CriteriaModel.WantsKids = (p.profiledata.wantsKidstatus == null | p.profiledata.wantsKidstatus.id == null) ? "Ask Me" : p.profiledata.wantsKidstatus.description ;
                CriteriaModel.EmploymentSatus = (p.profiledata.employmentstatus  == null | p.profiledata.employmentstatus.id == null) ? "Ask Me" : p.profiledata.employmentstatus.description;
                CriteriaModel.EducationLevel = (p.profiledata.educationlevel  == null | p.profiledata.educationlevel.Id  == null) ? "Ask Me" : p.profiledata.educationlevel.description ;
                CriteriaModel.Profession = (p.profiledata.profession  == null | p.profiledata.profession .id == null) ? "Ask Me" : p.profiledata.profession.description ;
                CriteriaModel.MaritalStatus = (p.profiledata.maritalstatus  == null | p.profiledata.maritalstatus.id == null) ? "Single" : p.profiledata.maritalstatus.description ;
                CriteriaModel.LivingSituation = (p.profiledata.livingsituation  == null | p.profiledata.livingsituation.id  == null) ? "Ask Me" : p.profiledata.livingsituation.description;
                //special case for ethnicty since they can have mutiples ?
                //loop though ethnicty thing I guess ?  
                //8/11/2011 have to loop though since these somehow get detached sometimes
                //Ethnicity = p.profiledata.ProfileData_Ethnicity;

                foreach (var item in p.profiledata.ethnicities )
                {
                    CriteriaModel.Ethnicity.Add(item.ethnicty.description );
                }

                foreach (var item in p.profiledata.hobbies)
                {
                    CriteriaModel.Hobbies.Add(item.hobby.description);
                }

                foreach (var item in p.profiledata.lookingfor )
                {
                    CriteriaModel.LookingFor.Add(item.lookingfor.description );
                }

                foreach (var item in p.profiledata.hotfeatures)
                {
                    CriteriaModel.HotFeature.Add(item.hotfeature.description );
                }

                //handle perfect match settings here .
                // first load perfect match settings for this user from database
                //set defaults if no values are available
                var PerfectMatchSettings = p.profiledata.searchsettings.First();


                //basic search settings here
                CriteriaModel.BasicSearchSettings.MaxDistanceFromMe = (PerfectMatchSettings == null || PerfectMatchSettings.distancefromme == null) ? 500 : PerfectMatchSettings.distancefromme ;
                CriteriaModel.BasicSearchSettings.SeekingAgeMin = (PerfectMatchSettings == null || PerfectMatchSettings.agemin == null) ? 18 : PerfectMatchSettings.agemin;
                CriteriaModel.BasicSearchSettings.SeekingAgeMax = (PerfectMatchSettings == null || PerfectMatchSettings.agemax == null) ? 99 : PerfectMatchSettings.agemax;

                //TO DO add this to search settings for now use what is in profiledata
                //These will come from search settings table in the future at some point
                CriteriaModel.BasicSearchSettings.Country = georepository.GetCountryNameByCountryId((byte)p.profiledata.countryid);  //TO DO allow a range of countries to be selected i.e multi select
                CriteriaModel.BasicSearchSettings.CityStateProvince = (p.profiledata.stateprovince == null) ? "Ask Me" : p.profiledata.stateprovince;
                CriteriaModel.BasicSearchSettings.PostalCode  = p.profiledata.postalcode;  //this could be for countries withoute p codes

                //populate list values
                foreach (var item in PerfectMatchSettings.genders )
                {
                    CriteriaModel.BasicSearchSettings.GendersList.Add(item.gender.description );
                }
                //appearance search settings here
                CriteriaModel.AppearanceSearchSettings.HeightMax = (PerfectMatchSettings == null || PerfectMatchSettings.heightmax == null) ? Extensions.ToFeetInches(48) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmax);
                CriteriaModel.AppearanceSearchSettings.HeightMin = (PerfectMatchSettings == null || PerfectMatchSettings.heightmin == null) ? Extensions.ToFeetInches(89) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmin);

                foreach (var item in PerfectMatchSettings.ethnicitys )
                {
                    CriteriaModel.AppearanceSearchSettings.ethnicitylist.Add(item.ethnicity.description );
                }

                foreach (var item in PerfectMatchSettings.bodytypes )
                {
                    CriteriaModel.AppearanceSearchSettings.bodytypeslist.Add(item.bodytype.description );
                }

                foreach (var item in PerfectMatchSettings.eyecolors )
                {
                    CriteriaModel.AppearanceSearchSettings.eyecolorlist.Add(item.eyecolor.description );
                }

                foreach (var item in PerfectMatchSettings.haircolors )
                {
                    CriteriaModel.AppearanceSearchSettings.haircolorlist.Add(item.haircolor.description );
                }


                foreach (var item in PerfectMatchSettings.hotfeature )
                {
                    CriteriaModel.AppearanceSearchSettings.hotfeaturelist.Add(item.hotfeature.description);
                }

                //populate lifestyle values here

                foreach (var item in PerfectMatchSettings.educationlevels )
                { CriteriaModel.LifeStyleSearchSettings.educationlevellist.Add(item.educationlevel.description ); }

                foreach (var item in PerfectMatchSettings.lookingfor )
                { CriteriaModel.LifeStyleSearchSettings.lookingforlist.Add(item.lookingfor.description ); }

                foreach (var item in PerfectMatchSettings.employmentstatus )
                { CriteriaModel.LifeStyleSearchSettings.employmentstatuslist.Add(item.employmentstatus.description); }

                foreach (var item in PerfectMatchSettings.havekids )
                { CriteriaModel.LifeStyleSearchSettings.havekidslist.Add(item.havekids.description ); }

                foreach (var item in PerfectMatchSettings.livingstituations )
                { CriteriaModel.LifeStyleSearchSettings.livingsituationlist.Add(item.livingsituation.description ); }

                foreach (var item in PerfectMatchSettings.maritalstatuses )
                { CriteriaModel.LifeStyleSearchSettings.maritalstatuslist.Add(item.maritalstatus.description ); }

                foreach (var item in PerfectMatchSettings.wantkids )
                { CriteriaModel.LifeStyleSearchSettings.wantskidslist.Add(item.wantskids.description ); }

                foreach (var item in PerfectMatchSettings.professions )
                { CriteriaModel.LifeStyleSearchSettings.professionlist.Add(item.profession.description ); }

                foreach (var item in PerfectMatchSettings.incomelevels )
                { CriteriaModel.LifeStyleSearchSettings.incomelevellist.Add(item.incomelevel.description ); }

                //Character settings for search here
                foreach (var item in PerfectMatchSettings.diets )
                { CriteriaModel.CharacterSearchSettings.dietlist.Add(item.diet.description ); }

                foreach (var item in PerfectMatchSettings.humors )
                { CriteriaModel.CharacterSearchSettings.humorlist.Add(item.humor.description ); }

                foreach (var item in PerfectMatchSettings.hobbies )
                { CriteriaModel.CharacterSearchSettings.hobbylist.Add(item.hobby.description ); }

                foreach (var item in PerfectMatchSettings.drinks )
                { CriteriaModel.CharacterSearchSettings.drinkslist.Add(item.drink.description ); }

                //FIX after database update
                foreach (var item in PerfectMatchSettings.exercises )
                { CriteriaModel.CharacterSearchSettings.exerciselist.Add(item.exercise.description ); }

                foreach (var item in PerfectMatchSettings.smokes )
                { CriteriaModel.CharacterSearchSettings.smokeslist.Add(item.smokes.description ); }

                foreach (var item in PerfectMatchSettings.signs )
                { CriteriaModel.CharacterSearchSettings.signlist.Add(item.sign.description ); }

                foreach (var item in PerfectMatchSettings.politicalviews )
                { CriteriaModel.CharacterSearchSettings.politicalviewlist.Add(item.politicalview.description ; }

                foreach (var item in PerfectMatchSettings.religions )
                { CriteriaModel.CharacterSearchSettings.religionlist.Add(item.religion.description); }

                foreach (var item in PerfectMatchSettings.religiousattendances )
                { CriteriaModel.CharacterSearchSettings.religiousattendancelist.Add(item.religiousattendance.description ); }


                return CriteriaModel;

            }
            else
            {
                CriteriaModel.BodyType = "NA";
                CriteriaModel.EyeColor = "NA";
                CriteriaModel.Ethnicity = null;
                CriteriaModel.HairColor = "NA";
                CriteriaModel.Exercise = "NA";
                CriteriaModel.Religion = "NA";
                CriteriaModel.ReligiousAttendance = "NA";
                CriteriaModel.Drinks = "NA";
                CriteriaModel.Smokes = "NA";
                CriteriaModel.Humor = "NA";
                // HotFeature = "NA";
                //Hobby = "NA";
                CriteriaModel.PoliticalView = "NA";
                CriteriaModel.Diet = "NA";
                CriteriaModel.Sign =
                CriteriaModel.IncomeLevel = "NA";
                CriteriaModel.HaveKids = "NA";
                CriteriaModel.WantsKids = "NA";
                CriteriaModel.EmploymentSatus = "NA";
                CriteriaModel.EducationLevel = "NA";
                CriteriaModel.Profession = "NA";
                CriteriaModel.MaritalStatus = "Single";
                CriteriaModel.LivingSituation = "NA";

                return CriteriaModel;
            }

        }
        //use an overload to return values if a user is not logged in i.e
        //no current profileData exists to retrive
        public ProfileCriteriaModel GetProfileCriteriaModel()
        {

            //build defualt empties for list values
            //  List<ProfileData_Ethnicity> EmptyEthnicty = new List<ProfileData_Ethnicity>();
            // Ethnicity.Add(new ProfileData_Ethnicity Temp)
            // EmptyEthnicty.EthnicityID = 1;
            ProfileCriteriaModel CriteriaModel = new ProfileCriteriaModel();

            CriteriaModel.BodyType = "NA";
            CriteriaModel.EyeColor = "NA";
            CriteriaModel.Ethnicity = null;
            CriteriaModel.HairColor = "NA";
            CriteriaModel.Exercise = "NA";
            CriteriaModel.Religion = "NA";
            CriteriaModel.ReligiousAttendance = "NA";
            CriteriaModel.Drinks = "NA";
            CriteriaModel.Smokes = "NA";
            CriteriaModel.Humor = "NA";
            // HotFeature = "NA";
            //Hobby = "NA";
            CriteriaModel.PoliticalView = "NA";
            CriteriaModel.Diet = "NA";
            CriteriaModel.Sign =
            CriteriaModel.IncomeLevel = "NA";
            CriteriaModel.HaveKids = "NA";
            CriteriaModel.WantsKids = "NA";
            CriteriaModel.EmploymentSatus = "NA";
            CriteriaModel.EducationLevel = "NA";
            CriteriaModel.Profession = "NA";
            CriteriaModel.MaritalStatus = "Single";
            CriteriaModel.LivingSituation = "NA";

            return CriteriaModel;

        }


        //gets search settings
        //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
        public MembersViewModel GetDefaultQuickSearchSettingsMembers(MembersViewModel Model)
        {
            QuickSearchModel QuickSearchModel = new QuickSearchModel();
           // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
            //set deafult paging or pull from DB
            QuickSearchModel.mySelectedPageSize = 4;
            QuickSearchModel.mySelectedCurrentPage = 1;
            //added state province with comma 

            QuickSearchModel.MySelectedCity = Model.Profile.profiledata.city;
            QuickSearchModel.MySelectedMaxDistanceFromMe = Model.Profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme != null ? Model.MaxDistanceFromMe : 1000;

            QuickSearchModel.MySelectedFromAge = Model.Profile.profilemetadata.searchsettings.FirstOrDefault().agemin != null ? Model.Profile.profilemetadata.searchsettings.FirstOrDefault().agemin.GetValueOrDefault() : 18;
            QuickSearchModel.MyselectedToAge = Model.Profile.profilemetadata.searchsettings.FirstOrDefault().agemax != null ? Model.Profile.profilemetadata.searchsettings.FirstOrDefault().agemax.GetValueOrDefault() : 99; ;


            QuickSearchModel.MySelectedIamGenderID = Model.profiledata.gender.id ;
            QuickSearchModel.MySelectedCityStateProvince = Model.profiledata.city + "," + Model.profiledata.stateprovince; ;
            //TO DO convert genders to a list of genders 
            QuickSearchModel.MySelectedSeekingGenderID = Extensions.GetLookingForGenderID(Model.profiledata.gender.id);
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

    }
}
