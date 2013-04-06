



    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Server;

    using Dating.Server.Data.Models;
    using Dating.Server.Data.Services;

    using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Security.Principal;

    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
   // using Common;
using Shell.MVC2.Infrastructure;


    using System.Globalization;
using Shell.MVC2.Interfaces;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;


namespace Shell.MVC2.Data.AuthenticationAndMembership
{

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================


    // this membership serverice is for MVC
    public class AnewLuvMembershipProvider :  MembershipProvider, IAnewLuvMembershipProvider
    {

        //constant strings for reseting passwords
        const String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; const String LOWER = "abcdefghijklmnopqrstuvwxyz"; const String NUMBERS = "1234567890"; const String SPECIAL = "*$-+?&=!%/";
        //AnewluvContext context = new AnewluvContext();
       // DatingService datingService = new DatingService();
      
         private IGeoRepository _georepository;
         private IMemberRepository _memberepository;
         private AnewluvContext _datingcontext;
        private IPhotoRepository _photorepository;
        private ErroLogging logger;


         public AnewLuvMembershipProvider(AnewluvContext datingcontext, IGeoRepository georepository,
             IMemberRepository memberepository,IPhotoRepository photorepository)            
        {
            _georepository = georepository;
            _memberepository = memberepository;
             _datingcontext = datingcontext ;
             _photorepository = photorepository ;
        }



        public override bool ValidateUser(string username, string password)
        {

            IQueryable<Shell.MVC2.Domain.Entities.Anewluv.profile> myQuery = default(IQueryable<Shell.MVC2.Domain.Entities.Anewluv.profile>);

            try
            {
              
                //first you have to get the encrypted sctring by email address and username 
                string encryptedPassword = "";
                //get profile created date
                DateTime creationdate;
                DateTime? passwordchangedate;
                DateTime EncrpytionChangeDate = new DateTime(2011, 8, 3, 4, 5, 0);
                string decryptedPassword;
                string actualpasswordstring;

                //Dim ctx As New Entities()
                //added profile status ID validation as well i.e 2 for activated and is not banned 
                myQuery = _datingcontext.profiles.Where(p => p.username == username && p.status.id == 2);



                if (myQuery.Count() > 0)
                {
                    //retirve encypted password
                    encryptedPassword = myQuery.FirstOrDefault().password;
                    creationdate = myQuery.FirstOrDefault().creationdate.GetValueOrDefault();
                    passwordchangedate = myQuery.FirstOrDefault().passwordChangeddate;
                }
                else
                {
                    return false;
                }

                //case for if the user account was created before encrpyption algorithm was changed
                //compare the dates
                int resultcreateddate = DateTime.Compare(creationdate, EncrpytionChangeDate);
                int resultpasswordchangedate = DateTime.Compare(passwordchangedate.GetValueOrDefault(), EncrpytionChangeDate);
                if (resultpasswordchangedate > 0 | resultcreateddate > 0)
                //use new decryption method
                {

                    decryptedPassword = Encryption.decryptString(encryptedPassword);
                    actualpasswordstring = password;
                }
                else
                {
                    decryptedPassword = Encryption.Decrypt(encryptedPassword, password);
                    actualpasswordstring = username.ToUpper() + Encryption.EncryptionKey;
                }






                //check if decrypted string macthed username to upper  + secret
                if (actualpasswordstring == decryptedPassword)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    _memberepository.updateuserlogintime(username, HttpContext.Current.Session.SessionID);
                    //also update the profiledata for the last login date
                    return true;
                }
                else
                {
                    return false;
                }
            }
              catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, myQuery !=null? myQuery.FirstOrDefault().id:0, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }




        }
    

        //5-82012 updated to only valudate username
        //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
        public  bool ValidateUser(string username)
        {
            //string encryptedPassword = Common.Encryption.EncodePasswordWithSalt(password, username);
            IQueryable<Shell.MVC2.Domain.Entities.Anewluv.profile> myQuery = default(IQueryable<Shell.MVC2.Domain.Entities.Anewluv.profile>);
            //Dim ctx As New Entities()

            try
            {
                //test code here 
                // dynamic user = from p in datingcontext.profiles
                //               where p.username == username
                //              select new { p.ScreenName };
                //end test code


                //use the encyrption service in common
                //dynamic user = datingcontext.profiles.Where(u => u.username == username && u.Password == Common.Encryption.EncodePasswordWithSalt(password, username).FirstOrDefault());
                // Return user IsNot Nothing
              
                myQuery = _datingcontext.profiles.Where(p => p.username == username);//&& p.ProfileStatusID == 2);


                if (myQuery.Count() > 0)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    _memberepository.updateuserlogintime(username, HttpContext.Current.Session.SessionID);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, myQuery != null ? myQuery.FirstOrDefault().id : 0, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
            
        }
                        
        public  bool ValidateUser(string VerifedEmail, string openidIdentifer,string openidProvidername)
        {

            Shell.MVC2.Domain.Entities.Anewluv.profile myprofile = new Domain.Entities.Anewluv.profile();
          
            try
            {
                //open ID members are already verifed but it is posublethat a member who is not activated tries to use open ID
                //so they could be in order status 1
                 myprofile = _datingcontext.profiles.Where(p => p.emailaddress == VerifedEmail && p.status.id <= 2).FirstOrDefault();

                //get the openid providoer
                lu_openidprovider provider = _datingcontext.lu_openidprovider.Where(p => (p.description).ToUpper() == openidProvidername.ToUpper()).FirstOrDefault();
                if (provider == null) return false;


                //check for the openIDidenfier , to see if it was used before , if it was do nothing but normal updates for user
                var myopenIDstore = myprofile.openids.Where(p => p.openididentifier == openidIdentifer && provider.description.ToUpper()   == openidProvidername.ToUpper() && p.active == true).FirstOrDefault();

                //if we found an openID store for this type
                if (myopenIDstore == null && myprofile != null)
                //add the openID provider if its a new one
                {
                    _memberepository.addnewopenidforprofile(myprofile.id, openidIdentifer, openidProvidername);
                }

                //first you have to get the encrypted sctring by email address and username 
                // string encryptedopenidIdentifer = "";
                //get profile created date
                //DateTime creationdate;
                // DateTime? passwordchangedate;
                //DateTime EncrpytionChangeDate = new DateTime(2011, 8, 3, 4, 5, 0);        

                //Dim ctx As New Entities()
                //added profile status ID validation as well i.e 2 for activated and is not banned 
                // myQuery = datingcontext.profiles.Where(p => p.ProfileID == VerifedEmail && p.ProfileStatusID == 2);
                if (myprofile != null)
                {
                    _memberepository.updateuserlogintimebyprofileid(myprofile.id, HttpContext.Current.Session.SessionID);
                    return true;
                }
                else
                {
                    return false;
                }



                // datingService.UpdateUserLoginTimeByProfileID(VerifedEmail, HttpContext.Current.Session.SessionID);


                //    return false;
                // }

            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, myprofile != null ? myprofile.id : 0, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }



        }


        public override string ApplicationName
        {

           
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

     

        public override MembershipUser CreateUser(string username,
           string password,
           string email,
           string securityQuestion,
                  string securityAnswer,
           bool isApproved,
           object providerUserKey,
           out MembershipCreateStatus status)
        {
            //set default values for a basic memeber created with just password,
            //sec question and aswers
            return this.CreateUserCustom(username, password,"","",
                  email,
                 //  securityQuestion,
                 //  securityAnswer
                 DateTime.Now,
                  "","", "", "", null,null,"","","",
                  false ,
                  null,
                  out status);
        }

        public  AnewLuvMembershipUser  CreateUserCustom(string username,
                   string password,string openidIdentifer,string openidProvidername,
                  string email,
                //  string securityQuestion,
                 // string securityAnswer,
                  DateTime birthdate, string gender, string country, string city,string stateprovince,double? longitude,double? latitude, string screenname, string zippostalcode,string activationcode,
                  bool isApproved,
                  object providerUserKey,
                 out MembershipCreateStatus status)

        {

           

           AnewLuvMembershipUser  newUser = null;
            
            try
            {
               // AnewluvContext  mydb = new  AnewluvContext();


                using (AnewluvContext  dbContext = new  AnewluvContext())
                {


                    Shell.MVC2.Domain.Entities.Anewluv.profile ObjProfileEntity = new Shell.MVC2.Domain.Entities.Anewluv.profile();
               profiledata objprofileDataEntity = new profiledata();
               //TO DO new entity for OPEN ID data
               int countryID = 0;
               Random objRandom = new Random();  
               int intStart = objRandom.Next(1, 9);
               int intLastTwo = objRandom.Next(10, 99);
               //convert the string values to byte as needed
               //NumberFormatInfo provider = new NumberFormatInfo();
               // These properties affect the conversion.
               //provider.PositiveSign = "pos";


               //conver the unquiqe coountry Name to an ID
                using (PostalDataService postaldbContext = new PostalDataService())
                {  //store country ID for use later
                    countryID =  _georepository.getcountryidbycountryname(country);               
                }

                //split up the city from state province
                var tempCityAndStateProvince = city.Split(',');
             
                //Build the profile data table
               // objprofileDataEntity.id  = ;
                objprofileDataEntity.profile.emailaddress = email;
                objprofileDataEntity.latitude  = latitude ;
                objprofileDataEntity.longitude  = longitude; //_GpsData.longitude;
                objprofileDataEntity.city  = tempCityAndStateProvince[0];
                objprofileDataEntity.countryregion = "NA";

                objprofileDataEntity.stateprovince  = ((tempCityAndStateProvince.Count() > 1)) ?  tempCityAndStateProvince[1] : "NA" ;

                objprofileDataEntity.countryid  = countryID;
                objprofileDataEntity.postalcode   = zippostalcode;
                 
                 objprofileDataEntity.gender.id  = Int32.Parse(gender);
                 
                
             //  =  Int32.Parse(gender): objprofileDataEntity.gender.GenderName  = gender;
                objprofileDataEntity.birthdate  = birthdate;
                objprofileDataEntity.phone  = "NA";
                objprofileDataEntity.aboutme  = "Hello";



                //set all the entity values
                ObjProfileEntity.username = username;
                //changed the encryption to something stronger
                //make username upper so that we can get actual mateches withoute user having to type in a case sensitive username
                    ObjProfileEntity.password  = (openidIdentifer !="") ? Encryption.encryptString(password) : openidIdentifer;
               // ObjProfileEntity.id   = email;
                ObjProfileEntity.screenname  = screenname;
                //need to add a new feild
                ObjProfileEntity.activationcode   = activationcode;
                //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                ObjProfileEntity.creationdate  = System.DateTime.Now;
                ObjProfileEntity.modificationdate  = System.DateTime.Now;
                ObjProfileEntity.logindate = System.DateTime.Now;
                // fix this to null
                ObjProfileEntity.forwardmessages  = 1;
              //  ObjProfileEntity.SecurityQuestionID = 1;                
               // ObjProfileEntity.SecurityAnswer =  securityAnswer;
              ObjProfileEntity.status.id  = (openidIdentifer ==  "" ||  openidIdentifer == null)?   1 : 2;  //auto activate profiles fi we have an openID user since we have verifed thier info

                //TOD DO add open ID identifier as well Profider type to ssoProvider table 

                   dbContext.profiles.Add(ObjProfileEntity);
                  dbContext.profiledata.Add (objprofileDataEntity);
                  

                  dbContext.SaveChanges();
                  
                }




                status = MembershipCreateStatus.Success;
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService );
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;               
                status = MembershipCreateStatus.ProviderError;
                newUser = null;
              throw;
            }
            
            return newUser;
        }


        public override string ResetPassword(string profileID, string answer)
        {

            try
            {
                return this.ResetPasswordCustom(Convert.ToInt16(profileID));
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
               // status = MembershipCreateStatus.ProviderError;
               // newUser = null;
                throw;
            }
             
        }

        //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
        // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
        //finally returns the new password to the calling functon or an empty string if failure.
        public string ResetPasswordCustom(int profileid)
        {

            try
            {
                //  if (securityquestionID == null) return "";

                // var username = datingService.ValidateSecurityAnswerIsCorrect(profileid, securityquestionID.GetValueOrDefault(), answer);
                var username = _memberepository.getusernamebyprofileid(Convert.ToInt16(profileid));
                var generatedpassword = "";
                if (username != "")
                {
                    //we have the generated password now update the user's account with new password

                    generatedpassword = GeneratePassword();
                    _memberepository.updatepassword(Convert.ToInt16(profileid), Encryption.encryptString(generatedpassword));


                    //'reset the password 
                    return generatedpassword;
                }
                // throw new NotImplementedException();
                return "";
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
             
        }

        public override void UpdateUser(MembershipUser user)
        {



            AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
            try
            {
                
                // AnewluvContext  mydb = new  AnewluvContext();
                using (AnewluvContext dbContext = new AnewluvContext())
                {
                   // dbContext.Dispose();

                    //get profile and profile datas
                    Shell.MVC2.Domain.Entities.Anewluv.profile ObjProfileEntity = dbContext.profiles.Where(p => p.id == Convert.ToInt16(u.profileid)).FirstOrDefault();
                    profiledata objprofileDateEntity = dbContext.profiledata.Where(p => p.profile_id  == Convert.ToInt16( u.profileid)).FirstOrDefault();
                    
                     new GpsData();

                    string[] tempCityAndStateProvince = u.city.Split(',');                   
                    Random objRandom = new Random();
                    int intStart = objRandom.Next(1, 9);
                    int intLastTwo = objRandom.Next(10, 99);
                    //convert the string values to byte as needed
                    //NumberFormatInfo provider = new NumberFormatInfo();
                    // These properties affect the conversion.
                    //provider.PositiveSign = "pos";


                    //conver the unquiqe coountry Name to an ID
                     //store country ID for use later 
                     int  countryID = _georepository.getcountryidbycountryname(u.country);

                        //get the longidtue and latttude 
                     GpsData _GpsData =  _georepository.getgpsdatasinglebycitycountryandpostalcode(u.country, tempCityAndStateProvince[0], u.ziporpostalcode);



                    //split up the city from state province
                    //Build the profile data table                   
                    objprofileDateEntity.latitude = _GpsData.Latitude ;
                    objprofileDateEntity.longitude = _GpsData.Longitude ;
                    objprofileDateEntity.city = tempCityAndStateProvince[0];
                    objprofileDateEntity.countryregion = "NA";

                    if (tempCityAndStateProvince.Count() == 2)
                    {
                        objprofileDateEntity.stateprovince = (string.IsNullOrEmpty(tempCityAndStateProvince[1])) ? "NA" : tempCityAndStateProvince[1];
                    }
                    else 
                    {
                        objprofileDateEntity.stateprovince = "NA";
                    }

                    objprofileDateEntity.countryid = countryID;
                    objprofileDateEntity.postalcode = u.ziporpostalcode;
                    objprofileDateEntity.gender.id  = Int32.Parse(u.gender);
                    objprofileDateEntity.birthdate = u.birthdate;
                    objprofileDateEntity.phone  = "NA";
                    //objprofileDateEntity.AboutMe = "Hello";



                    //set all the entity values
                    // ObjProfileEntity.username = username;
                    //changed the encryption to something stronger

                    //only update password if it changed
                    if (u.password != null)
                    {
                        ObjProfileEntity.password  = Encryption.encryptString(u.password);
                        ObjProfileEntity.passwordChangeddate  = DateTime.Now;
                        ObjProfileEntity.passwordchangecount = (ObjProfileEntity.passwordchangecount == null) ? 1 : ObjProfileEntity.passwordchangecount + 1;
                    }
                    // ObjProfileEntity.ProfileID = email;
                    // ObjProfileEntity.ScreenName = screenname;
                    //need to add a new feild
                    // ObjProfileEntity.ActivationCode = Common.Encryption.EncodeString(email + screenname);
                    //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                    //ObjProfileEntity.creationdate = System.DateTime.Now;
                    ObjProfileEntity.modificationdate  = System.DateTime.Now;
                    // ObjProfileEntity.LoginDate = System.DateTime.Now;
                    // fix this to null
                    //ObjProfileEntity.ForwardMessages = 1;
                  //  ObjProfileEntity.SecurityQuestionID = System.Convert.ToByte(u.securityquestion );
                   //   ObjProfileEntity.SecurityAnswer = u.securityanswer;
                    //ObjProfileEntity.ProfileStatusID = 1;



                    //dbContext.AddToprofiledatas(objprofileDateEntity);
                   // dbContext.AddToprofiles(ObjProfileEntity);

                    dbContext.SaveChanges();

                }




                // status = MembershipCreateStatus.Success;
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
             






        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return this.GetUserCustom(username, userIsOnline);
        }

        //custom remapped membership get user function
        public AnewLuvMembershipUser GetUserCustom(string username, bool userIsOnline)
        {
            Object providerUserKey = null;  //we dont use this
            Shell.MVC2.Domain.Entities.Anewluv.profile u = new Shell.MVC2.Domain.Entities.Anewluv.profile();

            //using (AnewluvContext datingcontext = new AnewluvContext())
            //{

            
            //    //get roles too maybe

            //    //    strScreenName = tmpScreenName.FirstOrDefault().ToString();

            //}

            u = _memberepository.getprofilebyusername (username);


            if(u == null)
                return null;


            AnewLuvMembershipUser usr = null;

            string providername = this.ApplicationName;
            //usr.= strScreenName;
            // usr.Name = username;
            // usr.IsSubscriber  = username;
         usr = new AnewLuvMembershipUser("AnewluvContext",
                                       username,
                                       providerUserKey,
                                      u.emailaddress  ,
                                      u.securityanswer ,                                    
                                      "",
                                      true,
                                      false,
                                      u.creationdate.GetValueOrDefault(),
                                      u.logindate.GetValueOrDefault()  ,
                                      u.modificationdate.GetValueOrDefault() ,
                                      DateTime.Now,
                                      DateTime.Now,
                                      "");

            //add the profile as a test
         usr.thisprofile = u;
        // datingService.Dispose();
            return usr;
        }

        public string GeneratePassword()
        {

            try
            {
                Random rand = new Random(); String password = "";
                List<String> data = new List<String>();
                for (int i = 0; i < 10; i++)
                {
                    if (i < 3) data.Add(UPPER[rand.Next(UPPER.Length)].ToString());
                    else if (i < 6) data.Add(LOWER[rand.Next(LOWER.Length)].ToString());
                    else if (i < 8) data.Add(NUMBERS[rand.Next(NUMBERS.Length)].ToString());
                    else if (i < 10) data.Add(SPECIAL[rand.Next(SPECIAL.Length)].ToString());
                }
                while (data.Count > 0)
                {
                    int pos = rand.Next(data.Count); password += data[pos];
                    data.RemoveAt(pos);
                }

                return password;
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
             
        }

  
        public void UpdateUserCustom(string username,string ProfileID,
                string password,
                string securityQuestion,
                string securityAnswer,
                DateTime birthdate, string gender, string country, string city, string zippostalcode)
        {
        }

        #region "Extra Methods added to interface to clean up MVC controllers that do member stuff"

       //1-8-2013 olawal addedrobust method for activating profiles
        public AnewluvMessages activateprofile(activateprofilecontainerviewmodel model)
        {
            AnewluvMessages messages = new AnewluvMessages();
            messages.message = "";
            messages.errormessages = null;
            Shell.MVC2.Domain.Entities.Anewluv.profile  profile= new    Shell.MVC2.Domain.Entities.Anewluv.profile();
            
            try
            {
            //Clear any errors kinda redundant tho
           

            //also create a members view model to store pertinent data i.e persist photos profile ID etc
            var membersmodel = new MembersViewModel();

            //get the macthcing member data using the profile ID/email entered
             profile = _memberepository.getprofilebyprofileid  ( model.activateprofilemodel.profileid);
           //  membersmodel =  _m .GetMemberData( model.activateprofilemodel.profileid);

            //verify that user entered correct email before doing anything
            //TO DO add these error messages to resource files
            if (profile == null)
            {
                messages.errormessages.Add("There is no registered account with the email address: " +  model.activateprofilemodel.emailaddress  + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                //hide the photo view in thsi case
                model.activateprofilemodel.photostatus = true;
                return messages;
             }            
                
            //11-1-2011
            //store the valid profileID in appfarbic cache
           // CachingFactory.MembersViewModelHelper.SaveProfileIDBySessionID( model.activateprofilemodel.profileid, this.HttpContext);

           
            //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
           // photoeditmodel photoviewmodel = new photoeditmodel();
            //registermodel registerviewmodel = new registermodel();
            model.registermodel.emailaddress  = profile.emailaddress ;
            model.registermodel.activationcode = profile.activationcode ; //model.activateprofilemodel.ActivationCode;
            model.registermodel.username  =  profile.username; // model.activateprofilemodel.profileid;  //store the profileID i.e email addy into photo viewmodel
            //registerviewmodel.RegistrationPhotos = photoviewmodel;  //map it to the empty photo view model
            //add the registermodel to the activate model          
            //membersmodel.Register = registerviewmodel;         
            //store the members viewmodel
            //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, model.activateprofilemodel.profileid);
            //populate the values of all the form feilds from model if they are empty
            //verify that the modelstate is good beforeing even starting, this so that in case it was a redirecect 
            // from action we display any preivous errors from another associated partial view
           // if (ModelState.IsValid != true)
           // {
                //show the photo partial view as well since any previous errors will be from here
               // model.activateprofilemodel.PhotoStatus = true;
                //ModelState.Clear();
               // return View(model);
           // }


            // create temprary instances of both models since the partial view only passes one or the other not both
            //depending on which partial view made the request
            var activateProfileModel = new activateprofilemodel();
            var photoModel = new photoeditmodel();

            //5/11/2011
            // add photo view model stuff
            model.activateprofilephotos = photoModel;


            //store temporary variables for this request
           // TempData["ProfileID"] =  model.activateprofilemodel.profileid;
           // TempData["ActivationCode"] = model.activateprofilemodel.ActivationCode;

            //no need for this sicne we have a route for it
            // HttpRequestBase rd = this.Request;
            // model.ProfileId =  rd.QueryString.Get("ProfileId");
            //model.ActivationCode = rd.QueryString.Get("ActivationCode");

            //after a photo has been uploaded or if no photo needed to be uploaded, attempt to validate the user's information           

            //Server side validation begins here  activation code is validated though the data model , but we still need to validate that the user has photo 
            //*****************************************************************************
                
            //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error
            if (_memberepository.checkifemailalreadyexists(profile.emailaddress) == false)
            {
                messages.errormessages.Add ("Invalid Activation Code or Email Address");
                //hide the photo view in thsi case
                // model.activateprofilemodel.PhotoStatus = true;
                //return View(model);
                return messages ;
            }
            //else
            //{
            //    ModelState.Clear();  // clear the model state , i.e removes prevalidation
            //}

            //since we got here we can now check if the user has a photo
            //first check to see if there is an email address for the given user on the server add it to the data anotaions validation                 
            //get a value for photo status so we know weather to display uplodad phot dialog or not
            //if the photo status is TRUE then hide the upload photo div
            model.activateprofilemodel.photostatus =  _photorepository.checkforuploadedphotobyprofileid(Convert.ToInt32( model.activateprofilemodel.profileid));
            if (model.activateprofilemodel.photostatus == false)
            {
                 messages.errormessages.Add("Please upload at least one profile photo using the browser below");
                return messages ;
                
            }
            //else
            //{
            //    ModelState.Clear();  // clear the model state , i.e removes prevalidation
            //}
            //add the error to the model if 


            
                //get username here
                string UserName = _memberepository.getusernamebyprofileid(Convert.ToInt32( model.activateprofilemodel.profileid));
                string ScreenName = _memberepository.getscreennamebyprofileid(Convert.ToInt32( model.activateprofilemodel.profileid));
                    //build log on model
                //create a new login model
                var logonmodel = new LogOnModel();
                var lostaccountinfomodel = new LostAccountInfoModel();
                var lostActivationcodemodel = new LostActivationCodeModel();
                var _logonmodel = new LogonViewModel
                {
                    LogOnModel = logonmodel,
                    LostAccountInfoModel = lostaccountinfomodel,
                    LostActivationCodeModel = lostActivationcodemodel
                };

                //popualate values
                _logonmodel.LogOnModel.UserName = UserName;
                _logonmodel.LogOnModel.Password = "";  //we do not sent password over wire

                //Check here if the profile was alrady activated, if it is add the error and return the view, 
                //If the profile was actived then the next check is if the mailbox folders were created, if they are not then create them here as well
                //validate the profileID first i.e dont even attemp to allow them to upload a photo if that was the issue if the profile ID is inccorect the just ruturn the view with the generic activaion code error



                if (_memberepository.checkifprofileisactivated( model.activateprofilemodel.profileid) == true)
                {
                    messages.errormessages.Add ( "Your Profile has already been activated");
                    //hide the photo view in thsi case
                    //ViewData["ActivateProfileStatus"]=
                    // return View("LogOn", _logonmodel);
                    return messages;
                }
                //activaate profile here
                else
                {
                    _memberepository.activateprofile( model.activateprofilemodel.profileid);
                }

                //check if mailbox folders exist, if they dont create em , don't add any error status
                if (_memberepository.checkifmailboxfoldersarecreated( model.activateprofilemodel.profileid) == true)
                {
                    //ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case                  
                }
                //create the mailbox folders if they do not exist
                else
                {
                    _memberepository.createmailboxfolders( model.activateprofilemodel.profileid);
                }

                messages.message = "Activation Sucssesful";
                return messages;

            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,ex,model.activateprofilemodel.profileid  , null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
             

                   
        }
        
        #endregion

        // Other overrides not implemented
        #region "Other overrides not implemented"

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }


        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

     


       



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {

            get
            {
                return 6;

              //  throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public override bool UnlockUser(string username)
        {
            throw new NotImplementedException();
        }


        #endregion


      
    }
}