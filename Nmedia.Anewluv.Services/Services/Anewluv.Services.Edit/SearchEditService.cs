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
                      
                        searchmodel.basicsearchsettings = this.getbasicsearchsettings(p, _unitOfWorkAsync);
                        searchmodel.lifestylesearchsettings = this.getlifestylesearchsettings(p, _unitOfWorkAsync);
                        searchmodel.appearancesearchsettings = this.getappearancesearchsettings(p, _unitOfWorkAsync);
                        searchmodel.charactersearchsettings = this.getcharactersearchsettings(p, _unitOfWorkAsync);


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

                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);

                      

                        return this.getbasicsearchsettings(p, _unitOfWorkAsync);
                       
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
                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return this.getappearancesearchsettings(p, _unitOfWorkAsync);

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
                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return this.getcharactersearchsettings(p, _unitOfWorkAsync);

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
                        searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().filtersearchsettings(searchmodel);
                        return this.getlifestylesearchsettings(p, _unitOfWorkAsync);

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
                            messages = (updatebasicsearchsettings(model.basicsearchsettings,p, messages));
                            messages = (updateappearancesearchsettings(model.appearancesearchsettings, p, messages));
                            messages = (updatecharactersearchsettings(model.charactersearchsettings, p, messages));
                            messages = (updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages));
                           

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
                            messages = (updatebasicsearchsettings(model.basicsearchsettings, p, messages));
                           

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
                            messages = (updateappearancesearchsettings(model.appearancesearchsettings, p, messages));                          

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
                            messages = (updatecharactersearchsettings(model.charactersearchsettings, p, messages));
                          

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
                            messages = (updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages));

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


        #region "private get methods for reuses"

   

       private BasicSearchSettingsModel getbasicsearchsettings(searchsetting p,IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
              


                BasicSearchSettingsModel model = new BasicSearchSettingsModel();

                //populate values here ok ?
               // if (p == null) return null;

                    // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                    //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();
                    //populate ages select list here I guess
                    //TODO get from app fabric
                    // SharedRepository sharedrepository = new SharedRepository();
                    //Ages = sharedrepository.AgesSelectList;
                    

                    //get all the showmes so we can deterimine which are checked and which are not
                    var showmelist = CachingFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);
                    var genderlist = CachingFactory.SharedObjectHelper.getgenderlist(_unitOfWorkAsync);
                      var sortbylist = CachingFactory.SharedObjectHelper.getsortbytypelist(_unitOfWorkAsync);
                var agelist = CachingFactory.SharedObjectHelper.getagelist();
                       
                    model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                    model.mygenderid =  p.profilemetadata != null? p.profilemetadata.profile.profiledata.gender_id.GetValueOrDefault():1;
                    model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                    model.creationdate = p.creationdate == null ? (DateTime?)null : p.creationdate.GetValueOrDefault();                 
                    model.distancefromme=  p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    model.lastupdatedate = p.lastupdatedate == null ? DateTime.Now : p.lastupdatedate;
                    model.searchname = p.searchname == null ? "Default" : p.searchname;
                    model.searchrank = p.searchrank == null ? 1 : p.searchrank;
                    model.myperfectmatch = p.myperfectmatch == null ? true : p.myperfectmatch;
                    model.systemmatch = p.systemmatch == null ? false : p.systemmatch;
                    //test of map the list items to the generic listitem object in order to clean up the models so no iselected item on them
                    model.showmelist = showmelist;
                    model.genderlist = genderlist;
                    model.sortbylist = sortbylist;
                    model.agelist = agelist;  //TO do have it use desction and IC as well instead of age object
                
                    //update the list with the items that are selected.
                    foreach (listitem showme in showmelist.Where(c => p.details.Where(m=>m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.showme).Any(f => f.value == c.id)))
                    {
                       //update the value as checked here on the list
                       model.showmelist.First(d => d.id == showme.id).selected = true; 
                    }

                    //update the list with the items that are selected.
                    foreach (listitem gender in genderlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Any(f => f.value == c.id)))
                    {
                        //update the value as checked here on the list
                        model.showmelist.First(d => d.id == gender.id).selected = true;
                    }


                    //update the list with the items that are selected.
                    foreach (listitem sortbytype in sortbylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sortbytype).Any(f => f.value == c.id)))
                    {
                        //update the value as checked here on the list
                        model.sortbylist.First(d => d.id == sortbytype.id).selected = true;
                    }



                    //Location does not match any list i think need to have this tweaked for now ignore
                    //full location since it includes the city
                    //for now UI only allows one but this code allows for many
                    foreach (var item in p.locations)
                    {
                        model.locationlist.Add(item);
                    }
                    

                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(p.profile_id));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private AppearanceSearchSettingsModel getappearancesearchsettings(searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
                //searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();

                //populate values here ok ?
                //if (p == null) return null;


                //get all the showmes so we can deterimine which are checked and which are not
                var ethnicitylist = CachingFactory.SharedObjectHelper.getethnicitylist(_unitOfWorkAsync);
                var bodytypelist = CachingFactory.SharedObjectHelper.getbodytypelist(_unitOfWorkAsync);
                var eyecolorlist = CachingFactory.SharedObjectHelper.geteyecolorlist(_unitOfWorkAsync);
                var haircolorlist = CachingFactory.SharedObjectHelper.gethaircolorlist(_unitOfWorkAsync);
                var hotfeaturelist = CachingFactory.SharedObjectHelper.gethotfeaturelist(_unitOfWorkAsync);
                var metricheightlist = CachingFactory.SharedObjectHelper.getmetricheightlist();
             
                var showmelist = CachingFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);

                model.heightmax = p.heightmax == null ? 210 : p.heightmax;
                model.heightmin = p.heightmin == null ? 100 : p.heightmin;

                model.ethnicitylist = ethnicitylist;
                model.bodytypeslist = bodytypelist;
                model.eyecolorlist = eyecolorlist;
                model.haircolorlist = haircolorlist;
                model.hotfeaturelist = hotfeaturelist;
                model.metricheightlist = metricheightlist;
               

                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = _unitOfWorkAsync.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                //update the list with the items that are selected.
                foreach (listitem ethnicity in ethnicitylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.ethnicitylist.First(d => d.id == ethnicity.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem bodytype in bodytypelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.bodytypeslist.First(d => d.id == bodytype.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem eyecolor in eyecolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.eyecolorlist.First(d => d.id == eyecolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem haircolor in haircolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.haircolorlist.First(d => d.id == haircolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem hotfeature in hotfeaturelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.hotfeaturelist.First(d => d.id == hotfeature.id).selected = true;
                }

                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(p.profile_id));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private CharacterSearchSettingsModel getcharactersearchsettings(searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
                //searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();

                //populate values here ok ?
               // if (p == null) return null;

                var humorlist = CachingFactory.SharedObjectHelper.gethumorlist(_unitOfWorkAsync);
                var dietlist = CachingFactory.SharedObjectHelper.getdietlist(_unitOfWorkAsync);
                var hobbylist = CachingFactory.SharedObjectHelper.gethobbylist(_unitOfWorkAsync);
                var drinklist = CachingFactory.SharedObjectHelper.getdrinkslist(_unitOfWorkAsync);
                var exerciselist = CachingFactory.SharedObjectHelper.getexerciselist(_unitOfWorkAsync);
                var smokeslist = CachingFactory.SharedObjectHelper.getsmokeslist(_unitOfWorkAsync);
                var signlist = CachingFactory.SharedObjectHelper.getsignlist(_unitOfWorkAsync);
                var politicalviewlist = CachingFactory.SharedObjectHelper.getpoliticalviewlist(_unitOfWorkAsync);
                var religionlist = CachingFactory.SharedObjectHelper.getreligionlist(_unitOfWorkAsync);
                var religiousattendancelist = CachingFactory.SharedObjectHelper.getreligiousattendancelist(_unitOfWorkAsync);

                model.humorlist = humorlist; ;
                model.dietlist = dietlist;
                model.hobbylist = hobbylist;
                model.drinkslist = drinklist;
                model.exerciselist = exerciselist;
                model.smokeslist = smokeslist;
                model.signlist = signlist;
                model.politicalviewlist = politicalviewlist;
                model.religionlist = religionlist;
                model.religiousattendancelist = religiousattendancelist;

              
                //update the list with the items that are selected.
                foreach (listitem humor in humorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.humor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.humorlist.First(d => d.id == humor.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem diet in dietlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.diet).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.dietlist.First(d => d.id == diet.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem hobby in hobbylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hobby).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.hobbylist.First(d => d.id == hobby.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem drink in drinklist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.drink).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.drinkslist.First(d => d.id == drink.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem exercise in exerciselist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.excercise).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.exerciselist.First(d => d.id == exercise.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem smokes in smokeslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.smokes).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.smokeslist.First(d => d.id == smokes.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem sign in signlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sign).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.signlist.First(d => d.id == sign.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem politicalview in politicalviewlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.politicalview).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem religion in religionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religion).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.religionlist.First(d => d.id == religion.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem religiousattendance in religiousattendancelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religiousattendance).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.religiousattendancelist.First(d => d.id == religiousattendance.id).selected = true;
                }


                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(p.profile_id));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private LifeStyleSearchSettingsModel getlifestylesearchsettings(searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
               // searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();

                //populate values here ok ?
               // if (p == null) return null;

                var educationlevellist = CachingFactory.SharedObjectHelper.geteducationlevellist(_unitOfWorkAsync);
                var lookingforlist = CachingFactory.SharedObjectHelper.getlookingforlist(_unitOfWorkAsync);
                var employmentstatuslist = CachingFactory.SharedObjectHelper.getemploymentstatuslist(_unitOfWorkAsync);
                var havekidslist = CachingFactory.SharedObjectHelper.gethavekidslist(_unitOfWorkAsync);
                var incomelevellist = CachingFactory.SharedObjectHelper.getincomelevellist(_unitOfWorkAsync);
                var livingsituationlist = CachingFactory.SharedObjectHelper.getlivingsituationlist(_unitOfWorkAsync);
                var maritialstatuslist = CachingFactory.SharedObjectHelper.getmaritalstatuslist(_unitOfWorkAsync); 
                var professionlist = CachingFactory.SharedObjectHelper.getprofessionlist(_unitOfWorkAsync);
                var wantkidslist = CachingFactory.SharedObjectHelper.getwantskidslist(_unitOfWorkAsync);


                model.educationlevellist = educationlevellist;
                   model. lookingforlist = lookingforlist;
                   model. employmentstatuslist = employmentstatuslist;
                   model. havekidslist = havekidslist;
                   model. incomelevellist = incomelevellist;
                   model. livingsituationlist = livingsituationlist;
                   model.maritalstatuslist = maritialstatuslist;
                   model. professionlist = professionlist;
                   model.wantskidslist = wantkidslist;


                //update the list with the items that are selected.
                   foreach (listitem educationlevel in educationlevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.educationlevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.educationlevellist.First(d => d.id == educationlevel.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem lookingfor in lookingforlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.lookingfor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.lookingforlist.First(d => d.id == lookingfor.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem employmentstatus in employmentstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.employmentstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.employmentstatuslist.First(d => d.id == employmentstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem incomelevel in incomelevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.incomelevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.incomelevellist.First(d => d.id == incomelevel.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem livingsituation in livingsituationlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.livingsituation).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.livingsituationlist.First(d => d.id == livingsituation.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem maritialstatus in maritialstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.maritialstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.maritalstatuslist.First(d => d.id == maritialstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem profession in professionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.profession).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.professionlist.First(d => d.id == profession.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (listitem wantkids in wantkidslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.wantskids).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.wantskidslist.First(d => d.id == wantkids.id).selected = true;

                }

             
                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(p.profile_id));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in searchsettings actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

#endregion


        #region "private update methods for reuse so we can also combine in one call"

        //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
        private AnewluvMessages updatebasicsearchsettings(BasicSearchSettingsModel model,searchsetting p, AnewluvMessages messages)
        {

                bool nothingupdated = true;
                try
                {

                 
                     
                        //create a new messages object
                        if (p == null)
                        {
                            messages.errormessages.Add("There is no search with this parameters");
                            return messages;
                        }

                        p.agemin = model.agemin;
                        p.agemax = model.agemax;

                        p.distancefromme = model.distancefromme;
                        p.lastupdatedate = model.lastupdatedate;
                        p.searchname = model.searchname;
                        p.searchrank = model.searchrank;
                        p.myperfectmatch = model.myperfectmatch;

                        //checkbos item updates 
                        if (model.genderlist.Count() > 0)
                            updatesearchsettingsdetail(model.genderlist, p, searchsettingdetailtypeEnum.gender);
                        if (model.sortbylist.Count() > 0)
                            updatesearchsettingsdetail(model.sortbylist, p, searchsettingdetailtypeEnum.sortbytype);
                        if (model.showmelist.Count > 0)
                            updatesearchsettingsdetail(model.showmelist, p, searchsettingdetailtypeEnum.showme);
                        if (model.locationlist.Count > 0)
                            updatesearchsettingslocation (model.locationlist, p);


                        _unitOfWorkAsync.Repository<searchsetting>().Update(p);
                      var i  =_unitOfWorkAsync.SaveChanges();

                       // return messages;

                 

                }
                catch (Exception ex)
                {
                    //handle logging here
                    var message = ex.Message;
                    throw ex;

                }
                return messages;

            


        }
        //TO DO add validation and pass back via messages 

        private AnewluvMessages updateappearancesearchsettings(AppearanceSearchSettingsModel model, searchsetting p, AnewluvMessages messages)
        {

            try
            {
                  //  searchsetting p =searchsettingdetailtypeEnum.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                    //create a new messages object
                    if (p == null)
                    {
                        messages.errormessages.Add("There is no appearance search with this parameters");
                        return messages;
                    }

                    p.heightmax = model.heightmin;
                    p.heightmax = model.heightmax;
                    //checkbos item updates 
                    if (model.ethnicitylist.Count > 0)
                        updatesearchsettingsdetail(model.ethnicitylist.ToList(), p,searchsettingdetailtypeEnum.ethnicity);
                    if (model.bodytypeslist.Count > 0)
                        updatesearchsettingsdetail(model.bodytypeslist.ToList(), p,searchsettingdetailtypeEnum.bodytype);
                    if (model.eyecolorlist.Count > 0)
                        updatesearchsettingsdetail(model.eyecolorlist.ToList(), p,searchsettingdetailtypeEnum.eyecolor);
                    if (model.haircolorlist.Count > 0)
                        updatesearchsettingsdetail(model.haircolorlist.ToList(), p,searchsettingdetailtypeEnum.haircolor);
                    if (model.hotfeaturelist.Count > 0)
                        updatesearchsettingsdetail(model.hotfeaturelist.ToList(), p,searchsettingdetailtypeEnum.hotfeature);

                     _unitOfWorkAsync.Repository<searchsetting>().Update(p);
                    var i  =_unitOfWorkAsync.SaveChanges();

                  //  return messages;
            }
            catch (Exception ex)
            {
                    //handle logging here
                    var message = ex.Message;
                    throw ex;

            }
            return messages;

        }

        private AnewluvMessages updatecharactersearchsettings(CharacterSearchSettingsModel model, searchsetting p, AnewluvMessages messages)
        {
         
                try
                {

                   
                       // AnewluvMessages messages = new AnewluvMessages();
                      //  searchsetting p =searchsettingdetailtypeEnum.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                        //create a new messages object
                        if (p == null)
                        {
                            messages.errormessages.Add("There is no search with this parameters");
                            return messages;
                        }


                        //checkbos item updates 
                        if (model.dietlist.Count > 0)
                            updatesearchsettingsdetail(model.dietlist.ToList(), p, searchsettingdetailtypeEnum.diet);

                        if (model.humorlist.Count > 0)
                            updatesearchsettingsdetail(model.humorlist.ToList(), p, searchsettingdetailtypeEnum.humor);

                        if (model.hobbylist.Count > 0)
                            updatesearchsettingsdetail(model.hobbylist.ToList(), p, searchsettingdetailtypeEnum.hobby);

                        if (model.drinkslist.Count > 0)
                            updatesearchsettingsdetail(model.drinkslist.ToList(), p, searchsettingdetailtypeEnum.drink);

                        if (model.exerciselist.Count > 0)
                            updatesearchsettingsdetail(model.exerciselist.ToList(), p, searchsettingdetailtypeEnum.excercise);

                        if (model.smokeslist.Count > 0)
                            updatesearchsettingsdetail(model.smokeslist.ToList(), p, searchsettingdetailtypeEnum.smokes);

                        if (model.signlist.Count > 0)
                            updatesearchsettingsdetail(model.signlist.ToList(), p, searchsettingdetailtypeEnum.sign);

                        if (model.politicalviewlist.Count > 0)
                            updatesearchsettingsdetail(model.politicalviewlist.ToList(), p, searchsettingdetailtypeEnum.politicalview);

                        if (model.religionlist.Count > 0)
                            updatesearchsettingsdetail(model.religionlist.ToList(), p, searchsettingdetailtypeEnum.religion);

                        if (model.religiousattendancelist.Count > 0)
                            updatesearchsettingsdetail(model.religiousattendancelist.ToList(), p, searchsettingdetailtypeEnum.religiousattendance);




                      var i  =_unitOfWorkAsync.SaveChanges();


               

                }
                 catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;
     


        }

        private AnewluvMessages updatelifestylesearchsettings(LifeStyleSearchSettingsModel model, searchsetting p, AnewluvMessages messages)
        {
           
                try
                {

                   
                       // AnewluvMessages messages = new AnewluvMessages();
                        //searchsetting p = searchsettingdetailtypeEnum..Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                        //create a new messages object
                        if (p == null)
                        {
                            messages.errormessages.Add("There is no search with this parameters, lifestype searchs");
                            return messages;
                        }


                        //checkbos item updates 
                        if (model.educationlevellist.Count > 0)
                            updatesearchsettingsdetail(model.educationlevellist, p, searchsettingdetailtypeEnum.educationlevel);

                        if (model.lookingforlist.Count > 0)
                            updatesearchsettingsdetail(model.lookingforlist.ToList(), p, searchsettingdetailtypeEnum.lookingfor);

                        if (model.employmentstatuslist.Count > 0)
                            updatesearchsettingsdetail(model.employmentstatuslist.ToList(), p, searchsettingdetailtypeEnum.employmentstatus);

                        if (model.havekidslist.Count > 0)
                            updatesearchsettingsdetail(model.havekidslist.ToList(), p, searchsettingdetailtypeEnum.havekids);

                        if (model.incomelevellist.Count > 0)
                            updatesearchsettingsdetail(model.incomelevellist.ToList(), p, searchsettingdetailtypeEnum.incomelevel);
                
                        if (model.livingsituationlist.Count > 0)
                            updatesearchsettingsdetail(model.livingsituationlist.ToList(), p, searchsettingdetailtypeEnum.livingsituation);

                        if (model.maritalstatuslist.Count > 0)
                            updatesearchsettingsdetail(model.maritalstatuslist.ToList(), p, searchsettingdetailtypeEnum.maritialstatus);

                        if (model.professionlist.Count > 0)
                            updatesearchsettingsdetail(model.professionlist.ToList(), p, searchsettingdetailtypeEnum.profession);

                        if (model.wantskidslist.Count > 0)
                            updatesearchsettingsdetail(model.wantskidslist.ToList(), p, searchsettingdetailtypeEnum.wantskids);


                      var i  =_unitOfWorkAsync.SaveChanges();



                }
                  catch (Exception ex)
                {
                    //handle logging here
                    var message = ex.Message;
                    throw ex;

                }
            return messages;
            


        }


        #endregion

        #region "PRIVATE Checkbox Update Functions for seaerch settings many to many"


        //Basic Checkbox settings updates
        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata gender
        private void updatesearchsettingsdetail(List<listitem> updateditems, searchsetting currentsearchsettings, searchsettingdetailtypeEnum searchsettingtype)
        {
            if (updateditems == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var item in updateditems.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingtype).Any(f => f.value == item.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = item.id; //add the current gender value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingtype;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.searchsettingdetailtype_id == (int)searchsettingtype && p.value == item.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
       
        //profiledata location
        private void updatesearchsettingslocation(List<searchsetting_location> updatedlocations, searchsetting currentsearchsettings)
        {
            if (updatedlocations == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var item in updatedlocations.Where(z => z.countryid !=null))
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if (currentsearchsettings.locations.Where(m => m.countryid == item.countryid && m.city ==item.city && m.postalcode ==item.postalcode ).FirstOrDefault()==null)
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_location();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.countryid = item.countryid; //add the current gender value since its new.
                    temp.postalcode  =  item.postalcode;
                    temp.city = item.city;
                    _unitOfWorkAsync.Repository<searchsetting_location>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_location>().Queryable().Where(m => m.countryid == item.countryid && m.city ==item.city && m.postalcode ==item.postalcode ).FirstOrDefault();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }     

        //END of Basic settings ///////////////////////


        #endregion

      
    }

}
