using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Web.Security;

using Anewluv.Services.Contracts;

using System.ServiceModel.Activation;
using System.ServiceModel;
using Anewluv.Services.Contracts.ServiceResponse;
using Nmedia.DataAccess.Interfaces;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Anewluv.DataExtentionMethods;
using Anewluv.Lib;
using System.Web;
using GeoData.Domain.Models.ViewModels;


namespace Anewluv.Services.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuthenticationService : MembershipProvider,IAuthenticationService
    {


          IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;
        //constant strings for reseting passwords
        const String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; const String LOWER = "abcdefghijklmnopqrstuvwxyz"; const String NUMBERS = "1234567890"; const String SPECIAL = "*$-+?&=!%/";
        
        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public AuthenticationService(IUnitOfWork unitOfWork)
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
         

        }

            public AnewluvResponse  createuser(MembershipUserViewModel model)
            {              

                try
                {
                    // string username,
                    //string password,
                    //string email, string securityQuestion,
                    //   string securityAnswer,
                    //bool isApproved,
                    //string providerUserKey,
                    MembershipCreateStatus status;

                    var membershipprovider = CreateUserCustom(model.username, model.password, model.openidIdentifer,
                       model.openidProvidername,
                     model.email,
                    DateTime.Now, model.gender, model.country, model.city, model.stateprovince, model.longitude, model.lattitude,
                    model.screenname, model.zippostalcode, model.activationcode, false, model.providerUserKey,
                     out status);

                    AnewluvResponse response = new AnewluvResponse();
                    ResponseMessage responsemessage = new ResponseMessage();
                    switch (status)
                    {

                        case MembershipCreateStatus.Success:
                            //get the profile info to return
                            //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getprofilebyusername(model.username);                         
                            response.profileid1 = membershipprovider.profileid.ToString(); //profile.id.ToString();
                            response.email = membershipprovider.Email; //profile.emailaddress;
                            responsemessage = new ResponseMessage("", "Profile created succesfully", "");
                            break;
                        case MembershipCreateStatus.DuplicateUserName :
                           // AnewluvResponse response = new AnewluvResponse();
                            responsemessage = new ResponseMessage("", "Unable to create profile","Duplicate username : the username :" + model.username + "already exists"); 
                            break;
                        case MembershipCreateStatus.DuplicateEmail :
                             responsemessage = new ResponseMessage("","Unable to create profile", "Duplicate email : the email :" + model.email  + "already exists");                         
                            break;
                        default:
                           // Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                              ResponseMessage reponsemessage = new ResponseMessage("", "Unable to create profile","There was a problem creation the profile, please try again later");
                        response.ResponseMessages.Add(reponsemessage);  
                            break;
                    }

                    response.ResponseMessages.Add(responsemessage);  
                    return response;
                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

                //return null;
            }

            public AnewLuvMembershipUser createusercustom(MembershipUserViewModel model)
            {
                MembershipCreateStatus status;
                return CreateUserCustom (model.username,
                            model.password, model.openidIdentifer, model.openidProvidername,
                           model.email,
                           model.birthdate, model.gender, model.country, model.city, model.stateprovince, 
                           model.longitude, model.lattitude, model.screenname, model.zippostalcode, model.activationcode,
                           model.isApproved,
                           model.providerUserKey, out status);
            
            }

            public bool validateuserbyusernamepassword(ProfileModel profile)
            {
                return ValidateUser(profile.username, profile.password);

            }

            //5-82012 updated to only valudate username
            //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
        
            public bool validateuserbyusername(ProfileModel profile)
            {
                return ValidateUser(profile.username);
            }

            public bool validateuserbyopenid(ProfileModel profile)
            {
                return ValidateUser(profile.email, profile.openididentifier, profile.openidprovider);
            }          

            public override bool ValidateUser(string username, string password)
            {

                var myQuery = new profile();

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        // return db.GetRepository<profiledata>().getprofiledatabyprofileid(model);
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
                         myQuery =  db.GetRepository<profile>().FindSingle(p => p.username == username && p.status_id == 2);



                        if (myQuery != null)
                        {
                            //retirve encypted password
                            encryptedPassword = myQuery.password;
                            creationdate = myQuery.creationdate.GetValueOrDefault();
                            passwordchangedate = myQuery.passwordChangeddate;
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
                                //Just for testing that it worked
                                //TO DO remove when in prod
                                AsyncCallback callback = result =>
                                {
                                    //we dont do anything really with the callback so not needed really
                                    var value = Api.MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                                };   
  
                                Api.MemberService.Beginupdateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = myQuery.id, sessionid = HttpContext.Current.Session.SessionID }, callback, Api.MemberService);

                            }
                            else
                            {

                                //Just for testing that it worked
                                //TO DO remove when in prod, we dont need a result
                                AsyncCallback callback = result =>
                                {
                                    //we dont do anything really with the callback so not needed really
                                    var value = Api.MemberService.Endupdateuserlogintimebyprofileid(result);                                    
                                };   
  
                                 Api.MemberService.Beginupdateuserlogintimebyprofileid(new ProfileModel { profileid = myQuery.id },callback,Api.MemberService);
                            }

                            //TO DO get geodata from IP address down the line
                            //also update profile activity
                              Api.MemberService.Beginaddprofileactvity(
                                new profileactivity
                                {
                                     lu_activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                                    ,
                                    creationdate = DateTime.Now,
                                    profile_id = myQuery.id,
                                    ipaddress = HttpContext.Current.Request.UserHostAddress,
                                    routeurl = HttpContext.Current.Request.RawUrl,
                                    sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                                },null,Api.MemberService);

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
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myQuery != null ? myQuery.id : 0, null);
                        FaultReason faultreason = new FaultReason("Error in authenitcation service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
                    finally
                    {
                        Api.DisposeMemberService();
                    }

                }
            }

            //5-82012 updated to only valudate username
            //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
            public bool ValidateUser(string username)
            {
                  var myQuery = new profile();

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
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

                        myQuery = db.GetRepository<profile>().FindSingle(p => p.username == username);//&& p.ProfileStatusID == 2);


                        if (myQuery != null)
                        {
                            //log the user logtime here so it is common to silverlight and MVC
                            if (HttpContext.Current != null)
                            {
                                Api.MemberService.Beginupdateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = myQuery.id, sessionid = HttpContext.Current.Session.SessionID },null,Api.MemberService);
                            }
                            else
                            {
                                Api.MemberService.Beginupdateuserlogintimebyprofileid(new ProfileModel { profileid = myQuery.id }, null, Api.MemberService);
                            }
                            //TO DO get geodata from IP address down the line
                            //also update profile activity
                            Api.MemberService.Beginaddprofileactvity(
                              new profileactivity
                              {
                                  lu_activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                                  ,
                                  creationdate = DateTime.Now,
                                  profile_id = myQuery.id,
                                  ipaddress = HttpContext.Current.Request.UserHostAddress,
                                  routeurl = HttpContext.Current.Request.RawUrl,
                                  sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                              }, null, Api.MemberService);

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
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myQuery != null ? myQuery.id : 0, null);
                        FaultReason faultreason = new FaultReason("Error in authenitcation service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }
                    finally
                    {
                        Api.DisposeMemberService();
                    }
                }
            }

            public bool ValidateUser(string VerifedEmail, string openidIdentifer, string openidProvidername)
            {

                var myprofile = new profile();
                  _unitOfWork.DisableProxyCreation = true;
                  using (var db = _unitOfWork)
                  {
                      try
                      {
                          //open ID members are already verifed but it is posublethat a member who is not activated tries to use open ID
                          //so they could be in order status 1
                          myprofile = db.GetRepository<profile>().FindSingle(p => p.emailaddress == VerifedEmail && p.status_id <= 2);

                          //get the openid providoer
                          lu_openidprovider provider = db.GetRepository<lu_openidprovider>().FindSingle(p => (p.description).ToUpper() == openidProvidername.ToUpper());
                          if (provider == null) return false;


                          //check for the openIDidenfier , to see if it was used before , if it was do nothing but normal updates for user
                          var myopenIDstore = myprofile.openids.Where(p => p.openididentifier == openidIdentifer && provider.description.ToUpper() == openidProvidername.ToUpper() && p.active == true).FirstOrDefault();

                          //if we found an openID store for this type
                          if (myopenIDstore == null && myprofile != null)
                          //add the openID provider if its a new one
                          {
                              Api.MemberService.addnewopenidforprofile(new ProfileModel { profileid = myprofile.id });
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
                              //log the user logtime here so it is common to silverlight and MVC
                              if (HttpContext.Current != null)
                              {
                                  Api.MemberService.Beginupdateuserlogintimebyprofileidandsessionid(new ProfileModel { profileid = myprofile.id, sessionid = HttpContext.Current.Session.SessionID }, null, Api.MemberService);
                              }
                              else
                              {
                                  Api.MemberService.Beginupdateuserlogintimebyprofileid(new ProfileModel { profileid = myprofile.id }, null, Api.MemberService);
                              }
                          //TO DO get geodata from IP address down the line
                          //also update profile activity
                          Api.MemberService.Beginaddprofileactvity(
                            new profileactivity
                            {
                                lu_activitytype = db.GetRepository<lu_activitytype>().FindSingle(p => p.id == (int)activitytypeEnum.login)
                                ,
                                creationdate = DateTime.Now,
                                profile_id = myprofile.id,
                                ipaddress = HttpContext.Current.Request.UserHostAddress,
                                routeurl = HttpContext.Current.Request.RawUrl,
                                sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                            }, null, Api.MemberService);

                          //also update the profiledata for the last login date
                          return true;



                          // datingService.UpdateUserLoginTimeByProfileID(VerifedEmail, HttpContext.Current.Session.SessionID);


                          //    return false;
                          // }

                      }
                      catch (Exception ex)
                      {
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myprofile != null ? myprofile.id : 0, null);
                          FaultReason faultreason = new FaultReason("Error in authenitcation service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                      }
                      finally
                      {
                          Api.DisposeMemberService();
                      }
                  }


            }

            public string applicationname()
            {
                // return _anewluvmembershipprovider.ApplicationName();
                return "AnewLuvMemberShipService";
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
                return this.CreateUserCustom(username, password, "", "",
                      email,
                    //  securityQuestion,
                    //  securityAnswer
                     DateTime.Now,
                      "", "", "", "", null, null, "", "", "",
                      false,
                      null,
                      out status);
            }

            public AnewLuvMembershipUser CreateUserCustom(string username,
                       string password, string openidIdentifer, string openidProvidername,
                      string email,
                //  string securityQuestion,
                // string securityAnswer,
                      DateTime birthdate, string gender, string country, string city, string stateprovince, double? longitude, double? latitude, string screenname, string zippostalcode, string activationcode,
                      bool isApproved,
                      object providerUserKey,
                     out MembershipCreateStatus status)
            {



                AnewLuvMembershipUser membershipprovider = new AnewLuvMembershipUser();

                 using (var db = _unitOfWork)
                  {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {

                     
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
                        ObjProfileEntity.lu_profilestatus = (openidIdentifer == "" || openidIdentifer == null) ? db.GetRepository<lu_profilestatus>().FindSingle(p => p.id == 1) : db.GetRepository<lu_profilestatus>().FindSingle(p => p.id == 2);   //auto activate profiles fi we have an openID user since we have verifed thier info



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
                        objprofileDataEntity.lu_gender = db.GetRepository<lu_gender>().FindSingle(p => p.description == gender);


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
                          logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                          FaultReason faultreason = new FaultReason("Error in User Authentication service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                          //throw convertedexcption;
                      }
                      finally
                      {
                           Api.DisposeGeoService();
                      }
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
                    logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    // status = MembershipCreateStatus.ProviderError;
                    // newUser = null;
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }



            //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
            // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
            //finally returns the new password to the calling functon or an empty string if failure.
            public string ResetPasswordCustom(int profileid)
            {

                using (var db = _unitOfWork)
                {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
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
                              Api.MemberService.updatepassword(new ProfileModel { profileid = Convert.ToInt16(profileid) }, Encryption.encryptString(generatedpassword));


                              //'reset the password 
                              return generatedpassword;
                          }
                          // throw new NotImplementedException();
                          return "";

                      }
                      catch (Exception ex)
                      {
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                          //log error mesasge
                          //handle logging here
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                      }
                      finally
                      {
                          Api.DisposeMemberService();
                      }
            }
            }
  }

            public string resetpassword(string profileid, string answer)
            {
                return ResetPassword(profileid, answer);
            }

            public override void UpdateUser(MembershipUser user)
            {



                AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
                using (var db = _unitOfWork)
                {
                    db.IsAuditEnabled = false; //do not audit on adds
                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {


                            //get profile and profile datas
                            profile ObjProfileEntity = db.GetRepository<profile>().FindSingle(p => p.id == Convert.ToInt16(u.profileid));
                            profiledata objprofileDateEntity = db.GetRepository<profiledata>().FindSingle(p => p.profile_id == Convert.ToInt16(u.profileid));

                           // new gpsdata;

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
                            int countryID =  Api.GeoService.getcountryidbycountryname(u.country);

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
                            objprofileDateEntity.gender_id = Int32.Parse(u.gender);
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
                            //save all changes bro                         
                            int i = db.Commit();
                            transaction.Commit();

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();                       
                            //instantiate logger here so it does not break anything else.
                            logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                            //int profileid = Convert.ToInt32(viewerprofileid);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                            FaultReason faultreason = new FaultReason("Error in User Authentication service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                            //throw convertedexcption;
                        }
                        finally
                        {
                            Api.DisposeGeoService();
                        }
                    }
                }
            }

            public void updateuser(MembershipUser user)
            {
                UpdateUser(user);
            }

        
        public override MembershipUser GetUser(string username, bool userIsOnline)
            {
                return this.GetUserCustom(username, userIsOnline);
            }

            public MembershipUser getuser(string username, string userisonline)
            {
                return GetUser(username, Convert.ToBoolean(userisonline));
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
               _unitOfWork.DisableProxyCreation = true;
               using (var db = _unitOfWork)
               {
                   try
                   {
                       u = db.GetRepository<profile>().getprofilebyusername(new ProfileModel { username = username });


                       if (u == null)
                           return null;


                       AnewLuvMembershipUser usr = null;

                       string providername = this.ApplicationName;
                       //usr.= strScreenName;
                       // usr.Name = username;
                       // usr.IsSubscriber  = username;
                       usr = new AnewLuvMembershipUser("AnewluvContext",
                                                     username,
                                                     providerUserKey,
                                                    u.emailaddress,
                                                    u.securityanswer,
                                                    "",
                                                    true,
                                                    false,
                                                    u.creationdate.GetValueOrDefault(),
                                                    u.logindate.GetValueOrDefault(),
                                                    u.modificationdate.GetValueOrDefault(),
                                                    DateTime.Now,
                                                    DateTime.Now,
                                                    "");

                       //add the profile as a test
                       usr.thisprofile = u;
                       // datingService.Dispose();
                       return usr;
                       
                   }
                   catch (Exception ex)
                   {

                       
                       //instantiate logger here so it does not break anything else.
                       logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                       //int profileid = Convert.ToInt32(viewerprofileid);
                       logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                       FaultReason faultreason = new FaultReason("Error in User Authentication service");
                       string ErrorMessage = "";
                       string ErrorDetail = "ErrorMessage: " + ex.Message;
                       throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                       //throw convertedexcption;
                   }

               }

                
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
                    logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    // status = MembershipCreateStatus.ProviderError;
                    // newUser = null;
                    throw;
                }

            }

            public string generatepassword()
            {
                return GeneratePassword();
            }

            public void UpdateUserCustom(string username, string ProfileID,
                    string password,
                    string securityQuestion,
                    string securityAnswer,
                    DateTime birthdate, string gender, string country, string city, string zippostalcode)
            {
            }

            #region "Extra Methods added to interface to clean up MVC controllers that do member stuff"

            //1-8-2013 olawal addedrobust method for activating profiles
            public AnewluvResponse activateprofile(activateprofilemodel model)
            {
                AnewluvMessages messages = new AnewluvMessages();
                messages.message = "";
                messages.errormessages = null;
               profile profile = new profile();


                using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
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
                              messages.errormessages.Add("There is no registered account with the email address: " + model.emailaddress + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                              //hide the photo view in thsi case
                              model.photostatus = true;
                            //  return messages;
                          }



                          //11-1-2011
                          //store the valid profileID in appfarbic cache
                          // CachingFactory.MembersViewModelHelper.SaveProfileIDBySessionID( model.activateprofilemodel.profileid, this.HttpContext);
                          model.photostatus = Api.PhotoService.checkforuploadedphotobyprofileid((profile.id.ToString()));


                          //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
                          // photoeditmodel photoviewmodel = new photoeditmodel();
                          //registermodel registerviewmodel = new registermodel();
                          model.emailaddress = profile.emailaddress;
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
                              //return messages;

                          }


                          if (db.GetRepository<profile>().checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true)
                          {
                              messages.errormessages.Add("Your Profile has already been activated");
                              //hide the photo view in thsi case
                              //ViewData["ActivateProfileStatus"]=
                              // return View("LogOn", _logonmodel);
                              //return messages;
                          }
                          //activaate profile here
                          else
                          {
                             Api.MemberService.activateprofile(new ProfileModel { profileid = profile.id });
                          }

                          //check if mailbox folders exist, if they dont create em , don't add any error status
                          if (Api.MemberService.checkifmailboxfoldersarecreated(new ProfileModel { profileid = profile.id }) == true)
                          {
                              //ModelState.AddModelError("", "Your Profile has already been activated");
                              //hide the photo view in thsi case                  
                          }
                          //create the mailbox folders if they do not exist
                          else
                          {
                              Api.MemberService.createmailboxfolders(new ProfileModel { profileid = profile.id });
                          }

                          messages.message = "Activation Sucssesful";

                          AnewluvResponse response = new AnewluvResponse();

                          if (messages.errormessages.Count() == 0)
                          {
                              //get the profile info to return
                              //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                              //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                              response.email = model.emailaddress;//profile.emailaddress;
                              ResponseMessage reponsemessage = new ResponseMessage("", messages.message, "");
                              response.ResponseMessages.Add(reponsemessage);

                          }
                          else
                          {
                              ResponseMessage reponsemessage = new ResponseMessage("", "There was a problem activating the profile, please try again later", messages.errormessages.First());
                              response.ResponseMessages.Add(reponsemessage);
                          }

                          return response;
                          
                         // return messages;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                          FaultReason faultreason = new FaultReason("Error in User Authentication service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                          //throw convertedexcption;
                      }
                      finally
                      {
                          Api.DisposePhotoService();
                          Api.DisposeMemberService();
                      }

                    
                  }
            }


        


            }

            public AnewluvResponse recoveractivationcode(activateprofilemodel model)
            {

                profile profile = new profile();
                AnewluvResponse response = new AnewluvResponse();


                using (var db = _unitOfWork)
                {
                    db.IsAuditEnabled = false; //do not audit on adds
                    using (var transaction = db.BeginTransaction())
                    {
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
                                messages.errormessages.Add("There is no registered account with the email address: " + model.emailaddress + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                                //hide the photo view in thsi case
                                model.photostatus = true;
                               
                            }
                            else if ( Api.MemberService.checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true)
                            {
                                messages.errormessages.Add("Your Profile has already been activated");
                                //hide the photo view in thsi case
                                //ViewData["ActivateProfileStatus"]=
                                // return View("LogOn", _logonmodel);
                               
                            }

                            if (messages.errormessages.Count() == 0)
                            {
                                //get the profile info to return
                                //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                                //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                                //oke send back theer activvation code
                                messages.message = "Your activiation code has been sent to the email address: " + model.emailaddress;

                                response.email = model.emailaddress;//profile.emailaddress;
                                ResponseMessage reponsemessage = new ResponseMessage("", messages.message, "");
                                response.ResponseMessages.Add(reponsemessage);
                                //send the email vai service

                            }
                            else
                            {
                                ResponseMessage reponsemessage = new ResponseMessage("", "There was a problem sending your activation code please try again later", messages.errormessages.First());
                                response.ResponseMessages.Add(reponsemessage);
                            }


                            return response;
                            //return messages;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new ErroLogging(logapplicationEnum.UserAuthorizationService);
                            //int profileid = Convert.ToInt32(viewerprofileid);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                            FaultReason faultreason = new FaultReason("Error in User Authentication service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                            //throw convertedexcption;
                        }
                        finally
                        {
                            Api.DisposeMemberService();
                        }
                    }
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
