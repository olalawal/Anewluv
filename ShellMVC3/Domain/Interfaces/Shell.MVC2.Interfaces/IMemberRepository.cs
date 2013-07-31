using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Interfaces
{
   public interface IMemberRepository
   {


       //TO Do posibly move this to a separate service for benchmarking
       //member viewmodoem mapping and registration models mappers
     
       //TO DO move to unit test
       // registermodel mapregistrationtest();  
       //end of profile mapping

       membersinrole getmemberrolebyprofileid(ProfileModel model);

       //initial profile stuffs
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyusername(ProfileModel model);
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyemailaddress(ProfileModel model);
        profiledata getprofiledatabyprofileid(ProfileModel model) ;
        searchsetting getperfectmatchsearchsettingsbyprofileid(ProfileModel model);
        searchsetting createmyperfectmatchsearchsettingsbyprofileid(ProfileModel model);       
     
       //get full profile stuff       
        string getgenderbyscreenname(ProfileModel model);
        string getgenderbyphotoid(ProfileModel model);  
       //TO DO this needs to be  linked to roles

      //Message and Email Quota stuff
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool checkifquoutareachedandupdate(ProfileModel model) ; 
       //Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
       //update the database i.e create folders and change profile status from guest to active ?!
        bool createmailboxfolders(ProfileModel model);
        bool activateprofile(ProfileModel model);      
     
       //updates the profile with a password that is presumed to be already encyrpted
        bool updatepassword(ProfileModel model, string encryptedpassword);       
        bool addnewopenidforprofile(ProfileModel model); 
       //check if profile is activated 
        bool checkifprofileisactivated(ProfileModel model);   
       //check if mailbox folder exist
        bool checkifmailboxfoldersarecreated(ProfileModel model);     

      //DateTimeFUcntiosn for longin etc "
       //********************************************
       // Description:	Updates the users logout time
       // added 1/18/2010 ola lawal
        bool updateuserlogouttimebyprofileid(ProfileModel model);
       //get the last time the user logged in from profile
        Nullable<DateTime> getmemberlastlogintimebyprofileid(ProfileModel model);
      
       //updates all the areas  that handle when a user logs in 
       // added 1/18/2010 ola lawal
       //also updates the last log in and profile data
        bool updateuserlogintimebyprofileidandsessionid(ProfileModel model);       
        bool updateuserlogintimebyprofileid(ProfileModel model);
       //7-30-2013 olawal to update the profile activity times
        bool updateprofileactivity(profileactivity model);
       
       //date time functions '
       //***********************************************************
       //this function will send back when the member last logged in
       //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
       //Ola Lawal 7/10/2009 feel free to drill down even to the day
        string getlastloggedinstring(DateTime LoginDate); 
       //returns true if somone logged on
        bool getuseronlinestatus(ProfileModel model);
       
       //other standard verifcation methods added here
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
       /// </summary>       
        bool checkifscreennamealreadyexists(ProfileModel model);
        bool checkifusernamealreadyexists(ProfileModel model);  
       //5-20-2012 added to check if a user email is registered       
       // bool checkifusernamealreadyexists(ProfileModel model);     
        string validatesecurityansweriscorrect(ProfileModel model);  
        int? getprofileidbyusername(ProfileModel model);
        int? getprofileidbyopenid(ProfileModel model);
        int? getprofileidbyscreenname(ProfileModel model);
        int? getprofileidbyssessionid(ProfileModel model);
        string getusernamebyprofileid(ProfileModel model);       
        string getscreennamebyprofileid(ProfileModel model);  
        string getscreennamebyusername(ProfileModel model);      
        bool checkifemailalreadyexists(ProfileModel model);
       // added by Deshola on 5/17/2011       
     //   byte[] GetGalleryPhotobyPhotoID(Guid strPhotoID);   
     //   byte[] GetGalleryPhotobyProfileID(ProfileModel model); 
    //    byte[] GetGalleryPhotobyScreenName(ProfileModel model);
    //    byte[] GetGalleryImagebyNormalizedScreenName(ProfileModel model);   
       // bool InsertPhotoCustom(Shell.MVC2.Domain.Entities.Anewluv.photo newphoto);
       // bool CheckIfPhotoCaptionAlreadyExists(ProfileModel model, string strPhotoCaption);  
       /// <summary>
       /// Determines wethare an activation code matches the value in the database for a given profileID
       /// </summary>
        bool checkifactivationcodeisvalid(ProfileModel model);      
      //  bool CheckForGalleryPhotobyProfileID(ProfileModel model);
      //  bool CheckForUploadedPhotobyProfileID(ProfileModel model);


       //Hereis where the members Repository stuff that was in the MVC project starts at
       //************************************************************************************

        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyprofileid(ProfileModel model);         
        bool deactivateprofile(ProfileModel model);
        visiblitysetting getprofilevisibilitysettingsbyprofileid(ProfileModel model);          
     
     
      
          
      //mapper calls that use the appfabric cache


    }
}
