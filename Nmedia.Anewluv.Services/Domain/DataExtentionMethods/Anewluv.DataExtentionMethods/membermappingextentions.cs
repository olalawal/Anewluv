using Anewluv.Caching;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.Models;
using GeoData.Domain.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data.Helpers;



namespace Anewluv.DataExtentionMethods
{


    public static class membermappingextentions
    {

        //shared extentionsused to map profiles together and desimnate detailed user data for view models
        #region "Member mapping extentions Functions"

        /// <summary>
        /// viewer can be blank or null : simply means we will not look for distance between memebers and other comparaitibe fatures      
        /// </summary>
        /// <param name="veiwerpeofileid"></param>
        /// <param name="viewingprofileid"></param>
        /// <param name="allphotos"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        // use this function to get distance at the same time, add it to the model
        public static FullProfileViewModel mapfullprofileviewmodel(int? veiwerpeofileid, int profileid, IUnitOfWorkAsync db, IGeoDataStoredProcedures _storedProcedures)
        {

            //we need lazy loading and proxy stuff
            // db.DisableProxyCreation = false;
            //  db.DisableLazyLoading = false;
            try
            {

                if (veiwerpeofileid != 0)
                {
                    FullProfileViewModel model = new FullProfileViewModel(); //new FullProfileViewModel();
                    //blank viewprofile simply means we will not look for distance between memebers and other comparaitibe fatures
                    profiledata viewerprofile = new profiledata();
                    //get profile of user
                    profile profile = db.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt32(profileid) });
                    if (veiwerpeofileid != null)
                    {
                        viewerprofile = db.Repository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = veiwerpeofileid.Value });

                        if (viewerprofile.latitude != null &&
                            viewerprofile.longitude != null &&
                            profile.profiledata.longitude != null &&
                             profile.profiledata.latitude != null)  //TO DO determine countries that use Mile or KM
                            model.distancefromme = spatialextentions.getdistancebetweenmembers(
                                viewerprofile.latitude.GetValueOrDefault(),
                                viewerprofile.longitude.GetValueOrDefault(),
                                 profile.profiledata.latitude.GetValueOrDefault(),
                                profile.profiledata.longitude.GetValueOrDefault(), "M");
                        //TO DO do this async in another call ?!
                        //Map the criteria
                        model.MemeberToMemberActions = membermappingextentions.mapmemberactionsrelationships(profileid,veiwerpeofileid.Value,db);
                    }

                    //we already have this so its overkill
                    // model.id = profile.id;
                    model.screenname = profile.screenname;
                    model.birthdate = profile.profiledata.birthdate;
                    model.lastloggedonstring =  profileextentionmethods.getlastloggedinstring(profile.logindate.GetValueOrDefault());
                    model.joindate = profile.creationdate.ToString(); //TO do format this
                    model.online = db.Repository<profile>().getuseronlinestatus(new ProfileModel { profileid = profile.id });
                    model.heightbyculture = (profile.profiledata.height == null) ? "Ask Me" : Extensions.ToFeetInches((double)profile.profiledata.height);
                    //TTO DO verifiy is this is created by getter
                    model.mycatchyintroline = profile.profiledata.mycatchyintroLine;
                    model.aboutme = profile.profiledata.aboutme;
                    model.genderid = profile.profiledata.gender_id.Value;
                    model.gender = DataFormatingExtensions.ConvertGenderID(model.genderid);

                    
                    model.stateprovince = profile.profiledata.stateprovince;
                    model.postalcode = profile.profiledata.postalcode;
                    model.countryid = profile.profiledata.countryid;
                    //TOD DO hard code country list from common and get it from cached version to filter
                    model.countryname = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = model.countryid.ToString() }, _storedProcedures);
                    // modelprofile = profile.profile;
                    model.longitude = (double)profile.profiledata.longitude;
                    model.latitude = (double)profile.profiledata.latitude;
                    //   model.hasgalleryphoto = profile.    profilemetadata.photos.Where(i => i.photostatus_id == (int)photostatusEnum.Gallery).FirstOrDefault() != null ? true : false;                 
                    model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);    
                   
                  
                    // PerfectMatchSettings = Currentprofiledata.SearchSettings.First();
                    //DistanceFromMe = 0  get distance from somwhere else
                    //to do do something with the unaproved photos so it is a nullable value , private photos are linked too here
                    //to do also figure out how to not show the gallery photo in the list but when they click off it allow it to default back
                    //or instead just have the photo the select zoom up                    
                    // var MyPhotos = membereditRepository.MyPhotos(model.profile.username);
                    // var Approved = membereditRepository.GetApproved(MyPhotos, "Yes", page, ps);
                    // var NotApproved = membereditRepository.GetApproved(MyPhotos, "No", page, ps);
                    // var Private = membereditRepository.GetPhotoByStatusID(MyPhotos, 3, page, ps);
                   // model.profilephotos = new PhotoViewModel();
                    // int something = (int)photoformatEnum.Thumbnail;
                     // model.profilephotos.SingleProfilePhoto = photorepository.getphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                     model.galleryphoto = db.Repository<photoconversion>().getgalleryphotomodelbyprofileid( profile.id, (int)photoformatEnum.Thumbnail);
                                     
                    //TO DO split this into another call
                    //TO DO put these into separate call i think
                    model.hasgalleryphoto = model.galleryphoto != null ? true : false;
                    // Api.DisposeGeoService();

                    return model;


                }


                return null;

            }
            catch (Exception ex)
            {
                //execptions will be handled in the caller
                throw;
            }

        }

        /// <summary>
        ///  SHOULD be called async probbaly
        /// </summary>
        /// <param name="profileid"></param>
        /// <param name="viewerprofileid"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static MemberToMemberActionsModel mapmemberactionsrelationships(int profileid, int viewerprofileid,IUnitOfWorkAsync db)
        {

            MemberToMemberActionsModel actions = new MemberToMemberActionsModel();
            try
            {
             

                
                //get the viwers actions to this member first :
                var vieweractionstoprofile = db.Repository<action>().getmyactionbyprofileid(viewerprofileid).Where(z => z.target_profile_id == profileid).ToList();
                var profileactionstoviewer = db.Repository<action>().getmyactionbyprofileid(profileid).Where(z => z.target_profile_id == viewerprofileid).ToList();


                actions.peekedataviewerdate = profileactionstoviewer.Count() > 0 ? profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Peek).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate : null;              
                actions.peekedataviewercount =  profileactionstoviewer.Count() > 0? profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Peek).Count():0;



                actions.likedviewerdate =  profileactionstoviewer.Count() > 0?  profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Like).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.likedviewercount =  profileactionstoviewer.Count() > 0?  profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Like).Count():0;

                actions.blockedviewerdate = profileactionstoviewer.Count() > 0? profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Block).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.blockedviewercount = profileactionstoviewer.Count() > 0? profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Block).Count():0;

              

                actions.intrestsenttoviewerDate = profileactionstoviewer.Count() > 0?  profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Interest).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.intrestsenttoviewercount =  profileactionstoviewer.Count() > 0?  profileactionstoviewer.Where(z => z.actiontype_id == (int)actiontypeEnum.Interest).Count():0;

                
                actions.peekedatbyviewerdate =  vieweractionstoprofile.Count() > 0?  vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Peek).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.peekedatbyviewercount = vieweractionstoprofile.Count() > 0? vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Peek).Count():0;

                actions.likedbyviewerdate =  vieweractionstoprofile.Count() > 0?vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Like).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.likedbyviewercount = vieweractionstoprofile.Count() > 0? vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Like).Count():0;
                
                actions.interestsentbyviewerDate =  vieweractionstoprofile.Count() > 0?vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Interest).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.interestsentbyviewercount =  vieweractionstoprofile.Count() > 0?vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Interest).Count():0;

                actions.blockedbyviewerdate = vieweractionstoprofile.Count() > 0? vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Block).OrderByDescending(p => p.creationdate).FirstOrDefault().creationdate:null;
                actions.blockedbyviewecount = vieweractionstoprofile.Count() > 0? vieweractionstoprofile.Where(z => z.actiontype_id == (int)actiontypeEnum.Block).Count():0;

                return actions;
            }
            catch (Exception ex)
            {
                //TO DO LOG
                throw ex;
            }

            return actions;
        }


        /// <summary>
        /// viewer can be blank or null : simply means we will not look for distance between memebers and other comparaitibe fatures      
        /// </summary>
        /// <param name="veiwerpeofileid"></param>
        /// <param name="viewingprofileid"></param>
        /// <param name="allphotos"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        // use this function to get distance at the same time, add it to the model
        public static MemberSearchViewModel mapmembersearchviewmodel(int? veiwerpeofileid, MemberSearchViewModel modeltomap, IUnitOfWorkAsync db, IGeoDataStoredProcedures _storedProcedures)
        {
            
            //we need lazy loading and proxy stuff
            // db.DisableProxyCreation = false;
          //  db.DisableLazyLoading = false;
            try
            {

                if (veiwerpeofileid != 0)
                {

                    //blank viewprofile simply means we will not look for distance between memebers and other comparaitibe fatures
                    profiledata viewerprofile = new profiledata();
                    if (veiwerpeofileid != null) viewerprofile = db.Repository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = veiwerpeofileid.Value });


                  
                    
                    //since it alrady has some feilds we dont want to overidte with nulls
                    MemberSearchViewModel model = modeltomap; //new MemberSearchViewModel();
                    //TO DO change to use Ninject maybe
                    // DatingService db = new DatingService();
                    //  MembersRepository membersrepo=  new MembersRepository();
                    profile profile = db.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt32(modeltomap.id) });




                    // membersrepository.getprofilebyprofileid(new ProfileModel { profileid = modeltomap.id }); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).Select().FirstOrDefault();
                    //  membereditRepository membereditRepository = new membereditRepository();

                    //TO DO work around this hack by fixing includes extentions
                    //to do build a custom include for this using search :
                //https://www.google.com/?gws_rd=ssl#q=linq++findinculding+extention
                //http://damieng.com/blog/2010/05/21/include-for-linq-to-sql-and-maybe-other-providers
                    //if (profile.profilemetadata == null)
                    //{
                    //    profile.profiledata = db.Repository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = Convert.ToInt32(modeltomap.id) });
                    //     profile.profilemetadata = db.Repository<profilemetadata>().getprofilemetadatabyprofileid(new ProfileModel { profileid = Convert.ToInt32(modeltomap.id)});
                    //}
                    //12-6-2012 olawal added the info for distance between members only if all these values are fufilled
                    if (viewerprofile.latitude != null &&
                        viewerprofile.longitude != null &&
                        profile.profiledata.longitude != null &&
                         profile.profiledata.latitude != null)  //TO DO determine countries that use Mile or KM
                        model.distancefromme = spatialextentions.getdistancebetweenmembers(
                            viewerprofile.latitude.GetValueOrDefault(),
                            viewerprofile.longitude.GetValueOrDefault(),
                             profile.profiledata.latitude.GetValueOrDefault(),
                            profile.profiledata.longitude.GetValueOrDefault(), "M");

                    //we already have this so its overkill
                    // model.id = profile.id;
                    model.screenname = profile.screenname;
                    model.heightbyculture = (profile.profiledata.height == null) ? "Ask Me" : Extensions.ToFeetInches((double)profile.profiledata.height);
                    //model.profiledata = profile.profiledata;
                    //model.profile = profile;
                    model.stateprovince = profile.profiledata.stateprovince;
                    model.postalcode = profile.profiledata.postalcode;
                    model.countryid = profile.profiledata.countryid;
                    model.countryname = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = model.countryid.ToString() }, _storedProcedures);
                    model.genderid = profile.profiledata.gender_id;
                    model.birthdate = profile.profiledata.birthdate;
                    // modelprofile = profile.profile;
                    model.longitude = (double)profile.profiledata.longitude;
                    model.latitude = (double)profile.profiledata.latitude;
                 //   model.hasgalleryphoto = profile.    profilemetadata.photos.Where(i => i.photostatus_id == (int)photostatusEnum.Gallery).FirstOrDefault() != null ? true : false;
                    model.creationdate = profile.creationdate;
                    model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                    model.lastlogindate = profile.logindate;
                    //TO DO move the generic infratructure extentions
                    model.lastloggedonstring = profileextentionmethods.getlastloggedinstring(profile.logindate.GetValueOrDefault());
                    //   membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                    model.mycatchyintroline = profile.profiledata.mycatchyintroLine;
                    model.aboutme = profile.profiledata.aboutme;
                    model.online = db.Repository<profile>().getuseronlinestatus(new ProfileModel { profileid = profile.id });
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
                   // int something = (int)photoformatEnum.Thumbnail;
                    //if (allphotos == true)
                    //{

                    //    model.photosearchresults= db.Repository<photoconversion>().getpagedphotomodelbyprofileidandstatusandalbumid(profile.id,(int)photoformatEnum.Thumbnail,)
                    //    profile.id,(int)photoapprovalstatusEnum.Approved, (int)photoformatEnum.Thumbnail, page, ps);


                    //    model.galleryphoto = model.profilephotos.ProfilePhotosApproved !=null ?model.profilephotos.ProfilePhotosApproved.Where(p => p.photostatusid == (int)photostatusEnum.Gallery).FirstOrDefault():null;
                    //}// approvedphotos = photorepository.
                    //else
                    //{
                        // model.profilephotos.SingleProfilePhoto = photorepository.getphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                    model.galleryphoto = db.Repository<photoconversion>().getgalleryphotomodelbyprofileid(profile.id, (int)photoformatEnum.Medium);

                   // }

                    model.hasgalleryphoto = model.galleryphoto != null ? true : false;
                    // Api.DisposeGeoService();

                    return model;


                }


                return null;

            }
            catch (Exception ex)
            {
                //execptions will be handled in the caller
                throw;
            }

        }


        public static List<MemberSearchViewModel> mapmembersearchviewmodels(ProfileModel model, IUnitOfWorkAsync db, IGeoDataStoredProcedures geodb)
        {

                try
                {

                  
                        List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                        ProfileModel tempmodel = model; //temp storage so we can modif it for use in iteration
                        foreach (var item in model.modelstomap)
                        {
                            //set current model for mapping
                            tempmodel.modeltomap = item;
                            models.Add(membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, tempmodel.modeltomap, db, geodb));

                        }
                        return models;

             


                }
                catch (Exception ex)
                {
                throw ex;
                    //throw convertedexcption;
                }
               

            

        }




        //IMPORTINNYT !

        //THIS NEEDS TO ALSO MAP THE MEMEBERS VIEWMODEL WHEN WE GET TIME
        //constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public static ProfileCriteriaModel getprofilecriteriamodel(int profileid, IUnitOfWorkAsync db)
        {

            // db.DisableProxyCreation = false;
          //  db.DisableLazyLoading = false;
            try
            {



                //load postaldata context
                ProfileCriteriaModel CriteriaModel = new ProfileCriteriaModel();
                if (profileid != 0)
                {

                    MemberSearchViewModel model = new MemberSearchViewModel();
                    //TO DO change to use Ninject maybe
                    // DatingService db = new DatingService();
                    //  MembersRepository membersrepo=  new MembersRepository();
                    profilemetadata metadata = db.Repository<profilemetadata>().getprofilemetadatabyprofileid(new ProfileModel { profileid = profileid });

                    // IKernel kernel = new StandardKernel();

                    //load the geo service for quic user
                    // IGeoRepository georepository = kernel.Get<IGeoRepository>();





                    //instantiate models
                    CriteriaModel.BasicSearchSettings = new BasicSearchSettingsModel();
                    CriteriaModel.AppearanceSearchSettings = new AppearanceSearchSettingsModel();
                    CriteriaModel.LifeStyleSearchSettings = new LifeStyleSearchSettingsModel();
                    CriteriaModel.CharacterSearchSettings = new CharacterSearchSettingsModel();
                    // IKernel kernel = new StandardKernel();
                    //Get these initalized
                    //  MembersRepository membersrepo = kernel.Get<MembersRepository>(); 

                    //TO DO populate these values corrrectly
                    //run a query h ere to populate these values 
                    //Ethnicity =      metadata.profile.profiledata_Ethnicity.Where(
                    //find a way to populate hoby, looking for and ethnicuty from the profiledata_Ethncity and etc

                    //CriteriaModel.ScreenName = (metadata.profile.screenname == null) ? Extensions.ReduceStringLength(metadata.profile.screenname, 10) : Extensions.ReduceStringLength(metadata.profile.screenname, 10);
                    //CriteriaModel.AboutMe = (metadata.profile.profiledata.aboutme == null || metadata.profile.profiledata.aboutme == "Hello") ? "This is the description of the type of person I am looking for.. comming soon. For Now Email Me to find out more about me" : metadata.profile.profiledata.aboutme;
                    //  MyCatchyIntroLine = metadata.prMyCatchyIntroLine == null ? "Hi There!" : metadata.MyCatchyIntroLine;
                    CriteriaModel.BodyType = (metadata.profile.profiledata.lu_bodytype == null & metadata.profile.profiledata.bodytype_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_bodytype.description;
                    CriteriaModel.EyeColor = (metadata.profile.profiledata.lu_eyecolor == null & metadata.profile.profiledata.eyecolor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_eyecolor.description;
                    // Ethnicity = metadata.CriteriaAppearance_Ethnicity == null ? "Ask Me" : metadata.CriteriaAppearance_Ethnicity.EthnicityName;
                    CriteriaModel.HairColor = (metadata.profile.profiledata.lu_haircolor == null & metadata.profile.profiledata.haircolor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_haircolor.description;
                    //TO DO determine weather metic or us sustem user //added 11-17-2011
                    CriteriaModel.HeightByCulture = (metadata.profile.profiledata.height == null & metadata.profile.profiledata.height == 0) ? "Ask Me" : Extensions.ToFeetInches((double)metadata.profile.profiledata.height);

                    CriteriaModel.Exercise = (metadata.profile.profiledata.lu_exercise == null & metadata.profile.profiledata.exercise_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_exercise.description;
                    CriteriaModel.Religion = (metadata.profile.profiledata.lu_religion == null & metadata.profile.profiledata.religion_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_religion.description;
                    CriteriaModel.ReligiousAttendance = (metadata.profile.profiledata.lu_religiousattendance == null & metadata.profile.profiledata.religiousattendance_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_religiousattendance.description;
                    CriteriaModel.Drinks = (metadata.profile.profiledata.lu_drinks == null & metadata.profile.profiledata.drinking_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_drinks.description;
                    CriteriaModel.Smokes = (metadata.profile.profiledata.lu_smokes == null & metadata.profile.profiledata.smoking_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_smokes.description;
                    CriteriaModel.Humor = (metadata.profile.profiledata.lu_humor == null & metadata.profile.profiledata.humor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_humor.description;
                    // HotFeature = metadata.profile.profiledata.CriteriaCharacter_HotFeature == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_HotFeature.HotFeatureName; 
                    //   Hobby =  metadata.profile.profiledata.CriteriaCharacter_Hobby == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_Hobby.HobbyName;
                    CriteriaModel.PoliticalView = (metadata.profile.profiledata.lu_politicalview == null & metadata.profile.profiledata.politicalview_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_politicalview.description;
                    CriteriaModel.Diet = (metadata.profile.profiledata.lu_diet == null & metadata.profile.profiledata.diet_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_diet.description;
                    //TO DO calculate this by bdate
                    CriteriaModel.Sign = (metadata.profile.profiledata.lu_sign == null & metadata.profile.profiledata.sign_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_sign.description;
                    CriteriaModel.IncomeLevel = (metadata.profile.profiledata.lu_incomelevel == null & metadata.profile.profiledata.incomelevel_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_incomelevel.description;
                    CriteriaModel.HaveKids = (metadata.profile.profiledata.lu_havekids == null & metadata.profile.profiledata.kidstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_havekids.description;
                    CriteriaModel.WantsKids = (metadata.profile.profiledata.lu_wantskids == null & metadata.profile.profiledata.wantsKidstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_wantskids.description;
                    CriteriaModel.EmploymentSatus = (metadata.profile.profiledata.lu_employmentstatus == null & metadata.profile.profiledata.employmentstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_employmentstatus.description;
                    CriteriaModel.EducationLevel = (metadata.profile.profiledata.lu_educationlevel == null & metadata.profile.profiledata.educationlevel_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_educationlevel.description;
                    CriteriaModel.Profession = (metadata.profile.profiledata.lu_profession == null & metadata.profile.profiledata.profession_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_profession.description;
                    CriteriaModel.MaritalStatus = (metadata.profile.profiledata.lu_maritalstatus == null & metadata.profile.profiledata.maritalstatus_id == null) ? "Single" : metadata.profile.profiledata.lu_maritalstatus.description;
                    CriteriaModel.LivingSituation = (metadata.profile.profiledata.lu_livingsituation == null & metadata.profile.profiledata.livingsituation_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_livingsituation.description;
                    //special case for ethnicty since they can have mutiples ?
                    //loop though ethnicty thing I guess ?  
                    //8/11/2011 have to loop though since these somehow get detached sometimes
                    //Ethnicity = metadata.profile.profiledata.profiledata_Ethnicity;

                    foreach (var item in metadata.profiledata_ethnicity)
                    {
                        CriteriaModel.Ethnicity.Add(item.lu_ethnicity.description);
                    }

                    foreach (var item in metadata.profiledata_hobby)
                    {
                        CriteriaModel.Hobbies.Add(item.lu_hobby.description);
                    }

                    foreach (var item in metadata.profiledata_lookingfor)
                    {
                        CriteriaModel.LookingFor.Add(item.lu_lookingfor.description);
                    }

                    foreach (var item in metadata.profiledata_hotfeature)
                    {
                        CriteriaModel.HotFeature.Add(item.lu_hotfeature.description);
                    }

                    //handle perfect match settings here .
                    // first load perfect match settings for this user from Initial Catalog=
                    //set defaults if no values are available
                    var PerfectMatchSettings = metadata.searchsettings.FirstOrDefault(); 


                    //basic search settings here
                    CriteriaModel.BasicSearchSettings.distancefromme = (PerfectMatchSettings == null || PerfectMatchSettings.distancefromme == null) ? 500 : PerfectMatchSettings.distancefromme;
                    CriteriaModel.BasicSearchSettings.agemin = (PerfectMatchSettings == null || PerfectMatchSettings.agemin == null) ? 18 : PerfectMatchSettings.agemin;
                    CriteriaModel.BasicSearchSettings.agemax = (PerfectMatchSettings == null || PerfectMatchSettings.agemax == null) ? 99 : PerfectMatchSettings.agemax;

                    if (PerfectMatchSettings != null)
                    {
                        //TO DO add this to search settings for now use what is in profiledata
                        //These will come from search settings table in the future at some point
                        //  CriteriaModel.BasicSearchSettings. = georepository.getcountrynamebycountryid((byte)metadata.profile.profiledata.countryid);  //TO DO allow a range of countries to be selected i.e multi select
                        CriteriaModel.BasicSearchSettings.locationlist = (PerfectMatchSettings == null || PerfectMatchSettings.locations != null) ? PerfectMatchSettings.locations.ToList() : null;
                        //  CriteriaModel.BasicSearchSettings.postalcode  = metadata.profile.profiledata.postalcode;  //this could be for countries withoute p codes


                        //populate list values
                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender))
                        {

                            CriteriaModel.BasicSearchSettings.genderlist.Add(CachingFactory.SharedObjectHelper.getgenderlist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        //appearance search settings here
                        CriteriaModel.AppearanceSearchSettings.heightmaxbyculture = (PerfectMatchSettings == null || PerfectMatchSettings.heightmax == null) ? Extensions.ToFeetInches(48) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmax);
                        CriteriaModel.AppearanceSearchSettings.heightminbyculture = (PerfectMatchSettings == null || PerfectMatchSettings.heightmin == null) ? Extensions.ToFeetInches(89) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmin);

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity))
                        {
                            CriteriaModel.AppearanceSearchSettings.ethnicitylist.Add(CachingFactory.SharedObjectHelper.getethnicitylist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype))
                        {
                            CriteriaModel.AppearanceSearchSettings.bodytypeslist.Add(CachingFactory.SharedObjectHelper.getbodytypelist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor))
                        {
                            CriteriaModel.AppearanceSearchSettings.eyecolorlist.Add(CachingFactory.SharedObjectHelper.geteyecolorlist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor))
                        {
                            CriteriaModel.AppearanceSearchSettings.haircolorlist.Add(CachingFactory.SharedObjectHelper.gethaircolorlist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }


                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature))
                        {
                            CriteriaModel.AppearanceSearchSettings.hotfeaturelist.Add(CachingFactory.SharedObjectHelper.gethotfeaturelist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        //populate lifestyle values here

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.educationlevel))
                        {
                            CriteriaModel.LifeStyleSearchSettings.educationlevellist.Add(CachingFactory.SharedObjectHelper.geteducationlevellist(db).Where(p => p.id == item.value).FirstOrDefault());
                        }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.lookingfor))
                        { CriteriaModel.LifeStyleSearchSettings.lookingforlist.Add(CachingFactory.SharedObjectHelper.getlookingforlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.employmentstatus))
                        { CriteriaModel.LifeStyleSearchSettings.employmentstatuslist.Add(CachingFactory.SharedObjectHelper.getemploymentstatuslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.havekids))
                        { CriteriaModel.LifeStyleSearchSettings.havekidslist.Add(CachingFactory.SharedObjectHelper.gethavekidslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.livingsituation))
                        { CriteriaModel.LifeStyleSearchSettings.livingsituationlist.Add(CachingFactory.SharedObjectHelper.getlivingsituationlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.maritialstatus))
                        { CriteriaModel.LifeStyleSearchSettings.maritalstatuslist.Add(CachingFactory.SharedObjectHelper.getmaritalstatuslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.wantskids))
                        { CriteriaModel.LifeStyleSearchSettings.wantskidslist.Add(CachingFactory.SharedObjectHelper.getwantskidslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.profession))
                        { CriteriaModel.LifeStyleSearchSettings.professionlist.Add(CachingFactory.SharedObjectHelper.getprofessionlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.incomelevel))
                        { CriteriaModel.LifeStyleSearchSettings.incomelevellist.Add(CachingFactory.SharedObjectHelper.getincomelevellist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        //Character settings for search here
                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.diet))
                        { CriteriaModel.CharacterSearchSettings.dietlist.Add(CachingFactory.SharedObjectHelper.getdietlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.humor))
                        { CriteriaModel.CharacterSearchSettings.humorlist.Add(CachingFactory.SharedObjectHelper.gethumorlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hobby))
                        { CriteriaModel.CharacterSearchSettings.hobbylist.Add(CachingFactory.SharedObjectHelper.gethobbylist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.drink))
                        { CriteriaModel.CharacterSearchSettings.drinkslist.Add(CachingFactory.SharedObjectHelper.getdrinkslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        //FIX after Initial Catalog= update
                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.excercise))
                        { CriteriaModel.CharacterSearchSettings.exerciselist.Add(CachingFactory.SharedObjectHelper.getexerciselist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.smokes))
                        { CriteriaModel.CharacterSearchSettings.smokeslist.Add(CachingFactory.SharedObjectHelper.getsmokeslist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sign))
                        { CriteriaModel.CharacterSearchSettings.signlist.Add(CachingFactory.SharedObjectHelper.getsignlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.politicalview))
                        { CriteriaModel.CharacterSearchSettings.politicalviewlist.Add(CachingFactory.SharedObjectHelper.getpoliticalviewlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religion))
                        { CriteriaModel.CharacterSearchSettings.religionlist.Add(CachingFactory.SharedObjectHelper.getreligionlist(db).Where(p => p.id == item.value).FirstOrDefault()); }

                        foreach (var item in PerfectMatchSettings.details.Where(p => p.searchsetting_id == PerfectMatchSettings.id && p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religiousattendance))
                        { CriteriaModel.CharacterSearchSettings.religiousattendancelist.Add(CachingFactory.SharedObjectHelper.getreligiousattendancelist(db).Where(p => p.id == item.value).FirstOrDefault()); }
                    }

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
            catch (Exception ex)
            {
                throw ex;
                //throw convertedexcption;
            }



        }
        //use an overload to return values if a user is not logged in i.e
        //no current profiledata exists to retrive
        public static ProfileCriteriaModel getprofilecriteriamodel()
        {

            // _unitOfWorkAsync.Dispose();

            try
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
            catch (Exception ex)
            {

                throw;

                //throw convertedexcption;
            }



        }

        //functions not exposed via WCF or otherwise
        public static MembersViewModel mapmember(ProfileModel newmodel, IUnitOfWorkAsync db, IGeoDataStoredProcedures geodb)
        {

            // db.DisableProxyCreation = false;
          //  db.DisableLazyLoading = false;
            MembersViewModel model = new MembersViewModel();
            profile profile = new profile();

    

            profile = db.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = newmodel.profileid.Value}); //  .getprofilebyprofileid(newmodel);
           
            //handles failues in lazy loading
            //TO DO this should be a try cacth with exception handling
            if (profile.profilemetadata == null)
            {
                profile.profiledata = db.Repository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = model.profile_id });
                profile.profilemetadata = db.Repository<profilemetadata>().getprofilemetadatabyprofileid(new ProfileModel { profileid = model.profile_id });
            }

            try
            {

                 // new HashSet<int>(profile.profilemetadata.searchsettings.FirstOrDefault().details.Where(p=>p.lu_searchsettingdetailtype == (int)searchsettingdetailtypeEnum.gender) : null;
                //TO DO do away with this since we already have the profile via include from the profile DATA
                // model.Profile = model.profile;
                model.profile_id = profile.id;
                //4-28-2012 added mapping for profile visiblity

                model.profilevisiblity = profile.profiledata.visiblitysettings.FirstOrDefault();   //swict this to have flags 
                model.profile = profile;
               
                //on first load this should always be false
                //to DO   DO  we still need this
                model.profilestatusmessageshown = false;
                model.mygenderid = profile.profiledata.gender_id.GetValueOrDefault();
               
                //get the user's gender and the use the if below to convert it to to default oppos

                if (profile.profilemetadata.searchsettings.FirstOrDefault() != null && profile.profilemetadata.searchsettings.FirstOrDefault().details !=null) 
                {
                    foreach (var item in profile.profilemetadata.searchsettings.FirstOrDefault().details.Where(p =>  p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender))
                    {
                        model.lookingforgendersid.Add(item.value);                        
                    }             
                }
                
                //add default i.e opposite if user does not have it in details
                if (model.lookingforgendersid.Count  != null)
                {
                    model.lookingforgendersid.Add(Extensions.GetLookingForGenderID(profile.profiledata.gender_id.GetValueOrDefault()));
                }


#if DISCONECTED

                model.mycountryname = "United States";// georepository.getcountrynamebycountryid(profile.profiledata.countryid);
#else

                model.mycountryname = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = profile.profiledata.countryid.GetValueOrDefault().ToString() }, geodb);

#endif

                model.mycountryid = profile.profiledata.countryid.GetValueOrDefault();
                model.mycity = profile.profiledata.city;
                //TO DO items need to be populated with real values, in this case change model to double for latt
                model.mylatitude = profile.profiledata.latitude.ToString(); //model.Lattitude
                model.mylongitude = profile.profiledata.longitude.ToString();
                //update 9-21-2011 get fro search settings
               // model.maxdistancefromme = profile.profilemetadata.searchsettings != null ? profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme.GetValueOrDefault() : 500;



                return model;

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        #endregion

    }

}