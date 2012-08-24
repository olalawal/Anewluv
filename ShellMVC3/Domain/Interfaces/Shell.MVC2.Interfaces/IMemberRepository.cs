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

     

       //initial profile stuffs
        profile getprofilebyusername(string username)  ;
        profiledata getprofiledatabyprofileid(string profileid) ;
        searchsetting getperfectmatchsearchsettingsbyprofileid(string profileid);
        searchsetting createmyperfectmatchsearchsettingsbyprofileid(string profileid);
       
     
       //get full profile stuff
       
        string getgenderbyscreenname(string strScreenName);            
        string getgenderbyphotoid(Guid guid);  
       //TO DO this needs to be  linked to roles

      //Message and Email Quota stuff
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool checkifquoutareachedandupdate(string profileID) ; 
       //Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
       //update the database i.e create folders and change profile status from guest to active ?!
        bool createmailboxfolders(string strProfileID);
        bool activateprofile(string strProfileID);      
     
       //updates the profile with a password that is presumed to be already encyrpted
        bool updatepassword(string profileID, string encryptedpassword);       
        bool addnewopenidforprofile(string profileID, string openidIdentifer, string openidProvidername); 
       //check if profile is activated 
        bool checkifprofileisactivated(string strProfileID);   
       //check if mailbox folder exist
        bool checkifmailboxfoldersarecreated(string strProfileID);      

      //DateTimeFUcntiosn for longin etc "
       //********************************************
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool updateuserlogouttime(string profileID, string sessionID);
       //get the last time the user logged in from profile
        Nullable<DateTime> getmemberlastlogintime(string profileid);
      
       //updates all the areas  that handle when a user logs in 
       // added 1/18/2010 ola lawal
       //also updates the last log in and profile data
        bool updateuserlogintime(string username, string sessionID);       
        bool updateuserlogintimebyprofileid(string ProfileID, string sessionID);
       
       //date time functions '
       //***********************************************************
       //this function will send back when the member last logged in
       //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
       //Ola Lawal 7/10/2009 feel free to drill down even to the day
        string getlastloggedinstring(DateTime LoginDate); 
       //returns true if somone logged on
        bool getuseronlinestatus(string profileid);
       
       //other standard verifcation methods added here
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
       /// </summary>       
        bool checkifscreennamealreadyexists(string strScreenName);   
       //5-20-2012 added to check if a user email is registered       
        bool checkifprofileidalreadyexists(string profileID);     
        string validatesecurityansweriscorrect(string strProfileID, int SecurityQuestionID, string strSecurityAnswer);  
        string getprofileidbyusername(string strusername);
        string getprofileidbyscreenname(string strscreenname);   
        string getusernamebyprofileid(string profileid);       
        string getscreennamebyprofileid(string profileid);  
        string getscreennamebyusername(string username);      
        bool checkifemailalreadyexists(string strEmail);
       // added by Deshola on 5/17/2011       
     //   byte[] GetGalleryPhotobyPhotoID(Guid strPhotoID);   
     //   byte[] GetGalleryPhotobyProfileID(string strProfileID); 
    //    byte[] GetGalleryPhotobyScreenName(string strScreenName);
    //    byte[] GetGalleryImagebyNormalizedScreenName(string strScreenName);   
       // bool InsertPhotoCustom(photo newphoto);
       // bool CheckIfPhotoCaptionAlreadyExists(string strProfileID, string strPhotoCaption);  
       /// <summary>
       /// Determines wethare an activation code matches the value in the database for a given profileID
       /// </summary>
        bool checkifactivationcodeisvalid(string strProfileID, string strActivationCode);      
      //  bool CheckForGalleryPhotobyProfileID(string strProfileID);
      //  bool CheckForUploadedPhotobyProfileID(string strProfileID);


       //Hereis where the members Repository stuff that was in the MVC project starts at
       //************************************************************************************
   
        profile getprofilebyprofileid(string ProfileId);       
                         
     
         bool deactivateprofile(string profileid);
         visiblitysetting getprofilevisibilitysettingsbyprofileid(string ProfileID);          
     
     
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
