

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
     using Dating.Server.Data.ViewModels;
    using System.Security.Principal;

    using Dating.Server.Data;
    using System.Data.Objects;


    using System.Data.EntityClient;
    using System.Collections.ObjectModel;
    using System.Web;
    using Shell.MVC2.Infrastructure;
    using Omu.Awesome.Core;



    public partial class DatingService : LinqToEntitiesDomainService<AnewLuvFTSEntities>
    {




       
        



        #region "Getprofile status's function"

        //public bool[] GetProfileStatuses(string profileID,string TargetProfileID)
        //{
            
        //    // Assign an element using enum index.
        //    ProfileStatuses._array[(int)ProfileStatusTypes.BLockedMe ] = true ;


        //    // Loop through enums.
        //    for (ProfileStatusTypes type = ProfileStatusTypes.PeekedAtMe; type < ProfileStatusTypes.Max; type++)
        //    {
        //        Console.Write(type);
        //        Console.Write(' ');
        //        Console.WriteLine(ProfileStatuses._array[(int)type]);
        //    }

        //   bool test=  ProfileStatuses._array[(int)ProfileStatusTypes.PeekedAtMe];

        //  return ProfileStatuses._array;


         

        //}


        #endregion


        //added 1/29/2010 ola lawal
        #region "Profile Update Actions Here, ie likes , friends , intrests mark as abuser etc"
        //no checks i.e invokes to test values here only updates deletes etc , for now on the MVC side
        // they are prevalidated on the model and the booleans are checked on the view  view possible if and if statement exists
        //    remeber emails must be sent on the client side since they user shared reference files    

        //intrest methods
        #region "Intrest methods"


        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>
        [Invoke()]
        public int WhoIamInterestedInCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in ObjectContext.Interests
               where (f.ProfileID  == profileid)
               select f).Count();

            // ?? operator example.
            

            // y = x, unless x is null, in which case y = -1.
           defaultvalue  = count ?? 0;

           

           return defaultvalue ;
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>
        [Invoke()]
        public int WhoisInterestedinmeCount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles and members who are banned
            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            count = (
               from p in ObjectContext.Interests
               join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
               where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
               where (p.InterestID == profileID)
               select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        //Updated to exculde blocks and bannes etc
        /// <summary>
        /// count all interests that have not been viewd by the target
        /// </summary>
        [Invoke()]
        public int WhoisInteredinmeNewCount(string profileID)
        {
            int? count = null;
            int defaultvalue = 0;

            //filter out blocked profiles and members who are banned
            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };


            count = (from p in ObjectContext.Interests  where (p.InterestID == profileID && p.IntrestViewed == false)             
              join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
              where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID)) 
              select f).Count();

             // ?? operator example.


             // y = x, unless x is null, in which case y = -1.
             defaultvalue = count ?? 0;



             return defaultvalue;
        }

        /// <summary>
        /// return all  new  interests as an object
        /// </summary>
        public IEnumerable<MemberSearch> interestNew(string profileID)
        {
            IEnumerable<MemberSearch> interestNew = default(IEnumerable<MemberSearch>);

            //gets all  intrestets from the intrest table based on if the person's profiles are stil lvalid tho

         

            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            interestNew = (from p in ObjectContext.Interests.Where(p => p.InterestID == profileID && p.IntrestViewed == false)
                                   join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                                   join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID 
                                   where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))                               
                                   select new MemberSearch
                                   {
                                       InterestDate = p.InterestDate,
                                       ProfileId = p.ProfileID,
                                       Age = f.Age,
                                       BirthDate = f.Birthdate,
                                       City = f.City,
                                       CountryID = f.CountryID,
                                       State_Province = f.State_Province,
                                       Longitude = f.Longitude,
                                       Latitude = f.Latitude,
                                       GenderID = f.GenderID,
                                       PostalCode = f.PostalCode,
                                       LastLoggedInTime = z.LoginDate,
                                       LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                       ScreenName = f.profile.ScreenName,
                                       MyCatchyIntroLine = f.MyCatchyIntroLine,
                                       AboutMe = f.AboutMe
                                   }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.InterestDate);

            return interestNew;

        }

      

        
        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool AddIntrest(string profileID, string targetprofileID)
        {

            //create new inetrest object
            Interest interest = new Interest();
            //make sure you are not trying to interest yourself
           if (profileID == targetprofileID) return false;
           if  (CheckIntrest(profileID, targetprofileID)) return false;
         


            try
            {
                //interest = this.ObjectContext.Interests.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                interest.ProfileID = profileID;
                interest.InterestID = targetprofileID;
                interest.MutualInterest = 0;  // not dealing with this calulatin yet
                interest.InterestDate = DateTime.Now;
                interest.IntrestViewed = false;
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this.ObjectContext.Interests.AddObject(interest);
                this.ObjectContext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool CheckIntrest(string profileID, string targetprofileID)
        {
            return this.ObjectContext.Interests.Any(r => r.ProfileID == profileID && r.InterestID == targetprofileID);
        }               


      

        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public IEnumerable<MemberSearch> intrests(string profileID)
        {
            IQueryable<MemberSearch> interests = default(IQueryable<MemberSearch>);

            //gets all  intrestets from the intrest table based on if the person's profiles are stil lvalid tho

              //interests = ((from x in (ObjectContext.Interests.Where(p => p.ProfileID == profileID))
              //             join f in ObjectContext.ProfileDatas on x.InterestID equals f.ProfileID
              //             select f) as ObjectQuery<ProfileData>).Include("profile");


             var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
             interests = (from p in ObjectContext.Interests.Where(p => p.ProfileID == profileID)
                                   join f in ObjectContext.ProfileDatas on p.InterestID  equals f.ProfileID
                          join z in ObjectContext.profiles on p.InterestID equals z.ProfileID 
                                   where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))                                 
                                   select new MemberSearch
                                   {
                                       InterestDate = p.InterestDate,
                                       ProfileId = p.ProfileID,
                                       Age = f.Age,
                                       BirthDate = f.Birthdate,
                                       City = f.City,
                                       CountryID = f.CountryID,
                                       State_Province = f.State_Province,
                                       Longitude = f.Longitude,
                                       Latitude = f.Latitude,
                                       GenderID = f.GenderID,
                                       PostalCode = f.PostalCode,
                                       LastLoggedInTime = z.LoginDate,
                                       LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                       ScreenName = f.profile.ScreenName,
                                       MyCatchyIntroLine = f.MyCatchyIntroLine,
                                       AboutMe = f.AboutMe
                                   }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.InterestDate);





             return interests;



         
        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public IEnumerable<MemberSearch> WhoIsInterestedInMe(string profileID)
        {
            IEnumerable<MemberSearch> whoisinterestedinme = default(IEnumerable<MemberSearch>);
     
            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            whoisinterestedinme = (from p in ObjectContext.Interests.Where(p => p.InterestID == profileID)
                                    join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                                    join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID 
                                    where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))                                
                                   select new MemberSearch 
                                    { 
                                     InterestDate = p.InterestDate, 
                                     ProfileId = p.ProfileID ,
                                     Age = f.Age ,
                                     BirthDate= f.Birthdate ,
                                     City = f.City ,
                                     CountryID = f.CountryID ,
                                     State_Province = f.State_Province,
                                     Longitude = f.Longitude ,
                                     Latitude =f.Latitude ,                                        
                                     GenderID= f.GenderID ,
                                     PostalCode = f.PostalCode ,
                                     LastLoggedInTime =  z.LoginDate,
                                      LastLoggedInString =  ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                     ScreenName = f.profile.ScreenName ,
                                     MyCatchyIntroLine = f.MyCatchyIntroLine,
                                     AboutMe = f.AboutMe 

                                    }).OrderByDescending(f=>f.LastLoggedInTime).ThenByDescending(f=>f.InterestDate);

         

            return whoisinterestedinme;
        }


        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public IEnumerable<MemberSearch> WhoIsInterestedInMeNew(string profileID)
        {
            IEnumerable<MemberSearch> whoisinterestedinme = default(IEnumerable<MemberSearch>);


            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            whoisinterestedinme = (from p in ObjectContext.Interests.Where(p => p.InterestID == profileID && p.IntrestViewed == false)
                                   join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                                   join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                                   where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                                   select new MemberSearch
                                   {
                                       InterestDate = p.InterestDate,
                                       ProfileId = p.ProfileID,
                                       Age = f.Age,
                                       BirthDate = f.Birthdate,
                                       City = f.City,
                                       CountryID = f.CountryID,
                                       State_Province = f.State_Province,
                                       Longitude = f.Longitude,
                                       Latitude = f.Latitude,
                                       GenderID = f.GenderID,
                                       PostalCode = f.PostalCode,
                                       LastLoggedInTime = z.LoginDate,
                                       LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                       ScreenName = f.profile.ScreenName,
                                       MyCatchyIntroLine = f.MyCatchyIntroLine,
                                       AboutMe = f.AboutMe
                                   }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.InterestDate);



            return whoisinterestedinme;
        }
        
        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearch> MutualIntrests(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearch> mutualinterests = default(IEnumerable<MemberSearch>);


            return mutualinterests;

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool RemoveInterest(string profileID, string InterestID)
        {

            //get the profile
            //profile Profile;
            Interest Intrests = new Interest();

            try
            {
                Intrests = this.ObjectContext.Interests.Where(p => p.ProfileID == profileID && p.InterestID == InterestID).FirstOrDefault();
                //update the profile status to 2

                this.ObjectContext.DeleteObject(Intrests);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
       
        /// </summary 
        public bool RemoveIntrestsByScreenNames(string profileID, List<String> screennames)
        {

            //get the profile
            //profile Profile;
            Interest Intrests = new Interest();

            try//
            {
               // Intrests = this.ObjectContext.Interests.Where(p => p.ProfileID == profileID && p.InterestID == InterestID).FirstOrDefault();
                //update the profile status to 2

                this.ObjectContext.DeleteObject(Intrests);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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


        /// <summary>
        ///  Update intrest with a view     
        /// </summary 
        public bool UpdateInterestViewStatus(string profileID, string IntrestSenderID)
        {

            //get the profile
            //profile Profile;
            Interest Intrests = new Interest();

            try
            {
                Intrests = this.ObjectContext.Interests.Where(p => p.InterestID  == profileID && p.ProfileID  == IntrestSenderID).FirstOrDefault();
                //update the profile status to 2

                Intrests.IntrestViewed = true;
                Intrests.IntrestViewedDate = DateTime.Now;
               // this.ObjectContext.(Intrests);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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

        #endregion
        //end of Inteerst methods
        //********************************************************

        //profile views in DB= peeks on UI
        #region "peek methods"

        //count methods first
        /// <summary>
        /// count all total Peeks
        /// </summary>
        [Invoke()]
        public int WhoIPeekedAtCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
                from f in ObjectContext.ProfileViews 
                where (f.ProfileViewerID  == profileid)
                select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }


        //count methods first
        /// <summary>
        /// count all total Peeks
        /// </summary>
        [Invoke()]
        public int WhoPeekedatmeCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
                from f in ObjectContext.ProfileViews
                where (f.ProfileID  == profileid)
                select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        /// <summary>
        /// count all Peeks that have not been viewd by the target
        /// </summary>
        [Invoke()]
        public int WhoPeekedatmeNewCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in ObjectContext.ProfileViews 
               where (f.ProfileID  == profileid && f.ProfileViewViewed == false)
               select f).Count();
            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }


        /// <summary>
        /// //gets all the members who are interested in me
        /// //TODO add filtering for blocked members that you blocked and system blocked
        /// </summary 
        public IEnumerable<MemberSearch> WhoPeekedAtMe(string profileID)
        {
            IEnumerable<MemberSearch> WhoPeekedAtMe = default(IEnumerable<MemberSearch>);

          

            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            WhoPeekedAtMe = (from p in ObjectContext.ProfileViews.Where(p => p.ProfileID  == profileID)
                                   join f in ObjectContext.ProfileDatas on p.ProfileViewerID  equals f.ProfileID
                             join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                             where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                             select new MemberSearch
                             {
                                 PeekDate  = p.ProfileViewDate,
                                 ProfileId = p.ProfileID,
                                 Age = f.Age,
                                 BirthDate = f.Birthdate,
                                 City = f.City,
                                 CountryID = f.CountryID,
                                 State_Province = f.State_Province,
                                 Longitude = f.Longitude,
                                 Latitude = f.Latitude,
                                 GenderID = f.GenderID,
                                 PostalCode = f.PostalCode,
                                 LastLoggedInTime = z.LoginDate,
                                 LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                 ScreenName = f.profile.ScreenName,
                                 MyCatchyIntroLine = f.MyCatchyIntroLine,
                                 AboutMe = f.AboutMe
                             }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.PeekDate);


            return WhoPeekedAtMe;

         }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public IEnumerable<MemberSearch> WhoPeekedAtMeNew(string profileID)
        {
            IEnumerable<MemberSearch> PeekNew = default(IEnumerable<MemberSearch>);

            //gets all  intrestets from the intrest table based on if the person's profiles are stil lvalid tho

            


            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            PeekNew = (from p in ObjectContext.ProfileViews.Where(p => p.ProfileID == profileID && p.ProfileViewViewed == false)
                                   join f in ObjectContext.ProfileDatas on p.ProfileViewerID equals f.ProfileID
                                join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                             where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                             select new MemberSearch
                             {
                                 PeekDate  = p.ProfileViewDate,
                                 ProfileId = p.ProfileID,
                                 Age = f.Age,
                                 BirthDate = f.Birthdate,
                                 City = f.City,
                                 CountryID = f.CountryID,
                                 State_Province = f.State_Province,
                                 Longitude = f.Longitude,
                                 Latitude = f.Latitude,
                                 GenderID = f.GenderID,
                                 PostalCode = f.PostalCode,
                                 LastLoggedInTime = z.LoginDate,
                                 LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                 ScreenName = f.profile.ScreenName,
                                 MyCatchyIntroLine = f.MyCatchyIntroLine,
                                 AboutMe = f.AboutMe
                             }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.PeekDate);




            return PeekNew ;

        }


        /// <summary>
        /// Adds a New Peek
        /// </summary>
        /// 
        [RequiresAuthentication]
        public bool AddPeek(string profileID, string targetprofileID)
        {


            //create new inetrest object
            ProfileView Peek = new ProfileView();
            //make sure you are not trying to peek at yourself
            if (profileID == targetprofileID) return false;
            //make sure this is a new peek , this is flip flopped need to change
            if (CheckPeek(targetprofileID,profileID )) return false;

            try
            {
                //Peek = this.ObjectContext.Peeks.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                Peek.ProfileID = targetprofileID ;
                Peek.ProfileViewerID = profileID ;
                Peek.MutualViews = 0;  // not dealing with this calulatin yet
                Peek.ProfileViewDate  = DateTime.Now;
                Peek.ProfileViewViewed  = false;

                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this.ObjectContext.ProfileViews.AddObject(Peek);
                this.ObjectContext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        /// //checks if you already sent and Peek to the target profile
        /// </summary        
        public bool CheckPeek(string ProfileID, string ProfileViewerID)
        {
            return this.ObjectContext.ProfileViews.Any(r => r.ProfileID == ProfileID   && r.ProfileViewerID  == ProfileViewerID);
        }

      

        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public IPageable<MemberSearchViewModel> Peeks(string profileID, int? Page, int? NumberPerPage)
        {
            //Page, int? NumberPerPage
          //  List<MemberSearch> peeks = default(List<MemberSearch>);        

            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
           var peeks = (from p in ObjectContext.ProfileViews.Where(p => p.ProfileViewerID == profileID)
                       join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                     join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                     where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                     select new MemberSearchViewModel 
                     {
                         PeekDate = p.ProfileViewDate,
                         ProfileID   = p.ProfileID,
                         Age = f.Age, 
                          Birthdate   = f.Birthdate,
                         City = f.City,
                         CountryID = f.CountryID,
                         State_Province = f.State_Province,
                         Longitude = (double)f.Longitude,
                         Latitude = (double)f.Latitude,
                         GenderID = f.GenderID,
                         PostalCode = f.PostalCode,
                         LastLoggedInTime = z.LoginDate,
                       //  LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                         ScreenName = f.profile.ScreenName,
                         MyCatchyIntroLine = f.MyCatchyIntroLine,
                         AboutMe = f.AboutMe,
                         PerfectMatchSettings = f.SearchSettings.Where(g=>g.MyPerfectMatch == true).FirstOrDefault()   //GetPerFectMatchSearchSettingsByProfileID(p.ProfileID )

                          

                     }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.PeekDate).ToList();
                    


           return new PaginatedList<MemberSearchViewModel>().GetPageableList(peeks, Page ?? 1, NumberPerPage.GetValueOrDefault());
          

           // return peeks;



            

        }
        
       

        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearch> MutualPeeks(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearch> mutualinterests = default(IEnumerable<MemberSearch>);


            return mutualinterests;

        }


        /// <summary>
        ///  Update Peek with a view     
        /// </summary 
        public bool UpdatePeekViewStatus(string profileID, string ProfileViewerID)
        {

            //get the profile
            //profile Profile;
          ProfileView _profileView = new ProfileView();

            try
            {
                _profileView = this.ObjectContext.ProfileViews.Where(p => p.ProfileID  == profileID   && p.ProfileViewerID == ProfileViewerID  ).FirstOrDefault();
                //update the profile status to 2

                _profileView.ProfileViewViewed  = true;
                _profileView.ProfileViewViewedDate = DateTime.Now;
                // this.ObjectContext.(Intrests);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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

        #endregion


          // MailboxLock methods , right now if you block user from mail they are blocked from your site on everything
        #region "blcok methods"


        //count methods first
        /// <summary>
        /// count all total Mailboxblocks
        /// </summary>
        [Invoke()]
        public int WhoIblockedCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
              from f in ObjectContext.Mailboxblocks 
              where (f.ProfileID  == profileid)
              select f).Count();

            // ?? operator example.
            

            // y = x, unless x is null, in which case y = -1.
           defaultvalue  = count ?? 0;

           

           return defaultvalue ;
        }

     
        /// <summary>
        /// return all    Mailboxblock as an object
        /// </summary>
        public IEnumerable<MemberSearch> WhoIBlocked(string ProfileID)
        {
            IEnumerable<MemberSearch> MailboxblockNew = default(IEnumerable<MemberSearch>);

            
            //var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == ProfileID && p.BlockRemoved == false)
            //                     select new
            //                     {
            //                         ProfilesBlockedId = c.BlockID
            //                     };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            MailboxblockNew = (from p in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == ProfileID)
                         join f in ObjectContext.ProfileDatas on p.BlockID  equals f.ProfileID
                         join z in ObjectContext.profiles on p.BlockID  equals z.ProfileID 
                         where (f.profile.ProfileStatusID < 3)
                         orderby (p.MailboxBlockDate) descending
                         select new MemberSearch
                         {
                             BlockDate  = p.MailboxBlockDate,
                             ProfileId = p.ProfileID,
                             Age = f.Age,
                             BirthDate = f.Birthdate,
                             City = f.City,
                             CountryID = f.CountryID,
                             State_Province = f.State_Province,
                             Longitude = f.Longitude,
                             Latitude = f.Latitude,
                             GenderID = f.GenderID,
                             LastLoggedInTime = z.LoginDate,
                             LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                             ScreenName = f.profile.ScreenName,
                             MyCatchyIntroLine = f.MyCatchyIntroLine,
                             AboutMe = f.AboutMe
                         }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.BlockDate );



            return MailboxblockNew;
        }
        
        /// <summary>
        /// Adds a New Mailboxblock
        /// </summary>
        public bool Addblock(string profileID, string targetprofileID)
        {


            //create new inetrest object
            Mailboxblock Mailboxblock = new Mailboxblock();
            //make sure you are not trying to block yourself
            if (profileID == targetprofileID) return false;
            if  (Checkblock(profileID, targetprofileID)) return false;


            try
            {
                //Mailboxblock = this.ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                Mailboxblock.ProfileID = profileID;
                Mailboxblock.BlockID = targetprofileID;
                Mailboxblock.MailboxBlockDate = DateTime.Now;
                //handele the update using EF
                this.ObjectContext.Mailboxblocks.AddObject(Mailboxblock);
                this.ObjectContext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        /// //checks if you already sent and Mailboxblock to the target profile
        /// </summary        
        public bool Checkblock(string profileID, string targetprofileID)
        {
            return this.ObjectContext.Mailboxblocks.Any(r => r.ProfileID == profileID && r.BlockID == targetprofileID && r.BlockRemoved != true);
        }               

     
        /// <summary>
        /// //gets all the members who are Mailboxblocked in me
        /// </summary 
        public IEnumerable<MemberSearch> WhoblockedMe(string profileID)
        {
            IEnumerable<MemberSearch> whoisMailboxblockedinme = default(IEnumerable<MemberSearch>);



            whoisMailboxblockedinme = (from p in ObjectContext.Mailboxblocks.Where(p => p.BlockID == profileID)
                               join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                                       join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                                       where (f.profile.ProfileStatusID < 3)
                                       orderby (p.MailboxBlockDate) descending
                                       select new MemberSearch
                                       {
                                           BlockDate = p.MailboxBlockDate,
                                           ProfileId = p.ProfileID,
                                           Age = f.Age,
                                           BirthDate = f.Birthdate,
                                           City = f.City,
                                           CountryID = f.CountryID,
                                           State_Province = f.State_Province,
                                           Longitude = f.Longitude,
                                           Latitude = f.Latitude,
                                           GenderID = f.GenderID,
                                           LastLoggedInTime = z.LoginDate,
                                           LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                                           ScreenName = f.profile.ScreenName,
                                           MyCatchyIntroLine = f.MyCatchyIntroLine,
                                           AboutMe = f.AboutMe
                                       }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.BlockDate);



            return whoisMailboxblockedinme;
        }
        
        /// <summary>
        ///  //Removes an iterest i.e makes you not Mailboxblocked in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool Removeblock(string profileID, string targetprofileID)
        {

            //get the profile
            //profile Profile;
              Mailboxblock Mailboxblock = new Mailboxblock();

            try
            {
                Mailboxblock = this.ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockID == targetprofileID && p.BlockRemoved != true).FirstOrDefault();
                //update the profile status to 2
               
                   //update the profile status to 2             
                   Mailboxblock.BlockRemovedDate  = DateTime.Now;
                   Mailboxblock.BlockRemoved = true;
                   //handele the update using EF             
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
        #endregion


        #region "Like methods"

        //count methods first
        /// <summary>
        /// count all total Likes
        /// </summary>
        [Invoke()]
        public int WhoILikeCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
                from f in ObjectContext.Likes 
                where (f.ProfileID  == profileid)
                select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }


        //count methods first
        /// <summary>
        /// count all total Likes
        /// </summary>
        [Invoke()]
        public int WhoLikesMeCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
                from f in ObjectContext.Likes
                where (f.ProfileID == profileid)
                select f).Count();

            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        /// <summary>
        /// count all Likes that have not been viewd by the target
        /// </summary>
        [Invoke()]
        public int WhoLikesMeNewCount(string profileid)
        {
            int? count = null;
            int defaultvalue = 0;

            count = (
               from f in ObjectContext.Likes
               where (f.LikeID  == profileid && f.LikeViewed == false  )
               select f).Count();
            // ?? operator example.


            // y = x, unless x is null, in which case y = -1.
            defaultvalue = count ?? 0;



            return defaultvalue;
        }

        /// <summary>
        /// return all  new  Likes as an object
        /// </summary>
        public IEnumerable<MemberSearch> WhoLikesMeNew(string profileid)
        {
            IEnumerable<MemberSearch> LikeNew = default(IEnumerable<MemberSearch>);





            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileid && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            LikeNew = (from p in ObjectContext.Likes.Where(p => p.LikeID == profileid && p.LikeViewed == false)
                       join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                       join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                       where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                       orderby (p.LikeDate) descending
                       select new MemberSearch
                       {
                           LikeDate = p.LikeDate,
                           ProfileId = p.ProfileID,
                           Age = f.Age,
                           BirthDate = f.Birthdate,
                           City = f.City,
                           CountryID = f.CountryID,
                           State_Province = f.State_Province,
                           Longitude = f.Longitude,
                           Latitude = f.Latitude,
                           GenderID = f.GenderID,
                           PostalCode = f.PostalCode,
                           LastLoggedInTime = z.LoginDate,
                           LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                           ScreenName = f.profile.ScreenName,
                           MyCatchyIntroLine = f.MyCatchyIntroLine,
                           AboutMe = f.AboutMe
                       }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.LikeDate);


            return LikeNew;




            return LikeNew;

        }



        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public IEnumerable<MemberSearch> WhoLikesMe(string profileID)
        {
            IEnumerable<MemberSearch> _Like = default(IEnumerable<MemberSearch>);


            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            _Like = (from p in ObjectContext.Likes.Where(p => p.LikeID == profileID)
                       join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                     join z in ObjectContext.profiles on p.ProfileID equals z.ProfileID
                     where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                     orderby (p.LikeDate) descending
                     select new MemberSearch
                     {
                         LikeDate = p.LikeDate,
                         ProfileId = p.ProfileID,
                         Age = f.Age,
                         BirthDate = f.Birthdate,
                         City = f.City,
                         CountryID = f.CountryID,
                         State_Province = f.State_Province,
                         Longitude = f.Longitude,
                         Latitude = f.Latitude,
                         GenderID = f.GenderID,
                         PostalCode = f.PostalCode,
                         LastLoggedInTime = z.LoginDate,
                         LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                         ScreenName = f.profile.ScreenName,
                         MyCatchyIntroLine = f.MyCatchyIntroLine,
                         AboutMe = f.AboutMe
                     }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.LikeDate);
            

            return _Like;
        }

        /// <summary>
        /// Adds a New Like
        /// </summary>        /// 
        [RequiresAuthentication]
        public bool AddLike(string profileID, string targetprofileID)
        {


            //create new inetrest object
              Like  _like  = new Like();
            //make sure you are not trying to Like at yourself
            if (profileID == targetprofileID) return false;
            if (CheckLike(profileID, targetprofileID)) return false;

            try
            {
                //Like = this.ObjectContext.Likes.Where(p => p.ProfileID == profileID).FirstOrDefault();
                //update the profile status to 2
                _like.LikeID = targetprofileID;
                _like.ProfileID = profileID;
               _like.MutuaLikes = 0;  // not dealing with this calulatin yet
                 _like.LikeDate   = DateTime.Now;
                _like.LikeViewed  = false;

                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                this.ObjectContext.Likes.AddObject(_like);
                this.ObjectContext.SaveChanges();

            }
            catch
            {
                // log the execption message

                return false;
            }

            return true;


        }

        /// <summary>
        /// //checks if you already sent and Like to the target profile
        /// </summary        
        public bool CheckLike(string profileID, string targetprofileID)
        {
            return this.ObjectContext.Likes.Any(r => r.ProfileID == profileID  && r.LikeID  == targetprofileID);
        }

        /// <summary>
        /// //gets list of all the profiles I Like
        /// </summary 
        public IEnumerable<MemberSearch> WhoILike(string profileID)
        {
            IEnumerable<MemberSearch> Likes = default(IEnumerable<MemberSearch>);

            //gets all  intrestets from the intrest table based on if the person's profiles are stil lvalid tho

            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };
            
            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            Likes = (from p in ObjectContext.Likes.Where(p => p.ProfileID == profileID)
                     join f in ObjectContext.ProfileDatas on p.LikeID  equals f.ProfileID
                     join z in ObjectContext.profiles on p.LikeID equals z.ProfileID
                     where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                     select new MemberSearch
                     {
                          LikeDate  = p.LikeDate ,
                         ProfileId = p.ProfileID,
                         Age = f.Age,
                         BirthDate = f.Birthdate,
                         City = f.City,
                         CountryID = f.CountryID,
                         State_Province = f.State_Province,
                         Longitude = f.Longitude,
                         Latitude = f.Latitude,
                         GenderID = f.GenderID,
                         PostalCode = f.PostalCode,
                         LastLoggedInTime = z.LoginDate,
                         LastLoggedInString = ObjectContext.fnGetLastLoggedOnTime(z.LoginDate),
                         ScreenName = f.profile.ScreenName,
                         MyCatchyIntroLine = f.MyCatchyIntroLine,
                         AboutMe = f.AboutMe
                     }).OrderByDescending(f => f.LastLoggedInTime).ThenByDescending(f => f.LikeDate );

           
            return Likes;

        }

     

        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public IEnumerable<MemberSearch> MutualLikes(string profileID, string targetprofileID)
        {
            IEnumerable<MemberSearch> mutualinterests = default(IEnumerable<MemberSearch>);
            return mutualinterests;

        }
        /// <summary>
        ///  Update Peek with a view     
        /// </summary 
        public bool UpdateLikeViewStatus(string ProfileID, string ProfileLikerID )
        {

            //get the profile
            //profile Profile;
            Like _Like = new Like();

            try
            {
                _Like = this.ObjectContext.Likes.Where(p => p.LikeID == ProfileID   && p.ProfileID   == ProfileLikerID).FirstOrDefault();
                //update the profile status to 2

                _Like.LikeViewed = true;
                _Like.LikeViewedDate = DateTime.Now;
                // this.ObjectContext.(Intrests);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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

        /// <summary>
        ///  //Removes a like 
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool RemoveLike(string profileID, string LikeID)
        {

            //get the profile
            //profile Profile;
            Like _Like = new Like();

            try
            {
                _Like = this.ObjectContext.Likes.Where(p => p.ProfileID == profileID && p.LikeID  == LikeID).FirstOrDefault();
                //update the profile status to 2

                this.ObjectContext.DeleteObject(_Like);
                //handele the update using EF
                // this.ObjectContext.profiles.AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
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


        #endregion


        #endregion


        #region "get result sets sunch as matches etc"


        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public IEnumerable<MemberSearch> MyQuickMatches(string profileID)
        {
            IQueryable<MemberSearch> QuickMatches = default(IQueryable<MemberSearch>);

            //gets all  intrestets from the intrest table based on if the person's profiles are stil lvalid tho

            //QuickMatches  = ((from x in (ObjectContext.ProfileDatas.Include("photos")
            //              .Where(p => p.GenderID  ==  1 ))
            //              join f in ObjectContext.photos  on x.ProfileID   equals f.ProfileID
            //              select f) as ObjectQuery<ProfileData>).Include("profile");


            var MyActiveblocks = from c in ObjectContext.Mailboxblocks.Where(p => p.ProfileID == profileID && p.BlockRemoved == false)
                                 select new
                                 {
                                     ProfilesBlockedId = c.BlockID
                                 };

            //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
            //rematerialize on the back end.
            //final query to send back only the profile datatas of the interests we want
            QuickMatches = (from p in ObjectContext.Likes.Where(p => p.ProfileID == profileID)
                     join f in ObjectContext.ProfileDatas on p.ProfileID equals f.ProfileID
                     where (f.profile.ProfileStatusID < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.ProfileID))
                     orderby (p.LikeDate) descending
                     select new MemberSearch
                     {
                         LikeDate = p.LikeDate,
                         ProfileId = p.ProfileID,
                         Age = f.Age,
                         BirthDate = f.Birthdate,
                         City = f.City,
                         CountryID = f.CountryID,
                         State_Province = f.State_Province,
                         Longitude = f.Longitude,
                         Latitude = f.Latitude,
                         GenderID = f.GenderID,
                         PostalCode = f.PostalCode,
                         LastLoggedInTime = f.profile.LoginDate,
                         ScreenName = f.profile.ScreenName,
                         MyCatchyIntroLine = f.MyCatchyIntroLine,
                         AboutMe = f.AboutMe 
                     });




            //interests = ((from x in (ObjectContext.Interests.Where(p => p.ProfileID == profileID))
            //              join f in ObjectContext.ProfileDatas on x.InterestID equals f.ProfileID
            //              select f) as ObjectQuery<ProfileData>).Include("profile");



            return QuickMatches;

        }


        #endregion



    }
}
