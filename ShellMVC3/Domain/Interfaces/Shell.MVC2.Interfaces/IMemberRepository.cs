using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Data
{
   public interface IMemberRepository
   {


       //TO Do posibly move this to a separate service for benchmarking
       //member viewmodoem mapping and registration models mappers
     
       //TO DO move to unit test
       // RegisterModel mapregistrationtest();  
       //end of profile mapping

       //initial profile stuffs
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyusername(string username);
        profiledata getprofiledatabyprofileid(int profileid) ;
        searchsetting getperfectmatchsearchsettingsbyprofileid(int profileid);
        searchsetting createmyperfectmatchsearchsettingsbyprofileid(int profileid);       
     
       //get full profile stuff       
        string getgenderbyscreenname(string strScreenName);            
        string getgenderbyphotoid(Guid guid);  
       //TO DO this needs to be  linked to roles

      //Message and Email Quota stuff
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool checkifquoutareachedandupdate(int profileid) ; 
       //Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
       //update the database i.e create folders and change profile status from guest to active ?!
        bool createmailboxfolders(int strProfileID);
        bool activateprofile(int strProfileID);      
     
       //updates the profile with a password that is presumed to be already encyrpted
        bool updatepassword(int profileid, string encryptedpassword);       
        bool addnewopenidforprofile(int profileid, string openidIdentifer, string openidProvidername); 
       //check if profile is activated 
        bool checkifprofileisactivated(int strProfileID);   
       //check if mailbox folder exist
        bool checkifmailboxfoldersarecreated(int strProfileID);     

      //DateTimeFUcntiosn for longin etc "
       //********************************************
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool updateuserlogouttime(int profileid, string sessionID);
       //get the last time the user logged in from profile
        Nullable<DateTime> getmemberlastlogintime(int profileid);
      
       //updates all the areas  that handle when a user logs in 
       // added 1/18/2010 ola lawal
       //also updates the last log in and profile data
        bool updateuserlogintime(string username, string sessionID);       
        bool updateuserlogintimebyprofileid(int profileid, string sessionID);
       
       //date time functions '
       //***********************************************************
       //this function will send back when the member last logged in
       //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
       //Ola Lawal 7/10/2009 feel free to drill down even to the day
        string getlastloggedinstring(DateTime LoginDate); 
       //returns true if somone logged on
        bool getuseronlinestatus(int profileid);
       
       //other standard verifcation methods added here
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
       /// </summary>       
        bool checkifscreennamealreadyexists(string strScreenName);   
       //5-20-2012 added to check if a user email is registered       
        bool checkifprofileidalreadyexists(int profileid);     
        string validatesecurityansweriscorrect(int strProfileID, int SecurityQuestionID, string strSecurityAnswer);  
        int getprofileidbyusername(string strusername);
        int getprofileidbyscreenname(string strscreenname);   
        string getusernamebyprofileid(int profileid);       
        string getscreennamebyprofileid(int profileid);  
        string getscreennamebyusername(string username);      
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
        bool checkifactivationcodeisvalid(int strProfileID, string strActivationCode);      
      //  bool CheckForGalleryPhotobyProfileID(int strProfileID);
      //  bool CheckForUploadedPhotobyProfileID(int strProfileID);


       //Hereis where the members Repository stuff that was in the MVC project starts at
       //************************************************************************************

        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyprofileid(int profileid);         
        bool deactivateprofile(int profileid);
        visiblitysetting getprofilevisibilitysettingsbyprofileid(int profileid);          
     
     
       //TO DO move these functions to a Matches or Search Services Area and repoisitory eventually
       //***********************************************************************************************
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
         List<MemberSearchViewModel> getquickmatches(MembersViewModel model);      
        //quick search for members in the same country for now, no more filters yet
        //this needs to be updated to search based on the user's prefered setting i.e thier looking for settings
         List<MemberSearchViewModel> getemailmatches(MembersViewModel model);
         List<MemberSearchViewModel> getquickmatcheswhenquickmatchesempty(MembersViewModel model);
          
      
    }
}
