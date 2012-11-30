using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;

using System.Web;
using System.Net;

using Shell.MVC2.Interfaces;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class MemberActionsService : IMemberActionsService
    {



        private IMemberActionsRepository  _memberactionsrepository;
       // private string _apikey;

        public MemberActionsService(IMemberActionsRepository memberactionsrepository)
            {
                _memberactionsrepository = memberactionsrepository;
               // _apikey  = HttpContext.Current.Request.QueryString["apikey"];
              //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
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
            return _memberactionsrepository.getwhoiaminterestedincount(profileid);
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmecount(int profileid)
        {
            return _memberactionsrepository.getwhoisinterestedinmecount(profileid);
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmenewcount(int profileid)
        {
            return _memberactionsrepository.getwhoisinterestedinmenewcount(profileid);
        }

        #endregion


        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public List<MemberSearchViewModel> getinterests(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getinterests(profileid, Page, NumberPerPage);

        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinme(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwhoisinterestedinme(profileid, Page, NumberPerPage);
        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinmenew(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwhoisinterestedinmenew(profileid, Page, NumberPerPage); ;
        }

        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both interest 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualinterests(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.getmutualinterests(profileid,targetprofileid);

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.checkinterest(profileid, targetprofileid);
     }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(int profileid, int targetprofileid)
        {


            return _memberactionsrepository.addinterest(profileid, targetprofileid);


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyprofileid(int profileid, int interestprofile_id)
        {
            return _memberactionsrepository.removeinterestbyinterestprofileid(profileid, interestprofile_id);

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyinterestprofileid(int interestprofile_id, int profileid)
        {

            return _memberactionsrepository.removeinterestbyinterestprofileid(interestprofile_id, profileid);
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyprofileid(int profileid, int interestprofile_id)
        {

            return _memberactionsrepository.restoreinterestbyprofileid(profileid, interestprofile_id);
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyinterestprofileid(int interestprofile_id, int profileid)
        {
            return _memberactionsrepository.restoreinterestbyinterestprofileid(interestprofile_id, profileid);

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeinterestsbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            return _memberactionsrepository.removeinterestsbyprofileidandscreennames(profileid, screennames);
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreinterestsbyprofileidandscreennames(int profileid, List<String> screennames)
        {


            return _memberactionsrepository.restoreinterestsbyprofileidandscreennames(profileid, screennames);

        }

        /// <summary>
        ///  Update interest with a view     
        /// </summary 
        public bool updateinterestviewstatus(int profileid, int targetprofileid)
        {

            return _memberactionsrepository.updatelikeviewstatus(profileid, targetprofileid);
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
            return _memberactionsrepository.getwhoipeekedatcount(profileid);
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmecount(int profileid)
        {
            return _memberactionsrepository.getwhopeekedatmecount(profileid);
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmenewcount(int profileid)
        {
            return _memberactionsrepository.getwhopeekedatmenewcount(profileid);
        }

        #endregion


        /// <summary>
        /// //gets all the members who are interested in me
        /// //TODO add filtering for blocked members that you blocked and system blocked
        /// </summary 
        public List<MemberSearchViewModel> getwhopeekedatme(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwhopeekedatme(profileid, Page, NumberPerPage);
        }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhopeekedatmenew(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwhopeekedatmenew(profileid, Page, NumberPerPage);
        }



        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public List<MemberSearchViewModel> getwhoipeekedat(int profileid, int? Page, int? NumberPerPage)
        {

            return _memberactionsrepository.getwhoipeekedat(profileid, Page, NumberPerPage);
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualpeeks(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.getmutualpeeks(profileid, targetprofileid);

        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.checkpeek(profileid, targetprofileid);
      
      }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(int profileid, int targetprofileid)
        {

            return _memberactionsrepository.addpeek(profileid, targetprofileid);

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbyprofileid(int profileid, int peekprofile_id)
        {

            return _memberactionsrepository.removepeekbypeekprofileid(profileid, peekprofile_id);

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbypeekprofileid(int peekprofile_id, int profileid)
        {

            return _memberactionsrepository.removepeekbypeekprofileid(peekprofile_id, profileid);
        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbyprofileid(int profileid, int peekprofile_id)
        {


            return _memberactionsrepository.restorepeekbypeekprofileid(profileid, peekprofile_id);

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbypeekprofileid(int peekprofile_id, int profileid)
        {
            return _memberactionsrepository.restorepeekbypeekprofileid(peekprofile_id, profileid);

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removepeeksbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            return _memberactionsrepository.removepeeksbyprofileidandscreennames(profileid, screennames);
        }

        /// <summary>
        ///  //Removes a peek i.e makes is seem like you never peeeked at  anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(int profileid, List<String> screennames)
        {

            return _memberactionsrepository.restorepeeksbyprofileidandscreennames(profileid, screennames);

        }

        /// <summary>
        ///  Update peek with a view     
        /// </summary 
        public bool updatepeekviewstatus(int profileid, int targetprofileid)
        {

            return _memberactionsrepository.updatepeekviewstatus(profileid, targetprofileid);

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

            return _memberactionsrepository.getwhoiblockedcount(profileid);
        }

        /// <summary>
        /// return all    block as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhoiblocked(int profileid, int? Page, int? NumberPerPage)
        {

            return _memberactionsrepository.getwhoiblocked(profileid, Page, NumberPerPage);
        }

        /// <summary>
        /// //gets all the members who areblocked in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoblockedme(int profileid, int? Page, int? NumberPerPage)
        {
          
           return _memberactionsrepository.getwhoblockedme(profileid, Page, NumberPerPage);
        }


        #region "update/check/reomve methods"

        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualblocks(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.getmutualblocks(profileid, targetprofileid);

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.checkblock(profileid, targetprofileid);
      
      }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(int profileid, int targetprofileid)
        {

            return _memberactionsrepository.addblock(profileid, targetprofileid);

        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool removeblock(int profileid, int blockprofile_id)
        {

            return _memberactionsrepository.removeblock(profileid, blockprofile_id);
        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool restoreblock(int profileid, int blockprofile_id)
        {
            return _memberactionsrepository.restoreblock(profileid, blockprofile_id);
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeblocksbyscreennames(int profileid, List<String> screennames)
        {

            return _memberactionsrepository.removeblocksbyscreennames(profileid, screennames);

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreblocksbyscreennames(int profileid, List<String> screennames)
        {


            return _memberactionsrepository.restoreblocksbyscreennames(profileid, screennames);

        }

        //TO DO this needs to me reviewed , all blocks need notes  if reviewed otherwise nothing
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        /// 
        public bool updateblockreviewstatus(int profileid, int targetprofileid, int reviewerid)
        {
            return _memberactionsrepository.updateblockreviewstatus(profileid, targetprofileid, reviewerid);
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
            return _memberactionsrepository.getwhoilikecount (profileid);
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmecount(int profileid)
        {

            return _memberactionsrepository.getwholikesmecount(profileid);
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoislikesmenewcount(int profileid)
        {
            return _memberactionsrepository.getwholikesmenewcount(profileid);
        }

        #endregion



        /// <summary>
        /// return all  new  likes as an object
        /// </summary>
        public List<MemberSearchViewModel> getwholikesmenew(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwholikesmenew(profileid, Page, NumberPerPage);

        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwholikesme(int profileid, int? Page, int? NumberPerPage)
        {

            return _memberactionsrepository.getwholikesme(profileid, Page, NumberPerPage);
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwhoilike(int profileid, int? Page, int? NumberPerPage)
        {
            return _memberactionsrepository.getwhoilike(profileid, Page, NumberPerPage);
        }


        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutuallikes(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.getmutuallikes(profileid, targetprofileid);

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.checklike(profileid, targetprofileid);
       }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(int profileid, int targetprofileid)
        {


            return _memberactionsrepository.addlike(profileid, targetprofileid);


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebyprofileid(int profileid, int likeprofile_id)
        {

            return _memberactionsrepository.removelikebylikeprofileid(profileid, likeprofile_id);
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebylikeprofileid(int likeprofile_id, int profileid)
        {


            return _memberactionsrepository.removelikebylikeprofileid(likeprofile_id, profileid);

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebyprofileid(int profileid, int likeprofile_id)
        {
            return _memberactionsrepository.restorelikebyprofileid(profileid, likeprofile_id);
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebylikeprofileid(int likeprofile_id, int profileid)
        {


            return _memberactionsrepository.restorelikebylikeprofileid(likeprofile_id, profileid);

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removelikesbyprofileidandscreennames(int profileid, List<String> screennames)
        {
            return _memberactionsrepository.removelikesbyprofileidandscreennames(profileid, screennames);
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorelikesbyprofileidandscreennames(int profileid, List<String> screennames)
        {


            return _memberactionsrepository.restorelikesbyprofileidandscreennames(profileid, screennames);
        }

        /// <summary>
        ///  Update like with a view     
        /// </summary 
        public bool updatelikeviewstatus(int profileid, int targetprofileid)
        {
            return _memberactionsrepository.updatelikeviewstatus(profileid, targetprofileid);

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
        //    // bool TestgenderMatch = (TestModel.ProfileVisiblitySetting.GenderID  != null || TestModel.ProfileVisiblitySetting.GenderID == model.profiledata.GenderID) ? true : false;

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
