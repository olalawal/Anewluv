using System;
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
using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using Anewluv.Services.Contracts;


namespace Anewluv.Services.Edit
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberEditService : IMemberEditService  
    {


        IUnitOfWork _unitOfWork;
       // private Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberEditService(IUnitOfWork unitOfWork)
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

    


        #region "profile visisiblity settings update here"
                    
        public bool updateprofilevisibilitysettings(visiblitysetting model)
        {
            if (model.id != null)
            {

                //Impement on member service ?
                // datingservice.updatememberVisiblitySetting(model);


                return true;
            }
            return false;
        }
        #endregion


        #region "Methods to GET current edit profile settings for a user"

        // constructor
        public BasicSettingsModel getbasicsettingsmodel(EditProfileModel editprofilemodel)
        {

              _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemodel.profileid).FirstOrDefault();
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

                     model.birthdate = p.profiledata.birthdate; //== null ? null :  p.profiledata.lu_birthdate;
                 //  model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                 model.gender = p.profiledata.lu_gender == null ? null : p.profiledata.lu_gender;
                 model.countryid = p.profiledata.countryid == null ? null : p.profiledata.countryid;
                 model.city = p.profiledata.city == null ? null : p.profiledata.city;
                 model.postalcode = p.profiledata.postalcode == null ? null : p.profiledata.postalcode;
                 model.aboutme = p.profiledata.aboutme == null ? null : p.profiledata.aboutme;
                 model.phonenumber = p.profiledata.phone == null ? null : p.profiledata.phone;




                 return model;

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
        public AppearanceSettingsModel getappearancesettingsmodel(EditProfileModel editprofilemodel)
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemodel.profileid).FirstOrDefault();

                    AppearanceSettingsModel model = new AppearanceSettingsModel();

                    model.height = p.profiledata.height == null ? null : p.profiledata.height;
                    model.bodytype = p.profiledata.lu_bodytype == null ? null : p.profiledata.lu_bodytype;
                    model.haircolor = p.profiledata.lu_haircolor == null ? null : p.profiledata.lu_haircolor;
                    model.eyecolor = p.profiledata.lu_eyecolor == null ? null : p.profiledata.lu_eyecolor;


                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    // var allhotfeature = db.lu_hotfeature;
                    //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                    foreach (var item in model.hotfeaturelist)
                    {
                        model.hotfeaturelist.Add(item);
                    }

                    foreach (var item in model.ethnicitylist)
                    {
                        model.ethnicitylist.Add(item);
                    }

                    return model;

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

        //populate the enities
        public LifeStyleSettingsModel getlifestylesettingsmodel(EditProfileModel editprofilemodel)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemodel.profileid).FirstOrDefault();                   
                    LifeStyleSettingsModel model = new LifeStyleSettingsModel();
                    model.educationlevel = p.profiledata.lu_educationlevel;
                    model.employmentstatus = p.profiledata.lu_employmentstatus;
                    model.incomelevel = p.profiledata.lu_incomelevel;

                    foreach (var item in model.lookingforlist)
                    {
                        model.lookingforlist.Add(item);
                    }


                    model.wantskids = p.profiledata.lu_wantskids;
                    model.profession = p.profiledata.lu_profession;
                    model.maritalstatus = p.profiledata.lu_maritalstatus;
                    model.livingsituation = p.profiledata.lu_livingsituation;
                    model.havekids = p.profiledata.lu_havekids;

                    return model;

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
        public CharacterSettingsModel getcharactersettingsmodel(EditProfileModel editprofilemodel)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemodel.profileid).FirstOrDefault();

                 //   profile p = db.profiles.Where(z => z.id == editprofilemodel.intprofileid).FirstOrDefault();
                    CharacterSettingsModel model = new CharacterSettingsModel();

                    model.diet = p.profiledata.lu_diet;
                    model.humor = p.profiledata.lu_humor;

                    //populiate the hobby list, remeber this comes from the metadata link so you have to drill down
                    //var allhobbies = db.lu_hobby;
                    foreach (var item in model.hobbylist)
                    {
                        model.hobbylist.Add(item);
                    }

                    model.drinking = p.profiledata.lu_drinks;
                    model.excercise = p.profiledata.lu_exercise;
                    model.smoking = p.profiledata.lu_smokes;
                    model.sign = p.profiledata.lu_sign;
                    model.politicalview = p.profiledata.lu_politicalview;
                    model.religiousattendance = p.profiledata.lu_religiousattendance;

                    return model;
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

        #region "Edit profile Basic Settings Updates here

        public AnewluvMessages membereditbasicsettings(EditProfileModel editprofilemodel)
        {

          //  _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemodel.profileid).FirstOrDefault();

                        //create a new messages object
                        AnewluvMessages messages = new AnewluvMessages();


                        messages = (updatememberbasicsettings(editprofilemodel.basicsettings, p, messages, db));
                        //  messages=(membereditBasicSettingsPage2Update(newmodel,profileid ,messages));


                        if (messages.errormessages.Count > 0)
                        {
                            messages.message = "There was a problem Editing You Basic Settings, Please try again later";
                            return messages;
                        }
                        messages.message = "Edit Basic Settings Successful";
                        return messages;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
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
        //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
        private AnewluvMessages updatememberbasicsettings(BasicSettingsModel newmodel, profile p, AnewluvMessages messages,IUnitOfWork db)
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

                db.Update(p);
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

        #endregion


        //#region "other editpages to implement"
        #region "Edit profile Appeareance Settings Updates here"

        public AnewluvMessages membereditappearancesettings(EditProfileModel editprofilemode)
        {

            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        //create a new messages object
                        AnewluvMessages messages = new AnewluvMessages();


                        //get the profile details :
                        profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemode.profileid).First();

                        messages = (updatememberappearancesettings(editprofilemode.appearancesettings, p, messages,db));
                        // messages = (membereditAppearanceSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditAppearanceSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditAppearanceSettingsPage4Update(newmodel, profileid, messages));

                        if (messages.errormessages.Count > 0)
                        {
                            messages.message = "There was a problem Editing You Appearance Settings, Please try again later";
                            return messages;
                        }
                        messages.message = "Edit Appearance Settings Successful";
                        return messages;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemode.profileid));
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

        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatememberappearancesettings(AppearanceSettingsModel newmodel, profile p, AnewluvMessages messages,IUnitOfWork db)
        {
            bool nothingupdated = true;

            try
            {
                //profile p = db.profiles.Where(z => z.id == profileid).First();
                //sample code for determining weather to edit an item or not or determin if a value changed'
                //nothingupdated = (newmodel.height  == p.profiledata.lu_height) ? false : true;

                //only update items that are not null
                var height = (newmodel.height == p.profiledata.height) ? newmodel.height : null;
                var bodytype = (newmodel.bodytype == p.profiledata.lu_bodytype) ? newmodel.bodytype : null;
                var haircolor = (newmodel.haircolor == p.profiledata.lu_haircolor) ? newmodel.haircolor : null;
                var eyecolor = (newmodel.eyecolor == p.profiledata.lu_eyecolor) ? newmodel.eyecolor : null;
                //TO DO test if anything changed
                var hotfeatures = newmodel.hotfeaturelist;
                //TO DO test if anything changed
                var ethicities = newmodel.ethnicitylist;



                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

                if (height.HasValue == true) p.profiledata.height = height;
                if (bodytype != null) p.profiledata.lu_bodytype = bodytype;
                if (haircolor != null) p.profiledata.lu_haircolor = haircolor;
                if (eyecolor != null) p.profiledata.lu_eyecolor = eyecolor;
                if (hotfeatures.Count > 0) p.profiledata.height = height;
                if (height.HasValue == true) p.profiledata.height = height;
                //if (height.HasValue == true) p.profiledata.lu_height = height;

                if (hotfeatures.Count > 0)
                    updatemembermetatdatahotfeature(hotfeatures, p.profilemetadata,db);
                if (ethicities.Count > 0)
                    updatemembermetatdataethnicity(ethicities, p.profilemetadata,db);


                //db.Entry(profiledata).State = EntityState.Modified;
                 db.Update(p);
                 db.Commit();

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

        #endregion


        #region "Edit profile LifeStyle Settings Updates here"

        public AnewluvMessages membereditlifestylesettings(EditProfileModel editprofilemode)
        {

            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        //create a new messages object
                        AnewluvMessages messages = new AnewluvMessages();
                        //get the profile details :
                        profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemode.profileid).First();


                        messages = (updatememberlifestylesettings(editprofilemode.lifestylesettings, p, messages,db));
                        // messages = (membereditlifestyleSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditlifestyleSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditlifestyleSettingsPage4Update(newmodel, profileid, messages));

                        if (messages.errormessages.Count > 0)
                        {
                            messages.message = "There was a problem Editing You lifestyle Settings, Please try again later";
                            return messages;
                        }
                        messages.message = "Edit lifestyle Settings Successful";
                        return messages;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemode.profileid));
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

        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatememberlifestylesettings(LifeStyleSettingsModel newmodel, profile p, AnewluvMessages messages,IUnitOfWork db)
        {
            bool nothingupdated = true;

            try
            {
                // profile p = db.profiles.Where(z => z.id == profileid).First();
                //sample code for determining weather to edit an item or not or determin if a value changed'
                //nothingupdated = (newmodel.educationlevel  == p.profiledata.lu_educationlevel) ? false : true;

                //only update items that are not null
                var educationlevel = (newmodel.educationlevel == p.profiledata.lu_educationlevel) ? newmodel.educationlevel : null;
                var employmentstatus = (newmodel.employmentstatus == p.profiledata.lu_employmentstatus) ? newmodel.employmentstatus : null;
                var incomelevel = (newmodel.incomelevel == p.profiledata.lu_incomelevel) ? newmodel.incomelevel : null;
                var wantskids = (newmodel.wantskids == p.profiledata.lu_wantskids) ? newmodel.wantskids : null;
                var profession = (newmodel.profession == p.profiledata.lu_profession) ? newmodel.profession : null;
                var maritalstatus = (newmodel.maritalstatus == p.profiledata.lu_maritalstatus) ? newmodel.maritalstatus : null;
                var livingsituation = (newmodel.livingsituation == p.profiledata.lu_livingsituation) ? newmodel.livingsituation : null;
                var havekids = (newmodel.havekids == p.profiledata.lu_havekids) ? newmodel.havekids : null;
                //TO DO test if anything changed
                var lookingfors = newmodel.lookingforlist;




                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

                if (educationlevel != null) p.profiledata.lu_educationlevel = educationlevel;
                if (employmentstatus != null) p.profiledata.lu_employmentstatus = employmentstatus;
                if (incomelevel != null) p.profiledata.lu_incomelevel = incomelevel;
                if (wantskids != null) p.profiledata.lu_wantskids = wantskids;
                if (profession != null) p.profiledata.lu_profession = profession;
                if (maritalstatus != null) p.profiledata.lu_maritalstatus = maritalstatus;
                if (livingsituation != null) p.profiledata.lu_livingsituation = livingsituation;
                if (havekids != null) p.profiledata.lu_havekids = havekids;

                if (lookingfors.Count > 0)
                    updatemembermetatdatalookingfor(lookingfors, p.profilemetadata,db);



                //db.Entry(profiledata).State = EntityState.Modified;
               // int changes = db.SaveChanges();
                db.Update(p);
                db.Commit();
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

        #endregion


        #region "Edit profile Character Settings Updates here"


        public AnewluvMessages membereditcharactersettings(EditProfileModel editprofilemode )
        {
           
              using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                       
                          //create a new messages object
                        AnewluvMessages messages = new AnewluvMessages();


                        //get the profile details :
                        profile p = db.GetRepository<profile>().Find().Where(z => z.id == editprofilemode.profileid).First();

                        messages = (updatemembercharactersettings(editprofilemode.charactersettings , p, messages,db));
                        // messages = (membereditcharacterSettingsPage2Update(newmodel, profileid, messages));
                        // messages = (membereditcharacterSettingsPage3Update(newmodel, profileid, messages));
                        //  messages = (membereditcharacterSettingsPage4Update(newmodel, profileid, messages));

                        if (messages.errormessages.Count > 0)
                        {
                            messages.message = "There was a problem Editing You character Settings, Please try again later";
                            return messages;
                        }
                        messages.message = "Edit character Settings Successful";
                        return messages;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        using (var logger = new  Logging(applicationEnum.EditMemberService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(editprofilemode.profileid));
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
        //TO DO send back the messages on errors and when nothing is changed
        private AnewluvMessages updatemembercharactersettings(CharacterSettingsModel newmodel, profile p, AnewluvMessages messages,IUnitOfWork db)
        {
            bool nothingupdated = true;

            try
            {
                // profile p = db.profiles.Where(z => z.id == profileid).First();
                //sample code for determining weather to edit an item or not or determin if a value changed'
                //nothingupdated = (newmodel.diet  == p.profiledata.lu_diet) ? false : true;

                //only update items that are not null
                var diet = (newmodel.diet == p.profiledata.lu_diet) ? newmodel.diet : null;
                var humor = (newmodel.humor == p.profiledata.lu_humor) ? newmodel.humor : null;
                var drinking = (newmodel.drinking == p.profiledata.lu_drinks) ? newmodel.drinking : null;
                var excercise = (newmodel.excercise == p.profiledata.lu_exercise) ? newmodel.excercise : null;
                var smoking = (newmodel.smoking == p.profiledata.lu_smokes) ? newmodel.smoking : null;
                var sign = (newmodel.sign == p.profiledata.lu_sign) ? newmodel.sign : null;
                var politicalview = (newmodel.politicalview == p.profiledata.lu_politicalview) ? newmodel.politicalview : null;
                var religion = (newmodel.religion == p.profiledata.lu_religion) ? newmodel.religion : null;
                var religiousattendance = (newmodel.religiousattendance == p.profiledata.lu_religiousattendance) ? newmodel.religiousattendance : null;
                //TO DO test if anything changed
                var hobylist = newmodel.hobbylist;




                //update my settings 
                //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

                if (diet != null) p.profiledata.lu_diet = diet;
                if (humor != null) p.profiledata.lu_humor = humor;
                if (drinking != null) p.profiledata.lu_drinks = drinking;
                if (excercise != null) p.profiledata.lu_exercise = excercise;
                if (smoking != null) p.profiledata.lu_smokes = smoking;
                if (sign != null) p.profiledata.lu_sign = sign;
                if (politicalview != null) p.profiledata.lu_politicalview = politicalview;
                if (religion != null) p.profiledata.lu_religion = religion;
                if (religiousattendance != null) p.profiledata.lu_religiousattendance = religiousattendance;
                if (hobylist.Count > 0)
                    updatemembermetatdatahobby(hobylist, p.profilemetadata,db);



                //db.Entry(profiledata).State = EntityState.Modified;
               // int changes = db.SaveChanges();
                db.Update(p);
                db.Commit();
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



        #endregion

        #endregion





        #region "PRIVATE Checkbox Update Functions for profiledata many to many"


        //profiledata ethnicity
        private void updatemembermetatdataethnicity(List<lu_ethnicity> slectedethnicities, profilemetadata currentprofilemetadata,IUnitOfWork db)
        {
            if (slectedethnicities == null)
            {
                // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
                return;
            }
            //build the search settings gender object
            // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
            //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


            foreach (var ethnicity in db.GetRepository<lu_ethnicity>().Find().ToList())
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if ((!currentprofilemetadata.profiledata_ethnicity.Where(z => z.ethnicty_id == ethnicity.id).Any()))
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new profiledata_ethnicity();
                    temp.id = ethnicity.id;
                    temp.profile_id = currentprofilemetadata.profile_id;
                    db.Add(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = db.GetRepository<profiledata_ethnicity>().Find().Where(p => p.profile_id == currentprofilemetadata.profile_id && p.ethnicty_id == ethnicity.id).First();
                    if (temp != null)
                        db.Remove(temp);                  
                }


            }


          
        }
        //profiledata hotfeature
        private void updatemembermetatdatahotfeature(List<lu_hotfeature> selectedhotfeature, profilemetadata currentprofilemetadata,IUnitOfWork db)
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
            foreach (var hotfeature in db.GetRepository<lu_hotfeature>().Find().ToList())
            {
                if (selectedhotfeature.Contains(hotfeature))
                {
                    //does not exist so we will add it
                    if (!currentprofilemetadata.profiledata_hotfeature.Any(p => p.id == hotfeature.id))
                    {

                        //SearchSettings_showme.showmeID = showme.showmeID;
                        var temp = new profiledata_hotfeature();
                        temp.id = hotfeature.id;
                        temp.profile_id = currentprofilemetadata.profile_id;
                        db.Add(temp);

                    }
                }
                else
                { //exists means we want to remove it
                    if (currentprofilemetadata.profiledata_hotfeature.Any(p => p.id == hotfeature.id))
                    {
                        var temp = db.GetRepository<profiledata_hotfeature>().Find().Where(p => p.profile_id == currentprofilemetadata.profile_id && p.hotfeature_id == hotfeature.id).First();
                        db.Remove(temp);

                    }
                }
            }
        }
        //profiledata hobby
        private void updatemembermetatdatahobby(List<lu_hobby> selectedhobby, profilemetadata currentprofilemetadata,IUnitOfWork db)
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
            foreach (var hobby in db.GetRepository<lu_hobby>().Find().ToList())
            {
                if (selectedhobby.Contains(hobby))
                {
                    //does not exist so we will add it
                    if (!currentprofilemetadata.profiledata_hobby.Any(p => p.id == hobby.id))
                    {

                        //SearchSettings_showme.showmeID = showme.showmeID;
                        var temp = new profiledata_hobby();
                        temp.id = hobby.id;
                        temp.profile_id = currentprofilemetadata.profile_id;
                        db.Add(temp);

                    }
                }
                else
                { //exists means we want to remove it
                    if (currentprofilemetadata.profiledata_hobby.Any(p => p.id == hobby.id))
                    {
                        var temp =  db.GetRepository<profiledata_hobby>().Find().ToList().Where(p => p.profile_id == currentprofilemetadata.profile_id && p.hobby_id == hobby.id).First();
                        db.Remove(temp);

                    }
                }
            }
        }
        //profiledata lookingfor
        private void updatemembermetatdatalookingfor(List<lu_lookingfor> selectedlookingfor, profilemetadata currentprofilemetadata, IUnitOfWork db)
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
            foreach (var lookingfor in db.GetRepository<lu_lookingfor>().Find().ToList())
            {
                if (selectedlookingfor.Contains(lookingfor))
                {
                    //does not exist so we will add it
                    if (!currentprofilemetadata.profiledata_lookingfor.Any(p => p.id == lookingfor.id))
                    {

                        //SearchSettings_showme.showmeID = showme.showmeID;
                        var temp = new profiledata_lookingfor();
                        temp.id = lookingfor.id;
                        temp.profile_id = currentprofilemetadata.profile_id;
                        db.Add(temp);

                    }
                }
                else
                { //exists means we want to remove it
                    if (currentprofilemetadata.profiledata_lookingfor.Any(p => p.id == lookingfor.id))
                    {
                        var temp = db.GetRepository<profiledata_lookingfor>().Find().ToList().Where(p => p.profile_id == currentprofilemetadata.profile_id && p.lookingfor_id == lookingfor.id).First();
                        db.Remove(temp);

                    }
                }
            }
        }

        #endregion


    }
}
