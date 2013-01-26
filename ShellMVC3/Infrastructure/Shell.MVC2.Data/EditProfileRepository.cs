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



    //TO DO update all these methods to uload the current searchsetting into cache as needed i.e after retriveal
    //at least for the perfect match
/// <summary>
/// TO DO split off search settings methods , if needed they should be references as an interface
/// </summary>
   public  class EditMemberRepository : MemberRepositoryBase  
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();    
        private IMemberRepository _membersrepository;
        private IEditSearchRepository  _searchrepository;
        
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();

        public class CheckBox
        {
            public int id { get; set; }
            public string description { get; set; }
            public bool selected { get; set; }
        }

        public EditMemberRepository(AnewluvContext datingcontext, IMemberRepository membersrepository,IEditSearchRepository searchrepository)
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
                    model.religiousattendance = p.profiledata.religiousattendance ;

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
       
        #endregion


//Edit Profile Settings Occur here.
       //here are the methdods that actually modify settings i.e old UI vs new

       #region "Methods to Update profile settings for a user"

       #region "Edit profile Basic Settings Updates here

       public AnewluvMessages editprofilebasicsettings(BasicSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

           //get the profile details :
           profile p = db.profiles.Where(z => z.id == profileid).First(); 

           
           messages = (updateprofilebasicsettings(newmodel, p, messages));
           //  messages=(EditProfileBasicSettingsPage2Update(newmodel,profileid ,messages));


           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You Basic Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit Basic Settings Successful";
           return messages;
       }

       //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
       private AnewluvMessages updateprofilebasicsettings(BasicSettingsModel newmodel, profile p, AnewluvMessages messages)
       {

           try
           {
             //  profile p = db.profiles.Where(z => z.id == profileid).First();

               //TO DO
               //Up here we will check to see if the values have not changed 
               var birthdate = newmodel.birthdate;
               var AboutMe = newmodel.aboutme;
               var MyCatchyIntroLine = newmodel.catchyintroline;
               var city = newmodel.city;
               var stateprovince = newmodel.stateprovince;
               var countryid = newmodel.countryid;
               var gender = newmodel.gender;
               var postalcode = newmodel.postalcode;
               var dd = newmodel.phonenumber;
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
           return messages;
       }
       //TO DO add validation and pass back via messages 

       #endregion


       //#region "other editpages to implement"
       #region "Edit profile Appeareance Settings Updates here"

       public AnewluvMessages editprofileappearancesettings(AppearanceSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();


           //get the profile details :
           profile p = db.profiles.Where(z => z.id == profileid).First(); 

           messages = (updateprofileappearancesettings(newmodel, p, messages));
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
       private AnewluvMessages updateprofileappearancesettings(AppearanceSettingsModel newmodel, profile p, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
               //profile p = db.profiles.Where(z => z.id == profileid).First();
               //sample code for determining weather to edit an item or not or determin if a value changed'
               //nothingupdated = (newmodel.height  == p.profiledata.height) ? false : true;

               //only update items that are not null
               var height = (newmodel.height == p.profiledata.height) ? newmodel.height : null;
               var bodytype = (newmodel.bodytype == p.profiledata.bodytype) ? newmodel.bodytype : null;
               var haircolor = (newmodel.haircolor == p.profiledata.haircolor) ? newmodel.haircolor : null;
               var eyecolor = (newmodel.eyecolor == p.profiledata.eyecolor) ? newmodel.eyecolor : null;
               //TO DO test if anything changed
               var hotfeatures = newmodel.hotfeaturelist;
               //TO DO test if anything changed
               var ethicities = newmodel.ethnicitylist;



               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

               if (height.HasValue == true) p.profiledata.height = height;
               if (bodytype != null) p.profiledata.bodytype = bodytype;
               if (haircolor != null) p.profiledata.haircolor = haircolor;
               if (eyecolor != null) p.profiledata.eyecolor = eyecolor;
               if (hotfeatures.Count > 0) p.profiledata.height = height;
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

       public AnewluvMessages editprofilelifestylesettings(LifeStyleSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();


           //get the profile details :
           profile p = db.profiles.Where(z => z.id == profileid).First(); 

           messages = (updateprofilelifestylesettings(newmodel, p, messages));
           // messages = (EditProfilelifestyleSettingsPage2Update(newmodel, profileid, messages));
           // messages = (EditProfilelifestyleSettingsPage3Update(newmodel, profileid, messages));
           //  messages = (EditProfilelifestyleSettingsPage4Update(newmodel, profileid, messages));

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You lifestyle Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit lifestyle Settings Successful";
           return messages;
       }

       //TO DO send back the messages on errors and when nothing is changed
       private AnewluvMessages updateprofilelifestylesettings(LifeStyleSettingsModel newmodel,profile p, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
              // profile p = db.profiles.Where(z => z.id == profileid).First();
               //sample code for determining weather to edit an item or not or determin if a value changed'
               //nothingupdated = (newmodel.educationlevel  == p.profiledata.educationlevel) ? false : true;

               //only update items that are not null
               var educationlevel = (newmodel.educationlevel == p.profiledata.educationlevel) ? newmodel.educationlevel : null;
               var employmentstatus = (newmodel.employmentstatus == p.profiledata.employmentstatus) ? newmodel.employmentstatus : null;
               var incomelevel = (newmodel.incomelevel == p.profiledata.incomelevel) ? newmodel.incomelevel : null;
               var wantskids = (newmodel.wantskids == p.profiledata.wantsKidstatus) ? newmodel.wantskids : null;
               var profession = (newmodel.profession == p.profiledata.profession) ? newmodel.profession : null;
               var maritalstatus = (newmodel.maritalstatus == p.profiledata.maritalstatus) ? newmodel.maritalstatus : null;
               var livingsituation = (newmodel.livingsituation == p.profiledata.livingsituation) ? newmodel.livingsituation : null;
               var havekids = (newmodel.havekids == p.profiledata.kidstatus) ? newmodel.havekids : null;
               //TO DO test if anything changed
               var lookingfors = newmodel.lookingforlist;




               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

               if (educationlevel != null) p.profiledata.educationlevel = educationlevel;
               if (employmentstatus != null) p.profiledata.employmentstatus = employmentstatus;
               if (incomelevel != null) p.profiledata.incomelevel = incomelevel;
               if (wantskids != null) p.profiledata.wantsKidstatus = wantskids;
               if (profession != null) p.profiledata.profession = profession;
               if (maritalstatus != null) p.profiledata.maritalstatus = maritalstatus;
               if (livingsituation != null) p.profiledata.livingsituation = livingsituation;
               if (havekids != null) p.profiledata.kidstatus = havekids;

               if (lookingfors.Count > 0)
                   updateprofilemetatdatalookingfor(lookingfors, p.profilemetadata);



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


       #region "Edit profile Character Settings Updates here"


       public AnewluvMessages editprofilecharactersettings(CharacterSettingsModel newmodel, int profileid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();


           //get the profile details :
           profile p = db.profiles.Where(z => z.id == profileid).First(); 

           messages = (updateprofilecharactersettings(newmodel, p, messages));
           // messages = (EditProfilecharacterSettingsPage2Update(newmodel, profileid, messages));
           // messages = (EditProfilecharacterSettingsPage3Update(newmodel, profileid, messages));
           //  messages = (EditProfilecharacterSettingsPage4Update(newmodel, profileid, messages));

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You character Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit character Settings Successful";
           return messages;
       }

       //TO DO send back the messages on errors and when nothing is changed
       private AnewluvMessages updateprofilecharactersettings(CharacterSettingsModel newmodel, profile p, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
              // profile p = db.profiles.Where(z => z.id == profileid).First();
               //sample code for determining weather to edit an item or not or determin if a value changed'
               //nothingupdated = (newmodel.diet  == p.profiledata.diet) ? false : true;

               //only update items that are not null
               var diet = (newmodel.diet == p.profiledata.diet) ? newmodel.diet : null;
               var humor = (newmodel.humor == p.profiledata.humor) ? newmodel.humor : null;
               var drinking = (newmodel.drinking == p.profiledata.drinking) ? newmodel.drinking : null;
               var excercise = (newmodel.excercise == p.profiledata.exercise ) ? newmodel.excercise : null;
               var smoking = (newmodel.smoking == p.profiledata.smoking) ? newmodel.smoking : null;
               var sign = (newmodel.sign == p.profiledata.sign) ? newmodel.sign : null;
               var politicalview = (newmodel.politicalview  == p.profiledata.politicalview) ? newmodel.politicalview : null;
               var religion = (newmodel.religion  == p.profiledata.religion) ? newmodel.religion  : null;
               var religiousattendance = (newmodel.religiousattendance == p.profiledata.religiousattendance) ? newmodel.religiousattendance : null;
               //TO DO test if anything changed
               var hobylist  = newmodel.hobbylist;




               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

               if (diet != null) p.profiledata.diet = diet;
               if (humor != null) p.profiledata.humor = humor;
               if (drinking != null) p.profiledata.drinking = drinking;
               if (excercise != null) p.profiledata.exercise  = excercise;
               if (smoking != null) p.profiledata.smoking = smoking;
               if (sign != null) p.profiledata.sign = sign;
               if (politicalview != null) p.profiledata.politicalview  = politicalview ;
               if (religion != null) p.profiledata.religion = religion;
               if (religiousattendance != null) p.profiledata.religiousattendance = religiousattendance;
               if (hobylist.Count > 0)
                   updateprofilemetatdatahobby(hobylist, p.profilemetadata);



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
