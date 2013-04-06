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
using System.ServiceModel.Activation;


namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class MemberService : IMemberService 
    {


        private IMemberRepository _memberrepository;
       // private string _apikey;

        public MemberService(IMemberRepository memberrepository)
            {
                _memberrepository = memberrepository;
              //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
             //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }


        //modifed to get search settings as well 
        //get full profile stuff
        //4-28-2012 added profile visibility settings

        public profiledata getprofiledatabyprofileid(string profileid)
        {

            return _memberrepository.getprofiledatabyprofileid(Convert.ToInt32(profileid));

        }

        public searchsetting getperfectmatchsearchsettingsbyprofileid(string profileid)
        {

            return _memberrepository.getperfectmatchsearchsettingsbyprofileid(Convert.ToInt32(profileid));
        }

        public searchsetting createmyperfectmatchsearchsettingsbyprofileid(string profileid)
        {

            return _memberrepository.createmyperfectmatchsearchsettingsbyprofileid(Convert.ToInt32(profileid));

        }

        //get full profile stuff
        //*****************************************************

        public string getgenderbyphotoid(string guid)
        {
            return _memberrepository.getgenderbyphotoid( Guid.Parse(guid));
        }

        //TO DO this needs to be  linked to roles
        //"Message and Email Quota stuff"
        //***********************************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool checkifquoutareachedandupdate(string profileid)
        {
            return _memberrepository.checkifquoutareachedandupdate(Convert.ToInt32(profileid));
        }

        // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //*************************************************************************************************
        //update the database i.e create folders and change profile status from guest to active ?!
        public bool createmailboxfolders(string profileid)
        {
            return _memberrepository.createmailboxfolders(Convert.ToInt32(profileid));

        }

        public bool activateprofile(string profileid)
        {
            return _memberrepository.activateprofile(Convert.ToInt32(profileid));
        }

        public bool deactivateprofile(string profileid)
        {
            return _memberrepository.deactivateprofile(Convert.ToInt32(profileid));
        }
        //updates the profile with a password that is presumed to be already encyrpted
        public bool updatepassword(string profileid, string encryptedpassword)
        {
            return _memberrepository.updatepassword( Convert.ToInt32(profileid), encryptedpassword);

        }

        public bool addnewopenidforprofile(string profileid, string openidIdentifer, string openidProvidername)
        {

            return _memberrepository.addnewopenidforprofile(Convert.ToInt32(profileid), openidIdentifer, openidProvidername);

        }

        //check if profile is activated 
        public bool checkifprofileisactivated(string profileid)
        {

            return _memberrepository.checkifprofileisactivated(Convert.ToInt32(profileid));

        }

        //check if mailbox folder exist
        public bool checkifmailboxfoldersarecreated(string profileid)
        {

            return _memberrepository.checkifmailboxfoldersarecreated(Convert.ToInt32(profileid));

        }


        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool updateuserlogouttime(string profileid, string sessionID)
        {
            return _memberrepository.updateuserlogouttime(Convert.ToInt32(profileid), sessionID);

        }


        //get the last time the user logged in from profile
        public Nullable<DateTime> getmemberlastlogintime(string profileid)
        {

            return _memberrepository.getmemberlastlogintime(Convert.ToInt32(profileid));

        }


        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        public bool updateuserlogintime(string username, string sessionID)
        {

            return _memberrepository.updateuserlogintime(username, sessionID);
        }

        public bool updateuserlogintimebyprofileid(string profileid, string sessionID)
        {
            return _memberrepository.updateuserlogintimebyprofileid(Convert.ToInt32(profileid), sessionID);
        }

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day

        public string getlastloggedinstring(string logindate)
        {

            return _memberrepository.getlastloggedinstring(DateTime.Parse(logindate));

        }

        //returns true if somone logged on
        public bool getuseronlinestatus(string profileid)
        {
            return _memberrepository.getuseronlinestatus(Convert.ToInt32(profileid));
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

        public bool checkifprofileidalreadyexists(string profileid)
        {
            return _memberrepository.checkifprofileidalreadyexists(Convert.ToInt32(profileid));

        }

        public bool checkifusernamealreadyexists(string strusername)
        {
            return _memberrepository.checkifusernamealreadyexists(strusername);
        }

        public string validatesecurityansweriscorrect(string profileid, string SecurityQuestionID, string strSecurityAnswer)
        {
            return _memberrepository.validatesecurityansweriscorrect(Convert.ToInt32(profileid), Convert.ToInt32(SecurityQuestionID), strSecurityAnswer);
        }

        /// <summary>
        /// Determines wethare an activation code matches the value in the database for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(string profileid, string strActivationCode)
        {
            return _memberrepository.checkifactivationcodeisvalid(Convert.ToInt32(profileid), strActivationCode);
        }


        public profile getprofilebyusername(string username)
        {

            return _memberrepository.getprofilebyusername(username);
        }


        //public int? getprofileidbyusername(string User)
        //{
        //    return _memberrepository.getprofileidbyusername(User);

        //}


        public profile getprofilebyprofileid(string profileid)
        {
            return _memberrepository.getprofilebyprofileid(Convert.ToInt32(profileid));

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

        public int? getprofileidbyscreenname(string screenname)
        {
            return _memberrepository.getprofileidbyscreenname(screenname);
        }

        public int? getprofileidbyssessionid(string strsessionid)
        {
            return _memberrepository.getprofileidbyssessionid(strsessionid);
        }

        public string getusernamebyprofileid(string profileid)
        {
            return _memberrepository.getusernamebyprofileid(Convert.ToInt32(profileid));
        }

        public string getscreennamebyprofileid(string profileid)
        {
            return _memberrepository.getscreennamebyprofileid(Convert.ToInt32(profileid));
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




        public visiblitysetting getprofilevisibilitysettingsbyprofileid(string profileid)
        {
            return _memberrepository.getprofilevisibilitysettingsbyprofileid(Convert.ToInt32(profileid));

        }

       


    }
}
