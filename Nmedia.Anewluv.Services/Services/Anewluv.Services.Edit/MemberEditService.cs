﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;





using System.Web;
using System.Net;


using System.ServiceModel.Activation;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using Anewluv.Services.Contracts;
using Anewluv.Caching;
using System.Threading.Tasks;
using Repository.Pattern.UnitOfWork;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.DependencyInjection;
using Nmedia.Infrastructure.DTOs;
using Anewluv.Caching.RedisCaching;
using Nmedia.Infrastructure;
using Repository.Pattern.Infrastructure;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Anewluv.Domain.Data.ViewModels;

//TO DO cache all the gets in redis and update when items are saved to keep it in sync

namespace Anewluv.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberEditService : IMemberEditService  
    {


        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        // private Logging logger;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberEditService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork)
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

        #region "profile visisiblity settings update here"
                    
        public async Task<bool> updateprofilevisibilitysettings(visiblitysetting model)
        {
            var task = Task.Factory.StartNew(() =>
            {

                if (model.id != null)
                {

                    //Impement on member service ?
                    // datingservice.updatememberVisiblitySetting(model);


                    return true;
                }
                return false;
              });
              return await task.ConfigureAwait(false);
        }
        #endregion


        #region "Methods to GET current edit profile settings for a user"

        // constructor
        public async Task<BasicSettingsViewModel> getbasicsettingsmodel(EditProfileModel editprofilemodel)
        {
           
      
         {
             try
             {
                   var task = Task.Factory.StartNew(() =>
                    {
                      


                         var p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid });

                         BasicSettingsViewModel viewmodel = new BasicSettingsViewModel();

                         BasicSettingsModel model = new BasicSettingsModel();


                         var showmelist = RedisCacheFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);
                         var genderlist = RedisCacheFactory.SharedObjectHelper.getgenderlist(_unitOfWorkAsync);
                         var sortbylist = RedisCacheFactory.SharedObjectHelper.getsortbytypelist(_unitOfWorkAsync);
                         var agelist = RedisCacheFactory.SharedObjectHelper.getagelist();

                         //populate values here ok ?
                         if (p != null)


                        // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                        //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                        // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();

                        //populate ages select list here I guess
                        //TODO get from app fabric
                        // SharedRepository sharedrepository = new SharedRepository();
                        //Ages = sharedrepository.AgesSelectList;

                          model.birthdate = p.profiledata.birthdate; //== null ? null :  p.profiledata.lu_birthdate;
                         //  model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                        
                         model.countryid = p.profiledata.countryid == null ? null : p.profiledata.countryid;
                         model.city = p.profiledata.city == null ? null : p.profiledata.city;
                         model.postalcode = p.profiledata.postalcode == null ? null : p.profiledata.postalcode;
                         model.aboutme = p.profiledata.aboutme == null ? null : p.profiledata.aboutme;
                         model.phonenumber = p.profiledata.phone == null ? null : p.profiledata.phone;
                         model.catchyintroline = p.profiledata.mycatchyintroLine;
                         model.aboutme = p.profiledata.aboutme;
                        //set default values
                        // model.genderlist. (genderlist); 
                        //new List<listitem>(genderlist);
                    
                         model.genderlist = Extensions.getDeepCopy<List<listitem>>(genderlist);                    

                         //update the value as checked here on the list i.e select the body type in the list
                         model.genderlist.First(d => d.id == p.profiledata.gender_id).selected = true;

                         viewmodel.basicsettings = model;


                         //handle search settings if we are doing full edit
                         if (editprofilemodel.isfullediting == true)
                         {

                             BasicSearchSettingsModel SearchViewModel = new BasicSearchSettingsModel();
                             SearchViewModel.showmelist = showmelist;
                             SearchViewModel.genderlist = genderlist;                        
                             SearchViewModel.sortbylist = sortbylist;
                             SearchViewModel.agelist = agelist;                            
                             searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id ,searchname="myperfectmatch"}, _unitOfWorkAsync);
                             //empty search setting handling
                            
               
                             SearchViewModel = searchsettingsextentions.getbasicsearchsettings(SearchViewModel, s, _unitOfWorkAsync);
                             //add the value to viewmodel
                             viewmodel.basicsearchsettings = SearchViewModel;
                         }
                      

                         return viewmodel;
                    });
                   return await task.ConfigureAwait(false);

             }
             catch (Exception ex)
             {

                    using (var logger = new  Logging(applicationEnum.EditMemberService ))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                    }   
                   
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }
            

         
        }
        //Using a contstructor populate the current values I suppose
        //The actual values will bind to viewmodel I think
         public async Task<AppearanceSettingsViewModel> getappearancesettingsmodel(EditProfileModel editprofilemodel)
         {

            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid });

                        AppearanceSettingsViewModel viewmodel = new AppearanceSettingsViewModel();
                        //the model for bodytype profile edit
                        AppearanceSettingsModel model = new AppearanceSettingsModel();

                        //12-26-2014 olawal added this to allow for multiple checkbox selections and checked etc
                        var ethnicitylist = RedisCacheFactory.SharedObjectHelper.getethnicitylist(_unitOfWorkAsync);
                        var bodytypelist = RedisCacheFactory.SharedObjectHelper.getbodytypelist(_unitOfWorkAsync);
                        var eyecolorlist = RedisCacheFactory.SharedObjectHelper.geteyecolorlist(_unitOfWorkAsync);
                        var haircolorlist = RedisCacheFactory.SharedObjectHelper.gethaircolorlist(_unitOfWorkAsync);
                        var hotfeaturelist = RedisCacheFactory.SharedObjectHelper.gethotfeaturelist(_unitOfWorkAsync);
                        var metricheightlist = RedisCacheFactory.SharedObjectHelper.getmetricheightlist();


                        model.height =  p.profiledata.height == null ? (int?)null : Convert.ToInt32(p.profiledata.height.Value);

                        model.bodytypelist = Extensions.getDeepCopy<List<listitem>>(bodytypelist); 
                        model.eyecolorlist = Extensions.getDeepCopy<List<listitem>>(eyecolorlist); 
                        model.haircolorlist = Extensions.getDeepCopy<List<listitem>>(haircolorlist); 
                        model.ethnicitylist = Extensions.getDeepCopy<List<listitem>>(ethnicitylist); 
                        model.hotfeaturelist = Extensions.getDeepCopy<List<listitem>>(hotfeaturelist);
                        model.metricheightlist = Extensions.getDeepCopy<List<metricheight>>(metricheightlist);  //added list of metric heights so it can be used in the drop down



                        model.metricheightlist.First(d => d.heightindex == model.height.ToString()).selected = true;

                       //update the value as checked here on the list i.e select the body type in the list
                        model.bodytypelist.First(d => d.id ==  p.profiledata.bodytype_id).selected = true;
                       
                        //update the value as checked here on the list
                        model.haircolorlist.First(d => d.id == p.profiledata.haircolor_id).selected = true;                      

                        //adds the single selected list of eyecolors i.e exculsive list                       
                        //update the value as checked here on the list
                        model.eyecolorlist.First(d => d.id == p.profiledata.eyecolor_id).selected = true;
                       

                        foreach (listitem ethnicity in ethnicitylist.Where(c => p.profilemetadata.profiledata_ethnicity.Any(f => f.ethnicty_id == c.id)))
                        {
                            //update the value as checked here on the list
                            model.ethnicitylist.First(d => d.id == ethnicity.id).selected = true;
                        }
                      

                        //update the list with the items that are selected.
                        foreach (listitem hotfeature in hotfeaturelist.Where(c => p.profilemetadata.profiledata_hotfeature.Any(f => f.hotfeature_id == c.id)))
                        {
                            //update the value as checked here on the list
                            model.hotfeaturelist.First(d => d.id == hotfeature.id).selected = true;
                        }
                        //add the value to the viewmodel
                        viewmodel.appearancesettings = model;

                        //handle search settings if we are doing full edit
                        if (editprofilemodel.isfullediting  == true)
                        {
                          
                            AppearanceSearchSettingsModel SearchViewModel = new AppearanceSearchSettingsModel();
                            SearchViewModel.bodytypelist = (bodytypelist);
                            SearchViewModel.eyecolorlist = (eyecolorlist);
                            SearchViewModel.haircolorlist = (haircolorlist);
                            SearchViewModel.ethnicitylist = (ethnicitylist);
                            SearchViewModel.hotfeaturelist = (hotfeaturelist);
                            SearchViewModel.metricheightlist =(metricheightlist); 
                            searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                            //empty search setting handling
                           
               
                            SearchViewModel = searchsettingsextentions.getappearancesearchsettings(SearchViewModel, s, _unitOfWorkAsync);
                            //add the value to viewmodel
                            viewmodel.appearancesearchsettings = SearchViewModel;
                        }
                      
                      
                        return viewmodel;
                    });
                      return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.EditMemberService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }
                    
         

        }

        //Using a contstructor populate the current values I suppose
        //The actual values will bind to viewmodel I think
        public async Task<CharacterSettingsViewModel> getcharactersettingsmodel(EditProfileModel editprofilemodel)
        {
         
            {
                try
                {
                      var task = Task.Factory.StartNew(() =>
                    {

                    profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid});

                    CharacterSettingsViewModel viewmodel = new CharacterSettingsViewModel();  //return model

                    CharacterSettingsModel model = new CharacterSettingsModel();

                    var humorlist = RedisCacheFactory.SharedObjectHelper.gethumorlist(_unitOfWorkAsync);
                    var dietlist = RedisCacheFactory.SharedObjectHelper.getdietlist(_unitOfWorkAsync);                  
                    var drinkslist = RedisCacheFactory.SharedObjectHelper.getdrinkslist(_unitOfWorkAsync);
                    var exerciselist = RedisCacheFactory.SharedObjectHelper.getexerciselist(_unitOfWorkAsync);
                    var smokeslist = RedisCacheFactory.SharedObjectHelper.getsmokeslist(_unitOfWorkAsync);
                    var signlist = RedisCacheFactory.SharedObjectHelper.getsignlist(_unitOfWorkAsync);
                    var politicalviewlist = RedisCacheFactory.SharedObjectHelper.getpoliticalviewlist(_unitOfWorkAsync);
                    var religionlist = RedisCacheFactory.SharedObjectHelper.getreligionlist(_unitOfWorkAsync);
                    var religiousattendancelist = RedisCacheFactory.SharedObjectHelper.getreligiousattendancelist(_unitOfWorkAsync);
                    //non exculsive
                    var hobbylist = RedisCacheFactory.SharedObjectHelper.gethobbylist(_unitOfWorkAsync);

                    //set default values for edit profile                
                    model.humorlist =  Extensions.getDeepCopy<List<listitem>>(humorlist); ;
                    model.dietlist =  Extensions.getDeepCopy<List<listitem>>(dietlist); ;
                    model.drinkslist =  Extensions.getDeepCopy<List<listitem>>(drinkslist).ToList(); ;
                    model.exerciselist =  Extensions.getDeepCopy<List<listitem>>(exerciselist); ;
                    model.smokeslist =  Extensions.getDeepCopy<List<listitem>>(smokeslist); ;
                    model.signlist =  Extensions.getDeepCopy<List<listitem>>(signlist); ;
                    model.politicalviewlist =  Extensions.getDeepCopy<List<listitem>>(politicalviewlist); ;
                    model.religionlist =  Extensions.getDeepCopy<List<listitem>>(religionlist); ;
                    model.religiousattendancelist =  Extensions.getDeepCopy<List<listitem>>(religiousattendancelist); ;
                    model.hobbylist =  Extensions.getDeepCopy<List<listitem>>(hobbylist); ;

                     //setup exculsive lists first (i.e only one value is selected)
                   if (p.profiledata.humor_id !=null)  model.humorlist.First(d => d.id == p.profiledata.humor_id).selected = true;
                   if (p.profiledata.diet_id != null) model.dietlist.First(d => d.id == p.profiledata.diet_id).selected = true;
                   if (p.profiledata.drinking_id != null) model.drinkslist.First(d => d.id == p.profiledata.drinking_id).selected = true;
                   if (p.profiledata.exercise_id != null) model.exerciselist.First(d => d.id == p.profiledata.exercise_id).selected = true;
                   if (p.profiledata.smoking_id != null) model.smokeslist.First(d => d.id == p.profiledata.smoking_id).selected = true;
                   if (p.profiledata.sign_id != null) model.signlist.First(d => d.id == p.profiledata.sign_id).selected = true;
                   if (p.profiledata.politicalview_id != null) model.politicalviewlist.First(d => d.id == p.profiledata.politicalview_id).selected = true;
                   if (p.profiledata.religion_id != null) model.religionlist.First(d => d.id == p.profiledata.religion_id).selected = true;
                   if (p.profiledata.religiousattendance_id != null) model.religiousattendancelist.First(d => d.id == p.profiledata.religiousattendance_id).selected = true;

                    //update the list with the items that are selected.
                    foreach (listitem hobby in hobbylist.Where(c => p.profilemetadata.profiledata_hobby.Any(f => f.hobby_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.hobbylist.First(d => d.id == hobby.id).selected = true;
                    }

                        //save va;ue
                    viewmodel.charactersettings = model;


                    if (editprofilemodel.isfullediting == true)
                    {

                        CharacterSearchSettingsModel SearchViewModel = new CharacterSearchSettingsModel();
                       //set default values for search settings
                        //set default values for edit profile                
                        SearchViewModel.humorlist = (humorlist); ;
                        SearchViewModel.dietlist = (dietlist); ;
                        SearchViewModel.drinkslist = (drinkslist); ;
                        SearchViewModel.exerciselist = (exerciselist); ;
                        SearchViewModel.smokeslist = (smokeslist); ;
                        SearchViewModel.signlist = (signlist); ;
                        SearchViewModel.politicalviewlist = (politicalviewlist); ;
                        SearchViewModel.religionlist = (religionlist); ;
                        SearchViewModel.religiousattendancelist = (religiousattendancelist); ;
                        SearchViewModel.hobbylist = (hobbylist); ;

                        searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                        //empty search setting handling
                       
               
                        SearchViewModel = searchsettingsextentions.getcharactersearchsettings(SearchViewModel, s, _unitOfWorkAsync);
                        //add the value to viewmodel
                        viewmodel.charactersearchsettings = SearchViewModel;
                    }

                  
                    return viewmodel;
                    });
                      return await task.ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.EditMemberService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }




        }

        //populate the enities
        public async Task<LifeStyleSettingsViewModel> getlifestylesettingsmodel(EditProfileModel editprofilemodel)
        {

            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        LifeStyleSettingsModel model = new LifeStyleSettingsModel();

                        LifeStyleSettingsViewModel viewmodel = new LifeStyleSettingsViewModel();

                    profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid});

                    var educationlevellist = RedisCacheFactory.SharedObjectHelper.geteducationlevellist(_unitOfWorkAsync);                   
                    var employmentstatuslist = RedisCacheFactory.SharedObjectHelper.getemploymentstatuslist(_unitOfWorkAsync);
                    var havekidslist = RedisCacheFactory.SharedObjectHelper.gethavekidslist(_unitOfWorkAsync);
                    var incomelevellist = RedisCacheFactory.SharedObjectHelper.getincomelevellist(_unitOfWorkAsync);
                    var livingsituationlist = RedisCacheFactory.SharedObjectHelper.getlivingsituationlist(_unitOfWorkAsync);
                    var maritalstatuslist = RedisCacheFactory.SharedObjectHelper.getmaritalstatuslist(_unitOfWorkAsync);
                    var professionlist = RedisCacheFactory.SharedObjectHelper.getprofessionlist(_unitOfWorkAsync);
                    var wantskidslist = RedisCacheFactory.SharedObjectHelper.getwantskidslist(_unitOfWorkAsync);
                        //multiple seclections allowed
                    var lookingforlist = RedisCacheFactory.SharedObjectHelper.getlookingforlist(_unitOfWorkAsync);

                        //set emply list values
                    model.educationlevellist =  Extensions.getDeepCopy<List<listitem>>(educationlevellist); 
                    model.employmentstatuslist =  Extensions.getDeepCopy<List<listitem>>(employmentstatuslist); 
                    model.havekidslist =  Extensions.getDeepCopy<List<listitem>>(havekidslist); 
                    model.incomelevellist =  Extensions.getDeepCopy<List<listitem>>(incomelevellist); 
                    model.livingsituationlist =  Extensions.getDeepCopy<List<listitem>>(livingsituationlist); 
                    model.maritalstatuslist =  Extensions.getDeepCopy<List<listitem>>(maritalstatuslist); 
                    model.professionlist =  Extensions.getDeepCopy<List<listitem>>(professionlist); 
                    model.wantskidslist =  Extensions.getDeepCopy<List<listitem>>(wantskidslist);
                    model.lookingforlist = Extensions.getDeepCopy<List<listitem>>(lookingforlist);
                        
                        //set the true values
                    if (p.profiledata.educationlevel_id != null )model.educationlevellist.First(d => d.id == p.profiledata.educationlevel_id).selected = true;
                    if (p.profiledata.employmentstatus_id != null) model.employmentstatuslist.First(d => d.id == p.profiledata.employmentstatus_id.GetValueOrDefault()).selected = true;
                    if (p.profiledata.kidstatus_id != null) model.havekidslist.First(d => d.id == p.profiledata.kidstatus_id.GetValueOrDefault()).selected = true;
                    if (p.profiledata.incomelevel_id != null) model.incomelevellist.First(d => d.id == p.profiledata.incomelevel_id).selected = true;
                    if (p.profiledata.livingsituation_id != null) model.livingsituationlist.First(d => d.id == p.profiledata.livingsituation_id.GetValueOrDefault()).selected = true;
                    if (p.profiledata.maritalstatus_id != null) model.maritalstatuslist.First(d => d.id == p.profiledata.maritalstatus_id.GetValueOrDefault()).selected = true;
                    if (p.profiledata.profession_id != null) model.professionlist.First(d => d.id == p.profiledata.profession_id.GetValueOrDefault()).selected = true;
                    if (p.profiledata.wantsKidstatus_id != null) model.wantskidslist.First(d => d.id == p.profiledata.wantsKidstatus_id.GetValueOrDefault()).selected = true;


                    //update the list with the items that are selected.
                    foreach (listitem lookingfor in lookingforlist.Where(c => p.profilemetadata.profiledata_lookingfor.Any(f => f.lookingfor_id == c.id)))
                    {
                        //update the value as checked here on the list
                        model.lookingforlist.First(d => d.id == lookingfor.id).selected = true;
                    }
                        //store the updated values
                    viewmodel.lifestylesettings = model;
                        
                    if (editprofilemodel.isfullediting == true)
                    {

                        LifeStyleSearchSettingsModel SearchViewModel = new LifeStyleSearchSettingsModel();
                        //set default values for search settings
                        SearchViewModel.lookingforlist = (lookingforlist);
                        SearchViewModel.educationlevellist =  (educationlevellist);
                        SearchViewModel.employmentstatuslist = (employmentstatuslist);
                        SearchViewModel.havekidslist =  (havekidslist);
                        SearchViewModel.incomelevellist = (incomelevellist);
                        SearchViewModel.livingsituationlist =  (livingsituationlist);
                        SearchViewModel.maritalstatuslist = (maritalstatuslist);
                        SearchViewModel.professionlist = (professionlist);
                        SearchViewModel.wantskidslist =  (wantskidslist); 
                        
                        searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                        //empty search setting handling
                       
               
                        SearchViewModel = searchsettingsextentions.getlifestylesearchsettings(SearchViewModel, s, _unitOfWorkAsync);
                        //add the value to viewmodel
                        viewmodel.lifestylesearchsettings = SearchViewModel;
                     }
                    return viewmodel;

                      });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.EditMemberService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }


        

        }
     
        #endregion


        //Edit Profile Settings Occur here.
        //here are the methdods that actually modify settings i.e old UI vs new

        #region "Methods to Update profile settings for a user"

        #region "Edit profile Public methods here "

        //this function validates the user has a matching apikey for this profile as well as has the role access to create new searches if its other than perfectmatch
        private AnewluvMessages validatecanmodifyprofile(EditProfileModel editprofilemodel, AnewluvMessages AnewluvMessages)
        {
            //make sure API key matches 
            //check the api key matches the profile id pased 
            bool profileidmatchesapikey = false;
            if (editprofilemodel.apikey != null)
            {

                 profileidmatchesapikey = Api.AsyncCalls.validateapikeybyuseridentifierasync(new
                              ApiKeyValidationModel { application_id = (int)applicationenum.anewluv, keyvalue = editprofilemodel.apikey.Value, useridentifier = editprofilemodel.profileid }).Result;
            }
           

            if (!profileidmatchesapikey)
                AnewluvMessages.errormessages.Add("invalid token for this profile id! please log in to perform this action");


            return AnewluvMessages;

        }

        //global profile upddate
        public async Task<AnewluvMessages> membereditallsettings(EditProfileModel editprofilemodel)
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
                            //validate that this user is corret for the passed apikey
                            AnewluvMessages = validatecanmodifyprofile(editprofilemodel, AnewluvMessages);
                            //exit with error status 
                            if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;


                            profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid});

                          

                            //messages = updatememberbasicsettings(editprofilemodel.basicsettings, p, messages);
                            //messages = updatememberappearancesettings(editprofilemodel.appearancesettings, p, messages);
                            //messages = updatemembercharactersettings (editprofilemodel.charactersettings, p, messages);
                            //messages = updatememberlifestylesettings(editprofilemodel.lifestylesettings, p, messages);

                            if (AnewluvMessages.errormessages.Count > 0)
                            {
                                AnewluvMessages.errormessages.Add("There was a problem Editing your profile settings, Please try again later");
                                return AnewluvMessages;
                            }
                            AnewluvMessages.messages.Add("Edit profile Settings Successful");
                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.EditSearchService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
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

        public async Task<AnewluvMessages> membereditbasicsettings(EditProfileModel editprofilemodel)
        {

          //
         
            {
               
             //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;   
                         var task = Task.Factory.StartNew(() =>
                    {

                      
                        //create a new messages object
                        AnewluvMessages AnewluvMessages = new AnewluvMessages();

                        //validate that this user is corret for the passed apikey
                        AnewluvMessages = validatecanmodifyprofile(editprofilemodel, AnewluvMessages);
                        //exit with error status 
                        if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;

                        profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid });


                        AnewluvMessages = updatememberbasicsettings(editprofilemodel, p, AnewluvMessages);
                        //  messages=(membereditBasicSettingsPage2Update(newmodel,profileid ,messages));


                        if (AnewluvMessages.errormessages.Count > 0)
                        {
                            AnewluvMessages.errormessages.Add("There was a problem Editing You Basic Settings, Please try again later");
                            return AnewluvMessages;
                        }

                        //log activity
                        foreach (activitytypeEnum activity in AnewluvMessages.actvitytypes) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id,null, (int)activity, ctx)); }
                        Api.AnewLuvLogging.LogProfileActivities(activitylist);

                        AnewluvMessages.messages.Add("Edit Basic Settings Successful");
                        return AnewluvMessages;
                    });
                         return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }
          
        }

        public async Task<AnewluvMessages> membereditappearancesettings(EditProfileModel editprofilemodel)
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

                        //validate that this user is corret for the passed apikey
                        AnewluvMessages = validatecanmodifyprofile(editprofilemodel, AnewluvMessages);
                        //exit with error status 
                        if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;

                        //get the profile details :
                        profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid });

                        AnewluvMessages = (updatememberappearancesettings(editprofilemodel, p, AnewluvMessages));
                        // messages = (membereditAppearanceSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditAppearanceSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditAppearanceSettingsPage4Update(newmodel, profileid, messages));

                        if (AnewluvMessages.errormessages.Count > 0)
                        {
                            AnewluvMessages.errormessages.Add("There was a problem Editing You Appearance Settings, Please try again later");
                            return AnewluvMessages;
                        }

                        var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
                        foreach (activitytypeEnum activity in AnewluvMessages.actvitytypes) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id,null, (int)activity, ctx)); }
                        Api.AnewLuvLogging.LogProfileActivities(activitylist);

                        AnewluvMessages.messages.Add("Edit Appearance Settings Successful");
                        return AnewluvMessages;

                    });
                         return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


           
        }

        public async Task<AnewluvMessages> membereditcharactersettings(EditProfileModel editprofilemodel)
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

                        //validate that this user is corret for the passed apikey
                        AnewluvMessages = validatecanmodifyprofile(editprofilemodel, AnewluvMessages);
                        //exit with error status 
                        if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;


                        //get the profile details :
                        profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid });

                        AnewluvMessages = (updatemembercharactersettings(editprofilemodel, p, AnewluvMessages));
                        // messages = (membereditcharacterSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditcharacterSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditcharacterSettingsPage4Update(newmodel, profileid, messages));

                        if (AnewluvMessages.errormessages.Count > 0)
                        {
                            AnewluvMessages.errormessages.Add("There was a problem Editing You character Settings, Please try again later");
                            return AnewluvMessages;
                        }

                        var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
                        foreach (activitytypeEnum activity in AnewluvMessages.actvitytypes) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id,null, (int)activity, ctx)); }
                        Api.AnewLuvLogging.LogProfileActivities(activitylist);

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
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }




        }

        public async Task<AnewluvMessages> membereditlifestylesettings(EditProfileModel editprofilemodel)
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

                        //validate that this user is corret for the passed apikey
                        AnewluvMessages = validatecanmodifyprofile(editprofilemodel, AnewluvMessages);
                        //exit with error status 
                        if (AnewluvMessages.errormessages.Count() > 0) return AnewluvMessages;

                        //get the profile details :
                        profile p = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = editprofilemodel.profileid});



                        AnewluvMessages = (updatememberlifestylesettings(editprofilemodel, p, AnewluvMessages));
                        // messages = (membereditlifestyleSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditlifestyleSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditlifestyleSettingsPage4Update(newmodel, profileid, messages));

                        if (AnewluvMessages.errormessages.Count > 0)
                        {
                            AnewluvMessages.errormessages.Add("There was a problem Editing You lifestyle Settings, Please try again later");
                            return AnewluvMessages;
                        }

                        var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
                        foreach (activitytypeEnum activity in AnewluvMessages.actvitytypes) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id,null, (int)activity, ctx)); }
                        Api.AnewLuvLogging.LogProfileActivities(activitylist);

                        AnewluvMessages.messages.Add("Edit lifestyle Settings Successful");
                        return AnewluvMessages;
                    });
                          return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemodel.profileid));
                        }
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }



        }

        #endregion

        #endregion

        #region "Private update methods that can be re-used"

        //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
        private AnewluvMessages updatememberbasicsettings(EditProfileModel newmodel, profile p, AnewluvMessages messages)
        {


            try
            {
               bool updated = false;
               var convertedbirthdate = (DateTime?)newmodel.basicsettings.birthdate.Value.Date;

                //TO DO
                //TO DO must be suscribere to change birthdate more than once so check the profile activities for prior change
                if (  p.profiledata.birthdate.Value.Date != convertedbirthdate ) { 
                    p.profiledata.birthdate = convertedbirthdate  ; updated =true;                 
                     messages.actvitytypes.Add(activitytypeEnum.changebirthdate);

                
                };

                //TO DO must be suscriber to change screen name 
                if (!string.IsNullOrEmpty(newmodel.basicsettings.screenname) && p.screenname != newmodel.basicsettings.screenname)
                {
                    var existingscreenname =  _unitOfWorkAsync.Repository<profile>().checkifscreennamealreadyexists(new ProfileModel { screenname  = newmodel.basicsettings.screenname});
                    if (!existingscreenname)
                    {
                       updated = true;
                        messages.actvitytypes.Add(activitytypeEnum.changescreenname);
                        updated = true;
                    }
                };

                if (newmodel.basicsettings.gender.id != 0 && p.profiledata.gender_id != newmodel.basicsettings.gender.id)
                { 
                    p.profiledata.gender_id = newmodel.basicsettings.gender.id; updated = true;                 
                    messages.actvitytypes.Add(activitytypeEnum.updatedgendersettings);
                };



                if (p.profiledata.aboutme != newmodel.basicsettings.aboutme) { p.profiledata.aboutme = newmodel.basicsettings.aboutme; updated = true; };                
                if (  p.profiledata.mycatchyintroLine !=newmodel.basicsettings.catchyintroline ) { p.profiledata.mycatchyintroLine = newmodel.basicsettings.catchyintroline  ; updated =true; };
                if (p.profiledata.city != newmodel.basicsettings.city) { p.profiledata.city = newmodel.basicsettings.city; updated = true; };                
                if ( (newmodel.basicsettings.stateprovince != null ) && p.profiledata.stateprovince != newmodel.basicsettings.stateprovince ) { p.profiledata.stateprovince = newmodel.basicsettings.stateprovince  ; updated =true; };              
                if (newmodel.basicsettings.countryid != null &&  p.profiledata.countryid != newmodel.basicsettings.countryid) { p.profiledata.countryid = newmodel.basicsettings.countryid  ; updated =true; };
               
                if ((p.profiledata.postalcode !=null |  p.profiledata.postalcode !="" )&&  p.profiledata.postalcode != newmodel.basicsettings.postalcode ) { p.profiledata.postalcode = newmodel.basicsettings.postalcode  ; updated =true; };
                if ((newmodel.basicsettings.phonenumber  != "" | newmodel.basicsettings.phonenumber  != "NA") &&  p.profiledata.phone!= newmodel.basicsettings.phonenumber ) { p.profiledata.phone = newmodel.basicsettings.phonenumber  ; updated =true; };
             

              
                //TO DO log actvity ?
                if (updated)
                {
                    p.modificationdate = DateTime.Now;
                    p.profiledata.ObjectState = ObjectState.Modified;
                    p.ObjectState = ObjectState.Modified;                   
                    _unitOfWorkAsync.Repository<profile>().Update(p);
                    _unitOfWorkAsync.Repository<profiledata>().Update(p.profiledata);
                    _unitOfWorkAsync.SaveChanges();
                      messages.actvitytypes.Add(activitytypeEnum.updateprofile);
                }

                  // LogProfileActivity(ProfileModel ProfileModel, int activitytypeid,OperationContext context)

                if (newmodel.basicsearchsettings != null)
                {
                    //update the search settings 
                    searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id,searchname = "MyPerfectMatch" },_unitOfWorkAsync);
                    //empty search setting handling
                    messages = searchsettingsextentions.updatebasicsearchsettings(newmodel.basicsearchsettings, s, messages, _unitOfWorkAsync);
                 
                }

                //log activities
               


                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //return newmodel;
            }
            catch (Exception ex)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                //  var message = dx.Message;
                throw ex;
            }

            return messages;
        }
        //TO DO add validation and pass back via messages 

        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatememberappearancesettings(EditProfileModel newmodel, profile p, AnewluvMessages messages)
        {
            bool profileupdated = false ;
            bool profilelistsupdated  = false;
         //  var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;

            try
            {
              

                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

                //var height = (newmodel.appearancesettings.height == p.profiledata.height) ? newmodel.appearancesettings.height : null;
                //if (height.HasValue == true)
                //{
                //    p.profiledata.height = height;
                //    profileupdated = true;
                //}

                //these all pass back the list of all body vlaues with the selecteced being exculsive so we need to parse through
                //update the list with the items that are selected.
                if (newmodel.appearancesettings.metricheightlist != null)
                {
                    foreach (metricheight metricheight in newmodel.appearancesettings.metricheightlist.Where(c => c.selected == true ))
                    {
                        //only update if the heigt is not a maych of existing
                        if (metricheight.heightindex != p.profiledata.height.ToString())
                        {
                            //update the value as checked here on the list
                            p.profiledata.height = Convert.ToInt16(metricheight.heightindex);
                            //  model.showmelist.First(d => d.id == showme.id).selected = true;
                            //if (bodytype != null) p.profiledata.lu_bodytype = bodytype;
                            profileupdated = true;
                        }
                    }
               }

                //these all pass back the list of all body vlaues with the selecteced being exculsive so we need to parse through
                 //update the list with the items that are selected.
                if (newmodel.appearancesettings.bodytypelist != null)
                {
                    foreach (listitem selectedbodytype in newmodel.appearancesettings.bodytypelist.Where(c => c.selected == true && c.id != p.profiledata.bodytype_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.bodytype_id = selectedbodytype.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (bodytype != null) p.profiledata.lu_bodytype = bodytype;
                        profileupdated = true;
                    }
                }

                if (newmodel.appearancesettings.haircolorlist != null)
                {
                    foreach (listitem selectedhaircolor in newmodel.appearancesettings.haircolorlist.Where(c => c.selected == true && c.id != p.profiledata.haircolor_id))
                    {
                        p.profiledata.haircolor_id = selectedhaircolor.id; 
                        profileupdated = true;
                    }                   
                }

                if (newmodel.appearancesettings.eyecolorlist != null)
                {
                    foreach (listitem selectedeyecolor in newmodel.appearancesettings.eyecolorlist.Where(c => c.selected == true && c.id != p.profiledata.eyecolor_id))
                    {
                        p.profiledata.eyecolor_id = selectedeyecolor.id;
                        profileupdated = true;
                    }
                }



                //if (height.HasValue == true) p.profiledata.lu_height = height;

                if (newmodel.appearancesettings.hotfeaturelist != null && newmodel.appearancesettings.hotfeaturelist.Count > 0)
                    profilelistsupdated = updatemembermetatdatahotfeatures(newmodel.appearancesettings.hotfeaturelist, p.profilemetadata, profilelistsupdated);
                if (newmodel.appearancesettings.ethnicitylist !=null && newmodel.appearancesettings.ethnicitylist.Count > 0)
                    profilelistsupdated = updatemembermetatdataethnicity(newmodel.appearancesettings.ethnicitylist, p.profilemetadata, profilelistsupdated);


                //_unitOfWorkAsync.Entry(profiledata).State = EntityState.Modified;
                // int changes = _unitOfWorkAsync.SaveChanges();
                if (profileupdated)
                {
                    p.ObjectState = ObjectState.Modified;
                    _unitOfWorkAsync.Repository<profiledata>().Update(p.profiledata);

                }


                if (profilelistsupdated | profileupdated)
                {
                    _unitOfWorkAsync.SaveChanges();
                    messages.messages.Add("Appearance settings have been updated !");
                     messages.actvitytypes.Add(activitytypeEnum.updateprofile);
                }
                else
                {
                    messages.errormessages.Add("Nothing to update!");
                }
              
                if (newmodel.appearancesearchsettings != null)
                {
                    searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                    //empty search setting handling     
                    messages = searchsettingsextentions.updateappearancesearchsettings(newmodel.appearancesearchsettings, s, messages, _unitOfWorkAsync);
                 
                  
                }

            
                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = profiledata;               

                //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
                //model.CurrentErrors.Clear();
                // return model;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;



        }

        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatemembercharactersettings(EditProfileModel newmodel, profile p, AnewluvMessages messages)
        {
            bool profileupdated = false;
            bool profilelistsupdated = false;
           var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;

            try
            {
                // profile p = _unitOfWorkAsync.profiles.Where(z => z.id == profileid).First();
                //sample code for determining weather to edit an item or not or determin if a value changed'
                //nothingupdated = (newmodel.diet  == p.profiledata.lu_diet) ? false : true;

                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list


                if (newmodel.charactersettings.dietlist != null)
                {
                    foreach (listitem selecteddiet in newmodel.charactersettings.dietlist.Where(c => c.selected == true && c.id != p.profiledata.diet_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.diet_id = selecteddiet.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (diet != null) p.profiledata.lu_diet = diet;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.humorlist != null)
                {
                    foreach (listitem selectedhumor in newmodel.charactersettings.humorlist.Where(c => c.selected == true && c.id != p.profiledata.humor_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.humor_id = selectedhumor.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (humor != null) p.profiledata.lu_humor = humor;
                        profileupdated = true;
                    }
                }


                if (newmodel.charactersettings.drinkslist != null)
                {
                    foreach (listitem selecteddrinks in newmodel.charactersettings.drinkslist.Where(c => c.selected == true && c.id != p.profiledata.drinking_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.drinking_id = selecteddrinks.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (drinks != null) p.profiledata.lu_drinks = drinks;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.exerciselist != null)
                {
                    foreach (listitem selectedexercise in newmodel.charactersettings.exerciselist.Where(c => c.selected == true && c.id != p.profiledata.exercise_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.exercise_id = selectedexercise.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (exercise != null) p.profiledata.lu_exercise = exercise;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.smokeslist != null)
                {
                    foreach (listitem selectedsmokes in newmodel.charactersettings.smokeslist.Where(c => c.selected == true && c.id != p.profiledata.smoking_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.smoking_id = selectedsmokes.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (smokes != null) p.profiledata.lu_smokes = smokes;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.signlist != null)
                {
                    foreach (listitem selectedsign in newmodel.charactersettings.signlist.Where(c => c.selected == true && c.id != p.profiledata.sign_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.sign_id = selectedsign.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (sign != null) p.profiledata.lu_sign = sign;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.politicalviewlist != null)
                {
                    foreach (listitem selectedpoliticalview in newmodel.charactersettings.politicalviewlist.Where(c => c.selected == true && c.id != p.profiledata.politicalview_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.politicalview_id = selectedpoliticalview.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (politicalview != null) p.profiledata.lu_politicalview = politicalview;
                        profileupdated = true;

                    }
                }

                if (newmodel.charactersettings.religionlist != null)
                {
                    foreach (listitem selectedreligion in newmodel.charactersettings.religionlist.Where(c => c.selected == true && c.id != p.profiledata.religion_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.religion_id = selectedreligion.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (religionlist != null) p.profiledata.lu_religionlist = religionlist;
                        profileupdated = true;
                    }
                }

                if (newmodel.charactersettings.religiousattendancelist != null)
                {
                    foreach (listitem selectedreligiousattendance in newmodel.charactersettings.religiousattendancelist.Where(c => c.selected == true && c.id != p.profiledata.religiousattendance_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.religiousattendance_id = selectedreligiousattendance.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (religionlist != null) p.profiledata.lu_religionlist = religionlist;
                        profileupdated = true;
                    }
                }


                if (newmodel.charactersettings.hobbylist !=null && newmodel.charactersettings.hobbylist.Count > 0)
                  profilelistsupdated =    updatemembermetatdatahobby(newmodel.charactersettings.hobbylist, p.profilemetadata,profilelistsupdated);


                if (profileupdated)
                {
                    p.ObjectState = ObjectState.Modified;
                    _unitOfWorkAsync.Repository<profiledata>().Update(p.profiledata);                   

                }

                if (profilelistsupdated | profileupdated)
                {
                    _unitOfWorkAsync.SaveChanges();
                    messages.messages.Add("Character settings have been updated !");
                    messages.actvitytypes.Add(activitytypeEnum.updateprofile);
                }
                {
                    messages.errormessages.Add("Nothing to update!");
                }
              

               if (newmodel.charactersearchsettings != null)
               {
                   searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                   //handling for a new search
                 //  foreach (activitytypeEnum activity in messages.actvitytypes) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id, (int)activity,ctx));} }
                   messages = searchsettingsextentions.updatecharactersearchsettings(newmodel.charactersearchsettings, s, messages, _unitOfWorkAsync);
                 
                  

               }

               //log activities
               //if (activitylist.Count() > 0) Api.AnewLuvLogging.LogProfileActivities(activitylist);

                
                
                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = profiledata;               

                //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
                //model.CurrentErrors.Clear();
                // return model;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;



        }

        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatememberlifestylesettings(EditProfileModel newmodel, profile p, AnewluvMessages messages)
        {
            bool profileupdated = false;
            bool profilelistsupdated = false;
          //  var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;

            try
            {
                // profile p = _unitOfWorkAsync.profiles.Where(z => z.id == profileid).First();
                //sample code for determining weather to edit an item or not or determin if a value changed'
                //nothingupdated = (newmodel.educationlevel  == p.profiledata.lu_educationlevel) ? false : true;





                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list
                if (newmodel.lifestylesettings.educationlevellist != null)
                {
                    foreach (listitem selectededucationlevel in newmodel.lifestylesettings.educationlevellist.Where(c => c.selected == true && c.id != p.profiledata.educationlevel_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.educationlevel_id = selectededucationlevel.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (educationlevel != null) p.profiledata.lu_educationlevel = educationlevel;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.employmentstatuslist != null)
                {
                    foreach (listitem selectedemploymentstatus in newmodel.lifestylesettings.employmentstatuslist.Where(c => c.selected == true && c.id != p.profiledata.employmentstatus_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.employmentstatus_id = selectedemploymentstatus.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (employmentstatus != null) p.profiledata.lu_employmentstatus = employmentstatus;
                        profileupdated = true;
                    }
                }
                if (newmodel.lifestylesettings.havekidslist != null)
                {

                    foreach (listitem selectedhavekids in newmodel.lifestylesettings.havekidslist.Where(c => c.selected == true && c.id != p.profiledata.kidstatus_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.kidstatus_id = selectedhavekids.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (havekids != null) p.profiledata.lu_havekids = havekids;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.incomelevellist != null)
                {
                    foreach (listitem selectedincomelevel in newmodel.lifestylesettings.incomelevellist.Where(c => c.selected == true && c.id != p.profiledata.incomelevel_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.incomelevel_id = selectedincomelevel.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (incomelevel != null) p.profiledata.lu_incomelevel = incomelevel;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.livingsituationlist != null)
                {
                    foreach (listitem selectedlivingsituation in newmodel.lifestylesettings.livingsituationlist.Where(c => c.selected == true && c.id != p.profiledata.livingsituation_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.livingsituation_id = selectedlivingsituation.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (livingsituation != null) p.profiledata.lu_livingsituation = livingsituation;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.maritalstatuslist != null)
                {
                    foreach (listitem selectedmaritalstatus in newmodel.lifestylesettings.maritalstatuslist.Where(c => c.selected == true && c.id != p.profiledata.maritalstatus_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.maritalstatus_id = selectedmaritalstatus.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (maritalstatus != null) p.profiledata.lu_maritalstatus = maritalstatus;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.professionlist != null)
                {

                    foreach (listitem selectedprofession in newmodel.lifestylesettings.professionlist.Where(c => c.selected == true && c.id != p.profiledata.profession_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.profession_id = selectedprofession.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (profession != null) p.profiledata.lu_profession = profession;
                        profileupdated = true;
                    }
                }

                if (newmodel.lifestylesettings.wantskidslist != null)
                {
                    foreach (listitem selectedwantskids in newmodel.lifestylesettings.wantskidslist.Where(c => c.selected == true && c.id != p.profiledata.wantsKidstatus_id))
                    {
                        //update the value as checked here on the list
                        p.profiledata.wantsKidstatus_id = selectedwantskids.id;
                        //  model.showmelist.First(d => d.id == showme.id).selected = true;
                        //if (wantskids != null) p.profiledata.lu_wantskids = wantskids;
                        profileupdated = true;
                    }
                }


                //checkbos item updates 
                if (newmodel.lifestylesettings.lookingforlist !=null  && newmodel.lifestylesettings.lookingforlist.Count > 0)             
                    profilelistsupdated = updatemembermetatdatalookingfor(newmodel.lifestylesettings.lookingforlist, p.profilemetadata, profilelistsupdated);




                if (profileupdated)
                {
                    p.ObjectState = ObjectState.Modified;
                    _unitOfWorkAsync.Repository<profiledata>().Update(p.profiledata);

                }


                if (profilelistsupdated | profileupdated)
                {
                    _unitOfWorkAsync.SaveChanges();
                    messages.messages.Add("Character settings have been updated !");
                    //activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id, (int)activitytypeEnum.updateprofile, ctx));
                    messages.actvitytypes.Add(activitytypeEnum.updateprofile);
                }
                {
                    messages.errormessages.Add("Nothing has changed so nothing updated!");
                }
              
                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = profiledata;               

                //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
                //model.CurrentErrors.Clear();
                // return model;
               if (newmodel.lifestylesearchsettings != null)
               {
                   searchsetting s = _unitOfWorkAsync.Repository<searchsetting>().getorcreatesearchsettings(new SearchSettingsModel { profileid = p.id, searchname = "MyPerfectMatch" }, _unitOfWorkAsync);
                   //add all relevant activities to the list
                  // foreach(activitytypeEnum activity in messages.actvitytypes ) { activitylist.Add(Api.AnewLuvLogging.CreateActivity(p.id, (int)activity,ctx));}
                   messages = searchsettingsextentions.updatelifestylesearchsettings(newmodel.lifestylesearchsettings, s, messages, _unitOfWorkAsync);
               }

               //log activities
             //  if (activitylist.Count() > 0) Api.AnewLuvLogging.LogProfileActivities(activitylist);

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
       

        #region "PRIVATE Checkbox Update Functions for profiledata many to many"


        //profiledata ethnicity
        private bool updatemembermetatdataethnicity(List<listitem> ethnicitylist, profilemetadata currentprofilemetadata,bool updated)
        {
            
            //get the selected items  passed
            //var selectedethnicity = ethnicitylist.Where(z => z.selected == true);

            if (ethnicitylist != null && ethnicitylist.Count() == 0) return false;

            try
            {
                //get a list of the approved values here so wrong id's cannot be injected
                var validethnicity = _unitOfWorkAsync.Repository<lu_ethnicity>().Queryable().ToList();

                foreach (var item in ethnicitylist)
                {
                   
                    //validate that this passed id matches what is in the database
                    if (validethnicity.Any(z => z.id == item.id))
                    {
                        var itemalreadyexists = currentprofilemetadata.profiledata_ethnicity.Where(m => m.ethnicty_id == item.id).Count() > 0;  


                        //new logic : if this item was selected and is not already add it
                        if (!itemalreadyexists && item.selected == true)
                        {
                            //SearchSettings_showme.showmeID = showme.showmeID;
                            var temp = new profiledata_ethnicity();
                            temp.ethnicty_id = item.id;
                            temp.profile_id = currentprofilemetadata.profile_id;
                            temp.ObjectState = ObjectState.Added;
                            _unitOfWorkAsync.Repository<profiledata_ethnicity>().Insert(temp);
                            updated = true;

                        }
                        else
                        {
                            //new logic if any of the existing values match this id and it is not selected
                            if (itemalreadyexists && item.selected == false)
                            {
                                var temp = _unitOfWorkAsync.Repository<profiledata_ethnicity>().Queryable()
                                          .Where(p => p.profile_id == currentprofilemetadata.profile_id && p.ethnicty_id == item.id).FirstOrDefault();
                                temp.ObjectState = ObjectState.Deleted;
                                _unitOfWorkAsync.Repository<profiledata_ethnicity>().Delete(temp);
                                updated = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updated;
        }
        //profiledata hotfeature
        private bool updatemembermetatdatahotfeatures(List<listitem> hotfeatureslist, profilemetadata currentprofilemetadata,bool updated)
        {

            //get the selected items  passed
            //var selectedhotfeatures = hotfeatureslist.Where(z => z.selected == true);

            if (hotfeatureslist != null && hotfeatureslist.Count() == 0) return false;

            try
            {
                //get a list of the approved values here so wrong id's cannot be injected
                var validhotfeatures = _unitOfWorkAsync.Repository<lu_hotfeature>().Queryable().ToList();

                foreach (var item in hotfeatureslist)
                {

                    //validate that this passed id matches what is in the database
                    if (validhotfeatures.Any(z => z.id == item.id))
                    {
                        var itemalreadyexists = currentprofilemetadata.profiledata_hotfeature.Where(m => m.hotfeature_id == item.id).Count() > 0;


                        //new logic : if this item was selected and is not already add it
                        if (!itemalreadyexists && item.selected == true)
                        {
                            //SearchSettings_showme.showmeID = showme.showmeID;
                            var temp = new profiledata_hotfeature();
                            temp.hotfeature_id = item.id;
                            temp.profile_id = currentprofilemetadata.profile_id;
                            temp.ObjectState = ObjectState.Added;
                            _unitOfWorkAsync.Repository<profiledata_hotfeature>().Insert(temp);
                            updated = true;

                        }
                        else
                        {
                            //new logic if any of the existing values match this id and it is not selected
                            if (itemalreadyexists && item.selected == false)
                            {
                                var temp = _unitOfWorkAsync.Repository<profiledata_hotfeature>().Queryable()
                                          .Where(p => p.profile_id == currentprofilemetadata.profile_id && p.hotfeature_id == item.id).FirstOrDefault();
                                temp.ObjectState = ObjectState.Deleted;
                                _unitOfWorkAsync.Repository<profiledata_hotfeature>().Delete(temp);
                                updated = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updated;
        }
        //profiledata hobby
        private bool updatemembermetatdatahobby(List<listitem> hobbylist, profilemetadata currentprofilemetadata,bool updated)
        {

            //get the selected items  passed
            //var selectedhobby = hobbylist.Where(z => z.selected == true);

            if (hobbylist != null && hobbylist.Count() == 0) return false;

            try
            {
                //get a list of the approved values here so wrong id's cannot be injected
                var validhobby = _unitOfWorkAsync.Repository<lu_hobby>().Queryable().ToList();

                foreach (var item in hobbylist)
                {

                    //validate that this passed id matches what is in the database
                    if (validhobby.Any(z => z.id == item.id))
                    {
                        var itemalreadyexists = currentprofilemetadata.profiledata_hobby.Where(m => m.hobby_id == item.id).Count() > 0;


                        //new logic : if this item was selected and is not already add it
                        if (!itemalreadyexists && item.selected == true)
                        {
                            //SearchSettings_showme.showmeID = showme.showmeID;
                            var temp = new profiledata_hobby();
                            temp.hobby_id = item.id;
                            temp.profile_id = currentprofilemetadata.profile_id;
                            temp.ObjectState = ObjectState.Added;
                            _unitOfWorkAsync.Repository<profiledata_hobby>().Insert(temp);
                            updated = true;

                        }
                        else
                        {
                            //new logic if any of the existing values match this id and it is not selected
                            if (itemalreadyexists && item.selected == false)
                            {
                                var temp = _unitOfWorkAsync.Repository<profiledata_hobby>().Queryable()
                                          .Where(p => p.profile_id == currentprofilemetadata.profile_id && p.hobby_id == item.id).FirstOrDefault();
                                temp.ObjectState = ObjectState.Deleted;
                                _unitOfWorkAsync.Repository<profiledata_hobby>().Delete(temp);
                                updated = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updated;
        }
        //profiledata lookingfor
        private bool updatemembermetatdatalookingfor(List<listitem> lookingforlist, profilemetadata currentprofilemetadata,bool updated)
        {

            //get the selected items  passed
            //var selectedlookingfor = lookingforlist.Where(z => z.selected == true);

            if (lookingforlist != null && lookingforlist.Count() == 0) return false;

            try
            {
                //get a list of the approved values here so wrong id's cannot be injected
                var validlookingfor = _unitOfWorkAsync.Repository<lu_lookingfor>().Queryable().ToList();

                foreach (var item in lookingforlist)
                {

                    //validate that this passed id matches what is in the database
                    if (validlookingfor.Any(z => z.id == item.id))
                    {
                        var itemalreadyexists = currentprofilemetadata.profiledata_lookingfor.Where(m => m.lookingfor_id == item.id).Count() > 0;


                        //new logic : if this item was selected and is not already add it
                        if (!itemalreadyexists && item.selected == true)
                        {
                            //SearchSettings_showme.showmeID = showme.showmeID;
                            var temp = new profiledata_lookingfor();
                            temp.lookingfor_id = item.id;
                            temp.profile_id = currentprofilemetadata.profile_id;
                            temp.ObjectState = ObjectState.Added;
                            _unitOfWorkAsync.Repository<profiledata_lookingfor>().Insert(temp);
                            updated = true;

                        }
                        else
                        {
                            //new logic if any of the existing values match this id and it is not selected
                            if (itemalreadyexists && item.selected == false)
                            {
                                var temp = _unitOfWorkAsync.Repository<profiledata_lookingfor>().Queryable()
                                          .Where(p => p.profile_id == currentprofilemetadata.profile_id && p.lookingfor_id == item.id).FirstOrDefault();
                                temp.ObjectState = ObjectState.Deleted;
                                _unitOfWorkAsync.Repository<profiledata_lookingfor>().Delete(temp);
                                updated = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updated;
        }

        #endregion


    }
}
