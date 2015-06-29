using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;



using System.Web;
using System.Net;


using System.ServiceModel.Activation;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data;
using Anewluv.Services.Contracts;
//using Nmedia.DataAccess.Interfaces;
using System.Threading.Tasks;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.Domain.Data.log;
using Anewluv.Caching;
using Nmedia.Infrastructure.DTOs;
using Repository.Pattern.UnitOfWork;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.DependencyInjection;
using Anewluv.Caching.RedisCaching;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Nmedia.Infrastructure.Domain.Data.Apikey;


namespace Anewluv.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "searchsService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SearchEditService : ISearchEditService 
    {



        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
       // private Logging logger;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _searchsettingsactionsrepository;
        // private string _apikey;

        public SearchEditService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork)
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
            //disable proxy stuff by default
            //_unitOfWorkAsync.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

        #region "search getter methods"
        public async Task<searchsetting> getsearchsettings(SearchSettingsModel model)
        {
           
         
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid && z.searchname == model.searchname).FirstOrDefault();
                        return p;
                    });
                    return await task.ConfigureAwait(false);

                }              

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<List<searchsetting>> getallsearchsettingsbyprofileid(SearchSettingsModel searchmodel)
        {
           
         
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        var p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.profile_id == searchmodel.profileid).ToList();
                        return p;
                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<SearchSettingsModel> getsearchsettingsviewmodel(SearchSettingsModel searchmodel)
        {
           
         
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                      //  searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid && z.searchname == model.searchname).FirstOrDefault();


                       
                        searchsetting s =_unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                        //searchmodel.basicsearchsettings = searchsettingsextentions.getbasicsearchsettings(p, _unitOfWorkAsync);
                        //searchmodel.lifestylesearchsettings = searchsettingsextentions.getlifestylesearchsettings(p, _unitOfWorkAsync);
                        //searchmodel.appearancesearchsettings = searchsettingsextentions.getappearancesearchsettings(p, _unitOfWorkAsync);
                        //searchmodel.charactersearchsettings = searchsettingsextentions.getcharactersearchsettings(p, _unitOfWorkAsync);


                        //TO DO add rest of searches.

                        return searchmodel;
                        

                        
                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<BasicSearchSettingsModel> getbasicsearchsettings(SearchSettingsModel searchmodel)
        {
           
         
            {
                //if (searchmodel.profileid == 0) return null;

                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        BasicSearchSettingsModel model = new BasicSearchSettingsModel();
                        //get all the showmes so we can deterimine which are checked and which are not
                           model. showmelist = RedisCacheFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);
                           model. genderlist = RedisCacheFactory.SharedObjectHelper.getgenderlist(_unitOfWorkAsync);
                           model. sortbylist = RedisCacheFactory.SharedObjectHelper.getsortbytypelist(_unitOfWorkAsync);
                           model. agelist = RedisCacheFactory.SharedObjectHelper.getagelist();

                        searchsetting s= _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                        //empty search setting handling
                      
               


                        return searchsettingsextentions.getbasicsearchsettings(model,s, _unitOfWorkAsync);
                       
                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<AppearanceSearchSettingsModel> getappearancesearchsettings(SearchSettingsModel searchmodel)
        {
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();
                        model.ethnicitylist = RedisCacheFactory.SharedObjectHelper.getethnicitylist(_unitOfWorkAsync);
                        model.bodytypelist = RedisCacheFactory.SharedObjectHelper.getbodytypelist(_unitOfWorkAsync);
                        model.eyecolorlist = RedisCacheFactory.SharedObjectHelper.geteyecolorlist(_unitOfWorkAsync);
                        model.haircolorlist = RedisCacheFactory.SharedObjectHelper.gethaircolorlist(_unitOfWorkAsync);
                        model.hotfeaturelist = RedisCacheFactory.SharedObjectHelper.gethotfeaturelist(_unitOfWorkAsync);
                        model.metricheightlist = RedisCacheFactory.SharedObjectHelper.getmetricheightlist();


                        searchsetting s =_unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                        //empty search setting handling
                      
               
                        return searchsettingsextentions.getappearancesearchsettings(model, s, _unitOfWorkAsync);

                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<CharacterSearchSettingsModel> getcharactersearchsettings(SearchSettingsModel searchmodel)
        {
           
         
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();
                           model. humorlist = RedisCacheFactory.SharedObjectHelper.gethumorlist(_unitOfWorkAsync);
                           model. dietlist = RedisCacheFactory.SharedObjectHelper.getdietlist(_unitOfWorkAsync);
                           model. hobbylist = RedisCacheFactory.SharedObjectHelper.gethobbylist(_unitOfWorkAsync);
                           model.drinkslist = RedisCacheFactory.SharedObjectHelper.getdrinkslist(_unitOfWorkAsync);
                           model. exerciselist = RedisCacheFactory.SharedObjectHelper.getexerciselist(_unitOfWorkAsync);
                           model. smokeslist = RedisCacheFactory.SharedObjectHelper.getsmokeslist(_unitOfWorkAsync);
                           model. signlist = RedisCacheFactory.SharedObjectHelper.getsignlist(_unitOfWorkAsync);
                           model. politicalviewlist = RedisCacheFactory.SharedObjectHelper.getpoliticalviewlist(_unitOfWorkAsync);
                           model. religionlist = RedisCacheFactory.SharedObjectHelper.getreligionlist(_unitOfWorkAsync);
                           model. religiousattendancelist = RedisCacheFactory.SharedObjectHelper.getreligiousattendancelist(_unitOfWorkAsync);

                        searchsetting s =_unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                        //empty search setting handling
                      
               
                        return searchsettingsextentions.getcharactersearchsettings(model,s, _unitOfWorkAsync);

                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        public async Task<LifeStyleSearchSettingsModel> getlifestylesearchsettings(SearchSettingsModel searchmodel)
        {
           
         
            {

                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();

                        //populate values here ok ?
                        // if (p == null) return null;

                        model.educationlevellist = RedisCacheFactory.SharedObjectHelper.geteducationlevellist(_unitOfWorkAsync);
                           model. lookingforlist = RedisCacheFactory.SharedObjectHelper.getlookingforlist(_unitOfWorkAsync);
                           model. employmentstatuslist = RedisCacheFactory.SharedObjectHelper.getemploymentstatuslist(_unitOfWorkAsync);
                           model. havekidslist = RedisCacheFactory.SharedObjectHelper.gethavekidslist(_unitOfWorkAsync);
                           model. incomelevellist = RedisCacheFactory.SharedObjectHelper.getincomelevellist(_unitOfWorkAsync);
                           model. livingsituationlist = RedisCacheFactory.SharedObjectHelper.getlivingsituationlist(_unitOfWorkAsync);
                           model.maritalstatuslist = RedisCacheFactory.SharedObjectHelper.getmaritalstatuslist(_unitOfWorkAsync);
                           model. professionlist = RedisCacheFactory.SharedObjectHelper.getprofessionlist(_unitOfWorkAsync);
                           model.wantskidslist = RedisCacheFactory.SharedObjectHelper.getwantskidslist(_unitOfWorkAsync);



                        searchsetting s =_unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                        //empty search setting handling
                      
               
                        return searchsettingsextentions.getlifestylesearchsettings(model,s, _unitOfWorkAsync);

                    });
                    return await task.ConfigureAwait(false);

                }

                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.EditSearchService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in photo service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }       
#endregion

        #region "search update methods exposed"

        //this function validates the user has a matching apikey for this profile as well as has the role access to create new searches if its other than perfectmatch
        private AnewluvMessages validatecanmodifysearch(SearchSettingsModel searchmodel, AnewluvMessages AnewluvMessages)
        {
             //make sure API key matches 
            //check the api key matches the profile id pased 
            bool profileidmatchesapikey = false;
            if (searchmodel.apikey != null)
            {

                profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                             ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = searchmodel.apikey.Value, useridentifier = searchmodel.profileid.Value }).Result;
            }

            if (!profileidmatchesapikey)
                AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");

            if (profileidmatchesapikey)
            {
                //check role if user is a member and saarch name is not perfect match allow save
                var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = searchmodel.profileid });

                //if this s a new search make sure user is at least suscriber role
                if (!String.IsNullOrEmpty(searchmodel.searchname) && searchmodel.searchname != "MyPerfectMatch" && profile != null && !profile.membersinroles.Any(z => z.role_id == (int)roleEnum.Suscriber && z.active != false && z.roleexpiredate > DateTime.Now))
                    AnewluvMessages.errormessages.Add("You must be a suscriber to save a new search other than your Perfect Match Search! ");
            }
           
           return AnewluvMessages;

        }


        //global search upddate
        public async Task<AnewluvMessages> searcheditallsettings(SearchSettingsModel searchmodel)
        {            
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages AnewluvMessages = new AnewluvMessages();

                            AnewluvMessages = validatecanmodifysearch(searchmodel,AnewluvMessages);

                            //exit with error status 
                            if( AnewluvMessages.errormessages.Count() > 0 ) return AnewluvMessages;

                            //get the profile details :
                            //AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                            //handling for a new search
                            
                            AnewluvMessages = (searchsettingsextentions.updatebasicsearchsettings(searchmodel.basicsearchsettings, p, AnewluvMessages, _unitOfWorkAsync));
                            AnewluvMessages = (searchsettingsextentions.updateappearancesearchsettings(searchmodel.appearancesearchsettings, p, AnewluvMessages, _unitOfWorkAsync));
                            AnewluvMessages = (searchsettingsextentions.updatecharactersearchsettings(searchmodel.charactersearchsettings, p, AnewluvMessages, _unitOfWorkAsync));
                            AnewluvMessages = (searchsettingsextentions.updatelifestylesearchsettings(searchmodel.lifestylesearchsettings, p, AnewluvMessages, _unitOfWorkAsync));
                           

                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing Your Search Settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit Search Settings Succesful");
                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditSearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            
        }

        public async Task<AnewluvMessages> searcheditbasicsettings(SearchSettingsModel searchmodel)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages AnewluvMessages = new AnewluvMessages();

                            //validate access to this profile
                            AnewluvMessages = validatecanmodifysearch(searchmodel, AnewluvMessages);
                            //exit with error status 
                            if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;


                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                            //handling for a new search                            
                            AnewluvMessages = (searchsettingsextentions.updatebasicsearchsettings(searchmodel.basicsearchsettings, p, AnewluvMessages, _unitOfWorkAsync));


                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit character Settings Successful");
                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditSearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }




        }

        public async Task<AnewluvMessages> searcheditappearancesettings(SearchSettingsModel searchmodel)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //create a new messages object
                            AnewluvMessages AnewluvMessages = new AnewluvMessages();

                            //validate access to this profile
                            AnewluvMessages = validatecanmodifysearch(searchmodel, AnewluvMessages);
                            //exit with error status 
                            if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;

                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                            //handling for a new search

                            AnewluvMessages = (searchsettingsextentions.updateappearancesearchsettings(searchmodel.appearancesearchsettings, p, AnewluvMessages, _unitOfWorkAsync));

                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing You Appearance Settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit Appearance Settings Successful");
                            return AnewluvMessages;

                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.SearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in search actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }



        }

        public async Task<AnewluvMessages> searcheditcharactersettings(SearchSettingsModel searchmodel)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages AnewluvMessages = new AnewluvMessages();

                            //validate access to this profile
                            AnewluvMessages = validatecanmodifysearch(searchmodel, AnewluvMessages);
                            //exit with error status 
                            if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;


                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                            //handling for a new search

                            AnewluvMessages = (searchsettingsextentions.updatecharactersearchsettings(searchmodel.charactersearchsettings, p, AnewluvMessages, _unitOfWorkAsync));


                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit character Settings Successful");
                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }




        }

        public async Task<AnewluvMessages> searcheditlifestylesettings(SearchSettingsModel searchmodel)
        {
           
         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //create a new messages object
                            AnewluvMessages AnewluvMessages = new AnewluvMessages();

                            //validate access to this profile
                            AnewluvMessages = validatecanmodifysearch(searchmodel, AnewluvMessages);
                            //exit with error status 
                            if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;


                            //get the profile details :
                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(searchmodel,_unitOfWorkAsync);
                            //handling for a new search

                            AnewluvMessages = (searchsettingsextentions.updatelifestylesearchsettings(searchmodel.lifestylesearchsettings, p, AnewluvMessages, _unitOfWorkAsync));

                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing You lifestyle Settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit lifestyle Settings Successful");
                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in search actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }



        }




        #endregion


        

      
    }

}
