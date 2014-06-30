using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;
using System.Web;
using System.Net;

using Nmedia.Infrastructure;


using System.ServiceModel.Activation;
using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
//using Nmedia.Infrastructure;.Domain.errorlog;
using Anewluv.DataExtentionMethods;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data;
//using Anewluv.Lib;

using Nmedia.Infrastructure.Domain.Data.errorlog;
using GeoData.Domain.Models;
using Anewluv.Services.Spatial;
using Anewluv.Domain;
using Anewluv.Services.Media;
using Anewluv.Services.Members;
using GeoData.Domain.ViewModels;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;

namespace Anewluv.Services.Mapping
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MembersMapperService : IMembersMapperService
    {

        private int maxwebmatches = 24;
        private int maxemailmatches = 4;
        private int maxsearchresults = 348;
        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private ErroLogging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MembersMapperService(IUnitOfWork unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

            //promotionrepository = _promotionrepository;
            _unitOfWork = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

    

        public MemberSearchViewModel  mapmembersearchviewmodel(string viewerprofileid, MemberSearchViewModel modeltomap, string allphotos)
        {

                var db = _unitOfWork;
                db.DisableProxyCreation = false;
                try
                {

                    if (modeltomap.id != 0)
                    {

                        profiledata viewerprofile = new profiledata();
                        if (viewerprofileid != null) viewerprofile = db.GetRepository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = Convert.ToInt32(viewerprofileid) });

                        MemberSearchViewModel model = modeltomap;
                        //TO DO change to use Ninject maybe
                        // DatingService db = new DatingService();
                        //  MembersRepository membersrepo=  new MembersRepository();
                        profile profile = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt32(modeltomap.id) });
                            // membersrepository.getprofilebyprofileid(new ProfileModel { profileid = modeltomap.id }); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
                        //  membereditRepository membereditRepository = new membereditRepository();

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


                        model.id = profile.id;
                        model.screenname = profile.screenname;

                        //model.profiledata = profile.profiledata;
                        //model.profile = profile;
                        model.stateprovince = profile.profiledata.stateprovince;
                        model.postalcode = profile.profiledata.postalcode;
                        model.countryid = profile.profiledata.countryid;
                        model.genderid = profile.profiledata.gender_id;
                        model.birthdate = profile.profiledata.birthdate;
                        // modelprofile = profile.profile;
                        model.longitude = (double)profile.profiledata.longitude;
                        model.latitude = (double)profile.profiledata.latitude;
                        model.hasgalleryphoto = profile.profilemetadata.photos.Where(i => i.photostatus_id == (int)photostatusEnum.Gallery).FirstOrDefault() != null ? true : false;
                        model.creationdate = profile.creationdate;
                        model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                        model.lastlogindate = profile.logindate;
                        //TO DO move the generic infratructure extentions
                        model.lastloggedonstring = profileextentionmethods.getlastloggedinstring(profile.logindate.GetValueOrDefault());
                            //   membersrepository.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
                        model.mycatchyintroline = profile.profiledata.mycatchyintroLine;
                        model.aboutme = profile.profiledata.aboutme;
                        model.online = db.GetRepository<profile>().getuseronlinestatus(new ProfileModel { profileid = profile.id });
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
                        int something = (int)photoformatEnum.Thumbnail;
                        if (allphotos == "true")
                        {
                          
                            model.profilephotos.ProfilePhotosApproved =db.GetRepository<photoconversion>().getpagedphotomodelbyprofileidandstatus(              
                                profile.id.ToString(),
                                photoapprovalstatusEnum.Approved.ToString(),((int)photoformatEnum.Thumbnail).ToString(), page.ToString(), ps.ToString());   //membereditRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
                        }// approvedphotos = photorepository.
                        else
                        {
                            // model.profilephotos.SingleProfilePhoto = photorepository.getphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                            model.galleryphoto = db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(profile.id.ToString(),((int)photoformatEnum.Thumbnail).ToString());
                        }

                       // Api.DisposeGeoService();

                        return model;


                    }


                    return null;

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //FaultReason faultreason = new FaultReason("Error in member mapper service");
                   // string ErrorMessage = "";
                  //  string ErrorDetail = "ErrorMessage: " + ex.Message;
                   // throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    throw;
                }

              
        }
        public List<MemberSearchViewModel> mapmembersearchviewmodels(string viewerprofileid, List<MemberSearchViewModel> modelstomap, string allphotos)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                    foreach (var item in modelstomap)
                    {
                        models.Add(mapmembersearchviewmodel(viewerprofileid, item, allphotos));

                    }
                    return models;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

         }
        public MemberSearchViewModel getmembersearchviewmodel(string viewerprofileid,string profileid,string allphotos)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    if (profileid != null)
                    {


                        // List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                        MemberSearchViewModel modeltomap = new MemberSearchViewModel();
                        modeltomap.id = Convert.ToInt32(profileid);
                        return (mapmembersearchviewmodel(viewerprofileid, modeltomap, allphotos));



                    }
                    return null;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }     
        public List<MemberSearchViewModel> getmembersearchviewmodels(string viewerprofileid, List<int> profileIds, string allphotos)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                    MemberSearchViewModel modeltomap = new MemberSearchViewModel();
                    foreach (var item in profileIds)
                    {
                        modeltomap = null;
                        modeltomap.id = item;
                        models.Add(mapmembersearchviewmodel(viewerprofileid, modeltomap, allphotos));

                    }
                    return models;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }
        public ProfileBrowseModel getprofilebrowsemodel(string viewerprofileid, string profileid, string allphotos)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var NewProfileBrowseModel = new ProfileBrowseModel
                    {
                        //TO Do user a mapper instead of a contructur and map it from the service
                        //Move all this to a service
                        ViewerProfileDetails = getmembersearchviewmodel(null, viewerprofileid, allphotos),
                        ProfileDetails = getmembersearchviewmodel(null, profileid, allphotos)
                    };

                    //add in the ProfileCritera
                    NewProfileBrowseModel.ViewerProfileCriteria = getprofilecriteriamodel(viewerprofileid);
                    NewProfileBrowseModel.ProfileCriteria = getprofilecriteriamodel(profileid);


                    return NewProfileBrowseModel;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }
        //returns a list of profile browsemodles for a given user
        public List<ProfileBrowseModel> getprofilebrowsemodels(string viewerprofileid, List<int> profileids, string allphotos)
        {
            
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    List<ProfileBrowseModel> BrowseModels = new List<ProfileBrowseModel>();
           
                    foreach (var item in profileids)
                    {
                        var NewProfileBrowseModel = new ProfileBrowseModel
                        {
                            //TO Do user a mapper instead of a contructur and map it from the service
                            //Move all this to a service
                            ViewerProfileDetails = getmembersearchviewmodel(null, viewerprofileid, allphotos),
                            ProfileDetails = getmembersearchviewmodel(null, item.ToString(), allphotos)



                        };

                        //add in the ProfileCritera
                        NewProfileBrowseModel.ViewerProfileCriteria = getprofilecriteriamodel(viewerprofileid);
                        NewProfileBrowseModel.ProfileCriteria = getprofilecriteriamodel(item.ToString());


                        BrowseModels.Add(NewProfileBrowseModel);
                    }

                    return BrowseModels;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }
        // constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public ProfileCriteriaModel getprofilecriteriamodel(string profileid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    MemberSearchViewModel model = new MemberSearchViewModel();
                    //TO DO change to use Ninject maybe
                    // DatingService db = new DatingService();
                    //  MembersRepository membersrepo=  new MembersRepository();
                    profilemetadata metadata =  db.GetRepository<profilemetadata>().getprofilemetadatabyprofileid (new ProfileModel { profileid = Convert.ToInt32(profileid) });


                    //load postaldata context
                    ProfileCriteriaModel CriteriaModel = new ProfileCriteriaModel();
                    if (profileid != null)
                    {

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

                        CriteriaModel.ScreenName = (metadata.profile.screenname == null) ? Extensions.ReduceStringLength(metadata.profile.screenname, 10) : Extensions.ReduceStringLength(metadata.profile.screenname, 10);
                        CriteriaModel.AboutMe = (metadata.profile.profiledata.aboutme == null || metadata.profile.profiledata.aboutme == "Hello") ? "This is the description of the type of person I am looking for.. comming soon. For Now Email Me to find out more about me" : metadata.profile.profiledata.aboutme;
                        //  MyCatchyIntroLine = metadata.prMyCatchyIntroLine == null ? "Hi There!" : metadata.MyCatchyIntroLine;
                        CriteriaModel.BodyType = (metadata.profile.profiledata.lu_bodytype == null | metadata.profile.profiledata.bodytype_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_bodytype.description;
                        CriteriaModel.EyeColor = (metadata.profile.profiledata.lu_eyecolor == null | metadata.profile.profiledata.eyecolor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_eyecolor.description;
                        // Ethnicity = metadata.CriteriaAppearance_Ethnicity == null ? "Ask Me" : metadata.CriteriaAppearance_Ethnicity.EthnicityName;
                        CriteriaModel.HairColor = (metadata.profile.profiledata.lu_haircolor == null | metadata.profile.profiledata.haircolor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_haircolor.description;
                        //TO DO determine weather metic or us sustem user //added 11-17-2011
                        CriteriaModel.HeightByCulture = (metadata.profile.profiledata.height == null | metadata.profile.profiledata.height == 0) ? "Ask Me" : Extensions.ToFeetInches((double)metadata.profile.profiledata.height);

                        CriteriaModel.Exercise = (metadata.profile.profiledata.lu_exercise == null | metadata.profile.profiledata.exercise_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_exercise.description;
                        CriteriaModel.Religion = (metadata.profile.profiledata.lu_religion == null | metadata.profile.profiledata.religion_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_religion.description;
                        CriteriaModel.ReligiousAttendance = (metadata.profile.profiledata.lu_religiousattendance == null | metadata.profile.profiledata.religiousattendance_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_religiousattendance.description;
                        CriteriaModel.Drinks = (metadata.profile.profiledata.lu_drinks == null | metadata.profile.profiledata.drinking_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_drinks.description;
                        CriteriaModel.Smokes = (metadata.profile.profiledata.lu_smokes == null | metadata.profile.profiledata.smoking_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_smokes.description;
                        CriteriaModel.Humor = (metadata.profile.profiledata.lu_humor == null | metadata.profile.profiledata.humor_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_humor.description;
                        // HotFeature = metadata.profile.profiledata.CriteriaCharacter_HotFeature == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_HotFeature.HotFeatureName; 
                        //   Hobby =  metadata.profile.profiledata.CriteriaCharacter_Hobby == null ? "Ask Me" : metadata.profile.profiledata.CriteriaCharacter_Hobby.HobbyName;
                        CriteriaModel.PoliticalView = (metadata.profile.profiledata.lu_politicalview == null | metadata.profile.profiledata.politicalview_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_politicalview.description;
                        CriteriaModel.Diet = (metadata.profile.profiledata.lu_diet == null | metadata.profile.profiledata.diet_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_diet.description;
                        //TO DO calculate this by bdate
                        CriteriaModel.Sign = (metadata.profile.profiledata.lu_sign == null | metadata.profile.profiledata.sign_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_sign.description;
                        CriteriaModel.IncomeLevel = (metadata.profile.profiledata.lu_incomelevel == null | metadata.profile.profiledata.incomelevel_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_incomelevel.description;
                        CriteriaModel.HaveKids = (metadata.profile.profiledata.lu_havekids == null | metadata.profile.profiledata.kidstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_havekids.description;
                        CriteriaModel.WantsKids = (metadata.profile.profiledata.lu_wantskids == null | metadata.profile.profiledata.wantsKidstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_wantskids.description;
                        CriteriaModel.EmploymentSatus = (metadata.profile.profiledata.lu_employmentstatus == null | metadata.profile.profiledata.employmentstatus_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_employmentstatus.description;
                        CriteriaModel.EducationLevel = (metadata.profile.profiledata.lu_educationlevel == null | metadata.profile.profiledata.educationlevel_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_educationlevel.description;
                        CriteriaModel.Profession = (metadata.profile.profiledata.lu_profession == null | metadata.profile.profiledata.profession_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_profession.description;
                        CriteriaModel.MaritalStatus = (metadata.profile.profiledata.lu_maritalstatus == null | metadata.profile.profiledata.maritalstatus_id == null) ? "Single" : metadata.profile.profiledata.lu_maritalstatus.description;
                        CriteriaModel.LivingSituation = (metadata.profile.profiledata.lu_livingsituation == null | metadata.profile.profiledata.livingsituation_id == null) ? "Ask Me" : metadata.profile.profiledata.lu_livingsituation.description;
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
                        // first load perfect match settings for this user from database
                        //set defaults if no values are available
                        var PerfectMatchSettings = metadata.searchsettings.First();


                        //basic search settings here
                        CriteriaModel.BasicSearchSettings.distancefromme = (PerfectMatchSettings == null || PerfectMatchSettings.distancefromme == null) ? 500 : PerfectMatchSettings.distancefromme;
                        CriteriaModel.BasicSearchSettings.agemin = (PerfectMatchSettings == null || PerfectMatchSettings.agemin == null) ? 18 : PerfectMatchSettings.agemin;
                        CriteriaModel.BasicSearchSettings.agemax = (PerfectMatchSettings == null || PerfectMatchSettings.agemax == null) ? 99 : PerfectMatchSettings.agemax;

                        //TO DO add this to search settings for now use what is in profiledata
                        //These will come from search settings table in the future at some point
                        //  CriteriaModel.BasicSearchSettings. = georepository.getcountrynamebycountryid((byte)metadata.profile.profiledata.countryid);  //TO DO allow a range of countries to be selected i.e multi select
                        CriteriaModel.BasicSearchSettings.locationlist = PerfectMatchSettings.searchsetting_location.ToList();
                        //  CriteriaModel.BasicSearchSettings.postalcode  = metadata.profile.profiledata.postalcode;  //this could be for countries withoute p codes

                        //populate list values
                        foreach (var item in PerfectMatchSettings.searchsetting_gender)
                        {
                            CriteriaModel.BasicSearchSettings.genderlist.Add(item.lu_gender);
                        }
                        //appearance search settings here
                        CriteriaModel.AppearanceSearchSettings.heightmax = (PerfectMatchSettings == null || PerfectMatchSettings.heightmax == null) ? Extensions.ToFeetInches(48) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmax);
                        CriteriaModel.AppearanceSearchSettings.heightmin = (PerfectMatchSettings == null || PerfectMatchSettings.heightmin == null) ? Extensions.ToFeetInches(89) : Extensions.ToFeetInches((double)PerfectMatchSettings.heightmin);

                        foreach (var item in PerfectMatchSettings.searchsetting_ethnicity)
                        {
                            CriteriaModel.AppearanceSearchSettings.ethnicitylist.Add(item.lu_ethnicity);
                        }

                        foreach (var item in PerfectMatchSettings.searchsetting_bodytype)
                        {
                            CriteriaModel.AppearanceSearchSettings.bodytypeslist.Add(item.lu_bodytype);
                        }

                        foreach (var item in PerfectMatchSettings.searchsetting_eyecolor)
                        {
                            CriteriaModel.AppearanceSearchSettings.eyecolorlist.Add(item.lu_eyecolor);
                        }

                        foreach (var item in PerfectMatchSettings.searchsetting_haircolor)
                        {
                            CriteriaModel.AppearanceSearchSettings.haircolorlist.Add(item.lu_haircolor);
                        }


                        foreach (var item in PerfectMatchSettings.searchsetting_hotfeature)
                        {
                            CriteriaModel.AppearanceSearchSettings.hotfeaturelist.Add(item.lu_hotfeature);
                        }

                        //populate lifestyle values here

                        foreach (var item in PerfectMatchSettings.searchsetting_educationlevel)
                        { CriteriaModel.LifeStyleSearchSettings.educationlevellist.Add(item.lu_educationlevel); }

                        foreach (var item in PerfectMatchSettings.searchsetting_lookingfor)
                        { CriteriaModel.LifeStyleSearchSettings.lookingforlist.Add(item.lu_lookingfor); }

                        foreach (var item in PerfectMatchSettings.searchsetting_employmentstatus)
                        { CriteriaModel.LifeStyleSearchSettings.employmentstatuslist.Add(item.lu_employmentstatus); }

                        foreach (var item in PerfectMatchSettings.searchsetting_havekids)
                        { CriteriaModel.LifeStyleSearchSettings.havekidslist.Add(item.lu_havekids); }

                        foreach (var item in PerfectMatchSettings.searchsetting_livingstituation)
                        { CriteriaModel.LifeStyleSearchSettings.livingsituationlist.Add(item.lu_livingsituation); }

                        foreach (var item in PerfectMatchSettings.searchsetting_maritalstatus)
                        { CriteriaModel.LifeStyleSearchSettings.maritalstatuslist.Add(item.lu_maritalstatus); }

                        foreach (var item in PerfectMatchSettings.searchsetting_wantkids)
                        { CriteriaModel.LifeStyleSearchSettings.wantskidslist.Add(item.lu_wantskids); }

                        foreach (var item in PerfectMatchSettings.searchsetting_profession)
                        { CriteriaModel.LifeStyleSearchSettings.professionlist.Add(item.lu_profession); }

                        foreach (var item in PerfectMatchSettings.searchsetting_incomelevel)
                        { CriteriaModel.LifeStyleSearchSettings.incomelevellist.Add(item.lu_incomelevel); }

                        //Character settings for search here
                        foreach (var item in PerfectMatchSettings.searchsetting_diet)
                        { CriteriaModel.CharacterSearchSettings.dietlist.Add(item.lu_diet); }

                        foreach (var item in PerfectMatchSettings.searchsetting_humor)
                        { CriteriaModel.CharacterSearchSettings.humorlist.Add(item.lu_humor); }

                        foreach (var item in PerfectMatchSettings.searchsetting_hobby)
                        { CriteriaModel.CharacterSearchSettings.hobbylist.Add(item.lu_hobby); }

                        foreach (var item in PerfectMatchSettings.searchsetting_drink)
                        { CriteriaModel.CharacterSearchSettings.drinkslist.Add(item.lu_drinks); }

                        //FIX after database update
                        foreach (var item in PerfectMatchSettings.searchsetting_exercise)
                        { CriteriaModel.CharacterSearchSettings.exerciselist.Add(item.lu_exercise); }

                        foreach (var item in PerfectMatchSettings.searchsetting_smokes)
                        { CriteriaModel.CharacterSearchSettings.smokeslist.Add(item.lu_smokes); }

                        foreach (var item in PerfectMatchSettings.searchsetting_sign)
                        { CriteriaModel.CharacterSearchSettings.signlist.Add(item.lu_sign); }

                        foreach (var item in PerfectMatchSettings.searchsetting_politicalview)
                        { CriteriaModel.CharacterSearchSettings.politicalviewlist.Add(item.lu_politicalview); }

                        foreach (var item in PerfectMatchSettings.searchsetting_religion)
                        { CriteriaModel.CharacterSearchSettings.religionlist.Add(item.lu_religion); }

                        foreach (var item in PerfectMatchSettings.searchsetting_religiousattendance)
                        { CriteriaModel.CharacterSearchSettings.religiousattendancelist.Add(item.lu_religiousattendance); }


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

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
       
        }
        //use an overload to return values if a user is not logged in i.e
        //no current profiledata exists to retrive
        public ProfileCriteriaModel getprofilecriteriamodel()
        {
 
            _unitOfWork.Dispose();
           
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

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            
       
        }
        //gets search settings
        //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
        public MembersViewModel getdefaultquicksearchsettingsmembers(ProfileModel Model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    quicksearchmodel quicksearchmodel = new quicksearchmodel();
                    MembersViewModel model = this.getmemberdata(Model.profileid.ToString());
                    // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
                    //set deafult paging or pull from DB
                    //quicksearchmodel.myse = 4;
                    quicksearchmodel.myselectedcurrentpage = 1;
                    //added state province with comma 

                    quicksearchmodel.myselectedcity = model.profile.profiledata.city;
                    quicksearchmodel.myselectedmaxdistancefromme = model.profile.profilemetadata.searchsettings.FirstOrDefault().distancefromme != null ? model.maxdistancefromme : 1000;

                    quicksearchmodel.myselectedfromage = model.profile.profilemetadata.searchsettings.FirstOrDefault().agemin != null ? model.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.GetValueOrDefault() : 18;
                    quicksearchmodel.myselectedtoage = model.profile.profilemetadata.searchsettings.FirstOrDefault().agemax != null ? model.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.GetValueOrDefault() : 99; ;


                    quicksearchmodel.myselectediamgenderid = model.profile.profiledata.gender_id.GetValueOrDefault();
                    quicksearchmodel.myselectedstateprovince = model.profile.profiledata.city + "," + model.profile.profiledata.stateprovince; ;
                    //TO DO convert genders to a list of genders 
                    quicksearchmodel.myselectedseekinggenderid = Extensions.GetLookingForGenderID(model.profile.profiledata.gender_id.GetValueOrDefault());
                    quicksearchmodel.myselectedcountryname = model.mycountryname; //use same country for now
                    //add the postal code status here as well
                    
                  PostalData2Context GeoContext = new PostalData2Context();
                  using (var tempdb = GeoContext)
                  {
                      GeoService GeoService = new GeoService(tempdb);

                      quicksearchmodel.myselectedpostalcodestatus = (GeoService.getpostalcodestatusbycountryname(new GeoModel { country = model.mycountryname })) ? true : false;
                  }
                                    //TO do get this from search settings
                    //default for has photos only get this from the 
                    quicksearchmodel.myselectedphotostatus = true;

                    model.myquicksearch = quicksearchmodel;  //save it

                 //   Api.DisposeGeoService();

                    return model;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }
        //populate search settings for guests 
        public MembersViewModel getdefaultsearchsettingsguest(ProfileModel Model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                          MembersViewModel model = new MembersViewModel();
                //check if the country is in our database

                //set defualt values for guests
                //model.myquicksearch.mySelectedPageSize = 4;
                model.myquicksearch.myselectedcurrentpage = 1;
                model.myquicksearch.myselectedcity = "";
                model.mypostalcodestatus = false;
                model.myquicksearch.myselectedmaxdistancefromme = 2000;
                model.myquicksearch.myselectedfromage = 18;
                model.myquicksearch.myselectedtoage = 99;
                model.myquicksearch.myselectediamgenderid = 1;
                model.myquicksearch.myselectedstateprovince = "ALL";
                model.myquicksearch.myselectedseekinggenderid = Extensions.GetLookingForGenderID(1);

                if (Model.Country != "")
                {                    
                    
                  PostalData2Context GeoContext = new PostalData2Context();
                  using (var tempdb = GeoContext)
                  {
                      GeoService GeoService = new GeoService(tempdb);
                      model.myquicksearch.myselectedcountryname = GeoService.getcountryidbycountryname(new GeoModel { country = Model.Country }) == 0 ? "United States" : Model.Country; //use same country for now
                  }
                }
                else
                {
                    model.myquicksearch.myselectedcountryname = "United States";
                }
                model.myquicksearch.myselectedphotostatus = true;

                //Api.DisposeGeoService();

                return model;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(Model.profileid ));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }

        //registration model update and mapping
        public registermodel getregistermodel(MembersViewModel membersmodel)
        {

              _unitOfWork.DisableProxyCreation = true;
              _unitOfWork.Dispose();
                try
                {
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
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(membersmodel.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            

        }
      
        public registermodel getregistermodelopenid(MembersViewModel membersmodel)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
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

                    model.emailaddress = membersmodel.rpxmodel.verifiedemail;
                    model.confirmemailaddress = membersmodel.rpxmodel.verifiedemail;
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


                        AnewluvContext AnewluvContext = new AnewluvContext();
                        using (var tempdb = AnewluvContext)
                        {
                              PhotoService PhotoService = new PhotoService(tempdb);
                              photobeinguploaded.imageb64string = PhotoService.getimageb64stringfromurl(membersmodel.rpxmodel.photo, "");
                          }
                        photobeinguploaded.imagetypeid = db.GetRepository<lu_photoimagetype>().Find().Where(p => p.id == (int)photoimagetypeEnum.Jpeg).FirstOrDefault().id;
                        photobeinguploaded.creationdate = DateTime.Now;
                        photobeinguploaded.caption = membersmodel.rpxmodel.preferredusername;
                        //TO DO rename this to upload image from URL ?

                        //add to repository


                         AnewluvContext = new AnewluvContext();
                         using (var tempdb = AnewluvContext)
                         {
                             PhotoService PhotoService = new PhotoService(tempdb);
                             PhotoService.addphotos(photouploadvm);
                         }
                    }
                    //make sure photos is not empty
                    //  if (membersmodel.MyPhotos == null)
                    // { //add new photo model to members model
                    //    var photolist = new List<Photo>();
                    //    membersmodel.MyPhotos = photolist;
                    // }
                    //don't pass back photos for now
                  //  Api.DisposePhotoService();


                    return model;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(membersmodel.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {
                   // Api.DisposePhotoService(); ;
                }

            }


        }

        public registermodel getregistermodeltest()
        {


            _unitOfWork.DisableProxyCreation = true;
            _unitOfWork.Dispose();
                try
                {
                    registermodel model = new registermodel();
                    //quicksearchmodel quicksearchmodel = new quicksearchmodel();
                    // IEnumerable<CityStateProvince> CityStateProvince ;



                    //model.Ages = sharedrepository.AgesSelectList();
                    // model.Genders = sharedrepository.GendersSelectList();
                    // model.Countries = sharedrepository.CountrySelectList();
                    // model.SecurityQuestions = sharedrepository.SecurityQuestionSelectList();
                    //test values
                    model.birthdate = DateTime.Parse("1/1/1983");

                    model.emailaddress = "ola_lawal@lyahoo.com";
                    model.confirmemailaddress = "ola_lawal@lyahoo.com";
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
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            
        }

        //TOD modifiy client to not bind from this model but load values asycnh
        //other member viewmodl methods
        //TO DO put in cache
        public MembersViewModel updatememberdata(MembersViewModel model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    //return CachingFactory.MembersViewModelHelper.updatememberdata(model, this);
                    //remap the user data if cache is empty
                    //var mm = new ViewModelMapper();
                    return this.mapmember(model.profile_id.ToString());


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }

        public MembersViewModel updatememberdatabyprofileid(string profileid)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                 //   return CachingFactory.MembersViewModelHelper.updatememberprofiledatabyprofile(profileid, this);
      
                   var model = mapmember(profileid);
                    model.profiledata = model.profile.profiledata;
                    return model;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid.ToString()));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
       
       }

        public bool updateguestdata(MembersViewModel model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   // return CachingFactory.MembersViewModelHelper.updateguestdata(model, this);
                    // return this.mapguest();
                    return true;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }
    
        public bool removeguestdata(string sessionid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   // return CachingFactory.MembersViewModelHelper.removeguestdata(sessionid);
                    return true;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex,null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }
        //cacheing of search stuff
        public MembersViewModel getguestdata(string sessionid)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   // return CachingFactory.MembersViewModelHelper.getguestdata(sessionid, this);
                     return this.mapguest();
                
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }
      
        public MembersViewModel getmemberdata(string profileid)
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                //    return CachingFactory.MembersViewModelHelper.getmemberdata(profileid, this);
     
                    return this.mapmember(profileid);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid.ToString()));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }

        //functions not exposed via WCF or otherwise
        public MembersViewModel mapmember(string profileid)
        {

             _unitOfWork.DisableProxyCreation = true;
            var db= _unitOfWork;
            

            MembersViewModel model = new MembersViewModel();
            profile profile = new profile();

            // IEnumerable<CityStateProvince> CityStateProvince ;
            // MailModelRepository mailrepository = new MailModelRepository();
            //var myProfile = membersrepository.GetprofiledataByProfileID(ProfileID).profile;
            // var perfectmatchsearchsettings = membersrepository.GetPerFectMatchSearchSettingsByProfileID(ProfileID);
            // model.Profile = myProfile;
            //Profile data will be on the include
            profile = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt32(profileid) });
            //TO DO this should be a try cacth with exception handling

            try
            {
                //TO DO do away with this since we already have the profile via include from the profile DATA
                // model.Profile = model.profile;
                model.profile_id = profile.id;
                //   model.profile.profiledata.SearchSettings(perfectmatchsearchsettings);
                //4-28-2012 added mapping for profile visiblity
                model.profilevisiblity = profile.profiledata.visiblitysetting;

                //on first load this should always be false
                //to DO   DO  we still need this
                model.profilestatusmessageshown = false;
                model.mygenderid = profile.profiledata.gender_id.GetValueOrDefault();
                //this should come from search settings eventually on the full blown model of this.
                //create hase list of genders they are looking for, if it is null add the default
                //TO DO change this to use membererepo
                model.lookingforgendersid = (profile.profilemetadata.searchsettings.FirstOrDefault() != null) ?
                new HashSet<int>(profile.profilemetadata.searchsettings.FirstOrDefault().searchsetting_gender.Select(c => c.id)) : null;
                if (model.lookingforgendersid != null)
                {
                    model.lookingforgendersid.Add(Extensions.GetLookingForGenderID(profile.profiledata.gender_id.GetValueOrDefault()));
                }

                //set selected value
                //model.Countries. =model.profile.profiledata.CountryID;
                //geographical data poulated here 
                //this is disabled when disconected ok
#if DISCONECTED

                model.mycountryname = "United States";// georepository.getcountrynamebycountryid(profile.profiledata.countryid);
#else
                //TO DO get this from appfabric ( get this list from there and use it from there)
                


                  PostalData2Context GeoContext = new PostalData2Context();
                  using (var tempdb = GeoContext)
                  {
                      GeoService GeoService = new GeoService(tempdb);
                      model.mycountryname = GeoService.getcountrynamebycountryid(new GeoModel { countryid = profile.profiledata.countryid.GetValueOrDefault().ToString() });
                  }
#endif

                model.mycountryid = profile.profiledata.countryid.GetValueOrDefault();
                model.mycity = profile.profiledata.city;
                //TO DO items need to be populated with real values, in this case change model to double for latt
                model.mylatitude = profile.profiledata.latitude.ToString(); //model.Lattitude
                model.mylongitude = profile.profiledata.longitude.ToString();
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
                    //TO DO put into extention so no need to make new service call
                    AnewluvContext AnewluvContext  = new AnewluvContext();
                    using (var tempdb = AnewluvContext)
                    {
                        MemberService MemberService = new MemberService(tempdb);
                        MemberService.createmyperfectmatchsearchsettingsbyprofileid(new ProfileModel { profileid = profile.id });
                    }
                    //update the profile data with the updated value
                    //TO DO stop storing profiledata
                    // model.profiledata = membersrepository.getprofiledata(profile.id);

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
            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(logapplicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid.ToString()));
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member mapper service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }
            finally
            {

               // Api.DisposeGeoService();
              //  Api.DisposeMemberService();
            }

            
            

            

        }

        public MembersViewModel mapguest()
        {

            _unitOfWork.DisableProxyCreation = true;
            _unitOfWork.Dispose();
           
                try
                {
                    MembersViewModel model = new MembersViewModel();
                    quicksearchmodel quicksearchmodel = new quicksearchmodel();
                    // IEnumerable<CityStateProvince> CityStateProvince ;
                     model.myquicksearch = quicksearchmodel;


                    return model;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex,null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            

       
        }


        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(ProfileModel Model)
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    //get search sttings from DB
                    //TO DO change this to not use API call
                    searchsetting perfectmatchsearchsettings = null;
                     AnewluvContext AnewluvContext  = new AnewluvContext();
                     using (var tempdb = AnewluvContext)
                     {
                         MemberService MemberService = new MemberService(tempdb);
                          perfectmatchsearchsettings = MemberService.getperfectmatchsearchsettingsbyprofileid(Model);   //model.profile.profilemetadata.searchsettings.FirstOrDefault();
                     }
                    MembersViewModel model = mapmember(Model.profileid.ToString());

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
                    // List<MemberSearchViewModel> MemberSearchViewmodels;
                    DateTime today = DateTime.Today;
                    DateTime max = today.AddYears(-(intAgeFrom + 1));
                    DateTime min = today.AddYears(-intAgeTo);



                    //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                    //requery all the has tbls
                    HashSet<int> LookingForGenderValues = new HashSet<int>();
                    LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_gender.Select(c => c.id)) : LookingForGenderValues;
                    //Appearacnce seache settings values         

                    //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                    HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                    LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_bodytype.Select(c => c.id)) : LookingForBodyTypesValues;

                    HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                    LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_ethnicity.Select(c => c.id)) : LookingForEthnicityValues;

                    HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                    LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_eyecolor.Select(c => c.id)) : LookingForEyeColorValues;

                    HashSet<int> LookingForHairColorValues = new HashSet<int>();
                    LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_haircolor.Select(c => c.id)) : LookingForHairColorValues;

                    HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                    LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_hotfeature.Select(c => c.id)) : LookingForHotFeatureValues;


                    //******** visiblitysettings test code ************************

                    // test all the values you are pulling here
                    // var TestModel =   (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case")
                    //                      select x).FirstOrDefault();
                    //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1));
                    // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profile.profiledata.GenderID) ? true : false;

                    //  var testmodel2 = (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case" &&  db.fnCheckIfBirthDateIsInRange(x.birthdate, 19, 20) == true  )
                    //                     select x).FirstOrDefault();


                    //****** end of visiblity test settings *****************************************

                    var MemberSearchViewmodels = (from x in db.GetRepository<profiledata>().Find().Where(p => p.birthdate > min && p.birthdate <= max &&
                          p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)).ToList()

                                        //** visiblity settings still needs testing           
                                                      //5-8-2012 add profile visiblity code here
                                                      // .Where(x => x.profile.username == "case")
                                                      //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.ProfileVisiblity == true)
                                                      //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate > today.AddYears(-(x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1)))
                                                      //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate < today.AddYears(-x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault()))
                                                      // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.countryid != null && x.ProfileVisiblitySetting.countryid == model.profile.profiledata.countryid  )
                                                      // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.GenderID != null && x.ProfileVisiblitySetting.GenderID ==  model.profile.profiledata.GenderID )
                                                      //** end of visiblity settings ***
                                     .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender_id.GetValueOrDefault())).ToList() //using whereIF predicate function 
                                     .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.gender_id.GetValueOrDefault())).ToList() //  == model.lookingforgenderid)    
                                                      //TO DO add the rest of the filitering here 
                                                      //Appearance filtering                         
                                     .WhereIf(blEvaluateHeights, z => z.height > intheightmin && z.height <= intheightmax).ToList() //Only evealuate if the user searching actually has height values they look for                         
                                                  join f in db.GetRepository<profile>().Find() on x.profile_id equals f.id
                                                  select new MemberSearchViewModel
                                                  {
                                                      // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                      id = x.profile_id,
                                                      stateprovince = x.stateprovince,
                                                      postalcode = x.postalcode,
                                                      countryid = x.countryid,
                                                      genderid = x.gender_id,
                                                      birthdate = x.birthdate,
                                                      //profile = f,
                                                      screenname = f.screenname,
                                                      longitude = x.longitude ?? 0,
                                                      latitude = x.latitude ?? 0,
                                                      hasgalleryphoto = (db.GetRepository<photo>().Find().Where(i => i.profile_id == f.id && i.photostatus_id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                                      creationdate = f.creationdate,
                                                      // city = db.fnTruncateString(x.city, 11),
                                                      // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                      lastlogindate = f.logindate,
                                                      distancefromme = spatialextentions.getdistancebetweenmembers((double)x.latitude, (double)x.longitude, myLattitude.Value, myLongitude.Value, "Miles")
                                                      //TO DO look at this and explore
                                                      //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                      //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                                                      //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",


                                                  }).OrderByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();





                    //11/20/2011 handle case where  no profiles were found
                    if (MemberSearchViewmodels.Count() == 0)
                        return this.getquickmatcheswhenquickmatchesempty(new ProfileModel { profileid = Model.profileid }).Take(maxwebmatches).ToList();


                    //filter our the ones in the right distance and reutnr the top webmacthes
                    var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                        .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                                                                    select q).Take(maxwebmatches)
                                                                :
                        MemberSearchViewmodels.Take(maxwebmatches);

                    //do any conversions and calcs here
                    return profiles.
                    Select(x => new MemberSearchViewModel
                    {
                        // MyCatchyIntroLineQuickSearch = x.AboutMe,
                        id = x.id,
                        stateprovince = x.stateprovince,
                        postalcode = x.postalcode,
                        countryid = x.countryid,
                        genderid = x.genderid,
                        birthdate = x.birthdate,
                        profile = x.profile,
                        screenname = x.screenname,
                        longitude = x.longitude ?? 0,
                        latitude = x.latitude ?? 0,
                        hasgalleryphoto = x.hasgalleryphoto,
                        creationdate = x.creationdate,
                        // city = db.fnTruncateString(x.city, 11),
                        lastloggedonstring = profileextentionmethods.getlastloggedinstring(x.lastlogindate.GetValueOrDefault()),
                        lastlogindate = x.lastlogindate,
                        distancefromme = x.distancefromme,
                        galleryphoto = db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(x.id.ToString(), ((int)photoformatEnum.Thumbnail).ToString()),
                        lookingforagefrom = x.lookingforagefrom,
                        lookingForageto = x.lookingForageto,
                        online = db.GetRepository<profile>().getuseronlinestatus(new ProfileModel { profileid = x.id })
                    }).ToList();

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {
                  //  Api.DisposeGeoService();
                  //  Api.DisposeMemberService();
               //   Api.DisposePhotoService();

                }

            }

         }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(ProfileModel Model)
        {

            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {
                    profile profile = new profile();
                    profile = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = (Model.profileid) });
                    MembersViewModel model = mapmember(Model.profileid.ToString());


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
                    //  List<MemberSearchViewModel> MemberSearchViewmodels;
                    DateTime today = DateTime.Today;
                    DateTime max = today.AddYears(-(intAgeFrom + 1));
                    DateTime min = today.AddYears(-intAgeTo);



                    //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                    //requery all the has tbls
                    HashSet<int> LookingForGenderValues = new HashSet<int>();
                    LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_gender.Select(c => c.id)) : LookingForGenderValues;
                    //Appearacnce seache settings values         

                    //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                    HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                    LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_bodytype.Select(c => c.id)) : LookingForBodyTypesValues;

                    HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                    LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_ethnicity.Select(c => c.id)) : LookingForEthnicityValues;

                    HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                    LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_eyecolor.Select(c => c.id)) : LookingForEyeColorValues;

                    HashSet<int> LookingForHairColorValues = new HashSet<int>();
                    LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_haircolor.Select(c => c.id)) : LookingForHairColorValues;

                    HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                    LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_hotfeature.Select(c => c.id)) : LookingForHotFeatureValues;

                   // var photostest = _datingcontext.profiles.Where(p => (p.profilemetadata.photos.Any(z => z.photostatus != null && z.photostatus.id != (int)photostatusEnum.Gallery)));

                    //add more values as we get more members 
                    //TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                    var MemberSearchViewmodels = (from x in db.GetRepository<profiledata>().Find().Where(p => p.birthdate > min && p.birthdate <= max &&
                          p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)).ToList()
                                    .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.lu_gender.id)).ToList() //using whereIF predicate function 
                                    .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.lu_gender.id)).ToList()
                                                      //Appearance filtering not implemented yet                        
                                    .WhereIf(blEvaluateHeights, z => z.height > intheightmin && z.height <= intheightmax).ToList() //Only evealuate if the user searching actually has height values they look for 
                                                  //we have to filter on the back end now since we cant use UDFs
                                                  // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                                                  join f in db.GetRepository<profile>().Find() on x.profile_id equals f.id
                                                  select new MemberSearchViewModel
                                                  {
                                                      // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                      id = x.profile_id,
                                                      stateprovince = x.stateprovince,
                                                      postalcode = x.postalcode,
                                                      countryid = x.countryid,
                                                      genderid = x.gender_id,
                                                      birthdate = x.birthdate,
                                                      //profile = f,
                                                      screenname = f.screenname,
                                                      longitude = x.longitude ?? 0,
                                                      latitude = x.latitude ?? 0,
                                                      // hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                                      creationdate = f.creationdate,
                                                      // city = db.fnTruncateString(x.city, 11),
                                                      // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                      lastlogindate = f.logindate,
                                                      distancefromme = spatialextentions.getdistancebetweenmembers((double)x.latitude, (double)x.longitude, myLattitude.Value, myLongitude.Value, "Miles")
                                                      //TO DO look at this and explore
                                                      //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                      //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                                                      //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",


                                                  }).OrderByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();





                    //11/20/2011 handle case where  no profiles were found
                    if (MemberSearchViewmodels.Count() == 0)
                        return getquickmatcheswhenquickmatchesempty(new ProfileModel { profileid = Model.profileid }).Take(maxemailmatches).ToList();

                    //filter our the ones in the right distance and reutnr the top webmacthes
                    //USes max search results snce this could be called by any other method with a variable set of return macthes or results
                    var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                        .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                                                                    select q).Take(maxemailmatches)
                                                                :
                        MemberSearchViewmodels.Take(maxemailmatches);

                    //do any conversions and calcs here
                    return profiles.
                    Select(x => new MemberSearchViewModel
                    {
                        // MyCatchyIntroLineQuickSearch = x.AboutMe,
                        id = x.id,
                        stateprovince = x.stateprovince,
                        postalcode = x.postalcode,
                        countryid = x.countryid,
                        genderid = x.genderid,
                        birthdate = x.birthdate,
                        profile = x.profile,
                        screenname = x.screenname,
                        longitude = x.longitude ?? 0,
                        latitude = x.latitude ?? 0,
                        hasgalleryphoto = x.hasgalleryphoto,
                        creationdate = x.creationdate,
                        // city = db.fnTruncateString(x.city, 11),                       
                                  lastloggedonstring = profileextentionmethods.getlastloggedinstring(x.lastlogindate.GetValueOrDefault()),
                    lastlogindate = x.lastlogindate,
                    distancefromme = x.distancefromme,
                        galleryphoto = db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(x.id.ToString(), ((int)photoformatEnum.Thumbnail).ToString()),
                        lookingforagefrom = x.lookingforagefrom,
                    lookingForageto = x.lookingForageto,
                    online = db.GetRepository<profile>().getuseronlinestatus(new ProfileModel { profileid = x.id })
           

                    }).ToList();


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {
                  //  Api.DisposeGeoService();
                 //   Api.DisposeMemberService();
                  //  Api.DisposePhotoService();

                }

            }





        }

       //search functions that should be moved to thier own service when time allows

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public async Task<List<MemberSearchViewModel>> getquicksearch(quicksearchmodel Model)
        {

            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {

                     var task = Task.Factory.StartNew(() =>
                        {

                          
                            //get the  gender's from search settings
                            int genderid = Model.myselectedseekinggenderid.GetValueOrDefault();
                            int mygenderid = Model.myselectediamgenderid.GetValueOrDefault();

                            
                            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
                            int AgeTo = Model.myselectedtoage != null ? Model.myselectedtoage.GetValueOrDefault() : 99;
                            int AgeFrom = Model.myselectedfromage != null ? Model.myselectedfromage.GetValueOrDefault() : 18;
                            //Height
                            //int intheightmin = Model.h != null ? Model.heightmin.GetValueOrDefault() : 0;
                           // int intheightmax = Model.heightmax != null ? Model.heightmax.GetValueOrDefault() : 100;
                          //  bool blEvaluateHeights = intheightmin > 0 ? true : false;
                            //get the rest of the values if they are needed in calculations
                            //convert lattitudes from string (needed for JSON) to bool           
                            double? myLongitude = (Model.myselectedlongitude != null) ? Convert.ToDouble(Model.myselectedlongitude) : 0;
                            double? myLattitude = (Model.myselectedlatitude != null) ? Convert.ToDouble(Model.myselectedlatitude) : 0;
                            

                            //set variables
                            //  List<MemberSearchViewModel> MemberSearchViewmodels;
                            DateTime today = DateTime.Today;
                            DateTime max = today.AddYears(-(AgeFrom + 1));
                            DateTime min = today.AddYears(-AgeTo);

                            //get country and city data
                            string countryname = Model.myselectedcountryname;
                            int countryid = Model.myselectedcountryid.GetValueOrDefault();
                            // myselectedcountryid 
                            string stringpostalcode = Model.myselectedpostalcode;

                            //added 10/17/20011 so we can toggle postalcode box similar to register 
                            string  city =Model.myselectedcity;
                            int photostatus = (Model.myselectedphotostatus !=null) ? (int)photostatusEnum.Gallery : (int)photostatusEnum.Nostatus;
                            string stateprovince = Model.myselectedstateprovince;
                            double? maxdistancefromme = Model.myselectedmaxdistancefromme;



                            //skip these
                            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                            //requery all the has tbls
                            HashSet<int> LookingForGenderValues = new HashSet<int>();
                            LookingForGenderValues.Add(genderid);  //add the gender id being searched for


                            ////Appearacnce seache settings values         

                            ////set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                            //HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                            //LookingForBodyTypesValues = (Model != null) ? new HashSet<int>(Model.searchsetting_bodytype.Select(c => c.id)) : LookingForBodyTypesValues;

                            //HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                            //LookingForEthnicityValues = (Model != null) ? new HashSet<int>(Model.searchsetting_ethnicity.Select(c => c.id)) : LookingForEthnicityValues;

                            //HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                            //LookingForEyeColorValues = (Model != null) ? new HashSet<int>(Model.searchsetting_eyecolor.Select(c => c.id)) : LookingForEyeColorValues;

                            //HashSet<int> LookingForHairColorValues = new HashSet<int>();
                            //LookingForHairColorValues = (Model != null) ? new HashSet<int>(Model.searchsetting_haircolor.Select(c => c.id)) : LookingForHairColorValues;

                            //HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                            //LookingForHotFeatureValues = (Model != null) ? new HashSet<int>(Model.searchsetting_hotfeature.Select(c => c.id)) : LookingForHotFeatureValues;

                            // var photostest = _datingcontext.profiles.Where(p => (p.profilemetadata.photos.Any(z => z.photostatus != null && z.photostatus.id != (int)photostatusEnum.Gallery)));

                            //add more values as we get more members 
                            //TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                            var MemberSearchViewmodels = (from x in db.GetRepository<profiledata>().Find().Where(p => p.birthdate > min && p.birthdate <= max &&
                             p.countryid == countryid && p.city == city && p.stateprovince == stateprovince).ToList()
                                                         
                                                        
                                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.lu_gender.id)).ToList() //using whereIF predicate function  
                                            
                                                              //Appearance filtering not implemented yet                        
                                          //Only evealuate if the user searching actually has height values they look for 
                                                          //we have to filter on the back end now since we cant use UDFs
                                                          // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                                                          join f in db.GetRepository<profile>().Find() on x.profile_id equals f.id
                                                          select new MemberSearchViewModel
                                                          {
                                                              // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                              id = x.profile_id,
                                                              stateprovince = x.stateprovince,
                                                              postalcode = x.postalcode,
                                                              countryid = x.countryid,
                                                              genderid = x.gender_id,
                                                              birthdate = x.birthdate,
                                                              //profile = f,
                                                              screenname = f.screenname,
                                                              longitude = x.longitude ?? 0,
                                                              latitude = x.latitude ?? 0,
                                                              // hasgalleryphoto = (_datingcontext.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                                              creationdate = f.creationdate,
                                                              // city = db.fnTruncateString(x.city, 11),
                                                              // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                              lastlogindate = f.logindate,
                                                              distancefromme = spatialextentions.getdistancebetweenmembers((double)x.latitude, (double)x.longitude, myLattitude.Value, myLongitude.Value, "Miles")
                                                              //TO DO look at this and explore
                                                              //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                              //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                                                              //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",


                                                          }).OrderByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();




                            //Come back to these filiters later
                            //11/20/2011 handle case where  no profiles were found
                            //if (MemberSearchViewmodels.Count() == 0)
                               // return null; //getquickmatcheswhenquickmatchesempty(new ProfileModel { profileid = Model.profileid }).Take(maxemailmatches).ToList();

                            //filter our the ones in the right distance and reutnr the top webmacthes
                            //USes max search results snce this could be called by any other method with a variable set of return macthes or results
                           // var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                            //    .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                            //                                                select q).Take(maxemailmatches)
                            //                                            :
                           //     MemberSearchViewmodels.Take(maxemailmatches);

                            //do any conversions and calcs here
                            return MemberSearchViewmodels.
                            Select(x => new MemberSearchViewModel
                            {
                                // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                id = x.id,
                                stateprovince = x.stateprovince,
                                postalcode = x.postalcode,
                                countryid = x.countryid,
                                genderid = x.genderid,
                                birthdate = x.birthdate,
                                profile = x.profile,
                                screenname = x.screenname,
                                longitude = x.longitude ?? 0,
                                latitude = x.latitude ?? 0,
                                hasgalleryphoto = x.hasgalleryphoto,
                                creationdate = x.creationdate,
                                // city = db.fnTruncateString(x.city, 11),                       
                                lastloggedonstring = profileextentionmethods.getlastloggedinstring(x.lastlogindate.GetValueOrDefault()),
                                lastlogindate = x.lastlogindate,
                                distancefromme = x.distancefromme,
                                galleryphoto = db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(x.id.ToString(), ((int)photoformatEnum.Thumbnail).ToString()),
                                lookingforagefrom = x.lookingforagefrom,
                                lookingForageto = x.lookingForageto,
                                online = db.GetRepository<profile>().getuseronlinestatus(new ProfileModel { profileid = x.id })


                            }).ToList();
                   });
                   return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {
                    //  Api.DisposeGeoService();
                    //   Api.DisposeMemberService();
                    //  Api.DisposePhotoService();

                }

            }





        }


        //TO DO clean up and just use gener and newest
        internal List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(ProfileModel profilemodel)
        {

            var db = _unitOfWork;
            _unitOfWork.DisableProxyCreation = false;
            try
            {

                //TO DO change to use unit of work in here
                //get search sttings from DB

                profile profile = new profile();
                profile = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = (profilemodel.profileid) });
               // MembersViewModel model = mapmember(profilemodel.profileid.ToString());

                MembersViewModel model = this.getmemberdata(profilemodel.profileid.ToString());

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
                //List<MemberSearchViewModel> MemberSearchViewmodels;
                DateTime today = DateTime.Today;
                DateTime max = today.AddYears(-(intAgeFrom + 1));
                DateTime min = today.AddYears(-intAgeTo);
                //convert lattitudes from string (needed for JSON) to bool
                double? myLongitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylongitude) : 0;
                double? myLattitude = (model.mylatitude != "") ? Convert.ToDouble(model.mylatitude) : 0;



                //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                //requery all the has tbls
                HashSet<int> LookingForGenderValues = new HashSet<int>();
                LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.searchsetting_gender.Select(c => c.id)) : LookingForGenderValues;


                //  where (LookingForGenderValues.Count !=null || LookingForGenderValues.Contains(x.GenderID)) 
                //  where (LookingForGenderValues.Count == null || x.GenderID == UserProfile.MyQuickSearch.MySelectedSeekingGenderID )   //this should not run if we have no gender in searchsettings
                var MemberSearchViewmodels = (from x in db.GetRepository<profiledata>().Find().Where(p => p.birthdate > min && p.birthdate <= max &&
                    p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)).ToList()
                                .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender_id.GetValueOrDefault())).ToList() //using whereIF predicate function 
                                .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.lu_gender.id)).ToList()

                                              join f in db.GetRepository<profile>().Find() on x.profile_id equals f.id
                                              select new MemberSearchViewModel
                                              {
                                                  // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                  id = x.profile_id,
                                                  stateprovince = x.stateprovince,
                                                  postalcode = x.postalcode,
                                                  countryid = x.countryid,
                                                  genderid = x.gender_id.GetValueOrDefault(),
                                                  birthdate = x.birthdate,
                                                  profile = f,
                                                  screenname = f.screenname,
                                                  longitude = x.longitude ?? 0,
                                                  latitude = x.latitude ?? 0,
                                                  hasgalleryphoto = true,  //set inthe above query 
                                                  creationdate = f.creationdate,
                                                  // city = db.fnTruncateString(x.city, 11),
                                                  // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                  lastlogindate = f.logindate,
                                                  distancefromme = spatialextentions.getdistancebetweenmembers((double)x.latitude, (double)x.longitude, myLattitude.Value, myLongitude.Value, "Miles")
                                                  //TO DO look at this and explore
                                                  //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                  //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                                                  //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",


                                              }).OrderByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();


                //filter our the ones in the right distance and reutnr the top webmacthes
                var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                    .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                                                                select q).Take(maxwebmatches)
                                                            :
                    MemberSearchViewmodels.Take(maxwebmatches);

                //do any conversions and calcs here
                return profiles.
                Select(x => new MemberSearchViewModel
                {
                    // MyCatchyIntroLineQuickSearch = x.AboutMe,
                    id = x.id,
                    stateprovince = x.stateprovince,
                    postalcode = x.postalcode,
                    countryid = x.countryid,
                    genderid = x.genderid,
                    birthdate = x.birthdate,
                    profile = x.profile,
                    screenname = x.screenname,
                    longitude = x.longitude ?? 0,
                    latitude = x.latitude ?? 0,
                    hasgalleryphoto = x.hasgalleryphoto,
                    creationdate = x.creationdate,
                    // city = db.fnTruncateString(x.city, 11),
                    lastloggedonstring = profileextentionmethods.getlastloggedinstring(x.lastlogindate.GetValueOrDefault()),
                    lastlogindate = x.lastlogindate,
                    distancefromme = x.distancefromme,
                    galleryphoto = db.GetRepository<photoconversion>().getgalleryphotomodelbyprofileid(x.id.ToString(),((int)photoformatEnum.Thumbnail).ToString()),
                    lookingforagefrom = x.lookingforagefrom,
                    lookingForageto = x.lookingForageto,
                    online = db.GetRepository<profile>().getuseronlinestatus(new ProfileModel { profileid = x.id })
                }).ToList();



                //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
                //final ordering 
                // profiles = profiles.OrderByDescending(p => p.hasgalleryphoto == true).ThenByDescending(p => p.creationdate)


                //  return profiles.ToList();
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(logapplicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profilemodel.profileid ));
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                

                throw;
            }
            finally
            {
               // Api.DisposeGeoService();
              //  Api.DisposeMemberService();
             //   Api.DisposePhotoService();

            }
        }

    }
}
