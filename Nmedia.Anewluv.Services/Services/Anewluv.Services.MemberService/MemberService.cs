using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;




using System.Web;
using System.Net;


using System.ServiceModel.Activation;
//using Nmedia.DataAccess.Interfaces;

using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
//using Nmedia.Infrastructure.Domain.Data.log;



using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data;
using Nmedia.Infrastructure.Domain.Data;

using Anewluv.DataExtentionMethods;
using System.Threading.Tasks;

using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using Nmedia.Infrastructure;



namespace Anewluv.Services.Members
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberService : IMemberService
    {


        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberService(IUnitOfWorkAsync unitOfWork)
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
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

        //modifed to get search settings as well 
        //get full profile stuff
        //4-28-2012 added profile visibility settings

        public profiledata getprofiledatabyprofileid(ProfileModel model)
        {


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profiledata>().getprofiledatabyprofileid(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }


        }

        public async Task<searchsetting> getperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {


            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return profileextentionmethods.getperfectmatchsearchsettingsbyprofileid(model, _unitOfWorkAsync);

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        public async Task createmyperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {



            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            profileextentionmethods.createsearchbyprofileid("MyPerfectMatch", true, model, _unitOfWorkAsync);

                        });
                        await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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



            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {
                        //get the profile
                        myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);

                        //update all other sessions that were not properly logged out   
                        //check if the user hit the count before updating that
                        int EmailDailyQuota = myProfile.dailsentmessagequota ?? 0;
                        int EmailQuotaLimitWithNoRoleCheck = _unitOfWorkAsync.Repository<communicationquota>().Queryable().ToList().Where(p => p.id == 1).FirstOrDefault().quotavalue ?? 0;
                        //TO DO check qoute for correct role down the line
                        if (EmailDailyQuota != 0 && EmailDailyQuota >= EmailQuotaLimitWithNoRoleCheck)
                        {
                            myProfile.sentemailquotahitcount = myProfile.sentemailquotahitcount == null ? 1 : myProfile.sentemailquotahitcount + 1;
                            QuotaHit = true;
                        }
                        // update the count
                        myProfile.dailysentemailquota = myProfile.dailysentemailquota == null ? 1 : myProfile.dailysentemailquota + 1;
                        myProfile.ObjectState = ObjectState.Modified;
                        _unitOfWorkAsync.Repository<profile>().Update(myProfile);
                        var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        //update the Initial Catalog= i.e create folders and change profile status from guest to active ?!
        public async Task createmailboxfolders(ProfileModel model)
        {


            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        bool mailboxfolderexist = (_unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(p => p.profile_id == model.profileid.Value).FirstOrDefault() != null);

                        if (mailboxfolderexist) return;

                        var task = Task.Factory.StartNew(() =>
                        {
                            int max = 5;
                            int i = 1;

                            for (i = 1; i < max; i++)
                            {
                                mailboxfolder p = new mailboxfolder();
                                p.creationdate = DateTime.Now;

                                p.maxsizeinbytes = 128;
                                p.defaultfolder_id = i;
                                p.displayname = _unitOfWorkAsync.Repository<lu_defaultmailboxfolder>().Queryable().Where(z => z.id == i).FirstOrDefault().description;
                                p.profile_id = model.profileid.Value;
                                p.ObjectState = ObjectState.Added;
                                //determin what the folder type is , e have inbox=1 , sent=2, Draft=3,Trash=4,Deleted=5
                                //switch (i)
                                //{
                                //    case 1:
                                //        p..lu_defaultmailboxfolder.description = "Inbox";
                                //        break;
                                //    case 2:
                                //        p.mailboxfoldertype.lu_defaultmailboxfolder.description = "Sent";
                                //        break;
                                //    case 3:
                                //        p.mailboxfoldertype.lu_defaultmailboxfolder.description = "Drafts";
                                //        break;
                                //    case 4:
                                //        p.mailboxfoldertype.lu_defaultmailboxfolder.description = "Trash";
                                //        break;
                                //    case 5:
                                //        p.mailboxfoldertype.lu_defaultmailboxfolder.description = "Deleted";
                                //        break;
                                //}
                                p.ObjectState = ObjectState.Added;
                                _unitOfWorkAsync.Repository<mailboxfolder>().Insert(p);
                                var m = _unitOfWorkAsync.SaveChanges();
                                // transaction.Commit();

                                //   return true;
                            }
                            //  return false;
                        });
                        await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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

        public async Task<bool> activateprofile(ProfileModel model)
        {


            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {

                        var task = Task.Run(() =>
                        {
                            var myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);
                            // if( myProfile == null ) return null;
                            //update the profile status to 2
                            myProfile.status_id = (int)profilestatusEnum.Activated;
                            myProfile.ObjectState = ObjectState.Modified;
                            //handele the update using EF
                            //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                            myProfile.ObjectState = ObjectState.Modified;
                            _unitOfWorkAsync.Repository<profile>().Update(myProfile);
                            var i = _unitOfWorkAsync.SaveChanges();
                            // transaction.Commit();

                            return true;
                        });
                        return await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {
                        var myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);
                        //update the profile status to 2
                        myProfile.status_id = (int)profilestatusEnum.Inactive;
                        //handele the update using EF
                        //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        myProfile.ObjectState = ObjectState.Modified;
                        _unitOfWorkAsync.Repository<profile>().Update(myProfile);
                        var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);
                        //update the profile status to 2
                        myProfile.password = encryptedpassword;
                        myProfile.modificationdate = DateTime.Now;
                        myProfile.passwordChangeddate = DateTime.Now;
                        myProfile.passwordchangecount = (myProfile.passwordchangecount == null) ? 1 : myProfile.passwordchangecount + 1;
                        //handele the update using EF
                        //  _unitOfWorkAsync.Repository<Country_PostalCode_List>().profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        myProfile.ObjectState = ObjectState.Modified;
                        _unitOfWorkAsync.Repository<profile>().Update(myProfile);
                        var i = _unitOfWorkAsync.SaveChanges();
                        // transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        public async Task<bool> addnewopenidforprofile(ProfileModel model)
        {


            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {

                        var query1 = await _unitOfWorkAsync.RepositoryAsync<lu_openidprovider>().Query(p => (p.description).ToUpper() == model.openidprovider.ToUpper()).SelectAsync();

                        var openidprovider = query1.FirstOrDefault();

                        if (openidprovider !=null)
                        {
                            var myQuery = await _unitOfWorkAsync.RepositoryAsync<profile>().Query(p => p.id == model.profileid.Value && p.openids.Any(f => f.openididentifier != model.openididentifier && f.lu_openidprovider.description.ToUpper() != model.openidprovider.ToUpper())).SelectAsync();

                            if (myQuery == null)
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

                            }
                        }

                        return false;



                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool> checkifprofileisactivated(ProfileModel model)
        {


            {


                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        return _unitOfWorkAsync.Repository<profile>().checkifprofileisactivated(model);
                    });
                    return await task.ConfigureAwait(false);




                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool> checkifmailboxfoldersarecreated(ProfileModel model)
        {


            {



                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        return (_unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(p => p.profile_id == model.profileid.Value).FirstOrDefault() != null);
                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        public async Task<DateTime?> getmemberlastlogintimebyprofileid(ProfileModel model)
        {



            {



                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        return _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model).logindate;

                    });
                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        public async Task updateuserlogintimebyprofileidandsessionid(ProfileModel model)
        {

            //get the profile
            //profile myProfile;
            // IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);




            {

                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            //update all other sessions that were not properly logged out
                            var myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;

                            foreach (userlogtime p in myQuery)
                            {
                                p.offline = true;
                                p.ObjectState = ObjectState.Modified;
                                _unitOfWorkAsync.Repository<userlogtime>().Update(p);
                            }


                            //aloso update the profile table with current login date
                            var myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);
                            //update the profile status to 2
                            myProfile.logindate = DateTime.Now;
                            myProfile.ObjectState = ObjectState.Modified;
                            _unitOfWorkAsync.Repository<profile>().Update(myProfile);


                            //noew aslo update the logtime and then 
                            userlogtime myLogtime = new userlogtime();
                            myLogtime.profile_id = model.profileid.Value;
                            myLogtime.offline = false;
                            myLogtime.sessionid = model.sessionid;
                            myLogtime.logintime = DateTime.Now;
                            myLogtime.ObjectState = ObjectState.Modified;
                            _unitOfWorkAsync.Repository<userlogtime>().Insert(myLogtime);
                            //save all changes bro                         
                            var i = _unitOfWorkAsync.SaveChanges();
                            // transaction.Commit();

                            // return true;
                        });
                        await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        public async Task updateuserlogouttimebyprofileid(ProfileModel model)
        {
            //_unitOfWork.DisableProxyCreation = true;

            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            //update all other sessions that were not properly logged out
                            var myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;

                            foreach (userlogtime p in myQuery)
                            {
                                p.offline = true;
                                p.logouttime = DateTime.Now;
                                p.ObjectState = ObjectState.Modified;
                                _unitOfWorkAsync.Repository<userlogtime>().Update(p);
                            }

                            var i = _unitOfWorkAsync.SaveChanges();
                            // transaction.Commit();

                            //     return true;
                        });
                        await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        public async Task updateuserlogintimebyprofileid(ProfileModel model)
        {
            //_unitOfWork.DisableProxyCreation = true;

            {
                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                            //update all other sessions that were not properly logged out
                            var myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;

                            foreach (userlogtime p in myQuery)
                            {
                                p.offline = true;
                                p.ObjectState = ObjectState.Modified;
                                _unitOfWorkAsync.Repository<userlogtime>().Update(p);
                            }


                            //aloso update the profile table with current login date
                            var myProfile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);
                            //update the profile status to 2
                            myProfile.logindate = DateTime.Now;
                            myProfile.ObjectState = ObjectState.Modified;
                            _unitOfWorkAsync.Repository<profile>().Update(myProfile);


                            //noew aslo update the logtime and then 
                            userlogtime myLogtime = new userlogtime();
                            myLogtime.profile_id = model.profileid.Value;
                            myLogtime.offline = false;
                            myLogtime.sessionid = model.sessionid;
                            myLogtime.logintime = DateTime.Now;
                            myLogtime.ObjectState = ObjectState.Modified;
                            _unitOfWorkAsync.Repository<userlogtime>().Insert(myLogtime);
                            //save all changes bro                         
                            var i = _unitOfWorkAsync.SaveChanges();
                            // transaction.Commit();

                            // return true;

                        });
                        await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


        public async Task addprofileactvity(ActivityModel model)
        {
            //get the profile
            //profile myProfile;
            //IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
            // profile myProfile = new profile();
            // userlogtime myLogtime = new userlogtime();
            //  DateTime currenttime = DateTime.Now;

            //_unitOfWork.DisableProxyCreation = true;

            {

                //do not audit on adds
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {
                        //update all other sessions that were not properly logged out
                        // var  myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;


                        var task = Task.Factory.StartNew(() =>
                        {
                            var id = addprofileactvity(model.activitybase);
                            //get the ID and save geodata if there is data for it
                            model.activitygeodata.activity_id = id;
                            if (id != 0 && (model.activitygeodata.countryname != null | (model.activitygeodata.lattitude != 0 & model.activitygeodata.longitude != 0)))
                                addprofileactvitygeodata(model.activitygeodata, _unitOfWorkAsync);
                        });
                        await task.ConfigureAwait(false);



                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.activitybase.profile_id));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member service");
                        //  string ErrorMessage = "";
                        //   string ErrorDetail = "ErrorMessage: " + ex.Message;
                        //  throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }
                }
            }

        }

        public async Task addprofileactivities(List<ActivityModel> models)
        {

            {

                try
                {
                    //update all other sessions that were not properly logged out
                    // var  myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;

                    //do nothing if we have no models
                    if (models.Count() == 0) return;

                    var task = Task.Factory.StartNew(() =>
                    {
                        foreach (ActivityModel item in models)
                        {
                          
                            var id = addprofileactvity(item.activitybase);
                            //get the ID and save geodata if there is data for it
                            item.activitygeodata.activity_id = id;

                            if (id != 0 && (item.activitygeodata.countryname != null | (item.activitygeodata.lattitude != 0 & item.activitygeodata.longitude != 0)))
                                addprofileactvitygeodata(item.activitygeodata, _unitOfWorkAsync);
                        }
                    });
                    await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    var dd = models.FirstOrDefault();

                    if (dd != null && dd.activitybase != null)
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(dd.activitybase.profile_id));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    //   FaultReason faultreason = new FaultReason("Error in member service");
                    //   string ErrorMessage = "";
                    //   string ErrorDetail = "ErrorMessage: " + ex.Message;
                    // throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
            }


        }

        /// <summary>
        /// actvity ID is needed for this so use this carefully
        /// </summary>
        /// <param name="model"></param>
        public async Task addprofileactvitygeodata(ActivityModel model)
        {


            try
            {
                //update all other sessions that were not properly logged out
                // var  myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;


                var task = Task.Factory.StartNew(() =>
                {

                    addprofileactvitygeodata(model.activitygeodata, _unitOfWorkAsync);

                    // return true;
                });
                await task.ConfigureAwait(false);



            }
            catch (Exception ex)
            {
                //  // transaction.Rollback();
                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.activitygeodata.activity_id));
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                //    FaultReason faultreason = new FaultReason("Error in member service");
                //    string ErrorMessage = "";
                //    string ErrorDetail = "ErrorMessage: " + ex.Message;
                //     throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                //throw convertedexcption;
            }



        }

        #region "private methods for actvity"

        private int addprofileactvity(profileactivity model)
        {

            {

                try
                {
                    //update all other sessions that were not properly logged out
                    // var  myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;

                    model.ObjectState = ObjectState.Added;
                    if (model.profile_id == 0) model.profile_id = null;
                    _unitOfWorkAsync.Repository<profileactivity>().Insert(model);
                    //save all changes bro
                    _unitOfWorkAsync.SaveChanges();

                    return model.id;
                    //   // transaction.Commit();

                }
                catch (Exception ex)
                {
                    throw ex;
                    //throw convertedexcption;
                }

            }

        }

        private int addprofileactvitygeodata(profileactivitygeodata model, IUnitOfWorkAsync db)
        {


            try
            {
                //update all other sessions that were not properly logged out
                // var  myQuery = _unitOfWorkAsync.Repository<userlogtime>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.offline == false).ToList(); ;


                model.ObjectState = ObjectState.Added;
                _unitOfWorkAsync.Repository<profileactivitygeodata>().Insert(model);
                //save all changes bro
                var i = _unitOfWorkAsync.SaveChanges();

                // transaction.Commit();

                return model.id; ; ;


            }
            catch (Exception ex)
            {
                throw ex;

                //throw convertedexcption;
            }



        }
        #endregion

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day

        public string getlastloggedinstring(string logindate)
        {


            {
                try
                {
                    return profileextentionmethods.getlastloggedinstring(Convert.ToDateTime(logindate));

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
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


            {
                try
                {

                    return _unitOfWorkAsync.Repository<profile>().getuseronlinestatus(model);
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {

                    //TO DO remove dd
                    var dd = _unitOfWorkAsync.Repository<profile>().checkifscreennamealreadyexists(model);
                    return dd;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    //IQueryable<profile> myQuery = default(IQueryable<profile>);
                    return _unitOfWorkAsync.Repository<profile>().checkifusernamealreadyexists(model);


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    myQuery = _unitOfWorkAsync.Repository<profile>().Queryable().Where(p => p.id == model.profileid.Value && p.securityanswer == model.securityanswer && p.lu_securityquestion.description == model.securityquestion);

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
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
        /// Determines wethare an activation code matches the value in the Initial Catalog= for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(ProfileModel model)
        {


            {
                try
                {

                    //Dim ctx As New Entities()
                    return _unitOfWorkAsync.Repository<profile>().checkifactivationcodeisvalid(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyusername(model);


                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {

                    return _unitOfWorkAsync.Repository<profile>().getprofilebyemailaddress(model);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyusername(model).id;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }

        }

        public int? getprofileidbyopenid(ProfileModel model)
        {


            {
                try
                {

                    return _unitOfWorkAsync.Repository<profile>().getprofileidbyopenid(model).id;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyscreenname(model).id;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    throw new NotImplementedException();

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model).username;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {

                    return _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(model).screenname;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profile>().getprofilebyusername(model).screenname;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    var dd = _unitOfWorkAsync.Repository<profile>().checkifemailalreadyexists(model);

                    return dd;

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<profiledata>().getprofiledatabyscreenname(model).lu_gender.description;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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


            {
                try
                {
                    return _unitOfWorkAsync.Repository<visiblitysetting>().getvisibilitysettingsbyprofileid(model);
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid.Value));
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
