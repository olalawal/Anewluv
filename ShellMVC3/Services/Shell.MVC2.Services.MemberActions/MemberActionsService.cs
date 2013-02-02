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

namespace Shell.MVC2.Services.Actions
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
        public int getwhoiaminterestedincount(string profileid)
        {
            return _memberactionsrepository.getwhoiaminterestedincount(Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmecount(string profileid)
        {
            return _memberactionsrepository.getwhoisinterestedinmecount(Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmenewcount(string profileid)
        {
            return _memberactionsrepository.getwhoisinterestedinmenewcount(Convert.ToInt32(profileid));
        }

        #endregion


        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public List<MemberSearchViewModel> getinterests(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getinterests(Convert.ToInt32(profileid),Convert.ToInt32(page) ,Convert.ToInt32(numberperpage));

        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinme(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwhoisinterestedinme(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinmenew(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwhoisinterestedinmenew(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage)); ;
        }

        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both interest 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualinterests(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.getmutualinterests(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.checkinterest(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));
     }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(string profileid, string targetprofileid)
        {


            return _memberactionsrepository.addinterest(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyprofileid(string profileid, string interestprofile_id)
        {
            return _memberactionsrepository.removeinterestbyinterestprofileid(Convert.ToInt32(profileid),Convert.ToInt32(interestprofile_id));

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {

            return _memberactionsrepository.removeinterestbyinterestprofileid(Convert.ToInt32(interestprofile_id), Convert.ToInt32(profileid));
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyprofileid(string profileid,string interestprofile_id)
        {

            return _memberactionsrepository.restoreinterestbyprofileid(Convert.ToInt32(profileid),Convert.ToInt32(interestprofile_id));
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {
            return _memberactionsrepository.restoreinterestbyinterestprofileid(Convert.ToInt32(interestprofile_id), Convert.ToInt32(profileid));

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {
            return _memberactionsrepository.removeinterestsbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            return _memberactionsrepository.restoreinterestsbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

        }

        /// <summary>
        ///  Update interest with a view     
        /// </summary 
        public bool updateinterestviewstatus(string profileid, string targetprofileid)
        {

            return _memberactionsrepository.updatelikeviewstatus(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));
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
            return _memberactionsrepository.getwhoipeekedatcount(Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmecount(string profileid)
        {
            return _memberactionsrepository.getwhopeekedatmecount(Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmenewcount(string profileid)
        {
            return _memberactionsrepository.getwhopeekedatmenewcount(Convert.ToInt32(profileid));
        }

        #endregion


        /// <summary>
        /// //gets all the members who are interested in me
        /// //TODO add filtering for blocked members that you blocked and system blocked
        /// </summary 
        public List<MemberSearchViewModel> getwhopeekedatme(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwhopeekedatme(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhopeekedatmenew(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwhopeekedatmenew(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }



        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public List<MemberSearchViewModel> getwhoipeekedat(string profileid, string page, string numberperpage)
        {

            return _memberactionsrepository.getwhoipeekedat(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualpeeks(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.getmutualpeeks(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.checkpeek(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));
      
      }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(string profileid, string targetprofileid)
        {

            return _memberactionsrepository.addpeek(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbyprofileid(string profileid, string peekprofile_id)
        {

            return _memberactionsrepository.removepeekbypeekprofileid(Convert.ToInt32(profileid), Convert.ToInt32(peekprofile_id));

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbypeekprofileid(string peekprofile_id, string profileid)
        {

            return _memberactionsrepository.removepeekbypeekprofileid(Convert.ToInt32(peekprofile_id), Convert.ToInt32(profileid));
        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbyprofileid(string profileid, string peekprofile_id)
        {


            return _memberactionsrepository.restorepeekbypeekprofileid(Convert.ToInt32(profileid), Convert.ToInt32(peekprofile_id));

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbypeekprofileid(string peekprofile_id, string profileid)
        {
            return _memberactionsrepository.restorepeekbypeekprofileid( Convert.ToInt32(peekprofile_id), Convert.ToInt32(profileid));

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {
            return _memberactionsrepository.removepeeksbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);
        }

        /// <summary>
        ///  //Removes a peek i.e makes is seem like you never peeeked at  anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            return _memberactionsrepository.restorepeeksbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

        }

        /// <summary>
        ///  Update peek with a view     
        /// </summary 
        public bool updatepeekviewstatus(string profileid, string targetprofileid)
        {

            return _memberactionsrepository.updatepeekviewstatus(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

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

            return _memberactionsrepository.getwhoiblockedcount(Convert.ToInt32(profileid));
        }

        /// <summary>
        /// return all    block as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhoiblocked(string profileid, string page, string numberperpage)
        {

            return _memberactionsrepository.getwhoiblocked(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }

        /// <summary>
        /// //gets all the members who areblocked in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoblockedme(string profileid, string page, string numberperpage)
        {
          
           return _memberactionsrepository.getwhoblockedme(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }


        #region "update/check/reomve methods"

        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualblocks(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.getmutualblocks(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.checkblock(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));
      
      }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(string profileid, string targetprofileid)
        {

            return _memberactionsrepository.addblock(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool removeblock(string profileid, string blockprofile_id)
        {

            return _memberactionsrepository.removeblock(Convert.ToInt32(profileid), Convert.ToInt32(blockprofile_id));
        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool restoreblock(string profileid, string blockprofile_id)
        {
            return _memberactionsrepository.restoreblock(Convert.ToInt32(profileid), Convert.ToInt32(blockprofile_id));
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeblocksbyscreennames(string profileid, List<String> screennames)
        {

            return _memberactionsrepository.removeblocksbyscreennames(Convert.ToInt32(profileid), screennames);

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreblocksbyscreennames(string profileid, List<String> screennames)
        {


            return _memberactionsrepository.restoreblocksbyscreennames(Convert.ToInt32(profileid), screennames);

        }

        //TO DO this needs to me reviewed , all blocks need notes  if reviewed otherwise nothing
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        /// 
        public bool updateblockreviewstatus(string profileid, string targetprofileid, string reviewerid)
        {
            return _memberactionsrepository.updateblockreviewstatus(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid), Convert.ToInt32( reviewerid));
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
            return _memberactionsrepository.getwhoilikecount (Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmecount(string profileid)
        {

            return _memberactionsrepository.getwholikesmecount(Convert.ToInt32(profileid));
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoislikesmenewcount(string profileid)
        {
            return _memberactionsrepository.getwholikesmenewcount(Convert.ToInt32(profileid));
        }

        #endregion



        /// <summary>
        /// return all  new  likes as an object
        /// </summary>
        public List<MemberSearchViewModel> getwholikesmenew(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwholikesmenew(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));

        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwholikesme(string profileid, string page, string numberperpage)
        {

            return _memberactionsrepository.getwholikesme(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwhoilike(string profileid, string page, string numberperpage)
        {
            return _memberactionsrepository.getwhoilike(Convert.ToInt32(profileid),Convert.ToInt32(page),Convert.ToInt32(numberperpage));
        }


        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutuallikes(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.getmutuallikes(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.checklike(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));
       }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(string profileid, string targetprofileid)
        {


            return _memberactionsrepository.addlike(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebyprofileid(string profileid, string likeprofile_id)
        {

            return _memberactionsrepository.removelikebylikeprofileid(Convert.ToInt32(profileid), Convert.ToInt32(likeprofile_id));
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebylikeprofileid(string likeprofile_id, string profileid)
        {


            return _memberactionsrepository.removelikebylikeprofileid(Convert.ToInt32(likeprofile_id), Convert.ToInt32(profileid));

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebyprofileid(string profileid, string likeprofile_id)
        {
            return _memberactionsrepository.restorelikebyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(likeprofile_id));
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebylikeprofileid(string likeprofile_id, string profileid)
        {


            return _memberactionsrepository.restorelikebylikeprofileid(Convert.ToInt32(likeprofile_id), Convert.ToInt32(profileid));

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {
            return _memberactionsrepository.removelikesbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            return _memberactionsrepository.restorelikesbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);
        }

        /// <summary>
        ///  Update like with a view     
        /// </summary 
        public bool updatelikeviewstatus(string profileid, string targetprofileid)
        {
            return _memberactionsrepository.updatelikeviewstatus(Convert.ToInt32(profileid),Convert.ToInt32(targetprofileid));

        }

        #endregion


        #endregion

        #region "Search methods"

        

        #endregion


    }
}
