using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Data
{
   public  class MemberRepository : MemberRepositoryBase , IMemberRepository 
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();
   
       
      

       public MemberRepository(AnewluvContext datingcontext) 
           :base(datingcontext)
       {
          
       }

       
             //Auth stuff, dunno if its doing anything now but we can use it to secure queries 
             //private IPrincipal _User;
             //public override void Initialize(DomainServiceContext context)
             //{
             //    base.Initialize(context);
             //    // Debug.WriteLine(context.User.Identity.Name);
             //    _User = context.User;
             //}



            //"Initial Profile STuff"
       //*********************************************************************
            
           

             //modifed to get search settings as well 
             //get full profile stuff
            //4-28-2012 added profile visibility settings
            
             public profiledata getprofiledatabyprofileid(int profileid)
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
                             where (i.id == profileid)
                             select i;


                 //now filter the search settings enity and only pull down the one that has the 
                 //  get perfect match settings and add it 


                 if (items.Count() > 0)
                 {
                 

                     items.FirstOrDefault().profilemetadata.searchsettings.Add(this.getperfectmatchsearchsettingsbyprofileid (profileid));

                     return items.FirstOrDefault();

                 }

                 //return nothing if bad
                 return null;




             }

             public searchsetting  getperfectmatchsearchsettingsbyprofileid(int profileid)
             {
                 IQueryable<searchsetting> tmpsearchsettings = default(IQueryable<searchsetting>);
                 //Dim ctx As New Entities()
                 tmpsearchsettings = this._datingcontext.searchsetting
                     // .Include("ProfileData1")
                 .Include("searchsettings_Genders")
                  .Include("searchsettings_BodyTypes")
                   .Include("searchsettings_Diet")
                    .Include("searchsettings_Drinks")
                  .Include("searchsettings_EducationLevel")
                  .Include("searchsettings_EmploymentStatus")
                    .Include("searchsettings_Ethnicity")
                   .Include("searchsettings_Exercise")
                    .Include(" searchsettings_EyeColor")
                    .Include("searchsettings_HairColor")
                    .Include("searchsettings_HaveKids")
                   .Include("searchsettings_Hobby")
                    .Include("searchsettings_HotFeature")
                   .Include("searchsettings_Humor")
                   .Include("searchsettings_IncomeLevel")
                   .Include("searchsettings_LivingStituation")
                   .Include("searchsettings_Location")
                   .Include("searchsettings_LookingFor")
                    .Include("searchsettings_MaritalStatus")
                   .Include("searchsettings_PoliticalView")
                   .Include("searchsettings_Profession")
                   .Include(" searchsettings_Religion")
                   .Include("searchsettings_ReligiousAttendance")
                    .Include("searchsettings_ShowMe")
                    .Include("searchsettings_Sign")
                   .Include("searchsettings_Smokes")
                    .Include("searchsettings_SortByType")
                   .Include("searchsettings_WantKids")
                   .Include("searchsettings_Tribe")



                 .Where(p => p.profile_id    == profileid && p.myperfectmatch  == true);

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
                     Newsearchsettings.profile_id  = profileid;
                     Newsearchsettings.myperfectmatch  = true;
                     Newsearchsettings.searchname  = "myperfectmatch";
                     //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                    //  this._datingcontext.searchsettings.Add(Newsearchsettings);
                    //  this._datingcontext.SaveChanges();
                     

                     //save the profile data with the search settings to the database so we dont have to create it again
                     return Newsearchsettings;



                 }
             }

             public searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid)
             {
                 

                 
                     //get the profileDta                    

                     searchsetting Newsearchsettings = new searchsetting();

                     Newsearchsettings = new searchsetting();
                     Newsearchsettings.profile_id  = profileid;
                     Newsearchsettings.myperfectmatch  = true;
                     Newsearchsettings.searchname = "myperfectmatch";
                     //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                       this._datingcontext.searchsetting.Add (Newsearchsettings);
                       this._datingcontext.SaveChanges();


                     //save the profile data with the search settings to the database so we dont have to create it again
                     return Newsearchsettings;



                 
             }
   
             //get full profile stuff
       //*****************************************************
   

            
           
            
             public string getgenderbyphotoid(Guid guid)
             {
                 lu_gender  _gender = new lu_gender ();
                

                 _gender = (from x in (_datingcontext.photos.Where(f=>f.id  == guid))
                            join f in _datingcontext.profiledata on x.profile_id  equals f.id   
                            select f.gender).FirstOrDefault();


                 return _gender.description ;
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
                     throw ex;
                     // log the execption message
                    // return false;
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
                    try{
                    
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
                    catch(Exception ex)
                    {
                        throw ex;
                        // log the execption message
                        //return false;
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
                        throw ex;
                        // log the execption message
                        //return false;
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
                        throw ex;
                        // log the execption message
                        //return false;

                        // log the execption message
                       //return false;
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
                        openidprovidername   = openidProvidername ,
                           openididentifier   = openidIdentifer
                        };
                     this._datingcontext.opendIds.Add (profileOpenIDStore);
                     this._datingcontext.SaveChanges();

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
                public bool checkifprofileisactivated(int intprofileid)
                {

                    IQueryable<profile> myQuery = default(IQueryable<profile>);
                    myQuery = this._datingcontext.profiles.Where(p => p.id  == intprofileid & p.status.id  != 1);


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
                public bool checkifmailboxfoldersarecreated(int intprofileid)
                {

                     mailboxfolder   myQuery;
                    myQuery = this._datingcontext.mailboxfolders.Where(p => p.profiled_id   == intprofileid).FirstOrDefault();


                    if (myQuery != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
                        throw ex;
                        // log the execption message
                        //return false;
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

                        throw ex;
                        // log the execption message
                        //return false;
                       // return null;
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
                    int profileid = getprofileidbyusername(username);
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
                        myLogtime.profile_id  = profileid;
                        myLogtime.sessionid  = sessionID;
                        myLogtime.logintime  = currenttime;
                        this._datingcontext.userlogtimes.Add(myLogtime);
                        //save all changes bro
                        this._datingcontext.SaveChanges();

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
            
   public string getlastloggedinstring( DateTime  logindate)
                {


                    if (logindate == null | string.IsNullOrEmpty(Convert.ToString(logindate)))
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
                    else if (logindate > DateOneMonthAgo )
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

                //returns true if somone logged on
     public bool getuseronlinestatus(int profileid)
                {
                    //get the profile
                    //profile myProfile;
                    IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);

                    myQuery = _datingcontext.userlogtimes.Where(p => p.profile_id  == profileid && p.offline  == false).Distinct().OrderBy(n => n.logintime);

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
                 

          

       //other standard verifcation methods added here
       /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>
   
   public bool checkifscreennamealreadyexists(string strscreenname)
        {
            
	        IQueryable<profile> myQuery = default(IQueryable<profile>);
            myQuery = this._datingcontext.profiles.Where(p => p.screenname == strscreenname | p.screenname.Replace(" ", "") == strscreenname.Replace(" ", "") | p.screenname.Replace(" ", "") == strscreenname );
           

	        if (myQuery.Count() > 0) {
		        return true;
	        } else {
		        return false;

	        }
        }

   //5-20-2012 added to check if a user email is registered
  
   public bool checkifprofileidalreadyexists(int profileid)
   {

       IQueryable<profile> myQuery = default(IQueryable<profile>);
       myQuery = this._datingcontext.profiles.Where(p => p.id  == profileid);


       if (myQuery.Count() > 0)
       {
           return true;
       }
       else
       {
           return false;

       }
   }
    
     public bool checkifusernamealreadyexists(string strusername)
    {
	    IQueryable<profile> myQuery = default(IQueryable<profile>);
	    myQuery =  this._datingcontext.profiles.Where(p=> p.username == strusername);

	    if (myQuery.Count() > 0) {
		    return true;
	    } else {
		    return false;
	    }
    }
          
    public string validatesecurityansweriscorrect(int intprofileid ,int SecurityQuestionID,string strSecurityAnswer )
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this._datingcontext.profiles.Where(p => p.id   ==  intprofileid && p.securityanswer   == strSecurityAnswer && p.securityquestion.id   == SecurityQuestionID  );
        
        if ( myQuery.Count()>0)
        {
            return myQuery.FirstOrDefault().username.ToString();
            
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Determines wethare an activation code matches the value in the database for a given profileid
    /// </summary>
    public bool checkifactivationcodeisvalid(int intprofileid, string strActivationCode)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        //Dim ctx As New Entities()
        myQuery = this._datingcontext.profiles.Where(p => p.activationcode  == strActivationCode & p.id == intprofileid);

        //End If
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
       
                  
    public profile getprofilebyusername(string username)
    {


        IQueryable<profile> tmpprofile = default(IQueryable<profile>);
        //Dim ctx As New Entities()
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


    public profile getprofilebyscreenname(string username)
    {


        IQueryable<profile> tmpprofile = default(IQueryable<profile>);
        //Dim ctx As New Entities()
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

    public int getProfileIdbyusername(string User)
    {

        return (from p in db.profiles
                where p.username == User
                select p.id).FirstOrDefault();
    }


    public profile getprofilebyprofileid(int profileid)
    {

        return (from p in db.profiles
                where p.id  == profileid
                select p).FirstOrDefault();
    }


    public int getprofileidbyusername(string strusername)
    {
        //IQueryable<profile> myQuery = default(IQueryable<profile>);
       return this._datingcontext.profiles.Where(p => p.username  == strusername ).FirstOrDefault().id;

        //if (myQuery.Count() > 0)
        //{
        //    return myQuery.FirstOrDefault().id.ToString();
        //}
        //else
        //{
        //    return "";
        //}
    }
                 
    public int getprofileidbyscreenname(string strscreenname)
    {
        return (from p in db.profiles
                where p.screenname == strscreenname
                select p.id).FirstOrDefault();
    }

   
    public string getusernamebyprofileid(int profileid)
    {
        IQueryable<profile> myQuery = default(IQueryable<profile>);
        myQuery = this._datingcontext.profiles.Where(p => p.id  == profileid );

        if (myQuery.Count() > 0)
        {
            return myQuery.FirstOrDefault().username.ToString();
        }
        else
        {
            return "";
        }
    }
   
    public string getscreennamebyprofileid(int profileid)
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
 
    public string getscreennamebyusername(string username)
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

     public bool checkifemailalreadyexists(string strEmail)
    {
	    IQueryable<profile> myQuery = default(IQueryable<profile>);
	    //Dim ctx As New Entities()

	
	    myQuery =  this._datingcontext.profiles.Where (p=> p.emailaddress  == strEmail);
	
	    if (myQuery.Count() > 0) {
		    return true;
	    } else {
		    return false;

	    }

	    // Return CInt(myQuery.First.screenname)
    }

   

       

       //Start of stuff pulled from MVC members repository

    
         
        public string getgenderbyscreenname(string screenname)
        {
            if (screenname == null) return null;

            return (from p in db.profiles where p.screenname   == screenname 
                      join f in  db.profiledata on p.id equals f.id
                    select f.gender).FirstOrDefault().description ;
        }


    
       
        public visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid)
        {

            return (from p in db.visibilitysettings 
                    where p.profile_id  == profileid  select p).FirstOrDefault();
        }
       
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(MembersViewModel model)
        {

            //get search sttings from DB
            searchsetting perfectmatchsearchsettings = model.profiledata.profilemetadata.searchsettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.distancefromme   == null | perfectmatchsearchsettings.distancefromme   == 0)
                model.MaxDistanceFromMe  = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.agemax   != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.agemin  != null ? perfectmatchsearchsettings.agemin .GetValueOrDefault() : 18;
            //Height
            int intheightmin = perfectmatchsearchsettings.heightmin != null ? perfectmatchsearchsettings.heightmin.GetValueOrDefault() : 0;
            int intheightmax = perfectmatchsearchsettings.heightmax  != null ? perfectmatchsearchsettings.heightmax.GetValueOrDefault() : 100;
            bool blEvaluateHeights = intheightmin >0 ? true : false;
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.MyLongitude != "")?  Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLongitude  != "")?  Convert.ToDouble(model.MyLongitude ):0;
            //get the rest of the values if they are needed in calculations

         
            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);
            


            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues =(perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id .GetValueOrDefault())) : LookingForGenderValues;
            //Appearacnce seache settings values         

            //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?
         
            HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
            LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.bodytypes.Select(c => c.id.GetValueOrDefault())) : LookingForBodyTypesValues;

            HashSet<int> LookingForEthnicityValues = new HashSet<int>();
            LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.ethnicitys.Select(c => c.id.GetValueOrDefault())) : LookingForEthnicityValues;

            HashSet<int> LookingForEyeColorValues = new HashSet<int>();
            LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.eyecolors.Select(c => c.id.GetValueOrDefault())) : LookingForEyeColorValues;

            HashSet<int> LookingForHairColorValues = new HashSet<int>();
            LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.haircolors.Select(c => c.id.GetValueOrDefault())) : LookingForHairColorValues;

            HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
            LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.hotfeature.Select(c => c.id.GetValueOrDefault())) : LookingForHotFeatureValues;

        
            //******** visiblitysettings test code ************************
            
            // test all the values you are pulling here
            // var TestModel =   (from x in db.profiledata.Where(x => x.profile.username  == "case")
           //                      select x).FirstOrDefault();
          //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1));
           // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profiledata.GenderID) ? true : false;

            //  var testmodel2 = (from x in db.profiledata.Where(x => x.profile.username  == "case" &&  db.fnCheckIfBirthDateIsInRange(x.birthdate, 19, 20) == true  )
           //                     select x).FirstOrDefault();

    
           //****** end of visiblity test settings *****************************************

            MemberSearchViewmodels = (from x in db.profiledata.Where(p=> p.birthdate > min && p.birthdate <= max)
                            
                               //** visiblity settings still needs testing           
                             //5-8-2012 add profile visiblity code here
                            // .Where(x => x.profile.username == "case")
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.ProfileVisiblity == true)
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profiledata.birthdate > today.AddYears(-(x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault() + 1)))
                            //.Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.agemaxVisibility != null && model.profiledata.birthdate < today.AddYears(-x.ProfileVisiblitySetting.agemaxVisibility.GetValueOrDefault()))
                           // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.countryid != null && x.ProfileVisiblitySetting.countryid == model.profiledata.countryid  )
                           // .Where(x => x.ProfileVisiblitySetting != null || x.ProfileVisiblitySetting.GenderID != null && x.ProfileVisiblitySetting.GenderID ==  model.profiledata.GenderID )
                          //** end of visiblity settings ***
                          
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender.id)) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z=>z.gender.id  == model.LookingForGendersID.FirstOrDefault())    
                            //TO DO add the rest of the filitering here 
                            //Appearance filtering                         
                            .WhereIf(blEvaluateHeights, z=> z.height  > intheightmin && z.height  <= intheightmax) //Only evealuate if the user searching actually has height values they look for                         
                                      join f in db.profiles on x.id equals f.id                                    
                                      select new MemberSearchViewModel
                                      {
                                         // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                           id = x.id,
                                          stateprovince = x.stateprovince,
                                          postalcode = x.postalcode,
                                          countryid = x.countryid,
                                          genderid  = x.gender.id ,
                                          birthdate = x.birthdate,
                                          profile = f,
                                            screenname = f.screenname ,
                                          longitude = (double)x.longitude,
                                          latitude = (double)x.latitude,
                                          hasgalleryphoto  = (db.photos.Where(i => i.profile_id  == f.id && i.photostatus.id  == (int)photostatusEnum.Gallery ).FirstOrDefault().id  != null )? true : false ,
                                          creationdate = f.creationdate,
                                          //city = db.(x.city, 11),
                                          // lastloggedonstring   = db.fnGetLastLoggedOnTime(f.logindate),
                                          lastlogindate = f.logindate,
                                          //online  = db.fnGetUserOlineStatus(x.ProfileID),
                                         // distancefromme = db.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles") 
                                      }).ToList();


          //  var temp = MemberSearchViewmodels;
            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (model.MaxDistanceFromMe  > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme  <= model.MaxDistanceFromMe ) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
            //               Profiles;

            //do the stuff for the user defined functions here



            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.hasgalleryphoto  == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);

            //11/20/2011 handle case where  no profiles were found
            if (Profiles.Count() == 0 )
            return getquickmatcheswhenquickmatchesempty(model);


            return Profiles.ToList();


        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(MembersViewModel model)
        {

            //get search sttings from DB
            searchsetting perfectmatchsearchsettings = model.profiledata.profilemetadata.searchsettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.distancefromme  == null | perfectmatchsearchsettings.distancefromme == 0)
                model.MaxDistanceFromMe  = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.agemax != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.agemin!= null ? perfectmatchsearchsettings.agemin .GetValueOrDefault() : 18;
            //Height
            int intheightmin = perfectmatchsearchsettings.heightmin != null ? perfectmatchsearchsettings.heightmin.GetValueOrDefault() : 0;
            int intheightmax = perfectmatchsearchsettings.heightmax != null ? perfectmatchsearchsettings.heightmax.GetValueOrDefault() : 100;
            bool blEvaluateHeights = intheightmin > 0 ? true : false;
            //get the rest of the values if they are needed in calculations
            //convert lattitudes from string (needed for JSON) to bool           
            double? myLongitude = (model.MyLongitude != "") ? Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLatitude  != "") ? Convert.ToDouble(model.MyLongitude ) : 0;


            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id.GetValueOrDefault())) : LookingForGenderValues;
            //Appearacnce seache settings values         

            //set a value to determine weather to evaluate hights i.e if this user has not height values whats the point ?

            HashSet<int> LookingForBodyTypesValues = new HashSet<int>();
            LookingForBodyTypesValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.bodytypes.Select(c => c.id.GetValueOrDefault())) : LookingForBodyTypesValues;

            HashSet<int> LookingForEthnicityValues = new HashSet<int>();
            LookingForEthnicityValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.ethnicitys.Select(c => c.id.GetValueOrDefault())) : LookingForEthnicityValues;

            HashSet<int> LookingForEyeColorValues = new HashSet<int>();
            LookingForEyeColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.eyecolors.Select(c => c.id.GetValueOrDefault())) : LookingForEyeColorValues;

            HashSet<int> LookingForHairColorValues = new HashSet<int>();
            LookingForHairColorValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.haircolors.Select(c => c.id.GetValueOrDefault())) : LookingForHairColorValues;

            HashSet<int> LookingForHotFeatureValues = new HashSet<int>();
            LookingForHotFeatureValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.hotfeature.Select(c => c.id.GetValueOrDefault())) : LookingForHotFeatureValues;



            MemberSearchViewmodels = (from x in db.profiledata.Where(p => p.birthdate > min && p.birthdate <= max)
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender.id )) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => z.gender.id  == model.LookingForGendersID.FirstOrDefault())
                            //Appearance filtering not implemented yet                        
                            .WhereIf(blEvaluateHeights, z => z.height  > intheightmin && z.height  <= intheightmax) //Only evealuate if the user searching actually has height values they look for 
                            //we have to filter on the back end now since we cant use UDFs
                            // .WhereIf(model.MaxDistanceFromMe  > 0, a => db.fnGetDistance((double)a.latitude, (double)a.longitude, Convert.ToDouble(model.Mylattitude) ,Convert.ToDouble(model.MyLongitude), "Miles") <= model.Maxdistancefromme)
                                      join f in db.profiles on x.id  equals f.id 
                                      select new MemberSearchViewModel
                                      {
                                          // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                          id = x.id  ,
                                          stateprovince = x.stateprovince,
                                          postalcode = x.postalcode,
                                          countryid = x.countryid,
                                           genderid  = x.gender.id ,
                                          birthdate = x.birthdate,
                                          profile = f,
                                          screenname = f.screenname,
                                          longitude = x.longitude ?? 0,
                                          latitude = x.latitude ?? 0,
                                          hasgalleryphoto = (db.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                          creationdate = f.creationdate,
                                         // city = db.fnTruncateString(x.city, 11),
                                          //lastloggedonString = db.fnGetLastLoggedOnTime(f.logindate),
                                          lastlogindate = f.logindate,
                                        //  Online = db.fnGetUserOlineStatus(x.ProfileID),
                                       //  distancefromme = db.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")


                                      }).ToList();



            //these could be added to where if as well, also limits values if they did selected all
           // var Profiles = (model.Maxdistancefromme > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme <= model.Maxdistancefromme) select q) : MemberSearchViewmodels;
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
            //final ordering 
            var Profiles = MemberSearchViewmodels.OrderByDescending(p => p.hasgalleryphoto  == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme).Take(4);

            //11/20/2011 handle case where  no profiles were found
           // if (Profiles.Count() == 0)
            //    return GetQuickMatchesWhenQuickMatchesEmpty(model);


            return Profiles.ToList();


        }
        public List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model)
        {

            //get search sttings from DB
            searchsetting perfectmatchsearchsettings = model.profiledata.profilemetadata.searchsettings.FirstOrDefault();
            //set default perfect match distance as 100 for now later as we get more members lower
            //TO DO move this to a db setting or resourcer file
            if (perfectmatchsearchsettings.distancefromme == null | perfectmatchsearchsettings.distancefromme == 0)
                model.MaxDistanceFromMe  = 500;

            //TO DO add this code to search after types have been made into doubles
            //postaldataservicecontext.GetdistanceByLatLon(p.latitude,p.longitude,UserProfile.Lattitude,UserProfile.longitude,"M") > UserProfile.DiatanceFromMe
            //right now returning all countries as well

            //** TEST ***
            //get the  gender's from search settings

            // int[,] courseIDs = new int[,] UserProfile.profiledata.searchsettings.FirstOrDefault().searchsettings_Genders.ToList();
            int intAgeTo = perfectmatchsearchsettings.agemax != null ? perfectmatchsearchsettings.agemax.GetValueOrDefault() : 99;
            int intAgeFrom = perfectmatchsearchsettings.agemin!= null ? perfectmatchsearchsettings.agemin .GetValueOrDefault() : 18;
           
            //set variables
            List<MemberSearchViewModel> MemberSearchViewmodels;
            DateTime today = DateTime.Today;
            DateTime max = today.AddYears(-(intAgeFrom + 1));
            DateTime min = today.AddYears(-intAgeTo);
            //convert lattitudes from string (needed for JSON) to bool
            double? myLongitude = (model.MyLongitude != "") ? Convert.ToDouble(model.MyLongitude) : 0;
            double? myLattitude = (model.MyLatitude != "") ? Convert.ToDouble(model.MyLatitude) : 0;



            //get values from the collections to test for , this should already be done in the viewmodel mapper but juts incase they made changes that were not updated
            //requery all the has tbls
            HashSet<int> LookingForGenderValues = new HashSet<int>();
            LookingForGenderValues = (perfectmatchsearchsettings != null) ? new HashSet<int>(perfectmatchsearchsettings.genders.Select(c => c.id.GetValueOrDefault())) : LookingForGenderValues;
                         

            //  where (LookingForGenderValues.Count !=null || LookingForGenderValues.Contains(x.GenderID)) 
            //  where (LookingForGenderValues.Count == null || x.GenderID == UserProfile.MyQuickSearch.MySelectedSeekingGenderID )   //this should not run if we have no gender in searchsettings
            MemberSearchViewmodels = (from x in db.profiledata.Where(p => p.birthdate > min && p.birthdate <= max)
                            .WhereIf(LookingForGenderValues.Count > 0, z => LookingForGenderValues.Contains(z.gender.id )) //using whereIF predicate function 
                            .WhereIf(LookingForGenderValues.Count == 0, z => z.gender.id  == model.LookingForGendersID.FirstOrDefault())                            

                                      join f in db.profiles on x.id equals f.id
                                      select new MemberSearchViewModel
                                      {
                                         // MyCatchyIntroLineQuickSearch = x.AboutMe,
                                           id = x.id  ,
                                          stateprovince = x.stateprovince,
                                          postalcode = x.postalcode,
                                          countryid = x.countryid,
                                           genderid  = x.gender.id ,
                                          birthdate = x.birthdate,
                                          profile = f,
                                          screenname = f.screenname,
                                          longitude = x.longitude ?? 0,
                                          latitude =  x.latitude ?? 0,
                                           hasgalleryphoto = (db.photos.Where(i => i.profile_id == f.id && i.photostatus.id == (int)photostatusEnum.Gallery).FirstOrDefault().id != null) ? true : false,
                                           creationdate = f.creationdate,
                                         // city = db.fnTruncateString(x.city, 11),
                                         // lastloggedonString = db.fnGetLastLoggedOnTime(f.logindate),
                                          lastlogindate = f.logindate,
                                          //Online = db.fnGetUserOlineStatus(x.ProfileID),
                                        //  distancefromme = db.fnGetDistance((double)x.latitude, (double)x.longitude,myLattitude.Value  , myLongitude.Value   , "Miles")

                                      }).ToList();


            //these could be added to where if as well, also limits values if they did selected all
            var Profiles = (model.MaxDistanceFromMe  > 0) ? (from q in MemberSearchViewmodels.Where(a => a.distancefromme <= model.MaxDistanceFromMe ) select q) : MemberSearchViewmodels.Take(500);
            //     Profiles; ; 
            // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
            //               Profiles;

            //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
            //final ordering 
            Profiles = Profiles.OrderByDescending(p => p.hasgalleryphoto  == true).ThenByDescending(p => p.creationdate).ThenByDescending(p => p.distancefromme);


            return Profiles.ToList();


        }
       

     

     
    }
}
