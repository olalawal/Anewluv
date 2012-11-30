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

using Shell.MVC2.AppFabric;



namespace Shell.MVC2.Data
{
    public class MembersMapperRepository : MemberRepositoryBase, IMembersMapperRepository
    {
        //TO DO do this a different way I think
        private IGeoRepository georepository;
        private IPhotoRepository photorepository;
        private IMemberRepository membersrepository;
        private IMemberActionsRepository memberactionsrepository;
        private IMailRepository mailrepository;
        // private AnewluvContext _db;
        //TO DO move from ria servives
       // private AnewluvContext  datingcontext;

       public MembersMapperRepository(IGeoRepository _georepository, IPhotoRepository
           _photorepository, IMemberRepository _membersrepository, IMemberActionsRepository
           _memberactionsrepository, IMailRepository _mailrepository, AnewluvContext datingcontext)
            : base(datingcontext)
        {
            georepository = _georepository;
            photorepository = _photorepository;
            membersrepository = _membersrepository;
            memberactionsrepository = _memberactionsrepository ;
            mailrepository = _mailrepository;
           // datingcontext = _datingcontext;
        }

        // constructor
        public MemberSearchViewModel getmembersearchviewmodel(int profileId)
        {
            if (profileId != null)
            {


                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profiledata Currentprofiledata =membersrepository.getprofiledatabyprofileid(profileId); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                //  EditProfileRepository editProfileRepository = new EditProfileRepository();



                model.id = Currentprofiledata.profile_id  ;
                model.profiledata = Currentprofiledata;
                model.profile = Currentprofiledata.profile;
                model.stateprovince  = Currentprofiledata.stateprovince;
                model.postalcode  = Currentprofiledata.postalcode;
                model.countryid  = Currentprofiledata.countryid;
                model.genderid  = Currentprofiledata.gender.id ;
                model.birthdate = Currentprofiledata.birthdate;
                // modelprofile = Currentprofiledata.profile;
                model.longitude = (double)Currentprofiledata.longitude;
                model.latitude = (double)Currentprofiledata.latitude;
                //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.creationdate  = Currentprofiledata.profile.creationdate;
                model.city = Extensions.ReduceStringLength(Currentprofiledata.city, 11);
                model.lastlogindate =  Currentprofiledata.profile.logindate ;
                model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                model.online  = membersrepository.getuseronlinestatus(Currentprofiledata.profile_id );
                // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
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
                model.profilephotos = photorepository.getpagededitphotoviewmodelbyprofileid(model.profile.id, page, ps);   //editProfileRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);

               return model;
            }
            return null;
        }
        public List<MemberSearchViewModel> getmembersearchviewmodels(List<int> profileIds)
        {

            List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
            foreach (var item in profileIds)
            {

                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profiledata Currentprofiledata = membersrepository.getprofiledatabyprofileid(item); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                //  EditProfileRepository editProfileRepository = new EditProfileRepository();



                model.id = Currentprofiledata.profile_id ;
                model.profiledata = Currentprofiledata;
                model.profile = Currentprofiledata.profile;
                model.stateprovince = Currentprofiledata.stateprovince;
                model.postalcode = Currentprofiledata.postalcode;
                model.countryid = Currentprofiledata.countryid;
                model.genderid = Currentprofiledata.gender.id;
                model.birthdate = Currentprofiledata.birthdate;
                // modelprofile = Currentprofiledata.profile;
                model.longitude = (double)Currentprofiledata.longitude;
                model.latitude = (double)Currentprofiledata.latitude;
                //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.creationdate = Currentprofiledata.profile.creationdate;
                model.city = Extensions.ReduceStringLength(Currentprofiledata.city, 11);
                model.lastlogindate = Currentprofiledata.profile.logindate;
                model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                model.online = membersrepository.getuseronlinestatus(Currentprofiledata.profile_id );
                // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
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
                model.profilephotos = photorepository.getpagededitphotoviewmodelbyprofileid(model.profile.id, page, ps);

            }
            return models;
        }
        public ProfileBrowseModel getprofilebrowsemodel(int viewerprofileId, int profileId)
        {



            var NewProfileBrowseModel = new ProfileBrowseModel
            {
                //TO Do user a mapper instead of a contructur and map it from the service
                //Move all this to a service
                ViewerProfileDetails = getmembersearchviewmodel(viewerprofileId),
                ProfileDetails = getmembersearchviewmodel(profileId)
            };

            //add in the ProfileCritera
            NewProfileBrowseModel.ViewerProfileCriteria = getprofilecriteriamodel(viewerprofileId);
            NewProfileBrowseModel.ProfileCriteria = getprofilecriteriamodel(profileId);           


            return NewProfileBrowseModel;
        }
        //returns a list of profile browsemodles for a given user
        public List<ProfileBrowseModel> getprofilebrowsemodels(int viewerprofileId, List<int> profileIds)
        {
            List<ProfileBrowseModel> BrowseModels = new List<ProfileBrowseModel>();

            foreach (var item in profileIds)
            {
                var NewProfileBrowseModel = new ProfileBrowseModel
                {
                    //TO Do user a mapper instead of a contructur and map it from the service
                    //Move all this to a service
                    ViewerProfileDetails = getmembersearchviewmodel(viewerprofileId),
                    ProfileDetails = getmembersearchviewmodel(item)



                };

                //add in the ProfileCritera
                NewProfileBrowseModel.ViewerProfileCriteria = getprofilecriteriamodel(viewerprofileId);
                NewProfileBrowseModel.ProfileCriteria = getprofilecriteriamodel(item);
          

                BrowseModels.Add(NewProfileBrowseModel);
            }

            return BrowseModels;
        }
        // constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public ProfileCriteriaModel getprofilecriteriamodel(int profileId)
        {
            
                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profilemetadata  metadata =membersrepository.getprofilebyprofileid(profileId).profilemetadata ; 


            //load postaldata context
            ProfileCriteriaModel CriteriaModel = new ProfileCriteriaModel();
            if (profileId != null)
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
                //Ethnicity =      metadata.profile.profiledata_Ethnicity.Where(
                //find a way to populate hoby, looking for and ethnicuty from the profiledata_Ethncity and etc

                CriteriaModel.ScreenName = (metadata.profile.screenname == null) ? Extensions.ReduceStringLength(metadata.profile.screenname, 10) : Extensions.ReduceStringLength(metadata.profile.screenname, 10);
                CriteriaModel.AboutMe  = (metadata.profile.profiledata.aboutme  == null || metadata.profile.profiledata.aboutme == "Hello") ? "This is the description of the type of person I am looking for.. comming soon. For Now Email Me to find out more about me" : metadata.profile.profiledata.aboutme;
                //  MyCatchyIntroLine = metadata.prMyCatchyIntroLine == null ? "Hi There!" : metadata.MyCatchyIntroLine;
                CriteriaModel.BodyType = (metadata.profile.profiledata.bodytype == null | metadata.profile.profiledata.bodytype.id   == null) ? "Ask Me" : metadata.profile.profiledata.bodytype.description;
                CriteriaModel.EyeColor = (metadata.profile.profiledata.eyecolor  == null | metadata.profile.profiledata.eyecolor.id == null) ? "Ask Me" : metadata.profile.profiledata.eyecolor.description ;
                // Ethnicity = metadata.CriteriaAppearance_Ethnicity == null ? "Ask Me" : metadata.CriteriaAppearance_Ethnicity.EthnicityName;
                CriteriaModel.HairColor = (metadata.profile.profiledata.haircolor  == null | metadata.profile.profiledata.haircolor.id  == null) ? "Ask Me" : metadata.profile.profiledata.haircolor.description ;
                //TO DO determine weather metic or us sustem user //added 11-17-2011
                CriteriaModel.HeightByCulture = (metadata.profile.profiledata.height  == null | metadata.profile.profiledata.height  == 0) ? "Ask Me" : Extensions.ToFeetInches((double)metadata.profile.profiledata.height );

                CriteriaModel.Exercise = (metadata.profile.profiledata.exercise == null | metadata.profile.profiledata.exercise.id == null) ? "Ask Me" : metadata.profile.profiledata.exercise.description;
                CriteriaModel.Religion = (metadata.profile.profiledata.religion  == null | metadata.profile.profiledata.religion.id  == null) ? "Ask Me" : metadata.profile.profiledata.religion.description;
                CriteriaModel.ReligiousAttendance = (metadata.profile.profiledata.religiousattendance == null | metadata.profile.profiledata.religiousattendance.id == null) ? "Ask Me" : metadata.profile.profiledata.religiousattendance.description;
                CriteriaModel.Drinks = (metadata.profile.profiledata.drinking == null | metadata.profile.profiledata.drinking.id  == null) ? "Ask Me" : metadata.profile.profiledata.drinking.description;
                CriteriaModel.Smokes = (metadata.profile.profiledata.smoking  == null | metadata.profile.profiledata.smoking.id  == null) ? "Ask Me" : metadata.profile.profiledata.smoking.description  ;
                CriteriaModel.Humor = (metadata.profile.profiledata.humor == null | metadata.profile.profiledata.humor.id  == null) ? "Ask Me" : metadata.profile.profiledata.humor.description ;
                // HotFeature = metadata.profile.profiledata.CriteriaCharacter_HotFeature == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_HotFeature.HotFeatureName; 
                //   Hobby =  metadata.profile.profiledata.CriteriaCharacter_Hobby == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_Hobby.HobbyName;
                CriteriaModel.PoliticalView =( metadata.profile.profiledata.politicalview  == null | metadata.profile.profiledata.politicalview  == null) ? "Ask Me" : metadata.profile.profiledata.politicalview.description ;
                CriteriaModel.Diet = (metadata.profile.profiledata.diet == null | metadata.profile.profiledata.diet.id  == null) ? "Ask Me" : metadata.profile.profiledata.diet.description ;
                //TO DO calculate this by bdate
                CriteriaModel.Sign = (metadata.profile.profiledata.sign  == null | metadata.profile.profiledata.sign.id == null) ? "Ask Me" : metadata.profile.profiledata.sign.description ;
                CriteriaModel.IncomeLevel = (metadata.profile.profiledata.incomelevel  == null | metadata.profile.profiledata.incomelevel  == null) ? "Ask Me" : metadata.profile.profiledata.incomelevel.description ;
                CriteriaModel.HaveKids = (metadata.profile.profiledata.kidstatus  == null | metadata.profile.profiledata.kidstatus.id  == null) ? "Ask Me" : metadata.profile.profiledata.kidstatus.description ;
                CriteriaModel.WantsKids = (metadata.profile.profiledata.wantsKidstatus == null | metadata.profile.profiledata.wantsKidstatus.id == null) ? "Ask Me" : metadata.profile.profiledata.wantsKidstatus.description ;
                CriteriaModel.EmploymentSatus = (metadata.profile.profiledata.employmentstatus  == null | metadata.profile.profiledata.employmentstatus.id == null) ? "Ask Me" : metadata.profile.profiledata.employmentstatus.description;
                CriteriaModel.EducationLevel = (metadata.profile.profiledata.educationlevel  == null | metadata.profile.profiledata.educationlevel.id   == null) ? "Ask Me" : metadata.profile.profiledata.educationlevel.description ;
                CriteriaModel.Profession = (metadata.profile.profiledata.profession  == null | metadata.profile.profiledata.profession .id == null) ? "Ask Me" : metadata.profile.profiledata.profession.description ;
                CriteriaModel.MaritalStatus = (metadata.profile.profiledata.maritalstatus  == null | metadata.profile.profiledata.maritalstatus.id == null) ? "Single" : metadata.profile.profiledata.maritalstatus.description ;
                CriteriaModel.LivingSituation = (metadata.profile.profiledata.livingsituation  == null | metadata.profile.profiledata.livingsituation.id  == null) ? "Ask Me" : metadata.profile.profiledata.livingsituation.description;
                //special case for ethnicty since they can have mutiples ?
                //loop though ethnicty thing I guess ?  
                //8/11/2011 have to loop though since these somehow get detached sometimes
                //Ethnicity = metadata.profile.profiledata.profiledata_Ethnicity;

                foreach (var item in metadata.ethnicities  )
                {
                    CriteriaModel.Ethnicity.Add(item.ethnicty.description );
                }

                foreach (var item in metadata.hobbies )
                {
                    CriteriaModel.Hobbies.Add(item.hobby.description);
                }

                foreach (var item in metadata.lookingfor )
                {
                    CriteriaModel.LookingFor.Add(item.lookingfor.description );
                }

                foreach (var item in metadata.hotfeatures)
                {
                    CriteriaModel.HotFeature.Add(item.hotfeature.description );
                }

                //handle perfect match settings here .
                // first load perfect match settings for this user from database
                //set defaults if no values are available
                var PerfectMatchSettings = metadata.searchsettings.First();


                //basic search settings here
                CriteriaModel.BasicSearchSettings.maxdistancefromme  = (PerfectMatchSettings == null || PerfectMatchSettings.distancefromme == null) ? 500 : PerfectMatchSettings.distancefromme ;
                CriteriaModel.BasicSearchSettings.seekingagemin = (PerfectMatchSettings == null || PerfectMatchSettings.agemin == null) ? 18 : PerfectMatchSettings.agemin;
                CriteriaModel.BasicSearchSettings.seekingagemax = (PerfectMatchSettings == null || PerfectMatchSettings.agemax == null) ? 99 : PerfectMatchSettings.agemax;

                //TO DO add this to search settings for now use what is in profiledata
                //These will come from search settings table in the future at some point
                CriteriaModel.BasicSearchSettings.country = georepository.getcountrynamebycountryid((byte)metadata.profile.profiledata.countryid);  //TO DO allow a range of countries to be selected i.e multi select
                CriteriaModel.BasicSearchSettings.citystateprovince = (metadata.profile.profiledata.stateprovince == null) ? "Ask Me" : metadata.profile.profiledata.stateprovince;
                CriteriaModel.BasicSearchSettings.postalcode  = metadata.profile.profiledata.postalcode;  //this could be for countries withoute p codes

                //populate list values
                foreach (var item in PerfectMatchSettings.genders )
                {
                    CriteriaModel.BasicSearchSettings.genderslist.Add(item.gender.description );
                }
                //appearance search settings here
                CriteriaModel.AppearanceSearchSettings.heightmax = (PerfectMatchSettings == null || PerfectMatchSettings.heightmax == null) ? Extensions.ToFeetInches(48) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmax);
                CriteriaModel.AppearanceSearchSettings.heightmin = (PerfectMatchSettings == null || PerfectMatchSettings.heightmin == null) ? Extensions.ToFeetInches(89) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmin);

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


                foreach (var item in PerfectMatchSettings.hotfeatures )
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
                { CriteriaModel.CharacterSearchSettings.smokeslist.Add(item.smoke.description ); }

                foreach (var item in PerfectMatchSettings.signs )
                { CriteriaModel.CharacterSearchSettings.signlist.Add(item.sign.description ); }

                foreach (var item in PerfectMatchSettings.politicalviews )
                { CriteriaModel.CharacterSearchSettings.politicalviewlist.Add(item.politicalview.description ); }

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
        //no current profiledata exists to retrive
        public ProfileCriteriaModel getprofilecriteriamodel()
        {

            //build defualt empties for list values
            //  List<profiledata_Ethnicity> EmptyEthnicty = new List<profiledata_Ethnicity>();
            // Ethnicity.Add(new profiledata_Ethnicity Temp)
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
        public MembersViewModel getdefaultquicksearchsettingsmembers(MembersViewModel model)
        {
            quicksearchmodel quicksearchmodel = new quicksearchmodel();
           // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
            //set deafult paging or pull from DB
            //quicksearchmodel.myse = 4;
            quicksearchmodel.myselectedcurrentpage = 1;
            //added state province with comma 

            quicksearchmodel.myselectedcity = model.profile.profiledata.city;
            quicksearchmodel.myselectedmaxdistancefromme = model.profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme != null ? model.maxdistancefromme : 1000;

            quicksearchmodel.myselectedfromage = model.profile.profilemetadata.searchsettings.FirstOrDefault().agemin != null ? model.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.GetValueOrDefault() : 18;
            quicksearchmodel.myselectedtoage = model.profile.profilemetadata.searchsettings.FirstOrDefault().agemax != null ? model.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.GetValueOrDefault() : 99; ;


            quicksearchmodel.myselectediamgenderid = model.profile.profiledata.gender.id ;
            quicksearchmodel.myselectedcitystateprovince = model.profile.profiledata.city + "," + model.profile.profiledata.stateprovince; ;
            //TO DO convert genders to a list of genders 
            quicksearchmodel.myselectedseekinggenderid = Extensions.GetLookingForGenderID(model.profile.profiledata.gender.id);
            quicksearchmodel.myselectedcountryname = model.mycountryname; //use same country for now
            //add the postal code status here as well
            quicksearchmodel.myselectedpostalcodestatus = (georepository.getcountry_postalcodestatusbycountryname(model.mycountryname) == 1) ? true : false;

            //TO do get this from search settings
            //default for has photos only get this from the 
            quicksearchmodel.myselectedphotostatus = true;

            model.myquicksearch = quicksearchmodel;  //save it

            return model;
        }
        //populate search settings for guests 
        public MembersViewModel getdefaultsearchsettingsguest(MembersViewModel model)
        {
            //set defualt values for guests
            //model.myquicksearch.mySelectedPageSize = 4;
            model.myquicksearch.myselectedcurrentpage = 1;
            model.myquicksearch.myselectedcity = "";
            model.mypostalcodestatus = false;
            model.myquicksearch.myselectedmaxdistancefromme = 2000;
            model.myquicksearch.myselectedfromage = 18;
            model.myquicksearch.myselectedtoage = 99;
            model.myquicksearch.myselectediamgenderid = 1;
            model.myquicksearch.myselectedcitystateprovince = "ALL";
            model.myquicksearch.myselectedseekinggenderid = Extensions.GetLookingForGenderID(1);
            model.myquicksearch.myselectedcountryname = "United States"; //use same country for now

            model.myquicksearch.myselectedphotostatus = true;

            return model;
        }

        //registration model update and mapping
        public RegisterModel getregistermodel(MembersViewModel membersmodel)
        {

            // MembersRepository membersrepository = new MembersRepository();


            RegisterModel model = new RegisterModel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;
            model.City = membersmodel.myquicksearch.myselectedcity;
            model.Country = membersmodel.myquicksearch.myselectedcountryname;
            model.longitude = membersmodel.myquicksearch.myselectedlongitude;
            model.lattitude = membersmodel.myquicksearch.myselectedlongitude;
            model.PostalCodeStatus = membersmodel.myquicksearch.myselectedpostalcodestatus;

            // model.SecurityAnswer = "moma";
            //5/8/2011  set other defualt values here
            //model.RegistrationPhotos.PhotoStatus = "";
            // model.PostalCodeStatus = false;
            return model;

        }
        public RegisterModel getregistermodelopenid(MembersViewModel membersmodel)
        {


            RegisterModel model = new RegisterModel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;
            model.openidIdentifer = membersmodel.rpxmodel.identifier;
            model.openidProvider = membersmodel.rpxmodel.providername;


            //model.Ages = sharedrepository.AgesSelectList();
            // model.Genders = sharedrepository.GendersSelectList();
            // model.Countries = sharedrepository.CountrySelectList();
            // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
            //test values
            model.BirthDate = DateTime.Parse(membersmodel.rpxmodel.birthday);

            model.Email = membersmodel.rpxmodel.verifiedemail;
            model.ConfirmEmail = membersmodel.rpxmodel.verifiedemail;
            model.Gender = Extensions.ConvertGenderName(membersmodel.rpxmodel.gender).ToString();


            // model.Password = "kayode02";
            //model.ConfirmPassword = "kayode02";
            model.ScreenName = membersmodel.rpxmodel.displayname;
            model.UserName = membersmodel.rpxmodel.preferredusername;
            model.City = membersmodel.mycitystateprovince;


            model.Country = membersmodel.mycountryname;
            model.longitude = Convert.ToDouble(membersmodel.mylongitude);
            model.lattitude = Convert.ToDouble(membersmodel.mylatitude);
            model.PostalCodeStatus = membersmodel.mypostalcodestatus;
            model.ZipOrPostalCode = membersmodel.mypostalcode;


            //added passwords temporary hack
            model.Password = "ssoUser";

            //5/29/2012

            //get the photo info
            // model.SecurityAnswer = "moma";
            //5/8/2011  set other defualt values here
            //model.RegistrationPhotos.PhotoStatus = "";
            // model.PostalCodeStatus = false;
            PhotoUploadViewModel photouploadvm = new PhotoUploadViewModel();
            //initlaize PhotoUploadViewModel object          
            photouploadvm.profileid = membersmodel.profile.id; //set the profileID  
            photouploadvm.photosuploaded = new List<PhotoUploadModel>();
            PhotoUploadModel photobeinguploaded = new PhotoUploadModel();

            //right now we are only uploading one photo 
            //for now we are using URL from each, we can hanlde mutiple provider formats that might return a byte using the source paremater
            //or the openID provider name to customize
            if (membersmodel.rpxmodel.photo != "")
            {   //build the photobeinguploaded object
                photobeinguploaded.image = photorepository.getimagebytesfromurl(membersmodel.rpxmodel.photo, "");
                photobeinguploaded.imagetype = _datingcontext.lu_photoimagetype.Where(p => p.id == (int)photoimagetypeEnum.Jpeg).FirstOrDefault();
                photobeinguploaded.creationdate = DateTime.Now;
                photobeinguploaded.caption = membersmodel.rpxmodel.preferredusername;
                //TO DO rename this to upload image from URL ?

                //add to repository
                photorepository.addphotos(photouploadvm);
            }
            //make sure photos is not empty
            //  if (membersmodel.MyPhotos == null)
            // { //add new photo model to members model
            //    var photolist = new List<Photo>();
            //    membersmodel.MyPhotos = photolist;
            // }
            //don't pass back photos for now


            return model;

        }
        public RegisterModel getregistermodeltest()
        {

            // MembersRepository membersrepository = new MembersRepository();


            RegisterModel model = new RegisterModel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
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

        
        //TOD modifiy client to not bind from this model but load values asycnh

        //other member viewmodl methods
        public MembersViewModel updatememberdata(MembersViewModel model)
        {
            return CachingFactory.MembersViewModelHelper.updatememberdata(model, this);
        }
        public MembersViewModel updatememberdatabyprofileid(int profileid)
        {
            return CachingFactory.MembersViewModelHelper.updatememberprofiledatabyprofile(profileid,this);
        }
        public bool updateguestdata(MembersViewModel model)
        {
            return CachingFactory.MembersViewModelHelper.updateguestdata(model,this);
        }
        public bool removeguestdata(string sessionid)
        {
            return CachingFactory.MembersViewModelHelper.removeguestdata(sessionid);
        }
        //cacheing of search stuff
        public MembersViewModel getguestdata(string sessionid)
        {
            return CachingFactory.MembersViewModelHelper.getguestdata(sessionid, this);
        }
        public MembersViewModel getmemberdata(int profileid)
        {
           return CachingFactory.MembersViewModelHelper.getmemberdata(profileid, this  );
        }

        //functions not exposed via WCF or otherwise
        public MembersViewModel mapmember(int ProfileID)
        {

            MembersViewModel model = new MembersViewModel();
            // IEnumerable<CityStateProvince> CityStateProvince ;
            // MailModelRepository mailrepository = new MailModelRepository();
            //var myProfile = membersrepository.GetprofiledataByProfileID(ProfileID).profile;
            // var perfectmatchsearchsettings = membersrepository.GetPerFectMatchSearchSettingsByProfileID(ProfileID);
            // model.Profile = myProfile;
            //Profile data will be on the include
            model.profile = membersrepository.getprofilebyprofileid(Convert.ToInt16(ProfileID));
            //TO DO this should be a try cacth with exception handling

            try
            {
                //TO DO do away with this since we already have the profile via include from the profile DATA
                // model.Profile = model.profile;

                //   model.profile.profiledata.SearchSettings(perfectmatchsearchsettings);
                //4-28-2012 added mapping for profile visiblity
                model.profilevisiblity = model.profile.profiledata.visibilitysettings;

                //on first load this should always be false
                //to DO   DO  we still need this
                model.profilestatusmessageshown = false;

                model.mygenderid = model.profile.profiledata.gender.id;
                //this should come from search settings eventually on the full blown model of this.
                //create hase list of genders they are looking for, if it is null add the default

                //TO DO change this to use membererepo
                model.lookingforgendersid = (model.profile.profilemetadata.searchsettings.FirstOrDefault() != null) ?
                new HashSet<int>(model.profile.profilemetadata.searchsettings.FirstOrDefault().genders.Select(c => c.id.GetValueOrDefault())) : null;
                if (model.lookingforgendersid != null)
                {
                    model.lookingforgendersid.Add(Extensions.GetLookingForGenderID(model.profile.profiledata.gender.id));
                }

                //set selected value
                //model.Countries. =model.profile.profiledata.CountryID;

                //geographical data poulated here
                model.mycountryname = georepository.getcountrynamebycountryid(model.profile.profiledata.countryid);
                model.mycountryid =model.profile.profiledata.countryid;
                model.mycity =model.profile.profiledata.city;
                //TO DO items need to be populated with real values, in this case change model to double for latt
                model.mylatitude =model.profile.profiledata.latitude.ToString(); //model.Lattitude
                model.mylongitude =model.profile.profiledata.longitude.ToString();
                //update 9-21-2011 get fro search settings
                model.maxdistancefromme = model.profile.profilemetadata.searchsettings.FirstOrDefault() != null ? model.profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme.GetValueOrDefault() : 500;

                //11-29-2012 olawl move this chunk to ajax calls 
                //mail counters
                //model.mymailcount = mailrepository.getallmailcountbyfolderid((int)defaultmailboxfoldertypeEnum.Sent, model.profile.id).ToString();
                //model.whomailedme = mailrepository.getallmailcountbyfolderid((int)defaultmailboxfoldertypeEnum.Inbox, model.profile.id).ToString();
                //model.whomailedmenewcount = mailrepository.getnewmailcountbyfolderid((int)defaultmailboxfoldertypeEnum.Inbox, model.profile.id).ToString();

                //model.WhoMailedMeNewCount =  
                //interests
                //TO DO move all these to ajax calls on client
                //model.myintrestcount = memberactionsrepository.getwhoiaminterestedincount(model.profile.id).ToString();
                //model.whoisinterestedinmecount = memberactionsrepository.getwhoisinterestedinmecount(model.profile.id).ToString();
                //model.whoisinterestedinmenewcount = memberactionsrepository.getwhoisinterestedinmenewcount(model.profile.id).ToString();
                ////peeks
                //model.mypeekscount = memberactionsrepository.getwhoipeekedatcount(model.profile.id).ToString();
                //model.whopeekededatmecount = memberactionsrepository.getwhopeekedatmecount(model.profile.id).ToString();
                //model.whopeekedatmenewcount = memberactionsrepository.getwhopeekedatmenewcount(model.profile.id).ToString();
                ////likes
                //model.wholikesmenewcount = memberactionsrepository.getwholikesmecount(model.profile.id).ToString();
                //model.wholikesmecount = memberactionsrepository.getwholikesmecount(model.profile.id).ToString();
                //model.whoilikecount = memberactionsrepository.getwhoilikecount(model.profile.id).ToString();

                //blocks
               // model.myblockcount = memberactionsrepository.getwhoiblockedcount(model.profile.id).ToString();

                //instantiate models for city state province and quick search
                // get users search setttings
                //model.MyQuickSearch = quicksearchmodel;



                // now instantiate city state province
                // model.MyQuickSearch.MySelectedCityStateProvince = CityStateProvince();
                // model = membersrepository.getdefaultquicksearchsettingsmembers(model);

                //added 5-10-2012
                //we dont want to add search setttings to the members model?
                //TO do remove profiledata at some point
                //check if the user has a profile search settings value in stored DB if not add one and save it
                if (model.profile.profilemetadata.searchsettings.Count == 0)
                {
                    membersrepository.createmyperfectmatchsearchsettingsbyprofileid(model.profile.id);
                    //update the profile data with the updated value
                    //TO DO stop storing profiledata
                    model.profiledata = membersrepository.getprofiledatabyprofileid(model.profile.id);

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
            catch (Exception ex)
            {

 var message = ex.Message;
            }


            return null;


        }
        public MembersViewModel mapguest()
        {
            MembersViewModel model = new MembersViewModel();


            quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;

            model.myquicksearch = quicksearchmodel;


            return model;



            //   model.MyMatchesPaged = null;
            //  model.MyMatches = null;
            // return model;
        }

    }
}
