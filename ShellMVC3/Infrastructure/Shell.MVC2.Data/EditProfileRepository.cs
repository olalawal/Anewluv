using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;
using System.Data;

namespace Shell.MVC2.Data
{


    //TO DO move this data out to a Searchsettings model and create a full view model maybe 
    //that way the edit code is re-usuable, final code change will be search settings object
    //i.e appearancesearchsettings  , and appearancesettings  combine into viewmodel
    //the search peice will be udpated via searchrepostiory as a separate call maybe since even the matches 
    //settings which we are updating is actually just a search

    /////################# TO DO make all the invidual save for the per page basis private , i.e save everything as one big blob since we do
    //// not want to limit how the UI creators set up thier UI.  Reuturn all errors with the AnewLuv Messages thing for the UPDATEs and have the UI creator
    /// navigate the user to the pages with issues them selvs.

/// <summary>
/// TO DO split off search settings methods , if needed they should be references as an interface
/// </summary>
   public  class EditMemberRepository : MemberRepositoryBase  
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();    
        private IMemberRepository _membersrepository;
        private ISearchRepository  _searchrepository;
        
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();

        public class CheckBox
        {
            public int id { get; set; }
            public string description { get; set; }
            public bool selected { get; set; }
        }

        public EditMemberRepository(AnewluvContext datingcontext, IMemberRepository membersrepository,ISearchRepository searchrepository)
            : base(datingcontext)
        {
            _membersrepository = membersrepository;
            _searchrepository = searchrepository;
        }

        #region "profile visisiblity settings update here"

        public bool UpdateProfileVisibilitySettings(visiblitysetting model)
        {
            if (model.id != null)
            {

                //Impement on member service ?
                // datingservice.UpdateProfileVisiblitySetting(model);


                return true;
            }
            return false;
        }
        #endregion


        #region "Methods to GET current edit profile settings for a user"
       
        // constructor
       public BasicSettingsModel  getbasicsettingsmodel(int intprofileid)
            {
                try
                {

                    profile p = db.profiles.Where(z => z.id  == intprofileid).FirstOrDefault();
                    BasicSettingsModel model = new BasicSettingsModel();

                    //populate values here ok ?
                    if (p != null)


                   // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                    //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                   // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();

                    //populate ages select list here I guess
                    //TODO get from app fabric
                    // SharedRepository sharedrepository = new SharedRepository();
                    //Ages = sharedrepository.AgesSelectList;

                       model.birthdate  = p.profiledata.birthdate ; //== null ? null :  p.profiledata.birthdate;
                  //  model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                       model.gender   =   p.profiledata.gender  == null ? null : p.profiledata.gender  ;
                       model.countryid   = p.profiledata.countryid   == null ? null : p.profiledata.countryid   ;
                       model.city    =p.profiledata.city     ==  null ? null : p.profiledata.city    ;
                       model.postalcode   = p.profiledata.postalcode    == null ? null : p.profiledata.postalcode    ;
                       model.aboutme  = p.profiledata.aboutme    == null ? null : p.profiledata.aboutme    ;                   
                       model.phonenumber   = p.profiledata.phone    == null ? null : p.profiledata.phone    ;

                                       
                   

                    return model;
                }
                         catch (DataException dx)
          {     
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
           }
           catch (Exception ex)
           {
               //handle logging here
               var message = ex.Message;
               throw ex;

           }
        
            }       
       //Using a contstructor populate the current values I suppose
       //The actual values will bind to viewmodel I think
       public AppearanceSettingsModel  getappearancesettingsmodel(int intprofileid)
            {


             try
             {
                    
            profile p = db.profiles.Where(z => z.id  == intprofileid).FirstOrDefault();
            AppearanceSettingsModel model = new AppearanceSettingsModel();

             model.height   = p.profiledata.height    == null ? null : p.profiledata.height;        
             model.bodytype = p.profiledata.bodytype  == null ? null : p.profiledata.bodytype;
             model.haircolor   = p.profiledata.haircolor     == null ? null : p.profiledata.haircolor     ;
             model.eyecolor = p.profiledata.eyecolor    == null ? null : p.profiledata.eyecolor     ;
                   
       
               //pilot how to show the rest of the values 
               //sample of doing string values
              // var allhotfeature = db.lu_hotfeature;
               //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                    var hotfeaturevalues = new HashSet<int>(p.profilemetadata.hotfeatures .Select(c => c.hotfeature.id));
                    foreach (var _hotfeature in  db.lu_hotfeature)
                    {
                        model.hotfeaturelist.Add(new lu_hotfeature
                        {
                            id = _hotfeature.id,
                            description = _hotfeature.description,
                            selected = hotfeaturevalues.Contains(_hotfeature.id)
                        });
                    }

               //model.ethnicitylist = p.profilemetadata.ethnicities.ToList();
                    var ethnicityvalues = new HashSet<int>(p.profilemetadata.ethnicities .Select(c => c.ethnicty.id));
                    foreach (var _ethnicity in  db.lu_ethnicity)
                    {
                        model.ethnicitylist.Add(new lu_ethnicity
                        {
                            id = _ethnicity.id,
                            description = _ethnicity.description,
                            selected = ethnicityvalues.Contains(_ethnicity.id)
                        });
                    }


                 return model ;
              
              }
              catch (DataException dx)
              {     
                   //Log the error (add a variable name after DataException) 
                   // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                   // return model;
                   //handle logging here
                   var message = dx.Message;
                   throw dx;
               }
               catch (Exception ex)
               {
                   //handle logging here
                   var message = ex.Message;
                   throw ex;

               }
          
       }
         
       //populate the enities
       public LifeStyleSettingsModel getlifestylesettingsmodel(int intprofileid)
            {

                try
                {                              
                    profile p = db.profiles.Where(z => z.id  == intprofileid).FirstOrDefault();
                    LifeStyleSettingsModel model = new LifeStyleSettingsModel();                 
                      model.educationlevel =p.profiledata.educationlevel ;   
                      model.employmentstatus =p.profiledata.employmentstatus ;      
                      model. incomelevel = p.profiledata.incomelevel ;       
                      
                    var lookingforvalues = new HashSet<int>(p.profilemetadata.lookingfor.Select(c => c.lookingfor.id));
                    foreach (var _lookingfor in  db.lu_lookingfor)
                    {
                        model.lookingforlist.Add(new lu_lookingfor
                        {
                            id = _lookingfor.id,
                            description = _lookingfor.description,
                            selected = lookingforvalues.Contains(_lookingfor.id)
                        });
                    }
                  
      
                      model. wantskids = p.profiledata.wantsKidstatus;       
                      model.profession = p.profiledata.profession ;       
                      model.maritalstatus = p.profiledata.maritalstatus ;      
                      model.livingsituation =p.profiledata.livingsituation ;      
                      model.havekids =  p.profiledata.kidstatus ;

                      return model ;
                }
                catch (DataException dx)
                {     
                   //Log the error (add a variable name after DataException) 
                   // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                   // return model;
                   //handle logging here
                   var message = dx.Message;
                   throw dx;
               }
               catch (Exception ex)
               {
                   //handle logging here
                   var message = ex.Message;
                   throw ex;

               }
               return null;
             }     
         //Using a contstructor populate the current values I suppose
            //The actual values will bind to viewmodel I think
       public CharacterSettingsModel getcharactersettingsmodel(int intprofileid)
            {

                try
                {
                    profile p = db.profiles.Where(z => z.id  == intprofileid).FirstOrDefault();
                    CharacterSettingsModel model = new CharacterSettingsModel();    

                    model.diet = p.profiledata.diet ;
                    model.humor = p.profiledata.humor ;
                  
                    //populiate the hobby list, remeber this comes from the metadata link so you have to drill down
                    //var allhobbies = db.lu_hobby;
                    var hobbyvalues = new HashSet<int>(p.profilemetadata.hobbies.Select(c => c.hobby.id));
                    foreach (var _hobby in  db.lu_hobby)
                    {
                        model.hobbylist.Add(new lu_hobby
                        {
                            id = _hobby.id,
                            description = _hobby.description,
                            selected = hobbyvalues.Contains(_hobby.id)
                        });
                    }
                  
                    model.drinking = p.profiledata.drinking ;
                    model.excercise = p.profiledata.exercise ;
                    model.smoking = p.profiledata.smoking ;
                    model.sign = p.profiledata.sign ;
                    model.politicalview = p.profiledata.politicalview ;
                    model.myreligiousattendance = p.profiledata.religiousattendance ;

                    return model;
                }
              catch (DataException dx)
                {     
                   //Log the error (add a variable name after DataException) 
                   // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                   // return model;
                   //handle logging here
                   var message = dx.Message;
                   throw dx;
               }
               catch (Exception ex)
               {
                   //handle logging here
                   var message = ex.Message;
                   throw ex;

               }
               return null;
            }
       
        #endregion


//Edit Profile Settings Occur here.
       //here are the methdods that actually modify settings i.e old UI vs new
       
       #region "Edit profile Basic Settings Updates here
   
       public AnewluvMessages  EditProfileBasicSettings(BasicSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

           //get the profile details :
        
           //create the search settings i.e matches if it does not exist 
          // if (profile.profilemetadata.searchsettings.Count() == 0) _membersrepository.createmyperfectmatchsearchsettingsbyprofileid(_ProfileID);
          // searchsetting SearchSettingsToUpdate = db.searchsetting.Where(p => p.profile_id  == _ProfileID && p.myperfectmatch == true && p.searchname  == "MyPerfectMatch").First();

           //TO DO this might be suplerflous ?
           //var  newmodel2 = this.getbasicsettingsmodel(profile.id);  

            messages=(EditProfileBasicSettingsUpdate(newmodel,profileid, messages));
          //  messages=(EditProfileBasicSettingsPage2Update(newmodel,profileid ,messages));


            if (messages.errormessages.Count > 0)
            {
                messages.message = "There was a problem Editing You Basic Settings, Please try again later";
               return messages ;
            }
              messages.message = "Edit Basic Settings Successful" ;
              return messages ;        
       }

       //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
       private AnewluvMessages EditProfileBasicSettingsUpdate(BasicSettingsModel newmodel,int profileid, AnewluvMessages messages)
       {

           try
           {
               profile p = db.profiles.Where(p => p.id == profileid).First();
         
               //TO DO
                //Up here we will check to see if the values have not changed 
               var birthdate = newmodel.birthdate ;
               var AboutMe = newmodel.aboutme  ;
               var MyCatchyIntroLine = newmodel.catchyintroline;
               var city = newmodel.city ;
               var stateprovince = newmodel.stateprovince;
               var countryid = newmodel.countryid ;
               var gender = newmodel.gender ;
               var postalcode = newmodel.postalcode;
               var dd = newmodel.phonenumber ;
               //get current values from DB in case some values were not updated
                                        
               //link the profiledata entities
               p.modificationdate = DateTime.Now;
               //manually update model i think
               //set properties in the about me
               p.profiledata.aboutme = AboutMe;
               p.profiledata.birthdate = birthdate;
               p.profiledata.mycatchyintroLine = MyCatchyIntroLine;
              
               db.SaveChanges();
               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //return newmodel;
           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
           }
           catch (Exception ex)
           {
               //handle logging here
               var message = ex.Message;
               throw ex;

           }
           return messages ;
       }
       //TO DO add validation and pass back via messages 
     
       #endregion


       //#region "other editpages to implement"
       #region "Edit profile Appeareance Settings Updates here"

       public AnewluvMessages editprofileappearancesettings(AppearanceSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

     
          // var newmodel2 = this.getAppearancesettingsviewmodel(profile.id);

           messages = (updateprofileappearancesettings(newmodel, profileid, messages));
          // messages = (EditProfileAppearanceSettingsPage2Update(newmodel, profileid, messages));
          // messages = (EditProfileAppearanceSettingsPage3Update(newmodel, profileid, messages));
         //  messages = (EditProfileAppearanceSettingsPage4Update(newmodel, profileid, messages));

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You Appearance Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit Appearance Settings Successful";
           return messages;
       }

       //TO DO send back the messages on errors and when nothing is changed
       private AnewluvMessages updateprofileappearancesettings(AppearanceSettingsModel  newmodel,int profileid, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
               profile p = db.profiles.Where(z => z.id == profileid).First();
             //sample code for determining weather to edit an item or not or determin if a value changed'
             //nothingupdated = (newmodel.height  == p.profiledata.height) ? false : true;

             //only update items that are not null
             var height = (newmodel.height == p.profiledata.height) ? newmodel.height : null;
             var bodytype = (newmodel.bodytype == p.profiledata.bodytype ) ? newmodel.bodytype : null ;
             var haircolor = (newmodel.haircolor  == p.profiledata.haircolor ) ? newmodel.haircolor : null;
             var eyecolor = (newmodel.eyecolor  == p.profiledata.eyecolor ) ? newmodel.eyecolor : null;
             //TO DO test if anything changed
             var hotfeatures = newmodel.hotfeaturelist;
             //TO DO test if anything changed
             var ethicities = newmodel.ethnicitylist;
           


               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

             if (height.HasValue == true) p.profiledata.height = height;
             if (bodytype  != null) p.profiledata.bodytype  = bodytype ;
             if (haircolor  != null) p.profiledata.haircolor  = haircolor ;
             if (eyecolor  != null) p.profiledata.eyecolor = eyecolor ;
             if (hotfeatures.Count >0) p.profiledata.height = height;
             if (height.HasValue == true) p.profiledata.height = height;
             if (height.HasValue == true) p.profiledata.height = height;

             if (hotfeatures.Count > 0)
                 updateprofilemetatdatahotfeature(hotfeatures, p.profilemetadata);
             if (ethicities.Count > 0)
               updateprofilemetatdataethnicity(ethicities, p.profilemetadata);
               

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //membersmodel.profiledata = profiledata;               

               //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
               //model.CurrentErrors.Clear();
              // return model;
           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
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


       #region "Edit profile LifeStyle Settings Updates here"

       public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage1Update(EditProfileSettingsViewModel model,
       FormCollection formCollection, int?[] SelectedYourMaritalStatusIds, int?[] SelectedYourLivingSituationIds,
           int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, string _ProfileID)
       {
           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MyMaritalStatusID = model.LifeStyleSettings.MaritalStatusID;
           var MyLivingSituationID = model.LifeStyleSettings.LivingSituationID;
           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(profiledata);
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
               _LookingFor.Selected = MyLookingForValues != null ? MyLookingForValues.Contains(_LookingFor.MyLookingForID) : false;
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



           // profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {
                profile.modificationdate = DateTime.Now;
               profiledata.MaritalStatusID = Convert.ToInt32(model.LifeStyleSettings.MaritalStatusID);
               profiledata.LivingSituationID = model.LifeStyleSettings.LivingSituationID;


               UpdateProfileDataLookingFor(SelectedMyLookingForIds,oldsearchsettings);
               UpdateSearchSettingsLookingFor(SelectedYourLookingForIds,oldsearchsettings);
               UpdateSearchSettingsMaritalStatus(SelectedYourMaritalStatusIds,oldsearchsettings);
               UpdateSearchSettingsLivingSituation(SelectedYourLivingSituationIds,oldsearchsettings);
               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;


               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //membersmodel.profiledata = profiledata;

               CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID,oldsearchsettings);


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

           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MyWantKidsID = model.LifeStyleSettings.WantsKidsID;
           var MyHaveKidsID = model.LifeStyleSettings.HaveKidsId;
           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(profiledata);
           model.LifeStyleSettings.WantsKidsID = MyWantKidsID;
           model.LifeStyleSettings.HaveKidsId = MyHaveKidsID;


           //reload search settings since it seems the checkbox values are lost on postback
           //we really should just rebuild them from form collection imo
           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
           //update the reloaded  searchmodl settings with current settings on the UI


           //update the searchmodl settings with current settings            
           //update UI display values with current displayed values as well for check boxes



           IEnumerable<int?> EnumerableYourWantsKids = SelectedYourWantsKidsIds;

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



           //profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {



                profile.modificationdate = DateTime.Now;

               profiledata.WantsKidsID = Convert.ToInt32(model.LifeStyleSettings.WantsKidsID);
               profiledata.HaveKidsId = model.LifeStyleSettings.HaveKidsId;



               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //membersmodel.profiledata = profiledata;

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

           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MyIncomeLevelID = model.LifeStyleSettings.IncomeLevelID;
           var MyEmploymentStatusID = model.LifeStyleSettings.EmploymentStatusID;
           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(profiledata);
           model.LifeStyleSettings.IncomeLevelID = MyIncomeLevelID;
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



           //profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {
                profile.modificationdate = DateTime.Now;

               profiledata.IncomeLevelID = Convert.ToInt32(model.LifeStyleSettings.IncomeLevelID);
               profiledata.EmploymentSatusID = model.LifeStyleSettings.EmploymentStatusID;



              
               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;


               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               // membersmodel.profiledata = profiledata;
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

           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
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
           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(profiledata);
           model.LifeStyleSettings.ProfessionID = MyProfessionID;
           model.LifeStyleSettings.EducationLevelID = MyEducationLevelID;


           //reload search settings since it seems the checkbox values are lost on postback
           //we really should just rebuild them from form collection imo
           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
           //update the reloaded  searchmodl settings with current settings on the UI


           //update the searchmodl settings with current settings            
           //update UI display values with current displayed values as well for check boxes





           //profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {

                profile.modificationdate = DateTime.Now;
               profiledata.ProfessionID = Convert.ToInt32(model.LifeStyleSettings.ProfessionID);
               profiledata.EducationLevelID = model.LifeStyleSettings.EducationLevelID;
            


               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;
               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //membersmodel.profiledata = profiledata;

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
           int?[] SelectedYourExerciseIds, int?[] SelectedYourSmokesIds, string _ProfileID)
       {
           //5-10-2012 moved this to get these items first.
           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
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
           var MyExerciseID = model.CharacterSettings.ExerciseID;
           var MySmokesID = model.CharacterSettings.SmokesID;
           //TO DO read from name value collection to incrfeate efficency
           model.CharacterSettings = new EditProfileCharacterSettingsModel(profiledata);
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




           //profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {
                profile.modificationdate = DateTime.Now;

               profiledata.DietID = Convert.ToInt32(model.CharacterSettings.DietID);
               profiledata.DrinksID = model.CharacterSettings.DrinksID;
               profiledata.ExerciseID = model.CharacterSettings.ExerciseID;
               profiledata.SmokesID = model.CharacterSettings.SmokesID;


               UpdateSearchSettingsExercise(SelectedYourExerciseIds,oldsearchsettings);
               UpdateSearchSettingsDiet(SelectedYourDietIds,oldsearchsettings);
               UpdateSearchSettingsDrinks(SelectedYourDrinksIds,oldsearchsettings);
               UpdateSearchSettingsSmokes(SelectedYourSmokesIds,oldsearchsettings);

               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               // membersmodel.profiledata = profiledata;

               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID,oldsearchsettings);


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
   FormCollection formCollection, int?[] SelectedYourHobbyIds, int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds,
        string _ProfileID)
       {
           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
           //5-10-2012 moved this to get these items first.
           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); // db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();



           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MySignID = model.CharacterSettings.SignID;
           model.CharacterSettings = new EditProfileCharacterSettingsModel(profiledata);
           model.CharacterSettings.SignID = MySignID;



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



           // profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {

                profile.modificationdate = DateTime.Now;

               profiledata.SignID = Convert.ToInt32(model.CharacterSettings.SignID);




               UpdateProfileDataHobby(SelectedMyHobbyIds,oldsearchsettings);
               UpdateSearchSettingsHobby(SelectedYourHobbyIds,oldsearchsettings);
               UpdateSearchSettingsSign(SelectedYourSignIds,oldsearchsettings);

               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               // membersmodel.profiledata = profiledata;

               // CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID,oldsearchsettings);

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
           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MyReligiousAttendanceID = model.CharacterSettings.ReligiousAttendanceID;
           var MyReligionID = model.CharacterSettings.ReligionID;
           model.CharacterSettings = new EditProfileCharacterSettingsModel(profiledata);
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



           //   profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {
                profile.modificationdate = DateTime.Now;

               profiledata.ReligiousAttendanceID = Convert.ToInt32(model.CharacterSettings.ReligiousAttendanceID);
               profiledata.ReligionID = Convert.ToInt32(model.CharacterSettings.ReligionID);
               profiledata.EmploymentSatusID = model.CharacterSettings.ReligionID;


               UpdateSearchSettingsReligiousAttendance(SelectedYourReligiousAttendanceIds,oldsearchsettings);
               UpdateSearchSettingsReligion(SelectedYourReligionIds,oldsearchsettings);

               //added modifciation date 1-9-2012 , confirm that it works as an inclided
                profile.modificationdate = DateTime.Now;
               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               // membersmodel.profiledata = profiledata;

               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID,oldsearchsettings);


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
           profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
           if (profiledata.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

           //re populate the models TO DO not sure this is needed index valiues are stored
           //if there are checkbox values on basic settings we would need to reload as well
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           //temp store values on UI also handle ANY case here !!
           //just for conistiancy.
           var MyHumorID = model.CharacterSettings.HumorID;
           var MyPoliticalViewID = model.CharacterSettings.PoliticalViewID;
           model.CharacterSettings = new EditProfileCharacterSettingsModel(profiledata);
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



           // profiledata profiledata = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
           //   SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


           try
           {
                profile.modificationdate = DateTime.Now;

               profiledata.HumorID = Convert.ToInt32(model.CharacterSettings.HumorID);
               profiledata.PoliticalViewID = Convert.ToInt32(model.CharacterSettings.PoliticalViewID);
               profiledata.EmploymentSatusID = model.CharacterSettings.PoliticalViewID;



               UpdateSearchSettingsHumor(SelectedYourHumorIds,oldsearchsettings);
               UpdateSearchSettingsPoliticalView(SelectedYourPoliticalViewIds,oldsearchsettings);



               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               // membersmodel.profiledata = profiledata;

               CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID,oldsearchsettings);


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
       

       //TO DO move to search setting repo i think

       

       #region "Checkbox Update Functions for profiledata many to many"


       //profiledata ethnicity
       private void updateprofilemetatdataethnicity(List<lu_ethnicity> selectedethnicity, profilemetadata currentprofilemetadata)
       {
           if (selectedethnicity == null)
           {
               // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var ethnicity in db.lu_ethnicity)
           {
               if (selectedethnicity.Contains(ethnicity))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == ethnicity.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_ethnicity();
                       temp.id = ethnicity.id;
                       temp.profile_id = currentprofilemetadata.profile_id ;
                       db.ethnicities.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.ethnicities.Any(p => p.id == ethnicity.id))
                   {
                       var temp = db.ethnicities.Where(p => p.profile_id == currentprofilemetadata.profile_id  && p.ethnicty.id == ethnicity.id).First();
                       db.ethnicities.Remove(temp);

                   }
               }
           }
       }
       //profiledata hotfeature
       private void updateprofilemetatdatahotfeature(List<lu_hotfeature> selectedhotfeature, profilemetadata currentprofilemetadata)
       {
           if (selectedhotfeature == null)
           {
               // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var hotfeature in db.lu_hotfeature)
           {
               if (selectedhotfeature.Contains(hotfeature))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == hotfeature.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_hotfeature();
                       temp.id = hotfeature.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.hotfeatures.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.hotfeatures.Any(p => p.id == hotfeature.id))
                   {
                       var temp = db.hotfeatures.Where(p => p.profile_id == currentprofilemetadata.profile_id  && p.hotfeature.id == hotfeature.id).First();
                       db.hotfeatures.Remove(temp);

                   }
               }
           }
       }
       //profiledata hobby
       private void updateprofilemetatdatahobby(List<lu_hobby> selectedhobby, profilemetadata currentprofilemetadata)
       {
           if (selectedhobby == null)
           {
               // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var hobby in db.lu_hobby)
           {
               if (selectedhobby.Contains(hobby))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == hobby.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_hobby();
                       temp.id = hobby.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.hobbies .Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.hobbies.Any(p => p.id == hobby.id))
                   {
                       var temp = db.hobbies.Where(p => p.profile_id ==currentprofilemetadata.profile_id && p.hobby.id == hobby.id).First();
                       db.hobbies.Remove(temp);

                   }
               }
           }
       }
       //profiledata lookingfor
       private void updateprofilemetatdatalookingfor(List<lu_lookingfor> selectedlookingfor, profilemetadata currentprofilemetadata)
       {
           if (selectedlookingfor == null)
           {
               // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var lookingfor in db.lu_lookingfor)
           {
               if (selectedlookingfor.Contains(lookingfor))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == lookingfor.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_lookingfor();
                       temp.id = lookingfor.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.lookingfor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.lookingfor.Any(p => p.id == lookingfor.id))
                   {
                       var temp = db.lookingfor.Where(p => p.profile_id ==currentprofilemetadata.profile_id && p.lookingfor.id == lookingfor.id).First();
                       db.lookingfor.Remove(temp);

                   }
               }
           }
       }

       #endregion

       

      
       
    }
}
