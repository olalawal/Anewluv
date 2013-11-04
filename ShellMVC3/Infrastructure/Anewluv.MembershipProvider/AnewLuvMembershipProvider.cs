
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using System.Data;






    using System.Collections.ObjectModel;
    using System.Security.Principal;

    using System.Linq;
    using System.Web;


   // using Common;
using Shell.MVC2.Infrastructure;


    using System.Globalization;

using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Interfaces;
using System.Web.Security;
using Anewluv.DataAccess.Interfaces;
using Anewluv.Domain.Data;
using Anewluv.DataExtentionMethods;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Lib;
using GeoData.Domain.Models.ViewModels;


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

        IUnitOfWork db;
        private LoggingLibrary.ErroLogging logger;


         public AnewLuvMembershipProvider(IUnitOfWork UnitofWork)            
        {
            db = UnitofWork;
        }



        public override bool ValidateUser(string username, string password)
        {
            profile profile = new profile();
            
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
                  profile = db.GetRepository<profile>().getactivatedgrofilebyusername(new ProfileModel { username = username});



                 if (profile != null)
                 {
                     //retirve encypted password
                     encryptedPassword = profile.password;
                     creationdate = profile.creationdate.GetValueOrDefault();
                     passwordchangedate = profile.passwordChangeddate;
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


                 //TO DO change this to use activity not log time since its a better measure for the data we need 
                 //FIX the logtime code
                 //check if decrypted string macthed username to upper  + secret
                 if (actualpasswordstring == decryptedPassword)
                 {
                     //log the user logtime here so it is common to silverlight and MVC                  
                     if (HttpContext.Current != null)
                     {
                           profileextentionmethods.updateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = profile.id, sessionid = HttpContext.Current.Session.SessionID },db );

                       //  _memberepository.updateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = profile.id, sessionid = HttpContext.Current.Session.SessionID });

                     }
                     else
                     {
                         profileextentionmethods.updateuserlogintimebyprofileid(new ProfileModel { profileid = profile.id },db);
                     }

                    // TO DO get geodata from IP address down the line
                    // also update profile activity
                     profileextentionmethods.updateprofileactivity(
                         new profileactivity
                         {
                             activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                             ,
                             creationdate = DateTime.Now,
                             profile_id = profile.id,
                             ipaddress = HttpContext.Current.Request.UserHostAddress,
                             routeurl = HttpContext.Current.Request.RawUrl,
                             sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                         },db);

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
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profile != null ? profile.id : 0, null);
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
            //IQueryable<profile> myQuery = default(IQueryable<profile>);
            //Dim ctx As New Entities()
            profile profile = new profile();

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

               profile  = db.GetRepository<profile>().FindSingle(p => p.username == username);//&& p.ProfileStatusID == 2);


              if (profile != null)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    if (HttpContext.Current != null)
                    {
                        profileextentionmethods.updateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = profile.id, sessionid = HttpContext.Current.Session.SessionID }, db);
                    }
                    else
                    {
                        profileextentionmethods.updateuserlogintimebyprofileid(new ProfileModel { profileid = profile.id }, db);
                    }
                    // also update profile activity
                    profileextentionmethods.updateprofileactivity(
                        new profileactivity
                        {
                            activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                            ,
                            creationdate = DateTime.Now,
                            profile_id = profile.id,
                            ipaddress = HttpContext.Current.Request.UserHostAddress,
                            routeurl = HttpContext.Current.Request.RawUrl,
                            sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                        }, db);

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
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profile != null ? profile.id : 0, null);
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

            profile profile = new profile();
          
            try
            {
                //open ID members are already verifed but it is posublethat a member who is not activated tries to use open ID
                //so they could be in order status 1
                profile = db.GetRepository<profile>().FindSingle(p => p.emailaddress == VerifedEmail && p.status.id <= 2);

                //get the openid providoer
                lu_openidprovider provider = db.GetRepository<lu_openidprovider>().FindSingle(p => (p.description).ToUpper() == openidProvidername.ToUpper());
                if (provider == null) return false;


                //check for the openIDidenfier , to see if it was used before , if it was do nothing but normal updates for user
                var myopenIDstore = profile.openids.Where(p => p.openididentifier == openidIdentifer && provider.description.ToUpper() == openidProvidername.ToUpper() && p.active == true).FirstOrDefault();

                //if we found an openID store for this type
                if (myopenIDstore == null && profile != null)
                //add the openID provider if its a new one
                {
                    profileextentionmethods.addnewopenidforprofile(new ProfileModel { profileid = profile.id, openididentifier = openidIdentifer ,openidprovider = openidProvidername },db);
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
                if (profile != null)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    if (HttpContext.Current != null)
                    {
                        profileextentionmethods.updateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = profile.id, sessionid = HttpContext.Current.Session.SessionID }, db);
                    }
                    else
                    {
                        profileextentionmethods.updateuserlogintimebyprofileid(new ProfileModel { profileid = profile.id }, db);
                    }
                    // also update profile activity
                    profileextentionmethods.updateprofileactivity(
                        new profileactivity
                        {
                            activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                            ,
                            creationdate = DateTime.Now,
                            profile_id = profile.id,
                            ipaddress = HttpContext.Current.Request.UserHostAddress,
                            routeurl = HttpContext.Current.Request.RawUrl,
                            sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                        }, db);

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
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profile != null ? profile.id : 0, null);
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



            AnewLuvMembershipUser membershipprovider = new AnewLuvMembershipUser();
            
          using (var transaction = db.BeginTransaction())
            {
                try
                {
                    // AnewluvContext  mydb = new  AnewluvContext();




                    //4/12/2013 OLAWAL  added code to make sure that dupe email,username is not allowed is now allowed

                    if (db.GetRepository<profile>().checkifemailalreadyexists(new ProfileModel { email = email }) == true)
                    {
                        status = MembershipCreateStatus.DuplicateEmail;
                        return membershipprovider;
                    }
                    if (db.GetRepository<profile>().checkifusernamealreadyexists(new ProfileModel { username = username }) == true)
                    {
                        status = MembershipCreateStatus.DuplicateUserName;
                        return membershipprovider;
                    }


                    profile ObjProfileEntity = new profile();
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
                    //store country ID for use later
                    countryID = Api.GeoService.getcountryidbycountryname(country);


                    //split up the city from state province
                    var tempCityAndStateProvince = city.Split(',');



                    //set all the entity values for profile
                    ObjProfileEntity.username = username;
                    ObjProfileEntity.emailaddress = email;
                    //changed the encryption to something stronger
                    //make username upper so that we can get actual mateches withoute user having to type in a case sensitive username
                    ObjProfileEntity.password = (openidIdentifer != "") ? Encryption.encryptString(password) : openidIdentifer;
                    // ObjProfileEntity.id   = email;
                    ObjProfileEntity.screenname = screenname;
                    //need to add a new feild
                    ObjProfileEntity.activationcode = activationcode;
                    //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                    ObjProfileEntity.creationdate = System.DateTime.Now;
                    ObjProfileEntity.modificationdate = System.DateTime.Now;
                    ObjProfileEntity.logindate = System.DateTime.Now;
                    // fix this to null
                    ObjProfileEntity.forwardmessages = 1;
                    //  ObjProfileEntity.SecurityQuestionID = 1;                
                    // ObjProfileEntity.SecurityAnswer =  securityAnswer;
                    ObjProfileEntity.status = (openidIdentifer == "" || openidIdentifer == null) ? db.GetRepository<lu_profilestatus>().FindSingle(p => p.id == 1) : db.GetRepository<lu_profilestatus>().FindSingle(p => p.id == 2);   //auto activate profiles fi we have an openID user since we have verifed thier info



                    //Build the profile data table
                    // objprofileDataEntity.id  = ;
                    //objprofileDataEntity.profile.emailaddress = email;
                    objprofileDataEntity.latitude = latitude;
                    objprofileDataEntity.longitude = longitude; //_GpsData.longitude;
                    objprofileDataEntity.city = tempCityAndStateProvince[0];
                    objprofileDataEntity.countryregion = "NA";

                    objprofileDataEntity.stateprovince = ((tempCityAndStateProvince.Count() > 1)) ? tempCityAndStateProvince[1] : "NA";

                    objprofileDataEntity.countryid = countryID;
                    objprofileDataEntity.postalcode = zippostalcode;
                    objprofileDataEntity.gender = db.GetRepository<lu_gender>().FindSingle(p => p.description == gender);


                    //  =  Int32.Parse(gender): objprofileDataEntity.gender.GenderName  = gender;
                    objprofileDataEntity.birthdate = birthdate;
                    objprofileDataEntity.phone = "NA";
                    objprofileDataEntity.aboutme = "Hello";



                    //TOD DO add open ID identifier as well Profider type to ssoProvider table 

                    db.Add(ObjProfileEntity);
                    db.Add(objprofileDataEntity);
                    int i = db.Commit();
                    transaction.Commit();


                    //populate the object to send back so we do not have to requery from athe service side
                    profile profile = db.GetRepository<profile>().getprofilebyusername(new ProfileModel { username = username });
                    membershipprovider.profileid = profile.id;
                    membershipprovider.Email = email;
                    status = MembershipCreateStatus.Success;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    status = MembershipCreateStatus.ProviderError;
                    membershipprovider = null;
                    throw;
                }
                finally
                {
                    Api.DisposeGeoService();
                }
        }

            return membershipprovider;
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
                var username = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt16(profileid) }).username;
                var generatedpassword = "";
                if (username != "")
                {
                    //we have the generated password now update the user's account with new password

                    generatedpassword = GeneratePassword();
                    profileextentionmethods.updatepassword(new ProfileModel { profileid = Convert.ToInt16(profileid), encryptedpassword = Encryption.encryptString(generatedpassword) }, db);


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
            using (var transaction = db.BeginTransaction())
            {
                try
                {

                    // AnewluvContext  mydb = new  AnewluvContext();

                    // dbContext.Dispose();


                    //get profile and profile datas
                    profile ObjProfileEntity = db.GetRepository<profile>().getprofilebyprofileid(new ProfileModel { profileid = Convert.ToInt16(u.profileid) });
                    profiledata objprofileDateEntity = db.GetRepository<profiledata>().getprofiledatabyprofileid(new ProfileModel { profileid = Convert.ToInt16(u.profileid) });
                    new gpsdata();

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
                    int countryID = Api.GeoService.getcountryidbycountryname(u.country);

                    //get the longidtue and latttude 
                    gpsdata _GpsData = Api.GeoService.getgpsdatabycitycountrypostalcode(u.country, tempCityAndStateProvince[0], u.ziporpostalcode);



                    //split up the city from state province
                    //Build the profile data table                   
                    objprofileDateEntity.latitude = _GpsData.Latitude;
                    objprofileDateEntity.longitude = _GpsData.Longitude;
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
                    objprofileDateEntity.gender.id = Int32.Parse(u.gender);
                    objprofileDateEntity.birthdate = u.birthdate;
                    objprofileDateEntity.phone = "NA";
                    //objprofileDateEntity.AboutMe = "Hello";



                    //set all the entity values
                    // ObjProfileEntity.username = username;
                    //changed the encryption to something stronger

                    //only update password if it changed
                    if (u.password != null)
                    {
                        ObjProfileEntity.password = Encryption.encryptString(u.password);
                        ObjProfileEntity.passwordChangeddate = DateTime.Now;
                        ObjProfileEntity.passwordchangecount = (ObjProfileEntity.passwordchangecount == null) ? 1 : ObjProfileEntity.passwordchangecount + 1;
                    }
                    // ObjProfileEntity.ProfileID = email;
                    // ObjProfileEntity.ScreenName = screenname;
                    //need to add a new feild
                    // ObjProfileEntity.ActivationCode = Common.Encryption.EncodeString(email + screenname);
                    //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                    //ObjProfileEntity.creationdate = System.DateTime.Now;
                    ObjProfileEntity.modificationdate = System.DateTime.Now;
                    // ObjProfileEntity.LoginDate = System.DateTime.Now;
                    // fix this to null
                    //ObjProfileEntity.ForwardMessages = 1;
                    //  ObjProfileEntity.SecurityQuestionID = System.Convert.ToByte(u.securityquestion );
                    //   ObjProfileEntity.SecurityAnswer = u.securityanswer;
                    //ObjProfileEntity.ProfileStatusID = 1;



                    //dbContext.AddToprofiledatas(objprofileDateEntity);
                    // dbContext.AddToprofiles(ObjProfileEntity);

                    db.Update(ObjProfileEntity);
                    db.Update(objprofileDateEntity);
                    db.Commit();
                    transaction.Commit();





                    // status = MembershipCreateStatus.Success;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
                finally
                {
                    Api.DisposeGeoService();
                }
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
           profile u = new profile();

            //using (AnewluvContext datingcontext = new AnewluvContext())
            //{

            
            //    //get roles too maybe

            //    //    strScreenName = tmpScreenName.FirstOrDefault().ToString();

            //}

           u = db.GetRepository<profile>().getprofilebyusername(new ProfileModel { username = username });


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
        public AnewluvMessages activateprofile(activateprofilemodel model)
        {
            AnewluvMessages messages = new AnewluvMessages();
            messages.message = "";
            messages.errormessages = null;
           profile  profile= new   profile();
            
            try
            {
            //Clear any errors kinda redundant tho  
            //also create a members view model to store pertinent data i.e persist photos profile ID etc
            var membersmodel = new MembersViewModel();
            //get the macthcing member data using the profile ID/email entered
            profile = db.GetRepository<profile>().getprofilebyemailaddress(new ProfileModel { email = model.emailaddress });
           //  membersmodel =  _m .GetMemberData( model.activateprofilemodel.profileid);

            //verify that user entered correct email before doing anything
            //TO DO add these error messages to resource files
            if (profile == null | db.GetRepository<profile>().checkifemailalreadyexists(new ProfileModel { email = profile.emailaddress }) == false)
            {
                messages.errormessages.Add("There is no registered account with the email address: " +  model.emailaddress  + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                //hide the photo view in thsi case
                model.photostatus = true;
                return messages;
             }            
               
 

            //11-1-2011
            //store the valid profileID in appfarbic cache
            // CachingFactory.MembersViewModelHelper.SaveProfileIDBySessionID( model.activateprofilemodel.profileid, this.HttpContext);
            model.photostatus =   Api.PhotoService.checkforuploadedphotobyprofileid( profile.id.ToString());

           
            //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
           // photoeditmodel photoviewmodel = new photoeditmodel();
            //registermodel registerviewmodel = new registermodel();
            model.emailaddress  = profile.emailaddress ;
            model.activationcode = profile.activationcode; //model.activateprofilemodel.ActivationCode;
           // model.profileid = profile.id;
           // model.activateprofilemodel.username = profile.username; // model.activateprofilemodel.profileid;  //store the profileID i.e email addy into photo viewmodel
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
            //var activateProfileModel = new activateprofilemodel();
            //var photoModel = new photoeditmodel();

            //5/11/2011
            //TO DO USE TASK for this
            // add photo view model stuff
            //Need to me made to run asynch
            if (model.photouploadviewmodel.photosuploaded.Count() > 0)
            {
                Api.PhotoService.addphotos(model.photouploadviewmodel);
            }

           
            //since we got here we can now check if the user has a photo
            //first check to see if there is an email address for the given user on the server add it to the data anotaions validation                 
            //get a value for photo status so we know weather to display uplodad phot dialog or not
            //if the photo status is TRUE then hide the upload photo div

            if (model.photostatus == false | model.photouploadviewmodel.photosuploaded.Count() > 0)
            {
                messages.errormessages.Add("Please upload at least one profile photo using the browser below");
                return messages ;
                
            }


            if (db.GetRepository<profile>().checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true)
                {
                    messages.errormessages.Add ("Your Profile has already been activated");
                    //hide the photo view in thsi case
                    //ViewData["ActivateProfileStatus"]=
                    // return View("LogOn", _logonmodel);
                    return messages;
                }
                //activaate profile here
                else
                {
                    profileextentionmethods.activateprofile(new ProfileModel { profileid = profile.id },db);
                }

                //check if mailbox folders exist, if they dont create em , don't add any error status
                if ( db.GetRepository<profile>().checkifmailboxfoldersarecreated(new ProfileModel { profileid = profile.id }) == true)
                {
                    //ModelState.AddModelError("", "Your Profile has already been activated");
                    //hide the photo view in thsi case                  
                }
                //create the mailbox folders if they do not exist
                else
                {
                    profileextentionmethods.createmailboxfolders(new ProfileModel { profileid = profile.id },db);
                }

                messages.message = "Activation Sucssesful";
                return messages;

            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,ex,profile.id  , null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                // status = MembershipCreateStatus.ProviderError;
                // newUser = null;
                throw;
            }
             

                   
        }

      public  AnewluvMessages recoveractivationcode(activateprofilemodel model)
        {
         
         profile profile = new profile();
            try
            {
                AnewluvMessages messages = new AnewluvMessages();
                messages.message = "";
                messages.errormessages = null;
             

                //Clear any errors kinda redundant tho  
                //also create a members view model to store pertinent data i.e persist photos profile ID etc
                var membersmodel = new MembersViewModel();
                //get the macthcing member data using the profile ID/email entered
                profile = db.GetRepository<profile>().getprofilebyemailaddress(new ProfileModel { email = model.emailaddress });
                //  membersmodel =  _m .GetMemberData( model.activateprofilemodel.profileid);

                //verify that user entered correct email before doing anything
                //TO DO add these error messages to resource files
                if (profile == null | db.GetRepository<profile>().checkifemailalreadyexists(new ProfileModel { email = profile.emailaddress }) == false)
                {
                    messages.errormessages.Add("There is no registered account with the email address: " + model.emailaddress +" on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                    //hide the photo view in thsi case
                    model.photostatus = true;
                    return messages;
                }
                else if (db.GetRepository<profile>().checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true)
                {
                    messages.errormessages.Add("Your Profile has already been activated");
                    //hide the photo view in thsi case
                    //ViewData["ActivateProfileStatus"]=
                    // return View("LogOn", _logonmodel);
                    return messages;
                }

                //oke send back theer activvation code
                messages.message = "Your activiation code has been sent to the email address: " + model.emailaddress;
         

                return messages;

            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.UserAuthorizationService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex,profile.id, null);
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