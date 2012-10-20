using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Interfaces;



using System.Web;
using System.Net;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    public class MemberService : IMemberRepository
    {


        private IMemberRepository _memberrepository;
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

            return _memberrepository.getprofiledatabyprofileid(profileid);

        }

        public searchsetting getperfectmatchsearchsettingsbyprofileid(int profileid)
        {

            return _memberrepository.getperfectmatchsearchsettingsbyprofileid(profileid);
        }

        public searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid)
        {

            return _memberrepository.createmyperfectmatchsearchsettingsbyprofileid(profileid);

        }

        //get full profile stuff
        //*****************************************************

        public string getgenderbyphotoid(Guid guid)
        {
            return _memberrepository.getgenderbyphotoid(guid);
        }

        //TO DO this needs to be  linked to roles
        //"Message and Email Quota stuff"
        //***********************************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool checkifquoutareachedandupdate(int profileid)
        {
            return _memberrepository.checkifquoutareachedandupdate(profileid);
        }

        // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //*************************************************************************************************
        //update the database i.e create folders and change profile status from guest to active ?!
        public bool createmailboxfolders(int intprofileid)
        {
            return _memberrepository.createmailboxfolders(intprofileid);

        }

        public bool activateprofile(int intprofileid)
        {
            return _memberrepository.activateprofile(intprofileid);
        }

        public bool deactivateprofile(int intprofileid)
        {
            return _memberrepository.deactivateprofile(intprofileid);
        }
        //updates the profile with a password that is presumed to be already encyrpted
        public bool updatepassword(int profileid, string encryptedpassword)
        {
            return _memberrepository.updatepassword(profileid, encryptedpassword);

        }

        public bool addnewopenidforprofile(int profileid, string openidIdentifer, string openidProvidername)
        {

            return _memberrepository.addnewopenidforprofile(profileid, openidIdentifer, openidProvidername);

        }

        //check if profile is activated 
        public bool checkifprofileisactivated(int intprofileid)
        {

            return _memberrepository.checkifprofileisactivated(intprofileid);

        }

        //check if mailbox folder exist
        public bool checkifmailboxfoldersarecreated(int intprofileid)
        {

            return _memberrepository.checkifmailboxfoldersarecreated(intprofileid);

        }


        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool updateuserlogouttime(int profileid, string sessionID)
        {
          return  _memberrepository.updateuserlogouttime(profileid, sessionID);

        }


        //get the last time the user logged in from profile
        public Nullable<DateTime> getmemberlastlogintime(int profileid)
        {

            return _memberrepository.getmemberlastlogintime(profileid);

        }


        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        public bool updateuserlogintime(string username, string sessionID)
        {

            return _memberrepository.updateuserlogintime(username, sessionID);
        }

        public bool updateuserlogintimebyprofileid(int intprofileid, string sessionID)
        {
            return _memberrepository.updateuserlogintimebyprofileid(intprofileid, sessionID);
        }

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day

        public string getlastloggedinstring(DateTime logindate)
        {

            return _memberrepository.getlastloggedinstring(logindate);

        }

        //returns true if somone logged on
        public bool getuseronlinestatus(int profileid)
        {
            return _memberrepository.getuseronlinestatus(profileid);
        }




        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>

        public bool checkifscreennamealreadyexists(string strscreenname)
        {
            return _memberrepository.checkifscreennamealreadyexists(strscreenname);

        }

        //5-20-2012 added to check if a user email is registered

        public bool checkifprofileidalreadyexists(int profileid)
        {
            return _memberrepository.checkifprofileidalreadyexists(profileid);

        }

        public bool checkifusernamealreadyexists(string strusername)
        {
            return _memberrepository.checkifusernamealreadyexists(strusername);
        }

        public string validatesecurityansweriscorrect(int intprofileid, int SecurityQuestionID, string strSecurityAnswer)
        {
            return _memberrepository.validatesecurityansweriscorrect(intprofileid, SecurityQuestionID, strSecurityAnswer);
        }

        /// <summary>
        /// Determines wethare an activation code matches the value in the database for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(int intprofileid, string strActivationCode)
        {
            return _memberrepository.checkifactivationcodeisvalid(intprofileid, strActivationCode);
        }


        public profile getprofilebyusername(string username)
        {

            return _memberrepository.getprofilebyusername(username);
        }


        public int? getprofileidbyusername(string User)
        {
            return _memberrepository.getprofileidbyusername(User);

        }


        public profile getprofilebyprofileid(int profileid)
        {
            return _memberrepository.getprofilebyprofileid(profileid);

        }


        /// <summary>
        /// added ability to grab from appfabric cache
        /// </summary>
        /// <param name="strusername"></param>
        /// <returns></returns>
        public int? getprofileidbyusername(string strusername)
        {
            return _memberrepository.getprofileidbyusername(strusername);
        }

        public int? getprofileidbyscreenname(string strscreenname)
        {
            return _memberrepository.getprofileidbyscreenname(strscreenname);
        }

        public int? getprofileidbyssessionid(string strsessionid)
        {
            return _memberrepository.getprofileidbyssessionid(strsessionid);
        }

        public string getusernamebyprofileid(int profileid)
        {
            return _memberrepository.getusernamebyprofileid(profileid);
        }

        public string getscreennamebyprofileid(int profileid)
        {
            return _memberrepository.getscreennamebyprofileid(profileid);
        }

        public string getscreennamebyusername(string username)
        {
            return _memberrepository.getscreennamebyusername(username);
        
        }

        public bool checkifemailalreadyexists(string strEmail)
        {
            return _memberrepository.checkifemailalreadyexists(strEmail);
        }





        //Start of stuff pulled from MVC members repository



        public string getgenderbyscreenname(string screenname)
        {
            return _memberrepository.getgenderbyscreenname(screenname);
        }




        public visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid)
        {
            return _memberrepository.getprofilevisibilitysettingsbyprofileid(profileid);

        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getquickmatches(MembersViewModel model)
        {


            return _memberrepository.getquickmatches(model);




        }

        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        public List<MemberSearchViewModel> getemailmatches(MembersViewModel model)
        {



            return _memberrepository.getemailmatches(model);



        }
        public List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model)
        {

            return _memberrepository.getquickmatcheswhenquickmatchesempty(model);

        }
       


    }
}
