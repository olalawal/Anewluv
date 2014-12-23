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
using Nmedia.DataAccess.Interfaces;
using System.Threading.Tasks;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.Domain.Data.log;
using Anewluv.Caching;


namespace Anewluv.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "searchsService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SearchEditService : ISearchEditService 
    {


      
        IUnitOfWork _unitOfWork;
       // private Logging logger;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public SearchEditService(IUnitOfWork unitOfWork)
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

        public async Task<searchsetting> getsearchsettings(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid && z.searchname == model.searchname).FirstOrDefault();
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

        public async Task<List<searchsetting>> getallsearchsettingsbyprofileid(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        var p = db.GetRepository<searchsetting>().Find().Where(z => z.profile_id == model.profileid).ToList();
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

        public async Task<SearchSettingsViewModel> getsearchsettingsviewmodel(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid && z.searchname == model.searchname).FirstOrDefault();

                        model.basicsearchsettings = this.getbasicsearchsettings(model,db);
                        model.lifestylesearchsettings = this.getlifestylesearchsettings(model, db);
                        model.appearancesearchsettings = this.getappearancesearchsettings(model,db);
                        model.charactersearchsettings = this.getcharactersearchsettings(model, db);


                        //TO DO add rest of searches.

                        return model;
                        

                        
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

        public async Task< BasicSearchSettingsModel> getbasicsearchsettings(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {

                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        return this.getbasicsearchsettings(model, db);
                       
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

        public async Task<AppearanceSearchSettingsModel> getappearancesearchsettings(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return this.getappearancesearchsettings(model, db);

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

        public async Task<CharacterSearchSettingsModel> getcharactersearchsettings(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return this.getcharactersearchsettings(model, db);

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

        public async Task<LifeStyleSearchSettingsModel> getlifestylesearchsettings(SearchSettingsViewModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return this.getlifestylesearchsettings(model, db);

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


        #region "edit methods for search settings"

            //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
            public async Task<AnewluvMessages> updatebasicsearchsettings(BasicSearchSettingsModel model)
            {
                _unitOfWork.DisableProxyCreation = false;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
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
                                updatesearchsettingsgender(p.searchsetting_gender.ToList(), p, db);
                            if (p.searchsetting_showme.Count > 0)
                                updatesearchsettingssortby(p.searchsetting_sortbytype.ToList(), p, db);
                            if (p.searchsetting_showme.Count > 0)
                                updatesearchsettingsshowme(p.searchsetting_showme.ToList(), p, db);
                            if (p.searchsetting_location.Count > 0)
                                updatesearchsettingslocation(p.searchsetting_location.ToList(), p, db);


                             db.Update(p);
                             int i = db.Commit();

                            return messages;


                        });
                        return await task.ConfigureAwait(false);

                    }                       
                        //TOD DO
                        //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                        //return newmodel;                    
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
            //TO DO add validation and pass back via messages 

            public async Task<AnewluvMessages> updateappearancesearchsettings(AppearanceSearchSettingsModel model)
            {
                _unitOfWork.DisableProxyCreation = false;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
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
                                updatesearchsettingsethnicity(p.searchsetting_ethnicity.ToList(), p, db);
                            if (p.searchsetting_bodytype.Count > 0)
                                updatesearchsettingsbodytypes(p.searchsetting_bodytype.ToList(), p, db);
                            if (p.searchsetting_eyecolor.Count > 0)
                                updatesearchsettingseyecolor(p.searchsetting_eyecolor.ToList(), p, db);
                            if (p.searchsetting_haircolor.Count > 0)
                                updatesearchsettingshaircolor(p.searchsetting_haircolor.ToList(), p, db);
                            if (p.searchsetting_hotfeature.Count > 0)
                                updatesearchsettingshotfeature(p.searchsetting_hotfeature.ToList(), p, db);

                            db.Update(p);
                            int i = db.Commit();

                            return messages;


                        });
                        return await task.ConfigureAwait(false);

                    }
                    //TOD DO
                    //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                    //return newmodel;                    
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

            public async Task<AnewluvMessages> updatecharactersearchsettings(CharacterSearchSettingsModel model)
            {
                _unitOfWork.DisableProxyCreation = false;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                            //create a new messages object
                            if (p == null)
                            {
                                messages.errormessages.Add("There is no search with this parameters");
                                return messages;
                            }


                            //checkbos item updates 
                            if (p.searchsetting_diet.Count > 0)
                                updatesearchsettingsgender(p.searchsetting_gender.ToList(), p, db);

                            if (p.searchsetting_humor.Count > 0)
                                updatesearchsettingssortby(p.searchsetting_sortbytype.ToList(), p, db);

                            if (p.searchsetting_hobby.Count > 0)
                                updatesearchsettingsshowme(p.searchsetting_showme.ToList(), p, db);

                            if (p.searchsetting_drink.Count > 0)
                                updatesearchsettingslocation(p.searchsetting_location.ToList(), p, db);

                            if (p.searchsetting_exercise.Count > 0)
                                updatesearchsettingsexercise(p.searchsetting_exercise.ToList(), p, db);

                            if (p.searchsetting_smokes.Count > 0)
                                updatesearchsettingssmokes(p.searchsetting_smokes.ToList(), p, db);

                            if (p.searchsetting_sign.Count > 0)
                                updatesearchsettingssign(p.searchsetting_sign.ToList(), p, db);

                            if (p.searchsetting_politicalview.Count > 0)
                                updatesearchsettingspoliticalview(p.searchsetting_politicalview.ToList(), p, db);

                            if (p.searchsetting_religion.Count > 0)
                                updatesearchsettingsreligion(p.searchsetting_religion.ToList(), p, db);

                            if (p.searchsetting_religiousattendance.Count > 0)
                                updatesearchsettingsreligiousattendance(p.searchsetting_religiousattendance.ToList(), p, db);




                            int i = db.Commit();


                            return messages;


                        });
                        return await task.ConfigureAwait(false);

                    }
                    //TOD DO
                    //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                    //return newmodel;                    
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

            public async Task<AnewluvMessages> updatbasicsearchsettings(LifeStyleSearchSettingsModel model)
            {
                _unitOfWork.DisableProxyCreation = false;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            AnewluvMessages messages = new AnewluvMessages();
                            searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                            //create a new messages object
                            if (p == null)
                            {
                                messages.errormessages.Add("There is no search with this parameters");
                                return messages;
                            }


                            //checkbos item updates 
                            if (p.searchsetting_educationlevel.Count > 0)
                                updatesearchsettingseducationlevel(p.searchsetting_educationlevel.ToList(), p, db);

                            if (p.searchsetting_lookingfor.Count > 0)
                                updatesearchsettingslookingfor(p.searchsetting_lookingfor.ToList(), p, db);

                            if (p.searchsetting_havekids.Count > 0)
                                updatesearchsettingshavekids(p.searchsetting_havekids.ToList(), p, db);

                            if (p.searchsetting_incomelevel.Count > 0)
                                updatesearchsettingsincomelevel(p.searchsetting_incomelevel.ToList(), p, db);

                            if (p.searchsetting_livingstituation.Count > 0)
                                updatesearchsettingslocation(p.searchsetting_location.ToList(), p, db);

                            if (p.searchsetting_location.Count > 0)
                                updatesearchsettingslivingsituation(p.searchsetting_livingstituation.ToList(), p, db);

                            if (p.searchsetting_maritalstatus.Count > 0)
                                updatesearchsettingsmaritalstatus(p.searchsetting_maritalstatus.ToList(), p, db);

                            if (p.searchsetting_profession.Count > 0)
                                updatesearchsettingsprofession(p.searchsetting_profession.ToList(), p, db);

                             if (p.searchsetting_wantkids.Count > 0)
                                 updatesearchsettingswantskids(p.searchsetting_wantkids.ToList(), p, db);


                             int i = db.Commit();


                            return messages;


                        });
                        return await task.ConfigureAwait(false);

                    }
                    //TOD DO
                    //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                    //return newmodel;                    
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

        #region "private get methods for reuses"


       private BasicSearchSettingsModel getbasicsearchsettings(SearchSettingsViewModel searchmodel,IUnitOfWork db)
        {
            try
            {
                searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                BasicSearchSettingsModel model = new BasicSearchSettingsModel();

                //populate values here ok ?
                if (p != null) return null;

                    // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                    //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();
                    //populate ages select list here I guess
                    //TODO get from app fabric
                    // SharedRepository sharedrepository = new SharedRepository();
                    //Ages = sharedrepository.AgesSelectList;
                    
                    model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                    model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                                                      
                    model.distancefromme=  p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    model.lastupdatedate = p.lastupdatedate;
                    model.searchname = p.searchname;
                    model.searchrank = p.searchrank;
                    model.myperfectmatch = p.myperfectmatch;
                    model.systemmatch = p.systemmatch;


                    //get all the showmes so we can deterimine which are checked and which are not
                    var showmelist =  CachingFactory.SharedObjectHelper.getshowmelist(db);

                    foreach (var item in showmelist)  //p.searchsetting_showme)
                    {
                        foreach (var item2 in p.searchsetting_showme)
                        {
                            if (item.id == item2.showme_id)
                            {
                                item.isselected = true;
                                model.showmelist.Add(item);
                            }
                            model.showmelist.Add(item);
                        }
                    }

                    foreach (var item in p.searchsetting_sortbytype)
                    {
                        model.sortbylist.Add(item.lu_sortbytype);
                    }

                    foreach (var item in p.searchsetting_gender)
                    {
                        model.genderlist.Add(item.lu_gender);
                    }

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
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private AppearanceSearchSettingsModel getappearancesearchsettings(SearchSettingsViewModel searchmodel, IUnitOfWork db)
        {
            try
            {
                searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();

                //populate values here ok ?
                if (p == null) return null;


                model.heightmax = p.heightmax == null ? 210 : p.heightmax;
                model.heightmin = p.heightmin == null ? 100 : p.heightmin;
              

                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = db.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                foreach (var item in p.searchsetting_ethnicity)
                {
                    model.ethnicitylist.Add(item.lu_ethnicity);
                }

                foreach (var item in p.searchsetting_bodytype)
                {
                    model.bodytypeslist.Add(item.lu_bodytype);
                }

                foreach (var item in p.searchsetting_eyecolor)
                {
                    model.eyecolorlist.Add(item.lu_eyecolor);
                }

                foreach (var item in p.searchsetting_haircolor)
                {
                    model.haircolorlist.Add(item.lu_haircolor);
                }

                foreach (var item in p.searchsetting_hotfeature)
                {
                    model.hotfeaturelist.Add(item.lu_hotfeature);
                }


                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private CharacterSearchSettingsModel getcharactersearchsettings(SearchSettingsViewModel searchmodel, IUnitOfWork db)
        {
            try
            {
                searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();

                //populate values here ok ?
                if (p == null) return null;



                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = db.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
               
                foreach (var item in p.searchsetting_humor)
                {
                    model.humorlist.Add(item.lu_humor);
                }

                foreach (var item in p.searchsetting_diet)
                {
                    model.dietlist.Add(item.lu_diet);
                }

                foreach (var item in p.searchsetting_hobby)
                {
                    model.hobbylist.Add(item.lu_hobby);
                }

                foreach (var item in p.searchsetting_drink)
                {
                    model.drinkslist.Add(item.lu_drinks);
                }

                foreach (var item in p.searchsetting_exercise)
                {
                    model.exerciselist.Add(item.lu_exercise);
                }

                foreach (var item in p.searchsetting_smokes)
                {
                    model.smokeslist.Add(item.lu_smokes);
                }

                foreach (var item in p.searchsetting_sign)
                {
                    model.signlist.Add(item.lu_sign);
                }

                foreach (var item in p.searchsetting_politicalview)
                {
                    model.politicalviewlist.Add(item.lu_politicalview);
                }

                foreach (var item in p.searchsetting_religion)
                {
                    model.religionlist.Add(item.lu_religion);
                }

                foreach (var item in p.searchsetting_religiousattendance)
                {
                    model.religiousattendancelist.Add(item.lu_religiousattendance);
                }



                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private LifeStyleSearchSettingsModel getlifestylesearchsettings(SearchSettingsViewModel searchmodel, IUnitOfWork db)
        {
            try
            {
                searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();

                //populate values here ok ?
                if (p == null) return null;
                
               
                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = db.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                foreach (var item in p.searchsetting_educationlevel)
                {
                    model.educationlevellist.Add(item.lu_educationlevel);
                }

                foreach (var item in p.searchsetting_lookingfor)
                {
                    model.lookingforlist.Add(item.lu_lookingfor);
                }

                foreach (var item in p.searchsetting_employmentstatus)
                {
                    model.employmentstatuslist.Add(item.lu_employmentstatus);
                }

                foreach (var item in p.searchsetting_havekids)
                {
                    model.havekidslist.Add(item.lu_havekids);
                }

                foreach (var item in p.searchsetting_incomelevel)
                {
                    model.incomelevellist.Add(item.lu_incomelevel);
                }

                foreach (var item in p.searchsetting_livingstituation)
                {
                    model.livingsituationlist.Add(item.lu_livingsituation);
                }

                foreach (var item in p.searchsetting_maritalstatus)
                {
                    model. maritalstatuslist.Add(item.lu_maritalstatus);
                }

                foreach (var item in p.searchsetting_profession)
                {
                    model.professionlist.Add(item.lu_profession);
                }

                foreach (var item in p.searchsetting_wantkids)
                {
                    model.wantskidslist.Add(item.lu_wantskids);
                }



                return model;

            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.EditMemberService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(searchmodel.profileid));
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

#endregion

        #region "PRIVATE Checkbox Update Functions for seaerch settings many to many"


        //Basic Checkbox settings updates
        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata gender
        private void updatesearchsettingsgender(List<searchsetting_gender> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var gender in db.GetRepository<searchsetting_gender>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_gender.Where(z => z.gender_id == gender.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_gender();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.gender_id = gender.id; //add the current gender value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_gender>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.gender_id == gender.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata showme
        private void updatesearchsettingsshowme(List<searchsetting_showme> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var showme in db.GetRepository<searchsetting_showme>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_showme.Where(z => z.showme_id == showme.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_showme();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.showme_id = showme.id; //add the current showme value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_showme>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.showme_id == showme.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }     
        //profiledata sortby
        private void updatesearchsettingssortby(List<searchsetting_sortbytype> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var sortby in db.GetRepository<searchsetting_sortbytype>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_sortbytype.Where(z => z.sortbytype_id == sortby.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_sortbytype();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.sortbytype_id = sortby.id; //add the current sortby value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_sortbytype>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.sortbytype_id == sortby.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }     
        //profiledata location
        private void updatesearchsettingslocation(List<searchsetting_location> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var location in db.GetRepository<searchsetting_location>().Find().ToList())
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
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_location>().Find().Where(z => z.searchsetting_id == currentsearchsettings.id && z.countryid == location.countryid & z.city == location.city & z.postalcode == location.postalcode).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }     

        //END of Basic settings ///////////////////////

        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata ethnicity
        private void updatesearchsettingsethnicity(List<searchsetting_ethnicity> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }
           
            foreach (var ethnicity in db.GetRepository<searchsetting_ethnicity>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_ethnicity.Where(z => z.ethnicity_id == ethnicity.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_ethnicity();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.ethnicity_id = ethnicity.id; //add the current ethnicity value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_ethnicity>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.ethnicity_id == ethnicity.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata bodytypes
        private void updatesearchsettingsbodytypes(List<searchsetting_bodytype> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var bodytypes in db.GetRepository<searchsetting_bodytype>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_bodytype.Where(z => z.bodytype_id == bodytypes.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_bodytype();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.bodytype_id = bodytypes.id; //add the current bodytypes value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_bodytype>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.bodytype_id == bodytypes.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata eyecolor
        private void updatesearchsettingseyecolor(List<searchsetting_eyecolor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var eyecolor in db.GetRepository<searchsetting_eyecolor>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_eyecolor.Where(z => z.eyecolor_id == eyecolor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_eyecolor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.eyecolor_id = eyecolor.id; //add the current eyecolor value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_eyecolor>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.eyecolor_id == eyecolor.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata haircolor
        private void updatesearchsettingshaircolor(List<searchsetting_haircolor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var haircolor in db.GetRepository<searchsetting_haircolor>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_haircolor.Where(z => z.haircolor_id == haircolor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_haircolor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.haircolor_id = haircolor.id; //add the current haircolor value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_haircolor>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.haircolor_id == haircolor.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata hotfeature
        private void updatesearchsettingshotfeature(List<searchsetting_hotfeature> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var hotfeature in db.GetRepository<searchsetting_hotfeature>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_hotfeature.Where(z => z.hotfeature_id == hotfeature.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_hotfeature();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.hotfeature_id = hotfeature.id; //add the current hotfeature value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_hotfeature>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.hotfeature_id == hotfeature.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        

        //End of apperarnce /////////////////////
        
        //Lifesatyle settings start //////////////////
        //profiledata educationlevel
        private void updatesearchsettingseducationlevel(List<searchsetting_educationlevel> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var educationlevel in db.GetRepository<searchsetting_educationlevel>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_educationlevel.Where(z => z.educationlevel_id == educationlevel.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_educationlevel();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.educationlevel_id = educationlevel.id; //add the current educationlevel value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_educationlevel>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.educationlevel_id == educationlevel.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata lookingfor
        private void updatesearchsettingslookingfor(List<searchsetting_lookingfor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var lookingfor in db.GetRepository<searchsetting_lookingfor>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_lookingfor.Where(z => z.lookingfor_id == lookingfor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_lookingfor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.lookingfor_id = lookingfor.id; //add the current lookingfor value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_lookingfor>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.lookingfor_id == lookingfor.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata employmentstatus
        private void updatesearchsettingsemploymentstatus(List<searchsetting_employmentstatus> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var employmentstatus in db.GetRepository<searchsetting_employmentstatus>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_employmentstatus.Where(z => z.employmentstatus_id == employmentstatus.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_employmentstatus();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.employmentstatus_id = employmentstatus.id; //add the current employmentstatus value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_employmentstatus>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.employmentstatus_id == employmentstatus.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata havekids
        private void updatesearchsettingshavekids(List<searchsetting_havekids> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var havekids in db.GetRepository<searchsetting_havekids>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_havekids.Where(z => z.havekids_id == havekids.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_havekids();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.havekids_id = havekids.id; //add the current havekids value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_havekids>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.havekids_id == havekids.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata incomelevel
        private void updatesearchsettingsincomelevel(List<searchsetting_incomelevel> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var incomelevel in db.GetRepository<searchsetting_incomelevel>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_incomelevel.Where(z => z.incomelevel_id == incomelevel.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_incomelevel();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.incomelevel_id = incomelevel.id; //add the current incomelevel value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_incomelevel>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.incomelevel_id == incomelevel.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata livingsituation
        private void updatesearchsettingslivingsituation(List<searchsetting_livingstituation> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var livingsituation in db.GetRepository<searchsetting_livingstituation>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_livingstituation.Where(z => z.livingsituation_id == livingsituation.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_livingstituation();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.livingsituation_id = livingsituation.id; //add the current livingsituation value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_livingstituation>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.livingsituation_id == livingsituation.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata maritalstatus
        private void updatesearchsettingsmaritalstatus(List<searchsetting_maritalstatus> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var maritalstatus in db.GetRepository<searchsetting_maritalstatus>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_maritalstatus.Where(z => z.maritalstatus_id == maritalstatus.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_maritalstatus();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.maritalstatus_id = maritalstatus.id; //add the current maritalstatus value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_maritalstatus>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.maritalstatus_id == maritalstatus.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata profession
        private void updatesearchsettingsprofession(List<searchsetting_profession> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var profession in db.GetRepository<searchsetting_profession>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_profession.Where(z => z.profession_id == profession.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_profession();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.profession_id = profession.id; //add the current profession value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_profession>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.profession_id == profession.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata wantskids
        private void updatesearchsettingswantskids(List<searchsetting_wantkids> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var wantskids in db.GetRepository<searchsetting_wantkids>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_wantkids.Where(z => z.wantskids_id == wantskids.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_wantkids();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.wantskids_id = wantskids.id; //add the current wantskids value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_wantkids>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.wantskids_id == wantskids.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
     
        //End of lfitestyle settings

        //// Start of Character Search settings ////

        //profiledata diet
        private void updatesearchsettingsdiet(List<searchsetting_diet> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var diet in db.GetRepository<searchsetting_diet>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_diet.Where(z => z.diet_id == diet.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_diet();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.diet_id = diet.id; //add the current diet value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_diet>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.diet_id == diet.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        
        //profiledata humor
        private void updatesearchsettingshumor(List<searchsetting_humor> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var humor in db.GetRepository<searchsetting_humor>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_humor.Where(z => z.humor_id == humor.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_humor();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.humor_id = humor.id; //add the current humor value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_humor>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.humor_id == humor.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        
        //profiledata hobby
        private void updatesearchsettingshobby(List<searchsetting_hobby> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var hobby in db.GetRepository<searchsetting_hobby>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_hobby.Where(z => z.hobby_id == hobby.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_hobby();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.hobby_id = hobby.id; //add the current hobby value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_hobby>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.hobby_id == hobby.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }       
        //profiledata drinks
        private void updatesearchsettingsdrinks(List<searchsetting_drink> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var drinks in db.GetRepository<searchsetting_drink>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_drink.Where(z => z.drink_id == drinks.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_drink();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.drink_id = drinks.id; //add the current drinks value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_drink>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.drink_id == drinks.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        
        //profiledata exercise
        private void updatesearchsettingsexercise(List<searchsetting_exercise> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var exercise in db.GetRepository<searchsetting_exercise>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_exercise.Where(z => z.exercise_id == exercise.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_exercise();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.exercise_id = exercise.id; //add the current exercise value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_exercise>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.exercise_id == exercise.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata smokes
        private void updatesearchsettingssmokes(List<searchsetting_smokes> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var smokes in db.GetRepository<searchsetting_smokes>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_smokes.Where(z => z.smoke_id == smokes.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_smokes();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.smoke_id = smokes.id; //add the current smokes value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_smokes>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.smoke_id == smokes.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        
        //profiledata sign
        private void updatesearchsettingssign(List<searchsetting_sign> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var sign in db.GetRepository<searchsetting_sign>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_sign.Where(z => z.sign_id == sign.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_sign();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.sign_id = sign.id; //add the current sign value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_sign>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.sign_id == sign.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }        
        //profiledata politicalview
        private void updatesearchsettingspoliticalview(List<searchsetting_politicalview> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var politicalview in db.GetRepository<searchsetting_politicalview>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_politicalview.Where(z => z.politicalview_id == politicalview.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_politicalview();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.politicalview_id = politicalview.id; //add the current politicalview value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_politicalview>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.politicalview_id == politicalview.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }       
        //profiledata religion
        private void updatesearchsettingsreligion(List<searchsetting_religion> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var religion in db.GetRepository<searchsetting_religion>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_religion.Where(z => z.religion_id == religion.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_religion();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.religion_id = religion.id; //add the current religion value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_religion>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.religion_id == religion.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }
        //profiledata religiousattendance
        private void updatesearchsettingsreligiousattendance(List<searchsetting_religiousattendance> slectedethnicities, searchsetting currentsearchsettings, IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                return;
            }

            foreach (var religiousattendance in db.GetRepository<searchsetting_religiousattendance>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentsearchsettings.searchsetting_religiousattendance.Where(z => z.religiousattendance_id == religiousattendance.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_religiousattendance();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.religiousattendance_id = religiousattendance.id; //add the current religiousattendance value since its new.
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<searchsetting_religiousattendance>().Find().Where(p => p.searchsetting_id == currentsearchsettings.id && p.religiousattendance_id == religiousattendance.id).First();
                    if (temp != null)
                        db.Remove(temp);
                }


            }



        }

        ////End of Character Search settings

        #endregion


    }

}
