using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

using Shell.MVC2.AppFabric;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Data
{
   public  class MemberRepository : MemberRepositoryBase , IMemberRepository 
    {

       
       
       // private  AnewluvContext _datingcontext; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();
       //private IPhotoRepository _photorepository;

       private LoggingLibrary.ErroLogging logger;

       public MemberRepository(AnewluvContext datingcontext) 
           :base(datingcontext)
       {
          
       }

       
           
       //2-13-2013 olawal
       //new stuff to get a users roles 

       public  membersinrole   getmemberrolebyprofileid(int profileid)
       {
           return this._datingcontext.membersinroles.Where(p => p.profile_id == profileid).FirstOrDefault();
       }





            //"Initial Profile STuff"
       //*********************************************************************
            
           

             //modifed to get search settings as well 
             //get full profile stuff
            //4-28-2012 added profile visibility settings
            
             public profiledata getprofiledatabyprofileid(int profileid)
             {

                 try
                 {
                     ////attempt to load the search settings as well
                     //var items = from i in this._datingcontext.profiledata.Include("searchsettings").Include("Profile")
                     //            where (i.ProfileID == profileid) && (i.searchsettings.Any(t => t.myperfectmatch == true))     
                     //   select i;

                     //var PerfectMatchsearchsettings = GetPerFectMatchsearchsettingsByProfileID(profileid);

                     //if (items.Count() > 0)
                     //{
                     //  return items.FirstOrDefault();
                     //}
                     //else
                     //{
                     //  items = from i in this._datingcontext.profiledata
                     //            where (i.ProfileID == profileid)         
                     //  select i;
                     //  return items.FirstOrDefault();
                     //}

                     var items = from i in this._datingcontext.profiledata
                                 where (i.profile_id == profileid)
                                 select i;


                     //now filter the search settings enity and only pull down the one that has the 
                     //  get perfect match settings and add it 


                     if (items.Count() > 0)
                     {


                         items.FirstOrDefault().profile.profilemetadata.searchsettings.Add(this.getperfectmatchsearchsettingsbyprofileid(profileid));

                         return items.FirstOrDefault();

                     }

                     //return nothing if bad
                     return null;

                 }
                 catch (Exception ex)
                 {
                     //instantiate logger here so it does not break anything else.
                     logger = new ErroLogging(applicationEnum.MemberService);
                     logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                     //log error mesasge
                     //handle logging here
                     var message = ex.Message;
                     throw;
                 }


             }

             public searchsetting  getperfectmatchsearchsettingsbyprofileid(int profileid)
             {
                 try
                 {
                     IQueryable<searchsetting> tmpsearchsettings = default(IQueryable<searchsetting>);
                     //Dim ctx As New Entities()
                     tmpsearchsettings = this._datingcontext.searchsetting
                         // .Include("ProfileData1")
                         //.Include("searchsettings_Genders")
                         // .Include("searchsettings_BodyTypes")
                         //  .Include("searchsettings_Diet")
                         //   .Include("searchsettings_Drinks")
                         // .Include("searchsettings_EducationLevel")
                         // .Include("searchsettings_EmploymentStatus")
                         //   .Include("searchsettings_Ethnicity")
                         //  .Include("searchsettings_Exercise")
                         //   .Include(" searchsettings_EyeColor")
                         //   .Include("searchsettings_HairColor")
                         //   .Include("searchsettings_HaveKids")
                         //  .Include("searchsettings_Hobby")
                         //   .Include("searchsettings_HotFeature")
                         //  .Include("searchsettings_Humor")
                         //  .Include("searchsettings_IncomeLevel")
                         //  .Include("searchsettings_LivingStituation")
                         //  .Include("searchsettings_Location")
                         //  .Include("searchsettings_LookingFor")
                         //   .Include("searchsettings_MaritalStatus")
                         //  .Include("searchsettings_PoliticalView")
                         //  .Include("searchsettings_Profession")
                         //  .Include(" searchsettings_Religion")
                         //  .Include("searchsettings_ReligiousAttendance")
                         //   .Include("searchsettings_ShowMe")
                         //   .Include("searchsettings_Sign")
                         //  .Include("searchsettings_Smokes")
                         //   .Include("searchsettings_SortByType")
                         //  .Include("searchsettings_WantKids")
                         //  .Include("searchsettings_Tribe")
                     .Where(p => p.profile_id == profileid && p.myperfectmatch == true);

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
                         Newsearchsettings.profile_id = profileid;
                         Newsearchsettings.myperfectmatch = true;
                         Newsearchsettings.searchname = "myperfectmatch";
                         //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                         //  this._datingcontext.searchsettings.Add(Newsearchsettings);
                         //  this._datingcontext.SaveChanges();
                         //save the profile data with the search settings to the database so we dont have to create it again
                         return Newsearchsettings;



                     }
                 }
                 catch (Exception ex)
                 {
                     //instantiate logger here so it does not break anything else.
                     logger = new ErroLogging(applicationEnum.MemberService);
                     logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                     //log error mesasge
                     //handle logging here
                     var message = ex.Message;
                     throw;
                 }
             }

             public searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid)
             {


                 try
                 {
                     //get the profileDta                    

                     searchsetting Newsearchsettings = new searchsetting();

                     Newsearchsettings = new searchsetting();
                     Newsearchsettings.profile_id = profileid;
                     Newsearchsettings.myperfectmatch = true;
                     Newsearchsettings.searchname = "myperfectmatch";
                     //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                     this._datingcontext.searchsetting.Add(Newsearchsettings);
                     this._datingcontext.SaveChanges();


                     //save the profile data with the search settings to the database so we dont have to create it again
                     return Newsearchsettings;
                 }
                 catch (Exception ex)
                 {
                     //instantiate logger here so it does not break anything else.
                     logger = new ErroLogging(applicationEnum.MemberService);
                     logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                     //log error mesasge
                     //handle logging here
                     var message = ex.Message;
                     throw;
                 }


                 
             }
   
             //get full profile stuff
            //*****************************************************
            
             public string getgenderbyphotoid(Guid guid)
             {
                 try
                 {
                     lu_gender _gender = new lu_gender();


                     _gender = (from x in (_datingcontext.photos.Where(f => f.id == guid))
                                join f in _datingcontext.profiledata on x.profile_id equals f.profile_id
                                select f.gender).FirstOrDefault();


                     return _gender.description;
                 }
                 catch (Exception ex)
                 {
                     //instantiate logger here so it does not break anything else.
                     logger = new ErroLogging(applicationEnum.MemberService);
                     logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                     //log error mesasge
                     //handle logging here
                     var message = ex.Message;
                     throw;
                 }
             }

            //TO DO this needs to be  linked to roles
             //"Message and Email Quota stuff"
       //***********************************************************************
             // Description:	Updates the users logout time
             // added 1/18/2010 ola lawal
             public bool checkifquoutareachedandupdate(int profileid)
             {

                 //get the profile
                 //profile myProfile;
               profile  myProfile = new profile();
               DateTime currenttime = DateTime.Now;
               bool QuotaHit = false ;

                 //get the profileid from userID
                 //int profileid = GetProfileIdbyusername(username);

                 try
                 {
                  //get the profile
                     myProfile = this._datingcontext.profiles.Where(p => p.id  == profileid).FirstOrDefault();

                     //update all other sessions that were not properly logged out
                    // myProfile = ;

                    // foreach (userlogtime p in myQuery)
                    //'{
                    //     p.LogoutTime = currenttime;
                    // }
                    
                     //check if the user hit the count before updating that
                     int EmailDailyQuota = myProfile.dailsentmessagequota  ?? 0;                      
                     int EmailQuotaLimitWithNoRoleCheck = this._datingcontext.communicationquotas.Where(p => p.id  == 1).FirstOrDefault().quotavalue ?? 0;
                     //TO DO check qoute for correct role down the line
                     if (EmailDailyQuota !=0 &&  EmailDailyQuota >= EmailQuotaLimitWithNoRoleCheck)
                     {
                         myProfile.sentemailquotahitcount = myProfile.sentemailquotahitcount == null ? 1 : myProfile.sentemailquotahitcount + 1;
                     QuotaHit = true;
                     }
                     // update the count
                     myProfile.dailysentemailquota = myProfile.dailysentemailquota == null ? 1 : myProfile.dailysentemailquota + 1;
                     _datingcontext.SaveChanges();

                     
                 }
                 catch (Exception ex)
                 {
                     //instantiate logger here so it does not break anything else.
                     logger = new ErroLogging(applicationEnum.MemberService);
                     logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                     //log error mesasge
                     //handle logging here
                     var message = ex.Message;
                     throw;
                 }

                 return QuotaHit;
             }

           // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
       //*************************************************************************************************
             //update the database i.e create folders and change profile status from guest to active ?!
                public bool createmailboxfolders(int intprofileid)
                {
                   
                    int max = 5;
                    int i = 1;
                    try
                    
                    {
                    
                    for(i = 1; i < max;i++){
                   mailboxfolder    p = new mailboxfolder();
                    p.foldertype.id   = i;
                    p.profiled_id  = intprofileid;
                    p.active   = 1;
                          //determin what the folder type is , we have inbox=1 , sent=2, Draft=3,Trash=4,Deleted=5
                      switch(i)       
                      {         
                         case 1:
                              p.foldertype.defaultfolder.description  = "Inbox";
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
                      this._datingcontext.mailboxfolders.Add(p);
                      this._datingcontext.SaveChanges();
                     // _datingcontext.AddToproducts(p);
                     // _datingcontext.SaveChanges();
  
                    }
                
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                    return true;
                }

                public bool activateprofile(int intprofileid)
                {
                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile  = this._datingcontext.profiles.Where(p => p.id  == intprofileid).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.status.id  = (int)profilestatusEnum.Activated;
                        //handele the update using EF
                       // this._datingcontext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }
                 
                    return true;
                }

                public bool deactivateprofile(int intprofileid)
                {
                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile = this._datingcontext.profiles.Where(p => p.id == intprofileid).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.status.id  = (int)profilestatusEnum.Inactive ;
                        //handele the update using EF
                        // this._datingcontext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                    return true;
                }
                //updates the profile with a password that is presumed to be already encyrpted
                public bool updatepassword(int profileid, string encryptedpassword)
                {

                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile = this._datingcontext.profiles.Where(p => p.id == profileid).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.password  = encryptedpassword;
                        myProfile.modificationdate  = DateTime.Now;
                        myProfile.passwordChangeddate  = DateTime.Now;
                        myProfile.passwordchangecount = (myProfile.passwordchangecount == null) ? 1 : myProfile.passwordchangecount + 1;
                        //handele the update using EF
                        // this._datingcontext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                    return true;
                }

                public bool addnewopenidforprofile(int profileid,string openidIdentifer, string openidProvidername)
                {

                  
                    try
                    {
                         var profileOpenIDStore = new openid 
                     {
                           active = true,
                          creationdate  = DateTime.UtcNow ,
                           profile_id   = profileid,
                         openidprovider    = _datingcontext.lu_openidprovider.Where(p=> (p.description ).ToUpper() == openidProvidername.ToUpper ()).FirstOrDefault(),
                           openididentifier   = openidIdentifer
                        };
                     this._datingcontext.opendIds.Add (profileOpenIDStore);
                     this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                    return true;
                }

                //check if profile is activated 
                public bool checkifprofileisactivated(int intprofileid)
                {
                    try
                    {
                        IQueryable<profile> myQuery = default(IQueryable<profile>);
                        myQuery = this._datingcontext.profiles.Where(p => p.id == intprofileid & p.status.id != 1);


                        if (myQuery.Count() > 0)
                        {
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
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }
                }

                //check if mailbox folder exist
                public bool checkifmailboxfoldersarecreated(int intprofileid)
                {
                    try
                    {

                        mailboxfolder myQuery;
                        myQuery = this._datingcontext.mailboxfolders.Where(p => p.profiled_id == intprofileid).FirstOrDefault();


                        if (myQuery != null)
                        {
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
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                }
       

            //"DateTimeFUcntiosn for longin etc "
       //**********************************************************
                // Description:	Updates the users logout time
                // added 1/18/2010 ola lawal
  public bool updateuserlogouttime(int profileid, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<userlogtime > myQuery = default(IQueryable<userlogtime>);
                    DateTime currenttime = DateTime.Now;

                    //get the profileid from userID
                    //int profileid = GetProfileIdbyusername(username);

                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this._datingcontext.userlogtimes.Where(p => p.profile_id == profileid && p.offline == true && p.sessionid  == sessionID);

                        foreach (userlogtime p in myQuery)
                        {
                            p.logouttime   = currenttime;
                        }

                        _datingcontext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }
                    return true;
                }


                //get the last time the user logged in from profile
  public Nullable<DateTime> getmemberlastlogintime(int profileid)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    // DateTime currenttime = DateTime.Now;                   
                    try
                    {

                        myQuery = this._datingcontext.profiles.Where(p => p.id  == profileid);
                        if (myQuery.Count() > 0)
                        {
                            return myQuery.FirstOrDefault().logindate ;
                        }
                        else
                        {
                            return null;
                        }                    
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }


                }


                //updates all the areas  that handle when a user logs in 
                // added 1/18/2010 ola lawal
                //also updates the last log in and profile data
   public bool updateuserlogintime(string username, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
                    profile myProfile = new profile();
                    userlogtime myLogtime = new userlogtime();
                    DateTime currenttime = DateTime.Now;

                    //get the profileid from userID
                    int? profileid = getprofileidbyusername(username);
                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this._datingcontext.userlogtimes.Where(p => p.profile_id  == profileid && p.offline  == false );

                        foreach (userlogtime p in myQuery)
                        {
                            p.offline  = true;

                        }

                        //aloso update the profile table with current login date
                        myProfile = this._datingcontext.profiles.Where(p => p.id == profileid).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.logindate  = currenttime;


                        //noew aslo update the logtime and then 
                        myLogtime.profile_id  = profileid.GetValueOrDefault();
                        myLogtime.sessionid  = sessionID;
                        myLogtime.logintime  = currenttime;
                        this._datingcontext.userlogtimes.Add(myLogtime);
                        //save all changes bro
                        this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                    return true;
                }

    public bool updateuserlogintimebyprofileid(int intprofileid, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
                    profile myProfile = new profile();
                    userlogtime myLogtime = new userlogtime();
                    DateTime currenttime = DateTime.Now;

                    //get the profileid from userID
                    int profileid = intprofileid;//GetProfileIdbyusername(username);
                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this._datingcontext.userlogtimes.Where(p => p.profile_id  == profileid && p.offline  == false );

                        foreach (userlogtime p in myQuery)
                        {
                            p.offline  = true;

                        }

                        //aloso update the profile table with current login date
                        myProfile = this._datingcontext.profiles.Where(p => p.id == profileid).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.logindate = currenttime;


                        //noew aslo update the logtime and then 
                        myLogtime.profile_id  = profileid;
                        myLogtime.sessionid  = sessionID;
                        myLogtime.logintime = currenttime;
                        this._datingcontext.userlogtimes.Add(myLogtime);
                        //save all changes bro
                        this._datingcontext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }
                    return true;
                }

    //date time functions '
   //***********************************************************
     //this function will send back when the member last logged in
  //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
  //Ola Lawal 7/10/2009 feel free to drill down even to the day
            
                public string getlastloggedinstring( DateTime  logindate)
                {

                    try
                    {
                        if (logindate == null | string.IsNullOrEmpty(Convert.ToString(logindate)))
                        {
                            return "Three Weeks Ago";
                        }

                        //'you can compare dates and times the same as you would any other number


                        DateTime DateThreeDaysAgo;
                        DateTime DateThreeWeeksago;
                        DateTime DateOneWeekAgo;
                        DateTime DateThreeMonthsAgo;
                        DateTime DateSixMonthsAgo;
                        DateTime DateOneMonthAgo;

                        DateTime Today = DateTime.Now;


                        DateThreeDaysAgo = Today.AddDays(-3);
                        //Subtract 3 days
                        DateOneWeekAgo = Today.AddDays(-7);
                        //Subtract 1 weeks
                        DateThreeWeeksago = Today.AddDays(-21);
                        //Subtract one monthe
                        DateOneMonthAgo = Today.AddMonths(-1);
                        //Subtract 3 weeks
                        DateThreeMonthsAgo = Today.AddMonths(-3);
                        //Subtract 12 weeks =3 months
                        DateSixMonthsAgo = Today.AddMonths(-6);
                        //Subtract 24 weeks =6 months

                        if (logindate > DateThreeDaysAgo)
                        {
                            return "Last Three Days";
                        }
                        else if (logindate > DateOneWeekAgo)
                        {
                            return "Last Week ";
                        }
                        else if (logindate > DateThreeWeeksago)
                        {
                            return "Three Weeks Ago";
                        }
                        else if (logindate > DateOneMonthAgo)
                        {
                            return "One Month Ago";
                        }

                        else if (logindate > DateThreeMonthsAgo)
                        {
                            return "Three Months Ago ";
                        }
                        else if (logindate > DateSixMonthsAgo)
                        {
                            return "Six Months Ago";
                        }
                        else
                        {
                            return "Over one Month";
                        }
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }

                }

                //returns true if somone logged on
     public bool getuseronlinestatus(int profileid)
                {
                    try
                    {
                        //get the profile
                        //profile myProfile;
                        IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);

                        myQuery = _datingcontext.userlogtimes.Where(p => p.profile_id == profileid && p.offline == false).Distinct().OrderBy(n => n.logintime);

                        //            var queryB =
                        //                (from o in db.Orders
                        // select o.Employee.LastName)
                        //.Distinct().OrderBy(n => n);
                        if (myQuery.Count() > 0)
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    catch (Exception ex)
                    {
                        //instantiate logger here so it does not break anything else.
                        logger = new ErroLogging(applicationEnum.MemberService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                        //log error mesasge
                        //handle logging here
                        var message = ex.Message;
                        throw;
                    }
                }
                 

          

       //other standard verifcation methods added here
       /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>
   
   public bool checkifscreennamealreadyexists(string strscreenname)
        {
            try
            {
                IQueryable<profile> myQuery = default(IQueryable<profile>);
                myQuery = this._datingcontext.profiles.Where(p => p.screenname == strscreenname | p.screenname.Replace(" ", "") == strscreenname.Replace(" ", "") | p.screenname.Replace(" ", "") == strscreenname);


                if (myQuery.Count() > 0)
                {
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
            logger = new ErroLogging(applicationEnum.MemberService );
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex,  null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
        }

   //5-20-2012 added to check if a user email is registered
  
   public bool checkifprofileidalreadyexists(int profileid)
   {

       try
       {
           IQueryable<profile> myQuery = default(IQueryable<profile>);
           myQuery = this._datingcontext.profiles.Where(p => p.id == profileid);


           if (myQuery.Count() > 0)
           {
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
           logger = new ErroLogging(applicationEnum.MemberService);
           logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
           //log error mesasge
           //handle logging here
           var message = ex.Message;
           throw;
       }
   }
    
     public bool checkifusernamealreadyexists(string strusername)
    {
        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.username == strusername);

            if (myQuery.Count() > 0)
            {
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
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null,null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }
          
    public string validatesecurityansweriscorrect(int intprofileid ,int SecurityQuestionID,string strSecurityAnswer )
    {
        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.id == intprofileid && p.securityanswer == strSecurityAnswer && p.securityquestion.id == SecurityQuestionID);

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
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }

    /// <summary>
    /// Determines wethare an activation code matches the value in the database for a given profileid
    /// </summary>
    public bool checkifactivationcodeisvalid(int intprofileid, string strActivationCode)
    {

        IQueryable<profile> myQuery = default(IQueryable<profile>);
        try
        {
            //Dim ctx As New Entities()
            myQuery = this._datingcontext.profiles.Where(p => p.activationcode == strActivationCode & p.id == intprofileid);

            //End If
            if (myQuery.Count() > 0)
            {
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
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, intprofileid , null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
        // Return CInt(myQuery.First.screenname)
    }
       
                  
    public profile getprofilebyusername(string username)
    {


        IQueryable<profile> tmpprofile = default(IQueryable<profile>);
        //Dim ctx As New Entities()
        try
        {
            tmpprofile = this._datingcontext.profiles.Include("profiledata")
                .Where(p => p.username == username);
            //End If
            if (tmpprofile.Count() > 0)
            {
                return tmpprofile.FirstOrDefault();
            }
            else
            {
                return null;

            }
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }


    public profile getprofilebyscreenname(string username)
    {


        IQueryable<profile> tmpprofile = default(IQueryable<profile>);
        //Dim ctx As New Entities()

        try
        {
            tmpprofile = this._datingcontext.profiles.Include("profiledata")
            .Where(p => p.username == username);
            //End If
            if (tmpprofile.Count() > 0)
            {
                return tmpprofile.FirstOrDefault();
            }
            else
            {
                return null;

            }
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }

    public int getProfileIdbyusername(string User)
    {
        try
        {
            return (from p in _datingcontext.profiles
                    where p.username == User
                    select p.id).FirstOrDefault();
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }


    public profile getprofilebyprofileid(int profileid)
    {
        try
        {
            return (from p in _datingcontext.profiles
                    where p.id == profileid
                    select p).FirstOrDefault();
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }


       /// <summary>
       /// added ability to grab from appfabric cache
       /// </summary>
       /// <param name="strusername"></param>
       /// <returns></returns>
    public int? getprofileidbyusername(string strusername)
    {
        try
        {
            //IQueryable<profile> myQuery = default(IQueryable<profile>);
            //return this._datingcontext.profiles.Where(p => p.username  == strusername ).FirstOrDefault().id;
            return CachingFactory.getprofileidbyusername(strusername, this._datingcontext);
            //if (myQuery.Count() > 0)
            //{
            //    return myQuery.FirstOrDefault().id.ToString();
            //}
            //else
            //{
            //    return "";
            //}
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }
                 
    public int? getprofileidbyscreenname(string screenname)
    {
        try
        {
            return CachingFactory.getprofileidbyscreenname(screenname, this._datingcontext);
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService );
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex,  null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }

    public int? getprofileidbyssessionid(string strsessionid)
    {
        try
        {
            return CachingFactory.getprofileidbysessionid(strsessionid);
        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }
   
    public string getusernamebyprofileid(int profileid)
    {
        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.id == profileid);

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
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }
   
    public string getscreennamebyprofileid(int profileid)
    {
        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.id == profileid);

            if (myQuery.Count() > 0)
            {
                return myQuery.FirstOrDefault().screenname.ToString();
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
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }
 
    public string getscreennamebyusername(string username)
    {
        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.username == username);

            if (myQuery.Count() > 0)
            {
                return myQuery.FirstOrDefault().screenname.ToString();
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
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }

     public bool checkifemailalreadyexists(string strEmail)
    {

        try
        {
            IQueryable<profile> myQuery = default(IQueryable<profile>);
            //Dim ctx As New Entities()


            myQuery = this._datingcontext.profiles.Where(p => p.emailaddress == strEmail);

            if (myQuery.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;

            }

            // Return CInt(myQuery.First.screenname)

        }
        catch (Exception ex)
        {
            //instantiate logger here so it does not break anything else.
            logger = new ErroLogging(applicationEnum.MemberService);
            logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
            //log error mesasge
            //handle logging here
            var message = ex.Message;
            throw;
        }
    }

   
          
         
        public string getgenderbyscreenname(string screenname)
        {

            try
            {
                if (screenname == null) return null;

                var profile = (from p in _datingcontext.profiles
                               where p.screenname == screenname
                               join f in _datingcontext.profiledata on p.id equals f.profile_id
                               select f.gender).FirstOrDefault();

                return profile != null ? profile.description : null;
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.MemberService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }

        }
       
        public visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid)
        {
            try
            {
                return (from p in _datingcontext.visibilitysettings
                        where p.profile_id == profileid
                        select p).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                logger = new ErroLogging(applicationEnum.MemberService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid , null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }
        }
       
     
       

     

     
    }
}
