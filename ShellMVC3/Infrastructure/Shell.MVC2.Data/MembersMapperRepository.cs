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
       // private IMemberActionsRepository memberactionsrepository;
        private IMailRepository mailrepository;
        // private AnewluvContext _db;
        //TO DO move from ria servives
       // private AnewluvContext  datingcontext;

       public MembersMapperRepository(IGeoRepository _georepository, IPhotoRepository
           _photorepository, IMemberRepository _membersrepository,  IMailRepository _mailrepository, AnewluvContext datingcontext)
            : base(datingcontext)
        {
            georepository = _georepository;
            photorepository = _photorepository;
            membersrepository = _membersrepository;
           // memberactionsrepository = _memberactionsrepository ;
            mailrepository = _mailrepository;
           // datingcontext = _datingcontext;
        }

        
        // constructor
        public MemberSearchViewModel mapmembersearchviewmodel(int? viewerprofileid, MemberSearchViewModel modeltomap,bool allphotos)
       {
           try
           {
               if (modeltomap.id != null)
               {

                   profiledata viewerprofile = new profiledata();
                   if (viewerprofileid != null) viewerprofile = membersrepository.getprofiledatabyprofileid(viewerprofileid.GetValueOrDefault());

                   MemberSearchViewModel model = modeltomap;
                   //TO DO change to use Ninject maybe
                   // DatingService db = new DatingService();
                   //  MembersRepository membersrepo=  new MembersRepository();
                   profile profile = membersrepository.getprofilebyprofileid(modeltomap.id); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                   //  membereditRepository membereditRepository = new membereditRepository();

                   //12-6-2012 olawal added the info for distance between members only if all these values are fufilled
                   if (viewerprofile.latitude != null &&
                       viewerprofile.longitude != null &&
                       profile.profiledata.longitude != null &&
                        profile.profiledata.latitude != null)
                       model.distancefromme = georepository.getdistancebetweenmembers(
                           viewerprofile.latitude.GetValueOrDefault(),
                           viewerprofile.longitude.GetValueOrDefault(),
                            profile.profiledata.latitude.GetValueOrDefault(),
                           profile.profiledata.longitude.GetValueOrDefault(), "M");


                   model.id = profile.id;
                   model.screenname = profile.screenname;
                   
                   //model.profiledata = profile.profiledata;
                   //model.profile = profile;
                   model.stateprovince = profile.profiledata.stateprovince;
                   model.postalcode = profile.profiledata.postalcode;
                   model.countryid = profile.profiledata.countryid;
                   model.genderid = profile.profiledata.gender.id;
                   model.birthdate = profile.profiledata.birthdate;
                   // modelprofile = profile.profile;
                   model.longitude = (double)profile.profiledata.longitude;
                   model.latitude = (double)profile.profiledata.latitude;
                   //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                   model.creationdate = profile.creationdate;
                   model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                   model.lastlogindate = profile.logindate;
                   model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                   model.mycatchyintroline = profile.profiledata.mycatchyintroLine;
                   model.aboutme = profile.profiledata.aboutme;
                   model.online = membersrepository.getuseronlinestatus(profile.id);
                   model.perfectmatchsettings = profile.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault();
                   // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
                   //DistanceFromMe = 0  get distance from somwhere else
                   //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                   //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                   //or instead just have the photo the select zoom up
                   int page = 1;
                   int ps = 12;
                   // var MyPhotos = membereditRepository.MyPhotos(model.profile.username);
                   // var Approved = membereditRepository.GetApproved(MyPhotos, "Yes", page, ps);
                   // var NotApproved = membereditRepository.GetApproved(MyPhotos, "No", page, ps);
                   // var Private = membereditRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                   model.profilephotos = new PhotoViewModel();
                   if (allphotos == true)
                   {
                       model.profilephotos.ProfilePhotosApproved = photorepository.getpagedphotomodelbyprofileidandstatus(profile.id, photoapprovalstatusEnum.Approved, photoformatEnum.Thumbnail, page, ps);   //membereditRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
                   }// approvedphotos = photorepository.
                   else
                   {
                       model.profilephotos.SingleProfilePhoto = photorepository.getphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                   }
                   return model;
               }
               return null;
           }
           catch (Exception ex)
           {
               //log error 
               throw ex;
           }
          // return null;
       }
        public List<MemberSearchViewModel> mapmembersearchviewmodels(int? viewerprofileid, List<MemberSearchViewModel> modelstomap,bool allphotos)
        {
            try
            {
                profiledata viewerprofile = new profiledata();
                if (viewerprofileid != null) viewerprofile = membersrepository.getprofiledatabyprofileid(viewerprofileid.GetValueOrDefault());

                List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                foreach (var item in modelstomap)
                {
                    models.Add(mapmembersearchviewmodel(viewerprofileid,item,allphotos));                    

                }
                return models;
            }
            catch (Exception ex)
            {
                //log error

            }

            return null;
        }
        public MemberSearchViewModel getmembersearchviewmodel(int? viewerprofileid,int profileId)
        {
            try
            {
            if (profileId != null)
            {

                 profiledata viewerprofile = new profiledata();
                if (viewerprofileid != null) viewerprofile = membersrepository.getprofiledatabyprofileid(viewerprofileid.GetValueOrDefault());

                MemberSearchViewModel model = new MemberSearchViewModel();
                //TO DO change to use Ninject maybe
               // DatingService db = new DatingService();
                //  MembersRepository membersrepo=  new MembersRepository();
                profile profile =membersrepository.getprofilebyprofileid(profileId); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                //  membereditRepository membereditRepository = new membereditRepository();

                   //12-6-2012 olawal added the info for distance between members only if all these values are fufilled
                    if (viewerprofile.latitude != null &&
                        viewerprofile.longitude != null &&
                        profile.profiledata .longitude != null &&
                         profile.profiledata.latitude != null)
                        model.distancefromme = georepository.getdistancebetweenmembers(
                            viewerprofile.latitude.GetValueOrDefault(), 
                            viewerprofile.longitude.GetValueOrDefault(),
                             profile.profiledata.latitude.GetValueOrDefault(),
                            profile.profiledata.longitude.GetValueOrDefault(), "M");


                model.id = profile.id  ;
                model.profiledata = profile.profiledata ;
                model.profile = profile;
                model.stateprovince  = profile.profiledata.stateprovince;
                model.postalcode = profile.profiledata.postalcode;
                model.countryid = profile.profiledata.countryid;
                model.genderid = profile.profiledata.gender.id;
                model.birthdate = profile.profiledata.birthdate;
                // modelprofile = profile.profile;
                model.longitude = (double)profile.profiledata.longitude;
                model.latitude = (double)profile.profiledata.latitude;
                //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                model.creationdate  = profile.creationdate;
                model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                model.lastlogindate =  profile.logindate ;
                model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                model.online  = membersrepository.getuseronlinestatus(profile.id );
                // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
                //DistanceFromMe = 0  get distance from somwhere else
                //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                //or instead just have the photo the select zoom up
                int page = 1;
                int ps = 12;
               // var MyPhotos = membereditRepository.MyPhotos(model.profile.username);
               // var Approved = membereditRepository.GetApproved(MyPhotos, "Yes", page, ps);
               // var NotApproved = membereditRepository.GetApproved(MyPhotos, "No", page, ps);
               // var Private = membereditRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                model.profilephotos = new PhotoViewModel();
                model.profilephotos.ProfilePhotosApproved  = photorepository.getpagedphotomodelbyprofileidandstatus(model.profile.id,photoapprovalstatusEnum.Approved ,photoformatEnum.Thumbnail,  page, ps);   //membereditRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
               // approvedphotos = photorepository.

               return model;
            }
            return null;
            }
            catch (Exception ex)
            {
            //log error 

            }
            return null;
        }
        public List<MemberSearchViewModel> getmembersearchviewmodels(int? viewerprofileid,List<int> profileIds)
        {
            try
            {
                profiledata viewerprofile = new profiledata();
                if (viewerprofileid != null) viewerprofile = membersrepository.getprofiledatabyprofileid(viewerprofileid.GetValueOrDefault());

                List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                foreach (var item in profileIds)
                {

                    MemberSearchViewModel model = new MemberSearchViewModel();
                    //TO DO change to use Ninject maybe
                    // DatingService db = new DatingService();
                    //  MembersRepository membersrepo=  new MembersRepository();
                    profile profile = membersrepository.getprofilebyprofileid(item); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                    //  membereditRepository membereditRepository = new membereditRepository();

                    //12-6-2012 olawal added the info for distance between members only if all these values are fufilled
                    if (viewerprofile.latitude != null &&
                        viewerprofile.longitude != null &&
                        profile.profiledata.longitude != null &&
                         profile.profiledata.latitude != null)
                        model.distancefromme = georepository.getdistancebetweenmembers(
                            viewerprofile.latitude.GetValueOrDefault(),
                            viewerprofile.longitude.GetValueOrDefault(),
                             profile.profiledata.latitude.GetValueOrDefault(),
                            profile.profiledata.longitude.GetValueOrDefault(), "M");


                    model.id = profile.id;
                    model.profiledata = profile.profiledata;
                    model.profile = profile;
                    model.stateprovince = profile.profiledata.stateprovince;
                    model.postalcode = profile.profiledata.postalcode;
                    model.countryid = profile.profiledata.countryid;
                    model.genderid = profile.profiledata.gender.id;
                    model.birthdate = profile.profiledata.birthdate;
                    // modelprofile = profile.profile;
                    model.longitude = (double)profile.profiledata.longitude;
                    model.latitude = (double)profile.profiledata.latitude;
                    //  HasGalleryPhoto = (from p in db.photos.Where(i => i.ProfileID == f.ProfileID && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
                    model.creationdate = profile.creationdate;
                    model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                    model.lastlogindate = profile.logindate;
                    model.lastloggedonstring = membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                    model.online = membersrepository.getuseronlinestatus(profile.id);
                    // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
                    //DistanceFromMe = 0  get distance from somwhere else
                    //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                    //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                    //or instead just have the photo the select zoom up
                    int page = 1;
                    int ps = 12;
                    // var MyPhotos = membereditRepository.MyPhotos(model.profile.username);
                    // var Approved = membereditRepository.GetApproved(MyPhotos, "Yes", page, ps);
                    // var NotApproved = membereditRepository.GetApproved(MyPhotos, "No", page, ps);
                    // var Private = membereditRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                  //12-27-2012 olawal added a contrructore to photoviewmodel to avoid null collections
                    model.profilephotos = new PhotoViewModel();
                    model.profilephotos.ProfilePhotosApproved = photorepository.getpagedphotomodelbyprofileidandstatus(model.profile.id, photoapprovalstatusEnum.Approved, photoformatEnum.Thumbnail, page, ps);   //membereditRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
                    // approvedphotos = photorepository.

                }
                return models;
            }
            catch (Exception ex)
            {
                //log error

            }

            return null;
        }
        public ProfileBrowseModel getprofilebrowsemodel(int viewerprofileId, int profileId)
        {



            var NewProfileBrowseModel = new ProfileBrowseModel
            {
                //TO Do user a mapper instead of a contructur and map it from the service
                //Move all this to a service
                ViewerProfileDetails = getmembersearchviewmodel(null,viewerprofileId),
                ProfileDetails = getmembersearchviewmodel(null,profileId)
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
                    ViewerProfileDetails = getmembersearchviewmodel(null,viewerprofileId),
                    ProfileDetails = getmembersearchviewmodel(null,item)



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
                CriteriaModel.BasicSearchSettings = new BasicSearchSettingsModel ();
                CriteriaModel.AppearanceSearchSettings = new AppearanceSearchSettingsModel ();
                CriteriaModel.LifeStyleSearchSettings = new LifeStyleSearchSettingsModel ();
                CriteriaModel.CharacterSearchSettings = new CharacterSearchSettingsModel ();
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
                CriteriaModel.BasicSearchSettings.distancefromme   = (PerfectMatchSettings == null || PerfectMatchSettings.distancefromme == null) ? 500 : PerfectMatchSettings.distancefromme ;
                CriteriaModel.BasicSearchSettings.agemin  = (PerfectMatchSettings == null || PerfectMatchSettings.agemin == null) ? 18 : PerfectMatchSettings.agemin;
                CriteriaModel.BasicSearchSettings.agemax  = (PerfectMatchSettings == null || PerfectMatchSettings.agemax == null) ? 99 : PerfectMatchSettings.agemax;

                //TO DO add this to search settings for now use what is in profiledata
                //These will come from search settings table in the future at some point
              //  CriteriaModel.BasicSearchSettings. = georepository.getcountrynamebycountryid((byte)metadata.profile.profiledata.countryid);  //TO DO allow a range of countries to be selected i.e multi select
                CriteriaModel.BasicSearchSettings.locationlist = PerfectMatchSettings.locations .ToList();
              //  CriteriaModel.BasicSearchSettings.postalcode  = metadata.profile.profiledata.postalcode;  //this could be for countries withoute p codes

                //populate list values
                foreach (var item in PerfectMatchSettings.genders )
                {
                    CriteriaModel.BasicSearchSettings.genderlist.Add(item.gender);
                }
                //appearance search settings here
                CriteriaModel.AppearanceSearchSettings.heightmax = (PerfectMatchSettings == null || PerfectMatchSettings.heightmax  == null) ? Extensions.ToFeetInches(48) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmax);
                CriteriaModel.AppearanceSearchSettings.heightmin = (PerfectMatchSettings == null || PerfectMatchSettings.heightmin == null) ? Extensions.ToFeetInches(89) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmin);

                foreach (var item in PerfectMatchSettings.ethnicities  )
                {
                    CriteriaModel.AppearanceSearchSettings.ethnicitylist.Add(item.ethnicity);
                }

                foreach (var item in PerfectMatchSettings.bodytypes )
                {
                    CriteriaModel.AppearanceSearchSettings.bodytypeslist.Add(item.bodytype);
                }

                foreach (var item in PerfectMatchSettings.eyecolors )
                {
                    CriteriaModel.AppearanceSearchSettings.eyecolorlist.Add(item.eyecolor );
                }

                foreach (var item in PerfectMatchSettings.haircolors )
                {
                    CriteriaModel.AppearanceSearchSettings.haircolorlist.Add(item.haircolor );
                }


                foreach (var item in PerfectMatchSettings.hotfeatures )
                {
                    CriteriaModel.AppearanceSearchSettings.hotfeaturelist.Add(item.hotfeature);
                }

                //populate lifestyle values here

                foreach (var item in PerfectMatchSettings.educationlevels )
                { CriteriaModel.LifeStyleSearchSettings.educationlevellist.Add(item.educationlevel ); }

                foreach (var item in PerfectMatchSettings.lookingfor )
                { CriteriaModel.LifeStyleSearchSettings.lookingforlist.Add(item.lookingfor ); }

                foreach (var item in PerfectMatchSettings.employmentstatus )
                { CriteriaModel.LifeStyleSearchSettings.employmentstatuslist.Add(item.employmentstatus); }

                foreach (var item in PerfectMatchSettings.havekids )
                { CriteriaModel.LifeStyleSearchSettings.havekidslist.Add(item.havekids ); }

                foreach (var item in PerfectMatchSettings.livingstituations )
                { CriteriaModel.LifeStyleSearchSettings.livingsituationlist.Add(item.livingsituation ); }

                foreach (var item in PerfectMatchSettings.maritalstatuses )
                { CriteriaModel.LifeStyleSearchSettings.maritalstatuslist.Add(item.maritalstatus ); }

                foreach (var item in PerfectMatchSettings.wantkids )
                { CriteriaModel.LifeStyleSearchSettings.wantskidslist.Add(item.wantskids ); }

                foreach (var item in PerfectMatchSettings.professions )
                { CriteriaModel.LifeStyleSearchSettings.professionlist.Add(item.profession ); }

                foreach (var item in PerfectMatchSettings.incomelevels )
                { CriteriaModel.LifeStyleSearchSettings.incomelevellist.Add(item.incomelevel ); }

                //Character settings for search here
                foreach (var item in PerfectMatchSettings.diets )
                { CriteriaModel.CharacterSearchSettings.dietlist.Add(item.diet ); }

                foreach (var item in PerfectMatchSettings.humors )
                { CriteriaModel.CharacterSearchSettings.humorlist.Add(item.humor ); }

                foreach (var item in PerfectMatchSettings.hobbies )
                { CriteriaModel.CharacterSearchSettings.hobbylist.Add(item.hobby ); }

                foreach (var item in PerfectMatchSettings.drinks )
                { CriteriaModel.CharacterSearchSettings.drinkslist.Add(item.drink ); }

                //FIX after database update
                foreach (var item in PerfectMatchSettings.exercises )
                { CriteriaModel.CharacterSearchSettings.exerciselist.Add(item.exercise ); }

                foreach (var item in PerfectMatchSettings.smokes )
                { CriteriaModel.CharacterSearchSettings.smokeslist.Add(item.smoke ); }

                foreach (var item in PerfectMatchSettings.signs )
                { CriteriaModel.CharacterSearchSettings.signlist.Add(item.sign ); }

                foreach (var item in PerfectMatchSettings.politicalviews )
                { CriteriaModel.CharacterSearchSettings.politicalviewlist.Add(item.politicalview ); }

                foreach (var item in PerfectMatchSettings.religions )
                { CriteriaModel.CharacterSearchSettings.religionlist.Add(item.religion); }

                foreach (var item in PerfectMatchSettings.religiousattendances )
                { CriteriaModel.CharacterSearchSettings.religiousattendancelist.Add(item.religiousattendance ); }


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
        public registermodel getregistermodel(MembersViewModel membersmodel)
        {

            // MembersRepository membersrepository = new MembersRepository();


            registermodel model = new registermodel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;
            model.city = membersmodel.myquicksearch.myselectedcity;
            model.country = membersmodel.myquicksearch.myselectedcountryname;
            model.longitude = membersmodel.myquicksearch.myselectedlongitude;
            model.lattitude = membersmodel.myquicksearch.myselectedlongitude;
            model.postalcodestatus = membersmodel.myquicksearch.myselectedpostalcodestatus;

            // model.SecurityAnswer = "moma";
            //5/8/2011  set other defualt values here
            //model.RegistrationPhotos.PhotoStatus = "";
            // model.PostalCodeStatus = false;
            return model;

        }
        public registermodel getregistermodelopenid(MembersViewModel membersmodel)
        {


            registermodel model = new registermodel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;
            model.openididentifer = membersmodel.rpxmodel.identifier;
            model.openidprovider = membersmodel.rpxmodel.providername;


            //model.Ages = sharedrepository.AgesSelectList();
            // model.Genders = sharedrepository.GendersSelectList();
            // model.Countries = sharedrepository.CountrySelectList();
            // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
            //test values
            model.birthdate = DateTime.Parse(membersmodel.rpxmodel.birthday);

            model.emailaddress  = membersmodel.rpxmodel.verifiedemail;
            model.confirmemailaddress  = membersmodel.rpxmodel.verifiedemail;
            model.gender = Extensions.ConvertGenderName(membersmodel.rpxmodel.gender).ToString();


            // model.Password = "kayode02";
            //model.ConfirmPassword = "kayode02";
            model.screenname = membersmodel.rpxmodel.displayname;
            model.username = membersmodel.rpxmodel.preferredusername;
            model.city = membersmodel.mycitystateprovince;


            model.country = membersmodel.mycountryname;
            model.longitude = Convert.ToDouble(membersmodel.mylongitude);
            model.lattitude = Convert.ToDouble(membersmodel.mylatitude);
            model.postalcodestatus = membersmodel.mypostalcodestatus;
            model.ziporpostalcode = membersmodel.mypostalcode;


            //added passwords temporary hack
            model.password = "ssoUser";

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
                photobeinguploaded.imagetypeid  = _datingcontext.lu_photoimagetype.Where(p => p.id == (int)photoimagetypeEnum.Jpeg).FirstOrDefault().id;
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
        public registermodel getregistermodeltest()
        {

            // MembersRepository membersrepository = new MembersRepository();


            registermodel model = new registermodel();
            //quicksearchmodel quicksearchmodel = new quicksearchmodel();
            // IEnumerable<CityStateProvince> CityStateProvince ;



            //model.Ages = sharedrepository.AgesSelectList();
            // model.Genders = sharedrepository.GendersSelectList();
            // model.Countries = sharedrepository.CountrySelectList();
            // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
            //test values
            model.birthdate = DateTime.Parse("1/1/1983");

            model.emailaddress  = "ola_lawal@lyahoo.com";
            model.confirmemailaddress  = "ola_lawal@lyahoo.com";
            // model.Gender = "Male";
            model.password = "kayode02";
            model.confirmpassword = "kayode02";
            model.screenname = "test1";
            model.username = "olalaw";

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
            profile profile = new profile();

            // IEnumerable<CityStateProvince> CityStateProvince ;
            // MailModelRepository mailrepository = new MailModelRepository();
            //var myProfile = membersrepository.GetprofiledataByProfileID(ProfileID).profile;
            // var perfectmatchsearchsettings = membersrepository.GetPerFectMatchSearchSettingsByProfileID(ProfileID);
            // model.Profile = myProfile;
            //Profile data will be on the include
            profile = membersrepository.getprofilebyprofileid(Convert.ToInt16(ProfileID));
            //TO DO this should be a try cacth with exception handling

            try
            {
                //TO DO do away with this since we already have the profile via include from the profile DATA
                // model.Profile = model.profile;
                model.profile_id = profile.id;
                //   model.profile.profiledata.SearchSettings(perfectmatchsearchsettings);
                //4-28-2012 added mapping for profile visiblity
                model.profilevisiblity = profile.profiledata.visibilitysettings;

                //on first load this should always be false
                //to DO   DO  we still need this
                model.profilestatusmessageshown = false;
                model.mygenderid = profile.profiledata.gender.id;
                //this should come from search settings eventually on the full blown model of this.
                //create hase list of genders they are looking for, if it is null add the default
                //TO DO change this to use membererepo
                model.lookingforgendersid = (profile.profilemetadata.searchsettings.FirstOrDefault() != null) ?
                new HashSet<int>(profile.profilemetadata.searchsettings.FirstOrDefault().genders.Select(c => c.id.GetValueOrDefault())) : null;
                if (model.lookingforgendersid != null)
                {
                    model.lookingforgendersid.Add(Extensions.GetLookingForGenderID(profile.profiledata.gender.id));
                }

                //set selected value
                //model.Countries. =model.profile.profiledata.CountryID;
                //geographical data poulated here 
                //this is disabled when disconected ok
#if DISCONECTED

                model.mycountryname = "United States";// georepository.getcountrynamebycountryid(profile.profiledata.countryid);
#else
            model.mycountryname = georepository.getcountrynamebycountryid(profile.profiledata.countryid.GetValueOrDefault());
#endif

                model.mycountryid =profile.profiledata.countryid.GetValueOrDefault();
                model.mycity =profile.profiledata.city;
                //TO DO items need to be populated with real values, in this case change model to double for latt
                model.mylatitude =profile.profiledata.latitude.ToString(); //model.Lattitude
                model.mylongitude =profile.profiledata.longitude.ToString();
                //update 9-21-2011 get fro search settings
                model.maxdistancefromme = profile.profilemetadata.searchsettings.FirstOrDefault() != null ? profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme.GetValueOrDefault() : 500;

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
                if (profile.profilemetadata.searchsettings.Count == 0)
                {
                    membersrepository.createmyperfectmatchsearchsettingsbyprofileid(profile.id);
                    //update the profile data with the updated value
                    //TO DO stop storing profiledata
                   // model.profiledata = membersrepository.getprofiledatabyprofileid(profile.id);

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

        //TO DO move these to a comprehensive search repo

        //matches and other things mapped to lists
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(MembersViewModel model)
        {

            //get search sttings from DB
            searchsetting perfectmatchsearchsettings = model.profile.profilemetadata.searchsettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0)
                model.maxdistancefromme = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.agemax != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.agemin != null ? perfectmatchsearchsettings.agemin.GetValueOrDefault() : 18;
            //Height
            int intheightmin = perfectmatchsearchsettings.heightmin != null ? perfectmatchsearchsettings.heightmin.GetValueOrDefault() : 0;
            int intheightmax = perfectmatchsearchsettings.heightmax != null ? perfectmatchsearchsettings.heightmax.GetValueOrDefault() : 100;
            bool blEvaluateHeights = intheightmin > 0 ? true : false;
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylongitude) : 0;
            double? myLattitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylongitude) : 0;
            //get the rest of the values if they are needed in calculations


            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id.GetValueOrDefault())) : LookingForGenderValues;
            //Appearacnce seache settings values         

            //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

            HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
            LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.bodytypes.Select(c => c.id.GetValueOrDefault())) : LookingForBodyTypesValues;

            HashSet<int> LookingForEthnicityValues = new HashSet<int>();
            LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.ethnicities.Select(c => c.id.GetValueOrDefault())) : LookingForEthnicityValues;

            HashSet<int> LookingForEyeColorValues = new HashSet<int>();
            LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.eyecolors.Select(c => c.id.GetValueOrDefault())) : LookingForEyeColorValues;

            HashSet<int> LookingForHairColorValues = new HashSet<int>();
            LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.haircolors.Select(c => c.id.GetValueOrDefault())) : LookingForHairColorValues;

            HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
            LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.hotfeatures.Select(c => c.id.GetValueOrDefault())) : LookingForHotFeatureValues;


            //******** visiblitysettings test code ************************

            // test all the values you are pulling here
            // var TestModel =   (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case")
            //                      select x).FirstOrDefault();
            //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1));
            // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profile.profiledata.GenderID) ? true : false;

            //  var testmodel2 = (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case" &&  db.fnCheckIfBirthDateIsInRange(x.birthdate, 19, 20) == true  )
            //                     select x).FirstOrDefault();


            //****** end of visiblity test settings *****************************************

            MemberSearchViewmodels = (from x in _datingcontext.profiledata.Where(p => p.birthdate > min && p.birthdate <= max)

                               //** visiblity settings still needs testing           
                                          //5-8-2012 add profile visiblity code here
                                          // .Where(x => x.profile.username == "case")
                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.ProfileVisiblity == true)
                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate > today.AddYears(-(x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1)))
                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate < today.AddYears(-x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault()))
                                          // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.countryid != null && x.ProfileVisiblitySetting.countryid == model.profile.profiledata.countryid  )
                                          // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.GenderID != null && x.ProfileVisiblitySetting.GenderID ==  model.profile.profiledata.GenderID )
                                          //** end of visiblity settings ***

                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender.id)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.gender.id)) //  == model.lookingforgenderid)    
                                          //TO DO add the rest of the filitering here 
                                          //Appearance filtering                         
                            .WhereIf(blEvaluateHeights, z => z.height > intheightmin && z.height <= intheightmax) //Only evealuate if the user searching actually has height values they look for                         
                                      join f in _datingcontext.profiles on x.profile_id equals f.id
                                      select new MemberSearchViewModel
                                      {
                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          id = x.profile_id,
                                          stateprovince = x.stateprovince,
                                          postalcode = x.postalcode,
                                          countryid = x.countryid,
                                          genderid = x.gender.id,
                                          birthdate = x.birthdate,
                                          profile = f,
                                          screenname = f.screenname,
                                          longitude = (double)x.longitude,
                                          latitude = (double)x.latitude,
                                          // hasgalleryphoto  = (_datingcontext.photos.Where(i => i.profile_id  == f.id && i.photostatus.id  == (int)photostatusEnum.Gallery ).FirstOrDefault().id  != null )? true : false ,
                                          // galleryphoto = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.photo.photostatus.id == (int)photostatusEnum.Gallery && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault()),
                                          hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                          //galleryphoto =  new PhotoModel { 

                                          //    photo = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image),
                                          //    //approvalstatus = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.approvalstatus),
                                          //    imagecaption = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.imagecaption),
                                          //    convertedsize = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().size),
                                          //    orginalsize = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.size)
                                          //},
                                          creationdate = f.creationdate,
                                          //city = _datingcontext.(x.city, 11),
                                          // lastloggedonstring   = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                          lastlogindate = f.logindate,
                                          //online  = _datingcontext.fnGetUserOlineStatus(x.ProfileID),
                                          // distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles") 
                                      }).ToList();


            //  var temp = MemberSearchViewmodels;
            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme <= model.maxdistancefromme) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
            //               Profiles;
            //do the stuff for the user defined functions here
            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.hasgalleryphoto == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);

            //TO DO find another way to do this maybe check and see if parsed values are empty or something or return a cached list?
            //11/20/2011 handle case where  no profiles were found
            if (Profiles.Count() == 0)
                return getquickmatcheswhenquickmatchesempty(model);

            return Profiles.ToList();

        }
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(MembersViewModel model)
        {



            try
            {

                profile profile = new profile();
                profile = membersrepository.getprofilebyprofileid(Convert.ToInt16(model.profile_id));

                model.profile = profile;

                //get search sttings from DB
                searchsetting perfectmatchsearchsettings = model.profile.profilemetadata.searchsettings.FirstOrDefault();
                //set default perfect match distance as 100 for now later as we get more members lower
                //TO DO move this to a _datingcontext setting or resourcer file
                if (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0)
                    model.maxdistancefromme = 500;

                //TO DO add this code to search after types have been made into doubles
                //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
                //right now returning all countries as well

                //** TEST ***
                //get the  gender's from search settings

                // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
                int intAgeTo = perfectmatchsearchsettings.agemax != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
                int intAgeFrom = perfectmatchsearchsettings.agemin != null ? perfectmatchsearchsettings.agemin.GetValueOrDefault() : 18;
                //Height
                int intheightmin = perfectmatchsearchsettings.heightmin != null ? perfectmatchsearchsettings.heightmin.GetValueOrDefault() : 0;
                int intheightmax = perfectmatchsearchsettings.heightmax != null ? perfectmatchsearchsettings.heightmax.GetValueOrDefault() : 100;
                bool blEvaluateHeights = intheightmin > 0 ? true : false;
                //get the rest of the values if they are needed in calculations
                //convert lattitudes from string (needed for JSON) to bool           
                double? myLongitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylongitude) : 0;
                double? myLattitude = (model.mylatitude != "") ? Convert.ToDouble(model.mylongitude) : 0;


                //set variables
                List<MemberSearchViewModel> MemberSearchViewmodels;
                DateTime today = DateTime.Today;
                DateTime max = today.AddYears(-(intAgeFrom + 1));
                DateTime min = today.AddYears(-intAgeTo);



                //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                //requery all the has tbls
                HashSet<int> LookingForGenderValues = new HashSet<int>();
                LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id.GetValueOrDefault())) : LookingForGenderValues;
                //Appearacnce seache settings values         

                //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.bodytypes.Select(c => c.id.GetValueOrDefault())) : LookingForBodyTypesValues;

                HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.ethnicities.Select(c => c.id.GetValueOrDefault())) : LookingForEthnicityValues;

                HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.eyecolors.Select(c => c.id.GetValueOrDefault())) : LookingForEyeColorValues;

                HashSet<int> LookingForHairColorValues = new HashSet<int>();
                LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.haircolors.Select(c => c.id.GetValueOrDefault())) : LookingForHairColorValues;

                HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.hotfeatures.Select(c => c.id.GetValueOrDefault())) : LookingForHotFeatureValues;

                var photostest = _datingcontext.profiles.Where(p => (p.profilemetadata.photos.Any(z => z.photostatus != null && z.photostatus.id != (int)photostatusEnum.Gallery)));

                //add more values as we get more members 
                //TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                var models = (from x in _datingcontext.profiles.Where(p => p.profiledata.birthdate > min && p.profiledata.birthdate <= max &&
                    (p.profilemetadata.photos.Any(z => z.photostatus.id == (int)photostatusEnum.Gallery)))
                                .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.profiledata.gender.id)) //using whereIF predicate function 
                                .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.profiledata.gender.id))
                                  //Appearance filtering not implemented yet                        
                                .WhereIf(blEvaluateHeights, z => z.profiledata.height > intheightmin && z.profiledata.height <= intheightmax) //Only evealuate if the user searching actually has height values they look for 
                              //we have to filter on the back end now since we cant use UDFs
                              // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                              join f in _datingcontext.profiles on x.profiledata.profile_id equals f.id
                              select x.id 
                    //{
                    //    // MyCatchyIntroLineQuickSearch = x.AboutMe,
                    //    id = x.profile_id,
                    //    stateprovince = x.stateprovince,
                    //    postalcode = x.postalcode,
                    //    countryid = x.countryid,
                    //    genderid = x.gender.id,
                    //    birthdate = x.birthdate,
                    //    //profile = f,
                    //    screenname = f.screenname,
                    //    longitude = x.longitude ?? 0,
                    //    latitude = x.latitude ?? 0,
                    //    //TO restore this when DB is fixed
                    //    //hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                    //    // galleryphoto = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.photo.photostatus.id == (int)photostatusEnum.Gallery && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault()),

                                          //    creationdate = f.creationdate,
                    //    hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                    //    //galleryphoto = new PhotoModel
                    //    //{
                    //    //    photo = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image),
                    //    //    approvalstatus = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.approvalstatus),
                    //    //    imagecaption = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.imagecaption),
                    //    //    convertedsize  = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().size),
                    //    //    orginalsize   = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.size )

                                          //    //},

                                          //    // city = db.fnTruncateString(x.city, 11),
                    //    //lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                    //    lastlogindate = f.logindate,
                    //    //  Online = _datingcontext.fnGetUserOlineStatus(x.ProfileID),
                    //    //distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")


                                          ).ToList();



                //these could be added to where if as well, also limits values if they did selected all
                // var Profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme <= model.maxdistancefromme) select q) : MemberSearchViewmodels;
                //     Profiles; ; 
                // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
                //               Profiles;

                //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
                //final ordering 
                //var Profiles = MemberSearchViewmodels.OrderByDescending(p => p.hasgalleryphoto == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme).Take(4);

                //bind the models to the memberserch view model

                var searchmodels = this.getmembersearchviewmodels(model.profile.id, models);

                var Profiles = searchmodels.Where(p => p.hasgalleryphoto == true).OrderBy(p => p.creationdate).ThenByDescending(p => p.distancefromme).Take(4);




                //TO DO find another way to do this maybe check and see if parsed values are empty or something or return a cached list?
                //11/20/2011 handle case where  no profiles were found
                if (Profiles.Count() == 0)
                    return getquickmatcheswhenquickmatchesempty(model).Take(4).ToList();

                return Profiles.ToList();
            }
            catch (Exception ex)
            {
                var errormessage = ex.Message;

            }
            return null;
        }
        public List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model)
        {

            //get search sttings from DB
            searchsetting perfectmatchsearchsettings = model.profile.profilemetadata.searchsettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a _datingcontext setting or resourcer file
            if (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0)
                model.maxdistancefromme = 5000;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.agemax != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.agemin != null ? perfectmatchsearchsettings.agemin.GetValueOrDefault() : 18;

            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylongitude) : 0;
            double? myLattitude = (model.mylatitude != "") ? Convert.ToDouble(model.mylatitude) : 0;



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id.GetValueOrDefault())) : LookingForGenderValues;


            //  where (LookingForGenderValues.Count !=null || LookingForGenderValues.Contains(x.GenderID)) 
            //  where (LookingForGenderValues.Count == null || x.GenderID == UserProfile.MyQuickSearch.MySelectedSeekingGenderID )   //this should not run if we have no gender in searchsettings
            MemberSearchViewmodels = (from x in _datingcontext.profiledata.Where(p => p.birthdate > min && p.birthdate <= max)
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender.id)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.gender.id))

                                      join f in _datingcontext.profiles on x.profile_id equals f.id
                                      select new MemberSearchViewModel
                                      {
                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          id = x.profile_id,
                                          stateprovince = x.stateprovince,
                                          postalcode = x.postalcode,
                                          countryid = x.countryid,
                                          genderid = x.gender.id,
                                          birthdate = x.birthdate,
                                          profile = f,
                                          screenname = f.screenname,
                                          longitude = x.longitude ?? 0,
                                          latitude = x.latitude ?? 0,
                                          //   hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                          //  galleryphoto = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.photo.photostatus.id == (int)photostatusEnum.Gallery && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault()),
                                          hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                          //galleryphoto = new PhotoModel
                                          //{
                                          //    photo = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().image),
                                          //    approvalstatus = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.approvalstatus),
                                          //    imagecaption = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.imagecaption),
                                          //    convertedsize = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().size),
                                          //    orginalsize = (_datingcontext.photoconversions.Where(i => i.photo.profile_id == x.profile_id && i.formattype.id == (int)photoformatEnum.Thumbnail).FirstOrDefault().photo.size)
                                          //},

                                          creationdate = f.creationdate,
                                          // city = db.fnTruncateString(x.city, 11),
                                          // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                          lastlogindate = f.logindate,
                                          //Online = _datingcontext.fnGetUserOlineStatus(x.ProfileID),
                                          //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")

                                      }).ToList();


            //these could be added to where if as well, also limits values if they did selected all
            // var Profiles = (model.maxdistancefromme  > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme <= model.maxdistancefromme ) select q) : MemberSearchViewmodels.Take(500);

            var Profiles = MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.hasgalleryphoto == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);


            return Profiles.ToList();


        }

    
    }
}
