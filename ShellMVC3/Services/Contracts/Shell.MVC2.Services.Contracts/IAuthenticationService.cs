using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;



namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAuthenticationService                    
    {

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ValidateUser/{username}/{password}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool ValidateUser(string username, string password); 
          
            //5-82012 updated to only valudate username
            //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
             
        [OperationContract(Name = "ValidateUserByUsername"), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ValidateUser/{username}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
         bool ValidateUser(string username);

       
       
        [OperationContract(Name = "ValidateUserByOpenID"), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ValidateUser/{VerifedEmail}/{openidIdentifer}/{openidProvidername}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]       
        bool ValidateUser(string VerifedEmail, string openidIdentifer, string openidProvidername);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ApplicationName", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	      
        string ApplicationName();


       // [WebInvoke(UriTemplate =
       // "/CreateUser/{username}/{password}/{email}/{securityQuestion}/{securityAnswer}/{isApproved}/{providerUserKey}",
       // Method = "POST", BodyStyle = WebMessageBodyStyle.Bare)]	


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(MembershipUser))]
        [WebInvoke(UriTemplate = "/CreateUser", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        MembershipUser CreateUser(MembershipUserViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(AnewLuvMembershipUser))]
        [WebInvoke(UriTemplate = "/CreateUserCustom", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AnewLuvMembershipUser  CreateUserCustom(MembershipUserViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ResetPassword/{profileID}/{answer}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        string ResetPassword(string profileID, string answer);
            
            //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
            // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
            //finally returns the new password to the calling functon or an empty string if failure.
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/ResetPasswordCustom/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	  
         string ResetPasswordCustom(string profileid) ;

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(AnewLuvMembershipUser))]
        [WebInvoke(UriTemplate = "/UpdateUser", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
              void UpdateUser(MembershipUser user);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/GetUser/{username}/{userIsOnline}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
              MembershipUser GetUser(string username, string  userIsOnline);
           
            //custom remapped membership get user function
     
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/GetUserCustom/{username}/{userIsOnline}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        AnewLuvMembershipUser GetUserCustom(string username, string  userIsOnline);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/GeneratePassword", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	                         
         string GeneratePassword()  ;

        #region "Custom methods specific for AnewLuv"

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(AnewluvMessages))]
        [WebInvoke(UriTemplate = "/activateprofile", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AnewluvMessages activateprofile(activateprofilecontainerviewmodel model);

        #endregion
        
        //[WebInvoke]
        //[OperationContract]
        //     void UpdateUserCustom(string username, string ProfileID,
        //            string password,
        //            string securityQuestion,
        //            string securityAnswer,
        //            DateTime birthdate, string gender, string country, string city, string zippostalcode);
            
           
        //TO DO expose the rest of these servces later
            //public bool CheckForUploadedPhotoByProfileID(string profileid);
           
            //public bool CheckIfPhotoCaptionAlreadyExists(string profileid, string photocaption);           

            //public bool checkifmailboxfoldersarecreated(string profileid)   ;        

            //public bool CheckIfEmailAlreadyExists(string email) ;         

            //public bool CheckIfProfileisActivated(string profileid);           

            //public bool ActivateProfile(string profileid);           

            //public bool createmailboxfolders(string profileid);           

            //public string GetUserNamebyProfileID(string profileid);           

            //public string GetScreenNamebyProfileID(string profileid);           

            //public string GetScreenNamebyUserName(string username);

            //// Other overrides not implemented
            
            //public  bool ChangePassword(string username, string oldPassword, string newPassword);           

            //public  bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);           

            //public  bool DeleteUser(string username, bool deleteAllRelatedData);          

            //public  bool EnablePasswordReset();          

            //public  bool EnablePasswordRetrieval();            

            //public  MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)   ;       

            //public  MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)  ;          

            //public  MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
           
            //public  int GetNumberOfUsersOnline() ;         

            //public  string GetPassword(string username, string answer);
           
            //public  MembershipUser GetUser(object providerUserKey, bool userIsOnline)       ;    

            //public  string GetUserNameByEmail(string email)    ;       

            //public  int MaxInvalidPasswordAttempts()    ;      

            //public  int MinRequiredNonAlphanumericCharacters() ;          

            //public  int MinRequiredPasswordLength();          

            //public  int PasswordAttemptWindow();
           
            //public  MembershipPasswordFormat PasswordFormat();          

            //public  string PasswordStrengthRegularExpression();         

            //public  bool RequiresQuestionAndAnswer();

            //public  bool RequiresUniqueEmail();    

            //public  bool UnlockUser(string userName);
           

           



        


    }
}
