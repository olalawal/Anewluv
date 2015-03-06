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
                    model.mygenderid =  p.profilemetadata != null? p.profilemetadata.profiledatas.gender_id.GetValueOrDefault():1;
                    model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                    model.creationdate = p.creationdate == null ? (DateTime?)null : p.creationdate.GetValueOrDefault();                 
                    model.distancefromme=  p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    model.lastupdatedate = p.lastupdatedate == null ? DateTime.Now : p.lastupdatedate;
                    model.searchname = p.searchname == null ? "Default" : p.searchname;
                    model.searchrank = p.searchrank == null ? 1 : p.searchrank;
                    model.myperfectmatch = p.myperfectmatch == null ? true : p.myperfectmatch;
                    model.systemmatch = p.systemmatch == null ? false : p.systemmatch;
                    //test of map the list items to the generic listitem object in order to clean up the models so no iselected item on them
                    model.showmelist =  showmelist.ToList().Select(o => new listitem { id = o.id, description= o.description }).ToList();                  
                    model.genderlist = genderlist.ToList().Select(o => new lu_gender { id = o.id, description= o.description, selected = false }).ToList();
                    model.sortbylist = sortbylist.ToList().Select(o => new lu_sortbytype  {  id = o.id, description= o.description, selected = false }).ToList();
                    model.agelist = agelist;  //TO do have it use desction and IC as well instead of age object
                
                    //update the list with the items that are selected.
                    foreach (lu_showme showme in showmelist.Where(c => p.searchsetting_showme.Any(f => f.showme_id == c.id))) {
                       //update the value as checked here on the list
                       model.showmelist.First(d => d.id == showme.id).selected = true; 
                    }

                    //update the list with the items that are selected.
                    foreach (lu_gender gender in genderlist.Where(c => p.searchsetting_gender.Any(f => f.gender_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.showmelist.First(d => d.id == gender.id).selected = true;
                    }


                    //update the list with the items that are selected.
                    foreach (lu_sortbytype sortbytype in sortbylist.Where(c => p.searchsetting_sortbytype.Any(f => f.sortbytype_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.sortbylist.First(d => d.id == sortbytype.id).selected = true;
                    }



                    //Location does not match any list i think need to have this tweaked for now ignore
                    //full location since it includes the city
                    //for now UI only allows one but this code allows for many
                    foreach (var item in p.searchsetting_location)
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
                foreach (lu_ethnicity ethnicity in ethnicitylist.Where(c => p.searchsetting_ethnicity.Any(f => f.ethnicity_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.ethnicitylist.First(d => d.id == ethnicity.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_bodytype bodytype in bodytypelist.Where(c => p.searchsetting_bodytype.Any(f => f.bodytype_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.bodytypeslist.First(d => d.id == bodytype.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_eyecolor eyecolor in eyecolorlist.Where(c => p.searchsetting_eyecolor.Any(f => f.eyecolor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.eyecolorlist.First(d => d.id == eyecolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_haircolor haircolor in haircolorlist.Where(c => p.searchsetting_haircolor.Any(f => f.haircolor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.haircolorlist.First(d => d.id == haircolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_hotfeature hotfeature in hotfeaturelist.Where(c => p.searchsetting_hotfeature.Any(f => f.hotfeature_id == c.id)))
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
                foreach (lu_humor humor in humorlist.Where(c => p.searchsetting_humor.Any(f => f.humor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.humorlist.First(d => d.id == humor.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_diet diet in dietlist.Where(c => p.searchsetting_diet.Any(f => f.diet_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.dietlist.First(d => d.id == diet.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_hobby hobby in hobbylist.Where(c => p.searchsetting_hobby.Any(f => f.hobby_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.hobbylist.First(d => d.id == hobby.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_drinks drink in drinklist.Where(c => p.searchsetting_drink.Any(f => f.drink_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.drinkslist.First(d => d.id == drink.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_exercise exercise in exerciselist.Where(c => p.searchsetting_exercise.Any(f => f.exercise_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.exerciselist.First(d => d.id == exercise.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_smokes smokes in smokeslist.Where(c => p.searchsetting_smokes.Any(f => f.smoke_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.smokeslist.First(d => d.id == smokes.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_sign sign in signlist.Where(c => p.searchsetting_sign.Any(f => f.sign_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.signlist.First(d => d.id == sign.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_politicalview politicalview in politicalviewlist.Where(c => p.searchsetting_politicalview.Any(f => f.politicalview_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_politicalview politicalview in politicalviewlist.Where(c => p.searchsetting_politicalview.Any(f => f.politicalview_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_religiousattendance religiousattendance in religiousattendancelist.Where(c => p.searchsetting_religiousattendance.Any(f => f.religiousattendance_id == c.id)))
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
                foreach (lu_educationlevel educationlevel in educationlevellist.Where(c => p.searchsetting_educationlevel.Any(f => f.educationlevel_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.educationlevellist.First(d => d.id == educationlevel.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_lookingfor lookingfor in lookingforlist.Where(c => p.searchsetting_lookingfor.Any(f => f.lookingfor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.lookingforlist.First(d => d.id == lookingfor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_employmentstatus employmentstatus in employmentstatuslist.Where(c => p.searchsetting_employmentstatus.Any(f => f.employmentstatus_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.employmentstatuslist.First(d => d.id == employmentstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_incomelevel incomelevel in incomelevellist.Where(c => p.searchsetting_incomelevel.Any(f => f.incomelevel_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.incomelevellist.First(d => d.id == incomelevel.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_livingsituation livingsituation in livingsituationlist.Where(c => p.searchsetting_livingstituation.Any(f => f.livingsituation_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.livingsituationlist.First(d => d.id == livingsituation.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_maritalstatus maritialstatus in maritialstatuslist.Where(c => p.searchsetting_maritalstatus.Any(f => f.maritalstatus_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.maritalstatuslist.First(d => d.id == maritialstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_profession profession in professionlist.Where(c => p.searchsetting_profession.Any(f => f.profession_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.professionlist.First(d => d.id == profession.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_wantskids wantkids in wantkidslist.Where(c => p.searchsetting_wantkids.Any(f => f.wantskids_id == c.id)))
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
                        if (p.searchsetting_gender.Count > 0)
                            updatesearchsettingsgender(p.searchsetting_gender.ToList(), p, _unitOfWorkAsync);
                        if (p.searchsetting_showme.Count > 0)
                            updatesearchsettingssortby(p.searchsetting_sortbytype.ToList(), p, _unitOfWorkAsync);
                        if (p.searchsetting_showme.Count > 0)
                            updatesearchsettingsshowme(p.searchsetting_showme.ToList(), p, _unitOfWorkAsync);
                        if (p.searchsetting_location.Count > 0)
                            updatesearchsettingslocation(p.searchsetting_location.ToList(), p, _unitOfWorkAsync);


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
                    if (p.searchsetting_ethnicity.Count > 0)
                        updatesearchsettingsethnicity(p.searchsetting_ethnicity.ToList(), p, _unitOfWorkAsync);
                    if (p.searchsetting_bodytype.Count > 0)
                        updatesearchsettingsbodytypes(p.searchsetting_bodytype.ToList(), p, _unitOfWorkAsync);
                    if (p.searchsetting_eyecolor.Count > 0)
                        updatesearchsettingseyecolor(p.searchsetting_eyecolor.ToList(), p, _unitOfWorkAsync);
                    if (p.searchsetting_haircolor.Count > 0)
                        updatesearchsettingshaircolor(p.searchsetting_haircolor.ToList(), p, _unitOfWorkAsync);
                    if (p.searchsetting_hotfeature.Count > 0)
                        updatesearchsettingshotfeature(p.searchsetting_hotfeature.ToList(), p, _unitOfWorkAsync);

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
                        if (p.searchsetting_diet.Count > 0)
                            updatesearchsettingsgender(p.searchsetting_gender.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_humor.Count > 0)
                            updatesearchsettingssortby(p.searchsetting_sortbytype.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_hobby.Count > 0)
                            updatesearchsettingsshowme(p.searchsetting_showme.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_drink.Count > 0)
                            updatesearchsettingslocation(p.searchsetting_location.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_exercise.Count > 0)
                            updatesearchsettingsexercise(p.searchsetting_exercise.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_smokes.Count > 0)
                            updatesearchsettingssmokes(p.searchsetting_smokes.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_sign.Count > 0)
                            updatesearchsettingssign(p.searchsetting_sign.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_politicalview.Count > 0)
                            updatesearchsettingspoliticalview(p.searchsetting_politicalview.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_religion.Count > 0)
                            updatesearchsettingsreligion(p.searchsetting_religion.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_religiousattendance.Count > 0)
                            updatesearchsettingsreligiousattendance(p.searchsetting_religiousattendance.ToList(), p, _unitOfWorkAsync);




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
                        if (p.searchsetting_educationlevel.Count > 0)
                            updatesearchsettingseducationlevel(p.searchsetting_educationlevel.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_lookingfor.Count > 0)
                            updatesearchsettingslookingfor(p.searchsetting_lookingfor.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_havekids.Count > 0)
                            updatesearchsettingshavekids(p.searchsetting_havekids.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_incomelevel.Count > 0)
                            updatesearchsettingsincomelevel(p.searchsetting_incomelevel.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_livingstituation.Count > 0)
                            updatesearchsettingslocation(p.searchsetting_location.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_location.Count > 0)
                            updatesearchsettingslivingsituation(p.searchsetting_livingstituation.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_maritalstatus.Count > 0)
                            updatesearchsettingsmaritalstatus(p.searchsetting_maritalstatus.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_profession.Count > 0)
                            updatesearchsettingsprofession(p.searchsetting_profession.ToList(), p, _unitOfWorkAsync);

                        if (p.searchsetting_wantkids.Count > 0)
                            updatesearchsettingswantskids(p.searchsetting_wantkids.ToList(), p, _unitOfWorkAsync);


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
        private void updatesearchsettingsgender(List<searchsetting_gender> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var gender in _unitOfWorkAsync.Repository<searchsetting_gender>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_gender.Where(z => z.gender_id == gender.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_gender();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.gender_id = gender.id; //add the current gender value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_gender>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_gender>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.gender_id == gender.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_gender>().Delete(temp);
                }


            }



        }
        //profiledata showme
        private void updatesearchsettingsshowme(List<searchsetting_showme> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var showme in _unitOfWorkAsync.Repository<searchsetting_showme>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_showme.Where(z => z.showme_id == showme.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_showme();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.showme_id = showme.id; //add the current showme value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_showme>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_showme>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.showme_id == showme.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_showme>().Delete(temp);
                }


            }



        }     
        //profiledata sortby
        private void updatesearchsettingssortby(List<searchsetting_sortbytype> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var sortby in _unitOfWorkAsync.Repository<searchsetting_sortbytype>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_sortbytype.Where(z => z.sortbytype_id == sortby.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_sortbytype();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.sortbytype_id = sortby.id; //add the current sortby value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_sortbytype>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_sortbytype>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.sortbytype_id == sortby.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_sortbytype>().Delete(temp);
                }


            }



        }     
        //profiledata location
        private void updatesearchsettingslocation(List<searchsetting_location> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var location in _unitOfWorkAsync.Repository<searchsetting_location>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_location.Where(z => z.countryid == location.countryid & z.city == location.city & z.postalcode == location.postalcode).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_location();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.countryid = location.countryid; //add the current location value since its new.
                    temp.postalcode = location.postalcode;
                    temp.city = location.city;
                    _unitOfWorkAsync.Repository<searchsetting_location>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_location>().Queryable().Where(z => z.searchsetting_id == currentsearchsettings.id && z.countryid == location.countryid & z.city == location.city & z.postalcode == location.postalcode).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_location>().Delete(temp);
                }


            }



        }     

        //END of Basic settings ///////////////////////

        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata ethnicity
        private void updatesearchsettingsethnicity(List<searchsetting_ethnicity> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }
           
            foreach (var ethnicity in _unitOfWorkAsync.Repository<searchsetting_ethnicity>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_ethnicity.Where(z => z.ethnicity_id == ethnicity.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_ethnicity();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.ethnicity_id = ethnicity.id; //add the current ethnicity value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_ethnicity>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_ethnicity>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.ethnicity_id == ethnicity.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_ethnicity>().Delete(temp);
                }


            }



        }
        //profiledata bodytypes
        private void updatesearchsettingsbodytypes(List<searchsetting_bodytype> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var bodytypes in _unitOfWorkAsync.Repository<searchsetting_bodytype>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_bodytype.Where(z => z.bodytype_id == bodytypes.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_bodytype();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.bodytype_id = bodytypes.id; //add the current bodytypes value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_bodytype>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_bodytype>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.bodytype_id == bodytypes.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_bodytype>().Delete(temp);
                }


            }



        }
        //profiledata eyecolor
        private void updatesearchsettingseyecolor(List<searchsetting_eyecolor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var eyecolor in _unitOfWorkAsync.Repository<searchsetting_eyecolor>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_eyecolor.Where(z => z.eyecolor_id == eyecolor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_eyecolor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.eyecolor_id = eyecolor.id; //add the current eyecolor value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_eyecolor>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_eyecolor>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.eyecolor_id == eyecolor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_eyecolor>().Delete(temp);
                }


            }



        }
        //profiledata haircolor
        private void updatesearchsettingshaircolor(List<searchsetting_haircolor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var haircolor in _unitOfWorkAsync.Repository<searchsetting_haircolor>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_haircolor.Where(z => z.haircolor_id == haircolor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_haircolor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.haircolor_id = haircolor.id; //add the current haircolor value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_haircolor>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_haircolor>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.haircolor_id == haircolor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_haircolor>().Delete(temp);
                }


            }



        }
        //profiledata hotfeature
        private void updatesearchsettingshotfeature(List<searchsetting_hotfeature> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var hotfeature in _unitOfWorkAsync.Repository<searchsetting_hotfeature>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_hotfeature.Where(z => z.hotfeature_id == hotfeature.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_hotfeature();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.hotfeature_id = hotfeature.id; //add the current hotfeature value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_hotfeature>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_hotfeature>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.hotfeature_id == hotfeature.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_hotfeature>().Delete(temp);
                }


            }



        }        

        //End of apperarnce /////////////////////
        
        //Lifesatyle settings start //////////////////
        //profiledata educationlevel
        private void updatesearchsettingseducationlevel(List<searchsetting_educationlevel> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var educationlevel in _unitOfWorkAsync.Repository<searchsetting_educationlevel>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_educationlevel.Where(z => z.educationlevel_id == educationlevel.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_educationlevel();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.educationlevel_id = educationlevel.id; //add the current educationlevel value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_educationlevel>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_educationlevel>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.educationlevel_id == educationlevel.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_educationlevel>().Delete(temp);
                }


            }



        }
        //profiledata lookingfor
        private void updatesearchsettingslookingfor(List<searchsetting_lookingfor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var lookingfor in _unitOfWorkAsync.Repository<searchsetting_lookingfor>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_lookingfor.Where(z => z.lookingfor_id == lookingfor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_lookingfor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.lookingfor_id = lookingfor.id; //add the current lookingfor value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_lookingfor>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_lookingfor>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.lookingfor_id == lookingfor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_lookingfor>().Delete(temp);
                }


            }



        }
        //profiledata employmentstatus
        private void updatesearchsettingsemploymentstatus(List<searchsetting_employmentstatus> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var employmentstatus in _unitOfWorkAsync.Repository<searchsetting_employmentstatus>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_employmentstatus.Where(z => z.employmentstatus_id == employmentstatus.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_employmentstatus();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.employmentstatus_id = employmentstatus.id; //add the current employmentstatus value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_employmentstatus>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_employmentstatus>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.employmentstatus_id == employmentstatus.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_employmentstatus>().Delete(temp);
                }


            }



        }
        //profiledata havekids
        private void updatesearchsettingshavekids(List<searchsetting_havekids> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var havekids in _unitOfWorkAsync.Repository<searchsetting_havekids>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_havekids.Where(z => z.havekids_id == havekids.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_havekids();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.havekids_id = havekids.id; //add the current havekids value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_havekids>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_havekids>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.havekids_id == havekids.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_havekids>().Delete(temp);
                }


            }



        }
        //profiledata incomelevel
        private void updatesearchsettingsincomelevel(List<searchsetting_incomelevel> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var incomelevel in _unitOfWorkAsync.Repository<searchsetting_incomelevel>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_incomelevel.Where(z => z.incomelevel_id == incomelevel.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_incomelevel();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.incomelevel_id = incomelevel.id; //add the current incomelevel value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_incomelevel>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_incomelevel>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.incomelevel_id == incomelevel.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_incomelevel>().Delete(temp);
                }


            }



        }
        //profiledata livingsituation
        private void updatesearchsettingslivingsituation(List<searchsetting_livingstituation> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var livingsituation in _unitOfWorkAsync.Repository<searchsetting_livingstituation>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_livingstituation.Where(z => z.livingsituation_id == livingsituation.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_livingstituation();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.livingsituation_id = livingsituation.id; //add the current livingsituation value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_livingstituation>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_livingstituation>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.livingsituation_id == livingsituation.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_livingstituation>().Delete(temp);
                }


            }



        }
        //profiledata maritalstatus
        private void updatesearchsettingsmaritalstatus(List<searchsetting_maritalstatus> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var maritalstatus in _unitOfWorkAsync.Repository<searchsetting_maritalstatus>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_maritalstatus.Where(z => z.maritalstatus_id == maritalstatus.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_maritalstatus();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.maritalstatus_id = maritalstatus.id; //add the current maritalstatus value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_maritalstatus>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_maritalstatus>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.maritalstatus_id == maritalstatus.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_maritalstatus>().Delete(temp);
                }


            }



        }
        //profiledata profession
        private void updatesearchsettingsprofession(List<searchsetting_profession> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var profession in _unitOfWorkAsync.Repository<searchsetting_profession>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_profession.Where(z => z.profession_id == profession.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_profession();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.profession_id = profession.id; //add the current profession value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_profession>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_profession>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.profession_id == profession.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_profession>().Delete(temp);
                }


            }



        }
        //profiledata wantskids
        private void updatesearchsettingswantskids(List<searchsetting_wantkids> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var wantskids in _unitOfWorkAsync.Repository<searchsetting_wantkids>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_wantkids.Where(z => z.wantskids_id == wantskids.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_wantkids();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.wantskids_id = wantskids.id; //add the current wantskids value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_wantkids>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_wantkids>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.wantskids_id == wantskids.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_wantkids>().Delete(temp);
                }


            }



        }
     
        //End of lfitestyle settings

        //// Start of Character Search settings ////

        //profiledata diet
        private void updatesearchsettingsdiet(List<searchsetting_diet> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var diet in _unitOfWorkAsync.Repository<searchsetting_diet>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_diet.Where(z => z.diet_id == diet.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_diet();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.diet_id = diet.id; //add the current diet value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_diet>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_diet>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.diet_id == diet.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_diet>().Delete(temp);
                }


            }



        }        
        //profiledata humor
        private void updatesearchsettingshumor(List<searchsetting_humor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var humor in _unitOfWorkAsync.Repository<searchsetting_humor>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_humor.Where(z => z.humor_id == humor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_humor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.humor_id = humor.id; //add the current humor value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_humor>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_humor>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.humor_id == humor.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_humor>().Delete(temp);
                }


            }



        }        
        //profiledata hobby
        private void updatesearchsettingshobby(List<searchsetting_hobby> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var hobby in _unitOfWorkAsync.Repository<searchsetting_hobby>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_hobby.Where(z => z.hobby_id == hobby.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_hobby();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.hobby_id = hobby.id; //add the current hobby value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_hobby>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_hobby>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.hobby_id == hobby.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_hobby>().Delete(temp);
                }


            }



        }       
        //profiledata drinks
        private void updatesearchsettingsdrinks(List<searchsetting_drink> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var drinks in _unitOfWorkAsync.Repository<searchsetting_drink>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_drink.Where(z => z.drink_id == drinks.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_drink();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.drink_id = drinks.id; //add the current drinks value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_drink>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_drink>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.drink_id == drinks.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_drink>().Delete(temp);
                }


            }



        }        
        //profiledata exercise
        private void updatesearchsettingsexercise(List<searchsetting_exercise> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var exercise in _unitOfWorkAsync.Repository<searchsetting_exercise>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_exercise.Where(z => z.exercise_id == exercise.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_exercise();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.exercise_id = exercise.id; //add the current exercise value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_exercise>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_exercise>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.exercise_id == exercise.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_exercise>().Delete(temp);
                }


            }



        }
        //profiledata smokes
        private void updatesearchsettingssmokes(List<searchsetting_smokes> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var smokes in _unitOfWorkAsync.Repository<searchsetting_smokes>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_smokes.Where(z => z.smoke_id == smokes.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_smokes();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.smoke_id = smokes.id; //add the current smokes value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_smokes>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_smokes>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.smoke_id == smokes.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_smokes>().Delete(temp);
                }


            }



        }        
        //profiledata sign
        private void updatesearchsettingssign(List<searchsetting_sign> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var sign in _unitOfWorkAsync.Repository<searchsetting_sign>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_sign.Where(z => z.sign_id == sign.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_sign();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.sign_id = sign.id; //add the current sign value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_sign>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_sign>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.sign_id == sign.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_sign>().Delete(temp);
                }


            }



        }        
        //profiledata politicalview
        private void updatesearchsettingspoliticalview(List<searchsetting_politicalview> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var politicalview in _unitOfWorkAsync.Repository<searchsetting_politicalview>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_politicalview.Where(z => z.politicalview_id == politicalview.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_politicalview();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.politicalview_id = politicalview.id; //add the current politicalview value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_politicalview>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_politicalview>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.politicalview_id == politicalview.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_politicalview>().Delete(temp);
                }


            }



        }       
        //profiledata religion
        private void updatesearchsettingsreligion(List<searchsetting_religion> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var religion in _unitOfWorkAsync.Repository<searchsetting_religion>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_religion.Where(z => z.religion_id == religion.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_religion();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.religion_id = religion.id; //add the current religion value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_religion>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_religion>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.religion_id == religion.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_religion>().Delete(temp);
                }


            }



        }
        //profiledata religiousattendance
        private void updatesearchsettingsreligiousattendance(List<searchsetting_religiousattendance> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var religiousattendance in _unitOfWorkAsync.Repository<searchsetting_religiousattendance>().Queryable().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_religiousattendance.Where(z => z.religiousattendance_id == religiousattendance.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_religiousattendance();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.religiousattendance_id = religiousattendance.id; //add the current religiousattendance value since its new.
                    _unitOfWorkAsync.Repository<searchsetting_religiousattendance>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_religiousattendance>().Queryable().Where(p => p.searchsetting_id == currentsearchsettings.id && p.religiousattendance_id == religiousattendance.id).First();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsetting_religiousattendance>().Delete(temp);
                }


            }



        }

        ////End of Character Search settings

        #endregion

      
    }

}
