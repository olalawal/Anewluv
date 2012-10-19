using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMembersService" in both code and config file together.
    [ServiceContract]
    public interface IMemberService
    {
        //TO Do posibly move this to a separate service for benchmarking
        //member viewmodoem mapping and registration models mappers

        //TO DO move to unit test
        // RegisterModel mapregistrationtest();  
        //end of profile mapping

        //initial profile stuffs
        [WebGet]
        [OperationContract]
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyusername(string username);
        profiledata getprofiledatabyprofileid(int profileid);
        [WebGet]
        [OperationContract]
        searchsetting getperfectmatchsearchsettingsbyprofileid(int profileid);
       [WebInvoke]
        [OperationContract]
        searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid);

        //get full profile stuff    
        [WebGet]
        [OperationContract]
        string getgenderbyscreenname(string strScreenName);
        [WebGet]
        [OperationContract]
        string getgenderbyphotoid(Guid guid);
        //TO DO this needs to be  linked to roles

        //Message and Email Quota stuff
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        [WebGet]
        [OperationContract]
        bool checkifquoutareachedandupdate(int profileid);
        //Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //update the database i.e create folders and change profile status from guest to active ?!
       [WebInvoke]
        [OperationContract]
        bool createmailboxfolders(int strProfileID);
         [WebInvoke]
        [OperationContract]
        bool activateprofile(int strProfileID);

        //updates the profile with a password that is presumed to be already encyrpted
        [WebInvoke]
        [OperationContract]
        bool updatepassword(int profileid, string encryptedpassword);
        [WebInvoke]
        [OperationContract]
        bool addnewopenidforprofile(int profileid, string openidIdentifer, string openidProvidername);
        //check if profile is activated 
        [WebGet]
        [OperationContract]
        bool checkifprofileisactivated(int strProfileID);
        //check if mailbox folder exist
        [WebGet]
        [OperationContract]
        bool checkifmailboxfoldersarecreated(int strProfileID);

        //DateTimeFUcntiosn for longin etc "
        //********************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        [WebGet]
        [OperationContract]
        bool updateuserlogouttime(int profileid, string sessionID);
        //get the last time the user logged in from profile
        [WebGet]
        [OperationContract]
        Nullable<DateTime> getmemberlastlogintime(int profileid);

        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        [WebInvoke]
        [OperationContract]
        bool updateuserlogintime(string username, string sessionID);
        [WebInvoke]
        [OperationContract]
        bool updateuserlogintimebyprofileid(int profileid, string sessionID);

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day
        [WebGet]
        [OperationContract]
        string getlastloggedinstring(DateTime LoginDate);
        //returns true if somone logged on
        [WebGet]
        [OperationContract]
        bool getuseronlinestatus(int profileid);

        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>      
        [WebGet]
        [OperationContract]
        bool checkifscreennamealreadyexists(string strScreenName);
        //5-20-2012 added to check if a user email is registered  
        [WebGet]
        [OperationContract]
        bool checkifprofileidalreadyexists(int profileid);
        [WebGet ]
        [OperationContract]
        string validatesecurityansweriscorrect(int strProfileID, int SecurityQuestionID, string strSecurityAnswer);
        [WebGet]
        [OperationContract]
        int? getprofileidbyusername(string strusername);
        [WebGet]
        [OperationContract]
        int? getprofileidbyscreenname(string strscreenname);
        [WebGet]
        [OperationContract]
        int? getprofileidbyssessionid(string sessionid);
        [WebGet]
        [OperationContract]
        string getusernamebyprofileid(int profileid);
        [WebGet]
        [OperationContract]
        string getscreennamebyprofileid(int profileid);
        [WebGet]
        [OperationContract]
        string getscreennamebyusername(string username);
        [WebGet]
        [OperationContract]
        bool checkifemailalreadyexists(string strEmail);
        // added by Deshola on 5/17/2011       
        //   byte[] GetGalleryPhotobyPhotoID(Guid strPhotoID);   
        //   byte[] GetGalleryPhotobyProfileID(int strProfileID); 
        //    byte[] GetGalleryPhotobyScreenName(string strScreenName);
        //    byte[] GetGalleryImagebyNormalizedScreenName(string strScreenName);   
        // bool InsertPhotoCustom(Shell.MVC2.Domain.Entities.Anewluv.photo newphoto);
        // bool CheckIfPhotoCaptionAlreadyExists(int strProfileID, string strPhotoCaption);  
        /// <summary>
        /// Determines wethare an activation code matches the value in the database for a given profileID
        /// </summary>
        
        [WebGet]
        [OperationContract]
        bool checkifactivationcodeisvalid(int strProfileID, string strActivationCode);
        //  bool CheckForGalleryPhotobyProfileID(int strProfileID);
        //  bool CheckForUploadedPhotobyProfileID(int strProfileID);


        //Hereis where the members Repository stuff that was in the MVC project starts at
        //************************************************************************************
        [WebGet]
        [OperationContract]
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyprofileid(int profileid);
        [WebInvoke ]
        [OperationContract]
        bool deactivateprofile(int profileid);
        [WebGet]
        [OperationContract]
        visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid);


        //TO DO move these functions to a Matches or Search Services Area and repoisitory eventually
        //***********************************************************************************************
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getquickmatches(MembersViewModel model);
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getemailmatches(MembersViewModel model);
        [WebGet]
        [OperationContract]
        List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model);

        //mapper calls that use the appfabric cache
    }
}
