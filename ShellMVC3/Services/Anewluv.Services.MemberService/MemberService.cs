using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;




using System.Web;
using System.Net;


using System.ServiceModel.Activation;
using Anewluv.DataAccess.Interfaces;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Anewluv.DataAccess.ExtentionMethods;


namespace Anewluv.Services.MemberService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberService : IMemberService 
    {


        IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberService(IUnitOfWork unitOfWork)
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
        
        //modifed to get search settings as well 
        //get full profile stuff
        //4-28-2012 added profile visibility settings

        public profiledata getprofiledatabyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   return  db.GetRepository<profiledata>().getprofiledatabyprofileid(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid ));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        
        }
        public searchsetting getperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    IQueryable<searchsetting> tmpsearchsettings = default(IQueryable<searchsetting>);
                    //Dim ctx As New Entities()
                    tmpsearchsettings = db.GetRepository<searchsetting>().Find().Where(p => p.profile_id == model.profileid && p.myperfectmatch == true);

                    //End If
                    if (tmpsearchsettings.Count() > 0)
                    {
                        return tmpsearchsettings.FirstOrDefault();
                    }
                    else
                    {
                        //get the profileDta                    

                        searchsetting Newsearchsettings = new searchsetting();

                        Newsearchsettings = new searchsetting();
                        Newsearchsettings.profile_id = model.profileid.GetValueOrDefault();
                        Newsearchsettings.myperfectmatch = true;
                        Newsearchsettings.searchname = "myperfectmatch";
                        //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                        //   db.GetRepository<Country_PostalCode_List>().searchsettings.Add(Newsearchsettings);
                        //   db.GetRepository<Country_PostalCode_List>().SaveChanges();
                        //save the profile data with the search settings to the database so we dont have to create it again
                        return Newsearchsettings;



                    }
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }
        public searchsetting createmyperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {
           
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {
                          //get the profileDta                    

                          searchsetting Newsearchsettings = new searchsetting();

                          Newsearchsettings = new searchsetting();
                          Newsearchsettings.profile_id = model.profileid.GetValueOrDefault();
                          Newsearchsettings.myperfectmatch = true;
                          Newsearchsettings.searchname = "myperfectmatch";
                          //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                           db.Add(Newsearchsettings);                        
                           int i = db.Commit();
                           transaction.Commit();


                          //save the profile data with the search settings to the database so we dont have to create it again
                          return Newsearchsettings;

                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }



                  }
            }
          
        }

        //get full profile stuff
        //*****************************************************
        public string getgenderbyphotoid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = false;
            using (var db = _unitOfWork)
            {
                try
                {

                 return  db.GetRepository<profiledata>().Find().Single(p=>p.profilemetadata.photos.ToList().Any(z=>z.id == model.photoid)).gender.description;
                   
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        
        }

        //TO DO this needs to be  linked to roles
        //"Message and Email Quota stuff"
        //***********************************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool checkifquoutareachedandupdate(ProfileModel model)
        {

            //get the profile
            //profile myProfile;
            profile myProfile = new profile();
            DateTime currenttime = DateTime.Now;
            bool QuotaHit = false;

            //get the profileid from userID
            //ProfileModel model = GetProfileIdbyusername(username);

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                   db.IsAuditEnabled = false; //do not audit on adds
                   using (var transaction = db.BeginTransaction())
                   {
                       try
                       {
                           //get the profile
                           myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);

                           //update all other sessions that were not properly logged out   
                           //check if the user hit the count before updating that
                           int EmailDailyQuota = myProfile.dailsentmessagequota ?? 0;
                           int EmailQuotaLimitWithNoRoleCheck = db.GetRepository<communicationquota>().Find().ToList().Where(p => p.id == 1).FirstOrDefault().quotavalue ?? 0;
                           //TO DO check qoute for correct role down the line
                           if (EmailDailyQuota != 0 && EmailDailyQuota >= EmailQuotaLimitWithNoRoleCheck)
                           {
                               myProfile.sentemailquotahitcount = myProfile.sentemailquotahitcount == null ? 1 : myProfile.sentemailquotahitcount + 1;
                               QuotaHit = true;
                           }
                           // update the count
                           myProfile.dailysentemailquota = myProfile.dailysentemailquota == null ? 1 : myProfile.dailysentemailquota + 1;
                          
                           db.Update(myProfile);
                           int i = db.Commit();
                           transaction.Commit();

                           return true;
                       }
                       catch (Exception ex)
                       {
                           transaction.Rollback();
                           //instantiate logger here so it does not break anything else.
                           logger = new ErroLogging(applicationEnum.MemberService);
                           //int profileid = Convert.ToInt32(viewerprofileid);
                           logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                           //can parse the error to build a more custom error mssage and populate fualt faultreason
                           FaultReason faultreason = new FaultReason("Error in member service");
                           string ErrorMessage = "";
                           string ErrorDetail = "ErrorMessage: " + ex.Message;
                           throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                           //throw convertedexcption;
                       }
                   }
            }
        }

        // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //*************************************************************************************************
        //update the database i.e create folders and change profile status from guest to active ?!
        public bool createmailboxfolders(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {

                      try
                      {
                          int max = 5;
                          int i = 1;

                          for (i = 1; i < max; i++)
                          {
                              mailboxfolder p = new mailboxfolder();
                              p.foldertype.id = i;
                              p.profiled_id = model.profileid.GetValueOrDefault();
                              //determin what the folder type is , we have inbox=1 , sent=2, Draft=3,Trash=4,Deleted=5
                              switch (i)
                              {
                                  case 1:
                                      p.foldertype.defaultfolder.description = "Inbox";
                                      break;
                                  case 2:
                                      p.foldertype.defaultfolder.description = "Sent";
                                      break;
                                  case 3:
                                      p.foldertype.defaultfolder.description = "Drafts";
                                      break;
                                  case 4:
                                      p.foldertype.defaultfolder.description = "Trash";
                                      break;
                                  case 5:
                                      p.foldertype.defaultfolder.description = "Deleted";
                                      break;
                              }
                               db.Add(p);
                               int z = db.Commit();
                               transaction.Commit();

                               return true;
                          }
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }

                  return false;
            }

     
        }
      
        public bool activateprofile(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {

                          var myProfile =  db.GetRepository<profile>().getprofilebyprofileid(model);
                         // if( myProfile == null ) return null;
                          //update the profile status to 2
                          myProfile.status.id = (int)profilestatusEnum.Activated;
                          //handele the update using EF
                          //  db.GetRepository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                          db.Update(myProfile);
                          int i = db.Commit();
                          transaction.Commit();

                          return true;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }
            }

         }

        public bool deactivateprofile(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {
                          var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model); 
                          //update the profile status to 2
                          myProfile.status.id = (int)profilestatusEnum.Inactive;
                          //handele the update using EF
                          //  db.GetRepository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                          db.Update(myProfile);
                          int i = db.Commit();
                          transaction.Commit();

                          return true;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }

            }

        }
        //updates the profile with a password that is presumed to be already encyrpted
        public bool updatepassword(ProfileModel model, string encryptedpassword)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {

                          var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model); 
                          //update the profile status to 2
                          myProfile.password = encryptedpassword;
                          myProfile.modificationdate = DateTime.Now;
                          myProfile.passwordChangeddate = DateTime.Now;
                          myProfile.passwordchangecount = (myProfile.passwordchangecount == null) ? 1 : myProfile.passwordchangecount + 1;
                          //handele the update using EF
                          //  db.GetRepository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                          db.Update(myProfile);
                          int i = db.Commit();
                          transaction.Commit();

                          return true;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }
            }

         }

        public bool addnewopenidforprofile(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {

                          var profileOpenIDStore = new openid
                          {
                              active = true,
                              creationdate = DateTime.UtcNow,
                              profile_id = model.profileid.GetValueOrDefault(),
                              openidprovider = db.GetRepository<lu_openidprovider>().Find().Where(p => (p.description).ToUpper() == model.openidprovider.ToUpper()).FirstOrDefault(),
                              openididentifier = model.openididentifier
                          };
                           db.Add(profileOpenIDStore);
                           int i = db.Commit();
                           transaction.Commit();

                           return true;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }
            }

         
        }

        //check if profile is activated 
        public bool checkifprofileisactivated(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    return db.GetRepository<profile>().checkifprofileisactivated(model);


                    
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        
        }

        //check if mailbox folder exist
        public bool checkifmailboxfoldersarecreated(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    return (db.GetRepository<mailboxfolder>().Find().Where(p => p.profiled_id == model.profileid).FirstOrDefault() != null);
    }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

     
        }      

        //get the last time the user logged in from profile
        public Nullable<DateTime> getmemberlastlogintimebyprofileid(ProfileModel model)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyprofileid(model).logindate;
                                  

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

    
        }

        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        public bool updateuserlogintimebyprofileidandsessionid(ProfileModel model)
        {

            //get the profile
            //profile myProfile;
           // IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
          

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
               
                  db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                      try
                      {
                          //update all other sessions that were not properly logged out
                         var  myQuery = db.GetRepository<userlogtime>().Find().Where(p => p.profile_id == model.profileid && p.offline == false).ToList(); ;

                          foreach (userlogtime p in myQuery)
                          {
                              p.offline = true;
                              db.Update(p);
                          }
                        

                          //aloso update the profile table with current login date
                         var  myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
                          //update the profile status to 2
                          myProfile.logindate = DateTime.Now;
                          db.Update(myProfile);


                          //noew aslo update the logtime and then 
                          userlogtime myLogtime = new userlogtime();
                          myLogtime.profile_id = model.profileid.GetValueOrDefault();
                          myLogtime.sessionid = model.sessionid;
                          myLogtime.logintime = DateTime.Now;
                          db.Add(myLogtime);
                          //save all changes bro                         
                          int i = db.Commit();
                          transaction.Commit();

                          return true;
                      }
                      catch (Exception ex)
                      {
                          transaction.Rollback();
                          //instantiate logger here so it does not break anything else.
                          logger = new ErroLogging(applicationEnum.MemberService);
                          //int profileid = Convert.ToInt32(viewerprofileid);
                          logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                          //can parse the error to build a more custom error mssage and populate fualt faultreason
                          FaultReason faultreason = new FaultReason("Error in member service");
                          string ErrorMessage = "";
                          string ErrorDetail = "ErrorMessage: " + ex.Message;
                          throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                          //throw convertedexcption;
                      }
                  }
            }

       }


        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool updateuserlogouttimebyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                 db.IsAuditEnabled = false; //do not audit on adds
                  using (var transaction = db.BeginTransaction())
                  {
                try
                {
                    //update all other sessions that were not properly logged out
                    var myQuery = db.GetRepository<userlogtime>().Find().Where(p => p.profile_id == model.profileid && p.offline == false).ToList(); ;

                    foreach (userlogtime p in myQuery)
                    {
                        p.offline = true;
                        p.logouttime = DateTime.Now;                          
                        db.Update(p);
                    }

                    int i = db.Commit();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
}
            }

        }

        public bool updateuserlogintimebyprofileid(ProfileModel model)
        {
            //get the profile
            //profile myProfile;
            //IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
           // profile myProfile = new profile();
           // userlogtime myLogtime = new userlogtime();
          //  DateTime currenttime = DateTime.Now;

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {

                    try
                    {
                        //update all other sessions that were not properly logged out
                       var  myQuery = db.GetRepository<userlogtime>().Find().Where(p => p.profile_id == model.profileid && p.offline == false).ToList(); ;

                        foreach (userlogtime p in myQuery)
                        {
                            p.offline = true;
                            db.Update(p);
                        }

                        //aloso update the profile table with current login date
                           //aloso update the profile table with current login date
                         var  myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
                          //update the profile status to 2
                          myProfile.logindate = DateTime.Now;
                          db.Update(myProfile);

                        //TO DO list wwhat are where they logged in from in that one table
                        //noew aslo update the logtime and then 
                          //noew aslo update the logtime and then 
                          userlogtime myLogtime = new userlogtime();
                         myLogtime.profile_id = model.profileid.GetValueOrDefault();
                         // myLogtime.sessionid  = model.sessionid ;
                         myLogtime.logintime = DateTime.Now;                         
                         db.Add(myLogtime);
                        //save all changes bro
                         int i = db.Commit();
                         transaction.Commit();

                         return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }
                }
            }

        }

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day

        public string getlastloggedinstring(string logindate)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return profileextentionmethods.getlastloggedinstring(Convert.ToDateTime(logindate));

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);                  
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        //returns true if somone logged on
        public bool getuseronlinestatus(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                 return   db.GetRepository<profile>().getuseronlinestatus(model);
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>
        public bool checkifscreennamealreadyexists(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                 return   db.GetRepository<profile>().checkifscreennamealreadyexists(model);                   

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        
        }

        //5-20-2012 added to check if a user email is registered

        //public bool checkifusernamealreadyexists(ProfileModel model)
        //{
        //    return _memberrepository.checkifusernamealreadyexists(model);

        //}

        public bool checkifusernamealreadyexists(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                     return db.GetRepository<profile>().checkifusernamealreadyexists(model);

                  
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }

        public string validatesecurityansweriscorrect(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    myQuery =  db.GetRepository<profile>().Find().Where(p => p.id == model.profileid && p.securityanswer == model.securityanswer && p.securityquestion.description == model.securityquestion);

                    if (myQuery.Count() > 0)
                    {
                        return myQuery.FirstOrDefault().username.ToString();

                    }
                    else
                    {
                        return "";
                    }

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
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
        /// Determines wethare an activation code matches the value in the database for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    //Dim ctx As New Entities()
                   return db.GetRepository<profile>().checkifactivationcodeisvalid(model);
                   
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        public profile getprofilebyusername(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyusername(model);
               

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }

        public profile getprofilebyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyprofileid(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

     
        }

       public profile getprofilebyemailaddress(ProfileModel model)
       {
           _unitOfWork.DisableProxyCreation = true;
           using (var db = _unitOfWork)
           {
               try
               {

                   return db.GetRepository<profile>().getprofilebyemailaddress(model);

               }
               catch (Exception ex)
               {

                   //instantiate logger here so it does not break anything else.
                   logger = new ErroLogging(applicationEnum.MemberService);
                   //int profileid = Convert.ToInt32(viewerprofileid);
                   logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
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
        /// added ability to grab from appfabric cache
        /// </summary>
        /// <param name="strusername"></param>
        /// <returns></returns>
       public int? getprofileidbyusername(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyusername(model).id ;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

}

        public int?  getprofileidbyopenid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    return db.GetRepository<profile>().getprofileidbyopenid(model).id ;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

       }      

       public int? getprofileidbyscreenname(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   return db.GetRepository<profile>().getprofilebyscreenname(model).id;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

       public int? getprofileidbyssessionid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    throw new NotImplementedException();

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

           
        }

        public string getusernamebyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyprofileid(model).username;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

           
        }

        public string getscreennamebyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    return db.GetRepository<profile>().getprofilebyprofileid(model).screenname ;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

           
        }

        public string getscreennamebyusername(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().getprofilebyusername(model).screenname ;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

           
        
        }

        public bool checkifemailalreadyexists(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profile>().checkifemailalreadyexists(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        
        }

        //Start of stuff pulled from MVC members repository
        
        public string getgenderbyscreenname(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    return db.GetRepository<profiledata>().getprofiledatabyscreenname(model).gender.description;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

          
        }
        
        public visiblitysetting getprofilevisibilitysettingsbyprofileid(ProfileModel model)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                   return  db.GetRepository<visiblitysetting>().getvisibilitysettingsbyprofileid(model);
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

           

        }

       


    }
}
