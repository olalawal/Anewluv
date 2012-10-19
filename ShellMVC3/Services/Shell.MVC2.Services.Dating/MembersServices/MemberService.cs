using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Data;
using System.Web;
using System.Net;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    public class MemberService : IMemberRepository
    {


        private IMemberRepository  _memberrepository;
        private string _apikey;

        public MemberService(IMemberRepository memberrepository)
            {
                _memberrepository = memberrepository;
                _apikey  = HttpContext.Current.Request.QueryString["apikey"];
                throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }


        //modifed to get search settings as well 
        //get full profile stuff
        //4-28-2012 added profile visibility settings

        public profiledata getprofiledatabyprofileid(int profileid)
        {



        }

        public searchsetting getperfectmatchsearchsettingsbyprofileid(int profileid)
        {


        }

        public searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid)
        {



        }

        //get full profile stuff
        //*****************************************************

        public string getgenderbyphotoid(Guid guid)
        {

        }

        //TO DO this needs to be  linked to roles
        //"Message and Email Quota stuff"
        //***********************************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool checkifquoutareachedandupdate(int profileid)
        {

        }

        // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //*************************************************************************************************
        //update the database i.e create folders and change profile status from guest to active ?!
        public bool createmailboxfolders(int intprofileid)
        {


        }

        public bool activateprofile(int intprofileid)
        {

        }

        public bool deactivateprofile(int intprofileid)
        {

        }
        //updates the profile with a password that is presumed to be already encyrpted
        public bool updatepassword(int profileid, string encryptedpassword)
        {


        }

        public bool addnewopenidforprofile(int profileid, string openidIdentifer, string openidProvidername)
        {



        }

        //check if profile is activated 
        public bool checkifprofileisactivated(int intprofileid)
        {



        }

        //check if mailbox folder exist
        public bool checkifmailboxfoldersarecreated(int intprofileid)
        {



        }


        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool updateuserlogouttime(int profileid, string sessionID)
        {


        }


        //get the last time the user logged in from profile
        public Nullable<DateTime> getmemberlastlogintime(int profileid)
        {



        }


        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        public bool updateuserlogintime(string username, string sessionID)
        {


        }

        public bool updateuserlogintimebyprofileid(int intprofileid, string sessionID)
        {

        }

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day

        public string getlastloggedinstring(DateTime logindate)
        {



        }

        //returns true if somone logged on
        public bool getuseronlinestatus(int profileid)
        {

        }




        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>

        public bool checkifscreennamealreadyexists(string strscreenname)
        {


        }

        //5-20-2012 added to check if a user email is registered

        public bool checkifprofileidalreadyexists(int profileid)
        {


        }

        public bool checkifusernamealreadyexists(string strusername)
        {

        }

        public string validatesecurityansweriscorrect(int intprofileid, int SecurityQuestionID, string strSecurityAnswer)
        {

        }

        /// <summary>
        /// Determines wethare an activation code matches the value in the database for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(int intprofileid, string strActivationCode)
        {

        }


        public profile getprofilebyusername(string username)
        {


        }


        public profile getprofilebyscreenname(string username)
        {



        }

        public int getProfileIdbyusername(string User)
        {


        }


        public profile getprofilebyprofileid(int profileid)
        {


        }


        /// <summary>
        /// added ability to grab from appfabric cache
        /// </summary>
        /// <param name="strusername"></param>
        /// <returns></returns>
        public int? getprofileidbyusername(string strusername)
        {

        }

        public int? getprofileidbyscreenname(string strscreenname)
        {
        }

        public int? getprofileidbyssessionid(string strsessionid)
        {

        }

        public string getusernamebyprofileid(int profileid)
        {

        }

        public string getscreennamebyprofileid(int profileid)
        {

        }

        public string getscreennamebyusername(string username)
        {
        }

        public bool checkifemailalreadyexists(string strEmail)
        {

        }





        //Start of stuff pulled from MVC members repository



        public string getgenderbyscreenname(string screenname)
        {

        }




        public visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid)
        {


        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(MembersViewModel model)
        {







        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(MembersViewModel model)
        {







        }
        public List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model)
        {



        }
       


    }
}
