using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Interfaces;
using Dating.Server.Data.Models;
using System.Web.Security;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Domain.Entities.Anewluv;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Shell.MVC2.Services.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class MembershipService : IAuthenticationService 
    {

        private IAnewLuvMembershipProvider _anewluvmembershipprovider;

        public MembershipService(IAnewLuvMembershipProvider anewluvmembershipprovider)
        {
            _anewluvmembershipprovider = anewluvmembershipprovider;
        }
        
            public bool ValidateUser(string username, string password)
            {
               return  _anewluvmembershipprovider.ValidateUser(username, password);
            }

            //5-82012 updated to only valudate username
            //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method

         
            public bool ValidateUser(string username)
            {
                return _anewluvmembershipprovider.ValidateUser(username);
            }

           
            public bool ValidateUser(string VerifedEmail, string openidIdentifer, string openidProvidername)
            {
                return _anewluvmembershipprovider.ValidateUser(VerifedEmail, openidIdentifer, openidProvidername);
            }          
            
            public  string ApplicationName()
            {
               // return _anewluvmembershipprovider.ApplicationName();
                return "AnewLuvMemberShipService";
            }



            public MembershipUser CreateUser(MembershipUserViewModel model)
            {

               // string username,
               //string password,
               //string email, string securityQuestion,
               //   string securityAnswer,
               //bool isApproved,
               //string providerUserKey,
                MembershipCreateStatus status;

                return _anewluvmembershipprovider.CreateUserCustom(model.username, model.password, model.openidIdentifer ,
                    model.openidProvidername,
                  model.email,                    
                 DateTime.Now,
                  "", "", "", "", null, null, "", "", "",
                  false,
                  null,
                  out status );
            }

            public AnewLuvMembershipUser CreateUserCustom(MembershipUserViewModel model)
            {
                MembershipCreateStatus status;
                return _anewluvmembershipprovider.CreateUserCustom (model.username,
                            model.password, model.openidIdentifer, model.openidProvidername,
                           model.email,
                           model.birthdate, model.gender, model.country, model.city, model.stateprovince, 
                           model.longitude, model.lattitude, model.screenname, model.zippostalcode, model.activationcode,
                           model.isApproved,
                           model.providerUserKey, out status);
            
            }

            
            public string ResetPassword(string profileID, string answer)
            {
           return   _anewluvmembershipprovider.ResetPassword( profileID, answer);
          }
            //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
            // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
            //finally returns the new password to the calling functon or an empty string if failure.
          
            
            public string ResetPasswordCustom(string profileid)
            {
               return _anewluvmembershipprovider.ResetPasswordCustom(Convert.ToInt16(profileid));
            }

       
            
            public void UpdateUser(MembershipUser user)
            {
                _anewluvmembershipprovider.UpdateUser(user);
            }
          
            
            public MembershipUser GetUser(string username, bool userIsOnline)
            {
                return _anewluvmembershipprovider.GetUser(username, userIsOnline);
            }

            //custom remapped membership get user function
          
            
            public AnewLuvMembershipUser GetUserCustom(string username, bool userIsOnline)
            {
                return _anewluvmembershipprovider.GetUserCustom(username, userIsOnline);
            }
                 
            
            public string GeneratePassword()
            {
                return _anewluvmembershipprovider.GeneratePassword();
            }


            //public void UpdateUserCustom(MembershipUser user)
            //{
            //    _anewluvmembershipprovider.UpdateUser(user);
            //}











        }
    
    
}
