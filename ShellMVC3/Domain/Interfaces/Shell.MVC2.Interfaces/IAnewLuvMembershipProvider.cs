using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
  using System.Web.Security;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Interfaces
{
   public  interface IAnewLuvMembershipProvider  
    {
     
         bool ValidateUser(string username, string password);

        //5-82012 updated to only valudate username
        //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method
 
         bool ValidateUser(string username);

      
         bool ValidateUser(string VerifedEmail, string openidIdentifer, string openidProvidername);

      
         // string ApplicationName;

        
       //Only expose the create user as a custom type
          MembershipUser CreateUser(string username,
           string password,
           string email,
           string securityQuestion,
                  string securityAnswer,
           bool isApproved,
           object providerUserKey,
           out MembershipCreateStatus status);


         AnewLuvMembershipUser  CreateUserCustom(string username,
                   string password, string openidIdentifer, string openidProvidername,
                  string email,
            //  string securityQuestion,
            // string securityAnswer,
                  DateTime birthdate, string gender, string country, string city, string stateprovince, double? longitude, double? lattitude, string screenname, string zippostalcode, string activationcode,
                  bool isApproved,
                  object providerUserKey,
                 out MembershipCreateStatus status);

    
         string ResetPassword(string profileid, string answer);

        //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
        // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
        //finally returns the new password to the calling functon or an empty string if failure.       
         string ResetPasswordCustom(int profileid);

      
         void UpdateUser(MembershipUser user);

         MembershipUser GetUser(string username, bool userIsOnline);

        //custom remapped membership get user function  
         AnewLuvMembershipUser GetUserCustom(string username, bool userIsOnline);
    
         string GeneratePassword();

         void UpdateUserCustom(string username, string profileid,
                string password,
                string securityQuestion,
                string securityAnswer,
                DateTime birthdate, string gender, string country, string city, string zippostalcode);


          AnewluvMessages activateprofile(activateprofilecontainerviewmodel model);

       // MembershipUser CreateUser(string username, string password, string email, string securityQuestion, string securityAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);
   
   
   
   }
}
