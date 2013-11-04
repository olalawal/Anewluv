using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;
using Anewluv.DataAccess.Interfaces;
using Anewluv.Domain.Data.ViewModels;



namespace Anewluv.DataExtentionMethods
{
    public static class profileextentionmethods
    {


        public static profiledata getprofiledatabyprofileid(this IRepository<profiledata> repo, ProfileModel model)
        {
            return repo.Find().OfType<profiledata>().Where(p => p.profile_id == model.profileid).FirstOrDefault();
        }

        public static profiledata getprofiledatabyscreenname(this IRepository<profiledata> repo, ProfileModel model)
        {
            return repo.Find().OfType<profiledata>().Where(p => p.profile.screenname == model.screenname).FirstOrDefault();
        }

        public static profilemetadata getprofilemetadatabyprofileid(this IRepository<profilemetadata> repo, ProfileModel model)
        {
            return repo.Find().OfType<profilemetadata>().Where(p => p.profile_id == model.profileid).FirstOrDefault();
        }

        public static profile getprofilebyprofileid(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.id == model.profileid).FirstOrDefault();
        }

        public static profile getprofilebyscreenname(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.screenname == model.screenname).FirstOrDefault();
        }

        public static profile getprofilebyemailaddress(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.emailaddress == model.email).FirstOrDefault();
        }

        public static profile getprofilebyusername(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.username == model.username).FirstOrDefault();
        }

        public static profile getprofileidbyopenid(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            var profile = repo.Find().OfType<profile>().Where(p => p.emailaddress == model.email).FirstOrDefault();


            //if we have an active cache we store the current value 
            if (profile != null && profile.openids.Any(p => p.openidprovider.description == model.openidprovider))
            {
                return profile;
            }
            return null;
        }

        public static profile getactivatedgrofilebyusername(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return repo.Find().OfType<profile>().Where(p => p.username == model.username && p.status.id == 2).FirstOrDefault();


        }

        public static bool checkifemailalreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Find().OfType<profile>().Where(p => p.emailaddress == model.email).FirstOrDefault() != null);


        }

        public static bool checkifscreennamealreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Find().OfType<profile>().Where(p => p.screenname == model.screenname).FirstOrDefault() != null);

        }

        public static bool checkifusernamealreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            return (repo.Find().OfType<profile>().Where(p => p.username == model.username).FirstOrDefault() != null);
        }

        //TO DO need enum for stats
        public static bool checkifprofileisactivated(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Find().OfType<profile>().Where(p => p.id == model.profileid & p.status.id != 1).FirstOrDefault() != null);

        }

        public static bool checkifmailboxfoldersarecreated(this IRepository<profile> repo, ProfileModel model)
        {
            return (repo.Find().Where(p => p.profilemetadata.mailboxfolders.Any( d=>d.profiled_id == model.profileid)) != null);
        }

        //TO DO need enum for stats
        public static bool checkifactivationcodeisvalid(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Find().OfType<profile>().Where(p => p.activationcode == model.activationcode & p.id == model.profileid).FirstOrDefault() != null);

        }





        public static visiblitysetting getvisibilitysettingsbyprofileid(this IRepository<visiblitysetting> repo, ProfileModel model)
        {
            return repo.Find().OfType<visiblitysetting>().Where(p => p.profile_id == model.profileid).FirstOrDefault();
        }

        //TO DO move the generic infratructure extentions
        public static string getlastloggedinstring(DateTime logindate)
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
                // logger = new ErroLogging(applicationEnum.MemberService);
                // logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }

        }

        public static bool getuseronlinestatus(this IRepository<userlogtime> repo, ProfileModel model)
        {
            try
            {
                //get the profile
                //profile myProfile;
                IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);

                myQuery = repo.Find().OfType<userlogtime>().Where(p => p.profile_id == model.profileid && p.offline == false).Distinct().OrderBy(n => n.logintime);

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
                throw ex;
            }

        }

        //**** mostly update methods added for authorization stuff *****

        public static bool updateuserlogintimebyprofileidandsessionid(ProfileModel model, IUnitOfWork db)
        {

            //get the profile
            //profile myProfile;
            // IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);


            db.DisableProxyCreation = true;


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
                        db.Update(p);
                    }


                    //aloso update the profile table with current login date
                    var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
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
                    throw ex;

                    //throw convertedexcption;
                }
            }


        }

        public static bool updateuserlogintimebyprofileid(ProfileModel model, IUnitOfWork db)
        {
            //get the profilevvvvvnhhhhhhhhhhhhhhhhhhhhhhhnnnnnv f65444445666666666666646354rttgf
            //profile myProfile;
            //IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
            // profile myProfile = new profile();
            // userlogtime myLogtime = new userlogtime();
            //  DateTime currenttime = DateTime.Now;

            db.DisableProxyCreation = true;

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
                        db.Update(p);
                    }

                    //aloso update the profile table with current login date
                    //aloso update the profile table with current login date
                    var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
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
                    throw ex;

                    //throw convertedexcption;
                }
            }
        }

        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public static bool updateuserlogouttimebyprofileid(ProfileModel model, IUnitOfWork db)
        {
            db.DisableProxyCreation = true;
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
                    throw ex;

                    //throw convertedexcption;
                }

                return false;
            }


        }      

        public static bool updateprofileactivity(profileactivity model, IUnitOfWork db)
        {
            db.DisableProxyCreation = true;
            db.IsAuditEnabled = false; //do not audit on adds
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    //In this code we will use the IP address to get the geo coding info and them check to see if it is amatch to another
                    // if not add a new one 
                    db.Add(model);
                    int i = db.Commit();
                    transaction.Commit();

                    return true;

                    // this._datingcontext.profileactivity.Add(model);
                    //save all changes bro
                    // this._datingcontext.SaveChanges();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return false;

        }

        public static bool activateprofile(ProfileModel model, IUnitOfWork db)
        {
         
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
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
                        throw ex;

                        //throw convertedexcption;
                    }
                }
            

        }

        public static bool updatepassword(ProfileModel model,IUnitOfWork db)
        {
           
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var myProfile = db.GetRepository<profile>().getprofilebyprofileid(model);
                        //update the profile status to 2
                        myProfile.password = model.encryptedpassword;
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
                        throw ex;

                        //throw convertedexcption;
                    }
                }
            

        }

        public static bool addnewopenidforprofile(ProfileModel model, IUnitOfWork db)
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
                        throw ex;

                        //throw convertedexcption;
                    }
                }
            


        }

        //update the database i.e create folders and change profile status from guest to active ?
        //TO DO move this to mailbox extentions 
        public static bool createmailboxfolders(ProfileModel model, IUnitOfWork db)
        {
            db.DisableProxyCreation = true;          
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
                       throw ex;

                        //throw convertedexcption;
                    }
                }

                return false;
            }


        }

    }
