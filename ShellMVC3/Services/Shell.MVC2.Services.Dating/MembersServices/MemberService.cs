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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
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

        public profiledata getprofiledatabyprofileid(ProfileModel model)
        {

            return _memberrepository.getprofiledatabyprofileid(model);

        }

        public searchsetting getperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {

            return _memberrepository.getperfectmatchsearchsettingsbyprofileid(model);
        }

        public searchsetting createmyperfectmatchsearchsettingsbyprofileid(ProfileModel model)
        {

            return _memberrepository.createmyperfectmatchsearchsettingsbyprofileid(model);

        }

        //get full profile stuff
        //*****************************************************

        public string getgenderbyphotoid(ProfileModel model)
        {
            return _memberrepository.getgenderbyphotoid(model);
        }

        //TO DO this needs to be  linked to roles
        //"Message and Email Quota stuff"
        //***********************************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        public bool checkifquoutareachedandupdate(ProfileModel model)
        {
            return _memberrepository.checkifquoutareachedandupdate(model);
        }

        // "Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //*************************************************************************************************
        //update the database i.e create folders and change profile status from guest to active ?!
        public bool createmailboxfolders(ProfileModel model)
        {
            return _memberrepository.createmailboxfolders(model);

        }

        public bool activateprofile(ProfileModel model)
        {
            return _memberrepository.activateprofile(model);
        }

        public bool deactivateprofile(ProfileModel model)
        {
            return _memberrepository.deactivateprofile(model);
        }
        //updates the profile with a password that is presumed to be already encyrpted
        public bool updatepassword(ProfileModel model, string encryptedpassword)
        {
            return _memberrepository.updatepassword( model, encryptedpassword);

        }

        public bool addnewopenidforprofile(ProfileModel model)
        {

            return _memberrepository.addnewopenidforprofile(model);

        }

        //check if profile is activated 
        public bool checkifprofileisactivated(ProfileModel model)
        {

            return _memberrepository.checkifprofileisactivated(model);

        }

        //check if mailbox folder exist
        public bool checkifmailboxfoldersarecreated(ProfileModel model)
        {

            return _memberrepository.checkifmailboxfoldersarecreated(model);

        }


   
      

        //get the last time the user logged in from profile
        public Nullable<DateTime> getmemberlastlogintimebyprofileid(ProfileModel model)
        {

            return _memberrepository.getmemberlastlogintimebyprofileid(model);

        }

        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        public bool updateuserlogintimebyprofileidandsessionid(ProfileModel model)
        {
            return _memberrepository.updateuserlogintimebyprofileidandsessionid(model);

        }


        //"DateTimeFUcntiosn for longin etc "
        //**********************************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal

        public bool updateuserlogouttimebyprofileid(ProfileModel model)
        {

            return _memberrepository.updateuserlogouttimebyprofileid (model);
        }

        public bool updateuserlogintimebyprofileid(ProfileModel model)
        {
            return _memberrepository.updateuserlogintimebyprofileid(model);
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
        public bool getuseronlinestatus(ProfileModel model)
        {
            return _memberrepository.getuseronlinestatus(model);
        }




        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>

        public bool checkifscreennamealreadyexists(ProfileModel model)
        {
            return _memberrepository.checkifscreennamealreadyexists(model);

        }

        //5-20-2012 added to check if a user email is registered

        //public bool checkifusernamealreadyexists(ProfileModel model)
        //{
        //    return _memberrepository.checkifusernamealreadyexists(model);

        //}

        public bool checkifusernamealreadyexists(ProfileModel model)
        {
            return _memberrepository.checkifusernamealreadyexists(model);
        }

        public string validatesecurityansweriscorrect(ProfileModel model)
        {
            return _memberrepository.validatesecurityansweriscorrect((model));
        }

        /// <summary>
        /// Determines wethare an activation code matches the value in the database for a given profileid
        /// </summary>
        public bool checkifactivationcodeisvalid(ProfileModel model)
        {
            return _memberrepository.checkifactivationcodeisvalid(model);
        }


        public profile getprofilebyusername(ProfileModel model)
        {

            return _memberrepository.getprofilebyusername(model);
        }


        //public int? getprofileidbyusername(string User)
        //{
        //    return _memberrepository.getprofileidbyusername(User);

        //}


        public profile getprofilebyprofileid(ProfileModel model)
        {
            return _memberrepository.getprofilebyprofileid(model);

        }

       public profile getprofilebyemailaddress(ProfileModel model)
       {
           return _memberrepository.getprofilebyemailaddress(model);
       }


        /// <summary>
        /// added ability to grab from appfabric cache
        /// </summary>
        /// <param name="strusername"></param>
        /// <returns></returns>
       public int? getprofileidbyusername(ProfileModel model)
        {
            return _memberrepository.getprofileidbyusername(model);
        }

        public int?  getprofileidbyopenid(ProfileModel model)
        {
            return _memberrepository.getprofileidbyopenid(model);
        }
      

       public int? getprofileidbyscreenname(ProfileModel model)
        {
            return _memberrepository.getprofileidbyscreenname(model);
        }

       public int? getprofileidbyssessionid(ProfileModel model)
        {
            return _memberrepository.getprofileidbyssessionid(model);
        }

        public string getusernamebyprofileid(ProfileModel model)
        {
            return _memberrepository.getusernamebyprofileid(model);
        }

        public string getscreennamebyprofileid(ProfileModel model)
        {
            return _memberrepository.getscreennamebyprofileid(model);
        }

        public string getscreennamebyusername(ProfileModel model)
        {
            return _memberrepository.getscreennamebyusername(model);
        
        }

        public bool checkifemailalreadyexists(ProfileModel model)
        {
            return _memberrepository.checkifemailalreadyexists(model);
        }





        //Start of stuff pulled from MVC members repository



        public string getgenderbyscreenname(ProfileModel model)
        {
            return _memberrepository.getgenderbyscreenname(model);
        }




        public visiblitysetting getprofilevisibilitysettingsbyprofileid(ProfileModel model)
        {
            return _memberrepository.getprofilevisibilitysettingsbyprofileid(model);

        }

       


    }
}
