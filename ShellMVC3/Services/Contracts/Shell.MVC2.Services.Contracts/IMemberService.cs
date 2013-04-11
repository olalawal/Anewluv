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
        // registermodel mapregistrationtest();  
        //end of profile mapping

        //initial profile stuffs
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilebyusername/{username}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyusername(string username);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofiledatabyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	      
        profiledata getprofiledatabyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getperfectmatchsearchsettingsbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        searchsetting getperfectmatchsearchsettingsbyprofileid(string profileid);
        
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/createmyperfectmatchsearchsettingsbyprofileid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        searchsetting createmyperfectmatchsearchsettingsbyprofileid(string profileid);

        //get full profile stuff    
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgenderbyscreenname/{strScreenName}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getgenderbyscreenname(string strScreenName);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgenderbyphotoid/{photoid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getgenderbyphotoid(string photoid);
        //TO DO this needs to be  linked to roles

        //Message and Email Quota stuff
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifquoutareachedandupdate/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool checkifquoutareachedandupdate(string profileid);
        
        //Activate, Valiate if Profile is Acivated Code and Create Mailbox Folders as well"
        //update the database i.e create folders and change profile status from guest to active ?!
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/createmailboxfolders/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool createmailboxfolders(string profileid);
        
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/activateprofile/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool activateprofile(string profileid);

        //updates the profile with a password that is presumed to be already encyrpted
         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebInvoke(UriTemplate = "/updatepassword/{profileid}/{encryptedpassword}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updatepassword(string profileid, string encryptedpassword);

         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebInvoke(UriTemplate = "/addnewopenidforprofile/{profileid}/{openidIdentifer}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool addnewopenidforprofile(string profileid, string openidIdentifer, string openidProvidername);
        
        //check if profile is activated 
         [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
         [WebGet(UriTemplate = "/checkifprofileisactivated/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
         bool checkifprofileisactivated(string profileid);
        //check if mailbox folder exist

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifmailboxfoldersarecreated/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
         bool checkifmailboxfoldersarecreated(string profileid);

        //DateTimeFUcntiosn for longin etc "
        //********************************************
        // Description:	Updates the users logout time
        // added 1/18/2010 ola lawal
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/updateuserlogouttime/{profileid}/{sessionid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool updateuserlogouttime(string profileid, string sessionid);
        
        //get the last time the user logged in from profile
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberlastlogintime/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        Nullable<DateTime> getmemberlastlogintime(string profileid);

        //updates all the areas  that handle when a user logs in 
        // added 1/18/2010 ola lawal
        //also updates the last log in and profile data
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updateuserlogintime/{username}/{sessionid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updateuserlogintime(string username, string sessionid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/updateuserlogintimebyprofileid/{profileid}/{sessionid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool updateuserlogintimebyprofileid(string profileid, string sessionid);

        //date time functions '
        //***********************************************************
        //this function will send back when the member last logged in
        //be it This Week,3 Weeks ago, 3 months Ago or In the last Six Months
        //Ola Lawal 7/10/2009 feel free to drill down even to the day
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getlastloggedinstring/{logindate}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getlastloggedinstring(string logindate);
        
        //returns true if somone logged on
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getuseronlinestatus/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool getuseronlinestatus(string profileid);

        //other standard verifcation methods added here
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// 5/5/2012 als added check that the screen name withoute spaces does not match an existing one with no spaces either
        /// </summary>      
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifscreennamealreadyexists/{screename}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool checkifscreennamealreadyexists(string screename);
        
        //5-20-2012 added to check if a user email is registered  
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifusernamealreadyexists/{username}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool checkifusernamealreadyexists(string username);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/validatesecurityansweriscorrect/{profileid}/{securityquestionid}/{strsecurityanswer}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string validatesecurityansweriscorrect(string profileid, string securityquestionid, string strsecurityanswer);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofileidbyusername/{username}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        int? getprofileidbyusername(string username);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofileidbyscreenname/{screename}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        int? getprofileidbyscreenname(string screename);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofileidbyssessionid/{sessionid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        int? getprofileidbyssessionid(string sessionid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getusernamebyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getusernamebyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getscreennamebyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getscreennamebyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getscreennamebyusername/{username}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string getscreennamebyusername(string username);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifemailalreadyexists/{emailaddress}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool checkifemailalreadyexists(string emailaddress);
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

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/checkifactivationcodeisvalid/{profileid}/{activationcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        bool checkifactivationcodeisvalid(string profileid, string activationcode);
        //  bool CheckForGalleryPhotobyProfileID(int strProfileID);
        //  bool CheckForUploadedPhotobyProfileID(int strProfileID);


        //Hereis where the members Repository stuff that was in the MVC project starts at
        //************************************************************************************
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilebyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        Shell.MVC2.Domain.Entities.Anewluv.profile getprofilebyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/deactivateprofile/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool deactivateprofile(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getprofilevisibilitysettingsbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        visiblitysetting getprofilevisibilitysettingsbyprofileid(string profileid);
     
        //mapper calls that use the appfabric cache
    }
}
