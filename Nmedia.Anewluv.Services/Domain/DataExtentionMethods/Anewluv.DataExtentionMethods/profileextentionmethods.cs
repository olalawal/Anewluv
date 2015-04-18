using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Nmedia.DataAccess.Interfaces;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;



namespace Anewluv.DataExtentionMethods
{
    public static class profileextentionmethods
    {


       


         //example using eager loading of profile metadata -- too slow right now 
        // return repo.Find(p => p.id == model.profileid.Value,p=>p.profilemetadata).Select().FirstOrDefault();

        public static profiledata getprofiledatabyprofileid(this IRepository<profiledata> repo, ProfileModel model)
        {
            return repo.Query(p => p.profile_id == model.profileid.Value).Select().FirstOrDefault();
        }

        public static profiledata getprofiledatabyscreenname(this IRepository<profiledata> repo, ProfileModel model)
        {
            return repo.Query(p => p.profile.screenname == model.screenname).Select().FirstOrDefault();
        }

        //TO DO add photos and photo conversions maybe ? so we dont need the profile viewmodel
        public static profilemetadata getprofilemetadatabyprofileid(this IRepository<profilemetadata> repo, ProfileModel model)
        {
            return repo.Query(p => p.profile_id == model.profileid.Value)

                .Include(x => x.profile)
                   .Include(z => z.photos.Select(s => s.photoconversions))
                      .Include(z => z.profiledata_ethnicity.Select(s => s.lu_ethnicity))
                         .Include(z => z.profiledata_hobby.Select(s => s.lu_hobby))
                            .Include(z => z.profiledata_hotfeature.Select(s => s.lu_hotfeature))
                    .Include(z => z.profiledata_lookingfor.Select(s=>s.lu_lookingfor))
                    .Include(z => z.rateeratingvalues)
                    .Include(z => z.createdactions)
                    .Include(z => z.targetofactions)
                    .Include(z => z.applications)

                    .Include(x => x.profile.profiledata)
                     .Include(x => x.profile.profiledata.lu_bodytype)
                     .Include(x => x.profile.profiledata.lu_diet)
                 .Include(x => x.profile.profiledata.lu_drinks)
                  .Include(x => x.profile.profiledata.lu_educationlevel)
                   .Include(x => x.profile.profiledata.lu_employmentstatus)
                    .Include(x => x.profile.profiledata.lu_exercise)
                     .Include(x => x.profile.profiledata.lu_eyecolor)

                      .Include(x => x.profile.profiledata.lu_gender)
                      .Include(x => x.profile.profiledata.lu_haircolor)
                      .Include(x => x.profile.profiledata.lu_havekids)
                      .Include(x => x.profile.profiledata.lu_humor)
                      .Include(x => x.profile.profiledata.lu_incomelevel)
                      .Include(x => x.profile.profiledata.lu_livingsituation)
                      .Include(x => x.profile.profiledata.lu_maritalstatus)
                      .Include(x => x.profile.profiledata.lu_politicalview)
                      .Include(x => x.profile.profiledata.lu_profession)
                      .Include(x => x.profile.profiledata.lu_religion)
                      .Include(x => x.profile.profiledata.lu_religiousattendance)
                      .Include(x => x.profile.profiledata.lu_sign)
                      .Include(x => x.profile.profiledata.lu_smokes)
                       .Include(x => x.profile.profiledata.lu_wantskids)
                    
                       //searchsettings
                       .Include(x => x.searchsettings.Select(s => s.details))
                          .Include(x => x.searchsettings.Select(s => s.locations))


                .Select().FirstOrDefault();
        }

        /// <summary>
        /// added roles
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static profile getprofilebyprofileid(this IRepository<profile> repo, ProfileModel model)
        {

            return repo.Query(p => p.id == model.profileid.Value).Include(x => x.profiledata)
                .Include(z => z.profilemetadata)
                .Include(p => p.membersinroles.Select(z => z.lu_role))                 
                .Include(i => i.profilemetadata.searchsettings.Select(s => s.details)).Select().FirstOrDefault();
               // .Include(z => z.profilemetadata.searchsettings).Select().FirstOrDefault();
        }
        /// <summary>
        /// This extention returns even more metadata stuff
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static profile getprofileandmetadatabyprofileid(this IRepository<profile> repo, ProfileModel model)
        {

            return repo.Query(p => p.id == model.profileid.Value).Include(x => x.profiledata)
                .Include(z => z.profilemetadata)              
                 .Include(p => p.membersinroles.Select(z => z.lu_role))   
                   .Include(z => z.profilemetadata.photos.Select(s=>s.photoconversions))
                      .Include(z => z.profilemetadata.profiledata_ethnicity)
                         .Include(z => z.profilemetadata.profiledata_hobby)
                            .Include(z => z.profilemetadata.profiledata_hotfeature)
                    .Include(z => z.profilemetadata.profiledata_lookingfor)
                    .Include(z => z.profilemetadata.rateeratingvalues)
                    .Include(z => z.profilemetadata.createdactions)
                    .Include(z => z.profilemetadata.targetofactions)
                    .Include(z => z.profilemetadata.applications)


                  .Include(i => i.profilemetadata.searchsettings.Select(s => s.details)).Select().FirstOrDefault();
        }

        public static profile getprofilebyscreenname(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Query(p => p.screenname == model.screenname).Include(x => x.profiledata)
                .Include(z=>z.profilemetadata)
                .Include(p => p.membersinroles.Select(z => z.lu_role))   
                .Select().FirstOrDefault();
        }

        public static profile getprofilebyemailaddress(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Query(p => p.emailaddress == model.email).Include(x => x.profiledata)
                .Include(p => p.membersinroles.Select(z => z.lu_role))   
                .Include(z=>z.profilemetadata).Select().FirstOrDefault();
        }

        public static profile getprofilebyusername(this IRepository<profile> repo, ProfileModel model)
        {
            return repo.Query(p => p.username == model.username)
                .Include(x => x.profiledata)
                .Include(p => p.membersinroles.Select(z => z.lu_role))   
                .Include(z=>z.profilemetadata).Select().FirstOrDefault();
        }

        public static profile getprofileidbyopenid(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            //lazy loading needed
            var profile = repo.Query(p => p.emailaddress == model.email)
                .Include(x => x.profiledata)
                .Include(p => p.membersinroles.Select(z => z.lu_role))   
                .Include(z=>z.profilemetadata).Select().FirstOrDefault();


            //if we have an active cache we store the current value 
            if (profile != null && profile.openids.Any(p => p.lu_openidprovider.description == model.openidprovider))
            {
                return profile;
            }
            return null;
        }

        public static bool checkifemailalreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Query(p => p.emailaddress == model.email) != null);


        }

        public static bool checkifscreennamealreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Query(p => p.screenname == model.screenname) != null);

        }

        public static bool checkifusernamealreadyexists(this IRepository<profile> repo, ProfileModel model)
        {
            return (repo.Query(p => p.username == model.username) != null);
        }

        //TO DO need enum for stats
        public static bool checkifprofileisactivated(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Query(p => p.username == model.username & p.status_id != 1).Select().FirstOrDefault() != null);

        }

        //TO DO need enum for stats
        public static bool checkifactivationcodeisvalid(this IRepository<profile> repo, ProfileModel model)
        {
            //MembersRepository membersrepository = new MembersRepository();
            //get the correct value from DB
            return (repo.Query(p => p.activationcode == model.activationcode & p.username == model.username).Select().FirstOrDefault() != null);

        }


        public static visiblitysetting getvisibilitysettingsbyprofileid(this IRepository<visiblitysetting> repo, ProfileModel model)
        {
            return repo.Query(p => p.profile_id == model.profileid.Value)
                .Include(p => p.profiledata)   
                .Select().FirstOrDefault();
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
                // logger = new  Logging(applicationEnum.MemberService);
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
                var profile = repo.Queryable().Where(p => p.id == 1).FirstOrDefault();
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

                // var  myQuery = repo.Query<userlogtime>().Where(p => p.profile_id == model.profileid.Value && p.offline == false).Distinct().OrderBy(n => n.logintime).ToList();
                var myQuery = repo.Queryable().Where(p => p.id == model.profileid.Value && !(p.userlogtimes != null && p.userlogtimes.Any(z => z.offline == false))).FirstOrDefault() != null;
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


        public static searchsetting getperfectmatchsearchsettingsbyprofileid(ProfileModel model,IUnitOfWorkAsync db)
        {
           
                try
                {

                    IQueryable<searchsetting> tmpsearchsettings = default(IQueryable<searchsetting>);
                    //Dim ctx As New Entities()
                    tmpsearchsettings = db.Repository<searchsetting>().Queryable().Where(p => p.profile_id == model.profileid.Value && p.myperfectmatch == true);

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
                        Newsearchsettings.profile_id = model.profileid.Value;
                        Newsearchsettings.myperfectmatch = true;
                        Newsearchsettings.searchname = "myperfectmatch";
                        //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                        //   db.Repository<Country_PostalCode_List>().searchsettings.Add(Newsearchsettings);
                        //   db.Repository<Country_PostalCode_List>().SaveChanges();
                        //save the profile data with the search settings to the Initial Catalog= so we dont have to create it again
                        return Newsearchsettings;



                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                    //throw convertedexcption;
                }

            
        }


        public static searchsetting createmyperfectmatchsearchsettingsbyprofileid(ProfileModel model, IUnitOfWorkAsync db)
        {

            // db.DisableProxyCreation = true;
         
               // db.IsAuditEnabled = false; //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        //get the profileDta                    

                        searchsetting Newsearchsettings = new searchsetting();

                        Newsearchsettings = new searchsetting();
                        Newsearchsettings.profile_id = model.profileid.Value; //.GetValueOrDefault();
                        Newsearchsettings.myperfectmatch = true;
                        Newsearchsettings.searchname = "myperfectmatch";
                        //Newsearchsettings.profiledata = this.GetProfileDataByProfileID(profileid);
                        db.Repository<searchsetting>().Insert(Newsearchsettings);
                        db.Commit();  
                       // transaction.Commit();

                        return Newsearchsettings;


                        //save the profile data with the search settings to the Initial Catalog= so we dont have to create it again
                        //return Newsearchsettings;

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       throw;
                        //throw convertedexcption;
                    }



                }
         }

        
    
    
    
    }


    
}
