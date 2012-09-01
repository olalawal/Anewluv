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



namespace Shell.MVC2.Data
{
    public class MemberActionsRepository :MemberRepositoryBase,  IMemberActionsRepository 
    {
        //TO DO do this a different way I think
        //private AnewluvContext  _datingcontext;
        private IMemberRepository  _membersrepository;

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
        public int getwhoiaminterestedincount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in _datingcontext.interests 
               where (f.profile_id  == profileid && f.deletedbymemberdate == null)
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
        public int getwhoisinterestedinmecount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks .Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            count = (
               from p in _datingcontext.interests where (p.interestprofile_id == profileID )
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
        public int getwhoisinterestedinmenewcount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.interests
               where (p.interestprofile_id == profileID && p.viewdate  == null)
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
        public IPageable<MemberSearchViewModel> getinterests(string profileID, int? Page, int? NumberPerPage)
        {
                        //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id   == profileID && p.removedate  == null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
         var   interests = (from p in _datingcontext.interests.Where(p => p.profile_id    == profileID)
                         join f in _datingcontext.profiledatas on p.interestprofile_id  equals f.id 
                         join z in _datingcontext.profiles on p.interestprofile_id equals z.id
                         where (f.profile.status.id  < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id ))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate  ).ThenByDescending(f => f.interestdate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(interests, Page ?? 1, NumberPerPage.GetValueOrDefault());





        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public IPageable<MemberSearchViewModel> getwhoisinterestedinme(string profileID, int? Page, int? NumberPerPage)
        {
            //IEnumerable<MemberSearchViewModel> whoisinterestedinme = default(IEnumerable<MemberSearchViewModel>);

            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
         var   whoisinterestedinme = (from p in _datingcontext.interests.Where(p => p.interestprofile_id == profileID)
                                    join f in _datingcontext.profiledatas on p.profile_id equals f.id
                                   join z in _datingcontext.profiles on p.profile_id  equals z.id 
                                   where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.interestdate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoisinterestedinme, Page ?? 1, NumberPerPage.GetValueOrDefault());

        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public IPageable<MemberSearchViewModel> getwhoisinterestedinmenew(string profileID, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> whoisinterestedinme = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
          var  whoisinterestedinmenew = (from p in _datingcontext.interests.Where(p => p.interestprofile_id == profileID && p.viewdate  ==  null)
                                    join f in _datingcontext.profiledatas on p.profile_id equals f.id
                                   join z in _datingcontext.profiles on p.profile_id  equals z.id 
                                   where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                             perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.interestdate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoisinterestedinmenew, Page ?? 1, NumberPerPage.GetValueOrDefault());
        }

        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both interest 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearchViewModel> getmutualinterests(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
            return mutualinterests;

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(string profileid, string targetprofileid)
        {
            return this._datingcontext.interests.Any(r => r.profile_id == profileid && r.interestprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(string profileID, string targetprofileID)
        {

            //create new inetrest object
            interest interest = new interest();
            //make sure you are not trying to interest at yourself
            if (profileID == targetprofileID) return false;

            //if this was a interest being restored just do that part
            if (checkinterest(profileID, targetprofileID))
            {


            };

            try
            {
                //interest = this._datingcontext.interests.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                interest.profile_id = profileID;
                interest.interestprofile_id = targetprofileID;
                interest.mutual = 0;  // not dealing with this calulatin yet
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
        public bool removeinterestbyprofileid(string profileID, string interestprofile_id)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
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
        public bool removeinterestbyinterestprofileid(string interestprofile_id, string profileID)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
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
        public bool restoreinterestbyprofileid(string profileID, string interestprofile_id)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
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
        public bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileID)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
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
        public bool removeinterestsbyprofileidandscreennames(string profileID, List<String> screennames)
        {
            try//
            {
                // interests = this._datingcontext.interests.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                interest interest = new interest();
                foreach (string value in screennames)
                {
                    string interestprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
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
        public bool restoreinterestsbyprofileidandscreennames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // interests = this._datingcontext.interests.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                interest interest = new interest();
                foreach (string value in screennames)
                {
                    string interestprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    interest = this._datingcontext.interests.Where(p => p.profile_id == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();

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
        public bool updateinterestviewstatus(string profileID, string targetprofileid)
        {

            //get the profile
            //profile Profile;
            interest interest = new interest();

            try
            {
                interest = this._datingcontext.interests.Where(p => p.interestprofile_id == targetprofileid && p.profile_id == profileID).FirstOrDefault();
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
        public int getwhoipeekedatcount(string profileid)
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
        public int getwhopeekedatmecount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.peeks
               where (p.peekprofile_id == profileID)
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
        public int getwhoeekedatmenewcount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.peeks
               where (p.peekprofile_id == profileID && p.viewdate == null)
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
        public IPageable<MemberSearchViewModel> getwhopeekedatme(string profileID, int? Page, int? NumberPerPage)
        {
          //  IEnumerable<MemberSearchViewModel> WhoPeekedAtMe = default(IEnumerable<MemberSearchViewModel>);



            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
         var   WhoPeekedAtMe = (from p in _datingcontext.peeks.Where(p => p.peekprofile_id  == profileID)
                             join f in _datingcontext.profiledatas on p.profile_id  equals f.id 
                             join z in _datingcontext.profiles on p.profile_id  equals z.id
                             where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.peekdate ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(WhoPeekedAtMe, Page ?? 1, NumberPerPage.GetValueOrDefault());

        }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public IPageable <MemberSearchViewModel> getwhopeekedatmenew(string profileID, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> PeekNew = default(IEnumerable<MemberSearchViewModel>);

            //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho

            
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var PeekNew = (from p in _datingcontext.peeks .Where(p => p.peekprofile_id  == profileID && p.viewdate  == null)
                       join f in _datingcontext.profiledatas on p.profile_id  equals f.id
                       join z in _datingcontext.profiles on p.profile_id  equals z.id
                       where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.peekdate ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(PeekNew, Page ?? 1, NumberPerPage.GetValueOrDefault());

        }

   

        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public IPageable<MemberSearchViewModel> getwhoipeekedat(string profileID, int? Page, int? NumberPerPage)
        {
            //Page, int? NumberPerPage
            //  List<MemberSearchViewModel> peeks = default(List<MemberSearchViewModel>);        

            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var peeks = (from p in _datingcontext.peeks.Where(p => p.profile_id  == profileID && p.deletedbymemberdate == null)
                          join f in _datingcontext.profiledatas on p.profile_id equals f.id
                         join z in _datingcontext.profiles on p.profile_id  equals z.id
                         where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.creationdate ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(peeks, Page ?? 1, NumberPerPage.GetValueOrDefault());
            
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearchViewModel> getmutualpeeks(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
            return mutualinterests;

        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(string profileid, string targetprofileid)
        {
            return this._datingcontext.peeks.Any(r => r.profile_id == profileid && r.peekprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(string profileID, string targetprofileID)
        {

            //create new inetrest object
            peek peek = new peek();
            //make sure you are not trying to peek at yourself
            if (profileID == targetprofileID) return false;

            //if this was a peek being restored just do that part
            if (checkpeek(profileID, targetprofileID))
            {


            };

            try
            {
                //interest = this._datingcontext.peeks.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                peek.profile_id = profileID;
                peek.peekprofile_id = targetprofileID;
                peek.mutual = 0;  // not dealing with this calulatin yet
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
        public bool removepeekbyprofileid(string profileID, string peekprofile_id)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();
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
        public bool removepeekbypeekprofileid( string peekprofile_id,string profileID)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();
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
        public bool restorepeekbyprofileid(string profileID, string peekprofile_id)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();
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
        public bool restorepeekbypeekprofileid(string peekprofile_id,string profileID)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();
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
        public bool removepeeksbyprofileidandscreennames(string profileID, List<String> screennames)
        {
            try//
            {
                // peeks = this._datingcontext.peeks.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                peek peek = new peek();
                foreach (string value in screennames)
                {
                    string peekprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();
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
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // peeks = this._datingcontext.peeks.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                peek peek = new peek();
                foreach (string value in screennames)
                {
                    string peekprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    peek = this._datingcontext.peeks.Where(p => p.profile_id == profileID && p.peekprofile_id == peekprofile_id).FirstOrDefault();

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
        public bool updatepeekviewstatus(string profileID, string targetprofileid)
        {

            //get the profile
            //profile Profile;
            peek peek = new peek();

            try
            {
                peek = this._datingcontext.peeks.Where(p => p.peekprofile_id == targetprofileid && p.profile_id == profileID).FirstOrDefault();
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
       
        public int getwhoiblockedcount(string profileid)
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
        public IPageable<MemberSearchViewModel> getwhoiblocked(string profileID, int? Page, int? NumberPerPage)
        {
            //IEnumerable<MemberSearchViewModel> MailboxblockNew = default(IEnumerable<MemberSearchViewModel>);


            //var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.ProfileID == ProfileID && p.BlockRemoved == false)
            //                     select new
            //                     {
            //                        ProfilesBlockedId = c.blockprofile_id 
            //                     };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
          var   MailboxblockNew = (from p in _datingcontext.blocks.Where(p => p.profile_id   == profileID && p.removedate == null)
                               join f in _datingcontext.profiledatas on p.blockprofile_id  equals f.id 
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.blockdate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(MailboxblockNew, Page ?? 1, NumberPerPage.GetValueOrDefault());
        }

           /// <summary>
        /// //gets all the members who are Mailboxblocked in me
        /// </summary 
        public IPageable <MemberSearchViewModel> getwhoblockedme(string profileID, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> whoisMailboxblockedinme = default(IEnumerable<MemberSearchViewModel>);



          var  whoblockedme = (from p in _datingcontext.blocks.Where(p => p.blockprofile_id  == profileID && p.removedate == null)
                                        join f in _datingcontext.profiledatas on p.profile_id equals f.id
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.blockdate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(whoblockedme, Page ?? 1, NumberPerPage.GetValueOrDefault());
        }


        #region "update/check/reomve methods"
        
        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearchViewModel> getmutualblocks(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearchViewModel> mutualblocks = default(IEnumerable<MemberSearchViewModel>);
            return mutualblocks;

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(string profileid, string targetprofileid)
        {
            return this._datingcontext.blocks.Any(r => r.profile_id == profileid && r.blockprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(string profileID, string targetprofileID)
        {

            //create new inetrest object
            block block = new block();
            //make sure you are not trying to block at yourself
            if (profileID == targetprofileID) return false;

            //if this was a block being restored just do that part
            if (checkblock(profileID, targetprofileID))
            { 
            
            
            };

            try
            {
                //interest = this._datingcontext.blocks.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                block.profile_id = profileID;
                block.blockprofile_id = targetprofileID;
                block.mutual = 0;  // not dealing with this calulatin yet
                block.creationdate = DateTime.Now;              
                block.reviewdate  = null;
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
        public bool removeblock(string profileID, string blockprofile_id)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.profile_id == profileID && p.blockprofile_id == blockprofile_id).FirstOrDefault();
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
        public bool restoreblock(string profileID, string blockprofile_id)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.profile_id == profileID && p.blockprofile_id == blockprofile_id).FirstOrDefault();
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
        public bool removeblocksbyscreennames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // blocks = this._datingcontext.blocks.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                block block = new block();
                foreach (string value in screennames)
                {
                    string blockprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    block = this._datingcontext.blocks.Where(p => p.profile_id == profileID && p.blockprofile_id == blockprofile_id).FirstOrDefault();

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
        public bool restoreblocksbyscreennames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // blocks = this._datingcontext.blocks.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                block block = new block();
                foreach (string value in screennames)
                {
                    string blockprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    block = this._datingcontext.blocks.Where(p => p.profile_id == profileID && p.blockprofile_id == blockprofile_id).FirstOrDefault();

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
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        public bool updateblockreviewstatus(string profileID,string targetprofileid, string reviewerid)
        {

            //get the profile
            //profile Profile;
            block block = new block();

            try
            {
                block = this._datingcontext.blocks.Where(p => p.blockprofile_id  == targetprofileid && p.profile_id == profileID ).FirstOrDefault();
                //update the profile status to 2            
                block.reviewdate = DateTime.Now;
                block.reviewerprofile_id = profileID;
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
        public int getwhoilikecount(string profileid)
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
        public int getwholikesmecount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.likes
               where (p.likeprofile_id == profileID)
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
        public int getwhoislikesmenewcount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles 
            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            count = (
               from p in _datingcontext.likes
               where (p.likeprofile_id == profileID && p.viewdate == null)
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
        public IPageable <MemberSearchViewModel> getwholikesmenew(string profileID, int? Page, int? NumberPerPage)
        {
          //  IEnumerable<MemberSearchViewModel> LikeNew = default(IEnumerable<MemberSearchViewModel>);



            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate == null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var LikeNew = (from p in _datingcontext.likes.Where(p => p.likeprofile_id  == profileID && p.viewdate  == null)
                        join f in _datingcontext.profiledatas on p.profile_id equals f.id
                       join z in _datingcontext.profiles on p.profile_id  equals z.id 
                       where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                             perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.likedate ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(LikeNew, Page ?? 1, NumberPerPage.GetValueOrDefault());

        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public IPageable <MemberSearchViewModel> getwholikesme(string profileID, int? Page, int? NumberPerPage)
        {
           // IEnumerable<MemberSearchViewModel> _Like = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                    ProfilesBlockedId = c.blockprofile_id 
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var wholikesme = (from p in _datingcontext.likes.Where(p => p.likeprofile_id   == profileID && p.deletedbylikedate == null)
                      join f in _datingcontext.profiledatas on p.profile_id equals f.id
                     join z in _datingcontext.profiles on p.profile_id  equals z.id 
                     where (f.profile.status.id< 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                              perfectmatchsettings   = f.searchsettings.Where(g => g.myperfectmatch  == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                         }).OrderByDescending(f => f.lastlogindate ).ThenByDescending(f => f.likedate  ).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(wholikesme, Page ?? 1, NumberPerPage.GetValueOrDefault());
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public IPageable<MemberSearchViewModel> getwhoilike(string profileID, int? Page, int? NumberPerPage)
        {
            // IEnumerable<MemberSearchViewModel> _Like = default(IEnumerable<MemberSearchViewModel>);


            var MyActiveblocks = from c in _datingcontext.blocks.Where(p => p.profile_id == profileID && p.removedate != null)
                                 select new
                                 {
                                     ProfilesBlockedId = c.blockprofile_id
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            var wholikesme = (from p in _datingcontext.likes.Where(p => p.profile_id  == profileID && p.deletedbymemberdate == null)
                              join f in _datingcontext.profiledatas on p.likeprofile_id  equals f.id
                              join z in _datingcontext.profiles on p.likeprofile_id  equals z.id
                              where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id))
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
                                  perfectmatchsettings = f.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )



                              }).OrderByDescending(f => f.lastlogindate).ThenByDescending(f => f.likedate).ToList();

            return new PaginatedList<MemberSearchViewModel>().GetPageableList(wholikesme, Page ?? 1, NumberPerPage.GetValueOrDefault());
        }


                #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearchViewModel> getmutuallikes(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
            return mutualinterests;

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(string profileid, string targetprofileid)
        {
            return this._datingcontext.likes.Any(r => r.profile_id == profileid && r.likeprofile_id == targetprofileid);
        }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(string profileID, string targetprofileID)
        {

            //create new inetrest object
            like like = new like();
            //make sure you are not trying to like at yourself
            if (profileID == targetprofileID) return false;

            //if this was a like being restored just do that part
            if (checklike(profileID, targetprofileID))
            {


            };

            try
            {
                //interest = this._datingcontext.likes.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                like.profile_id = profileID;
                like.likeprofile_id = targetprofileID;
                like.mutual = 0;  // not dealing with this calulatin yet
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
        public bool removelikebyprofileid(string profileID, string likeprofile_id)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();
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
        public bool removelikebylikeprofileid( string likeprofile_id,string profileID)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();
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
        public bool restorelikebyprofileid(string profileID, string likeprofile_id)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();
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
        public bool restorelikebylikeprofileid(string likeprofile_id,string profileID)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();
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
        public bool removelikesbyprofileidandscreennames(string profileID, List<String> screennames)
        {
            try//
            {
                // likes = this._datingcontext.likes.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                like like = new like();
                foreach (string value in screennames)
                {
                    string likeprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();
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
        public bool restorelikesbyprofileidandscreennames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;         

            try//
            {
                // likes = this._datingcontext.likes.Where(p => p.ProfileID == profileID && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                //update the profile status to 2
                like like = new like();
                foreach (string value in screennames)
                {
                    string likeprofile_id = _membersrepository.getprofileidbyscreenname(value);
                    like = this._datingcontext.likes.Where(p => p.profile_id == profileID && p.likeprofile_id == likeprofile_id).FirstOrDefault();

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
        public bool updatelikeviewstatus(string profileID, string targetprofileid)
        {

            //get the profile
            //profile Profile;
            like like = new like();

            try
            {
                like = this._datingcontext.likes.Where(p => p.likeprofile_id == targetprofileid && p.profile_id == profileID).FirstOrDefault();
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
        //                                            string strSelectedCity, string strSelectedStateProvince, double MaxDistanceFromMe, bool HasPhoto, MembersViewModel model)
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

        //    // ObjectSet <profiledata> products = db.profiledatas ;  


        //    //******** visiblitysettings test code ************************

        //    //// test all the values you are pulling here
        //    //var TestModel = (from x in db.profiledatas.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //    //                      select x).FirstOrDefault();
        //    //  var MinVis = today.AddYears(-(TestModel.ProfileVisiblitySetting.AgeMaxVisibility.GetValueOrDefault() + 1));
        //    // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profiledata.GenderID) ? true : false;

        //    // var testmodel2 = (from x in db.profiledatas.Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //    //                     .Where(z=>z.ProfileVisiblitySetting !=null || z.ProfileVisiblitySetting.ProfileVisiblity == true)
        //    //                     select x).FirstOrDefault();

        //    // Expression<Func<profiledata, bool>> MyWhereExpr = default(Expression<Func<profiledata,  bool>>);



        //    MemberSearchViewmodels = (from x in _datingcontext.profiledatas.Include("ProfileVisiblitySetting").Where(p => p.gender.GenderName == strLookingForSelectedGenderName && p.birthdate > min && p.birthdate <= max)
        //                             .WhereIf(strSelectedCity == "ALL", z => z.countryid == intSelectedCountryId)                                      
        //                              join  f in _datingcontext.profiles on x.ProfileID equals f.profile_id
        //                              // from fp in f  where(x.countryid == intSelectedCountryId)
        //                              //join z in db.photos on x.ProfileID equals z.ProfileID
        //                              select new MemberSearchViewModel
        //                              {
        //                                  // MyCatchyIntroLineQuickSearch = x.AboutMe,
        //                                  ProfileID = x.ProfileID,
        //                                  stateprovince = x.stateprovince,
        //                                  postalcode = x.postalcode,
        //                                  countryid = x.countryid,
        //                                  GenderID = x.GenderID,
        //                                  birthdate = x.birthdate,
        //                                  profile = f,
        //                                  longitude = (double)x.longitude,
        //                                  latitude = (double)x.latitude,
        //                                  HasGalleryPhoto = (from p in _datingcontext.photos.Where(i => i.ProfileID == f.profile_id && i.ProfileImageType == "Gallery") select p.ProfileImageType).FirstOrDefault(),
        //                                  creationdate = f.creationdate,
        //                                  city = _datingcontext.fnTruncateString(x.city, 11),
        //                                  lastloggedonString = _datingcontext.fnGetLastLoggedOnTime(f.LoginDate),
        //                                  lastlogindate = f.LoginDate,
        //                                  Online = _datingcontext.fnGetUserOlineStatus(x.ProfileID),
        //                                  DistanceFromMe = _datingcontext.fnGetDistance((double)x.latitude, (double)x.longitude, model.MyQuickSearch.MySelectedlattitude.Value, model.MyQuickSearch.MySelectedLongitude.Value, "Miles"),
        //                                  ProfileVisibility = x.ProfileVisiblitySetting.ProfileVisiblity

        //                              }).ToList();


        //    //these could be added to where if as well, also limits values if they did selected all
        //    var Profiles = (MaxDistanceFromMe > 0 && strSelectedCity != "ALL") ? (from q in MemberSearchViewmodels.Where(a => a.DistanceFromMe <= MaxDistanceFromMe) select q) : MemberSearchViewmodels.Take(500);
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
