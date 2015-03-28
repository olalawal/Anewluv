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
                            messages = (updatebasicsearchsettings(model.basicsearchsettings,p, messages, _unitOfWorkAsync));
                            messages = (updateappearancesearchsettings(model.appearancesearchsettings, p, messages, _unitOfWorkAsync));
                            messages = (updatecharactersearchsettings(model.charactersearchsettings, p, messages, _unitOfWorkAsync));
                            messages = (updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages, _unitOfWorkAsync));
                           

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
                            messages = (updatebasicsearchsettings(model.basicsearchsettings, p, messages, _unitOfWorkAsync));
                           

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
                            messages = (updateappearancesearchsettings(model.appearancesearchsettings, p, messages, _unitOfWorkAsync));                          

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
                            messages = (updatecharactersearchsettings(model.charactersearchsettings, p, messages, _unitOfWorkAsync));
                          

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
                            messages = (updatelifestylesearchsettings(model.lifestylesearchsettings, p, messages, _unitOfWorkAsync));

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
                    model.mygenderid =  p.profilemetadata != null? p.profilemetadata.profiledata.gender_id.GetValueOrDefault():1;
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

                model.ethnicitylist = ethnicitylist.ToList().Select(o => new lu_ethnicity { id = o.id, description = o.description, selected = false }).ToList();
                model.bodytypeslist = bodytypelist.ToList().Select(o => new lu_bodytype { id = o.id, description = o.description, selected = false }).ToList();
                model.eyecolorlist = eyecolorlist.ToList().Select(o => new lu_eyecolor { id = o.id, description = o.description, selected = false }).ToList();
                model.haircolorlist = haircolorlist.ToList().Select(o => new lu_haircolor { id = o.id, description = o.description, selected = false }).ToList();
                model.hotfeaturelist = hotfeaturelist.ToList().Select(o => new lu_hotfeature { id = o.id, description = o.description, selected = false }).ToList();
                model.metricheightlist = metricheightlist;
               

                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = _unitOfWorkAsync.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                //update the list with the items that are selected.
                foreach (lu_ethnicity ethnicity in ethnicitylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.ethnicitylist.First(d => d.id == ethnicity.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_bodytype bodytype in bodytypelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.bodytypeslist.First(d => d.id == bodytype.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_eyecolor eyecolor in eyecolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.eyecolorlist.First(d => d.id == eyecolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_haircolor haircolor in haircolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.haircolorlist.First(d => d.id == haircolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_hotfeature hotfeature in hotfeaturelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Any(f => f.value == c.id)))
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

                model.humorlist = humorlist.ToList().Select(o => new lu_humor { id = o.id, description = o.description, selected = false }).ToList();
                model.dietlist = dietlist.ToList().Select(o => new lu_diet { id = o.id, description = o.description, selected = false }).ToList();
                model.hobbylist = hobbylist.ToList().Select(o => new lu_hobby { id = o.id, description = o.description, selected = false }).ToList();
                model.drinkslist = drinklist.ToList().Select(o => new lu_drinks { id = o.id, description = o.description, selected = false }).ToList();
                model.exerciselist = exerciselist.ToList().Select(o => new lu_exercise { id = o.id, description = o.description, selected = false }).ToList();
                model.smokeslist = smokeslist.ToList().Select(o => new lu_smokes { id = o.id, description = o.description, selected = false }).ToList();
                model.signlist = signlist.ToList().Select(o => new lu_sign { id = o.id, description = o.description, selected = false }).ToList();
                model.politicalviewlist = politicalviewlist.ToList().Select(o => new lu_politicalview { id = o.id, description = o.description, selected = false }).ToList();
                model.religionlist = religionlist.ToList().Select(o => new lu_religion { id = o.id, description = o.description, selected = false }).ToList();
                model.religiousattendancelist = religiousattendancelist.ToList().Select(o => new lu_religiousattendance { id = o.id, description = o.description, selected = false }).ToList();

              
                //update the list with the items that are selected.
                foreach (lu_humor humor in humorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.humor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.humorlist.First(d => d.id == humor.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_diet diet in dietlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.diet).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.dietlist.First(d => d.id == diet.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_hobby hobby in hobbylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hobby).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.hobbylist.First(d => d.id == hobby.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_drinks drink in drinklist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.drink).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.drinkslist.First(d => d.id == drink.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_exercise exercise in exerciselist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.excercise).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.exerciselist.First(d => d.id == exercise.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_smokes smokes in smokeslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.smokes).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.smokeslist.First(d => d.id == smokes.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_sign sign in signlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sign).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.signlist.First(d => d.id == sign.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_politicalview politicalview in politicalviewlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.politicalview).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_religion religion in religionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religion).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.religionlist.First(d => d.id == religion.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_religiousattendance religiousattendance in religiousattendancelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religiousattendance).Any(f => f.value == c.id)))
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


                model.educationlevellist = educationlevellist.ToList().Select(o => new lu_educationlevel { id = o.id, description = o.description, selected = false }).ToList();
                   model. lookingforlist = lookingforlist.ToList().Select(o => new lu_lookingfor { id = o.id, description = o.description, selected = false }).ToList();
                   model. employmentstatuslist = employmentstatuslist.ToList().Select(o => new lu_employmentstatus { id = o.id, description = o.description, selected = false }).ToList();
                   model. havekidslist = havekidslist.ToList().Select(o => new lu_havekids { id = o.id, description = o.description, selected = false }).ToList();
                   model. incomelevellist = incomelevellist.ToList().Select(o => new lu_incomelevel { id = o.id, description = o.description, selected = false }).ToList();
                   model. livingsituationlist = livingsituationlist.ToList().Select(o => new lu_livingsituation { id = o.id, description = o.description, selected = false }).ToList();
                   model.maritalstatuslist = maritialstatuslist.ToList().Select(o => new lu_maritalstatus { id = o.id, description = o.description, selected = false }).ToList();
                   model. professionlist = professionlist.ToList().Select(o => new lu_profession { id = o.id, description = o.description, selected = false }).ToList();
                   model.wantskidslist = wantkidslist.ToList().Select(o => new lu_wantskids { id = o.id, description = o.description, selected = false }).ToList();


                //update the list with the items that are selected.
                   foreach (lu_educationlevel educationlevel in educationlevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.educationlevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.educationlevellist.First(d => d.id == educationlevel.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_lookingfor lookingfor in lookingforlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.lookingfor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.lookingforlist.First(d => d.id == lookingfor.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_employmentstatus employmentstatus in employmentstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.employmentstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.employmentstatuslist.First(d => d.id == employmentstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_incomelevel incomelevel in incomelevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.incomelevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.incomelevellist.First(d => d.id == incomelevel.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_livingsituation livingsituation in livingsituationlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.livingsituation).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.livingsituationlist.First(d => d.id == livingsituation.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_maritalstatus maritialstatus in maritialstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.maritialstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.maritalstatuslist.First(d => d.id == maritialstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_profession profession in professionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.profession).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.professionlist.First(d => d.id == profession.id).selected = true;
                }

                //update the list with the items that are selected.
                   foreach (lu_wantskids wantkids in wantkidslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.w).Any(f => f.value == c.id)))
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
        private AnewluvMessages updatebasicsearchsettings(BasicSearchSettingsModel model,searchsetting p, AnewluvMessages messages, IUnitOfWorkAsync _unitOfWorkAsync)
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
                            updatesearchsettingsgender(model.genderlist, p, _unitOfWorkAsync);
                        if (model.sortbylist.Count() > 0)
                            updatesearchsettingssortby(model.sortbylist, p, _unitOfWorkAsync);
                        if (model.showmelist.Count > 0)
                            updatesearchsettingsshowme(model.showmelist, p, _unitOfWorkAsync);
                        if (model.locationlist.Count > 0)
                            updatesearchsettingslocation(model.locationlist, p, _unitOfWorkAsync);


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

        private AnewluvMessages updateappearancesearchsettings(AppearanceSearchSettingsModel model, searchsetting p, AnewluvMessages messages, IUnitOfWorkAsync _unitOfWorkAsync)
        {

            try
            {
                  //  searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
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
                        updatesearchsettingsethnicity(model.ethnicitylist.ToList(), p, _unitOfWorkAsync);
                    if (model.bodytypeslist.Count > 0)
                        updatesearchsettingsbodytypes(model.bodytypeslist.ToList(), p, _unitOfWorkAsync);
                    if (model.eyecolorlist.Count > 0)
                        updatesearchsettingseyecolor(model.eyecolorlist.ToList(), p, _unitOfWorkAsync);
                    if (model.haircolorlist.Count > 0)
                        updatesearchsettingshaircolor(model.haircolorlist.ToList(), p, _unitOfWorkAsync);
                    if (model.hotfeaturelist.Count > 0)
                        updatesearchsettingshotfeature(model.hotfeaturelist.ToList(), p, _unitOfWorkAsync);

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

        private AnewluvMessages updatecharactersearchsettings(CharacterSearchSettingsModel model, searchsetting p, AnewluvMessages messages, IUnitOfWorkAsync _unitOfWorkAsync)
        {
         
                try
                {

                   
                       // AnewluvMessages messages = new AnewluvMessages();
                      //  searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                        //create a new messages object
                        if (p == null)
                        {
                            messages.errormessages.Add("There is no search with this parameters");
                            return messages;
                        }


                        //checkbos item updates 
                        if (model.diet.Count > 0)
                            updatesearchsettingsgender(model.gender.ToList(), p, _unitOfWorkAsync);

                        if (model.humor.Count > 0)
                            updatesearchsettingssortby(model.sortbytype.ToList(), p, _unitOfWorkAsync);

                        if (model.hobby.Count > 0)
                            updatesearchsettingsshowme(model.showme.ToList(), p, _unitOfWorkAsync);

                        if (model.drink.Count > 0)
                            updatesearchsettingslocation(model.location.ToList(), p, _unitOfWorkAsync);

                        if (model.exercise.Count > 0)
                            updatesearchsettingsexercise(model.exercise.ToList(), p, _unitOfWorkAsync);

                        if (model.smokes.Count > 0)
                            updatesearchsettingssmokes(model.smokes.ToList(), p, _unitOfWorkAsync);

                        if (model.sign.Count > 0)
                            updatesearchsettingssign(model.sign.ToList(), p, _unitOfWorkAsync);

                        if (model.politicalview.Count > 0)
                            updatesearchsettingspoliticalview(model.politicalview.ToList(), p, _unitOfWorkAsync);

                        if (model.religion.Count > 0)
                            updatesearchsettingsreligion(model.religion.ToList(), p, _unitOfWorkAsync);

                        if (model.religiousattendance.Count > 0)
                            updatesearchsettingsreligiousattendance(model.religiousattendance.ToList(), p, _unitOfWorkAsync);




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

        private AnewluvMessages updatelifestylesearchsettings(LifeStyleSearchSettingsModel model, searchsetting p, AnewluvMessages messages, IUnitOfWorkAsync _unitOfWorkAsync)
        {
           
                try
                {

                   
                       // AnewluvMessages messages = new AnewluvMessages();
                        //searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                        //create a new messages object
                        if (p == null)
                        {
                            messages.errormessages.Add("There is no search with this parameters, lifestype searchs");
                            return messages;
                        }


                        //checkbos item updates 
                        if (model.educationlevel.Count > 0)
                            updatesearchsettingseducationlevel(model.educationlevel.ToList(), p, _unitOfWorkAsync);

                        if (model.lookingfor.Count > 0)
                            updatesearchsettingslookingfor(model.lookingfor.ToList(), p, _unitOfWorkAsync);

                        if (model.havekids.Count > 0)
                            updatesearchsettingshavekids(model.havekids.ToList(), p, _unitOfWorkAsync);

                        if (model.incomelevel.Count > 0)
                            updatesearchsettingsincomelevel(model.incomelevel.ToList(), p, _unitOfWorkAsync);

                        if (model.livingstituation.Count > 0)
                            updatesearchsettingslocation(model.location.ToList(), p, _unitOfWorkAsync);

                        if (model.location.Count > 0)
                            updatesearchsettingslivingsituation(model.livingstituation.ToList(), p, _unitOfWorkAsync);

                        if (model.maritalstatus.Count > 0)
                            updatesearchsettingsmaritalstatus(model.maritalstatus.ToList(), p, _unitOfWorkAsync);

                        if (model.profession.Count > 0)
                            updatesearchsettingsprofession(model.profession.ToList(), p, _unitOfWorkAsync);

                        if (model.wantkids.Count > 0)
                            updatesearchsettingswantskids(model.wantkids.ToList(), p, _unitOfWorkAsync);


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
        private void updatesearchsettingsgender(List<lu_gender> selectedgenders, searchsetting currentsearchsettings)
        {
            if (selectedgenders == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var gender in selectedgenders.Where(z=>z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Any(f => f.value == gender.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = gender.id; //add the current gender value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.gender;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == gender.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata showme
        private void updatesearchsettingsshowme(List<lu_showme> selectedshowmes, searchsetting currentsearchsettings)
        {
            if (selectedshowmes == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var showme in selectedshowmes.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings showme values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.showme).Any(f => f.value == showme.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = showme.id; //add the current showme value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.showme;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == showme.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }


        }     
        //profiledata sortby
        private void updatesearchsettingssortby(List<lu_sortby> selectedsortbys, searchsetting currentsearchsettings)
        {
            if (selectedsortbys == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var sortby in selectedsortbys.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings sortby values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sortby).Any(f => f.value == sortby.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = sortby.id; //add the current sortby value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.sortby;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == sortby.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }     
        //profiledata location
        private void updatesearchsettingslocation(List<searchsetting_location> selectedgenders, searchsetting currentsearchsettings)
        {
            if (selectedgenders == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var gender in selectedgenders.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Any(f => f.value == gender.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = gender.id; //add the current gender value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.gender;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == gender.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }     

        //END of Basic settings ///////////////////////

        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata ethnicity
        private void updatesearchsettingsethnicity(List<lu_ethnicity> selectedethnicitys, searchsetting currentsearchsettings)
        {
            if (selectedethnicitys == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var ethnicity in selectedethnicitys.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings ethnicity values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Any(f => f.value == ethnicity.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = ethnicity.id; //add the current ethnicity value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.ethnicity;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == ethnicity.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }
        //profiledata bodytypes
        private void updatesearchsettingsbodytypes(List<lu_bodytypes> selectedbodytypess, searchsetting currentsearchsettings)
        {
            if (selectedbodytypess == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var bodytypes in selectedbodytypess.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings bodytypes values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytypes).Any(f => f.value == bodytypes.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = bodytypes.id; //add the current bodytypes value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.bodytypes;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == bodytypes.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }


        }
        //profiledata eyecolor
        private void updatesearchsettingseyecolor(List<lu_eyecolor> selectedeyecolors, searchsetting currentsearchsettings)
        {
            if (selectedeyecolors == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var eyecolor in selectedeyecolors.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings eyecolor values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Any(f => f.value == eyecolor.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = eyecolor.id; //add the current eyecolor value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.eyecolor;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == eyecolor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }
        //profiledata haircolor
        private void updatesearchsettingshaircolor(List<lu_haircolor> selectedhaircolors, searchsetting currentsearchsettings)
        {
            if (selectedhaircolors == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var haircolor in selectedhaircolors.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings haircolor values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Any(f => f.value == haircolor.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = haircolor.id; //add the current haircolor value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.haircolor;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == haircolor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }
        //profiledata hotfeature
        private void updatesearchsettingshotfeature(List<lu_hotfeature> selectedhotfeatures, searchsetting currentsearchsettings)
        {
            if (selectedhotfeatures == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var hotfeature in selectedhotfeatures.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings hotfeature values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Any(f => f.value == hotfeature.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = hotfeature.id; //add the current hotfeature value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.hotfeature;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == hotfeature.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }        

        //End of apperarnce /////////////////////
        
        //Lifesatyle settings start //////////////////
        //profiledata educationlevel
        private void updatesearchsettingseducationlevel(List<lu_educationlevel> selectededucationlevels, searchsetting currentsearchsettings)
        {
            if (selectededucationlevels == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var educationlevel in selectededucationlevels.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings educationlevel values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.educationlevel).Any(f => f.value == educationlevel.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = educationlevel.id; //add the current educationlevel value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.educationlevel;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == educationlevel.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }
        //profiledata lookingfor
        private void updatesearchsettingslookingfor(List<lu_lookingfor> selectedlookingfors, searchsetting currentsearchsettings)
        {
            if (selectedlookingfors == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var lookingfor in selectedlookingfors.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings lookingfor values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.lookingfor).Any(f => f.value == lookingfor.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = lookingfor.id; //add the current lookingfor value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.lookingfor;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == lookingfor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }


        }
        //profiledata employmentstatus
        private void updatesearchsettingsemploymentstatus(List<lu_employmentstatus> selectedemploymentstatuss, searchsetting currentsearchsettings)
        {
            if (selectedemploymentstatuss == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var employmentstatus in selectedemploymentstatuss.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings employmentstatus values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.employmentstatus).Any(f => f.value == employmentstatus.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = employmentstatus.id; //add the current employmentstatus value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.employmentstatus;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == employmentstatus.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }
        //profiledata havekids
        private void updatesearchsettingshavekids(List<lu_havekids> selectedhavekidss, searchsetting currentsearchsettings)
        {
            if (selectedhavekidss == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var havekids in selectedhavekidss.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings havekids values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.havekids).Any(f => f.value == havekids.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = havekids.id; //add the current havekids value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.havekids;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == havekids.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata incomelevel
        private void updatesearchsettingsincomelevel(List<lu_incomelevel> selectedincomelevels, searchsetting currentsearchsettings)
        {
            if (selectedincomelevels == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var incomelevel in selectedincomelevels.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings incomelevel values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.incomelevel).Any(f => f.value == incomelevel.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = incomelevel.id; //add the current incomelevel value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.incomelevel;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == incomelevel.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata livingsituation
        private void updatesearchsettingslivingsituation(List<lu_livingsituation> selectedlivingsituations, searchsetting currentsearchsettings)
        {
            if (selectedlivingsituations == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var livingsituation in selectedlivingsituations.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings livingsituation values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.livingsituation).Any(f => f.value == livingsituation.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = livingsituation.id; //add the current livingsituation value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.livingsituation;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == livingsituation.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata maritalstatus
        private void updatesearchsettingsmaritalstatus(List<lu_maritalstatus> selectedmaritalstatuss, searchsetting currentsearchsettings)
        {
            if (selectedmaritalstatuss == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var maritalstatus in selectedmaritalstatuss.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings maritalstatus values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.maritalstatus).Any(f => f.value == maritalstatus.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = maritalstatus.id; //add the current maritalstatus value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.maritalstatus;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == maritalstatus.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata profession
        private void updatesearchsettingsprofession(List<lu_profession> selectedprofessions, searchsetting currentsearchsettings)
        {
            if (selectedprofessions == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var profession in selectedprofessions.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings profession values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.profession).Any(f => f.value == profession.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = profession.id; //add the current profession value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.profession;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == profession.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata wantskids
        private void updatesearchsettingswantskids(List<lu_wantskids> selectedwantskidss, searchsetting currentsearchsettings)
        {
            if (selectedwantskidss == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var wantskids in selectedwantskidss.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings wantskids values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.wantskids).Any(f => f.value == wantskids.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = wantskids.id; //add the current wantskids value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.wantskids;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == wantskids.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
     
        //End of lfitestyle settings

        //// Start of Character Search settings ////

        //profiledata diet
        private void updatesearchsettingsdiet(List<lu_diet> selecteddiets, searchsetting currentsearchsettings)
        {
            if (selecteddiets == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var diet in selecteddiets.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings diet values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.diet).Any(f => f.value == diet.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = diet.id; //add the current diet value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.diet;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == diet.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }        
        //profiledata humor
        private void updatesearchsettingshumor(List<lu_humor> selectedhumors, searchsetting currentsearchsettings)
        {
            if (selectedhumors == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var humor in selectedhumors.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings humor values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.humor).Any(f => f.value == humor.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = humor.id; //add the current humor value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.humor;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == humor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }        
        //profiledata hobby
        private void updatesearchsettingshobby(List<lu_hobby> selectedhobbys, searchsetting currentsearchsettings)
        {
            if (selectedhobbys == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var hobby in selectedhobbys.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings hobby values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hobby).Any(f => f.value == hobby.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = hobby.id; //add the current hobby value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.hobby;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == hobby.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }       
        //profiledata drinks
        private void updatesearchsettingsdrinks(List<lu_drinks> selecteddrinkss, searchsetting currentsearchsettings)
        {
            if (selecteddrinkss == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var drinks in selecteddrinkss.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings drinks values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.drinks).Any(f => f.value == drinks.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = drinks.id; //add the current drinks value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.drinks;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == drinks.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }        
        //profiledata exercise
        private void updatesearchsettingsexercise(List<lu_exercise> selectedexercises, searchsetting currentsearchsettings)
        {
            if (selectedexercises == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var exercise in selectedexercises.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings exercise values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.exercise).Any(f => f.value == exercise.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = exercise.id; //add the current exercise value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.exercise;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == exercise.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }
        }
        //profiledata smokes
        private void updatesearchsettingssmokes(List<lu_smokes> selectedsmokess, searchsetting currentsearchsettings)
        {
            if (selectedsmokess == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var smokes in selectedsmokess.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings smokes values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.smokes).Any(f => f.value == smokes.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = smokes.id; //add the current smokes value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.smokes;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == smokes.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }        
        //profiledata sign
        private void updatesearchsettingssign(List<lu_sign> selectedsigns, searchsetting currentsearchsettings)
        {
            if (selectedsigns == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var sign in selectedsigns.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings sign values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sign).Any(f => f.value == sign.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = sign.id; //add the current sign value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.sign;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == sign.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }        
        //profiledata politicalview
        private void updatesearchsettingspoliticalview(List<lu_politicalview> selectedpoliticalviews, searchsetting currentsearchsettings)
        {
            if (selectedpoliticalviews == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var politicalview in selectedpoliticalviews.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings politicalview values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.politicalview).Any(f => f.value == politicalview.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = politicalview.id; //add the current politicalview value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.politicalview;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == politicalview.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }       
        //profiledata religion
        private void updatesearchsettingsreligion(List<lu_religion> selectedreligions, searchsetting currentsearchsettings)
        {
            if (selectedreligions == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var religion in selectedreligions.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings religion values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religion).Any(f => f.value == religion.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = religion.id; //add the current religion value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.religion;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == religion.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }
        //profiledata religiousattendance
        private void updatesearchsettingsreligiousattendance(List<lu_religiousattendance> selectedreligiousattendances, searchsetting currentsearchsettings)
        {
            if (selectedreligiousattendances == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var religiousattendance in selectedreligiousattendances.Where(z => z.selected == true))
            {
                //new logic : if this item was selected and is not already in the search settings religiousattendance values add it 
                if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religiousattendance).Any(f => f.value == religiousattendance.id)))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsettingdetail();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.value = religiousattendance.id; //add the current religiousattendance value since its new.
                    temp.creationdate = DateTime.Now;
                    temp.searchsettingdetailtype_id = (int)searchsettingdetailtypeEnum.religiousattendance;
                    _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsettingdetail>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.value == religiousattendance.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }

        }

        ////End of Character Search settings

        #endregion

      
    }

}
