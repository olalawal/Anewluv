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

        public async Task<List<searchsetting>> getallsearchsettingsbyprofileid(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        var p = db.GetRepository<searchsetting>().Find().Where(z => z.profile_id == searchmodel.profileid).ToList();
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

        public async Task<SearchSettingsViewModel> getsearchsettingsviewmodel(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                      //  searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == model.searchid || z.profile_id == model.profileid && z.searchname == model.searchname).FirstOrDefault();



                        searchsetting p = filtersearchsettings(searchmodel, db);
                      
                        searchmodel.basicsearchsettings = this.getbasicsearchsettings(p, db);
                        searchmodel.lifestylesearchsettings = this.getlifestylesearchsettings(p, db);
                        searchmodel.appearancesearchsettings = this.getappearancesearchsettings(p, db);
                        searchmodel.charactersearchsettings = this.getcharactersearchsettings(p, db);


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

        public async Task<BasicSearchSettingsModel> getbasicsearchsettings(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                //if (searchmodel.profileid == 0) return null;

                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        searchsetting p = filtersearchsettings(searchmodel, db);
                        return this.getbasicsearchsettings(p, db);
                       
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

        public async Task<AppearanceSearchSettingsModel> getappearancesearchsettings(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        searchsetting p = filtersearchsettings(searchmodel, db);
                        return this.getappearancesearchsettings(p, db);

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

        public async Task<CharacterSearchSettingsModel> getcharactersearchsettings(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        searchsetting p = filtersearchsettings(searchmodel, db);
                        return this.getcharactersearchsettings(p, db);

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

        public async Task<LifeStyleSearchSettingsModel> getlifestylesearchsettings(SearchSettingsViewModel searchmodel)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {

                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        searchsetting p = filtersearchsettings(searchmodel, db);
                        return this.getlifestylesearchsettings(p, db);

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

        #region "private get methods for reuses"

        //generic filtering function we can reuse
        private searchsetting filtersearchsettings (SearchSettingsViewModel searchmodel,IUnitOfWork db)
        {

            try
            {
                //This query assumes that one search is always called default and cannot be deleted dont like that
                List<searchsetting> allsearchsettings = new List<searchsetting>();
                searchsetting p = new searchsetting();

                allsearchsettings = db.GetRepository<searchsetting>().Find().Where
                (z => (searchmodel.searchid != 0 && z.id == searchmodel.searchid) ||
                (searchmodel.profileid != 0 && (z.profile_id == searchmodel.profileid))).ToList();

                if (allsearchsettings.Count() > 0 & searchmodel.searchname != null )//|searchmodel.searchname != ""  )
                {
                    p = allsearchsettings.Where(z => z.searchname == searchmodel.searchname).FirstOrDefault();
                }
                else if (allsearchsettings.Count() > 0)
                {
                    p = allsearchsettings.OrderByDescending(z => z.creationdate).FirstOrDefault();  //get the first one thats probbaly the default.
                }

                return p;
            }
            catch (Exception ex)
            { throw ex; }
        }

       private BasicSearchSettingsModel getbasicsearchsettings(searchsetting p,IUnitOfWork db)
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
                    var showmelist = CachingFactory.SharedObjectHelper.getshowmelist(db);
                    var genderlist = CachingFactory.SharedObjectHelper.getgenderlist(db);
                    var sortbylist = CachingFactory.SharedObjectHelper.getsortbytypelist(db);

                    model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                    model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                    model.creationdate = p.creationdate == null ? (DateTime?)null : p.creationdate.GetValueOrDefault();                 
                    model.distancefromme=  p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    model.lastupdatedate = p.lastupdatedate == null ? DateTime.Now : p.lastupdatedate;
                    model.searchname = p.searchname == null ? "Default" : p.searchname;
                    model.searchrank = p.searchrank == null ? 1 : p.searchrank;
                    model.myperfectmatch = p.myperfectmatch == null ? true : p.myperfectmatch;
                    model.systemmatch = p.systemmatch == null ? false : p.systemmatch;
                   
                    model.showmelist =  showmelist.ToList().Select(o => new lu_showme { id = o.id, description= o.description, isselected = false }).ToList();                  
                    model.genderlist = genderlist.ToList().Select(o => new lu_gender { id = o.id, description= o.description, isselected = false }).ToList();
                    model.sortbylist = sortbylist.ToList().Select(o => new lu_sortbytype  {  id = o.id, description= o.description, isselected = false }).ToList();
            
                
                    //update the list with the items that are selected.
                    foreach (lu_showme showme in showmelist.Where(c => p.searchsetting_showme.Any(f => f.showme_id == c.id))) {
                       //update the value as checked here on the list
                       model.showmelist.First(d => d.id == showme.id).isselected = true; 
                    }

                    //update the list with the items that are selected.
                    foreach (lu_gender gender in genderlist.Where(c => p.searchsetting_gender.Any(f => f.gender_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.showmelist.First(d => d.id == gender.id).isselected = true;
                    }


                    //update the list with the items that are selected.
                    foreach (lu_sortbytype sortbytype in sortbylist.Where(c => p.searchsetting_sortbytype.Any(f => f.sortbytype_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.sortbylist.First(d => d.id == sortbytype.id).isselected = true;
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
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private AppearanceSearchSettingsModel getappearancesearchsettings(searchsetting p, IUnitOfWork db)
        {
            try
            {
                //searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();

                //populate values here ok ?
                //if (p == null) return null;


                //get all the showmes so we can deterimine which are checked and which are not
                var ethnicitylist = CachingFactory.SharedObjectHelper.getethnicitylist(db);
                var bodytypelist = CachingFactory.SharedObjectHelper.getbodytypelist(db);
                var eyecolorlist = CachingFactory.SharedObjectHelper.geteyecolorlist(db);
                var haircolorlist = CachingFactory.SharedObjectHelper.gethaircolorlist(db);
                var hotfeaturelist = CachingFactory.SharedObjectHelper.gethotfeaturelist(db);
             
                var showmelist = CachingFactory.SharedObjectHelper.getshowmelist(db);

                model.heightmax = p.heightmax == null ? 210 : p.heightmax;
                model.heightmin = p.heightmin == null ? 100 : p.heightmin;

                model.ethnicitylist = ethnicitylist.ToList().Select(o => new lu_ethnicity { id = o.id, description = o.description, isselected = false }).ToList();
                model.bodytypeslist = bodytypelist.ToList().Select(o => new lu_bodytype { id = o.id, description = o.description, isselected = false }).ToList();
                model.eyecolorlist = eyecolorlist.ToList().Select(o => new lu_eyecolor { id = o.id, description = o.description, isselected = false }).ToList();
                model.haircolorlist = haircolorlist.ToList().Select(o => new lu_haircolor { id = o.id, description = o.description, isselected = false }).ToList();
                model.hotfeaturelist = hotfeaturelist.ToList().Select(o => new lu_hotfeature { id = o.id, description = o.description, isselected = false }).ToList(); 

               

                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = db.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                //update the list with the items that are selected.
                foreach (lu_ethnicity ethnicity in ethnicitylist.Where(c => p.searchsetting_ethnicity.Any(f => f.ethnicity_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.ethnicitylist.First(d => d.id == ethnicity.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_bodytype bodytype in bodytypelist.Where(c => p.searchsetting_bodytype.Any(f => f.bodytype_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.bodytypeslist.First(d => d.id == bodytype.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_eyecolor eyecolor in eyecolorlist.Where(c => p.searchsetting_eyecolor.Any(f => f.eyecolor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.eyecolorlist.First(d => d.id == eyecolor.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_haircolor haircolor in haircolorlist.Where(c => p.searchsetting_haircolor.Any(f => f.haircolor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.haircolorlist.First(d => d.id == haircolor.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_hotfeature hotfeature in hotfeaturelist.Where(c => p.searchsetting_hotfeature.Any(f => f.hotfeature_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.hotfeaturelist.First(d => d.id == hotfeature.id).isselected = true;
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
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private CharacterSearchSettingsModel getcharactersearchsettings(searchsetting p, IUnitOfWork db)
        {
            try
            {
                //searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();

                //populate values here ok ?
               // if (p == null) return null;

                var humorlist = CachingFactory.SharedObjectHelper.gethumorlist(db);
                var dietlist = CachingFactory.SharedObjectHelper.getdietlist(db);
                var hobbylist = CachingFactory.SharedObjectHelper.gethobbylist(db);
                var drinklist = CachingFactory.SharedObjectHelper.getdrinkslist(db);
                var exerciselist = CachingFactory.SharedObjectHelper.getexerciselist(db);
                var smokeslist = CachingFactory.SharedObjectHelper.getsmokeslist(db);
                var signlist = CachingFactory.SharedObjectHelper.getsignlist(db);
                var politicalviewlist = CachingFactory.SharedObjectHelper.getpoliticalviewlist(db);
                var religionlist = CachingFactory.SharedObjectHelper.getreligionlist(db);
                var religiousattendancelist = CachingFactory.SharedObjectHelper.getreligiousattendancelist(db);

                model.humorlist = humorlist.ToList().Select(o => new lu_humor { id = o.id, description = o.description, isselected = false }).ToList();
                model.dietlist = dietlist.ToList().Select(o => new lu_diet { id = o.id, description = o.description, isselected = false }).ToList();
                model.hobbylist = hobbylist.ToList().Select(o => new lu_hobby { id = o.id, description = o.description, isselected = false }).ToList();
                model.drinkslist = drinklist.ToList().Select(o => new lu_drinks { id = o.id, description = o.description, isselected = false }).ToList();
                model.exerciselist = exerciselist.ToList().Select(o => new lu_exercise { id = o.id, description = o.description, isselected = false }).ToList();
                model.smokeslist = smokeslist.ToList().Select(o => new lu_smokes { id = o.id, description = o.description, isselected = false }).ToList();
                model.signlist = signlist.ToList().Select(o => new lu_sign { id = o.id, description = o.description, isselected = false }).ToList();
                model.politicalviewlist = politicalviewlist.ToList().Select(o => new lu_politicalview { id = o.id, description = o.description, isselected = false }).ToList();
                model.religionlist = religionlist.ToList().Select(o => new lu_religion { id = o.id, description = o.description, isselected = false }).ToList();
                model.religiousattendancelist = religiousattendancelist.ToList().Select(o => new lu_religiousattendance { id = o.id, description = o.description, isselected = false }).ToList();

              
                //update the list with the items that are selected.
                foreach (lu_humor humor in humorlist.Where(c => p.searchsetting_humor.Any(f => f.humor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.humorlist.First(d => d.id == humor.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_diet diet in dietlist.Where(c => p.searchsetting_diet.Any(f => f.diet_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.dietlist.First(d => d.id == diet.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_hobby hobby in hobbylist.Where(c => p.searchsetting_hobby.Any(f => f.hobby_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.hobbylist.First(d => d.id == hobby.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_drinks drink in drinklist.Where(c => p.searchsetting_drink.Any(f => f.drink_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.drinkslist.First(d => d.id == drink.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_exercise exercise in exerciselist.Where(c => p.searchsetting_exercise.Any(f => f.exercise_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.exerciselist.First(d => d.id == exercise.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_smokes smokes in smokeslist.Where(c => p.searchsetting_smokes.Any(f => f.smoke_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.smokeslist.First(d => d.id == smokes.id).isselected = true;
                }


                //update the list with the items that are selected.
                foreach (lu_sign sign in signlist.Where(c => p.searchsetting_sign.Any(f => f.sign_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.signlist.First(d => d.id == sign.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_politicalview politicalview in politicalviewlist.Where(c => p.searchsetting_politicalview.Any(f => f.politicalview_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_politicalview politicalview in politicalviewlist.Where(c => p.searchsetting_politicalview.Any(f => f.politicalview_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_religiousattendance religiousattendance in religiousattendancelist.Where(c => p.searchsetting_religiousattendance.Any(f => f.religiousattendance_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.religiousattendancelist.First(d => d.id == religiousattendance.id).isselected = true;
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
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        private LifeStyleSearchSettingsModel getlifestylesearchsettings(searchsetting p, IUnitOfWork db)
        {
            try
            {
               // searchsetting p = db.GetRepository<searchsetting>().Find().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();

                //populate values here ok ?
               // if (p == null) return null;

                var educationlevellist = CachingFactory.SharedObjectHelper.geteducationlevellist(db);
                var lookingforlist = CachingFactory.SharedObjectHelper.getlookingforlist(db);
                var employmentstatuslist = CachingFactory.SharedObjectHelper.getemploymentstatuslist(db);
                var havekidslist = CachingFactory.SharedObjectHelper.gethavekidslist(db);
                var incomelevellist = CachingFactory.SharedObjectHelper.getincomelevellist(db);
                var livingsituationlist = CachingFactory.SharedObjectHelper.getlivingsituationlist(db);
                var maritialstatuslist = CachingFactory.SharedObjectHelper.getmaritalstatuslist(db); 
                var professionlist = CachingFactory.SharedObjectHelper.getprofessionlist(db);
                var wantkidslist = CachingFactory.SharedObjectHelper.getwantskidslist(db);


                model.educationlevellist = educationlevellist.ToList().Select(o => new lu_educationlevel { id = o.id, description = o.description, isselected = false }).ToList();
                   model. lookingforlist = lookingforlist.ToList().Select(o => new lu_lookingfor { id = o.id, description = o.description, isselected = false }).ToList();
                   model. employmentstatuslist = employmentstatuslist.ToList().Select(o => new lu_employmentstatus { id = o.id, description = o.description, isselected = false }).ToList();
                   model. havekidslist = havekidslist.ToList().Select(o => new lu_havekids { id = o.id, description = o.description, isselected = false }).ToList();
                   model. incomelevellist = incomelevellist.ToList().Select(o => new lu_incomelevel { id = o.id, description = o.description, isselected = false }).ToList();
                   model. livingsituationlist = livingsituationlist.ToList().Select(o => new lu_livingsituation { id = o.id, description = o.description, isselected = false }).ToList();
                   model.maritalstatuslist = maritialstatuslist.ToList().Select(o => new lu_maritalstatus { id = o.id, description = o.description, isselected = false }).ToList();
                   model. professionlist = professionlist.ToList().Select(o => new lu_profession { id = o.id, description = o.description, isselected = false }).ToList();
                   model.wantskidslist = wantkidslist.ToList().Select(o => new lu_wantskids { id = o.id, description = o.description, isselected = false }).ToList();


                //update the list with the items that are selected.
                foreach (lu_educationlevel educationlevel in educationlevellist.Where(c => p.searchsetting_educationlevel.Any(f => f.educationlevel_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.educationlevellist.First(d => d.id == educationlevel.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_lookingfor lookingfor in lookingforlist.Where(c => p.searchsetting_lookingfor.Any(f => f.lookingfor_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.lookingforlist.First(d => d.id == lookingfor.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_employmentstatus employmentstatus in employmentstatuslist.Where(c => p.searchsetting_employmentstatus.Any(f => f.employmentstatus_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.employmentstatuslist.First(d => d.id == employmentstatus.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_incomelevel incomelevel in incomelevellist.Where(c => p.searchsetting_incomelevel.Any(f => f.incomelevel_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.incomelevellist.First(d => d.id == incomelevel.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_livingsituation livingsituation in livingsituationlist.Where(c => p.searchsetting_livingstituation.Any(f => f.livingsituation_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.livingsituationlist.First(d => d.id == livingsituation.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_maritalstatus maritialstatus in maritialstatuslist.Where(c => p.searchsetting_maritalstatus.Any(f => f.maritalstatus_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.maritalstatuslist.First(d => d.id == maritialstatus.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_profession profession in professionlist.Where(c => p.searchsetting_profession.Any(f => f.profession_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.professionlist.First(d => d.id == profession.id).isselected = true;
                }

                //update the list with the items that are selected.
                foreach (lu_wantskids wantkids in wantkidslist.Where(c => p.searchsetting_wantkids.Any(f => f.wantskids_id == c.id)))
                {
                    //update the value as checked here on the list
                    model.wantskidslist.First(d => d.id == wantkids.id).isselected = true;

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
    }

}
