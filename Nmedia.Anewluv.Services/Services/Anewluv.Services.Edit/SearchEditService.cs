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


                       
                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);

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
                           model. showmelist = CachingFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);
                           model. genderlist = CachingFactory.SharedObjectHelper.getgenderlist(_unitOfWorkAsync);
                           model. sortbylist = CachingFactory.SharedObjectHelper.getsortbytypelist(_unitOfWorkAsync);
                           model. agelist = CachingFactory.SharedObjectHelper.getagelist();

                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);



                        return searchsettingsextentions.getbasicsearchsettings(model,p, _unitOfWorkAsync);
                       
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
                        model.ethnicitylist = CachingFactory.SharedObjectHelper.getethnicitylist(_unitOfWorkAsync);
                        model.bodytypelist = CachingFactory.SharedObjectHelper.getbodytypelist(_unitOfWorkAsync);
                        model.eyecolorlist = CachingFactory.SharedObjectHelper.geteyecolorlist(_unitOfWorkAsync);
                        model.haircolorlist = CachingFactory.SharedObjectHelper.gethaircolorlist(_unitOfWorkAsync);
                        model.hotfeaturelist = CachingFactory.SharedObjectHelper.gethotfeaturelist(_unitOfWorkAsync);
                        model.metricheightlist = CachingFactory.SharedObjectHelper.getmetricheightlist();


                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return searchsettingsextentions.getappearancesearchsettings(model, p, _unitOfWorkAsync);

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
                           model. humorlist = CachingFactory.SharedObjectHelper.gethumorlist(_unitOfWorkAsync);
                           model. dietlist = CachingFactory.SharedObjectHelper.getdietlist(_unitOfWorkAsync);
                           model. hobbylist = CachingFactory.SharedObjectHelper.gethobbylist(_unitOfWorkAsync);
                           model.drinkslist = CachingFactory.SharedObjectHelper.getdrinkslist(_unitOfWorkAsync);
                           model. exerciselist = CachingFactory.SharedObjectHelper.getexerciselist(_unitOfWorkAsync);
                           model. smokeslist = CachingFactory.SharedObjectHelper.getsmokeslist(_unitOfWorkAsync);
                           model. signlist = CachingFactory.SharedObjectHelper.getsignlist(_unitOfWorkAsync);
                           model. politicalviewlist = CachingFactory.SharedObjectHelper.getpoliticalviewlist(_unitOfWorkAsync);
                           model. religionlist = CachingFactory.SharedObjectHelper.getreligionlist(_unitOfWorkAsync);
                           model. religiousattendancelist = CachingFactory.SharedObjectHelper.getreligiousattendancelist(_unitOfWorkAsync);

                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return searchsettingsextentions.getcharactersearchsettings(model,p, _unitOfWorkAsync);

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

                        model.educationlevellist = CachingFactory.SharedObjectHelper.geteducationlevellist(_unitOfWorkAsync);
                           model. lookingforlist = CachingFactory.SharedObjectHelper.getlookingforlist(_unitOfWorkAsync);
                           model. employmentstatuslist = CachingFactory.SharedObjectHelper.getemploymentstatuslist(_unitOfWorkAsync);
                           model. havekidslist = CachingFactory.SharedObjectHelper.gethavekidslist(_unitOfWorkAsync);
                           model. incomelevellist = CachingFactory.SharedObjectHelper.getincomelevellist(_unitOfWorkAsync);
                           model. livingsituationlist = CachingFactory.SharedObjectHelper.getlivingsituationlist(_unitOfWorkAsync);
                           model.maritalstatuslist = CachingFactory.SharedObjectHelper.getmaritalstatuslist(_unitOfWorkAsync);
                           model. professionlist = CachingFactory.SharedObjectHelper.getprofessionlist(_unitOfWorkAsync);
                           model.wantskidslist = CachingFactory.SharedObjectHelper.getwantskidslist(_unitOfWorkAsync);



                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return searchsettingsextentions.getlifestylesearchsettings(model,p, _unitOfWorkAsync);

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

        //global search upddate
        public async Task<AnewluvMessages> searcheditallsettings(SearchSettingsModel model)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages messages = new AnewluvMessages();

                            

                            //get the profile details :
                            //AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(model);
                            messages = (searchsettingsextentions.updatebasicsearchsettings(model.basicsearchsettings, p, messages,_unitOfWorkAsync));
                            messages = (searchsettingsextentions.updateappearancesearchsettings(model.appearancesearchsettings, p, messages,_unitOfWorkAsync));
                            messages = (searchsettingsextentions.updatecharactersearchsettings(model.charactersearchsettings, p, messages,_unitOfWorkAsync));
                            messages = (searchsettingsextentions.updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages,_unitOfWorkAsync));
                           

                            if (messages.errormessages.Count > 0)
                            {
                                messages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                                return messages;
                            }
                            messages.messages.Add("Edit character Settings Successful");
                            return messages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditSearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        public async Task<AnewluvMessages> searcheditbasicsettings(SearchSettingsModel model)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages messages = new AnewluvMessages();


                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(model);
                            messages = (searchsettingsextentions.updatebasicsearchsettings(model.basicsearchsettings, p, messages,_unitOfWorkAsync));
                           

                            if (messages.errormessages.Count > 0)
                            {
                                messages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                                return messages;
                            }
                            messages.messages.Add("Edit character Settings Successful");
                            return messages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditSearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        public async Task<AnewluvMessages> searcheditappearancesettings(SearchSettingsModel model)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //create a new messages object
                            AnewluvMessages messages = new AnewluvMessages();

                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(model);
                            messages = (searchsettingsextentions.updateappearancesearchsettings(model.appearancesearchsettings, p, messages,_unitOfWorkAsync));                          

                            if (messages.errormessages.Count > 0)
                            {
                                messages.errormessages.Add("There was a problem Editing You Appearance Settings, Please try again later");
                                return messages;
                            }
                            messages.messages.Add("Edit Appearance Settings Successful");
                            return messages;

                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.SearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        public async Task<AnewluvMessages> searcheditcharactersettings(SearchSettingsModel model)
        {

         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create a new messages object
                            AnewluvMessages messages = new AnewluvMessages();

                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(model);
                            messages = (searchsettingsextentions.updatecharactersearchsettings(model.charactersearchsettings, p, messages,_unitOfWorkAsync));
                          

                            if (messages.errormessages.Count > 0)
                            {
                                messages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                                return messages;
                            }
                            messages.messages.Add("Edit character Settings Successful");
                            return messages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        public async Task<AnewluvMessages> searcheditlifestylesettings(SearchSettingsModel model)
        {
           
         
            {
              
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //create a new messages object
                            AnewluvMessages messages = new AnewluvMessages();
                            //get the profile details :
                            searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(model);
                            messages = (searchsettingsextentions.updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages,_unitOfWorkAsync));

                            if (messages.errormessages.Count > 0)
                            {
                                messages.errormessages.Add("There was a problem Editing You lifestyle Settings, Please try again later");
                                return messages;
                            }
                            messages.messages.Add("Edit lifestyle Settings Successful");
                            return messages;
                        });
                        return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
