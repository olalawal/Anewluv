using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

////using RiaServicesContrib.Mvc;
////using RiaServicesContrib.Mvc.Services;

using System.Text;

using System.Web.Mvc;

using Common;

using System.Data;
using System.Data.Entity;
using Shell.MVC2.AppFabric;
using Ninject.Web.Mvc;
using Ninject;

namespace Shell.MVC2.Models
{
    public class EditProfileRepository
    {

        //TO DO
        //Get these initalized
        //DatingService datingservicecontext = new DatingService().Initialize();
        //PostalDataService postaldataservicecontext = new PostalDataService().Initialize();
       // private AnewLuvFTSEntities db = new AnewLuvFTSEntities();
       // private MembersRepository membersrepository = new MembersRepository();
        //PostalData2Entities postaldb = new PostalData2Entities();


      
   
        private  AnewLuvFTSEntities db; // = new AnewLuvFTSEntities();
        private DatingService datingservice;
        private MembersRepository membersrepository;
       // private  PostalData2Entities postaldb; //= new PostalData2Entities();


        //TO DO


        public EditProfileRepository()
       {
           IKernel kernel = new StandardKernel();
          //Get these initalized

           datingservice = kernel.Get<DatingService>();
           db =  kernel.Get<AnewLuvFTSEntities>();
           membersrepository = kernel.Get<MembersRepository>();
           
        }

        #region "call functions that actually update the individual pages on edit profile links"


        //public MembersViewModel  GetMembersViewModelAddSearchSettingsAndUpdate(string _ProfileID)
        //{
        //    //get members model from appfabric
        //    // EditProfileRepository editprofilerepo = new EditProfileRepository();
        //    MembersViewModel membersmodel = new MembersViewModel();
        //    //get users session data
        //    //TO DO change this to a new query against user name
        //    membersmodel = CachingFactory.MembersViewModelHelper.GetMemberData(_ProfileID);
                      

        //    //requery in case data was changed on a previous page
        //    membersmodel.profiledata = membersrepository.GetProfileDataByProfileID(membersmodel.profiledata.ProfileID);
                     
        //    //get current search settings data if any
        //    //membersmodel.profiledata.SearchSettings.Add(membersrepository.GetPerFectMatchSearchSettingsByProfileID(membersmodel.profiledata.ProfileID));
        //    //save the search settings here with my ma*tches 



        //    //update the saved model in ram
        //    //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, _ProfileID);
        //    return membersmodel;

        //}


        #region "Edit profile Basic Settings Updates here
        public bool EditProfileBasicSettingsPage1Update(EditProfileSettingsViewModel model,
            FormCollection formCollection, int?[] SelectedGenderIds, string _ProfileID)
        {

           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
           
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            //create the search settings i.e matches if it does not exist 
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();
           
          
            //get all the values that should post from page 1
            //var AboutMe = formCollection["Editor"];  
            //var MyCatchyIntroLine = formCollection["MyCatchyIntroLine"];
            var AboutMe = model.BasicProfileSettings.AboutMe;
            var MyCatchyIntroLine = model.BasicProfileSettings.MyCatchyIntroLine;
            var agemin = model.BasicSearchSettings.agemin;
            var agemax = model.BasicSearchSettings.agemax;
            //get current values from DB in case some values were not updated
            model.BasicProfileSettings  =  new EditProfileBasicSettingsModel(ProfileDataToUpdate);            
            //test if thoe are empty
            // var testLookingForAgeFrom = formCollection["BasicSearchSettings.agemin"];

            //check if user checked at least one gender
            // bool isGenderSelected = formCollection.GetValues("SelectedGenderIds").Contains("true");  
            var GendersValues = formCollection["SelectedGenderIds"];

            //re populate the models
            //build Basic Profile Settings from Submited view 
            model.BasicProfileSettings.AboutMe = AboutMe;
            model.BasicProfileSettings.MyCatchyIntroLine = MyCatchyIntroLine;

            //get map the basic search settings to the search settings pulled from databse
            model.BasicSearchSettings = new SearchModelBasicSettings(SearchSettingsToUpdate);
            //update the searchmodl settings with current settings
            model.BasicSearchSettings.agemin = agemin;
            model.BasicSearchSettings.agemax = agemax;
            //update gender values as well 
            IEnumerable<int?> myEnumerable = SelectedGenderIds;

            var GenderValues = myEnumerable != null ? new HashSet<int?>(myEnumerable) : null;

            foreach (var _Gender in model.BasicSearchSettings.genderslist)
            {
                _Gender.Selected = GendersValues != null ? GenderValues.Contains(_Gender.GenderID) : false;
            }

            //TO DO move this to client side validaton
            //now validate and update the page the page we are on
            if (GendersValues == null)
            {
                //  ModelState.AddModelError("selectedId", "You have to select at least one gender!");
                //add errors to model
                model.CurrentErrors.Add("You have to select at least one gender!");
                return model;
            }


         
            //var ModelToUpdate = db.ProfileDatas
            //   .Where(i => i.ProfileID == membersmodel.profiledata.ProfileID)
            // .Select(p => new
            // {
            //     profiledata = p,
            //     SearchSettings = p.SearchSettings.Where(i => i.MyPerfectMatch == true).FirstOrDefault()
            // }).SingleOrDefault();

            try
            {
                //link the profiledata entities


                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
                //manually update model i think
                //set properties in the about me
                ProfileDataToUpdate.AboutMe = AboutMe;
                ProfileDataToUpdate.MyCatchyIntroLine = MyCatchyIntroLine;

                //detrmine if we are in edit or add mode for search settings for perfect match
                //if its null add a new entity  
                //noew update searchsettings text values
                SearchSettingsToUpdate.AgeMax = agemax;
                SearchSettingsToUpdate.AgeMin = agemin;

                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; //addded time stamp for updates this should be somone where else tho ?
                //TO DO move this code to searchssettings Repositoury
                this.UpdateSearchSettingsGenders(SelectedGenderIds, ProfileDataToUpdate);
                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
            
              //  CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);
                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }
        }


        public bool EditProfileBasicSettingsPage2Update(EditProfileSettingsViewModel model,
                FormCollection formCollection,
               int?[] SelectedShowMeIds, int?[] SelectedSortByIds, string _ProfileID)
        {



            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

            var DistanceFromMe = model.BasicSearchSettings.DistanceFromMe;
            //re populate the models
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            model.BasicProfileSettings = new EditProfileBasicSettingsModel(ProfileDataToUpdate);  

            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.BasicSearchSettings = new SearchModelBasicSettings(SearchSettingsToUpdate);
            //update the searchmodl settings with current settings on the UI
            model.BasicSearchSettings.DistanceFromMe = DistanceFromMe;
        
                       
            //update show me and sortby with correct values from UI as well
            //this code is just for serv side validation does nothing atm

            //showme next
            IEnumerable<int?> myEnumerableShowmes = SelectedShowMeIds;

            var ShowMeTypeValues = myEnumerableShowmes != null ? new HashSet<int?>(myEnumerableShowmes) : null;

            foreach (var _ShowMeType in model.BasicSearchSettings.showmelist)
            {
                _ShowMeType.Selected = ShowMeTypeValues != null ? ShowMeTypeValues.Contains(_ShowMeType.ShowMeID) : false;
            }
            
            
            IEnumerable<int?> myEnumerableSortBys = SelectedSortByIds;

            var SortByTypeValues = myEnumerableSortBys != null ? new HashSet<int?>(myEnumerableSortBys) : null;

            foreach (var _SortByType in model.BasicSearchSettings.SortByList)
            {
                _SortByType.Selected = SortByTypeValues != null ? SortByTypeValues.Contains(_SortByType.SortByTypeID) : false;
            }
           



            try
            {
                //link the profiledata entities
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ////Add searchh settings as well if its not null
                //if (ModelToUpdate.SearchSettings == null)
                //{
                //    SearchSetting NewSearchSettings = new SearchSetting();
                //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
                //}
                //else
                //{
                //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
                //}

                //detrmine if we are in edit or add mode for search settings for perfect match
                //if its null add a new entity  
                //noew update searchsettings text values
                SearchSettingsToUpdate.DistanceFromMe = model.BasicSearchSettings.DistanceFromMe;


                //TO DO move this code to searchssettings Repositoury
                UpdateSearchSettingsShowMe(SelectedShowMeIds, ProfileDataToUpdate);
                UpdateSearchSettingsSortByTypes(SelectedSortByIds, ProfileDataToUpdate);
                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
              //  membersmodel.profiledata = ProfileDataToUpdate;
              //  CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,ProfileDataToUpdate  );

                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }
        #endregion
               
        #region "Edit profile Appeareance Settings Updates here"

        public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage1Update(EditProfileSettingsViewModel model,
        FormCollection formCollection, int?[] SelectedYourBodyTypesID,  string _ProfileID)
        {

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            
           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

           //TO DO finde a better way to do this I guess get the current

            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //relaod appreadnce settings as needed
            var UiHeight = model.AppearanceSettings.Height ;
            var UiBodyType = model.AppearanceSettings.BodyTypesID;

            model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);

            //noew updated the reloaded model with the saved higit on UI
            model.AppearanceSettings.Height = UiHeight;
            model.AppearanceSettings.BodyTypesID = UiBodyType;

            var heightmin = model.AppearanceSearchSettings.heightmin == -1 ? 48 : model.AppearanceSearchSettings.heightmin;
            var heightmax = model.AppearanceSearchSettings.heightmax == -1 ? 89  : model.AppearanceSearchSettings.heightmax ;
           

            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI
            model.AppearanceSearchSettings.heightmin = heightmin ;
            model.AppearanceSearchSettings.heightmax = heightmax ;


            //update the searchmodl settings with current settings            
            //update body types mine For UI
            IEnumerable<int?> EnumerableYourBodyTypes = SelectedYourBodyTypesID;

            var YourBodyTypesValues = EnumerableYourBodyTypes != null ? new HashSet<int?>(EnumerableYourBodyTypes) : null;

            foreach (var _BodyTypes in model.AppearanceSearchSettings.bodytypeslist )
            {
                _BodyTypes.Selected = YourBodyTypesValues != null ? YourBodyTypesValues.Contains(_BodyTypes.BodyTypesID ) : false;
            }
            

            try
            {
                //link the profiledata entities
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                //search settings will never be null anymore , should have been created before we got here and added the members model
                //Add searchh settings as well if its not null
                //if (ModelToUpdate.SearchSettings == null)
                //{
                //    SearchSetting NewSearchSettings = new SearchSetting();
                //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
                //}
                //else
                //{
                //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
                //}

                //detrmine if we are in edit or add mode for search settings for perfect match
                //if its null add a new entity  
                //noew update searchsettings text values
                //update my settings 
                 ProfileDataToUpdate.Height = Convert.ToInt32(model.AppearanceSettings.Height);
                 ProfileDataToUpdate.BodyTypeID = model.AppearanceSettings.BodyTypesID;
                
                 //now update the search settings 
                  SearchSettingsToUpdate.HeightMin  = model.AppearanceSearchSettings.heightmin;
                  SearchSettingsToUpdate.HeightMax = model.AppearanceSearchSettings.heightmax;
                  SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 
                  UpdateSearchSettingsBodyTypes (SelectedYourBodyTypesID  , ProfileDataToUpdate);            
                  

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                   int changes  =db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;               
                          
             //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,ProfileDataToUpdate  );
                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage2Update(EditProfileSettingsViewModel model,
             FormCollection formCollection, int?[] SelectedYourEthnicityIds, int?[] SelectedMyEthnicityIds, string _ProfileID
            )
        {


            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();
                        
            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);
         


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes

            IEnumerable<int?> EnumerableMyEthnicity = SelectedMyEthnicityIds;

            var MyEthnicityValues = EnumerableMyEthnicity != null ? new HashSet<int?>(EnumerableMyEthnicity) : null;

            foreach (var _Ethnicity in model.AppearanceSettings.Myethnicitylist)
            {
                _Ethnicity.Selected = MyEthnicityValues != null ? MyEthnicityValues.Contains(_Ethnicity.EthnicityID) : false;
            }
            
            IEnumerable<int?> EnumerableYourEthnicity =  SelectedYourEthnicityIds  ;

            var YourEthnicityValues = EnumerableYourEthnicity != null ? new HashSet<int?>(EnumerableYourEthnicity) : null;

            foreach (var _Ethnicity in model.AppearanceSearchSettings.ethnicitylist)
            {
                _Ethnicity.Selected = YourEthnicityValues != null ? YourEthnicityValues.Contains(_Ethnicity.EthnicityID) : false;
            }

          
            try
            {
                //link the profiledata entities
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ////Add searchh settings as well if its not null
                //if (ModelToUpdate.SearchSettings == null)
                //{
                //    SearchSetting NewSearchSettings = new SearchSetting();
                //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
                //}
                //else
                //{
                //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
                //}
                               
                 UpdateSearchSettingsEthnicity (SelectedYourEthnicityIds , ProfileDataToUpdate);
                 UpdateProfileDataEthnicity(SelectedMyEthnicityIds, ProfileDataToUpdate);
                 SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;
             //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID ,ProfileDataToUpdate );

                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage3Update(EditProfileSettingsViewModel model,
             FormCollection formCollection, int?[] SelectedYourEyeColorIds, 
            int?[] SelectedYourHairColorIds,
            string _ProfileID)
        {

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            

            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo


            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var EyColorID = model.AppearanceSettings.EyeColorID;
            var HairCOlorID = model.AppearanceSettings.HairColorID;
            model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);
            model.AppearanceSettings.HairColorID = HairCOlorID;
            model.AppearanceSettings.EyeColorID = EyColorID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI
          

            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes
            IEnumerable<int?> EnumerableYourHairColor = SelectedYourHairColorIds;

            var YourHairColorValues = EnumerableYourHairColor != null ? new HashSet<int?>(EnumerableYourHairColor) : null;

            foreach (var _HairColor in model.AppearanceSearchSettings.haircolorlist)
            {
                _HairColor.Selected = YourHairColorValues != null ? YourHairColorValues.Contains(_HairColor.HairColorID) : false;
            }

            IEnumerable<int?> EnumerableYourEyeColor = SelectedYourEyeColorIds;

            var YourEyeColorValues = EnumerableYourEyeColor != null ? new HashSet<int?>(EnumerableYourEyeColor) : null;

            foreach (var _EyeColor in model.AppearanceSearchSettings.eyecolorlist)
            {
                _EyeColor.Selected = YourEyeColorValues != null ? YourEyeColorValues.Contains(_EyeColor.EyeColorID) : false;
            }

            //UI updates done

            //get active profile data 
      


            try
            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
               
                //update my settings 
                ProfileDataToUpdate.EyeColorID  = model.AppearanceSettings.EyeColorID ;
                ProfileDataToUpdate.HairColorID  = model.AppearanceSettings.HairColorID;

                //now update the search settings 
                SearchSettingsToUpdate.HeightMin = model.AppearanceSearchSettings.heightmin;
                SearchSettingsToUpdate.HeightMax = model.AppearanceSearchSettings.heightmax;

                UpdateSearchSettingsEyeColor (SelectedYourEyeColorIds , ProfileDataToUpdate);
                UpdateSearchSettingsHairColor(SelectedYourHairColorIds , ProfileDataToUpdate);

                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 
                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;
               // CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);

                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }
        }

        public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage4Update(EditProfileSettingsViewModel model,
             FormCollection formCollection, int?[] SelectedYourHotFeatureIds, int?[] SelectedMyHotFeatureIds, string _ProfileID)
        {

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            //no validation needed just save 
            //TOD DO move this to a function i think or repository for search and have it populate those there
            //repopulate checkboxes for postback
            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
           
            //reload the Apppearance values
            model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);

            model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);

            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes
            IEnumerable<int?> EnumerableYourHotFeature = SelectedYourHotFeatureIds;

            var YourHotFeatureValues = EnumerableYourHotFeature != null ? new HashSet<int?>(EnumerableYourHotFeature) : null;

            foreach (var _HotFeature in model.AppearanceSearchSettings.hotfeaturelist)
            {
                _HotFeature.Selected = YourHotFeatureValues != null ? YourHotFeatureValues.Contains(_HotFeature.HotFeatureID) : false;
            }

            IEnumerable<int?> EnumerableMyHotFeature = SelectedMyHotFeatureIds;

            var MyHotFeatureValues = EnumerableMyHotFeature != null ? new HashSet<int?>(EnumerableMyHotFeature) : null;

            foreach (var _HotFeature in model.AppearanceSettings.Myhotfeaturelist)
            {
                _HotFeature.Selected = MyHotFeatureValues != null ? MyHotFeatureValues.Contains(_HotFeature.HotFeatureID) : false;
            }

            //UI updates done

            //get active profile data 
            //get active profile data 
          

            try
            {
                //link the profiledata entities

                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
                //now update the search settings 
                UpdateSearchSettingsHotFeature (SelectedYourHotFeatureIds , ProfileDataToUpdate);
                UpdateProfileDataHotFeature(SelectedMyHotFeatureIds, ProfileDataToUpdate);

                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 
                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;
               // CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID ,ProfileDataToUpdate );

                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }
        }

        #endregion


        #region "Edit profile LifeStyle Settings Updates here"

        public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage1Update(EditProfileSettingsViewModel model,
        FormCollection formCollection, int?[] SelectedYourMaritalStatusIds, int?[] SelectedYourLivingSituationIds,
            int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, string _ProfileID)
        {
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyMaritalStatusID = model.LifeStyleSettings.MaritalStatusID;
            var MyLivingSituationID = model.LifeStyleSettings.LivingSituationID;
            model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
            model.LifeStyleSettings.MaritalStatusID = MyMaritalStatusID;
            model.LifeStyleSettings.LivingSituationID = MyLivingSituationID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes

            IEnumerable<int?> EnumerableMyLookingFor = SelectedMyLookingForIds;

            var MyLookingForValues = EnumerableMyLookingFor != null ? new HashSet<int?>(EnumerableMyLookingFor) : null;

            foreach (var _LookingFor in model.LifeStyleSettings.MyLookingForList)
            {
                _LookingFor.Selected = MyLookingForValues != null ? MyLookingForValues.Contains(_LookingFor.MyLookingForID ) : false;
            }


            IEnumerable<int?> EnumerableYourLookingFor = SelectedYourLookingForIds;

            var YourLookingForValues = EnumerableYourLookingFor != null ? new HashSet<int?>(EnumerableYourLookingFor) : null;

            foreach (var _LookingFor in model.LifeStyleSearchSettings.lookingforlist)
            {
                _LookingFor.Selected = YourLookingForValues != null ? YourLookingForValues.Contains(_LookingFor.LookingForID) : false;
            }

            IEnumerable<int?> EnumerableYourLivingSituation = SelectedYourLivingSituationIds;

            var YourLivingSituationValues = EnumerableYourLivingSituation != null ? new HashSet<int?>(EnumerableYourLivingSituation) : null;

            foreach (var _LivingSituation in model.LifeStyleSearchSettings.livingsituationlist)
            {
                _LivingSituation.Selected = YourLivingSituationValues != null ? YourLivingSituationValues.Contains(_LivingSituation.LivingSituationID) : false;
            }

            IEnumerable<int?> EnumerableYourMaritalStatus = SelectedYourMaritalStatusIds;

            var YourMaritalStatusValues = EnumerableYourMaritalStatus != null ? new HashSet<int?>(EnumerableYourMaritalStatus) : null;

            foreach (var _MaritalStatus in model.LifeStyleSearchSettings.maritalstatuslist)
            {
                _MaritalStatus.Selected = YourMaritalStatusValues != null ? YourMaritalStatusValues.Contains(_MaritalStatus.MaritalStatusID) : false;
            }

              

          // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
          //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            
            try
            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
                ProfileDataToUpdate.MaritalStatusID = Convert.ToInt32(model.LifeStyleSettings.MaritalStatusID );
                ProfileDataToUpdate.LivingSituationID  = model.LifeStyleSettings.LivingSituationID ;


                UpdateProfileDataLookingFor (SelectedMyLookingForIds, ProfileDataToUpdate);
                UpdateSearchSettingsLookingFor (SelectedYourLookingForIds , ProfileDataToUpdate);
                UpdateSearchSettingsMaritalStatus (SelectedYourMaritalStatusIds , ProfileDataToUpdate);
                UpdateSearchSettingsLivingSituation (SelectedYourLivingSituationIds, ProfileDataToUpdate);
                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 


                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;

                CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage2Update(EditProfileSettingsViewModel model,
    FormCollection formCollection, int?[] SelectedYourHaveKidsIds, int?[] SelectedYourWantsKidsIds,
         string _ProfileID)
        {
           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyWantKidsID = model.LifeStyleSettings.WantsKidsID;
            var MyHaveKidsID = model.LifeStyleSettings.HaveKidsId;
            model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
            model.LifeStyleSettings.WantsKidsID  = MyWantKidsID;
            model.LifeStyleSettings.HaveKidsId  = MyHaveKidsID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes
                    


            IEnumerable<int?> EnumerableYourWantsKids = SelectedYourWantsKidsIds ;

            var YourWantsKidsValues = EnumerableYourWantsKids != null ? new HashSet<int?>(EnumerableYourWantsKids) : null;

            foreach (var _WantsKids in model.LifeStyleSearchSettings.wantskidslist)
            {
                _WantsKids.Selected = YourWantsKidsValues != null ? YourWantsKidsValues.Contains(_WantsKids.WantsKidsID) : false;
            }

           
            IEnumerable<int?> EnumerableYourHaveKids = SelectedYourHaveKidsIds;

            var YourHaveKidsValues = EnumerableYourHaveKids != null ? new HashSet<int?>(EnumerableYourHaveKids) : null;

            foreach (var _HaveKids in model.LifeStyleSearchSettings.havekidslist)
            {
                _HaveKids.Selected = YourHaveKidsValues != null ? YourHaveKidsValues.Contains(_HaveKids.HaveKidsID) : false;
            }



           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
            {



                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.WantsKidsID  = Convert.ToInt32(model.LifeStyleSettings.WantsKidsID );
                ProfileDataToUpdate.HaveKidsId = model.LifeStyleSettings.HaveKidsId;


              
                UpdateSearchSettingsWantsKids(SelectedYourWantsKidsIds, ProfileDataToUpdate);
                 UpdateSearchSettingsHaveKids(SelectedYourHaveKidsIds, ProfileDataToUpdate);

                 SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;

               // CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }


        public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage3Update(EditProfileSettingsViewModel model,
 FormCollection formCollection, int?[] SelectedYourEmploymentStatusIds, int?[] SelectedYourIncomeLevelIds,
      string _ProfileID)
        {
           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyIncomeLevelID = model.LifeStyleSettings.IncomeLevelID;
            var MyEmploymentStatusID = model.LifeStyleSettings.EmploymentStatusID;
            model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
            model.LifeStyleSettings.IncomeLevelID= MyIncomeLevelID;
            model.LifeStyleSettings.EmploymentStatusID = MyEmploymentStatusID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes



            IEnumerable<int?> EnumerableYourIncomeLevel = SelectedYourIncomeLevelIds;

            var YourIncomeLevelValues = EnumerableYourIncomeLevel != null ? new HashSet<int?>(EnumerableYourIncomeLevel) : null;

            foreach (var _IncomeLevel in model.LifeStyleSearchSettings.incomelevellist)
            {
                _IncomeLevel.Selected = YourIncomeLevelValues != null ? YourIncomeLevelValues.Contains(_IncomeLevel.IncomeLevelID) : false;
            }


            IEnumerable<int?> EnumerableYourEmploymentStatus = SelectedYourEmploymentStatusIds;

            var YourEmploymentStatusValues = EnumerableYourEmploymentStatus != null ? new HashSet<int?>(EnumerableYourEmploymentStatus) : null;

            foreach (var _EmploymentStatus in model.LifeStyleSearchSettings.employmentstatuslist)
            {
                _EmploymentStatus.Selected = YourEmploymentStatusValues != null ? YourEmploymentStatusValues.Contains(_EmploymentStatus.EmploymentStatusID) : false;
            }



           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
                
            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.IncomeLevelID  = Convert.ToInt32(model.LifeStyleSettings.IncomeLevelID );
                ProfileDataToUpdate.EmploymentSatusID  = model.LifeStyleSettings.EmploymentStatusID ;



                UpdateSearchSettingsIncomeLevel (SelectedYourIncomeLevelIds, ProfileDataToUpdate);
                UpdateSearchSettingsEmploymentStatus(SelectedYourEmploymentStatusIds, ProfileDataToUpdate);
                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 


                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;
                //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage4Update(EditProfileSettingsViewModel model,
 FormCollection formCollection, int?[] SelectedYourEducationLevelIds, int?[] SelectedYourProfessionIds,
      string _ProfileID)
        {

            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            //MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyProfessionID = model.LifeStyleSettings.ProfessionID;
            var MyEducationLevelID = model.LifeStyleSettings.EducationLevelID;
            model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
            model.LifeStyleSettings.ProfessionID = MyProfessionID;
            model.LifeStyleSettings.EducationLevelID = MyEducationLevelID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes



            IEnumerable<int?> EnumerableYourProfession = SelectedYourProfessionIds;

            var YourProfessionValues = EnumerableYourProfession != null ? new HashSet<int?>(EnumerableYourProfession) : null;

            foreach (var _Profession in model.LifeStyleSearchSettings.professionlist)
            {
                _Profession.Selected = YourProfessionValues != null ? YourProfessionValues.Contains(_Profession.ProfessionID) : false;
            }


            IEnumerable<int?> EnumerableYourEducationLevel = SelectedYourEducationLevelIds;

            var YourEducationLevelValues = EnumerableYourEducationLevel != null ? new HashSet<int?>(EnumerableYourEducationLevel) : null;

            foreach (var _EducationLevel in model.LifeStyleSearchSettings.educationlevellist)
            {
                _EducationLevel.Selected = YourEducationLevelValues != null ? YourEducationLevelValues.Contains(_EducationLevel.EducationLevelID) : false;
            }



           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
            {

                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
                ProfileDataToUpdate.ProfessionID = Convert.ToInt32(model.LifeStyleSettings.ProfessionID);
                ProfileDataToUpdate.EducationLevelID = model.LifeStyleSettings.EducationLevelID;
                UpdateSearchSettingsProfession(SelectedYourProfessionIds, ProfileDataToUpdate);
                UpdateSearchSettingsEducationLevel(SelectedYourEducationLevelIds, ProfileDataToUpdate);


                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 
                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
                //membersmodel.profiledata = ProfileDataToUpdate;

                //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        #endregion


        #region "Edit profile Character Settings Updates here"

        public EditProfileSettingsViewModel EditProfileCharacterSettingsPage1Update(EditProfileSettingsViewModel model,
        FormCollection formCollection, int?[] SelectedYourDietIds, int?[] SelectedYourDrinksIds,
            int?[] SelectedYourExerciseIds,int?[] SelectedYourSmokesIds,  string _ProfileID)
        {
            //5-10-2012 moved this to get these items first.
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID  ).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID);// db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            
            //MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyDietID = model.CharacterSettings.DietID;
            var MyDrinksID = model.CharacterSettings.DrinksID;
            var MyExerciseID = model.CharacterSettings.ExerciseID ;
            var MySmokesID = model.CharacterSettings.SmokesID;
            //TO DO read from name value collection to incrfeate efficency
            model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
            model.CharacterSettings.DietID = MyDietID;
            model.CharacterSettings.DrinksID = MyDrinksID;
            model.CharacterSettings.ExerciseID = MyExerciseID;
            model.CharacterSettings.SmokesID = MySmokesID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI
          

          
            IEnumerable<int?> EnumerableYourExercise = SelectedYourExerciseIds;

            var YourExerciseValues = EnumerableYourExercise != null ? new HashSet<int?>(EnumerableYourExercise) : null;

            foreach (var _Exercise in model.CharacterSearchSettings.exerciselist)
            {
                _Exercise.Selected = YourExerciseValues != null ? YourExerciseValues.Contains(_Exercise.ExerciseID) : false;
            }

            IEnumerable<int?> EnumerableYourDrinks = SelectedYourDrinksIds;

            var YourDrinksValues = EnumerableYourDrinks != null ? new HashSet<int?>(EnumerableYourDrinks) : null;

            foreach (var _Drinks in model.CharacterSearchSettings.drinkslist)
            {
                _Drinks.Selected = YourDrinksValues != null ? YourDrinksValues.Contains(_Drinks.DrinksID) : false;
            }

            IEnumerable<int?> EnumerableYourDiet = SelectedYourDietIds;

            var YourDietValues = EnumerableYourDiet != null ? new HashSet<int?>(EnumerableYourDiet) : null;

            foreach (var _Diet in model.CharacterSearchSettings.dietlist)
            {
                _Diet.Selected = YourDietValues != null ? YourDietValues.Contains(_Diet.DietID) : false;
            }

            IEnumerable<int?> EnumerableYourSmokes = SelectedYourSmokesIds;

            var YourSmokesValues = EnumerableYourSmokes != null ? new HashSet<int?>(EnumerableYourSmokes) : null;

            foreach (var _Smokes in model.CharacterSearchSettings.smokeslist)
            {
                _Smokes.Selected = YourSmokesValues != null ? YourSmokesValues.Contains(_Smokes.SmokesID) : false;
            }




           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.DietID = Convert.ToInt32(model.CharacterSettings.DietID);
                ProfileDataToUpdate.DrinksID = model.CharacterSettings.DrinksID;
                ProfileDataToUpdate.ExerciseID = model.CharacterSettings.ExerciseID ;
                ProfileDataToUpdate.SmokesID = model.CharacterSettings.SmokesID;

         
                UpdateSearchSettingsExercise(SelectedYourExerciseIds, ProfileDataToUpdate);
                UpdateSearchSettingsDiet(SelectedYourDietIds, ProfileDataToUpdate);
                UpdateSearchSettingsDrinks(SelectedYourDrinksIds, ProfileDataToUpdate);
                UpdateSearchSettingsSmokes(SelectedYourSmokesIds, ProfileDataToUpdate);

                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;

                //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileCharacterSettingsPage2Update(EditProfileSettingsViewModel model,
    FormCollection formCollection, int?[] SelectedYourHobbyIds,int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds,
         string _ProfileID)
        {
          // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
            //5-10-2012 moved this to get these items first.
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); // db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            

            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MySignID = model.CharacterSettings.SignID;
            model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
            model.CharacterSettings.SignID = MySignID ;
      


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes



            IEnumerable<int?> EnumerableMyHobby = SelectedMyHobbyIds;

            var MyHobbyValues = EnumerableMyHobby != null ? new HashSet<int?>(EnumerableMyHobby) : null;

            foreach (var _Hobby in model.CharacterSettings.MyHobbyList)
            {
                _Hobby.Selected = MyHobbyValues != null ? MyHobbyValues.Contains(_Hobby.MyHobbyID) : false;
            }

            IEnumerable<int?> EnumerableYourHobby = SelectedYourHobbyIds;

            var YourHobbyValues = EnumerableYourHobby != null ? new HashSet<int?>(EnumerableYourHobby) : null;

            foreach (var _Hobby in model.CharacterSearchSettings.hobbylist)
            {
                _Hobby.Selected = YourHobbyValues != null ? YourHobbyValues.Contains(_Hobby.HobbyID) : false;
            }


            IEnumerable<int?> EnumerableYourSign = SelectedYourSignIds;

            var YourSignValues = EnumerableYourSign != null ? new HashSet<int?>(EnumerableYourSign) : null;

            foreach (var _Sign in model.CharacterSearchSettings.signlist)
            {
                _Sign.Selected = YourSignValues != null ? YourSignValues.Contains(_Sign.SignID) : false;
            }



          // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
            {

                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.SignID = Convert.ToInt32(model.CharacterSettings.SignID);
              



                UpdateProfileDataHobby(SelectedMyHobbyIds, ProfileDataToUpdate);
                UpdateSearchSettingsHobby(SelectedYourHobbyIds, ProfileDataToUpdate);
                UpdateSearchSettingsSign(SelectedYourSignIds, ProfileDataToUpdate);

                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;

               // CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);

               model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }



        public EditProfileSettingsViewModel EditProfileCharacterSettingsPage3Update(EditProfileSettingsViewModel model,
 FormCollection formCollection, int?[] SelectedYourReligionIds, int?[] SelectedYourReligiousAttendanceIds,
      string _ProfileID)
        {
            // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
            //5-10-2012 moved this to get these items first.
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyReligiousAttendanceID = model.CharacterSettings.ReligiousAttendanceID;
            var MyReligionID = model.CharacterSettings.ReligionID;
            model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
            model.CharacterSettings.ReligiousAttendanceID = MyReligiousAttendanceID;
            model.CharacterSettings.ReligionID = MyReligionID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes



            IEnumerable<int?> EnumerableYourReligiousAttendance = SelectedYourReligiousAttendanceIds;

            var YourReligiousAttendanceValues = EnumerableYourReligiousAttendance != null ? new HashSet<int?>(EnumerableYourReligiousAttendance) : null;

            foreach (var _ReligiousAttendance in model.CharacterSearchSettings.religiousattendancelist)
            {
                _ReligiousAttendance.Selected = YourReligiousAttendanceValues != null ? YourReligiousAttendanceValues.Contains(_ReligiousAttendance.ReligiousAttendanceID) : false;
            }


            IEnumerable<int?> EnumerableYourReligion = SelectedYourReligionIds;

            var YourReligionValues = EnumerableYourReligion != null ? new HashSet<int?>(EnumerableYourReligion) : null;

            foreach (var _Religion in model.CharacterSearchSettings.religionlist)
            {
                _Religion.Selected = YourReligionValues != null ? YourReligionValues.Contains(_Religion.ReligionID) : false;
            }



         //   profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
          //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try
            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.ReligiousAttendanceID = Convert.ToInt32(model.CharacterSettings.ReligiousAttendanceID);
                ProfileDataToUpdate.ReligionID = Convert.ToInt32(model.CharacterSettings.ReligionID );
                ProfileDataToUpdate.EmploymentSatusID = model.CharacterSettings.ReligionID;
                

                UpdateSearchSettingsReligiousAttendance(SelectedYourReligiousAttendanceIds, ProfileDataToUpdate);
                UpdateSearchSettingsReligion(SelectedYourReligionIds, ProfileDataToUpdate);

                //added modifciation date 1-9-2012 , confirm that it works as an inclided
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 
                SearchSettingsToUpdate.LastUpdateDate = DateTime.Now; 

                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;

                //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        public EditProfileSettingsViewModel EditProfileCharacterSettingsPage4Update(EditProfileSettingsViewModel model,
 FormCollection formCollection, int?[] SelectedYourPoliticalViewIds, int?[] SelectedYourHumorIds,
      string _ProfileID)
        {
            // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
            //5-10-2012 moved this to get these items first.
            profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
            if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
            SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

            //re populate the models TO DO not sure this is needed index valiues are stored
            //if there are checkbox values on basic settings we would need to reload as well
            //build Basic Profile Settings from Submited view 
            // model.BasicProfileSettings. = AboutMe;
            //temp store values on UI also handle ANY case here !!
            //just for conistiancy.
            var MyHumorID = model.CharacterSettings.HumorID;
            var MyPoliticalViewID = model.CharacterSettings.PoliticalViewID;
            model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
            model.CharacterSettings.HumorID = MyHumorID;
            model.CharacterSettings.PoliticalViewID = MyPoliticalViewID;


            //reload search settings since it seems the checkbox values are lost on postback
            //we really should just rebuild them from form collection imo
            model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
            //update the reloaded  searchmodl settings with current settings on the UI


            //update the searchmodl settings with current settings            
            //update UI display values with current displayed values as well for check boxes



            IEnumerable<int?> EnumerableYourHumor = SelectedYourHumorIds;

            var YourHumorValues = EnumerableYourHumor != null ? new HashSet<int?>(EnumerableYourHumor) : null;

            foreach (var _Humor in model.CharacterSearchSettings.humorlist)
            {
                _Humor.Selected = YourHumorValues != null ? YourHumorValues.Contains(_Humor.HumorID) : false;
            }


            IEnumerable<int?> EnumerableYourPoliticalView = SelectedYourPoliticalViewIds;

            var YourPoliticalViewValues = EnumerableYourPoliticalView != null ? new HashSet<int?>(EnumerableYourPoliticalView) : null;

            foreach (var _PoliticalView in model.CharacterSearchSettings.politicalviewlist)
            {
                _PoliticalView.Selected = YourPoliticalViewValues != null ? YourPoliticalViewValues.Contains(_PoliticalView.PoliticalViewID) : false;
            }



          // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
         //   SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


            try

            {
                ProfileDataToUpdate.profile.ModificationDate = DateTime.Now; 

                ProfileDataToUpdate.HumorID = Convert.ToInt32(model.CharacterSettings.HumorID);
                ProfileDataToUpdate.PoliticalViewID = Convert.ToInt32(model.CharacterSettings.PoliticalViewID );
                ProfileDataToUpdate.EmploymentSatusID = model.CharacterSettings.PoliticalViewID;



                UpdateSearchSettingsHumor(SelectedYourHumorIds, ProfileDataToUpdate);
                UpdateSearchSettingsPoliticalView(SelectedYourPoliticalViewIds, ProfileDataToUpdate);



                //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
                int changes = db.SaveChanges();

                //TOD DO
                //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
                //update session too just in case
               // membersmodel.profiledata = ProfileDataToUpdate;

                CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


                model.CurrentErrors.Clear();
                return model;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException) 
                model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return model;
            }

        }

        #endregion

        #endregion

        #region "Checkbox Update Functions for profiledata many to many"



        private void UpdateProfileDataEthnicity(int?[] selectedEthnicity, profiledata ProfileDataToUpdate)
        {
            if (selectedEthnicity == null)
            {
                // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_Ethnicity  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            string ProfileDataID = ProfileDataToUpdate.ProfileID;
            //ProfileData_Ethnicity CurrentProfileData_Ethnicity = db.ProfileData_Ethnicity.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


            var selectedEthnicityHS = new HashSet<int?>(selectedEthnicity);
            //get the values for this members profiledata Ethnicity
            var ProfileDataEthnicity = new HashSet<int?>(ProfileDataToUpdate.ProfileData_Ethnicity.Select(c => c.EthnicityID));
            foreach (var Ethnicity in db.CriteriaAppearance_Ethnicity)
            {
                if (selectedEthnicityHS.Contains(Ethnicity.EthnicityID))
                {
                    if (!ProfileDataEthnicity.Contains(Ethnicity.EthnicityID))
                    {

                        //ProfileData_Ethnicity.EthnicityID = Ethnicity.EthnicityID;
                        var temp = new ProfileData_Ethnicity();
                        temp.EthnicityID = Ethnicity.EthnicityID;
                        temp.ProfileID= ProfileDataID;

                        db.AddObject("ProfileData_Ethnicity", temp);
                    }
                }
                else
                {
                    if (ProfileDataEthnicity.Contains(Ethnicity.EthnicityID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_Ethnicity.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateProfileDataHotFeature(int?[] selectedHotFeature, profiledata ProfileDataToUpdate)
        {
            if (selectedHotFeature == null)
            {
                // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_HotFeature  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            string ProfileDataID = ProfileDataToUpdate.ProfileID;
            //ProfileData_HotFeature CurrentProfileData_HotFeature = db.ProfileData_HotFeature.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


            var selectedHotFeatureHS = new HashSet<int?>(selectedHotFeature);
            //get the values for this members profiledata HotFeature
            var ProfileDataHotFeature = new HashSet<int?>(ProfileDataToUpdate.ProfileData_HotFeature.Select(c => c.HotFeatureID));
            foreach (var HotFeature in db.CriteriaCharacter_HotFeature )
            {
                if (selectedHotFeatureHS.Contains(HotFeature.HotFeatureID))
                {
                    if (!ProfileDataHotFeature.Contains(HotFeature.HotFeatureID))
                    {

                        //ProfileData_HotFeature.HotFeatureID = HotFeature.HotFeatureID;
                        var temp = new ProfileData_HotFeature();
                        temp.HotFeatureID = HotFeature.HotFeatureID;
                        temp.ProfileID = ProfileDataID;

                        db.AddObject("ProfileData_HotFeature", temp);
                    }
                }
                else
                {
                    if (ProfileDataHotFeature.Contains(HotFeature.HotFeatureID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_HotFeature.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateProfileDataHobby(int?[] selectedHobby, profiledata ProfileDataToUpdate)
        {
            if (selectedHobby == null)
            {
                // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_Hobby  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            string ProfileDataID = ProfileDataToUpdate.ProfileID;
            //ProfileData_Hobby CurrentProfileData_Hobby = db.ProfileData_Hobby.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


            var selectedHobbyHS = new HashSet<int?>(selectedHobby);
            //get the values for this members profiledata Hobby
            var ProfileDataHobby = new HashSet<int?>(ProfileDataToUpdate.ProfileData_Hobby.Select(c => c.HobbyID));
            foreach (var Hobby in db.CriteriaCharacter_Hobby )
            {
                if (selectedHobbyHS.Contains(Hobby.HobbyID))
                {
                    if (!ProfileDataHobby.Contains(Hobby.HobbyID))
                    {

                        //ProfileData_Hobby.HobbyID = Hobby.HobbyID;
                        var temp = new ProfileData_Hobby();
                        temp.HobbyID = Hobby.HobbyID;
                        temp.ProfileID = ProfileDataID;

                        db.AddObject("ProfileData_Hobby", temp);
                    }
                }
                else
                {
                    if (ProfileDataHobby.Contains(Hobby.HobbyID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_Hobby.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateProfileDataLookingFor(int?[] selectedLookingFor, profiledata ProfileDataToUpdate)
        {
            if (selectedLookingFor == null)
            {
                // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_LookingFor  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            string ProfileDataID = ProfileDataToUpdate.ProfileID;
            //ProfileData_LookingFor CurrentProfileData_LookingFor = db.ProfileData_LookingFor.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


            var selectedLookingForHS = new HashSet<int?>(selectedLookingFor);
            //get the values for this members profiledata LookingFor
            var ProfileDataLookingFor = new HashSet<int?>(ProfileDataToUpdate.ProfileData_LookingFor.Select(c => c.LookingForID));
            foreach (var LookingFor in db.CriteriaLife_LookingFor )
            {
                if (selectedLookingForHS.Contains(LookingFor.LookingForID))
                {
                    if (!ProfileDataLookingFor.Contains(LookingFor.LookingForID))
                    {

                        //ProfileData_LookingFor.LookingForID = LookingFor.LookingForID;
                        var temp = new ProfileData_LookingFor();
                        temp.LookingForID = LookingFor.LookingForID;
                        temp.ProfileID = ProfileDataID;

                        db.AddObject("ProfileData_LookingFor", temp);
                    }
                }
                else
                {
                    if (ProfileDataLookingFor.Contains(LookingFor.LookingForID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_LookingFor.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }



        #endregion

        #region "checkbox updated functions searchsettings values for all lists"

        private void UpdateSearchSettingsGenders(int?[] selectedGenders, profiledata ProfileDataToUpdate)
        {
            if (selectedGenders == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Genders  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Genders CurrentSearchSettings_Genders = db.SearchSettings_Genders.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedGendersHS = new HashSet<int?>(selectedGenders);
            //get the values for this members searchsettings Genders
            var SearchSettingsGenders = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Genders.Select(c => c.GenderID ));
            foreach (var Genders in db.genders )
            {
                if (selectedGendersHS.Contains(Genders.GenderID))
                {
                    if (!SearchSettingsGenders.Contains(Genders.GenderID))
                    {

                        //SearchSettings_Genders.GendersID = Genders.GendersID;
                        var temp = new SearchSettings_Genders();
                        temp.GenderID  = Genders.GenderID ;
                        temp.SearchSettingsID = SearchSettingsID;                       
                        db.AddObject("SearchSettings_Genders", temp);

                    }
                }
                else
                {
                    if (SearchSettingsGenders.Contains(Genders.GenderID))
                    {
                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Genders.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsShowMe(int?[] selectedShowMe, profiledata ProfileDataToUpdate)
        {
            if (selectedShowMe == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ShowMe  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_ShowMe CurrentSearchSettings_ShowMe = db.SearchSettings_ShowMe.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedShowMeHS = new HashSet<int?>(selectedShowMe);
            //get the values for this members searchsettings ShowMe
            var SearchSettingsShowMe = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ShowMe.Select(c => c.ShowMeID));
            foreach (var ShowMe in db.ShowMes)
            {
                if (selectedShowMeHS.Contains(ShowMe.ShowMeID))
                {
                    if (!SearchSettingsShowMe.Contains(ShowMe.ShowMeID))
                    {

                        //SearchSettings_ShowMe.ShowMeID = ShowMe.ShowMeID;
                        var temp = new SearchSettings_ShowMe();
                        temp.ShowMeID = ShowMe.ShowMeID;
                        temp.SearchSettingsID = SearchSettingsID;
                    
                        db.AddObject("SearchSettings_ShowMe", temp);

                    }
                }
                else
                {
                    if (SearchSettingsShowMe.Contains(ShowMe.ShowMeID))
                    {
                    
                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_ShowMe.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsSortByTypes(int?[] selectedSortBy, profiledata ProfileDataToUpdate)
        {
            if (selectedSortBy == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_SortBy  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_SortBy CurrentSearchSettings_SortBy = db.SearchSettings_SortBy.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedSortByHS = new HashSet<int?>(selectedSortBy);
            //get the values for this members searchsettings SortBy
            var SearchSettingsSortBy = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_SortByType.Select(c => c.SortByTypeID ));
            foreach (var SortBy in db.SortByTypes)
            {
                if (selectedSortByHS.Contains(SortBy.SortByTypeID ))
                {
                    if (!SearchSettingsSortBy.Contains(SortBy.SortByTypeID))
                    {

                        //SearchSettings_SortBy.SortByID = SortBy.SortByID;
                        var temp = new SearchSettings_SortByType();
                        temp.SortByTypeID = SortBy.SortByTypeID;
                        temp.SearchSettingsID = SearchSettingsID;
                      //  var dd =   db.ProfileDatas.Where(p=>p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p=>p.SearchSettingsID == SearchSettingsID).First().SearchSettings_SortByType.First();

                        db.AddObject("SearchSettings_SortByType", temp);
                    }
                }
                else
                {
                    if (SearchSettingsSortBy.Contains(SortBy.SortByTypeID))
                    {
                  
                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_SortByType.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsBodyTypes(int?[] selectedBodyTypes, profiledata ProfileDataToUpdate)
        {
            if (selectedBodyTypes == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_BodyTypes CurrentSearchSettings_BodyTypes = db.SearchSettings_BodyTypes.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();
            

            var selectedBodyTypesHS = new HashSet<int?>(selectedBodyTypes);
            //get the values for this members searchsettings BodyTypes
            var SearchSettingsBodyTypes = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes.Select(c => c.BodyTypesID));
            foreach (var BodyTypes in db.CriteriaAppearance_Bodytypes)
            {
                if (selectedBodyTypesHS.Contains(BodyTypes.BodyTypesID ))
                {
                    if (!SearchSettingsBodyTypes.Contains(BodyTypes.BodyTypesID ))
                    {

                      //SearchSettings_BodyTypes.BodyTypesID = BodyTypes.BodyTypesID;
                        var temp = new SearchSettings_BodyTypes();
                        temp.BodyTypesID = BodyTypes.BodyTypesID;
                        temp.SearchSettingsID = SearchSettingsID;                        
                      //  temp.SearchSettings_BodyTypeID = BodyTypes.SearchSettings_BodyTypeID;
                       // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity.Add(SearchSettings_BodyTypes);
                        // SearchSettingsGender.SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
                    //var dd =   db.ProfileDatas.Where(p=>p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p=>p.SearchSettingsID == SearchSettingsID).First().SearchSettings_BodyTypes.First();

                    db.AddObject("SearchSettings_BodyTypes",temp);


                     // You do not have to call the Load method to load the details for the order,
                     // because  lazy loading is set to true 
                     // by the constructor of the AdventureWorksEntities object. 
                     // With  lazy loading set to true the related objects are loaded when
                     // you access the navigation property. In this case SalesOrderDetails.

                     // Delete the first item in the order.
            


                     
                    }
                }
                else
                {
                    if (SearchSettingsBodyTypes.Contains(BodyTypes.BodyTypesID))
                    {
                        //SearchSettings_BodyTypes.BodyTypesID  = BodyTypes.BodyTypesID;
                       // var temp = new SearchSettings_BodyTypes();
                       // temp.BodyTypesID = BodyTypes.BodyTypesID;
                        //temp.SearchSettingsID = BodyTypes.SearchSettingsID;
                        //temp.SearchSettings_BodyTypeID = BodyTypes.SearchSettings_BodyTypeID;
                       // temp.SearchSettings_BodyTypeID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes.Where(p => p.SearchSettingsID == 18 && p.BodyTypesID == BodyTypes.BodyTypesID).FirstOrDefault();
                       // temp.SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
                    //  var CurrentBodyType =  db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).SingleOrDefault().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).SingleOrDefault().SearchSettings_BodyTypes.Remove(temp);

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_BodyTypes.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsEthnicity(int?[] selectedEthnicity, profiledata ProfileDataToUpdate)
        {
            if (selectedEthnicity == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Ethnicity CurrentSearchSettings_Ethnicity = db.SearchSettings_Ethnicity.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedEthnicityHS = new HashSet<int?>(selectedEthnicity);
            //get the values for this members searchsettings Ethnicity
            var SearchSettingsEthnicity = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity.Select(c => c.EthicityID));
            foreach (var Ethnicity in db.CriteriaAppearance_Ethnicity)
            {
                if (selectedEthnicityHS.Contains(Ethnicity.EthnicityID))
                {
                    if (!SearchSettingsEthnicity.Contains(Ethnicity.EthnicityID))
                    {

                        //SearchSettings_Ethnicity.EthnicityID = Ethnicity.EthnicityID;
                        var temp = new SearchSettings_Ethnicity();
                        temp.EthicityID  = Ethnicity.EthnicityID;
                        temp.SearchSettingsID = SearchSettingsID;
                     
                        db.AddObject("SearchSettings_Ethnicity", temp);                       
                    }
                }
                else
                {
                    if (SearchSettingsEthnicity.Contains(Ethnicity.EthnicityID))
                    {
                        
                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Ethnicity.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsHairColor(int?[] selectedHairColor, profiledata ProfileDataToUpdate)
        {
            if (selectedHairColor == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HairColor  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_HairColor CurrentSearchSettings_HairColor = db.SearchSettings_HairColor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedHairColorHS = new HashSet<int?>(selectedHairColor);
            //get the values for this members searchsettings HairColor
            var SearchSettingsHairColor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HairColor.Select(c => c.HairColorID));
            foreach (var HairColor in db.CriteriaAppearance_HairColor)
            {
                if (selectedHairColorHS.Contains(HairColor.HairColorID))
                {
                    if (!SearchSettingsHairColor.Contains(HairColor.HairColorID))
                    {

                        //SearchSettings_HairColor.HairColorID = HairColor.HairColorID;
                        var temp = new SearchSettings_HairColor();
                        temp.HairColorID = HairColor.HairColorID;
                        temp.SearchSettingsID = SearchSettingsID;
                     
                        db.AddObject("SearchSettings_HairColor", temp);

                    }
                }
                else
                {
                    if (SearchSettingsHairColor.Contains(HairColor.HairColorID))
                    {
                    
                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HairColor.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsEyeColor(int?[] selectedEyeColor, profiledata ProfileDataToUpdate)
        {
            if (selectedEyeColor == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EyeColor  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_EyeColor CurrentSearchSettings_EyeColor = db.SearchSettings_EyeColor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedEyeColorHS = new HashSet<int?>(selectedEyeColor);
            //get the values for this members searchsettings EyeColor
            var SearchSettingsEyeColor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EyeColor.Select(c => c.EyeColorID));
            foreach (var EyeColor in db.CriteriaAppearance_EyeColor)
            {
                if (selectedEyeColorHS.Contains(EyeColor.EyeColorID))
                {
                    if (!SearchSettingsEyeColor.Contains(EyeColor.EyeColorID))
                    {

                        //SearchSettings_EyeColor.EyeColorID = EyeColor.EyeColorID;
                        var temp = new SearchSettings_EyeColor();
                        temp.EyeColorID = EyeColor.EyeColorID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_EyeColor", temp);

                    }
                }
                else
                {
                    if (SearchSettingsEyeColor.Contains(EyeColor.EyeColorID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EyeColor.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

       //Usee the search settings ID as parameter instead I guess upate all of em ugh!!
        private void UpdateSearchSettingsHotFeature(int?[] selectedHotFeature, profiledata ProfileDataToUpdate)
        {
            if (selectedHotFeature == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HotFeature  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_HotFeature CurrentSearchSettings_HotFeature = db.SearchSettings_HotFeature.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedHotFeatureHS = new HashSet<int?>(selectedHotFeature);
            //get the values for this members searchsettings HotFeature
            var SearchSettingsHotFeature = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HotFeature.Select(c => c.HotFeatureID));
            foreach (var HotFeature in db.CriteriaCharacter_HotFeature )
            {
                if (selectedHotFeatureHS.Contains(HotFeature.HotFeatureID))
                {
                    if (!SearchSettingsHotFeature.Contains(HotFeature.HotFeatureID))
                    {

                        //SearchSettings_HotFeature.HotFeatureID = HotFeature.HotFeatureID;
                        var temp = new SearchSettings_HotFeature();
                        temp.HotFeatureID = HotFeature.HotFeatureID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_HotFeature", temp);

                    }
                }
                else
                {
                    if (SearchSettingsHotFeature.Contains(HotFeature.HotFeatureID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HotFeature.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsDiet(int?[] selectedDiet, profiledata ProfileDataToUpdate)
        {
            if (selectedDiet == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Diet  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Diet CurrentSearchSettings_Diet = db.SearchSettings_Diet.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedDietHS = new HashSet<int?>(selectedDiet);
            //get the values for this members searchsettings Diet
            var SearchSettingsDiet = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Diet.Select(c => c.DietID));
            foreach (var Diet in db.CriteriaCharacter_Diet)
            {
                if (selectedDietHS.Contains(Diet.DietID))
                {
                    if (!SearchSettingsDiet.Contains(Diet.DietID))
                    {

                        //SearchSettings_Diet.DietID = Diet.DietID;
                        var temp = new SearchSettings_Diet();
                        temp.DietID = Diet.DietID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Diet", temp);

                    }
                }
                else
                {
                    if (SearchSettingsDiet.Contains(Diet.DietID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Diet.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsDrinks(int?[] selectedDrinks, profiledata ProfileDataToUpdate)
        {
            if (selectedDrinks == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Drinks  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Drinks CurrentSearchSettings_Drinks = db.SearchSettings_Drinks.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedDrinksHS = new HashSet<int?>(selectedDrinks);
            //get the values for this members searchsettings Drinks
            var SearchSettingsDrinks = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Drinks.Select(c => c.DrinksID));
            foreach (var Drinks in db.CriteriaCharacter_Drinks)
            {
                if (selectedDrinksHS.Contains(Drinks.DrinksID))
                {
                    if (!SearchSettingsDrinks.Contains(Drinks.DrinksID))
                    {

                        //SearchSettings_Drinks.DrinksID = Drinks.DrinksID;
                        var temp = new SearchSettings_Drinks();
                        temp.DrinksID = Drinks.DrinksID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Drinks", temp);

                    }
                }
                else
                {
                    if (SearchSettingsDrinks.Contains(Drinks.DrinksID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Drinks.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsExercise(int?[] selectedExercise, profiledata ProfileDataToUpdate)
        {
            if (selectedExercise == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Exercise  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Exercise CurrentSearchSettings_Exercise = db.SearchSettings_Exercise.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedExerciseHS = new HashSet<int?>(selectedExercise);
            //get the values for this members searchsettings Exercise
            var SearchSettingsExercise = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Exercise.Select(c => c.ExerciseID));
            foreach (var Exercise in db.CriteriaCharacter_Exercise)
            {
                if (selectedExerciseHS.Contains(Exercise.ExerciseID))
                {
                    if (!SearchSettingsExercise.Contains(Exercise.ExerciseID))
                    {

                        //SearchSettings_Exercise.ExerciseID = Exercise.ExerciseID;
                        var temp = new SearchSettings_Exercise();
                        temp.ExerciseID = Exercise.ExerciseID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Exercise", temp);

                    }
                }
                else
                {
                    if (SearchSettingsExercise.Contains(Exercise.ExerciseID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Exercise.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsHobby(int?[] selectedHobby, profiledata ProfileDataToUpdate)
        {
            if (selectedHobby == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Hobby  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Hobby CurrentSearchSettings_Hobby = db.SearchSettings_Hobby.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedHobbyHS = new HashSet<int?>(selectedHobby);
            //get the values for this members searchsettings Hobby
            var SearchSettingsHobby = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Hobby.Select(c => c.HobbyID));
            foreach (var Hobby in db.CriteriaCharacter_Hobby)
            {
                if (selectedHobbyHS.Contains(Hobby.HobbyID))
                {
                    if (!SearchSettingsHobby.Contains(Hobby.HobbyID))
                    {

                        //SearchSettings_Hobby.HobbyID = Hobby.HobbyID;
                        var temp = new SearchSettings_Hobby();
                        temp.HobbyID = Hobby.HobbyID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Hobby", temp);

                    }
                }
                else
                {
                    if (SearchSettingsHobby.Contains(Hobby.HobbyID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Hobby.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsHumor(int?[] selectedHumor, profiledata ProfileDataToUpdate)
        {
            if (selectedHumor == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Humor  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Humor CurrentSearchSettings_Humor = db.SearchSettings_Humor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedHumorHS = new HashSet<int?>(selectedHumor);
            //get the values for this members searchsettings Humor
            var SearchSettingsHumor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Humor.Select(c => c.HumorID));
            foreach (var Humor in db.CriteriaCharacter_Humor)
            {
                if (selectedHumorHS.Contains(Humor.HumorID))
                {
                    if (!SearchSettingsHumor.Contains(Humor.HumorID))
                    {

                        //SearchSettings_Humor.HumorID = Humor.HumorID;
                        var temp = new SearchSettings_Humor();
                        temp.HumorID = Humor.HumorID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Humor", temp);

                    }
                }
                else
                {
                    if (SearchSettingsHumor.Contains(Humor.HumorID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Humor.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsPoliticalView(int?[] selectedPoliticalView, profiledata ProfileDataToUpdate)
        {
            if (selectedPoliticalView == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_PoliticalView  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_PoliticalView CurrentSearchSettings_PoliticalView = db.SearchSettings_PoliticalView.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedPoliticalViewHS = new HashSet<int?>(selectedPoliticalView);
            //get the values for this members searchsettings PoliticalView
            var SearchSettingsPoliticalView = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_PoliticalView.Select(c => c.PoliticalViewID));
            foreach (var PoliticalView in db.CriteriaCharacter_PoliticalView)
            {
                if (selectedPoliticalViewHS.Contains(PoliticalView.PoliticalViewID))
                {
                    if (!SearchSettingsPoliticalView.Contains(PoliticalView.PoliticalViewID))
                    {

                        //SearchSettings_PoliticalView.PoliticalViewID = PoliticalView.PoliticalViewID;
                        var temp = new SearchSettings_PoliticalView();
                        temp.PoliticalViewID = PoliticalView.PoliticalViewID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_PoliticalView", temp);

                    }
                }
                else
                {
                    if (SearchSettingsPoliticalView.Contains(PoliticalView.PoliticalViewID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_PoliticalView.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsReligion(int?[] selectedReligion, profiledata ProfileDataToUpdate)
        {
            if (selectedReligion == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Religion  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Religion CurrentSearchSettings_Religion = db.SearchSettings_Religion.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedReligionHS = new HashSet<int?>(selectedReligion);
            //get the values for this members searchsettings Religion
            var SearchSettingsReligion = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Religion.Select(c => c.ReligionID));
            foreach (var Religion in db.CriteriaCharacter_Religion)
            {
                if (selectedReligionHS.Contains(Religion.religionID ))
                {
                    if (!SearchSettingsReligion.Contains(Religion.religionID ))
                    {

                        //SearchSettings_Religion.ReligionID = Religion.ReligionID;
                        var temp = new SearchSettings_Religion();
                        temp.ReligionID = Religion.religionID ;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Religion", temp);

                    }
                }
                else
                {
                    if (SearchSettingsReligion.Contains(Religion.religionID ))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Religion.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsReligiousAttendance(int?[] selectedReligiousAttendance, profiledata ProfileDataToUpdate)
        {
            if (selectedReligiousAttendance == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ReligiousAttendance  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_ReligiousAttendance CurrentSearchSettings_ReligiousAttendance = db.SearchSettings_ReligiousAttendance.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedReligiousAttendanceHS = new HashSet<int?>(selectedReligiousAttendance);
            //get the values for this members searchsettings ReligiousAttendance
            var SearchSettingsReligiousAttendance = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ReligiousAttendance.Select(c => c.ReligiousAttendanceID));
            foreach (var ReligiousAttendance in db.CriteriaCharacter_ReligiousAttendance)
            {
                if (selectedReligiousAttendanceHS.Contains(ReligiousAttendance.ReligiousAttendanceID))
                {
                    if (!SearchSettingsReligiousAttendance.Contains(ReligiousAttendance.ReligiousAttendanceID))
                    {

                        //SearchSettings_ReligiousAttendance.ReligiousAttendanceID = ReligiousAttendance.ReligiousAttendanceID;
                        var temp = new SearchSettings_ReligiousAttendance();
                        temp.ReligiousAttendanceID = ReligiousAttendance.ReligiousAttendanceID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_ReligiousAttendance", temp);

                    }
                }
                else
                {
                    if (SearchSettingsReligiousAttendance.Contains(ReligiousAttendance.ReligiousAttendanceID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_ReligiousAttendance.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsSign(int?[] selectedSign, profiledata ProfileDataToUpdate)
        {
            if (selectedSign == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Sign  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Sign CurrentSearchSettings_Sign = db.SearchSettings_Sign.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedSignHS = new HashSet<int?>(selectedSign);
            //get the values for this members searchsettings Sign
            var SearchSettingsSign = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Sign.Select(c => c.SignID));
            foreach (var Sign in db.CriteriaCharacter_Sign)
            {
                if (selectedSignHS.Contains(Sign.SignID))
                {
                    if (!SearchSettingsSign.Contains(Sign.SignID))
                    {

                        //SearchSettings_Sign.SignID = Sign.SignID;
                        var temp = new SearchSettings_Sign();
                        temp.SignID = Sign.SignID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Sign", temp);

                    }
                }
                else
                {
                    if (SearchSettingsSign.Contains(Sign.SignID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Sign.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsSmokes(int?[] selectedSmokes, profiledata ProfileDataToUpdate)
        {
            if (selectedSmokes == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Smokes  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Smokes CurrentSearchSettings_Smokes = db.SearchSettings_Smokes.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedSmokesHS = new HashSet<int?>(selectedSmokes);
            //get the values for this members searchsettings Smokes
            var SearchSettingsSmokes = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Smokes.Select(c => c.SmokesID));
            foreach (var Smokes in db.CriteriaCharacter_Smokes)
            {
                if (selectedSmokesHS.Contains(Smokes.SmokesID))
                {
                    if (!SearchSettingsSmokes.Contains(Smokes.SmokesID))
                    {

                        //SearchSettings_Smokes.SmokesID = Smokes.SmokesID;
                        var temp = new SearchSettings_Smokes();
                        temp.SmokesID = Smokes.SmokesID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Smokes", temp);

                    }
                }
                else
                {
                    if (SearchSettingsSmokes.Contains(Smokes.SmokesID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Smokes.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsEducationLevel(int?[] selectedEducationLevel, profiledata ProfileDataToUpdate)
        {
            if (selectedEducationLevel == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EducationLevel  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_EducationLevel CurrentSearchSettings_EducationLevel = db.SearchSettings_EducationLevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedEducationLevelHS = new HashSet<int?>(selectedEducationLevel);
            //get the values for this members searchsettings EducationLevel
            var SearchSettingsEducationLevel = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EducationLevel.Select(c => c.EducationLevelID));
            foreach (var EducationLevel in db.CriteriaLife_EducationLevel)
            {
                if (selectedEducationLevelHS.Contains(EducationLevel.EducationLevelID))
                {
                    if (!SearchSettingsEducationLevel.Contains(EducationLevel.EducationLevelID))
                    {

                        //SearchSettings_EducationLevel.EducationLevelID = EducationLevel.EducationLevelID;
                        var temp = new SearchSettings_EducationLevel();
                        temp.EducationLevelID = EducationLevel.EducationLevelID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_EducationLevel", temp);

                    }
                }
                else
                {
                    if (SearchSettingsEducationLevel.Contains(EducationLevel.EducationLevelID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EducationLevel.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsEmploymentStatus(int?[] selectedEmploymentStatus, profiledata ProfileDataToUpdate)
        {
            if (selectedEmploymentStatus == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EmploymentStatus  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_EmploymentStatus CurrentSearchSettings_EmploymentStatus = db.SearchSettings_EmploymentStatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedEmploymentStatusHS = new HashSet<int?>(selectedEmploymentStatus);
            //get the values for this members searchsettings EmploymentStatus
            var SearchSettingsEmploymentStatus = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EmploymentStatus.Select(c => c.EmploymentStatusID));
            foreach (var EmploymentStatus in db.CriteriaLife_EmploymentStatus)
            {
                if (selectedEmploymentStatusHS.Contains(EmploymentStatus.EmploymentSatusID ))
                {
                    if (!SearchSettingsEmploymentStatus.Contains(EmploymentStatus.EmploymentSatusID ))
                    {

                        //SearchSettings_EmploymentStatus.EmploymentStatusID = EmploymentStatus.EmploymentStatusID;
                        var temp = new SearchSettings_EmploymentStatus();
                        temp.EmploymentStatusID = EmploymentStatus.EmploymentSatusID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_EmploymentStatus", temp);

                    }
                }
                else
                {
                    if (SearchSettingsEmploymentStatus.Contains(EmploymentStatus.EmploymentSatusID ))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EmploymentStatus.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsHaveKids(int?[] selectedHaveKids, profiledata ProfileDataToUpdate)
        {
            if (selectedHaveKids == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HaveKids  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_HaveKids CurrentSearchSettings_HaveKids = db.SearchSettings_HaveKids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedHaveKidsHS = new HashSet<int?>(selectedHaveKids);
            //get the values for this members searchsettings HaveKids
            var SearchSettingsHaveKids = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HaveKids.Select(c => c.HaveKidsID));
            foreach (var HaveKids in db.CriteriaLife_HaveKids)
            {
                if (selectedHaveKidsHS.Contains(HaveKids.HaveKidsId ))
                {
                    if (!SearchSettingsHaveKids.Contains(HaveKids.HaveKidsId))
                    {

                        //SearchSettings_HaveKids.HaveKidsID = HaveKids.HaveKidsID;
                        var temp = new SearchSettings_HaveKids();
                        temp.HaveKidsID = HaveKids.HaveKidsId;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_HaveKids", temp);

                    }
                }
                else
                {
                    if (SearchSettingsHaveKids.Contains(HaveKids.HaveKidsId ))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HaveKids.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsIncomeLevel(int?[] selectedIncomeLevel, profiledata ProfileDataToUpdate)
        {
            if (selectedIncomeLevel == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_IncomeLevel  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_IncomeLevel CurrentSearchSettings_IncomeLevel = db.SearchSettings_IncomeLevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedIncomeLevelHS = new HashSet<int?>(selectedIncomeLevel);
            //get the values for this members searchsettings IncomeLevel
            var SearchSettingsIncomeLevel = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_IncomeLevel.Select(c => c.ImcomeLevelID));
            foreach (var IncomeLevel in db.CriteriaLife_IncomeLevel)
            {
                if (selectedIncomeLevelHS.Contains(IncomeLevel.IncomeLevelID))
                {
                    if (!SearchSettingsIncomeLevel.Contains(IncomeLevel.IncomeLevelID))
                    {

                        //SearchSettings_IncomeLevel.IncomeLevelID = IncomeLevel.IncomeLevelID;
                        var temp = new SearchSettings_IncomeLevel();
                        temp.ImcomeLevelID  = IncomeLevel.IncomeLevelID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_IncomeLevel", temp);

                    }
                }
                else
                {
                    if (SearchSettingsIncomeLevel.Contains(IncomeLevel.IncomeLevelID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_IncomeLevel.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsLivingSituation(int?[] selectedLivingSituation, profiledata ProfileDataToUpdate)
        {
            if (selectedLivingSituation == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LivingSituation  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_LivingSituation CurrentSearchSettings_LivingSituation = db.SearchSettings_LivingSituation.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedLivingSituationHS = new HashSet<int?>(selectedLivingSituation);
            //get the values for this members searchsettings LivingSituation
            var SearchSettingsLivingSituation = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LivingStituation.Select(c => c.LivingStituationID));
            foreach (var LivingSituation in db.CriteriaLife_LivingSituation)
            {
                if (selectedLivingSituationHS.Contains(LivingSituation.LivingSituationID))
                {
                    if (!SearchSettingsLivingSituation.Contains(LivingSituation.LivingSituationID))
                    {

                        //SearchSettings_LivingSituation.LivingSituationID = LivingSituation.LivingSituationID;
                        var temp = new  SearchSettings_LivingStituation();
                        temp.LivingStituationID  = LivingSituation.LivingSituationID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_LivingStituation", temp);
                                     
                    }
                }
                else
                {
                    if (SearchSettingsLivingSituation.Contains(LivingSituation.LivingSituationID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_LivingStituation .First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsLookingFor(int?[] selectedLookingFor, profiledata ProfileDataToUpdate)
        {
            if (selectedLookingFor == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LookingFor  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_LookingFor CurrentSearchSettings_LookingFor = db.SearchSettings_LookingFor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedLookingForHS = new HashSet<int?>(selectedLookingFor);
            //get the values for this members searchsettings LookingFor
            var SearchSettingsLookingFor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LookingFor.Select(c => c.LookingForID ));
            foreach (var LookingFor in db.CriteriaLife_LookingFor)
            {
                if (selectedLookingForHS.Contains(LookingFor.LookingForID))
                {
                    if (!SearchSettingsLookingFor.Contains(LookingFor.LookingForID))
                    {

                        //SearchSettings_LookingFor.LookingForID = LookingFor.LookingForID;
                        var temp = new SearchSettings_LookingFor();
                        temp.LookingForID  = LookingFor.LookingForID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_LookingFor", temp);

                    }
                }
                else
                {
                    if (SearchSettingsLookingFor.Contains(LookingFor.LookingForID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_LookingFor.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsMaritalStatus(int?[] selectedMaritalStatus, profiledata ProfileDataToUpdate)
        {
            if (selectedMaritalStatus == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_MaritalStatus  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_MaritalStatus CurrentSearchSettings_MaritalStatus = db.SearchSettings_MaritalStatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedMaritalStatusHS = new HashSet<int?>(selectedMaritalStatus);
            //get the values for this members searchsettings MaritalStatus
            var SearchSettingsMaritalStatus = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_MaritalStatus.Select(c => c.MaritalStatusID));
            foreach (var MaritalStatus in db.CriteriaLife_MaritalStatus)
            {
                if (selectedMaritalStatusHS.Contains(MaritalStatus.MaritalStatusID))
                {
                    if (!SearchSettingsMaritalStatus.Contains(MaritalStatus.MaritalStatusID))
                    {

                        //SearchSettings_MaritalStatus.MaritalStatusID = MaritalStatus.MaritalStatusID;
                        var temp = new SearchSettings_MaritalStatus();
                        temp.MaritalStatusID = MaritalStatus.MaritalStatusID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_MaritalStatus", temp);

                    }
                }
                else
                {
                    if (SearchSettingsMaritalStatus.Contains(MaritalStatus.MaritalStatusID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_MaritalStatus.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsProfession(int?[] selectedProfession, profiledata ProfileDataToUpdate)
        {
            if (selectedProfession == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Profession  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_Profession CurrentSearchSettings_Profession = db.SearchSettings_Profession.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedProfessionHS = new HashSet<int?>(selectedProfession);
            //get the values for this members searchsettings Profession
            var SearchSettingsProfession = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Profession.Select(c => c.ProfessionID));
            foreach (var Profession in db.CriteriaLife_Profession)
            {
                if (selectedProfessionHS.Contains(Profession.ProfessionID))
                {
                    if (!SearchSettingsProfession.Contains(Profession.ProfessionID))
                    {

                        //SearchSettings_Profession.ProfessionID = Profession.ProfessionID;
                        var temp = new SearchSettings_Profession();
                        temp.ProfessionID = Profession.ProfessionID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_Profession", temp);

                    }
                }
                else
                {
                    if (SearchSettingsProfession.Contains(Profession.ProfessionID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Profession.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }

        private void UpdateSearchSettingsWantsKids(int?[] selectedWantsKids, profiledata ProfileDataToUpdate)
        {
            if (selectedWantsKids == null)
            {
                // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_WantsKids  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
            //SearchSettings_WantsKids CurrentSearchSettings_WantsKids = db.SearchSettings_WantsKids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            var selectedWantsKidsHS = new HashSet<int?>(selectedWantsKids);
            //get the values for this members searchsettings WantsKids
            var SearchSettingsWantsKids = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_WantKids.Select(c => c.WantKidsID));
            foreach (var WantsKids in db.CriteriaLife_WantsKids)
            {
                if (selectedWantsKidsHS.Contains(WantsKids.WantsKidsID))
                {
                    if (!SearchSettingsWantsKids.Contains(WantsKids.WantsKidsID))
                    {

                        //SearchSettings_WantsKids.WantsKidsID = WantsKids.WantsKidsID;
                        var temp = new SearchSettings_WantKids ();
                        temp.WantKidsID = WantsKids.WantsKidsID;
                        temp.SearchSettingsID = SearchSettingsID;

                        db.AddObject("SearchSettings_WantKids", temp);

                    }
                }
                else
                {
                    if (SearchSettingsWantsKids.Contains(WantsKids.WantsKidsID))
                    {

                        var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_WantKids.First();
                        db.DeleteObject(Temp);

                    }
                }
            }
        }
        #endregion

        #region "profile visisiblity settings update here"

        public bool UpdateProfileVisibilitySettings(ProfileVisiblitySetting model)
        {
           if( model.ProfileID != null)
            {
               datingservice.UpdateProfileVisiblitySetting(model);

               return true;
            }
           return false;
        }
        #endregion

        #region "Custom functions used for photo editing here"






        public IQueryable<photo> MyPhotos(string username)
        {
            // Retrieve All User's Photo's that have not deleted by Admin and User.
            IQueryable<photo> query = db.photos.Where(o => o.profiledata.profile.UserName == username
                                && o.PhotoStatusID != 4 && o.PhotoStatusID !=5);

            return query;
        }

        public IEnumerable<EditProfileViewPhotoModel> GetApproved(IQueryable<photo> MyPhotos, string approved,
                                                                    int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.Aproved == approved);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            if (approved == "Yes") { photos = photos.Where(a => a.PhotoStatusID != 3); }          
            
            var model = (from p in photos              
                         select new EditProfileViewPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ScreenName = p.profiledata.profile.ScreenName ,
                             CheckedPhoto = false,
                             PhotoDate = p.PhotoDate
                         });
            

            if (model.Count() > pagesize) { pagesize = model.Count(); }
           
            
            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize));

           

        }
        //gets all approved non prviate photos athat are not gallery 
        public IEnumerable<EditProfileViewPhotoModel> GetApprovedMinusGallery(IQueryable<photo> MyPhotos, string approved,
                                                                int page, int pagesize)
        {
            // Retrieve All User's Photos that are not approved.
            var photos = MyPhotos.Where(a => a.Aproved == approved);

            // Retrieve All User's Approved Photo's that are not Private and approved.
            if (approved == "Yes") { photos = photos.Where(a => a.PhotoStatusID != 3 && a.ProfileImageType != "Gallery"); }

            var model = (from p in photos
                         select new EditProfileViewPhotoModel
                         {
                             PhotoID = p.PhotoID,
                             ScreenName = p.profiledata.profile.ScreenName,
                             CheckedPhoto = false,
                             PhotoDate = p.PhotoDate
                         });


            if (model.Count() > pagesize) { pagesize = model.Count(); }

            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize));

        }

         public IEnumerable<EditProfileViewPhotoModel> GetPhotoByStatusID(IQueryable<photo> MyPhotos, int photoStatusID,
                                                                    int page, int pagesize)
        {
            var model = (from p in MyPhotos.Where(a => a.PhotoStatusID == photoStatusID)
                    select new EditProfileViewPhotoModel
                    {
                        PhotoID = p.PhotoID,
                        ScreenName = p.profiledata.profile.ScreenName,
                        CheckedPhoto = false,
                        PhotoDate = p.PhotoDate
                    });

            if (model.Count() > pagesize) { pagesize = model.Count(); }
            return (model.OrderByDescending(u => u.PhotoDate).Skip((page - 1) * pagesize).Take(pagesize));

        }             

        public EditProfilePhotosViewModel GetPhotoViewModel(IEnumerable<EditProfileViewPhotoModel> Approved,
                                                            IEnumerable<EditProfileViewPhotoModel> NotApproved,
                                                            IEnumerable<EditProfileViewPhotoModel> Private,
                                                            IQueryable<photo> model)
        {
            // Retrieve singlephotoProfile from either the approved model or photo model
            PhotoEditViewModel src = new PhotoEditViewModel();
            if (Approved.Count() > 0)
            {
                src = (from p in model
                                join x in Approved
                                on p.PhotoID equals x.PhotoID
                                select new PhotoEditViewModel
                                {
                                    PhotoID = p.PhotoID,
                                    ScreenName = p.profiledata.profile.ScreenName,
                                    ProfileID = p.ProfileID,
                                    Aproved = p.Aproved,
                                    ProfileImageType = p.ProfileImageType,
                                    ImageCaption = p.ImageCaption,
                                    PhotoDate = p.PhotoDate,
                                    PhotoStatusID = p.PhotoStatusID
                                } )
                             .OrderBy(o => o.ProfileImageType).ThenByDescending(o => o.PhotoDate)
                             .FirstOrDefault();
            }
            else
            {
                 src = (from p in model
                                select new PhotoEditViewModel
                                {
                                    PhotoID = p.PhotoID,
                                    ScreenName = p.profiledata.profile.ScreenName,
                                    ProfileID = p.ProfileID,
                                    Aproved = p.Aproved,
                                    ProfileImageType = p.ProfileImageType,
                                    ImageCaption = p.ImageCaption,
                                    PhotoDate = p.PhotoDate,
                                    PhotoStatusID = p.PhotoStatusID
                                } )
                                .OrderByDescending(o => o.PhotoDate)
                                .FirstOrDefault();
            }

            try
            {
                src.checkedPrimary = src.BoolImageType(src.ProfileImageType.ToString());
            }
            catch
            { 
            
            
            }

            photoeditmodel UploadPhotos = new photoeditmodel();

            EditProfilePhotosViewModel ViewModel = new EditProfilePhotosViewModel { 
                SingleProfilePhoto = src,
                ProfilePhotosApproved = Approved.ToList(),
                ProfilePhotosNotApproved = NotApproved.ToList(),
                ProfilePhotosPrivate = Private.ToList(),
                Photo = UploadPhotos};


            return (ViewModel);


        }
        public PhotoEditViewModel GetSingleProfilePhotoByphotoID(Guid photoid)
        {
            PhotoEditViewModel model = (from p in db.photos.Where(p => p.PhotoID == photoid)
                                         select new PhotoEditViewModel
                                         {
                                             PhotoID = p.PhotoID,
                                             ProfileID = p.ProfileID,
                                             ScreenName = p.profiledata.profile.ScreenName,
                                             Aproved = p.Aproved,
                                             ProfileImageType = p.ProfileImageType,
                                             ImageCaption = p.ImageCaption,
                                             PhotoDate = p.PhotoDate,
                                             PhotoStatusID = p.PhotoStatusID
                                         }).Single();
            model.checkedPrimary = model.BoolImageType(model.ProfileImageType.ToString());

            //Product product789 = products.FirstOrDefault(p => p.ProductID == 789);



            return (model);


        }
        public EditProfilePhotosViewModel GetEditPhotoModel(string UserName, string ApprovedYes,string NotApprovedNo,
                                                            int photoStatusID, int page, int pagesize)
        {

            var myPhotos = MyPhotos(UserName);
            var Approved = GetApproved(myPhotos, ApprovedYes, page, pagesize);
            var NotApproved = GetApproved(myPhotos, NotApprovedNo, page, pagesize);
            var PhotoStatus = GetPhotoByStatusID(myPhotos, photoStatusID, page, pagesize);
            var model = GetPhotoViewModel(Approved, NotApproved, PhotoStatus, myPhotos);

            return (model);


        }         
       
        public void DeletedUserPhoto(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = db.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.PhotoStatusID = 4; //deletebyuser
            PhotoModify.ProfileImageType = "NoStatus";
            // Update database
            db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            db.SaveChanges();
        }
        public void MakeUserPhoto_Private(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = db.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.PhotoStatusID = 3; //private
            PhotoModify.ProfileImageType = "NoStatus";
            // Update database
            db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            db.SaveChanges();
        }
        public void MakeUserPhoto_Public(Guid PhotoID)
        {
            // Retrieve single value from photos table
            photo PhotoModify = db.photos.Single(u => u.PhotoID == PhotoID);
            PhotoModify.PhotoStatusID = 1; //public values:1 or 2 are public values
            // Update database
            db.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
            db.SaveChanges();
        }

        #endregion

    }
}