
namespace Dating.Server.Data.Services
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;
    using System.Security.Principal;

    
  

    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Web;

    



    //Implements application logic using the AnewluvFTSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    //<RequiresAuthentication> _



        public partial class DatingService : LinqToEntitiesDomainService<AnewLuvFTSEntities>
         {


             //Auth stuff, dunno if its doing anything now but we can use it to secure queries 
             private IPrincipal _User;
             public override void Initialize(DomainServiceContext context)
             {
                 base.Initialize(context);
                 // Debug.WriteLine(context.User.Identity.Name);
                 _User = context.User;
             }



             #region "Initial Profile STuff"
             [Invoke()]
             public profile GetProfileByUsername(string username)
             {


                 IQueryable<profile> tmpprofile = default(IQueryable<profile>);
                 //Dim ctx As New Entities()
                 tmpprofile = this.ObjectContext.profiles.Include("ProfileData")
                     .Where(p => p.UserName == username);
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


             //modifed to get search settings as well 
             //get full profile stuff
            //4-28-2012 added profile visibility settings
             [Invoke()]
             public ProfileData GetProfileDataByProfileID(string profileid)
             {
                 ////attempt to load the search settings as well
                 //var items = from i in this.ObjectContext.ProfileDatas.Include("Searchsettings").Include("Profile")
                 //            where (i.ProfileID == profileid) && (i.SearchSettings.Any(t => t.MyPerfectMatch == true))     
                 //   select i;

                 //var PerfectMatchSearchSettings = GetPerFectMatchSearchSettingsByProfileID(profileid);

                 //if (items.Count() > 0)
                 //{
                 //  return items.FirstOrDefault();
                 //}
                 //else
                 //{
                 //  items = from i in this.ObjectContext.ProfileDatas
                 //            where (i.ProfileID == profileid)         
                 //  select i;
                 //  return items.FirstOrDefault();
                 //}

                 var items = from i in this.ObjectContext.ProfileDatas
                                 .Include("Profile")
                                 .Include("ProfileVisiblitySetting")
                                 .Include("ProfileData_Ethnicity.CriteriaAppearance_Ethnicity")
                                 .Include("ProfileData_Hobby.CriteriaCharacter_Hobby")
                                 .Include("ProfileData_LookingFor.CriteriaLife_LookingFor")
                                 .Include("ProfileData_HotFeature.CriteriaCharacter_HotFeature")
                                 .Include("CriteriaAppearance_Bodytypes")
                                 .Include("CriteriaAppearance_EyeColor").Include("CriteriaAppearance_HairColor")
                                 .Include("CriteriaCharacter_Diet").Include("CriteriaCharacter_Drinks")
                                 .Include("CriteriaCharacter_Exercise").Include("CriteriaCharacter_Humor")
                                 .Include("CriteriaCharacter_PoliticalView").Include("CriteriaCharacter_Religion")
                                 .Include("CriteriaCharacter_ReligiousAttendance").Include("CriteriaCharacter_Sign")
                                 .Include("CriteriaCharacter_Smokes").Include("CriteriaLife_EducationLevel")
                                 .Include("CriteriaLife_EmploymentStatus").Include("CriteriaLife_HaveKids")
                                 .Include("CriteriaLife_IncomeLevel").Include("CriteriaLife_LivingSituation")
                                 .Include("CriteriaLife_MaritalStatus").Include("CriteriaLife_Profession")
                                 .Include("CriteriaLife_WantsKids")
                             where (i.ProfileID == profileid)
                             select i;


                 //now filter the search settings enity and only pull down the one that has the 
                 //  get perfect match settings and add it 


                 if (items.Count() > 0)
                 {

                     items.FirstOrDefault().SearchSettings.Add(GetPerFectMatchSearchSettingsByProfileID(profileid));

                     return items.FirstOrDefault();

                 }

                 //return nothing if bad
                 return null;




             }

             public SearchSetting GetPerFectMatchSearchSettingsByProfileID(string profileid)
             {
                 IQueryable<SearchSetting> tmpSearchSettings = default(IQueryable<SearchSetting>);
                 //Dim ctx As New Entities()
                 tmpSearchSettings = this.ObjectContext.SearchSettings
                     // .Include("ProfileData1")
                 .Include("SearchSettings_Genders")
                  .Include("SearchSettings_BodyTypes")
                   .Include("SearchSettings_Diet")
                    .Include("SearchSettings_Drinks")
                  .Include("SearchSettings_EducationLevel")
                  .Include("SearchSettings_EmploymentStatus")
                    .Include("SearchSettings_Ethnicity")
                   .Include("SearchSettings_Exercise")
                    .Include(" SearchSettings_EyeColor")
                    .Include("SearchSettings_HairColor")
                    .Include("SearchSettings_HaveKids")
                   .Include("SearchSettings_Hobby")
                    .Include("SearchSettings_HotFeature")
                   .Include("SearchSettings_Humor")
                   .Include("SearchSettings_IncomeLevel")
                   .Include("SearchSettings_LivingStituation")
                   .Include("SearchSettings_Location")
                   .Include("SearchSettings_LookingFor")
                    .Include("SearchSettings_MaritalStatus")
                   .Include("SearchSettings_PoliticalView")
                   .Include("SearchSettings_Profession")
                   .Include(" SearchSettings_Religion")
                   .Include("SearchSettings_ReligiousAttendance")
                    .Include("SearchSettings_ShowMe")
                    .Include("SearchSettings_Sign")
                   .Include("SearchSettings_Smokes")
                    .Include("SearchSettings_SortByType")
                   .Include("SearchSettings_WantKids")
                   .Include("SearchSettings_Tribe")



                 .Where(p => p.ProfileID == profileid && p.MyPerfectMatch == true);

                 //End If
                 if (tmpSearchSettings.Count() > 0)
                 {
                     return tmpSearchSettings.FirstOrDefault();
                 }
                 else
                 {
                     //get the profileDta                    

                     SearchSetting NewSearchSettings = new SearchSetting();

                     NewSearchSettings = new SearchSetting();
                     NewSearchSettings.ProfileID = profileid;
                     NewSearchSettings.MyPerfectMatch = true;
                     NewSearchSettings.SearchName = "MyPerfectMatch";
                     //NewSearchSettings.ProfileData = this.GetProfileDataByProfileID(profileid);
                    //  this.ObjectContext.SearchSettings.AddObject(NewSearchSettings);
                    //  this.ObjectContext.SaveChanges();
                     

                     //save the profile data with the search settings to the database so we dont have to create it again
                     return NewSearchSettings;



                 }
             }

             public SearchSetting CreateMyPerFectMatchSearchSettingsByProfileID(string profileid)
             {
                 

                 
                     //get the profileDta                    

                     SearchSetting NewSearchSettings = new SearchSetting();

                     NewSearchSettings = new SearchSetting();
                     NewSearchSettings.ProfileID = profileid;
                     NewSearchSettings.MyPerfectMatch = true;
                     NewSearchSettings.SearchName = "MyPerfectMatch";
                     //NewSearchSettings.ProfileData = this.GetProfileDataByProfileID(profileid);
                       this.ObjectContext.SearchSettings.AddObject(NewSearchSettings);
                       this.ObjectContext.SaveChanges();


                     //save the profile data with the search settings to the database so we dont have to create it again
                     return NewSearchSettings;



                 
             }
             #endregion
             //get full profile stuff
   

             [Invoke()]
             public string  GetGenderByScreenName(string strScreenName)
             {
                 if (strScreenName == null) return null;

                 gender _gender = new gender();
               
               
              _gender   = (from x in (ObjectContext.profiles.Where(p => p.ScreenName  == strScreenName  ))
                           join f in ObjectContext.ProfileDatas on x.ProfileID  equals f.ProfileID 
                           select f.gender  ).FirstOrDefault();


              return _gender.GenderName;
             }

             [Invoke()]
             public string GetGenderByPhotoId(Guid guid)
             {
                 gender _gender = new gender();
                

                 _gender = (from x in (ObjectContext.photos.Where(f=>f.PhotoID == guid))
                            join f in ObjectContext.ProfileDatas on x.ProfileID equals f.ProfileID 
                            select f.gender).FirstOrDefault();


                 return _gender.GenderName;
             }


            //TO DO this needs to be  linked to roles
             #region "Message and Email Quota stuff"
             // Description:	Updates the users logout time
             // added 1/18/2010 ola lawal
             public bool CheckIfQuoutaReachedAndUpdate(string profileID)
             {

                 //get the profile
                 //profile myProfile;
               profile  myProfile = new profile();
               DateTime currenttime = DateTime.Now;
               bool QuotaHit = false ;

                 //get the profileID from userID
                 //string profileID = GetProfileIdbyUsername(username);

                 try
                 {
                  //get the profile
                     myProfile = this.ObjectContext.profiles.Where(p => p.ProfileID == profileID).FirstOrDefault();

                     //update all other sessions that were not properly logged out
                    // myProfile = ;

                    // foreach (User_Logtime p in myQuery)
                    //'{
                    //     p.LogoutTime = currenttime;
                    // }
                    
                     //check if the user hit the count before updating that
                     int EmailDailyQuota = myProfile.DailySentEmailQuota ?? 0;                      
                     int EmailQuotaLimitWithNoRoleCheck = this.ObjectContext.CommunicationQuotas.Where(p => p.QuotaID == 1).FirstOrDefault().QuotaValue ?? 0;
                     //TO DO check qoute for correct role down the line
                     if (EmailDailyQuota !=0 &&  EmailDailyQuota >= EmailQuotaLimitWithNoRoleCheck)
                     {
                         myProfile.SentEmailQuotaHitCount = myProfile.SentEmailQuotaHitCount == null ? 1 : myProfile.SentEmailQuotaHitCount + 1;
                     QuotaHit = true;
                     }
                     // update the count
                     myProfile.DailySentEmailQuota = myProfile.DailySentEmailQuota == null ? 1 : myProfile.DailySentEmailQuota + 1;
                     ObjectContext.SaveChanges();

                     
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                     // log the execption message
                    // return false;
                 }

                 return QuotaHit;
             }

             #endregion


             #region "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
             //update the database i.e create folders and change profile status from guest to active ?!
                public bool CreateMailBoxFolders(string strProfileID)
                {
                   
                    int max = 5;
                    int i = 1;
                    try{
                    
                    for(i = 1; i < max;i++){
                   MailboxFolder   p = new MailboxFolder();
                    p.MailboxFolderTypeID  = i;
                    p.ProfileID = strProfileID;
                    p.Active  = 1;
                          //determin what the folder type is , we have inbox=1 , sent=2, Draft=3,Trash=4,Deleted=5
                      switch(i)       
                      {         
                         case 1:
                              p.MailboxFolderTypeName = "Inbox";
                              break;           
                         case 2:
                            p.MailboxFolderTypeName = "Sent";
                            break;     
                         case 3:
                            p.MailboxFolderTypeName = "Drafts";
                            break; 
                          case 4:
                            p.MailboxFolderTypeName = "Trash";
                            break; 
                          case 5:
                            p.MailboxFolderTypeName = "Deleted";
                            break; 
                       }
                      this.ObjectContext.MailboxFolders.AddObject(p);
                      this.ObjectContext.SaveChanges();
                     // db.AddToproducts(p);
                     // db.SaveChanges();
  
                    }
                
                    }
                    catch {
                        return false;   
                        //log error here
                    }

                    return true;
                }

                public bool ActivateProfile(string strProfileID)
                {
                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile  = this.ObjectContext.profiles.Where(p => p.ProfileID == strProfileID).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.ProfileStatusID = 2;
                        //handele the update using EF
                       // this.ObjectContext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this.ObjectContext.SaveChanges();

                    }
                    catch(Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;
                    }
                 
                    return true;
                }




                public bool DeActivateProfile(string strProfileID)
                {
                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile = this.ObjectContext.profiles.Where(p => p.ProfileID == strProfileID).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.ProfileStatusID = 4;
                        //handele the update using EF
                        // this.ObjectContext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this.ObjectContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;
                    }

                    return true;
                }
                //updates the profile with a password that is presumed to be already encyrpted
                public bool UpdatePassword(string profileID, string encryptedpassword)
                {

                    //get the profile
                    //profile myProfile;
                    profile myProfile = new profile();

                    try
                    {
                        myProfile = this.ObjectContext.profiles.Where(p => p.ProfileID == profileID).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.Password = encryptedpassword;
                        myProfile.ModificationDate = DateTime.Now;
                        myProfile.PasswordChangedDate = DateTime.Now;
                        myProfile.PasswordChangedCount = (myProfile.PasswordChangedCount == null) ? 1 : myProfile.PasswordChangedCount + 1;
                        //handele the update using EF
                        // this.ObjectContext.profiles.AttachAsModified(myProfile, this.ChangeSet.GetOriginal(myProfile));
                        this.ObjectContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;

                        // log the execption message
                       //return false;
                    }

                    return true;
                }

                public bool AddNewOpenIDForProfile(string profileID,string openidIdentifer, string openidProvidername)
                {

                  
                    try
                    {
                         var profileOpenIDStore = new profileOpenIDStore
                     {
                           active = true,
                           creationDate = DateTime.UtcNow ,
                           ProfileID = profileID,
                           openidProviderName = openidProvidername ,
                           openidIdentifier = openidIdentifer
                        };
                     this.ObjectContext.profileOpenIDStores.AddObject(profileOpenIDStore);
                     this.ObjectContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;

                        // log the execption message
                        //return false;
                    }

                    return true;
                }


                //check if profile is activated 
                public bool CheckIfProfileisActivated(string strProfileID)
                {

                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID  == strProfileID & p.ProfileStatusID != 1);


                    if (myQuery.Count() > 0)
                    {
                        return true;
                    }
                         else
                    {
                        return false;

                    }

                   
                }

                //check if mailbox folder exist
                public bool CheckIfMailBoxFoldersAreCreated(string strProfileID)
                {

                    MailboxFolder myQuery;
                    myQuery = this.ObjectContext.MailboxFolders.Where(p => p.ProfileID  == strProfileID).FirstOrDefault();


                    if (myQuery != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

                #endregion

             #region "DateTimeFUcntiosn for longin etc "
                // Description:	Updates the users logout time
                // added 1/18/2010 ola lawal
                public bool UpdateUserLogoutTime(string profileID, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<User_Logtime> myQuery = default(IQueryable<User_Logtime>);
                    DateTime currenttime = DateTime.Now;

                    //get the profileID from userID
                    //string profileID = GetProfileIdbyUsername(username);

                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this.ObjectContext.User_Logtime.Where(p => p.ProfileID == profileID && p.Offline == 0 && p.SessionID == sessionID);

                        foreach (User_Logtime p in myQuery)
                        {
                            p.LogoutTime = currenttime;
                        }

                        ObjectContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;
                    }

                    return true;
                }


                //get the last time the user logged in from profile
                public Nullable<DateTime> GetMemberLastLoginTime(string profileid)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    // DateTime currenttime = DateTime.Now;                   
                    try
                    {

                        myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID == profileid);
                        if (myQuery.Count() > 0)
                        {
                            return myQuery.FirstOrDefault().LoginDate;
                        }
                        else
                        {
                            return null;
                        }                    
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                        // log the execption message
                        //return false;
                       // return null;
                    }



                }


                //updates all the areas  that handle when a user logs in 
                // added 1/18/2010 ola lawal
                //also updates the last log in and profile data
                public bool UpdateUserLoginTime(string username, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<User_Logtime> myQuery = default(IQueryable<User_Logtime>);
                    profile myProfile = new profile();
                    User_Logtime myLogtime = new User_Logtime();
                    DateTime currenttime = DateTime.Now;

                    //get the profileID from userID
                    string profileID = GetProfileIdbyUsername(username);
                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this.ObjectContext.User_Logtime.Where(p => p.ProfileID == profileID && p.Offline == 0);

                        foreach (User_Logtime p in myQuery)
                        {
                            p.Offline = 1;

                        }

                        //aloso update the profile table with current login date
                        myProfile = this.ObjectContext.profiles.Where(p => p.ProfileID == profileID).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.LoginDate = currenttime;


                        //noew aslo update the logtime and then 
                        myLogtime.ProfileID = profileID;
                        myLogtime.SessionID = sessionID;
                        myLogtime.LoginTime = currenttime;
                        this.ObjectContext.User_Logtime.AddObject(myLogtime);
                        //save all changes bro
                        this.ObjectContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;

                        // log the execption message
                      //  return false;
                    }

                    return true;
                }

                public bool UpdateUserLoginTimeByProfileID(string ProfileID, string sessionID)
                {

                    //get the profile
                    //profile myProfile;
                    IQueryable<User_Logtime> myQuery = default(IQueryable<User_Logtime>);
                    profile myProfile = new profile();
                    User_Logtime myLogtime = new User_Logtime();
                    DateTime currenttime = DateTime.Now;

                    //get the profileID from userID
                    string profileID = ProfileID;//GetProfileIdbyUsername(username);
                    try
                    {
                        //update all other sessions that were not properly logged out
                        myQuery = this.ObjectContext.User_Logtime.Where(p => p.ProfileID == profileID && p.Offline == 0);

                        foreach (User_Logtime p in myQuery)
                        {
                            p.Offline = 1;

                        }

                        //aloso update the profile table with current login date
                        myProfile = this.ObjectContext.profiles.Where(p => p.ProfileID == profileID).FirstOrDefault();
                        //update the profile status to 2
                        myProfile.LoginDate = currenttime;


                        //noew aslo update the logtime and then 
                        myLogtime.ProfileID = profileID;
                        myLogtime.SessionID = sessionID;
                        myLogtime.LoginTime = currenttime;
                        this.ObjectContext.User_Logtime.AddObject(myLogtime);
                        //save all changes bro
                        this.ObjectContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;

                        // log the execption message
                        //  return false;
                    }

                    return true;
                }

                //date time functions '
                //***********************************************************
                //this function will send back when the member last logged in
                //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
                //Ola Lawal 7/10/2009 feel free to drill down even to the day
            
                public string GetLastLoggedInString( DateTime  LoginDate)
                {


                    if (LoginDate == null | string.IsNullOrEmpty(Convert.ToString(LoginDate)))
                    {
                        return "Three Weeks Ago";
                    }

                    //'you can compare dates and times the same as you would any other number

                    
                    DateTime DateThreeDaysAgo;
                    DateTime DateThreeWeeksago; 
                    DateTime DateOneWeekAgo ;
                    DateTime DateThreeMonthsAgo; 
                    DateTime DateSixMonthsAgo;                   
                    DateTime DateOneMonthAgo;

                   DateTime  Today = DateTime.Now;
                    

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

                    if (LoginDate > DateThreeDaysAgo)
                    {
                        return "Last Three Days";
                    }
                    else if (LoginDate > DateOneWeekAgo)
                    {
                        return "Last Week ";
                    }
                    else if (LoginDate > DateThreeWeeksago)
                    {
                        return "Three Weeks Ago";
                    }
                    else if (LoginDate > DateOneMonthAgo )
                    {
                        return "One Month Ago";
                    }
                   
                    else if (LoginDate > DateThreeMonthsAgo)
                    {
                        return "Three Months Ago ";
                    }
                    else if (LoginDate > DateSixMonthsAgo)
                    {
                       return "Six Months Ago";
                   }
                    else
                   {
                       return "Over one Month";
                   }


                }

                //returns true if somone logged on
                public bool GetUserOnlineStatus(string profileid)
                {
                    //get the profile
                    //profile myProfile;
                    IQueryable<User_Logtime> myQuery = default(IQueryable<User_Logtime>);

                    myQuery = ObjectContext.User_Logtime.Where(p => p.ProfileID == profileid && p.Offline == 0).Distinct().OrderBy(n => n.LoginTime);

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
                 

                #endregion

                //other standard verifcation methods added here


                /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>
   [Invoke()] 
   public bool CheckIfScreenNameAlreadyExists(string strScreenName)
        {
            
	        IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this.ObjectContext.profiles.Where(p => p.ScreenName == strScreenName | p.ScreenName.Replace(" ", "") == strScreenName.Replace(" ", "") | p.ScreenName.Replace(" ", "") == strScreenName );
           

	        if (myQuery.Count() > 0) {
		        return true;
	        } else {
		        return false;

	        }
        }



   //5-20-2012 added to check if a user email is registered
   [Invoke()]
   public bool CheckIfProfileIDAlreadyExists(string profileID)
   {

       IQueryable<profile> myQuery = default(IQueryable<profile>);
       myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID == profileID);


       if (myQuery.Count() > 0)
       {
           return true;
       }
       else
       {
           return false;

       }
   }

    [Invoke()] 
     public bool CheckIfUserNameAlreadyExists(string strUserName)
    {
	    IQueryable<profile> myQuery = default(IQueryable<profile>);
	    myQuery =  this.ObjectContext.profiles.Where(p=> p.UserName == strUserName);

	    if (myQuery.Count() > 0) {
		    return true;
	    } else {
		    return false;
	    }
    }

    [Invoke()]
    public string ValidateSecurityAnswerIsCorrect(string strProfileID ,int SecurityQuestionID,string strSecurityAnswer )
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID  ==  strProfileID && p.SecurityAnswer  == strSecurityAnswer && p.SecurityQuestionID == SecurityQuestionID  );
        
        if ( myQuery.Count()>0)
        {
            return myQuery.FirstOrDefault().UserName.ToString();
            
        }
        else
        {
            return "";
        }
    }


    [Invoke()]
    public string GetProfileIdbyUsername(string strusername)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.UserName  == strusername );

        if (myQuery.Count() > 0)
        {
            return myQuery.FirstOrDefault().ProfileID.ToString();
        }
        else
        {
            return "";
        }
    }

    [Invoke()]
    public string GetProfileIdbyScreenName(string strscreenname)
    {
        
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.ScreenName  == strscreenname);

        try
        {


            if (myQuery.Count() > 0)
            {
                return myQuery.FirstOrDefault().ProfileID.ToString();
            }
            else
            {
                return "";
            }
        }
        catch
        {
                    
        }

        return "";
    }

    [Invoke()]
    public string GetUserNamebyProfileID(string profileid)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID  == profileid );

        if (myQuery.Count() > 0)
        {
            return myQuery.FirstOrDefault().UserName.ToString();
        }
        else
        {
            return "";
        }
    }

    [Invoke()]
    public string GetScreenNamebyProfileID(string profileid)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.ProfileID == profileid);

        if (myQuery.Count() > 0)
        {
            return myQuery.FirstOrDefault().ScreenName.ToString();
        }
        else
        {
            return "";
        }
    }

  [Invoke()]
    public string GetScreenNamebyUserName(string username)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this.ObjectContext.profiles.Where(p => p.UserName == username);

        if (myQuery.Count() > 0)
        {
            return myQuery.FirstOrDefault().ScreenName.ToString();
        }
        else
        {
            return "";
        }
    }




    [Invoke()] 
     public bool CheckIfEmailAlreadyExists(string strEmail)
    {
	    IQueryable<profile> myQuery = default(IQueryable<profile>);
	    //Dim ctx As New Entities()

	
	    myQuery =  this.ObjectContext.profiles.Where (p=> p.ProfileID == strEmail);
	
	    if (myQuery.Count() > 0) {
		    return true;
	    } else {
		    return false;

	    }

	    // Return CInt(myQuery.First.ScreenName)
    }


    // added by Deshola on 5/17/2011
    [Invoke()]
    public byte[] GetGalleryPhotoByPhotoID(Guid strPhotoID)
    {
        IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
        //Dim ctx As New Entities()
        GalleryPhoto = this.ObjectContext.photos.Where(p => p.PhotoID == strPhotoID);

        //End If
        if (GalleryPhoto.Count() > 0)
        {
            return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();
        }
        else
        {
            return null;

        }


        // Return CInt(myQuery.First.ScreenName)

    }


    [Invoke()]
    public byte[] GetGalleryPhotoByProfileID(string strProfileID)
    {
        IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
        //Dim ctx As New Entities()
        GalleryPhoto = this.ObjectContext.photos.Where(p => p.ProfileID == strProfileID & p.Aproved == "Yes" & p.ProfileImageType == "Gallery");

        //End If
        if (GalleryPhoto.Count() > 0)
        {
            return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();
        }
        else
        {
            return null;

        }
    }

    [Invoke()] 
    public byte[] GetGalleryPhotoByScreenName(string strScreenName)
    {
	    IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
	    //Dim ctx As New Entities()

        // string strProfileID = this.GetProfileIdbyScreenName(strScreenName);
        GalleryPhoto = (from p in ObjectContext.profiles.Where(p => p.ScreenName == strScreenName )
                        join f in ObjectContext.photos on p.ProfileID equals f.ProfileID
                        where (f.Aproved == "Yes" & f.ProfileImageType == "Gallery")
                        select f);
	   
	 try
        {
          //End If
	    if (GalleryPhoto.Count() > 0) return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();
	    
        }
        catch
        { 
        
        }
        return null;
	    


	// Return CInt(myQuery.First.ScreenName)

}

    [Invoke()]
    public byte[] GetGalleryImageByNormalizedScreenName(string strScreenName)
    {
        IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
        //Dim ctx As New Entities()
        //

       // string strProfileID = this.GetProfileIdbyScreenName(strScreenName);
        GalleryPhoto = (from p in ObjectContext.profiles.Where(p => p.ScreenName.Replace(" ", "") == strScreenName)
                        join f in ObjectContext.photos on p.ProfileID equals f.ProfileID
                        where (f.Aproved == "Yes" & f.ProfileImageType == "Gallery")
                        select f);
                

        try
        {
            //End If
            if (GalleryPhoto.Count() > 0) return GalleryPhoto.FirstOrDefault().ProfileImage.ToArray();

        }
        catch
        {

        }
        return null;



        // Return CInt(myQuery.First.ScreenName)

    }

    [Invoke()]
    public bool InsertPhotoCustom(photo newphoto)
    {

        try
        {
            this.ObjectContext.photos.AddObject(newphoto);

            this.ObjectContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

     [Invoke()]
    public bool CheckIfPhotoCaptionAlreadyExists( string strProfileID,string strPhotoCaption)
    {
        IQueryable<photo> myPhotoList = default(IQueryable<photo>);
        //Dim ctx As New Entities()


        myPhotoList = this.ObjectContext.photos.Where(p => p.ProfileID == strProfileID && p.ImageCaption == strPhotoCaption);

        if (myPhotoList.Count() > 0)
        {
            return true;
        }
        else
        {
            return false;

        }

        // Return CInt(myQuery.First.ScreenName)
    }

    /// <summary>
    /// Determines wethare an activation code matches the value in the database for a given profileID
    /// </summary>
    public bool CheckIfActivationCodeIsValid(string strProfileID, string strActivationCode)
{
	IQueryable<profile> myQuery = default(IQueryable<profile>);
	//Dim ctx As New Entities()
	myQuery = this.ObjectContext.profiles.Where (p=> p.ActivationCode == strActivationCode & p.ProfileID == strProfileID);

	//End If
	if (myQuery.Count() > 0) {
		return true;
	} else {
		return false;

	}

	// Return CInt(myQuery.First.ScreenName)
}
            
    public bool CheckForGalleryPhotoByProfileID(string strProfileID)
{
	IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
	//Dim ctx As New Entities()
	GalleryPhoto = this.ObjectContext.photos.Where(p=> p.ProfileID == strProfileID & p.Aproved == "Yes" & p.ProfileImageType == "Gallery");

	if (GalleryPhoto.Count() > 0) {
		return true;
	} else {
		return false;

	}
	// Return CInt(myQuery.First.ScreenName)
}

    public bool CheckForUploadedPhotoByProfileID(string strProfileID)
    {
        IQueryable<photo> GalleryPhoto = default(IQueryable<photo>);
        //Dim ctx As New Entities()
        GalleryPhoto = this.ObjectContext.photos.Where(p => p.ProfileID == strProfileID );

        if (GalleryPhoto.Count() > 0)
        {
            return true;
        }
        else
        {
            return false;

        }
        // Return CInt(myQuery.First.ScreenName)
    }

       
 }

}
