using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using System.Web;
using System.Net;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;
using Anewluv.DataAccess.Interfaces;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Anewluv.DataAccess.ExtentionMethods;

namespace Shell.MVC2.Services.Actions
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MembersMapperService : IMembersMapperService
    {

        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;

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

        //BEfore unit of work contrcutor
        //public MemberActionsService(IMemberActionsRepository memberactionsrepository)
        //    {
        //        _memberactionsrepository = memberactionsrepository;
        //       // _apikey  = HttpContext.Current.Request.QueryString["apikey"];
        //      //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        //    }


        // constructor

        public MemberSearchViewModel  mapmembersearchviewmodel(string viewerprofileid, MemberSearchViewModel modeltomap, string allphotos)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
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
                        profile profile = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt32(viewerprofileid) });
                            // membersrepository.getprofilebyprofileid(new ProfileModel { profileid = modeltomap.id }); //db.profiledatas.Include("profile").Include("SearchSettings").Where(p=>p.ProfileID == ProfileId).FirstOrDefault();
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
                        model.hasgalleryphoto = profile.profilemetadata.photos.Where(i => i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault() != null ? true : false;
                        model.creationdate = profile.creationdate;
                        model.city = Extensions.ReduceStringLength(profile.profiledata.city, 11);
                        model.lastlogindate = profile.logindate;
                        //TO DO move the generic infratructure extentions
                        model.lastloggedonstring = profileextentionmethods.getlastloggedinstring(model.lastlogindate.GetValueOrDefault());
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
                        if (allphotos == "true")
                        {
                            model.profilephotos.ProfilePhotosApproved = photorepository.getpagedphotomodelbyprofileidandstatus(profile.id, photoapprovalstatusEnum.Approved, photoformatEnum.Thumbnail, page, ps);   //membereditRepository.GetPhotoViewModel(Approved, NotApproved, Private, MyPhotos);
                        }// approvedphotos = photorepository.
                        else
                        {
                            // model.profilephotos.SingleProfilePhoto = photorepository.getphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                            model.galleryphoto = photorepository.getgalleryphotomodelbyprofileid(profile.id, photoformatEnum.Thumbnail);
                        }
                        return model;
                    }
                    return null;

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(viewerprofileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            return _mapmembermapperrepo.mapmembersearchviewmodel(Convert.ToInt32(viewerprofileid), modeltomap, Convert.ToBoolean(allphotos));
        }

       public List<MemberSearchViewModel> mapmembersearchviewmodels(string viewerprofileid, List<MemberSearchViewModel> modelstomap, string allphotos)
        {

            return _mapmembermapperrepo.mapmembersearchviewmodels(Convert.ToInt32(viewerprofileid), modelstomap, Convert.ToBoolean(allphotos));
        }

        public MemberSearchViewModel getmembersearchviewmodel(string viewerprofileid,string profileid,string allphotos)
        {
            return _mapmembermapperrepo.getmembersearchviewmodel(Convert.ToInt32(profileid), Convert.ToInt32(profileid), Convert.ToBoolean(allphotos));
        }
        public List<MemberSearchViewModel> getmembersearchviewmodels(string viewerprofileid, List<int> profileIds, string allphotos)
        {
            return _mapmembermapperrepo.getmembersearchviewmodels(Convert.ToInt32(viewerprofileid), profileIds, Convert.ToBoolean(allphotos));
        }
        public ProfileBrowseModel getprofilebrowsemodel(string viewerprofileId, string profileid, string allphotos)
        {
            return _mapmembermapperrepo.getprofilebrowsemodel(Convert.ToInt32(viewerprofileId), Convert.ToInt32(profileid), Convert.ToBoolean(allphotos));
        }
        //returns a list of profile browsemodles for a given user
        public List<ProfileBrowseModel> getprofilebrowsemodels(string viewerprofileId, List<int> profileIds, string allphotos)
        {
            return _mapmembermapperrepo.getprofilebrowsemodels(Convert.ToInt32(viewerprofileId), profileIds, Convert.ToBoolean(allphotos));
        }
        // constructor
        //4-12-2012 added screen name
        //4-18-2012 added search settings
        public ProfileCriteriaModel getprofilecriteriamodel(string profileid)
        {
            return _mapmembermapperrepo.getprofilecriteriamodel(Convert.ToInt32(profileid));          

        }
        //use an overload to return values if a user is not logged in i.e
        //no current profiledata exists to retrive
        public ProfileCriteriaModel getprofilecriteriamodel()
        {

            return _mapmembermapperrepo.getprofilecriteriamodel();
        }
        //gets search settings
        //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
        public MembersViewModel getdefaultquicksearchsettingsmembers(ProfileModel Model)
        {
            return _mapmembermapperrepo.getdefaultquicksearchsettingsmembers(Model);
        }
        //populate search settings for guests 
        public MembersViewModel getdefaultsearchsettingsguest(ProfileModel Model)
        {
            return _mapmembermapperrepo.getdefaultsearchsettingsguest(Model);
        }

        //registration model update and mapping
        public registermodel getregistermodel(MembersViewModel membersmodel)
        {
            return _mapmembermapperrepo.getregistermodel(membersmodel);          
        }
        public registermodel getregistermodelopenid(MembersViewModel membersmodel)
        {
            return _mapmembermapperrepo.getregistermodelopenid(membersmodel);
        }
        public registermodel getregistermodeltest()
        {
            return _mapmembermapperrepo.getregistermodeltest();
        }

        //TOD modifiy client to not bind from this model but load values asycnh
        //other member viewmodl methods
        public MembersViewModel updatememberdata(MembersViewModel model)
        {
            return _mapmembermapperrepo.updatememberdata(model);
        }
        public MembersViewModel updatememberdatabyprofileid(string profileid)
        {
            return _mapmembermapperrepo.updatememberdatabyprofileid(Convert.ToInt32(profileid));
        }
        public bool updateguestdata(MembersViewModel model)
        {
            return _mapmembermapperrepo.updateguestdata(model);
        }
        public bool removeguestdata(string sessionid)
        {
            return _mapmembermapperrepo.removeguestdata(sessionid);
        }
        //cacheing of search stuff
        public MembersViewModel getguestdata(string sessionid)
        {
            return _mapmembermapperrepo.getguestdata(sessionid);
        }
        public MembersViewModel getmemberdata(string profileid)
        {
            return _mapmembermapperrepo.getmemberdata(Convert.ToInt32(profileid));
        }

        //functions not exposed via WCF or otherwise
        public MembersViewModel mapmember(string profileid)
        {
            return _mapmembermapperrepo.mapmember(Convert.ToInt32(profileid));
           

        }

        public MembersViewModel mapguest()
        {
            return _mapmembermapperrepo.mapguest();
        }


        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(ProfileModel model)
        {


            return _mapmembermapperrepo.getquickmatches(model);




        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(ProfileModel model)
        {



            return _mapmembermapperrepo.getemailmatches(model);



        }
        //public List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(ProfileModel model)
        //{

        //    return _mapmembermapperrepo.getquickmatcheswhenquickmatchesempty(model);

        //}

    }
}
