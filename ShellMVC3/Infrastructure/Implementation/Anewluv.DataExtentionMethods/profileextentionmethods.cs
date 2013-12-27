using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;
using Nmedia.DataAccess.Interfaces;
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
            return repo.Find().OfType<profiledata>().Where(p => p.profile.screenname  == model.screenname).FirstOrDefault();
        }

        public static profilemetadata  getprofilemetadatabyprofileid(this IRepository<profilemetadata> repo, ProfileModel model)
        {
            return repo.Find().OfType<profilemetadata>().Where(p => p.profile_id  == model.profileid).FirstOrDefault();
        }

        public static profile getprofilebyprofileid(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.id == model.profileid).FirstOrDefault();
        }

        public static profile getprofilebyscreenname(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.screenname  == model.screenname ).FirstOrDefault();
        }

        public static profile getprofilebyemailaddress(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.emailaddress  == model.email ).FirstOrDefault();
        }

        public static profile getprofilebyusername(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Find().OfType<profile>().Where(p => p.username == model.username ).FirstOrDefault();
        }

        public static profile getprofileidbyopenid(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            var profile = repo.Find().OfType<profile>().Where(p => p.emailaddress == model.email).FirstOrDefault();
            

            //if we have an active cache we store the current value 
            if (profile!=null &&  profile.openids.Any(p=>p.lu_openidprovider.description  == model.openidprovider))
            {
                return profile;
            }
            return null;
        }

         public static bool checkifemailalreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
           return  (repo.Find().OfType<profile>().Where(p => p.emailaddress == model.email).FirstOrDefault() != null);
            

        }

         public static bool  checkifscreennamealreadyexists(this IRepository<profile> repo, ProfileModel model)
         {
             //MembersRepository membersrepository = new MembersRepository();
             //get the correct value from DB
             return (repo.Find().OfType<profile>().Where(p => p.screenname  == model.screenname).FirstOrDefault() != null);

         }

         public static bool checkifusernamealreadyexists(this IRepository<profile> repo, ProfileModel model)
         {
             return (repo.Find().OfType<profile>().Where(p => p.username  == model.username).FirstOrDefault() != null);
         }

        //TO DO need enum for stats
         public static bool checkifprofileisactivated(this IRepository<profile> repo, ProfileModel model)
         {
             //MembersRepository membersrepository = new MembersRepository();
             //get the correct value from DB
             return (repo.Find().OfType<profile>().Where(p => p.id == model.profileid & p.status_id != 1).FirstOrDefault() != null);

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
               // logger = new ErroLogging(logapplicationEnum.MemberService);
               // logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }

        }

        public static bool getuseronlinestatus(this IRepository<profile> repo, ProfileModel model)
        {
            try
            {
                var profile = repo.Find().Where(p => p.id == 1).FirstOrDefault();
                var logtimes = profile.userlogtimes;

                if (logtimes.Count > 0)
                {
                    return logtimes.Any(z => z.offline == false);
                }
                else
                {
                    return false;
                }
                //get the profile
                //profile myProfile;
               // IQueryable<userlogtime> myQuery = default(IQueryable<userlogtime>);
               
              // var  myQuery = repo.Find().OfType<userlogtime>().Where(p => p.profile_id == model.profileid && p.offline == false).Distinct().OrderBy(n => n.logintime).ToList();
                var myQuery = repo.Find().Where(p => p.id == model.profileid && !(p.userlogtimes !=null && p.userlogtimes.Any(z => z.offline == false))).FirstOrDefault() != null;
                return myQuery;
                //            var queryB =
                //                (from o in db.Orders
                // select o.Employee.LastName)
                //.Distinct().OrderBy(n => n);
                //if (myQuery.Count() > 0)
                //{
                //    return true;
                //}
                //else { return false; }
            }
            catch (Exception ex)
            {                
                throw ex;
            }

        }

         
    }
}
