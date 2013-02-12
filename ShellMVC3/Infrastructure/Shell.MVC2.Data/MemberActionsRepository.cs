using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Runtime.Serialization;
using System.Text;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

using Shell.MVC2.Infrastructure;
using Shell.MVC2.Interfaces;


using Shell.MVC2.Data.Infrastructure;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

using LoggingLibrary;

//TO DO move this kind of code to client
////return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoisinterestedinme, Page ?? 1, NumberPerPage.GetValueOrDefault())

namespace Shell.MVC2.Data
{
    public class MemberActionsRepository :MemberRepositoryBase,  IMemberActionsRepository 
    {
        //TO DO do this a different way I think
        //private AnewluvContext  _datingcontext;
        private IMemberRepository  _membersrepository;
        private LoggingLibrary.ErroLogging  logger;

        public MemberActionsRepository(AnewluvContext datingcontext ,IMemberRepository membersrepository ) 
            :base(datingcontext)
        {
            _membersrepository = membersrepository;
          
        }



        #region "Interest Methods"

        //added 1/29/2010 ola lawal
        //no checks i.e invokes to test values here only updates deletes etc , for now on the MVC side
        // they are prevalidated on the model and the booleans are checked on the view  view possible if and if statement exists
        //    remeber emails must be sent on the client side since they user shared reference files    

        //interest methods

        #region "Count methods"
      
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoiaminterestedincount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            try
            {
                count = (
               from f in _datingcontext.interests
               where (f.profile_id == profileid && f.deletedbymemberdate == null)
               select f).Count();
                // ?? operator example.
                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;
            }
            catch (Exception ex)
            {
                logger = new ErroLogging(applicationEnum.MemberActionsService);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw ex;
            }

            
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmecount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks .Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            count = (
               from p in _datingcontext.interests where (p.interestprofile_id == profileid )
               join f in _datingcontext.profiles  on p.profile_id   equals f.id
               where (f.status.id  < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId  == f.id )) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;
            
            return defaultvalue;
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmenewcount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.interests
               where (p.interestprofile_id == profileid && p.viewdate  == null)
               join f in _datingcontext.profiles on p.profile_id equals f.id
               where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;

            return defaultvalue;
        }

        #endregion

       
        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public List<MemberSearchViewModel> getinterests(int profileid, int? Page, int? NumberPerPage)
        {
                        //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho
;


         try
         {


             var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                  select new
                                  {
                                      ProfilesBlockedId = c.blockprofile_id
                                  };

             //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
             //rematerialize on the back end.
             //final query to send back only the profile datatas of the interests we want
             var interests = (from p in _datingcontext.interests.Where(p => p.profile_id == profileid)
                              join f in _datingcontext.profiledata on p.interestprofile_id equals f.profile_id
                              join z in _datingcontext.profiles on p.interestprofile_id equals z.id
                              where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                              select new MemberSearchViewModel
                              {
                                  creationdate = p.creationdate,
                                  id = p.id,
                                  age = f.age,
                                  birthdate = f.birthdate,
                                  city = f.city,
                                  countryid = f.countryid,
                                  stateprovince = f.stateprovince,
                                  longitude = (double)f.longitude,
                                  latitude = (double)f.latitude,
                                  genderid = f.gender.id,
                                  postalcode = f.postalcode,
                                  lastlogindate = z.logindate,
                                  //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                                  screenname = z.screenname,
                                  mycatchyintroline = f.mycatchyintroLine,
                                  aboutme = f.aboutme,
                                  perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                              }).OrderByDescending(f => f.lastlogindate.Value ).ThenByDescending(f => f.interestdate ).ToList();

             return interests; //new PaginatedList<MemberSearchViewModel>().GetPageableList(interests, Page ?? 1, NumberPerPage.GetValueOrDefault())

         }
         catch (Exception ex)
         {
             //instantiate logger here so it does not break anything else.
             logger = new ErroLogging(applicationEnum.MemberActionsService);
             logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
             //log error mesasge
             //handle logging here
             var message = ex.Message;
             throw ex;
         }


        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinme(int profileid, int? Page, int? NumberPerPage)
        {
            //IEnumerable<MemberSearchViewModel> whoisinterestedinme = default(IEnumerable<MemberSearchViewModel>);

            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
         var   whoisinterestedinme = (from p in _datingcontext.interests.Where(p => p.interestprofile_id == profileid)
                                    join f in _datingcontext.profiledata on p.profile_id equals f.profile_id 
                                   join z in _datingcontext.profiles on p.profile_id  equals z.id 
                                   where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id ))
                                   select new MemberSearchViewModel
                                   {
                                                creationdate    = p.creationdate,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                             lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.interestdate  ).ToList();

            //return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoisinterestedinme, Page ?? 1, NumberPerPage.GetValueOrDefault());
         return whoisinterestedinme;
        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinmenew(int profileid, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> whoisinterestedinme = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
          var  whoisinterestedinmenew = (from p in _datingcontext.interests.Where(p => p.interestprofile_id == profileid && p.viewdate  ==  null)
                                    join f in _datingcontext.profiledata on p.profile_id equals f.profile_id 
                                   join z in _datingcontext.profiles on p.profile_id  equals z.id
                                         where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                   select new MemberSearchViewModel
                                   {
                                        creationdate    = p.creationdate ,
                             id = p.profile_id ,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                             genderid  = f.gender.id,
                             postalcode = f.postalcode,
                              lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                             perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.interestdate  ).ToList();

           // return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoisinterestedinmenew, Page ?? 1, NumberPerPage.GetValueOrDefault());
          return whoisinterestedinmenew;
        }

        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both interest 
        ///  //not inmplemented
        /// </summary 
        //work on this later
          public List<MemberSearchViewModel> getmutualinterests(int profileid, int targetprofileid)
        {
            IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
            return mutualinterests.ToList();

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(int profileid, int targetprofileid)
        {
            return this._datingcontext.interests.Any(r => r.profile_id == profileid && r.interestprofile_id  == targetprofileid);
        }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(int profileid, int targetprofileid)
        {

            //create new inetrest object
            interest interest = new interest();
            //make sure you are not trying to interest at yourself
            if (profileid == targetprofileid) return false;

            //if this was a interest being restored just do that part
            if (checkinterest(profileid, targetprofileid))
            {


            };

            try
            {
                //interest = this._datingcontext.interests.Where(p => p.profileid == profileid).FirstOrDefault();
                //update the profile status to 2
                interest.profile_id = profileid;
                interest.interestprofile_id = targetprofileid;
                interest.mutual = false;  // not dealing with this calulatin yet
                interest.creationdate = DateTime.Now;
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this._datingcontext.interests.Add(interest);
                this._datingcontext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyprofileid(int profileid, int interestprofile_id)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2

                interest.deletedbymemberdate = DateTime.Now;
                interest.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyinterestprofileid(int interestprofile_id, int profileid)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2

                interest.deletedbyinterestdate = DateTime.Now;
                interest.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyprofileid(int profileid, int interestprofile_id)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2

                interest.deletedbymemberdate = null;
                interest.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyinterestprofileid(int interestprofile_id, int profileid)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2

                interest.deletedbyinterestdate = null;
                interest.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeinterestsbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            try//
            {
                // interests = this._datingcontext.interests.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                interest interest = new interest();
                foreach (string value in screennames)
                {
                    int? interestprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                    interest.deletedbymemberdate = DateTime.Now;
                    interest.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }
            return true;
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreinterestsbyprofileidandscreennames(int profileid, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // interests = this._datingcontext.interests.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                interest interest = new interest();
                foreach (string value in screennames)
                {
                    int? interestprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    interest = this._datingcontext.interests.Where(p => p.profile_id == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();

                    interest.deletedbymemberdate = null;
                    interest.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  Update interest with a view     
        /// </summary 
        public bool updateinterestviewstatus(int profileid, int targetprofileid)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.interestprofile_id == targetprofileid && p.profile_id == profileid).FirstOrDefault();
                //update the profile status to 2            
                if (interest.viewdate == null)
                {
                    interest.viewdate = DateTime.Now;
                    interest.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        #endregion


        #endregion

        //end of Inteerst methods
        //********************************************************

        //profile views in DB= peeks on UI
        #region "peek methods"

        //count methods first

        #region "Count methods"

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhoipeekedatcount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in _datingcontext.peeks
               where (f.profile_id == profileid && f.deletedbymemberdate == null)
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmecount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.peeks
               where (p.peekprofile_id == profileid)
               join f in _datingcontext.profiles on p.profile_id equals f.id
               where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;

            return defaultvalue;
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmenewcount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.peeks
               where (p.peekprofile_id == profileid && p.viewdate == null)
               join f in _datingcontext.profiles on p.profile_id equals f.id
               where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;

            return defaultvalue;
        }

        #endregion


        /// <summary>
        /// //gets all the members who are interested in me
        /// //TODO add filtering for blocked members that you blocked and system blocked
        /// </summary 
        public List<MemberSearchViewModel> getwhopeekedatme(int profileid, int? Page, int? NumberPerPage)
        {
          //  IEnumerable<MemberSearchViewModel> WhoPeekedAtMe = default(IEnumerable<MemberSearchViewModel>);



            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
         var   WhoPeekedAtMe = (from p in _datingcontext.peeks.Where(p => p.peekprofile_id  == profileid)
                                join f in _datingcontext.profiledata on p.profile_id equals f.profile_id 
                             join z in _datingcontext.profiles on p.profile_id  equals z.id
                                where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                             select new MemberSearchViewModel
                             {
                                 peekdate  = p.creationdate ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                              lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.peekdate ).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(WhoPeekedAtMe, Page ?? 1, NumberPerPage.GetValueOrDefault());
         return WhoPeekedAtMe;
        }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public List <MemberSearchViewModel> getwhopeekedatmenew(int profileid, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> PeekNew = default(IEnumerable<MemberSearchViewModel>);

            //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho

            
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var PeekNew = (from p in _datingcontext.peeks .Where(p => p.peekprofile_id  == profileid && p.viewdate  == null)
                           join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                       join z in _datingcontext.profiles on p.profile_id  equals z.id
                           where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                       select new MemberSearchViewModel
                       {
                             peekdate  = p.creationdate ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                              lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.peekdate ).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(PeekNew, Page ?? 1, NumberPerPage.GetValueOrDefault());
            return PeekNew;

        }

   

        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public List<MemberSearchViewModel> getwhoipeekedat(int profileid, int? Page, int? NumberPerPage)
        {
            //Page, int? NumberPerPage
            //  List<MemberSearchViewModel> peeks = default(List<MemberSearchViewModel>);        

            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var peeks = (from p in _datingcontext.peeks.Where(p => p.profile_id  == profileid && p.deletedbymemberdate == null)
                         join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                         join z in _datingcontext.profiles on p.profile_id  equals z.id
                         where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                         select new MemberSearchViewModel
                         {
                            peekdate = p.creationdate ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                             lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.creationdate ).ToList();

           // return new PaginatedList<MemberSearchViewModel>().GetPageableList(peeks, Page ?? 1, NumberPerPage.GetValueOrDefault());
            return peeks;
            
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
          public List<MemberSearchViewModel> getmutualpeeks(int profileid, int targetprofileid)
        {
            IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
            return mutualinterests.ToList();

        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(int profileid, int targetprofileid)
        {
            return this._datingcontext.peeks.Any(r => r.profile_id == profileid && r.peekprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(int profileid, int targetprofileid)
        {

            //create new inetrest object
            peek peek = new peek();
            //make sure you are not trying to peek at yourself
            if (profileid == targetprofileid) return false;

            //if this was a peek being restored just do that part
            if (checkpeek(profileid, targetprofileid))
            {


            };

            try
            {
                //interest = this._datingcontext.peeks.Where(p => p.profileid == profileid).FirstOrDefault();
                //update the profile status to 2
                peek.profile_id = profileid;
                peek.peekprofile_id = targetprofileid;
                peek.mutual = false ;  // not dealing with this calulatin yet
                peek.creationdate = DateTime.Now;
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this._datingcontext.peeks.Add(peek);
                this._datingcontext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbyprofileid(int profileid,int peekprofile_id)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                //update the profile status to 2

                peek.deletedbymemberdate = DateTime.Now;
                peek.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbypeekprofileid(int peekprofile_id,int profileid)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                //update the profile status to 2

                peek.deletedbypeekdate = DateTime.Now;
                peek.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbyprofileid(int profileid,int peekprofile_id)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                //update the profile status to 2

                peek.deletedbymemberdate = null;
                peek.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbypeekprofileid(int peekprofile_id,int profileid)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                //update the profile status to 2

                peek.deletedbypeekdate = null;
                peek.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removepeeksbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            try//
            {
                // peeks = this._datingcontext.peeks.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                peek peek = new peek();
                foreach (string value in screennames)
                {
                   int? peekprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                    peek.deletedbymemberdate = DateTime.Now;
                    peek.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }
            return true;
        }

        /// <summary>
        ///  //Removes a peek i.e makes is seem like you never peeeked at  anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(int profileid, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // peeks = this._datingcontext.peeks.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                peek peek = new peek();
                foreach (string value in screennames)
                {
                   int? peekprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    peek = this._datingcontext.peeks.Where(p => p.profile_id == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();

                    peek.deletedbymemberdate = null;
                    peek.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  Update peek with a view     
        /// </summary 
        public bool updatepeekviewstatus(int profileid, int targetprofileid)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.peekprofile_id == targetprofileid && p.profile_id == profileid).FirstOrDefault();
                //update the profile status to 2            
                if (peek.viewdate == null )
                {
                  peek.viewdate = DateTime.Now;
                peek.modificationdate = DateTime.Now;
                this._datingcontext.SaveChanges();
                }               
              

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        #endregion
   

        #endregion

        // MailboxLock methods , right now if you block user from mail they are blocked from your site on everything
        #region "block methods"


        //count methods first
        /// <summary>
        /// count all total blocks
        /// </summary>
       
        public int getwhoiblockedcount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
              from f in _datingcontext.blocks
              where (f.profile_id == profileid && f.removedate == null)
              select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        /// <summary>
        /// return all    block as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhoiblocked(int profileid, int? Page, int? NumberPerPage)
        {
            //IEnumerable<MemberSearchViewModel>blockNew = default(IEnumerable<MemberSearchViewModel>);


            //var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profileid == profileid && p.BlockRemoved == false)
            //                     select new
            //                     {
            //                        ProfilesBlockedId = c.blockprofile_id 
            //                     };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
          var  blockNew = (from p in _datingcontext.blocks.Where(p => p.profile_id   == profileid && p.removedate == null)
                           join f in _datingcontext.profiledata on p.blockprofile_id equals f.profile_id 
                               join z in _datingcontext.profiles on p.blockprofile_id equals z.id 
                               where (f.profile.status.id< 3)
                               orderby (p.creationdate) descending
                               select new MemberSearchViewModel
                               {
                                    blockdate    = p.creationdate  ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                             lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.blockdate  ).ToList();

         //   return new PaginatedList<MemberSearchViewModel>().GetPageableList(blockNew, Page ?? 1, NumberPerPage.GetValueOrDefault());
          return blockNew;
        }

           /// <summary>
        /// //gets all the members who areblocked in me
        /// </summary 
        public List <MemberSearchViewModel> getwhoblockedme(int profileid, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> whoisMailboxblockedinme = default(IEnumerable<MemberSearchViewModel>);



          var  whoblockedme = (from p in _datingcontext.blocks.Where(p => p.blockprofile_id  == profileid && p.removedate == null)
                               join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                                       join z in _datingcontext.profiles on p.profile_id  equals z.id 
                                       where (f.profile.status.id< 3)
                                       orderby (p.creationdate ) descending
                                       select new MemberSearchViewModel
                                       {
                                            blockdate    = p.creationdate ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                              lastlogindate    = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.blockdate  ).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoblockedme, Page ?? 1, NumberPerPage.GetValueOrDefault());
          return whoblockedme;
        }


        #region "update/check/reomve methods"
        
        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
          public List<MemberSearchViewModel> getmutualblocks(int profileid, int targetprofileid)
        {
            IEnumerable<MemberSearchViewModel> mutualblocks = default(IEnumerable<MemberSearchViewModel>);
            return mutualblocks.ToList();

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(int profileid, int targetprofileid)
        {
            return this._datingcontext.blocks.Any(r => r.profile_id == profileid && r.blockprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(int profileid, int targetprofileid)
        {

            //create new inetrest object
            block block = new block();
            //make sure you are not trying to block at yourself
            if (profileid == targetprofileid) return false;

            //if this was a block being restored just do that part
            if (checkblock(profileid, targetprofileid))
            { 
            
            
            };

            try
            {
                //interest = this._datingcontext.blocks.Where(p => p.profileid == profileid).FirstOrDefault();
                //update the profile status to 2
                block.profile_id = profileid;
                block.blockprofile_id = targetprofileid;
                block.mutual = 0;  // not dealing with this calulatin yet
                block.creationdate = DateTime.Now;              
                //block.  = null;
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this._datingcontext.blocks.Add(block);
                this._datingcontext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool removeblock(int profileid, int blockprofile_id)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.profile_id == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                //update the profile status to 2

                block.removedate  = DateTime.Now;
                block.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool restoreblock(int profileid, int blockprofile_id)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.profile_id == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                //update the profile status to 2

                block.removedate = null;
                block.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeblocksbyscreennames(int profileid, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // blocks = this._datingcontext.blocks.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                block block = new block();
                foreach (string value in screennames)
                {
                    int? blockprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    block = this._datingcontext.blocks.Where(p => p.profile_id == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();

                    block.removedate  = DateTime.Now;
                    block.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreblocksbyscreennames(int profileid, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // blocks = this._datingcontext.blocks.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                block block = new block();
                foreach (string value in screennames)
                {
                    int? blockprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    block = this._datingcontext.blocks.Where(p => p.profile_id == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();

                    block.removedate  = null;
                    block.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        //TO DO this needs to me reviewed , all blocks need notes  if reviewed otherwise nothing
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        /// 
        public bool updateblockreviewstatus(int profileid,int targetprofileid, int reviewerid)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.blockprofile_id  == targetprofileid && p.profile_id == profileid ).FirstOrDefault();
                //update the profile status to 2            
                //block. = DateTime.Now;
                //block.reviewerprofile_id = profileid;
                block.modificationdate = DateTime.Now;
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
        #endregion

        #endregion

        #region "Like methods"

        //count methods first

        #region "Count methods"

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoilikecount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in _datingcontext.likes
               where (f.profile_id == profileid && f.deletedbymemberdate == null)
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmecount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.likes
               where (p.likeprofile_id == profileid)
               join f in _datingcontext.profiles on p.profile_id equals f.id
               where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;

            return defaultvalue;
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmenewcount(int profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.likes
               where (p.likeprofile_id == profileid && p.viewdate == null)
               join f in _datingcontext.profiles on p.profile_id equals f.id
               where (f.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;

            return defaultvalue;
        }

        #endregion
       
             

        /// <summary>
        /// return all  new  likes as an object
        /// </summary>
        public List <MemberSearchViewModel> getwholikesmenew(int profileid, int? Page, int? NumberPerPage)
        {
          //  IEnumerable<MemberSearchViewModel> LikeNew = default(IEnumerable<MemberSearchViewModel>);



            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var LikeNew = (from p in _datingcontext.likes.Where(p => p.likeprofile_id  == profileid && p.viewdate  == null)
                           join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                       join z in _datingcontext.profiles on p.profile_id  equals z.id
                           where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                       orderby (p.creationdate) descending
                       select new MemberSearchViewModel
                       {
                              likedate     = p.creationdate  ,
                              id  = p.profile_id ,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                             genderid  = f.gender.id,
                             postalcode = f.postalcode,
                              lastlogindate    = z.logindate  ,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                             screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine ,
                             aboutme   = f.aboutme,
                             perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.likedate ).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(LikeNew, Page ?? 1, NumberPerPage.GetValueOrDefault());
            return LikeNew;

        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List <MemberSearchViewModel> getwholikesme(int profileid, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> _Like = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var wholikesme = (from p in _datingcontext.likes.Where(p => p.likeprofile_id   == profileid && p.deletedbylikedate == null)
                              join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                     join z in _datingcontext.profiles on p.profile_id  equals z.id
                              where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                     orderby (p.creationdate ) descending
                     select new MemberSearchViewModel
                     {
                            creationdate    = p.creationdate  ,
                           id = p.profile_id,
                             age = f.age,
                             birthdate = f.birthdate,
                             city = f.city,
                             countryid = f.countryid,
                             stateprovince = f.stateprovince,
                             longitude = (double)f.longitude,
                             latitude = (double)f.latitude,
                           genderid  = f.gender.id,
                             postalcode = f.postalcode,
                             lastlogindate     = z.logindate,
                             //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                            screenname  = z.screenname,
                             mycatchyintroline   = f.mycatchyintroLine,
                          aboutme   = f.aboutme,
                              perfectmatchsettings   = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.likedate.Value   ).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(wholikesme, Page ?? 1, NumberPerPage.GetValueOrDefault());
            return wholikesme;
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwhoilike(int profileid, int? Page, int? NumberPerPage)
        {
            // IEnumerable<MemberSearchViewModel> _Like = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var whoilike = (from p in _datingcontext.likes.Where(p => p.profile_id  == profileid && p.deletedbymemberdate == null)
                            join f in _datingcontext.profiledata on p.likeprofile_id equals f.profile_id
                              join z in _datingcontext.profiles on p.likeprofile_id  equals z.id
                            where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                              orderby (p.creationdate) descending
                              select new MemberSearchViewModel
                              {
                                  creationdate = p.creationdate,
                                  id = p.profile_id,
                                  age = f.age,
                                  birthdate = f.birthdate,
                                  city = f.city,
                                  countryid = f.countryid,
                                  stateprovince = f.stateprovince,
                                  longitude = (double)f.longitude,
                                  latitude = (double)f.latitude,
                                  genderid = f.gender.id,
                                  postalcode = f.postalcode,
                                  lastlogindate = z.logindate,
                                  //  LastLoggedInString = _datingcontext.fnGetLastLoggedOnTime(z.LoginDate),
                                  screenname = z.screenname,
                                  mycatchyintroline = f.mycatchyintroLine,
                                  aboutme = f.aboutme,
                                  perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )



                              }).OrderByDescending(f => f.lastlogindate).ThenByDescending(f => f.likedate).ToList();

          //  return new PaginatedList<MemberSearchViewModel>().GetPageableList(wholikesme, Page ?? 1, NumberPerPage.GetValueOrDefault());
            return whoilike;
        }


                #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
          public List<MemberSearchViewModel> getmutuallikes(int profileid, int targetprofileid)
        {
            IEnumerable<MemberSearchViewModel> mutuallikes = default(IEnumerable<MemberSearchViewModel>);
            return mutuallikes.ToList();

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(int profileid, int targetprofileid)
        {
            return this._datingcontext.likes.Any(r => r.profile_id == profileid && r.likeprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(int profileid, int targetprofileid)
        {

            //create new inetrest object
            like like = new like();
            //make sure you are not trying to like at yourself
            if (profileid == targetprofileid) return false;

            //if this was a like being restored just do that part
            if (checklike(profileid, targetprofileid))
            {


            };

            try
            {
                //interest = this._datingcontext.likes.Where(p => p.profileid == profileid).FirstOrDefault();
                //update the profile status to 2
                like.profile_id = profileid;
                like.likeprofile_id = targetprofileid;
                like.mutual = false;  // not dealing with this calulatin yet
                like.creationdate = DateTime.Now;
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this._datingcontext.likes.Add(like);
                this._datingcontext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebyprofileid(int profileid, int likeprofile_id)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                //update the profile status to 2

                like.deletedbymemberdate = DateTime.Now;
                like.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebylikeprofileid( int likeprofile_id,int profileid)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                //update the profile status to 2

                like.deletedbylikedate = DateTime.Now;
                like.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebyprofileid(int profileid, int likeprofile_id)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                //update the profile status to 2

                like.deletedbymemberdate = null;
                like.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebylikeprofileid(int likeprofile_id,int profileid)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                //update the profile status to 2

                like.deletedbylikedate = null;
                like.modificationdate = DateTime.Now;

                this._datingcontext.SaveChanges();
                //handele the update using EF
                // this._datingcontext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                // this._datingcontext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removelikesbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            try//
            {
                // likes = this._datingcontext.likes.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                like like = new like();
                foreach (string value in screennames)
                {
                    int? likeprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                    like.deletedbymemberdate = DateTime.Now;
                    like.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }
            return true;
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorelikesbyprofileidandscreennames(int profileid, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // likes = this._datingcontext.likes.Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                like like = new like();
                foreach (string value in screennames)
                {
                    int? likeprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    like = this._datingcontext.likes.Where(p => p.profile_id == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();

                    like.deletedbymemberdate = null;
                    like.modificationdate = DateTime.Now;
                    this._datingcontext.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        /// <summary>
        ///  Update like with a view     
        /// </summary 
        public bool updatelikeviewstatus(int profileid, int targetprofileid)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.likeprofile_id == targetprofileid && p.profile_id == profileid).FirstOrDefault();
                //update the profile status to 2            
                if (like.viewdate == null )
                {
                  like.viewdate = DateTime.Now;
                like.modificationdate = DateTime.Now;
                this._datingcontext.SaveChanges();
                }               
              

            }
            catch (Exception ex)
            {
                throw ex;
                // log the execption message
                //return false;
            }

            return true;

        }

        #endregion


        #endregion

        #region "Search methods"

        ////updated do not use the data from the members stuff since we are correctly populating values now , just find the lat long for the city and country entered
        //public List<MemberSearchViewModel> GetQuickSearchMembers(int intAgeFrom, int intAgeTo,
        //                                            string strLookingForSelectedGenderName, int intSelectedCountryId,
        //                                            string strSelectedCity, string strSelectedStateProvince, double maxdistancefromme, bool HasPhoto, MembersViewModel model)
        //{
        //    List<MemberSearchViewModel> MemberSearchViewmodels;


        //    DateTime today = DateTime.Today;
        //    DateTime max = today.AddYears(-(intAgeFrom + 1));
        //    DateTime min = today.AddYears(-intAgeTo);

        //    // var years = employee.Where(e => e.DOB != null && e.DOB > min && e.DOB <= max); 


        //    //if selected sity is all or empty lets just pull the top 100 members, that macth the rest of the ce

        //    //if selected sity is all or empty lets just pull the top 100 members, that macth the rest of the ce           
        //    //TO DO
        //    //make this more efficnet and resitrcted later maybe only vip members
        //    //  where (strSelectedCity != "ALL" || x.city == strSelectedCity)
        //    //(x.age >= intAgeFrom && x.age <= intAgeTo  )
        //    //let age = System.Data.Objects.SqlClient.SqlFunctions.DateDiff("y", x.birthdate, DateTime.Now)  
        //    //from uir in Aspnet_UsersInRoles 
        //    //            from r in Aspnet_Roles.Where( _r => _r.RoleId == uir.RoleId).DefaultIfEmpty() 
        //    //            group r.RoleName by uir.UserId into gr 
        //    //            join u in Aspnet_Users on gr.Key equals u.UserId         
        //    //            from up in UserProfiles.Where( _up => _up.UserId == u.UserId ).DefaultIfEmpty() 
        //    //            orderby  up.FirstName, up.LastName, u.username 
        //    //            select new 
        //    //            { 

        //    // ObjectSet <profiledata> products = db.profiledata ;  


        //    //******** visiblitysettings test code ************************

        //    //// test all the values you are pulling here
        //    //var TestModel = (from x in db.profiledata.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //    //                      select x).FirstOrDefault();
        //    //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault() + 1));
        //    // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profile.profiledata.GenderID) ? true : false;

        //    // var testmodel2 = (from x in db.profiledata.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //    //                     .Where(z=>z.ProfileVisiblitySetting !=null || z.ProfileVisiblitySetting.ProfileVisiblity == true)
        //    //                     select x).FirstOrDefault();

        //    // Expression<Func<profiledata, bool>> MyWhereExpr = default(Expression<Func<profiledata,  bool>>);



        //    MemberSearchViewmodels = (from x in _datingcontext.profiledata.Include("ProfileVisiblitySetting").Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //                             .WhereIf(strSelectedCity == "ALL", z => z.countryid == intSelectedCountryId)                                      
        //                              join  f in _datingcontext.profiles on x.profileid equals f.profile_id
        //                              // from fp in f  where(x.countryid == intSelectedCountryId)
        //                              //join z in db.photos on x.profileid equals z.profileid
        //                              select new MemberSearchViewModel
        //                              {
        //                                  // MyCatchyIntroLineQuickSearch = x.AboutMe,
        //                                  profileid = x.profileid,
        //                                  stateprovince = x.stateprovince,
        //                                  postalcode = x.postalcode,
        //                                  countryid = x.countryid,
        //                                  GenderID = x.GenderID,
        //                                  birthdate = x.birthdate,
        //                                  profile = f,
        //                                  longitude = (double)x.longitude,
        //                                  latitude = (double)x.latitude,
        //                                  HasGalleryPhoto = (from p in _datingcontext.photos.Where(i => i.profileid == f.profile_id && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
        //                                  creationdate = f.creationdate,
        //                                  city = _datingcontext.fnTruncateString(x.city, 11),
        //                                  lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.LoginDate),
        //                                  lastlogindate = f.LoginDate,
        //                                  Online = _datingcontext.fnGetUserOlineStatus(x.profileid),
        //                                  DistanceFromMe = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude, model.MyQuickSearch.MySelectedlattitude.Value, model.MyQuickSearch.MySelectedLongitude.Value, "Miles"),
        //                                  ProfileVisibility = x.ProfileVisiblitySetting.ProfileVisiblity

        //                              }).ToList();


        //    //these could be added to where if as well, also limits values if they did selected all
        //    var Profiles = (maxdistancefromme > 0 && strSelectedCity != "ALL") ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= maxdistancefromme) select q) : MemberSearchViewmodels.Take(500);
        //    //     Profiles; ; 
        //    // Profiles = (intSelectedCountryId  != null) ? (from q in Profiles.Where(a => a.countryid  == intSelectedCountryId) select q) :
        //    //               Profiles;

        //    //TO DO switch this to most active postible and then filter by last logged in date instead .ThenByDescending(p => p.lastlogindate)
        //    //final ordering 
        //    Profiles = Profiles.OrderByDescending(p => p.HasGalleryPhoto == "Gallery").ThenByDescending(p => p.creationdate).ThenByDescending(p => p.DistanceFromMe);


        //    //5-15-2012 filter out by visiblity settings and other visiblity stuff
        //    Profiles = Profiles.Where(x => x.ProfileVisibility != false);


        //    return Profiles.ToList();

        //}

        #endregion

    }
}
