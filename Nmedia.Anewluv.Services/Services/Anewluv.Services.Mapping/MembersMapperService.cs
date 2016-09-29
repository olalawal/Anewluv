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
//using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
//using Nmedia.Infrastructure;.Domain.log;
using Anewluv.DataExtentionMethods;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data;
//using Anewluv.Lib;

using Nmedia.Infrastructure.Domain.Data.log;
using GeoData.Domain.Models;

using Anewluv.Domain;


using GeoData.Domain.ViewModels;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;
using Anewluv.Api;
using Nmedia.Infrastructure.DependencyInjection;
using Repository.Pattern.UnitOfWork;



//  <!-- THis code uses mars due to threading issue with  an injected data context.  The best solutuion is a data content tracker i.e http://mehdi.me/ has a dbcontext scope i want to test and maybe implement in furthe releases
//   this works for reads but for writes it would be a terrible idea -->
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

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        //  private readonly IUnitOfWorkAsync _geodatastoredProcedures;
        private readonly IGeoDataStoredProcedures _geodatastoredProcedures;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;



        public MembersMapperService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork, [ISpatialEntitesScope]IGeoDataStoredProcedures storedProcedures)
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
            _unitOfWorkAsync = unitOfWork;
            _geodatastoredProcedures = storedProcedures; ;

            //disable proxy stuff by default
            //_unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }




        /// maps a single profile to another
        /// </summary>
        /// <param name="model"></param>
        /// <param name="allphotos"></param>
        /// <returns></returns>
        public async Task<MemberSearchViewModel> getmembersearchviewmodel(ProfileModel model)
        {


            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {


                        return membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, model.modeltomap, db, geodb);


                    });
                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //FaultReason faultreason = new FaultReason("Error in member mapper service");
                    // string ErrorMessage = "";
                    //  string ErrorDetail = "ErrorMessage: " + ex.Message;
                    // throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    throw;
                }
                finally
                {
                    // 
                }
            }

        }

        public async Task<SearchResultsViewModel> getmembersearchviewmodels(ProfileModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        var returnmodel = new SearchResultsViewModel();
                        returnmodel.totalresults = model.profileids.Count();
                        List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
                        MemberSearchViewModel modeltomap = new MemberSearchViewModel();
                        ProfileModel tempmodel = model; //temp storage so we can modif it for use in iteration
                        foreach (var item in model.profileids)
                        {
                            modeltomap = null;
                            modeltomap.id = Convert.ToInt32(item);
                            tempmodel.modeltomap = modeltomap;
                            returnmodel.results.Add(membermappingextentions.mapmembersearchviewmodel(tempmodel.profileid, tempmodel.modeltomap, db, geodb));


                        }
                        return returnmodel;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }

            }
        }


        //TO DO use roles to determine what photos and other data a user sees btw  
        /// <summary>
        /// profile detail that compares two users
        /// the viewer is the profileid and the person being viewed is the viewing profileid
        /// </summary>
        /// <param name="viewerprofileid"></param>
        /// <param name="profileid"></param>
        /// <param name="allphotos"></param>
        /// <returns></returns>
        public async Task<ProfileBrowseModel> getprofilebrowsemodel(ProfileModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        //THIS NEEDS to go away and be done in the profile browsemodel !!!!
                        var NewProfileBrowseModel = new ProfileBrowseModel
                        {
                            //TO Do user a mapper instead of a contructur and map it from the service
                            //Move all this to a service
                            //ViewerProfileDetails = membermappingextentions.mapmembersearchviewmodel(null, new MemberSearchViewModel { id = model.profileid.Value }, model.allphotos, db, geodb),
                            //profile of the person being viewed
                            ProfileDetails = membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, new MemberSearchViewModel { id = model.viewingprofileid.GetValueOrDefault() }, db, geodb),
                            ProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.viewingprofileid.GetValueOrDefault(), db),
                            ViewActionsToProfile = membermappingextentions.mapmemberactionsrelationships(model.profileid.Value, model.viewingprofileid.Value, db)

                            //we dont need the viewer data since we just displya the current users profile along with thier search settings
                            // ViewerProfileDetails = membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, new MemberSearchViewModel { id = model.profileid.Value }, db, geodb),
                          //  ViewerProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.profileid.Value, db),
                           
                        };

                        //TO DO add a cache object for the profilebrowesemodel and Memberseachmodel of the currently logged in user
                        //this should probbaly be cached client side or server side no need to requery, if anything get from Cache
                        //NewProfileBrowseModel.ViewerProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.profileid.Value, db);


                        // NewProfileBrowseModel.ProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.viewingprofileid.GetValueOrDefault(), db);


                        return NewProfileBrowseModel;

                    });
                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }

            }
        }

        public async Task<FullProfileViewModel> getprofiledetails(ProfileModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {


                        //TO DO add a cache object for the profilebrowesemodel and Memberseachmodel of the currently logged in user
                        //this should probbaly be cached client side or server side no need to requery, if anything get from Cache
                        //NewProfileBrowseModel.ViewerProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.profileid.Value, db);

                        return membermappingextentions.mapfullprofileviewmodel(model.profileid.Value, model.viewingprofileid.Value, db, _geodatastoredProcedures);

                    });
                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }

            }
        }

        /// <summary>
        /// get cretieri details for a given user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProfileCriteriaModel> getprofilesearchcriteria(ProfileModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return membermappingextentions.getprofilecriteriamodel(model.profileid.Value, db);

                    });
                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }

            }
        }

        //returns a list of profile browsemodles for a given user
        public async Task<List<ProfileBrowseModel>> getprofilebrowsemodels(ProfileModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        List<ProfileBrowseModel> BrowseModels = new List<ProfileBrowseModel>();

                        foreach (var item in model.profileids)
                        {
                            var NewProfileBrowseModel = new ProfileBrowseModel
                            {
                                //TO Do user a mapper instead of a contructur and map it from the service
                                //Move all this to a service
                                ViewerProfileDetails = membermappingextentions.mapmembersearchviewmodel(null, new MemberSearchViewModel { id = model.profileid.Value }, db, geodb),
                                ProfileDetails = membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, new MemberSearchViewModel { id = Convert.ToInt32(item) }, db, geodb)

                            };

                            //add in the ProfileCritera
                            NewProfileBrowseModel.ViewerProfileCriteria = membermappingextentions.getprofilecriteriamodel(model.profileid.Value, db);
                            NewProfileBrowseModel.ProfileCriteria = membermappingextentions.getprofilecriteriamodel(Convert.ToInt32(item), db);


                            BrowseModels.Add(NewProfileBrowseModel);
                        }

                        return BrowseModels;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }


            }
        }

        //gets search settings
        //TO DO this function is just setting temp values for now
        //9 -21- 2011 added code to get age at least from search settings , more values to follow
        public async Task<MembersViewModel> getdefaultquicksearchsettingsmembers(ProfileModel Model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        quicksearchmodel quicksearchmodel = new quicksearchmodel();
                        MembersViewModel model = membermappingextentions.mapmember(Model, db, geodb);
                        // PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
                        //set deafult paging or pull from DB
                        //quicksearchmodel.myse = 4;
                        quicksearchmodel.numberperpage = 1;
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

                        quicksearchmodel.myselectedpostalcodestatus = spatialextentions.getpostalcodestatusbycountryname(new GeoModel { country = model.mycountryname }, geodb);

                        //TO do get this from search settings
                        //default for has photos only get this from the 
                        quicksearchmodel.myselectedphotostatus = true;

                        model.myquicksearch = quicksearchmodel;  //save it

                        //   Api.DisposeGeoService();

                        return model;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }


            }
        }
        //populate search settings for guests 
        public MembersViewModel getdefaultsearchsettingsguest(ProfileModel Model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    MembersViewModel model = new MembersViewModel();
                    //check if the country is in our Initial Catalog=

                    //set defualt values for guests
                    //model.myquicksearch.mySelectedPageSize = 4;
                    model.myquicksearch.numberperpage = 1;
                    model.myquicksearch.myselectedcity = "";
                    model.mypostalcodestatus = false;
                    model.myquicksearch.myselectedmaxdistancefromme = 2000;
                    model.myquicksearch.myselectedfromage = 18;
                    model.myquicksearch.myselectedtoage = 99;
                    model.myquicksearch.myselectediamgenderid = 1;
                    model.myquicksearch.myselectedstateprovince = "ALL";
                    model.myquicksearch.myselectedseekinggenderid = Extensions.GetLookingForGenderID(1);

                    if (Model.Countryname != "")
                    {

                        //PostalData2Context GeoContext = new PostalData2Context();
                        //  using (var tempdb = GeoContext)
                        //  {
                        // GeoService GeoService = new GeoService(tempdb);

                        model.myquicksearch.myselectedcountryname = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = Model.Countryid }, _geodatastoredProcedures) == "" ? "United States" : Model.Countryname; //use same country for now
                        // }
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
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
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

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            _unitOfWorkAsync.Dispose();
            try
            {
                registermodel model = new registermodel();
                //quicksearchmodel quicksearchmodel = new quicksearchmodel();
                // IEnumerable<CityStateProvince> CityStateProvince ;
                model.city = membersmodel.myquicksearch.myselectedcity;
                model.country = membersmodel.myquicksearch.myselectedcountryname;
                model.longitude = membersmodel.myquicksearch.myselectedlongitude;
                model.lattitude = membersmodel.myquicksearch.myselectedlongitude;
                model.postalcodestatus = membersmodel.myquicksearch.myselectedpostalcodestatus.GetValueOrDefault();

                // model.SecurityAnswer = "moma";
                //5/8/2011  set other defualt values here
                //model.RegistrationPhotos.PhotoStatus = "";
                // model.PostalCodeStatus = false;
                return model;

            }
            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(membersmodel.profile_id));
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

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
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
                    //PhotoUploadModel> photouploadvm = new List<PhotoUploadModel>();

                    //initlaize PhotosUploadModel object          
                    // photouploadvm.profileid = membersmodel.profile.id; //set the profileID  
                    // photouploadvm.photosuploaded = new List<PhotoUploadModel>();
                    var photobeinguploaded = new PhotoUploadModel();

                    //right now we are only uploading one photo 
                    //for now we are using URL from each, we can hanlde mutiple provider formats that might return a byte using the source paremater
                    //or the openID provider name to customize
                    if (membersmodel.rpxmodel.photo != "")
                    {   //build the photobeinguploaded object


                        //  AnewluvContext AnewluvContext = new AnewluvContext();
                        //  using (var tempdb = AnewluvContext)
                        //  {
                        //  PhotoService PhotoService = new PhotoService(tempdb);


                        var returnedTaskTResult = AsyncCalls.getimageb64stringfromurlasync(new PhotoModel { imageUrl = membersmodel.rpxmodel.photo, inmagesource = "" });
                        photobeinguploaded.imageb64string = returnedTaskTResult.Result;

                        //    }
                        photobeinguploaded.imagetypeid = db.Repository<lu_photoimagetype>().Queryable().Where(p => p.id == (int)photoimagetypeEnum.Jpeg).FirstOrDefault().id;
                        photobeinguploaded.creationdate = DateTime.Now;
                        photobeinguploaded.caption = membersmodel.rpxmodel.preferredusername;
                        //TO DO rename this to upload image from URL ?

                        //add to repository


                        // AnewluvContext = new AnewluvContext();
                        //  using (var tempdb = AnewluvContext)
                        // {
                        // PhotoService PhotoService = new PhotoService(tempdb);

                        var photouploadTaskResult = AsyncCalls.addphotosasync(new PhotoModel { singlephototoupload = photobeinguploaded });
                        var messages = photouploadTaskResult.Result;

                        //  }
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
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(membersmodel.profile_id));
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


            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            _unitOfWorkAsync.Dispose();
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
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member mapper service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }


        }


        //Not implemented
        //TOD modifiy client to not bind from this model but load values asycnh
        //other member viewmodl methods
        //TO DO put in cache
        public MembersViewModel updatememberdata(MembersViewModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    //return CachingFactory.MembersViewModelHelper.updatememberdata(model, this);
                    //remap the user data if cache is empty
                    //var mm = new ViewModelMapper();
                    //return this.mapmember(new ProfileModel { profileid = model.profile_id },db);
                    return null;


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }

        public async Task<MembersViewModel> updatememberdatabyprofileid(ProfileModel newmodel)
        {
            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    //   return CachingFactory.MembersViewModelHelper.updatememberprofiledatabyprofile(profileid, this);
                    var task = Task.Factory.StartNew(() =>
                    {

                        var model = membermappingextentions.mapmember(newmodel, db, geodb);
                        model.profiledata = model.profile.profiledata;
                        return model;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(newmodel.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
                finally
                {

                }

            }

        }

        public bool updateguestdata(MembersViewModel model)
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
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
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profile_id));
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

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    // return CachingFactory.MembersViewModelHelper.removeguestdata(sessionid);
                    return true;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
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

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    // return CachingFactory.MembersViewModelHelper.getguestdata(sessionid, this);
                    return this.mapguest();

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }


        //Not implemented yet
        public MembersViewModel getmemberdata(ProfileModel newmodel)
        {


            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    //    return CachingFactory.MembersViewModelHelper.getmemberdata(profileid, this);

                    //  return this.mapmember(newmodel,db);
                    return null;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, newmodel.profileid);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member mapper service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        public MembersViewModel mapguest()
        {

            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            _unitOfWorkAsync.Dispose();

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
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member mapper service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }




        }

        // TO DO use the same filtering done by the prmotion objects search  service where the filter is done during the first search
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public async Task<SearchResultsViewModel> getquickmatches(ProfileModel Model)
        {


            //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;            
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        //get search sttings from DB
                        //TO DO change this to not use API call
                        //searchsetting perfectmatchsearchsettings = null;

                        MembersViewModel model = membermappingextentions.mapmember(Model, db, geodb);
                        searchsetting perfectmatchsearchsettings = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = model.profile_id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);

                        //TO do handle empty perfect match settings here
                        if (perfectmatchsearchsettings == null)
                        {
                            perfectmatchsearchsettings = profileextentionmethods.createsearchbyprofileid("MyPerfectMatch", true, new ProfileModel { profileid = Model.profileid }, db);
                        }
                        else
                        {
                            int searchid = model.profile.profilemetadata.searchsettings.FirstOrDefault().id;
                            perfectmatchsearchsettings = db.Repository<searchsetting>().getsearchsettingsbysearchid(searchid);
                        }


                        //set default perfect match distance as 100 for now later as we get more members lower
                        //TO DO move this to a db setting or resourcer file
                        int maxdistancefromme = (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0) ? 1000 : perfectmatchsearchsettings.distancefromme.GetValueOrDefault();


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
                        double? myLattitude = (model.mylongitude != "") ? Convert.ToDouble(model.mylatitude) : 0;
                        //get the rest of the values if they are needed in calculations


                        //set variables
                        // List<MemberSearchViewModel> MemberSearchViewmodels;
                        DateTime today = DateTime.Today;
                        DateTime max = today.AddYears(-(intAgeFrom + 1));
                        DateTime min = today.AddYears(-intAgeTo);


                        //TO DO Move this to a function so its cleaner and re-usable
                        //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                        //requery all the has tbls
                        HashSet<int> LookingForGenderValues = new HashSet<int>();
                        LookingForGenderValues = (perfectmatchsearchsettings != null && perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Count() > 0) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Select(c => c.value))
                        : model.defaultlookingforgenders;
                        //Appearacnce seache settings values         

                        //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                        HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                        LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype).Select(c => c.value)) : LookingForBodyTypesValues;

                        HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                        LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Select(c => c.value)) : LookingForEthnicityValues;

                        HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                        LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Select(c => c.value)) : LookingForEyeColorValues;

                        HashSet<int> LookingForHairColorValues = new HashSet<int>();
                        LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Select(c => c.value)) : LookingForHairColorValues;

                        HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                        LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Select(c => c.value)) : LookingForHotFeatureValues;


                        //******** visiblitysettings test code ************************

                        // test all the values you are pulling here
                        // var TestModel =   (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case")
                        //                      select x).FirstOrDefault();
                        //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1));
                        // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profile.profiledata.GenderID) ? true : false;

                        //  var testmodel2 = (from x in _datingcontext.profiledata.Where(x => x.profile.username  == "case" &&  db.fnCheckIfBirthDateIsInRange(x.birthdate, 19, 20) == true  )
                        //                     select x).FirstOrDefault();


                        //TO DO add code to filter out blocked members
                        var otherblocks = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profile_id, (int)actiontypeEnum.Block);
                        //mailboxmessagefolderlist = (from m in mailboxmessagefolderlist.Where(a => a.mailboxmessage.sender_id == model.profileid.Value)
                        //                           where (!otherblocks.Any(f => f.target_profile_id != m.mailboxmessage.sender_id))
                        //                           select m).AsQueryable();     

                        //****** end of visiblity test settings *****************************************


                        var sourcePoint = spatialextentions.CreatePoint(myLattitude.Value, myLongitude.Value);
                        // find any locations within 5 miles ordered by distance
                        //first convert miles value to meters
                        var MaxdistanceInMiles = spatialextentions.MilesToMeters(maxdistancefromme);

                     

                        var MemberSearchViewmodels = (from x in db.Repository<profiledata>().Queryable().Where(p => p.birthdate > min && p.birthdate <= max &&
                              p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)
                              && LookingForGenderValues.Contains((int)p.gender_id)
                              ).ToList()

                                            //** visiblity settings still needs testing           
                                                          //5-8-2012 add profile visiblity code here
                                                          // .Where(x => x.profile.username == "case")
                                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.ProfileVisiblity == true)
                                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate > today.AddYears(-(x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1)))
                                                          //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profile.profiledata.birthdate < today.AddYears(-x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault()))
                                                          // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.countryid != null && x.ProfileVisiblitySetting.countryid == model.profile.profiledata.countryid  )
                                                          // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.GenderID != null && x.ProfileVisiblitySetting.GenderID ==  model.profile.profiledata.GenderID )
                                                          //** end of visiblity settings ***
                                                          //using whereIF predicate function 
                                                          // .WhereIf(LookingForGenderValues.Count == 0, z => model.lookingforgendersid.Contains(z.gender_id.GetValueOrDefault())).ToList() //  == model.lookingforgenderid)    
                                                          //TO DO add the rest of the filitering here 
                                                          //Appearance filtering                         
                                         .WhereIf(blEvaluateHeights, z => z.height > intheightmin && z.height <= intheightmax).ToList() //Only evealuate if the user searching actually has height values they look for                         
                                                      join f in db.Repository<profile>().Queryable() on x.profile_id equals f.id
                                                      select new MemberSearchViewModel
                                                      {
                                                          
                                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                          id = x.profile_id,
                                                          stateprovince = x.stateprovince,
                                                          city = x.city,
                                                          postalcode = x.postalcode,
                                                          countryid = x.countryid,
                                                          genderid = x.gender_id,
                                                          birthdate = x.birthdate,
                                                          //profile = f,
                                                          screenname = f.screenname,
                                                          longitude = x.longitude ?? 0,
                                                          latitude = x.latitude ?? 0,
                                                          creationdate = f.creationdate,
                                                          // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                          lastlogindate = f.logindate,
                                                          distancefromme = x.location.Distance(sourcePoint)

                                                      }).OrderBy(p => p.distancefromme).ThenByDescending(p => p.creationdate);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();






                        //11/20/2011 handle case where  no profiles were found
                        if (MemberSearchViewmodels.Count() == 0)
                        {
                            var dd = getquickmatcheswhenquickmatchesempty(model, perfectmatchsearchsettings, Model.page, Model.numberperpage, db, geodb); //.Take(maxemailmatches).ToList();
                            dd.results.Take(maxemailmatches);
                            return dd;
                        }


                        //filter our the ones in the right distance and reutnr the top webmacthes
                        var FilteredMemberSearchviewmodels = (maxdistancefromme > 0 && MemberSearchViewmodels.Where(d => d.distancefromme.GetValueOrDefault() <= maxdistancefromme).Count() > 8) ? (from q in MemberSearchViewmodels
                              .Where(a => a.distancefromme.GetValueOrDefault() <= maxdistancefromme)
                                                                                                                                                                                                    select q).Take(maxwebmatches)
                                                                    : MemberSearchViewmodels.Take(maxwebmatches);

                        return GenerateSearchSearchResults(FilteredMemberSearchviewmodels, Model.page, Model.numberperpage, db);

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
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
        public async Task<SearchResultsViewModel> getemailmatches(ProfileModel Model)
        {

            //            _unitOfWorkAsync.DisableProxyCreation = false;
            var geodb = _geodatastoredProcedures;
            var db = _unitOfWorkAsync;
            {
                try
                {


                    var task = Task.Factory.StartNew(() =>
                    {


                        profile profile = new profile();
                        // profile = db.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = (Model.profileid) });

                        MembersViewModel model = membermappingextentions.mapmember(new ProfileModel { profileid = profile.id }, db, geodb);


                        // model.profile = profile;
                        //get search sttings from DB
                        searchsetting perfectmatchsearchsettings =
                       _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = model.profile_id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);


                        //TO do handle empty perfect match settings here
                        if (perfectmatchsearchsettings == null)
                        {
                            perfectmatchsearchsettings = profileextentionmethods.createsearchbyprofileid("MyPerfectMatch", true, new ProfileModel { profileid = Model.profileid }, db);
                        }

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
                        double? myLattitude = (model.mylatitude != "") ? Convert.ToDouble(model.mylatitude) : 0;


                        //set variables
                        //  List<MemberSearchViewModel> MemberSearchViewmodels;
                        DateTime today = DateTime.Today;
                        DateTime max = today.AddYears(-(intAgeFrom + 1));
                        DateTime min = today.AddYears(-intAgeTo);


                        //TO DO this needs to be in a function to build all these
                        HashSet<int> LookingForGenderValues = new HashSet<int>();
                        LookingForGenderValues = (perfectmatchsearchsettings != null && perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Count() > 0) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Select(c => c.value))
                        : model.defaultlookingforgenders;
                        //Appearacnce seache settings values         

                        //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                        HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                        LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype).Select(c => c.value)) : LookingForBodyTypesValues;

                        HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                        LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Select(c => c.value)) : LookingForEthnicityValues;

                        HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                        LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Select(c => c.value)) : LookingForEyeColorValues;

                        HashSet<int> LookingForHairColorValues = new HashSet<int>();
                        LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Select(c => c.value)) : LookingForHairColorValues;

                        HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                        LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Select(c => c.value)) : LookingForHotFeatureValues;


                        // var photostest = _datingcontext.profiles.Where(p => (p.profilemetadata.photos.Any(z => z.photostatus != null && z.photostatus.id != (int)photostatusEnum.Gallery)));

                        //TO DO add code to filter out blocked members
                        var otherblocks = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profile_id, (int)actiontypeEnum.Block);
                        //mailboxmessagefolderlist = (from m in mailboxmessagefolderlist.Where(a => a.mailboxmessage.sender_id == model.profileid.Value)
                        //                           where (!otherblocks.Any(f => f.target_profile_id != m.mailboxmessage.sender_id))
                        //                           select m).AsQueryable();   




                        var sourcePoint = spatialextentions.CreatePoint(myLattitude.Value, myLongitude.Value);
                        // find any locations within 5 miles ordered by distance
                        //first convert miles value to meters
                        var MaxdistanceInMiles = spatialextentions.MilesToMeters(model.maxdistancefromme);

                     

                        //basic search
                        var repo = db.Repository<profiledata>().Query(p => p.birthdate > min && p.birthdate <= max &&
                            p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)).Select();

                        var MemberSearchViewmodels = (from x in repo
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender_id.GetValueOrDefault())) //using whereIF predicate function                                         
                                                          //if there are no genders selected in seach settigs since this is required handle the other case.. also handle the issue where no mapping was done
                                                          //to create a default list of looking for geners and just use the opposite of the user searching i.e mapped user
                            .WhereIf(LookingForGenderValues.Count == 0, z =>
                                 (model.defaultlookingforgenders.Count > 0 && model.defaultlookingforgenders.Contains(z.gender_id.GetValueOrDefault())
                                                                     || z.gender_id != model.mygenderid))

                                        .WhereIf(blEvaluateHeights, z => z.height > intheightmin && z.height <= intheightmax).ToList() //Only evealuate if the user searching actually has height values they look for 
                                                      //we have to filter on the back end now since we cant use UDFs
                                                      // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                                                      join f in db.Repository<profile>().Queryable() on x.profile_id equals f.id
                                                      select new MemberSearchViewModel
                                                      {
                                                          
                                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                          id = x.profile_id,
                                                          stateprovince = x.stateprovince,
                                                          city = x.city,
                                                          postalcode = x.postalcode,
                                                          countryid = x.countryid,
                                                          genderid = x.gender_id,
                                                          birthdate = x.birthdate,
                                                          //profile = f,
                                                          screenname = f.screenname,
                                                          longitude = x.longitude ?? 0,
                                                          latitude = x.latitude ?? 0,
                                                          creationdate = f.creationdate,
                                                          // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                          lastlogindate = f.logindate,
                                                          distancefromme = x.location.Distance(sourcePoint), 
                                                          //TO DO look at this and explore
                                                          //distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                          //lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",


                                                      }).OrderBy(p => p.distancefromme).ThenByDescending(p => p.creationdate);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();





                        //11/20/2011 handle case where  no profiles were found
                        if (MemberSearchViewmodels.Count() == 0)
                        {
                            var dd = getquickmatcheswhenquickmatchesempty(model, perfectmatchsearchsettings, Model.page, Model.numberperpage, db, geodb); //.Take(maxemailmatches).ToList();
                            dd.results.Take(maxemailmatches);
                            return dd;
                        }

                        //filter our the ones in the right distance and reutnr the top webmacthes
                        //USes max search results snce this could be called by any other method with a variable set of return macthes or results
                        var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                            .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                                                                        select q).Take(maxemailmatches)
                                                                    : MemberSearchViewmodels.Take(maxemailmatches);


                        return GenerateSearchSearchResults(MemberSearchViewmodels, Model.page, Model.numberperpage, db);


                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(Model.profileid));
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
        public async Task<SearchResultsViewModel> getquicksearch(quicksearchmodel Model)
        {


            var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
            //            _unitOfWorkAsync.DisableProxyCreation = false;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        // SearchResultsViewModel searchresults = new SearchResultsViewModel();
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
                        string city = Model.myselectedcity;
                        int photostatus = (Model.myselectedphotostatus != null) ? (int)photostatusEnum.Gallery : (int)photostatusEnum.Nostatus;
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



                        var sourcePoint = spatialextentions.CreatePoint(myLattitude.Value, myLongitude.Value);
                        // find any locations within 5 miles ordered by distance
                        //first convert miles value to meters
                        var MaxdistanceInMiles = spatialextentions.MilesToMeters(maxdistancefromme);


                     

                        //TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                        var MemberSearchViewmodels = (from x in db.Repository<profiledata>().Queryable().Where(p => p.birthdate > min && p.birthdate <= max &&
                         p.countryid == countryid && p.city == city && p.stateprovince == stateprovince)
                                        .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.lu_gender.id)).ToList() //using whereIF predicate function  

                                                      //Appearance filtering not implemented yet                        
                                                      //Only evealuate if the user searching actually has height values they look for 
                                                      //we have to filter on the back end now since we cant use UDFs
                                                      // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                                                      join f in db.Repository<profile>().Queryable() on x.profile_id equals f.id
                                                      select new MemberSearchViewModel
                                                      {
                                                          
                                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                                          id = x.profile_id,
                                                          stateprovince = x.stateprovince,
                                                          city = x.city,
                                                          postalcode = x.postalcode,
                                                          countryid = x.countryid,
                                                          genderid = x.gender_id,
                                                          birthdate = x.birthdate,
                                                          //profile = f,
                                                          screenname = f.screenname,
                                                          longitude = x.longitude ?? 0,
                                                          latitude = x.latitude ?? 0,
                                                          creationdate = f.creationdate,
                                                          // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                                                          lastlogindate = f.logindate,
                                                          distancefromme = x.location.Distance(sourcePoint), 
                                                          //TO DO look at this and explore
                                                          //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                                                          //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                                                          //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",
                                                      }).OrderByDescending(p => p.hasgalleryphoto == true).OrderBy(p => p.distancefromme).ThenByDescending(p => p.creationdate).ToList();//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();




                        //if we find no records in the city and state location, we want to use the zip postal code to check using logitude and lattitude of that zip postcal and do a range search using distance
                         



                       


                        activitylist.Add(Api.AnewLuvLogging.CreateActivity(Model.profileid,null, (int)activitytypeEnum.quicksearch, ctx));

                        if (activitylist.Count() > 0) Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();

                        return GenerateSearchSearchResults(MemberSearchViewmodels, Model.page, Model.numberperpage, db);




                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
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


        //search functions that should be moved to thier own service when time allows

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public async Task<SearchResultsViewModel> getipquicksearch(quicksearchmodel Model)
        {


           // var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
            //            _unitOfWorkAsync.DisableProxyCreation = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    double currentdistance = Model.myselectedmaxdistancefromme ?? 200 ; 
                    var matches = filtermatches(Model); 
                    
                    //get the number of values for male and female
                    var divisor = Model.numberperpage / 2;
                    var modulo = Model.numberperpage % 2;



                    //check gender counts if they are good continue otherwise reun the query 
                    while ((matches.Where(z=>z.genderid==(int)genderEnum.Male).Count())  < divisor && (matches.Where(z=>z.genderid==(int)genderEnum.Female).Count() < (divisor + modulo))) 
                    {
                        Model.myselectedmaxdistancefromme = currentdistance + 500;  //TO DO tune this down from 500 to 50 mile increments 
                        matches = filtermatches(Model); 
                    }



                    var matchesarray = matches.ToArray();
                    var matcheslist = matches.ToList();
                    MemberSearchViewModel[] orderedmathes = new MemberSearchViewModel[matches.Count()];                 
                    
                    int index = 0;                   
                    int activegender = 0;
                    //foreach (MemberSearchViewModel dd in matches)
                    
                    //initlize                 
                    orderedmathes[index] = matcheslist.First();

                    matcheslist.Remove(orderedmathes[index]); //remove it from source list
                    //save the current gender
                    activegender = orderedmathes[index].genderid.GetValueOrDefault();

                     while (index < Model.numberperpage)
                     {
                        //increment indexe for list we are building
                        index = index + 1;                        
                     

                         //find the first item of opposite gender next
                        var firstMatch = matcheslist.First(s => s.genderid != activegender);
                        orderedmathes[index] = firstMatch;
                        matcheslist.Remove(firstMatch);

                        activegender = orderedmathes[index].genderid.Value;               
                        
                    }




                     return GenerateSearchSearchResults(orderedmathes, Model.page, Model.numberperpage, db);






                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
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

        private IEnumerable<MemberSearchViewModel> filtermatches(quicksearchmodel Model)
        {

            // SearchResultsViewModel searchresults = new SearchResultsViewModel();
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
            string city = Model.myselectedcity;
            int photostatus = (Model.myselectedphotostatus != null) ? (int)photostatusEnum.Gallery : (int)photostatusEnum.Nostatus;
            string stateprovince = Model.myselectedstateprovince;
            double? maxdistancefromme = Model.myselectedmaxdistancefromme ?? 500;



            //skip these
            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues.Add(genderid);  //add the gender id being searched for



            var sourcePoint = spatialextentions.CreatePoint(myLattitude.Value, myLongitude.Value);
            // find any locations within 5 miles ordered by distance
            //first convert miles value to meters
            var MaxdistanceInMiles = spatialextentions.MilesToMeters(maxdistancefromme);

         

            //TO DO needs filter for the profile ranking as well , and photo quality etc
            var matches = _unitOfWorkAsync.Repository<profiledata>()
                         .Query(z => z.profile.profilemetadata.photos.Any(m => m.photostatus_id == (int)photostatusEnum.Gallery
                          && z.location.Distance(sourcePoint) <= MaxdistanceInMiles)).Include(z => z.profile).Select()
                         .OrderBy(y => y.location.Distance(sourcePoint))
                        .Select
                        (x => new MemberSearchViewModel
                        {
                            //populate values server side to be used later                                                        
                            
                            id = x.profile_id,
                            creationdate = x.profile.creationdate,
                            profile = x.profile,
                            distancefromme = x.location.Distance(sourcePoint),
                            stateprovince = x.stateprovince,
                            city = x.city,
                            postalcode = x.postalcode,
                            countryid = x.countryid,
                            genderid = x.gender_id,
                            birthdate = x.birthdate,

                            screenname = x.profile.screenname,
                            longitude = x.longitude ?? 0,
                            latitude = x.latitude ?? 0,
                            lastlogindate = x.profile.logindate

                        }).Take(100);


            return matches;

        }

      
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public async Task<SearchResultsViewModel> getadvancedsearch(AdvancedSearchModel model)
        {
            var dd = new SearchResultsViewModel();
            //            _unitOfWorkAsync.DisableProxyCreation = false;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        return dd;

                        //// SearchResultsViewModel searchresults = new SearchResultsViewModel();
                        ////get the  gender's from search settings
                        //int genderid = Model.myselectedseekinggenderid.GetValueOrDefault();
                        //int mygenderid = Model.myselectediamgenderid.GetValueOrDefault();


                        //// int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
                        //int AgeTo = Model.myselectedtoage != null ? Model.myselectedtoage.GetValueOrDefault() : 99;
                        //int AgeFrom = Model.myselectedfromage != null ? Model.myselectedfromage.GetValueOrDefault() : 18;
                        ////Height
                        ////int intheightmin = Model.h != null ? Model.heightmin.GetValueOrDefault() : 0;
                        //// int intheightmax = Model.heightmax != null ? Model.heightmax.GetValueOrDefault() : 100;
                        ////  bool blEvaluateHeights = intheightmin > 0 ? true : false;
                        ////get the rest of the values if they are needed in calculations
                        ////convert lattitudes from string (needed for JSON) to bool           
                        //double? myLongitude = (Model.myselectedlongitude != null) ? Convert.ToDouble(Model.myselectedlongitude) : 0;
                        //double? myLattitude = (Model.myselectedlatitude != null) ? Convert.ToDouble(Model.myselectedlatitude) : 0;


                        ////set variables
                        ////  List<MemberSearchViewModel> MemberSearchViewmodels;
                        //DateTime today = DateTime.Today;
                        //DateTime max = today.AddYears(-(AgeFrom + 1));
                        //DateTime min = today.AddYears(-AgeTo);

                        ////get country and city data
                        //string countryname = Model.myselectedcountryname;
                        //int countryid = Model.myselectedcountryid.GetValueOrDefault();
                        //// myselectedcountryid 
                        //string stringpostalcode = Model.myselectedpostalcode;

                        ////added 10/17/20011 so we can toggle postalcode box similar to register 
                        //string city = Model.myselectedcity;
                        //int photostatus = (Model.myselectedphotostatus != null) ? (int)photostatusEnum.Gallery : (int)photostatusEnum.Nostatus;
                        //string stateprovince = Model.myselectedstateprovince;
                        //double? maxdistancefromme = Model.myselectedmaxdistancefromme;



                        ////skip these
                        ////get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
                        ////requery all the has tbls
                        //HashSet<int> LookingForGenderValues = new HashSet<int>();
                        //LookingForGenderValues.Add(genderid);  //add the gender id being searched for


                        //////Appearacnce seache settings values         

                        //////set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

                        ////HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
                        ////LookingForBodyTypesValues = (Model != null) ? new HashSet<int>(Model.searchsetting_bodytype.Select(c => c.id)) : LookingForBodyTypesValues;

                        ////HashSet<int> LookingForEthnicityValues = new HashSet<int>();
                        ////LookingForEthnicityValues = (Model != null) ? new HashSet<int>(Model.searchsetting_ethnicity.Select(c => c.id)) : LookingForEthnicityValues;

                        ////HashSet<int> LookingForEyeColorValues = new HashSet<int>();
                        ////LookingForEyeColorValues = (Model != null) ? new HashSet<int>(Model.searchsetting_eyecolor.Select(c => c.id)) : LookingForEyeColorValues;

                        ////HashSet<int> LookingForHairColorValues = new HashSet<int>();
                        ////LookingForHairColorValues = (Model != null) ? new HashSet<int>(Model.searchsetting_haircolor.Select(c => c.id)) : LookingForHairColorValues;

                        ////HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
                        ////LookingForHotFeatureValues = (Model != null) ? new HashSet<int>(Model.searchsetting_hotfeature.Select(c => c.id)) : LookingForHotFeatureValues;

                        //// var photostest = _datingcontext.profiles.Where(p => (p.profilemetadata.photos.Any(z => z.photostatus != null && z.photostatus.id != (int)photostatusEnum.Gallery)));


                        //TO DO add code to filter out blocked members
                        var otherblocks = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
                        //mailboxmessagefolderlist = (from m in mailboxmessagefolderlist.Where(a => a.mailboxmessage.sender_id == model.profileid.Value)
                        //                           where (!otherblocks.Any(f => f.target_profile_id != m.mailboxmessage.sender_id))
                        //                           select m).AsQueryable();   

                        ////add more values as we get more members 
                        ////TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                        //var MemberSearchViewmodels = (from x in db.Repository<profiledata>().Queryable().Where(p => p.birthdate > min && p.birthdate <= max &&
                        // p.countryid == countryid && p.city == city && p.stateprovince == stateprovince)
                        //                .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.lu_gender.id)).ToList() //using whereIF predicate function  

                        //                              //Appearance filtering not implemented yet                        
                        //                              //Only evealuate if the user searching actually has height values they look for 
                        //                              //we have to filter on the back end now since we cant use UDFs
                        //                              // .WhereIf(model.maxdistancefromme  > 0, a => _datingcontext.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.maxdistancefromme)
                        //                              join f in db.Repository<profile>().Queryable() on x.profile_id equals f.id
                        //                              select new MemberSearchViewModel
                        //                              {
                        //                                  // MyCatchyIntroLineQuickSearch = x.AboutMe,
                        //                                  id = x.profile_id,
                        //                                  stateprovince = x.stateprovince,
                        //                                  city = x.city,
                        //                                  postalcode = x.postalcode,
                        //                                  countryid = x.countryid,
                        //                                  genderid = x.gender_id,
                        //                                  birthdate = x.birthdate,
                        //                                  //profile = f,
                        //                                  screenname = f.screenname,
                        //                                  longitude = x.longitude ?? 0,
                        //                                  latitude = x.latitude ?? 0,
                        //                                  creationdate = f.creationdate,
                        //                                  // city = db.fnTruncateString(x.city, 11),
                        //                                  // lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.logindate),
                        //                                  lastlogindate = f.logindate,
                        //                                  distancefromme = spatialextentions.getdistancebetweenmembers((double)x.latitude, (double)x.longitude, myLattitude.Value, myLongitude.Value, "Miles")
                        //                                  //TO DO look at this and explore
                        //                                  //  distancefromme = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")
                        //                                  //       lookingforagefrom = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemin.ToString() : "25",
                        //                                  //lookingForageto = x.profile.profilemetadata.searchsettings != null ? x.profile.profilemetadata.searchsettings.FirstOrDefault().agemax.ToString() : "45",
                        //                              }).OrderByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme).ToList();//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();



                        ////this.AddRange(pageData.ToList());
                        //// var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();


                        ////Come back to these filiters later
                        ////11/20/2011 handle case where  no profiles were found
                        ////if (MemberSearchViewmodels.Count() == 0)
                        //// return null; //getquickmatcheswhenquickmatchesempty(new ProfileModel { profileid = Model.profileid }).Take(maxemailmatches).ToList();

                        ////filter our the ones in the right distance and reutnr the top webmacthes
                        ////USes max search results snce this could be called by any other method with a variable set of return macthes or results
                        //// var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                        ////    .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                        ////                                                select q).Take(maxemailmatches)
                        ////                                            :
                        ////     MemberSearchViewmodels.Take(maxemailmatches);


                        ////               var page = query.OrderBy(p => p.Name)
                        ////   .Select(p => new PersonResult { Name = p.Name })
                        ////   .Skip(skipRows).Take(pageSize)
                        ////   .GroupBy(p => new { Total = query.Count() })
                        ////  .First();

                        ////do paging here after last filtering
                        //// int? totalrecordcount = MemberSearchViewmodels.Count;
                        ////handle zero and null paging values
                        //return GenerateSearchSearchResults(MemberSearchViewmodels, Model.page, Model.numberperpage, db);


                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
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
        internal SearchResultsViewModel getquickmatcheswhenquickmatchesempty(MembersViewModel model, searchsetting perfectmatchsearchsettings, int? page, int? numberperpage, IUnitOfWorkAsync db, IGeoDataStoredProcedures geodb)
        {


            //            _unitOfWorkAsync.DisableProxyCreation = false;
            try
            {

                //TO DO change to use unit of work in here
                //get search sttings from DB

                profile profile = model.profile;
                //  profile = db.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = (profilemodel.profileid) });
                // MembersViewModel model = mapmember(profilemodel.profileid.ToString());

                // MembersViewModel model = membermappingextentions.mapmember(profilemodel,db,geodb);

                //get search sttings from DB

                //set default perfect match distance as 100 for now later as we get more members lower
                //TO DO move this to a _datingcontext setting or resourcer file

                if (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0)
                    model.maxdistancefromme = 2000;

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
                LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.details.Where(p => p.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Select(c => c.value)) : LookingForGenderValues;




                //basic search
                var repo = db.Repository<profiledata>().Query(p => p.birthdate > min && p.birthdate <= max &&
                    p.profile.profilemetadata.photos.Any(z => z.photostatus_id == (int)photostatusEnum.Gallery)).Select();

                var sourcePoint = spatialextentions.CreatePoint(myLattitude.Value, myLongitude.Value);
                // find any locations within 5 miles ordered by distance
                //first convert miles value to meters
                var MaxdistanceInMiles = spatialextentions.MilesToMeters(model.maxdistancefromme);

              

                //  where (LookingForGenderValues.Count !=null || LookingForGenderValues.Contains(x.GenderID)) 
                //  where (LookingForGenderValues.Count == null || x.GenderID == UserProfile.MyQuickSearch.MySelectedSeekingGenderID )   //this should not run if we have no gender in searchsettings
                //add more values as we get more members 
                //TO DO change the photostatus thing to where if maybe, based on HAS PHOTOS only matches
                var MemberSearchViewmodels = (from x in repo
                                .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender_id.GetValueOrDefault())) //using whereIF predicate function                                         
                                                  //if there are no genders selected in seach settigs since this is required handle the other case.. also handle the issue where no mapping was done
                                                  //to create a default list of looking for geners and just use the opposite of the user searching i.e mapped user
                                .WhereIf(LookingForGenderValues.Count == 0, z =>
                                     (model.defaultlookingforgenders.Count > 0 && model.defaultlookingforgenders.Contains(z.gender_id.GetValueOrDefault())
                                                                         || z.gender_id != model.mygenderid))


                                              join f in db.Repository<profile>().Queryable() on x.profile_id equals f.id
                                              select new MemberSearchViewModel
                                              {
                                                
                                                  city = x.city,
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
                                                  lastlogindate = f.logindate,
                                                  distancefromme = x.location.Distance(sourcePoint), 


                                              }).OrderBy(p => p.distancefromme).ThenByDescending(p => p.creationdate);//.OrderBy(p=>p.creationdate ).Take(maxwebmatches).ToList();


                //filter our the ones in the right distance and reutnr the top webmacthes
                var profiles = (model.maxdistancefromme > 0) ? (from q in MemberSearchViewmodels
                    .Where(a => a.distancefromme.GetValueOrDefault() <= model.maxdistancefromme)
                                                                select q).Take(maxwebmatches)
                                                            : MemberSearchViewmodels.Take(maxwebmatches);

                return GenerateSearchSearchResults(profiles, page, numberperpage, db);




                //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
                //final ordering 
                // profiles = profiles.OrderByDescending(p => p.hasgalleryphoto == true).ThenByDescending(p => p.creationdate)


                //  return profiles.ToList();
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profile.id));
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

        internal SearchResultsViewModel GenerateSearchSearchResults(IEnumerable<MemberSearchViewModel> source, int? page, int? numberperpage, IUnitOfWorkAsync db)
        {

            try
            {

               

                //create the ordered list for use on client for paging withoute th eentire model passed down  
                var counter =0;
                
                //ADDED  code to skip the nulls if any from a search
                var dd = source.Where(item => item != null).                
                Select(z=> new SearchResult { id=z.id, searchindex = counter++, selected = false}).ToList();


                // int? totalrecordcount = MemberSearchViewmodels.Count;
                //handle zero and null paging values
                if (page == null || page == 0) page = 1;
                if (numberperpage == null || numberperpage == 0) numberperpage = 4;

                bool allowpaging = (source.Count() >  numberperpage ? true : false);
                var pageData = page >= 1 & allowpaging ?
                    new PaginatedList<MemberSearchViewModel>().GetCurrentPages(source.ToList(), page ?? 1, numberperpage ?? 20) : source.Take(numberperpage.GetValueOrDefault());


                //do any conversions and calcs here
                var test = pageData.Select(x => new MemberSearchViewModel
                {
                    //resultsindex = x.resultsindex,
                    MyCatchyIntroLineQuickSearch = x.aboutme,
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

                    hasgalleryphoto = db.Repository<photo>().Queryable().Where(i => i.profile_id == x.id && i.photostatus_id == (int)photostatusEnum.Gallery).FirstOrDefault() != null ? true : false,
                    creationdate = x.creationdate,
                    city = Extensions.Chop(x.city, 11),
                    lastloggedonstring = profileextentionmethods.getlastloggedinstring(x.lastlogindate.GetValueOrDefault()),
                    lastlogindate =  x.lastlogindate,
                    distancefromme = spatialextentions.MetersToMiles(x.distancefromme), //TO DO toggle based on country of viewwe i.e globalize
                    galleryphoto = db.Repository<photoconversion>().getgalleryphotomodelbyprofileid(x.id, (int)photoformatEnum.Medium),
                    lookingforagefrom = x.lookingforagefrom,
                    lookingForageto = x.lookingForageto,
                    online = db.Repository<profile>().getuseronlinestatus(new ProfileModel { profileid = x.id })


                }).ToList();


                return new SearchResultsViewModel { results = test, totalresults = source.Count() , orderedresultids = dd};

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}


//backup of old code

///// <summary>
///// asumes modeltomap and profile id is populated
///// </summary>
///// <param name="model"></param>
///// <returns></returns>
//private MemberSearchViewModel getmembersearchviewmodel(ProfileModel model, IUnitOfWorkAsync db)
//{

//  //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;

//    try
//    {
//        if (model.profileid.Value != null)
//        {


//            // List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
//            MemberSearchViewModel modeltomap = new MemberSearchViewModel();
//            // modeltomap.id = Convert.ToInt32(profileid);
//            return mapmembersearchviewmodel(model, db);


//        }
//        return null;

//    }
//    catch (Exception ex)
//    {

//        //instantiate logger here so it does not break anything else.
//        logger = new Logging(applicationEnum.MemberService);
//        //int profileid = Convert.ToInt32(viewerprofileid);
//        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(viewerprofileid));
//        //can parse the error to build a more custom error mssage and populate fualt faultreason
//        FaultReason faultreason = new FaultReason("Error in member mapper service");
//        string ErrorMessage = "";
//        string ErrorDetail = "ErrorMessage: " + ex.Message;
//        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

//        //throw convertedexcption;
//    }


//}     




////public method that exposes mapper
///// <summary>
///// 
//      /// <summary>
//        ///  same as above but maps a single profile to a list of other profiles in the model.modelstomap variable
//        /// </summary>
//        /// <param name="model"></param>
//        /// <param name="allphotos"></param>
//        /// <returns></returns>
//        public async Task<List<MemberSearchViewModel>> mapmembersearchviewmodels(ProfileModel model)
//        {

//          //  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//            var geodb = _geodatastoredProcedures;
//           var db = _unitOfWorkAsync;
//            {
//                try
//                {

//                      var task = Task.Factory.StartNew(() =>
//                    {

//                       List<MemberSearchViewModel> models = new List<MemberSearchViewModel>();
//                    ProfileModel tempmodel = model; //temp storage so we can modif it for use in iteration
//                    foreach (var item in model.modelstomap)
//                    {
//                        //set current model for mapping
//                        tempmodel.modeltomap = item;
//                        models.Add(membermappingextentions.mapmembersearchviewmodel(model.profileid.Value, item.id, model.allphotos, db, geodb));

//                    }
//                    return models;

//                    });
//                    return await task.ConfigureAwait(false);


//                }
//                catch (Exception ex)
//                {

//                    //instantiate logger here so it does not break anything else.
//                    logger = new Logging(applicationEnum.MemberService);
//                    //int profileid = Convert.ToInt32(viewerprofileid);
//                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
//                    //can parse the error to build a more custom error mssage and populate fualt faultreason
//                    FaultReason faultreason = new FaultReason("Error in member mapper service");
//                    string ErrorMessage = "";
//                    string ErrorDetail = "ErrorMessage: " + ex.Message;
//                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

//                    //throw convertedexcption;
//                }
//                finally
//                {
//                   
//                }

//            }

//        }

