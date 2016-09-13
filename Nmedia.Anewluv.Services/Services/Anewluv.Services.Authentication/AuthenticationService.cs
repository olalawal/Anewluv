using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Web.Security;


using Anewluv.Services.Contracts;

using System.ServiceModel.Activation;
using System.ServiceModel;
//using Nmedia.DataAccess.Interfaces;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data;
using System.Web;
using Anewluv.Domain;

using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using GeoData.Domain.Models;
using GeoData.Domain.ViewModels;
using GeoData.Domain.Models.ViewModels;
using Anewluv.Services.Contracts.ServiceResponse;
using Nmedia.Infrastructure;
using Anewluv.DataExtentionMethods;
using System.Threading.Tasks;

using Nmedia.Infrastructure.Domain.Data.CustomClaimToken;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Repository.Pattern.UnitOfWork;
using Nmedia.Infrastructure.Domain.Data.Notification;
using Nmedia.Infrastructure.Helpers;
using Repository.Pattern.Infrastructure;
using Nmedia.Infrastructure.Utils;
using System.Data.Entity.Spatial;




namespace Anewluv.Services.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuthenticationService : MembershipProvider, IAuthenticationService
    {

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGeoDataStoredProcedures _storedProcedures;
        //private  IUnitOfWorkAsync _unitOfWorkAsync;
        //private LoggingLibrary.Logging logger;
        //constant strings for reseting passwords
        const String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; const String LOWER = "abcdefghijklmnopqrstuvwxyz"; const String NUMBERS = "1234567890"; const String SPECIAL = "*$-+?&=!%/";

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public AuthenticationService(IUnitOfWorkAsync unitOfWork, IGeoDataStoredProcedures storedProcedures)
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
            _storedProcedures = storedProcedures;

        }


        public async Task<AnewluvResponse> createuser(MembershipUserViewModel model)
        {

            try
            {

                var task = Task.Factory.StartNew(() =>
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
                   model.birthdate.Value, model.genderid, model.country, model.countryid, model.city, model.stateprovince, model.longitude, model.latitude,
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
                        case MembershipCreateStatus.DuplicateUserName:
                            // AnewluvResponse response = new AnewluvResponse();
                            responsemessage = new ResponseMessage("", "Unable to create profile", "Duplicate username : the username :" + model.username + "already exists");
                            break;
                        case MembershipCreateStatus.DuplicateEmail:
                            responsemessage = new ResponseMessage("", "Unable to create profile", "Duplicate email : the email :" + model.email + "already exists");
                            break;
                        default:
                            // Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                            ResponseMessage reponsemessage = new ResponseMessage("", "Unable to create profile", "There was a problem creation the profile, please try again later");
                            response.ResponseMessages.Add(reponsemessage);
                            break;
                    }

                    response.ResponseMessages.Add(responsemessage);
                    return response;
                });
                return await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                }

                FaultReason faultreason = new FaultReason("Error in authenitcation service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

            //return null;
        }

        public async Task<AnewLuvMembershipUser> createusercustom(MembershipUserViewModel model)
        {
            MembershipCreateStatus status;
            return CreateUserCustom(model.username,
                        model.password, model.openidIdentifer, model.openidProvidername,
                       model.email,
                       model.birthdate.Value, model.genderid, model.country, model.countryid, model.city, model.stateprovince,
                       model.longitude, model.latitude, model.screenname, model.zippostalcode, model.activationcode,
                       model.isApproved,
                       model.providerUserKey, out status);

        }

        public async Task<bool> validateuserbyusernamepassword(ProfileModel profile)
        {
            var task = Task.Factory.StartNew(() =>
            {
                return ValidateUser(profile.username, profile.password);
            });
            return await task.ConfigureAwait(false);


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

        /// <summary>
        /// depreciated for the new method 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override bool ValidateUser(string username, string password)
        {

            var myQuery = new profile();

            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
            {
                try
                {
                    // return _unitOfWorkAsync.Repository<profiledata>().getprofiledatabyprofileid(model);
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
                    myQuery = _unitOfWorkAsync.Repository<profile>().Query(p => p.username == username && p.status_id == 2).Select().FirstOrDefault();



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
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };



                        }
                        else
                        {


                            //Just for testing that it worked
                            //TO DO remove when in prod
                            AsyncCallback callback = result =>
                            {
                                //we dont do anything really with the callback so not needed really
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };


                        }


                        //TO DO get geodata from IP address down the line
                        //also update profile activity
                        //MemberService.Beginaddprofileactvity(
                        //  new profileactivity
                        //  {
                        //      lu_activitytype = _unitOfWorkAsync.Repository<lu_activitytype>().Query(p => p.id == (int)activitytypeEnum.login)
                        //      ,
                        //      creationdate = DateTime.Now,
                        //      profile_id = myQuery.id,
                        //      ipaddress = HttpContext.Current.Request.UserHostAddress,
                        //      routeurl = HttpContext.Current.Request.RawUrl,
                        //      sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                        //  }, null, MemberService);

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

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myQuery != null ? myQuery.id : 0, null);
                    }

                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                //finally
                //{
                //    Api.DisposeMemberService();
                //}

            }
        }

        //5-82012 updated to only valudate username
        //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
        public bool ValidateUser(string username)
        {
            var myQuery = new profile();

            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
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

                    myQuery = _unitOfWorkAsync.Repository<profile>().Query(p => p.username == username).Select().FirstOrDefault();//&& p.ProfileStatusID == 2);


                    if (myQuery != null)
                    {
                        //log the user logtime here so it is common to silverlight and MVC
                        if (HttpContext.Current != null)
                        {
                            //Just for testing that it worked
                            //TO DO remove when in prod
                            AsyncCallback callback = result =>
                            {
                                //we dont do anything really with the callback so not needed really
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };



                        }
                        else
                        {


                            //Just for testing that it worked
                            //TO DO remove when in prod
                            AsyncCallback callback = result =>
                            {
                                //we dont do anything really with the callback so not needed really
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };

                            //use anew  the same DB context


                        }
                        //TO DO get geodata from IP address down the line
                        //also update profile activity
                        //MemberService.Beginaddprofileactvity(
                        //  new profileactivity
                        //  {
                        //      lu_activitytype = _unitOfWorkAsync.Repository<lu_activitytype>().Query(p => p.id == (int)activitytypeEnum.login)
                        //      ,
                        //      creationdate = DateTime.Now,
                        //      profile_id = myQuery.id,
                        //      ipaddress = HttpContext.Current.Request.UserHostAddress,
                        //      routeurl = HttpContext.Current.Request.RawUrl,
                        //      sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                        //  }, null, MemberService);

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
                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myQuery != null ? myQuery.id : 0, null);
                    }

                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                finally
                {
                    // Api.DisposeMemberService();
                }
            }
        }

        public bool ValidateUser(string VerifedEmail, string openidIdentifer, string openidProvidername)
        {

            var myprofile = new profile();
            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
            {
                try
                {
                    //open ID members are already verifed but it is posublethat a member who is not activated tries to use open ID
                    //so they could be in order status 1
                    myprofile = _unitOfWorkAsync.Repository<profile>().Query(p => p.emailaddress == VerifedEmail && p.status_id <= 2).Select().FirstOrDefault();



                    //get the openid providoer
                    lu_openidprovider provider = _unitOfWorkAsync.Repository<lu_openidprovider>().Queryable().Where(p => (p.description).ToUpper() == openidProvidername.ToUpper()).FirstOrDefault();
                    if (provider == null) return false;


                    //check for the openIDidenfier , to see if it was used before , if it was do nothing but normal updates for user
                    var myopenIDstore = myprofile.openids.Where(p => p.openididentifier == openidIdentifer && provider.description.ToUpper() == openidProvidername.ToUpper() && p.active == true).FirstOrDefault();

                    //if we found an openID store for this type
                    if (myopenIDstore == null && myprofile != null)
                    //add the openID provider if its a new one
                    {

                        //MemberService.addnewopenidforprofile(new ProfileModel { profileid = myprofile.id });
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
                            //Just for testing that it worked
                            //TO DO remove when in prod
                            AsyncCallback callback = result =>
                            {
                                //we dont do anything really with the callback so not needed really
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };



                        }
                        else
                        {


                            //Just for testing that it worked
                            //TO DO remove when in prod
                            AsyncCallback callback = result =>
                            {
                                //we dont do anything really with the callback so not needed really
                                //  MemberService.Endupdateuserlogintimebyprofileidandsessionid(result);
                            };

                            //use anew  the same DB context

                        }
                    //TO DO get geodata from IP address down the line
                    //also update profile activity
                    //MemberService.Beginaddprofileactvity(
                    //  new profileactivity
                    //  {
                    //      lu_activitytype = _unitOfWorkAsync.Repository<lu_activitytype>().Query(p => p.id == (int)activitytypeEnum.login)
                    //      ,
                    //      creationdate = DateTime.Now,
                    //      profile_id = myprofile.id,
                    //      ipaddress = HttpContext.Current.Request.UserHostAddress,
                    //      routeurl = HttpContext.Current.Request.RawUrl,
                    //      sessionid = HttpContext.Current != null ? HttpContext.Current.Session.SessionID : null
                    //  }, null, MemberService);

                    //also update the profiledata for the last login date
                    return true;



                    // datingService.UpdateUserLoginTimeByProfileID(VerifedEmail, HttpContext.Current.Session.SessionID);


                    //    return false;
                    // }

                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {

                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myprofile != null ? myprofile.id : 0, null);
                    }


                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                //finally
                //{
                //    Api.DisposeMemberService();
                //}
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
            //set default values for a basic member created with just password,
            //sec question and answers
            return this.CreateUserCustom(username, password, "", "",
                  email,
                //  securityQuestion,
                //  securityAnswer
                 DateTime.Now,
                  "", "", null, "", "", null, null, "", "", "",
                  false,
                  null,
                  out status);
        }


        /// <summary>
        /// actual impelemation
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="openidIdentifer"></param>
        /// <param name="openidProvidername"></param>
        /// <param name="email"></param>
        /// <param name="birthdate"></param>
        /// <param name="gender"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="stateprovince"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="screenname"></param>
        /// <param name="zippostalcode"></param>
        /// <param name="activationcode"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public AnewLuvMembershipUser CreateUserCustom(string username,
                   string password, string openidIdentifer, string openidProvidername,
                  string email,
            //  string securityQuestion,
            // string securityAnswer,
                  DateTime birthdate, string genderid, string country, int? countryid, string city, string stateprovince, string longitude, string latitude, string screenname, string zippostalcode, string activationcode,
                  bool isApproved,
                  object providerUserKey,
                 out MembershipCreateStatus status)
        {



            AnewLuvMembershipUser membershipprovider = new AnewLuvMembershipUser();

            //  using (var db = _unitOfWorkAsync)
            {
                //////do not audit on adds
                //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        //4/12/2013 OLAWAL  added code to make sure that dupe email,username is not allowed is now allowed
                        //get profile and profile data's
                        var profilerepo = _unitOfWorkAsync.Repository<profile>();
                        var profiledatarepo = _unitOfWorkAsync.Repository<profiledata>();

                        if (_unitOfWorkAsync.Repository<profile>().checkifemailalreadyexists(new ProfileModel { email = email }) == true)
                        {
                            status = MembershipCreateStatus.DuplicateEmail;
                            return membershipprovider;
                        }
                        if (_unitOfWorkAsync.Repository<profile>().checkifusernamealreadyexists(new ProfileModel { username = username }) == true)
                        {
                            status = MembershipCreateStatus.DuplicateUserName;
                            return membershipprovider;
                        }


                        profile ObjProfileEntity = new profile();
                        profiledata objprofileDataEntity = new profiledata();
                        profilemetadata objprofileMetaDataEntity = new profilemetadata();
                        

                        //TO DO new entity for OPEN ID data

                        Random objRandom = new Random();
                        int intStart = objRandom.Next(1, 9);
                        int intLastTwo = objRandom.Next(10, 99);
                        //int genderid = Convert.ToInt32(genderid);
                        //convert the string values to byte as needed
                        //NumberFormatInfo provider = new NumberFormatInfo();
                        // These properties affect the conversion.
                        //provider.PositiveSign = "pos";



                        //var guid = Api.AsyncCalls.getcountryidbycountryname(country).Result;
                        //   PostalData2Context GeoContext = new PostalData2Context();
                        // using (var tempdb = GeoContext)
                        //   {
                        //       GeoService GeoService = new GeoService(tempdb);
                        // countryID = GeoService.getcountryidbycountryname(new GeoModel { country = country });    
                        //  }
                        // countryID = Api.GeoService.getcountryidbycountryname(country);


                        //split up the city from state province
                        //  var tempCityAndStateProvince = city.Split(',');
                        //set all the entity values for profile
                        ObjProfileEntity.username = username;
                        ObjProfileEntity.emailaddress = email;
                        //changed the encryption to something stronger
                        //make username upper so that we can get actual matches without user having to type in a case sensitive username
                        ObjProfileEntity.password = (String.IsNullOrEmpty(openidIdentifer)) ? Encryption.encryptString(password) : null;
                        // ObjProfileEntity.id   = email;
                        ObjProfileEntity.screenname = screenname;
                        //need to add a new feild
                        ObjProfileEntity.activationcode = Nmedia.Infrastructure.Helpers.StringHelpers.RandomReadableString(15);
                        //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                        ObjProfileEntity.creationdate = System.DateTime.Now;
                        ObjProfileEntity.modificationdate = System.DateTime.Now;
                        ObjProfileEntity.logindate = System.DateTime.Now;
                        // fix this to null
                        ObjProfileEntity.forwardmessages = 1;
                        //  ObjProfileEntity.SecurityQuestionID = 1;                
                        // ObjProfileEntity.SecurityAnswer =  securityAnswer;
                        ObjProfileEntity.status_id = (!String.IsNullOrEmpty(openidIdentifer)) ? (int)profilestatusEnum.NotActivated : (int)profilestatusEnum.Activated;
                        //auto activate profiles fi we have an openID user since we have verifed thier info



                        //Build the profile data table
                        // objprofileDataEntity.id  = ;
                        //objprofileDataEntity.profile.emailaddress = email;
                        objprofileDataEntity.latitude = Convert.ToDouble(latitude);
                        objprofileDataEntity.longitude = Convert.ToDouble(longitude); //_GpsData.longitude;

                      
                            // Create a point using native DbGeography Factory method
                        objprofileDataEntity.location = DbGeography.PointFromText(
                                        string.Format("POINT({0} {1})", longitude,latitude)
                                        , 4326);
                       
                       // objprofileDataEntity.location =   DbGeography.FromText("POINT(" + latitude + " "  + longitude   + ")"); 
                        objprofileDataEntity.city = city;
                        objprofileDataEntity.countryregion = "NA";


                        objprofileDataEntity.stateprovince = (!String.IsNullOrEmpty(stateprovince)) ? "" : stateprovince;

                        objprofileDataEntity.countryid = countryid;
                        objprofileDataEntity.postalcode = zippostalcode;
                        objprofileDataEntity.gender_id = Convert.ToInt16(genderid);
                        //objprofileDataEntity.lu_gender = _unitOfWorkAsync.Repository<lu_gender>().Query(p => p.description == gender ).Select().FirstOrDefault();


                        //  =  Int32.Parse(gender): objprofileDataEntity.gender.GenderName  = gender;
                        objprofileDataEntity.birthdate = birthdate;
                        objprofileDataEntity.phone = "NA";
                        objprofileDataEntity.aboutme = "Hello";



                      



                        //get profile and profile datas





                        //set states 
                        objprofileMetaDataEntity.ObjectState = ObjectState.Added;
                        objprofileDataEntity.ObjectState = ObjectState.Added;

                        ObjProfileEntity.profiledata = objprofileDataEntity;
                        ObjProfileEntity.profilemetadata = objprofileMetaDataEntity;

                        ObjProfileEntity.ObjectState = ObjectState.Added;
                        _unitOfWorkAsync.Repository<profile>().InsertOrUpdateGraph(ObjProfileEntity);

                      
                        _unitOfWorkAsync.SaveChanges();


                        if (!String.IsNullOrEmpty(openidIdentifer))
                            createopenid(new ProfileModel { profileid = ObjProfileEntity.id, openididentifier = openidIdentifer, openidprovider = openidProvidername }).DoNotAwait();



                        //_unitOfWorkAsync

                        // transaction.Commit();

                        //add profile activities
                        var activitylist = new List<ActivityModel>();
                        activitylist.Add(Api.AnewLuvLogging.CreateActivity(ObjProfileEntity.id, null, (int)activitytypeEnum.newprofile, OperationContext.Current));
                        Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();

                        // create emails
                        var EmailModels = new List<EmailModel>();
                       
                        if (!String.IsNullOrEmpty(openidIdentifer))
                        {
                           
                            EmailModels.Add(new EmailModel
                            {
                                templateid = (int)templateenum.MemberCreatedJianRainOrOPenIDMemberNotification,
                                messagetypeid = (int)messagetypeenum.UserUpdate,
                                addresstypeid = (int)addresstypeenum.SiteUser,
                                openidprovidername = openidProvidername,
                                emailaddress = email,                                
                                screenname = screenname,
                                username = username

                            });

                            EmailModels.Add(new EmailModel
                            {
                                templateid = (int)templateenum.MemberCreatedJainRanOrOpenIDAdminNotification,
                                messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                addresstypeid = (int)addresstypeenum.SystemAdmin,
                                openidprovidername = openidProvidername,
                                emailaddress = email,
                                screenname = screenname,
                                username = username
                            });
                        }
                        else
                        {
                            EmailModels.Add(new EmailModel
                            {
                                templateid = (int)templateenum.MemberCreatedMemberNotification,
                                messagetypeid = (int)messagetypeenum.UserUpdate,
                                addresstypeid = (int)addresstypeenum.SiteUser,
                                activationcode = ObjProfileEntity.activationcode,
                                emailaddress = email,
                                screenname = screenname,
                                username = username

                            });

                            EmailModels.Add(new EmailModel
                            {
                                templateid = (int)templateenum.MemberCreatedAdminNotification,
                                messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                addresstypeid = (int)addresstypeenum.SystemAdmin,
                                emailaddress = email,
                                screenname = screenname,
                                username = username
                            });


                        }




                        //send the emails
                        //**************************************


                       
                      
                        //this sends both ad-min and user emails  
                        Api.AsyncCalls.sendmessagesbytemplate(EmailModels);
                        //************* end of email send ************************

                        //populate the object to send back so we do not have to re-query from the service side
                        profile profile = _unitOfWorkAsync.Repository<profile>().getprofilebyusername(new ProfileModel { username = username });
                        membershipprovider.profileid = profile.id;
                        membershipprovider.Email = email;

                        status = MembershipCreateStatus.Success;

                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();

                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {

                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        }


                        FaultReason faultreason = new FaultReason("Error in User Authentication service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                        //throw convertedexcption;
                    }
                    finally
                    {
                        //  Api.DisposeGeoService();
                    }
                }
            }

            return membershipprovider;
        }

       
 
     
        public override void UpdateUser(MembershipUser user)
        {



            AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
            //  using (var db = _unitOfWorkAsync)
            {
                //db.IsAuditEnabled = false; //do not audit on adds
                //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        //get profile and profile datas
                        var profilerepo = _unitOfWorkAsync.Repository<profile>();
                        var profiledatarepo = _unitOfWorkAsync.Repository<profiledata>();

                        profile ObjProfileEntity = _unitOfWorkAsync.Repository<profile>().Query(p => p.id == Convert.ToInt16(u.profileid)).Select().FirstOrDefault();
                        profiledata objprofileDateEntity = _unitOfWorkAsync.Repository<profiledata>().Query(p => p.profile_id == Convert.ToInt16(u.profileid)).Select().FirstOrDefault();

                        // new gpsdata;

                        string[] tempCityAndStateProvince = u.city.Split(',');
                        Random objRandom = new Random();
                        int intStart = objRandom.Next(1, 9);
                        int intLastTwo = objRandom.Next(10, 99);
                        //convert the string values to byte as needed
                        //NumberFormatInfo provider = new NumberFormatInfo();
                        // These properties affect the conversion.
                        //provider.PositiveSign = "pos";
                        gpsdata _GpsData = null;
                        int? countryID = null;

                        //conver the unquiqe coountry Name to an ID
                        //store country ID for use later 
                        // PostalData2Context GeoContext = new PostalData2Context();
                        //    using (var tempdb = GeoContext)
                        // {
                        // GeoService GeoService = new GeoService(tempdb);
                        //  countryID = GeoService.getcountryidbycountryname(country);

                        var value = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = u.country }, _storedProcedures);

                        //get the longidtue and latttude 
                        _GpsData = spatialextentions.getgpsdatabycitycountrypostalcode(new GeoModel { country = u.country, city = tempCityAndStateProvince[0], postalcode = u.ziporpostalcode }, _storedProcedures);
                        //  }

                        //  int countryID = Api.GeoService.getcountryidbycountryname(u.country);

                        //get the longidtue and latttude 
                        //   gpsdata _GpsData = Api.GeoService.getgpsdatabycitycountrypostalcode(u.country, tempCityAndStateProvince[0], u.ziporpostalcode);



                        //split up the city from state province
                        //Build the profile data table                   
                        objprofileDateEntity.latitude = Convert.ToDouble(_GpsData.latitude);
                        objprofileDateEntity.longitude = Convert.ToDouble(_GpsData.longitude);
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
                        objprofileDateEntity.gender_id = Int32.Parse(u.genderid);
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
                        profilerepo.Update(ObjProfileEntity);
                        profiledatarepo.Update(objprofileDateEntity);
                        //save all changes bro                         
                        //                        _unitOfWorkAsync
                        var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();


                        var activitylist = new List<ActivityModel>();
                        activitylist.Add(Api.AnewLuvLogging.CreateActivity(ObjProfileEntity.id, null, (int)activitytypeEnum.updateprofile, OperationContext.Current));
                        Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();

                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {

                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        }

                        FaultReason faultreason = new FaultReason("Error in User Authentication service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                        //throw convertedexcption;
                    }
                    finally
                    {
                        //Api.DisposeGeoService();
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
            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
            {
                try
                {
                    u = _unitOfWorkAsync.Repository<profile>().getprofilebyusername(new ProfileModel { username = username });


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
                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                    }

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
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                }

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
                DateTime birthdate, string genderid, string country, string city, string zippostalcode)
        {
        }

        #region "validators needed for creating profiles"


        public async Task<bool> checkifemailalreadyexists(ProfileModel model)
        {

            try
            {
                if (model == null | model.email == null) return false;



                //while (db.ObjectContext.Connection.State  != System.Data.ConnectionState.Closed)
                //{

                // db.DisableProxyCreation = true;;
                //db.DisableLazyLoading = true;
                var result = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.emailaddress == model.email).SelectAsync();

                if (result.FirstOrDefault() != null) return true;
                return false;


            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                }
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                // logger.Dispose();
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                //throw convertedexcption;
            }
        }

                               
        public async Task<bool> checkifopenidalreadyexists(ProfileModel model)
        {
            try
            {
                if (model == null | model.email == null) return false;

                //get profile info an open id info 
                var result = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.emailaddress == model.email).Include(z=>z.openids.Select(y => y.lu_openidprovider)).SelectAsync();

                var profile = result.FirstOrDefault();

                //added a new condition to make sure the provider is the same overkill but just to be safe if items migrate 
                if (profile != null && profile.openids.Count() > 0 && profile.openids.Any( z => z.lu_openidprovider.description.ToUpper() == model.openidprovider.ToUpper() && z.openididentifier == model.openididentifier))
                {
                    return true;

                }
              
                return false;


            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                }
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                // logger.Dispose();
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                //throw convertedexcption;
            }

        }



        public async Task<bool> createopenid(ProfileModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.openididentifier) || !String.IsNullOrEmpty(model.openidprovider))
                {
                    openid newopenid = new openid();

                    newopenid.active = true;
                    newopenid.creationdate = DateTime.Now;
                    var matchedopenid = await _unitOfWorkAsync.RepositoryAsync<lu_openidprovider>().Query(z => z.description.ToUpper() == model.openidprovider.ToUpper()).SelectAsync();
                    if (matchedopenid != null)
                    {
                        newopenid.openidprovider_id = matchedopenid.FirstOrDefault().id;
                    }
                    newopenid.openididentifier = model.openididentifier;
                    newopenid.profile_id = model.profileid.GetValueOrDefault();

                    newopenid.ObjectState = ObjectState.Added;
                    _unitOfWorkAsync.Repository<openid>().InsertOrUpdateGraph(newopenid);


                    _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();

                    return true;


                }


            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                }
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                // logger.Dispose();
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                //throw convertedexcption;
            }

            return false;

        }

        /// <summary>
        /// Determines wethare an activation code matches the value in the Initial Catalog= for a given profileid
        /// </summary>
        public async Task<bool> checkifactivationcodeisvalid(ProfileModel model)
        {

            {

                try
                {
                    if (model == null | model.username == null | model.activationcode == null) return false;
                    var task = Task.Factory.StartNew(() =>
                    {
                        //Dim ctx As New Entities()
                        var dd = _unitOfWorkAsync.Repository<profile>().checkifactivationcodeisvalid(model);

                        return dd;


                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {


                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {

                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }
        //check if profile is activated 
        public async Task<bool> checkifprofileisactivated(ProfileModel model)
        {
            //BAEntities Context

            {
                // db.DisableProxyCreation = true;;
                if (model == null | model.username == null) return false;
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        var dd = _unitOfWorkAsync.Repository<profile>().checkifprofileisactivated(model);
                        return dd;
                    });
                    return await task.ConfigureAwait(false);




                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }


        }


        /// <summary>
        /// TO DO : this method is the only one using Iunitof work and the repo pattern 
        /// its not breaking anymore but need to look into it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> checkifusernamealreadyexists(ProfileModel model)
        {

            Boolean result = false;

            {
                //  db.DisableProxyCreation = false;
                //  db.DisableLazyLoading = false;
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        if (model == null | model.username == null) return false;


                        result = _unitOfWorkAsync.Repository<profile>().checkifusernamealreadyexists(model);
                        //    }

                        //using (var db = new AnewluvContext())
                        // {
                        //  // db.DisableProxyCreation = true;;
                        //    db.DisableLazyLoading = true;
                        //    // IQueryable<profile> myQuery = default(IQueryable<profile>);
                        //   result =   _unitOfWorkAsync.Repository<profile>().Query.checkifusernamealreadyexists(model);                        
                        //}
                        return result;
                    });
                    return await task.ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                    }
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
            }

        }

        public async Task<bool> checkifscreennamealreadyexists(ProfileModel model)
        {


            try
            {

                var result = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.screenname == model.screenname).SelectAsync();
                if (result.FirstOrDefault() != null) return true;
                return false;


            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                }
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }




        }

        #endregion


        #region "Extra  methods custom to anewluv"

        //1-8-2013 olawal addedrobust method for activating profiles
        public async Task<AnewluvMessages> activateprofile(activateprofilemodel model)
        {
            AnewluvMessages messages = new AnewluvMessages();
            // messages.message = "";
            // //messages.errormessages = null;
            profile profile = new profile();
           // AnewluvResponse response = new AnewluvResponse();


            {

                {

                    try
                    {

                        ResponseMessage reponsemessage = new ResponseMessage();


                        var task = Task.Factory.StartNew(() =>
                        {

                            //Clear any errors kinda redundant tho  
                            //also create a members view model to store pertinent data i.e persist photos profile ID etc
                            var membersmodel = new MembersViewModel();
                            //get the macthcing member data using the profile ID/email entered
                            if (!string.IsNullOrEmpty(model.emailaddress))
                            {
                                profile = _unitOfWorkAsync.Repository<profile>().getprofilebyemailaddress(new ProfileModel { email = model.emailaddress });
                            }
                            else if (!string.IsNullOrEmpty(model.username))
                            {
                                profile = _unitOfWorkAsync.Repository<profile>().getprofilebyusername(new ProfileModel { username = model.username });

                            }
                            else
                            {
                                profile = null;

                            };


                            //  membersmodel =  _m .GetMemberData( model.activateprofilemodel.profileid);

                            //verify that user entered correct email before doing anything
                            //TO DO add these error messages to resource files
                            if (profile == null)
                            {
                                // messages.errormessages.Add("There is no registered account with the email address: " + model.emailaddress + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                                messages.errormessages.Add("invalid useraccount or Email");
                                //hide the photo view in thsi case
                                // model.photostatus = true;
                                // return messages;
                            }
                            else if (_unitOfWorkAsync.Repository<profile>().checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true)
                            {
                                messages.errormessages.Add("Your Profile has already been activated");
                                //hide the photo view in thsi case
                                //ViewData["ActivateProfileStatus"]=
                                // return View("LogOn", _logonmodel);
                                //return messages;
                            }
                            else
                            {

                                var UploadedPhoto = _unitOfWorkAsync.Repository<photo>().Queryable().Where(p => p.profile_id == profile.id).FirstOrDefault();
                                if (UploadedPhoto == null) messages.errormessages.Add("Please upload at least one profile photo");

                                //var returnedTaskTResult = checkforuploadedphotobyprofileidasync(profile.id).Result;
                                // bool result =
                                // model.photostatus = returnedTaskTResult;
                                //}

                                //5/3/2011 instantiace the photo upload model as well since its the next step if we were succesful    
                                // photoeditmodel photoviewmodel = new photoeditmodel();
                                //registermodel registerviewmodel = new registermodel();
                                // model.emailaddress = profile.emailaddress;
                                // model.activationcode = profile.activationcode; //model.activateprofilemodel.ActivationCode;

                                //5/11/2011
                                //TO DO USE TASK for this
                                // add photo view model stuff
                                //Need to me made to run asynch
                                //if (model.PhotosUploadModel.photosuploaded.Count() > 0)
                                //{

                                //    var returnedTaskTResult = AsyncCalls.addphotosasync(model.PhotosUploadModel);

                                //    // Api.PhotoService.addphotos(model.PhotosUploadModel);
                                //}

                                //since we got here we can now check if the user has a photo
                                //first check to see if there is an email address for the given user on the server add it to the data anotaions validation                 
                                //get a value for photo status so we know weather to display uplodad phot dialog or not
                                //if the photo status is TRUE then hide the upload photo div

                                //we area still allowing activation with no photo but we will force user to re-direct to edit profile/photos


                                //activaate profile here as long as photo exists 

                                //TO DO convert to Asynch call
                                // AnewluvContext = new AnewluvContext();
                                // using (var tempdb = AnewluvContext)
                                // {
                                //       MemberService MemberService = new MemberService(tempdb);
                                // var activateProfileResult= activateprofileasync(new ProfileModel { profileid = profile.id }).Result;
                                // activationsuccesful = activateProfileResult;
                                //  }

                                profile.status_id = (int)profilestatusEnum.Activated;
                                profile.ObjectState = ObjectState.Modified;
                                //handele the update using EF
                                //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                                _unitOfWorkAsync.Repository<profile>().Update(profile);
                                var i = _unitOfWorkAsync.SaveChanges();


                                //check if mailbox folders exist, if they dont create em , don't add any error status

                                // var areamailboxfolderscreated = false;
                                //AnewluvContext = new AnewluvContext();
                                //  using (var tempdb = AnewluvContext)
                                //   {
                                //     MemberService MemberService = new MemberService(tempdb);
                                //  areamailboxfolderscreated =  Api.AsyncCalls.checkifmailboxfoldersarecreatedasync(new ProfileModel { profileid = profile.id }).Result;
                                // }

                                //if (!(areamailboxfolderscreated))                                    
                                // {
                                //    AnewluvContext = new AnewluvContext();
                                //  using (var tempdb = AnewluvContext)
                                //   {
                                //    MemberService MemberService = new MemberService(tempdb);
                                Api.AsyncCalls.createmailboxfoldersasync(new ProfileModel { profileid = profile.id });
                                //  }
                                // MemberService.createmailboxfolders(new ProfileModel { profileid = profile.id });
                                //  }

                                messages.messages.Add("Activation Sucssesful");
                            }




                            //  if (messages.errormessages.Count() > 0 )
                            // {
                            //get the profile info to return
                            //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                            //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                            //  response.email = model.emailaddress;//profile.emailaddress;
                            ////  ResponseMessage currentmessages = new ResponseMessage("", messages.messages.FirstOrDefault(), "");
                            //  response.ResponseMessages.Add(currentmessages);

                            //  }
                            //  else
                            //  {

                            //  messages.errormessages.Add("There was a problem activating the profile, please try again later");
                            //   response.ResponseMessages.Add(reponsemessage);
                            //  }

                            return messages;

                            // return messages;
                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                        // // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        }
                        FaultReason faultreason = new FaultReason("Error in User Authentication service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                        //throw convertedexcption;
                    }
                    finally
                    {
                        //  Api.DisposePhotoService();
                        // Api.DisposeMemberService();
                    }





                }
            }





        }

        public async Task<AnewluvMessages> recoveractivationcode(activateprofilemodel model)
        {

            profile profile = new profile();
          //  AnewluvResponse response = new AnewluvResponse();


            //  using (var db = _unitOfWorkAsync)
            {
                //  ////do not audit on adds
                //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            AnewluvMessages messages = new AnewluvMessages();
                            // messages.messages = "";
                           //  //messages.errormessages = null;
                            var isprofileactivated = false;

                            //Clear any errors kinda redundant tho  
                            //also create a members view model to store pertinent data i.e persist photos profile ID etc
                            var membersmodel = new MembersViewModel();
                            //get the macthcing member data using the profile ID/email entered
                            profile = _unitOfWorkAsync.Repository<profile>().getprofilebyemailaddress(new ProfileModel { email = model.emailaddress });
                            //  membersmodel =  _m .GetMemberData( model.activateprofilemodel.profileid);

                            //verify that user entered correct email before doing anything
                            //TO DO add these error messages to resource files
                            if (profile == null)
                            {
                                // messages.errormessages.Add("There is no registered account with the email address: " + model.emailaddress + " on AnewLuv.com, please either register for a new account or use the contact us link to get help");
                                messages.errormessages.Add("invalid useraccount or Email");
                                //hide the photo view in thsi case

                            }
                            else
                            {

                                isprofileactivated = (_unitOfWorkAsync.Repository<profile>().checkifprofileisactivated(new ProfileModel { profileid = profile.id }) == true);


                                if (isprofileactivated == true)
                                {
                                    messages.errormessages.Add("Your Profile has already been activated");
                                    //hide the photo view in thsi case
                                    //ViewData["ActivateProfileStatus"]=
                                    // return View("LogOn", _logonmodel);
                                }
                                else
                                {

                                    var EmailModels = new List<EmailModel>();

                                    //memeber notification
                                    EmailModels.Add(new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberActivationCodeRecoveredMemberNotification,
                                        messagetypeid = (int)messagetypeenum.UserUpdate,
                                        addresstypeid = (int)addresstypeenum.SiteUser,
                                        emailaddress = profile.emailaddress,
                                        screenname = profile.screenname,
                                        username = profile.username,
                                        activationcode = profile.activationcode

                                    });

                                    //admin notificaiton
                                    EmailModels.Add(new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberActivationCodeRecoveredAdminNotification,
                                        messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                        addresstypeid = (int)addresstypeenum.SystemAdmin,
                                        emailaddress = profile.emailaddress,
                                        screenname = profile.screenname,
                                        username = profile.username,
                                    });
                                    //this sends both admin and user emails  
                                    Api.AsyncCalls.sendmessagesbytemplate(EmailModels);

                                    //get the profile info to return
                                    //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                                    //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                                    //oke send back theer activvation code
                                    messages.messages.Add("Your activiation code has been sent to the email address: " + model.emailaddress);

                                    //Code to send message here 




                                }


                            }



                            return messages;

                        });
                        return await task.ConfigureAwait(false);
                        //return messages;
                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        }
                        FaultReason faultreason = new FaultReason("Error in User Authentication service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }
                    finally
                    {
                        //  Api.DisposeMemberService();
                    }
                }
            }





        }

        public async Task<NmediaToken> validateuserandgettoken(ProfileModel model)
        {

            var profile = new profile();
            var currenttoken = new NmediaToken();

            
         
            //hard exit if no username or open id identifier
            if (model == null || model.username == null && model.openididentifier == null ) return currenttoken;
            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
            {
                try
                {
                                      

                  
                    //If we have no open id identifier use username and password verification
                    if (model.openididentifier == "" | model.openididentifier == null)
                    {

                        // return _unitOfWorkAsync.Repository<profiledata>().getprofiledatabyprofileid(model);
                        //first you have to get the encrypted string by email address and username 
                        string encryptedPassword = "";
                        //get profile created date
                        DateTime creationdate;
                        DateTime? passwordchangedate;
                        DateTime EncrpytionChangeDate = new DateTime(2011, 8, 3, 4, 5, 0);
                        string decryptedPassword;
                        string actualpasswordstring;




                        //we need profile id regardless
                        //TO DO add an inactive count login to track how many times a user logs in before they active profile default max should be = 3
                        //added profile status ID validation as well i.e 2 for activated and is not banned 
                        var profileresult = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.username == model.username &&
                            (p.status_id != (int)profilestatusEnum.Banned | p.status_id != (int)profilestatusEnum.Inactive | p.status_id != (int)profilestatusEnum.ResetingPassword)
                             ).Include(z => z.profileactivities).SelectAsync();

                        profile = profileresult.FirstOrDefault();




                        if (profile != null && profile.id != 0)
                        {

                            //retirve encrypted password
                            encryptedPassword = profile.password;
                            creationdate = profile.creationdate.GetValueOrDefault();
                            passwordchangedate = profile.passwordChangeddate;
                        }
                        else
                        {                           
                            return currenttoken;
                        }

                        //case for if the user account was created before encryption algorithm was changed
                        //compare the dates
                        int resultcreateddate = DateTime.Compare(creationdate, EncrpytionChangeDate);
                        int resultpasswordchangedate = DateTime.Compare(passwordchangedate.GetValueOrDefault(), EncrpytionChangeDate);
                        if (resultpasswordchangedate > 0 | resultcreateddate > 0)
                        //use new decryption method
                        {

                            decryptedPassword = Encryption.decryptString(encryptedPassword);
                            actualpasswordstring = model.password;
                        }
                        else
                        {
                            decryptedPassword = Encryption.Decrypt(encryptedPassword, model.password);
                            actualpasswordstring = model.username.ToUpper() + Encryption.EncryptionKey;
                        }


                        //TO DO change this to use activity not log time since its a better measure for the data we need 
                        //FIX the log time code
                        //check if decrypted string matched username to upper  + secret
                        if (actualpasswordstring == decryptedPassword)
                        {

                            //No need to log this since its used the APIkey inspector on checkascccesscore
                            currenttoken.id = profile.id;
                            currenttoken.timestamp = DateTime.Now;
                            //return the profile ID so it can be used for whatver

                            //for now have it generate a new GUID each time to test 
                            // var existingguid = getcurrentapikeybyprofileid(myQuery.id, db);

                            var guid = Api.AsyncCalls.validateorgetapikeyasync(new ApiKeyValidationModel
                            {
                                service = "AuthenticationService",
                                username = model.username,
                                useridentifier = currenttoken.id,
                                application = "Anewluv",
                                application_id = (int)applicationenum.anewluv,
                                keyvalue = null
                            }).Result;
                            // var guid = getcurrentapikeybyprofileid(myQuery.id,db);
                            if (guid != null)
                                currenttoken.Apikey = guid;

                            //updated activity // TO Do we might use to replace log-times below ?
                            var activitylist = new List<ActivityModel>();
                            activitylist.Add(Api.AnewLuvLogging.CreateActivity(profile.id, guid, (int)activitytypeEnum.login, OperationContext.Current));
                            Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();


                            //login time updated here
                            updateuserlogintime(profile.id, OperationContext.Current, guid.ToString()).DoNotAwait();
                            return currenttoken;
                            //get the token here

                        }
                        else
                        {
                            return currenttoken;
                        }
                    }
                    //handling for open ID logins 
                    else
                    {
                        bool validuser = false;

                        //TO DO add an inactive count login to track how many times a user logs in before they active profile default max should be = 3
                        //added profile status ID validation as well i.e 2 for activated and is not banned 

                        // return _unitOfWorkAsync.Repository<profiledata>().getprofiledatabyprofileid(model);
                        //first you have to get the encrypted string by email address and username 

                        DateTime creationdate =DateTime.Now;
                       
                     
                        //so we want to use the open ID identifier to get the profile                        

                        //get profile info an open id info 
                        var profileresult = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.emailaddress== model.email |  p.openids.Any(z=>z.openididentifier == model.openididentifier)   &&
                            (p.status_id != (int)profilestatusEnum.Banned | p.status_id != (int)profilestatusEnum.Inactive | p.status_id != (int)profilestatusEnum.ResetingPassword)
                        ).Include(z => z.openids.Select(y => y.lu_openidprovider)).SelectAsync();



                         profile = profileresult.FirstOrDefault();

                        //added a new condition to make sure the provider is the same overkill but just to be safe if items migrate 
                        if (profile != null && profile.openids.Count() > 0 && profile.openids.Any(z => z.lu_openidprovider.description.ToUpper() == model.openidprovider.ToUpper() && z.openididentifier == model.openididentifier))
                        {

                            validuser = true;
                           
                        }
                        //only add a new provider if email addresses match
                        else if (profile != null &&  model.email == profile.emailaddress && profile.openids.Count() > 0 && !profile.openids.Any(z => z.lu_openidprovider.description.ToUpper() == model.openidprovider.ToUpper() && z.openididentifier == model.openididentifier))
                        {

                            //TO do figure out why this wont work 
                            //new open id idenmtifier
                            //Anewluv.Api.AsyncCalls.addnewopenidforprofile(new ProfileModel { profileid = profile.id, openididentifier = model.openididentifier,openidprovider = model.openidprovider, email = model.email }).DoNotAwait();
                            //TO DO do we let users log on if this fails ?

                            var dd= this.addopenid(new ProfileModel { profileid = profile.id, openididentifier = model.openididentifier, openidprovider = model.openidprovider, email = model.email });
                            validuser = true;
                            //TO DO add activity and email message for this 

                        }
                        else
                        {
                            validuser = false;
                        }

                        //if user is valid allow the login
                        if (validuser)
                        {

                            //No need to log this since its used the APIkey inspector on checkascccesscore
                            currenttoken.id = profile.id;
                            currenttoken.timestamp = DateTime.Now;
                            //return the profile ID so it can be used for whatver

                            //for now have it generate a new GUID each time to test 
                            // var existingguid = getcurrentapikeybyprofileid(myQuery.id, db);

                            var guid = Api.AsyncCalls.validateorgetapikeyasync(new ApiKeyValidationModel
                            {
                                service = "AuthenticationService",
                                username = model.username,
                                useridentifier = currenttoken.id,
                                application = "Anewluv",
                                application_id = (int)applicationenum.anewluv,
                                keyvalue = null
                            }).Result;
                            // var guid = getcurrentapikeybyprofileid(myQuery.id,db);
                            if (guid != null)
                                currenttoken.Apikey = guid;

                            //updated activity // TO Do we migght use to replace logtimes below ?
                            var activitylist = new List<ActivityModel>();
                            activitylist.Add(Api.AnewLuvLogging.CreateActivity(profile.id, guid, (int)activitytypeEnum.login, OperationContext.Current));
                            Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();


                            //login time updated here
                            updateuserlogintime(profile.id, OperationContext.Current, guid.ToString()).DoNotAwait();
                          
                        }

                        return currenttoken;
                    }



                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //instantiate logger here so it does not break anything else.

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, profile != null ? profile.id : 0, null);
                    }

                    FaultReason faultreason = new FaultReason("Error in authentication service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                //finally
                //{
                //    Api.DisposeMemberService();
                //}

            }
        }

        public async Task<Boolean> logoutuserandinvalidatetoken(ProfileModel profile)
        {

            var myQuery = new profile();
            var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
            if (profile == null | profile.username == null) return false; ;
            // _unitOfWorkAsync.DisableProxyCreation = true;
            //  using (var db = _unitOfWorkAsync)
            {
                try
                {


                    var task = Task.Factory.StartNew(() =>
                    {



                        //Dim ctx As New Entities()
                        //TO DO add an inactive count login to track how many times a user logs in before they active profile default max should be = 3
                        //added profile status ID validation as well i.e 2 for activated and is not banned 
                        myQuery = _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.id == profile.profileid.GetValueOrDefault()).FirstOrDefault();


                        if (myQuery != null)
                        {
                            Api.AsyncCalls.invalidateuserapikey(new ApiKeyValidationModel
                            {
                                service = "AuthenticationService",
                                username = profile.username,
                                useridentifier = myQuery.id,
                                application = "Anewluv",
                                application_id = (int)applicationenum.anewluv,
                                keyvalue = null
                            }).DoNotAwait();


                            //updated logout time
                            updateuserloggout(myQuery.id, ctx, profile.apikey);

                            activitylist.Add(Api.AnewLuvLogging.CreateActivity(profile.profileid, new Guid(profile.apikey), (int)activitytypeEnum.changebirthdate, ctx));
                            Anewluv.Api.AsyncCalls.addprofileactivities(activitylist).DoNotAwait();


                            return true;

                        }
                        return false;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //instantiate logger here so it does not break anything else.

                    using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, myQuery != null ? myQuery.id : 0, null);
                    }

                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                //finally
                //{
                //    Api.DisposeMemberService();
                //}

            }
        }


        /// <summary>
        /// used to update all proile stuff that is changable , including password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task updateuser(MembershipUserViewModel user)
        {



            //  AnewLuvMembershipUser u = (AnewLuvMembershipUser)user;
            //  using (var db = _unitOfWorkAsync)
            {
                //db.IsAuditEnabled = false; //do not audit on adds
                //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        bool profileupdated = false;
                        bool profiledataupdated = false;
                        //get profile and profile datas
                        var profilerepo = _unitOfWorkAsync.Repository<profile>();
                        var profiledatarepo = _unitOfWorkAsync.Repository<profiledata>();

                        var ObjProfileEntityresult = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.id == Convert.ToInt16(user.profileid)).SelectAsync();
                        profile ObjProfileEntity = ObjProfileEntityresult.FirstOrDefault();
                        var objprofileDateEntityresult = await _unitOfWorkAsync.RepositoryAsync<profiledata>().Query(p => p.profile_id == Convert.ToInt16(user.profileid)).SelectAsync();
                        profiledata objprofileDateEntity = objprofileDateEntityresult.FirstOrDefault();

                        // new gpsdata;

                        //string[] tempCityAndStateProvince = user.city.Split(',');
                        Random objRandom = new Random();
                        int intStart = objRandom.Next(1, 9);
                        int intLastTwo = objRandom.Next(10, 99);
                        //convert the string values to byte as needed
                        //NumberFormatInfo provider = new NumberFormatInfo();
                        // These properties affect the conversion.
                        //provider.PositiveSign = "pos";
                        gpsdata _GpsData = null;
                        //int? countryID = null;
                        string matchedcontryname = "";
                        int? matchedcountryid = null;

                        //conver the unquiqe coountry Name to an ID
                        //store country ID for use later 
                        // PostalData2Context GeoContext = new PostalData2Context();
                        //    using (var tempdb = GeoContext)
                        // {
                        // GeoService GeoService = new GeoService(tempdb);
                        //  countryID = GeoService.getcountryidbycountryname(country);


                        //verify the country 
                        if (user.country == "" && user.countryid != null)
                        {
                            matchedcountryid = spatialextentions.getcountrycountryidbycountryname(new GeoModel { country = user.country }, _storedProcedures).Result;
                            matchedcontryname = user.country;
                        }
                        else if (user.country != "")
                        {
                            matchedcontryname = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = user.countryid.ToString() }, _storedProcedures);
                            matchedcountryid = user.countryid;
                        }



                        //to do validate city as well

                        //verify all the spatial data
                        if (matchedcontryname != "")
                            _GpsData = spatialextentions.getgpsdatabycitycountrypostalcode(new GeoModel { country = user.country, city = user.city, postalcode = user.zippostalcode }, _storedProcedures);
                        //  }

                        //  int countryID = Api.GeoService.getcountryidbycountryname(user.country);

                        //get the longidtue and latttude 
                        //   gpsdata _GpsData = Api.GeoService.getgpsdatabycitycountrypostalcode(user.country, tempCityAndStateProvince[0], user.ziporpostalcode);



                        //split up the city from state province
                        //Build the profile data table                   
                        if (_GpsData != null) { objprofileDateEntity.latitude = Convert.ToDouble(_GpsData.latitude); profiledataupdated = true; }
                        if (_GpsData != null) { objprofileDateEntity.longitude = Convert.ToDouble(_GpsData.longitude); profiledataupdated = true; }
                        if (_GpsData != null && user.city != "") { objprofileDateEntity.city = user.city; profiledataupdated = true; }
                        if (_GpsData != null && user.stateprovince != "") { objprofileDateEntity.stateprovince = user.stateprovince; profiledataupdated = true; }
                        //if (_GpsData != null && (user.region != "" && user.stateprovince =="")) objprofileDateEntity.stateprovince = user.region;



                        if (_GpsData != null && user.country != "") { objprofileDateEntity.countryid = matchedcountryid; profiledataupdated = true; }
                        if (_GpsData != null && user.zippostalcode != "") { objprofileDateEntity.postalcode = user.zippostalcode; profiledataupdated = true; }
                        if (user.genderid != "") { objprofileDateEntity.gender_id = Convert.ToInt16(user.genderid); profiledataupdated = true; }
                        if (user.birthdate != null) { objprofileDateEntity.birthdate = Convert.ToDateTime(user.birthdate); profiledataupdated = true; }
                        if (user.phonenumber != "") { objprofileDateEntity.phone = user.phonenumber; profiledataupdated = true; }
                        //objprofileDateEntity.AboutMe = "Hello";



                        //set all the entity values
                        // ObjProfileEntity.username = username;
                        //changed the encryption to something stronger

                        //only update password if it changed
                        if (user.verificationcode == user.verificationcode && user.password != null | user.password != "")
                        {
                            this.updatepasswordbyprofileid(ObjProfileEntity, Encryption.encryptString(user.password));
                            profileupdated = true;
                        }
                        // ObjProfileEntity.ProfileID = email;
                        // ObjProfileEntity.ScreenName = screenname;
                        //need to add a new feild
                        // ObjProfileEntity.ActivationCode = Common.Encryption.EncodeString(email + screenname);
                        //Mid(intStart, intStart, 14) & CStr(intLastTwo) 'need to beef this up with the session variable
                        //ObjProfileEntity.creationdate = System.DateTime.Now;

                        if (profiledataupdated) profiledatarepo.Update(objprofileDateEntity);


                        if (profileupdated)
                        {


                            profilerepo.Update(ObjProfileEntity);
                            ObjProfileEntity.modificationdate = System.DateTime.Now;
                            //save all changes bro                         
                            //                        _unitOfWorkAsync

                        }
                        // ObjProfileEntity.LoginDate = System.DateTime.Now;
                        // fix this to null
                        //ObjProfileEntity.ForwardMessages = 1;
                        //  ObjProfileEntity.SecurityQuestionID = System.Convert.ToByte(user.securityquestion );
                        //   ObjProfileEntity.SecurityAnswer = user.securityanswer;
                        //ObjProfileEntity.ProfileStatusID = 1;


                        if (profiledataupdated | profileupdated) _unitOfWorkAsync.SaveChangesAsync().DoNotAwait();
                        //dbContext.AddToprofiledatas(objprofileDateEntity);
                        // dbContext.AddToprofiles(ObjProfileEntity);

                        // transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {

                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        }

                        FaultReason faultreason = new FaultReason("Error in User Authentication service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);


                        //throw convertedexcption;
                    }
                    finally
                    {
                        //Api.DisposeGeoService();
                    }
                }
            }
        }

        /// <summary>
        /// puts user account in password reset mode and sets a GUID to the user's email that is needed to allow reset of password
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public async Task<string> resetpassword(MembershipUserViewModel user)
        {


            try
            {
                var task = Task.Factory.StartNew(() =>
                {
                     var dd = this.ResetPassword(user.email, user.securityAnswer);

                     return dd;
                  

                });
                return await task.ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {

                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                }

                //log error mesasge
                //handle logging here
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
            finally
            {
                // Api.DisposeMemberService();
            }


        }


        public async Task<string> changepassword(MembershipUserViewModel user)
        {
            
            try
            {

                var task = Task.Factory.StartNew(() =>
                {


                    var result = this.ChangePassword(user.username, user.passwordtoken, user.password);

                    if (result)
                    {
                        return "Password changed sucessfully";
                    }
                    else
                    {
                        return "Unable to change password at this time: either a bad token or your 30 minute timer has expired, please request a new password link if that does not work contact support";
                    }


                   
                
                });
                 return await task.ConfigureAwait(false);

            }
            
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {

                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                }

                //log error mesasge
                //handle logging here
                FaultReason faultreason = new FaultReason("Error in member service, unable to change password");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
            finally
            {
                // Api.DisposeMemberService();
            }
        }


        #endregion

        #region "private methods for reuse or other async calls"

        private Guid? getcurrentapikeybyprofileid(int profileid, IUnitOfWorkAsync db)
        {

            //////do not audit on adds               
            try
            {
                //get the last current activity 
                //dont worry about user logtime here and session ID's since user is re-validating we are starting over with this user anyways                        
                var myQuery = _unitOfWorkAsync.Repository<profileactivity>().Queryable().Where(p => p.profile_id == profileid && p.apikey != null).OrderByDescending(p => p.creationdate).FirstOrDefault();

                if (myQuery != null && myQuery.apikey != null)
                {
                    return myQuery.apikey;
                }
                return null;




            }
            catch (Exception ex)
            {

                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, profileid, null);
                }

                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }
        }

        private async Task updateuserlogintime(int profileid, OperationContext ctx, string newtoken)
        {
            //  MemberService MemberService = new MemberService(db);
            try
            {
                // OperationContext ctx = OperationContext.Current;


                if (newtoken != null)
                {
                    //log the user logtime here so it is common to silverlight and MVC
                    if (ctx.SessionId != null)
                    {
                        //Just for testing that it worked
                        //TO DO remove when in prod                           
                       await Api.AsyncCalls.updateuserlogintimebyprofileidandsessionidasync(new ProfileModel { profileid = profileid, sessionid = ctx.SessionId, apikey = newtoken });
                        //  MemberService.
                    }
                    else
                    {
                        //  MemberService.updateuserlogintimebyprofileid(new ProfileModel { profileid = profileid });
                        await Api.AsyncCalls.updateuserlogintimeasync(new ProfileModel { profileid = profileid, apikey = newtoken });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void updateuserloggout(int profileid, OperationContext ctx, string newtoken)
        {
            //  MemberService MemberService = new MemberService(db);
            try
            {
                // OperationContext ctx = OperationContext.Current;
                // var apikey = WCFContextParser.GetAPIKey(ctx);
                if (newtoken != null)
                {
                    //  MemberService.updateuserlogintimebyprofileid(new ProfileModel { profileid = profileid });
                    Api.AsyncCalls.updateuserlogouttimebyprofileidasync(new ProfileModel { profileid = profileid, apikey = newtoken }).DoNotAwait();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //validate of api key only not profile id if its not passed
        private async Task<Guid> validateorgetapikeyasync(ApiKeyValidationModel model)
        {

            Task<Guid> returnedTaskTResult = Api.AsyncCalls.validateorgetapikeyasync(model);
            Guid result = await returnedTaskTResult;

            return result;


        }

        private async Task<bool> checkifmailboxfoldersarecreatedasync(ProfileModel model)
        {
            Task<bool> returnedTaskTResult = Api.AsyncCalls.checkifmailboxfoldersarecreatedasync(model);
            bool result = await returnedTaskTResult;
            return result;
        }

        private async Task<bool> activateprofileasync(ProfileModel model)
        {
            Task<bool> returnedTaskTResult = Api.AsyncCalls.activateprofileasync(model);
            bool result = await returnedTaskTResult;
            return result;
        }

        private async Task<bool> checkforuploadedphotobyprofileidasync(int profileid)
        {
            Task<bool> returnedTaskTResult = Api.AsyncCalls.checkforuploadedphotobyprofileidasync(profileid);
            bool result = await returnedTaskTResult;
            return result;
        }

        private profile updatepasswordbyprofileid(profile profile, string encryptedpassword)
        {

             try
                    {
                        // var profilerepo = _unitOfWorkAsync.Repository<profile>();
                        //var myProfile = profilerepo.getprofilebyprofileid(model);
                        //update the profile status to 2
                        profile.password = encryptedpassword;
                        profile.passwordChangeddate = DateTime.Now;
                        profile.passwordchangecount = (profile.passwordchangecount == null) ? 1 : profile.passwordchangecount + 1;
                        //handele the update using EF
                        //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        //profilerepo.Update(myProfile);
                        //  var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();

                        return profile;
                    }
                    catch (Exception ex)
                    {

                        throw ex;

                        //throw convertedexcption;
                    }
                
            

        }

        private bool addopenid(ProfileModel model)
        {

            try
            {

                var query1 = _unitOfWorkAsync.Repository<lu_openidprovider>().Query(p => (p.description).ToUpper() == model.openidprovider.ToUpper()).Select();

                var openidprovider = query1.FirstOrDefault();

                if (openidprovider != null)
                {
                    var myQuery = _unitOfWorkAsync.Repository<profile>().Query(p => p.id == model.profileid.Value && p.openids.Any(f => f.openididentifier != model.openididentifier && f.lu_openidprovider.description.ToUpper() != model.openidprovider.ToUpper())).Select();

                    if (myQuery != null)
                    {
                        var profileOpenIDStore = new openid
                        {
                            active = true,
                            creationdate = DateTime.UtcNow,
                            profile_id = model.profileid.Value,
                            openidprovider_id = openidprovider.id,
                            openididentifier = model.openididentifier,
                            ObjectState = ObjectState.Added
                        };
                        _unitOfWorkAsync.Repository<openid>().Insert(profileOpenIDStore);
                        var i = _unitOfWorkAsync.SaveChangesAsync();
                        // transaction.Commit();

                        return true;

                    }
                }

               
            }
            catch (Exception ex)
            {
                var dd = "logthis";
            }

            return false;

        }

        //updates the profile with a password that is presumed to be already encyrpted
        private bool enablepasswordreset(ProfileModel model, ShortGuid shortguid)
        {

            //// 
            {
                // ////do not audit on adds
                //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        var profilerepo = _unitOfWorkAsync.Repository<profile>();
                        var myProfile = profilerepo.getprofilebyprofileid(model);
                        //update the profile status to 2
                        myProfile.status_id = (int)profilestatusEnum.ResetingPassword;
                        myProfile.passwordresettoken = shortguid;
                        myProfile.passwordresetwindow = DateTime.Now.AddMinutes(30);
                        myProfile.passwordchangeattempts = myProfile.passwordchangeattempts + 1;
                        myProfile.modificationdate = DateTime.Now;
                        // myProfile.passwordChangeddate = DateTime.Now;
                        // myProfile.passwordchangecount = (myProfile.passwordchangecount == null) ? 1 : myProfile.passwordchangecount + 1;
                        //handele the update using EF
                        //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        profilerepo.Update(myProfile);
                        var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {

                        throw ex;

                        //throw convertedexcption;
                    }
                }
            }

        }




        #endregion



        #region "overrides implemented"

        public override string ResetPassword(string emailaddress, string answer)
        {

            try
            {

                bool accountreset = false;
                // var username = datingService.ValidateSecurityAnswerIsCorrect(profileid, securityquestionID.GetValueOrDefault(), answer);
                var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyemailaddress(new ProfileModel { email = emailaddress });
                if (profile != null)
                {
                    //we have the generated password now update the user's account with new password

                    //generatedpassword = GeneratePassword();
                    //AnewluvContext AnewluvContext  = new AnewluvContext();
                    Guid guid = Guid.NewGuid();
                    ShortGuid sguid1 = guid; // implicitly cast the guid as a shortguid


                    if (profile.status_id == (int)profilestatusEnum.ResetingPassword)
                    {
                        //if the reset window is expired re-set it again
                        if (profile.passwordresetwindow != null && DateTime.Now < profile.passwordresetwindow.GetValueOrDefault().AddMinutes(30))
                        {

                            System.TimeSpan diff1 = profile.passwordresetwindow.GetValueOrDefault().Subtract(DateTime.Now);

                            accountreset = false;
                            return "has allready been reset you still have " + diff1.TotalMinutes.ToString() + " before you can request a new change password link";
                        }

                    }
                        
                    //if we get here we want to reset the password
                    accountreset = enablepasswordreset(new ProfileModel { profileid = profile.id }, sguid1);
                                      

                    if (accountreset)
                    {
                        var EmailModels = new List<EmailModel>();

                        EmailModels.Add(new EmailModel
                        {
                            templateid = (int)templateenum.MemberPasswordResetMemberNotification,
                            messagetypeid = (int)messagetypeenum.UserUpdate,
                            addresstypeid = (int)addresstypeenum.SiteUser,
                            emailaddress = profile.emailaddress,
                            screenname = profile.screenname,
                            username = profile.username,
                            passwordtoken = sguid1
                        });
                        EmailModels.Add(new EmailModel
                        {
                            templateid = (int)templateenum.MemberPasswordResetAdminNotification,
                            messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                            addresstypeid = (int)addresstypeenum.SystemAdmin,                            
                            emailaddress = profile.emailaddress,
                            screenname = profile.screenname,
                            username = profile.username,
                        });
                        //this sends both admin and user emails  
                        Api.AsyncCalls.sendmessagesbytemplate(EmailModels);


                        return "Account Reset";
                    }
                    // throw new NotImplementedException();

                    return "Unable to Reset your Account please contact support";
                }

                return "Unable to Reset your Account please contact support";

            }
            catch (Exception ex)
            {
                using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                {

                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                }


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


        public override bool ChangePassword(string username, string passwordtoken, string newPassword)
        {
            //make username upper so that we can get actual mateches withoute user having to type in a case sensitive username
            bool success = false;
            try
            {

                // var username = datingService.ValidateSecurityAnswerIsCorrect(profileid, securityquestionID.GetValueOrDefault(), answer);
                var profile = _unitOfWorkAsync.RepositoryAsync<profile>().getprofilebyusername(new ProfileModel { username = username });

                if (profile != null && profile.passwordresettoken == passwordtoken && DateTime.Now < profile.passwordresetwindow.GetValueOrDefault())
                {
                    //

                    profile.password = Encryption.encryptString(newPassword);
                    _unitOfWorkAsync.Repository<profile>().Update(profile);

                    _unitOfWorkAsync.SaveChanges();

                    success = true;

                    if (profile !=null)
                    {
                        var EmailModels = new List<EmailModel>();

                        EmailModels.Add(new EmailModel
                        {
                            templateid = (int)templateenum.MemberPasswordChangeMemberNotification,
                            messagetypeid = (int)messagetypeenum.UserUpdate,
                            addresstypeid = (int)addresstypeenum.SiteUser,
                            emailaddress = profile.emailaddress,
                            screenname = profile.screenname,
                            username = profile.username

                        });
                        EmailModels.Add(new EmailModel
                        {
                            templateid = (int)templateenum.MemberPasswordChangedAdminNotification,
                            messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                            addresstypeid = (int)addresstypeenum.SystemAdmin,
                            emailaddress = profile.emailaddress,
                            screenname = profile.screenname,
                            username = profile.username,
                        });
                        //this sends both admin and user emails  
                        Api.AsyncCalls.sendmessagesbytemplate(EmailModels);

                    }
                    else
                    {
                        using (var logger = new Logging(applicationEnum.UserAuthorizationService))
                        {
                            var dd = new Exception("User password change failed for user");
                            logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, dd, profile.id, null);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                success = false;
                throw ex;

            }


            return success;
        }

        #endregion
        // Other overrides not implemented
        #region "Other overrides not implemented"



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
