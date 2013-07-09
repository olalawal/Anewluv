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
using Shell.MVC2.Services.Contracts.ServiceResponse;

namespace Shell.MVC2.Services.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class MembershipService : IAuthenticationService 
    {

        private IAnewLuvMembershipProvider _anewluvmembershipprovider;
        //private IMemberService _memberservice;

        public MembershipService(IAnewLuvMembershipProvider anewluvmembershipprovider)
        {
            _anewluvmembershipprovider = anewluvmembershipprovider;
         //   _memberservice = memberservice;
        }
        
            public bool validateuser(string username, string password)
            {
               return  _anewluvmembershipprovider.ValidateUser(username, password);
            }

            //5-82012 updated to only valudate username
            //overide for validate user that uses just the username, this can be used for pass through auth where a user was already prevalidated via another method

         
            public bool validateuser(string username)
            {
                return _anewluvmembershipprovider.ValidateUser(username);
            }

           
            public bool validateuser(string verifedemail, string openididentifer, string openidprovidername)
            {
                return _anewluvmembershipprovider.ValidateUser(verifedemail, openididentifer, openidprovidername);
            }          
            
            public  string applicationname()
            {
               // return _anewluvmembershipprovider.ApplicationName();
                return "AnewLuvMemberShipService";
            }



            public AnewluvResponse  createuser(MembershipUserViewModel model)
            {
                

                try
                {

                    // string username,
                    //string password,
                    //string email, string securityQuestion,
                    //   string securityAnswer,
                    //bool isApproved,
                    //string providerUserKey,
                    MembershipCreateStatus status;

                    var membershipprovider = _anewluvmembershipprovider.CreateUserCustom(model.username, model.password, model.openidIdentifer,
                       model.openidProvidername,
                     model.email,
                    DateTime.Now, model.gender, model.country, model.city, model.stateprovince, model.longitude, model.lattitude,
                    model.screenname, model.zippostalcode, model.activationcode, false, model.providerUserKey,
                     out status);

                    AnewluvResponse response = new AnewluvResponse();
                    ResponseMessage responsemessage = new ResponseMessage();
                    switch (status)
                    {

                        case MembershipCreateStatus.Success:
                            //get the profile info to return
                            //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getprofilebyusername(model.username);                         
                            response.profileid1 = membershipprovider.profileid.ToString(); //profile.id.ToString();
                            response.email = membershipprovider.Email; //profile.emailaddress;
                            responsemessage = new ResponseMessage("", "Profile created succesfully", "");
                            break;
                        case MembershipCreateStatus.DuplicateUserName :
                           // AnewluvResponse response = new AnewluvResponse();
                            responsemessage = new ResponseMessage("", "Unable to create profile","Duplicate username : the username :" + model.username + "already exists"); 
                            break;
                        case MembershipCreateStatus.DuplicateEmail :
                             responsemessage = new ResponseMessage("","Unable to create profile", "Duplicate email : the email :" + model.email  + "already exists");                         
                            break;
                        default:
                           // Console.WriteLine("Invalid selection. Please select 1, 2, or 3.");
                              ResponseMessage reponsemessage = new ResponseMessage("", "Unable to create profile","There was a problem creation the profile, please try again later");
                        response.ResponseMessages.Add(reponsemessage);  
                            break;
                    }

                    response.ResponseMessages.Add(responsemessage);  
                    return response;
                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in authenitcation service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

                //return null;
            }

            public AnewLuvMembershipUser createusercustom(MembershipUserViewModel model)
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

            
            public string resetpassword(string profileid, string answer)
            {
           return   _anewluvmembershipprovider.ResetPassword( profileid, answer);
          }
            //handles reseting password duties.  First verifys that security uqestion was correct for the profile ID, the generated a password
            // using the local generatepassword method the send the encyrpted passwoerd and profile ID to the dating service so it can be updated in the DB
            //finally returns the new password to the calling functon or an empty string if failure.
          
            
            public string resetpasswordcustom(string profileid)
            {
               return _anewluvmembershipprovider.ResetPasswordCustom(Convert.ToInt16(profileid));
            }

       
            
            public void updateuser(MembershipUser user)
            {
                _anewluvmembershipprovider.UpdateUser(user);
            }
          
            
            public MembershipUser getuser(string username, string userisonline)
            {
                return _anewluvmembershipprovider.GetUser(username, Convert.ToBoolean(userisonline));
            }

            //custom remapped membership get user function
          
            
            public AnewLuvMembershipUser getusercustom(string username, string userisonline)
            {
                return _anewluvmembershipprovider.GetUserCustom(username, Convert.ToBoolean(userisonline));
            }
                 
            
            public string generatepassword()
            {
                return _anewluvmembershipprovider.GeneratePassword();
            }


            //public void UpdateUserCustom(MembershipUser user)
            //{
            //    _anewluvmembershipprovider.UpdateUser(user);
            //}

            #region "Custom methods specific for AnewLuv"
            public  AnewluvResponse activateprofile(activateprofilemodel model)
            {
                AnewluvResponse response = new AnewluvResponse();

                try
                {
                    var anewluvmessages = _anewluvmembershipprovider.activateprofile(model);

                    if (anewluvmessages.errormessages.Count() == 0)
                    {
                        //get the profile info to return
                        //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                      //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                        response.email = model.emailaddress;//profile.emailaddress;
                        ResponseMessage reponsemessage = new ResponseMessage("", anewluvmessages.message , "");
                        response.ResponseMessages.Add(reponsemessage);
                      
                    }   
                    else
                    {
                        ResponseMessage reponsemessage = new ResponseMessage("","There was a problem activating the profile, please try again later", anewluvmessages.errormessages.First());
                        response.ResponseMessages.Add(reponsemessage);
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error activating profile : authenticantion  service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }

            public AnewluvResponse recoveractivationcode(activateprofilemodel model)
            {
                AnewluvResponse response = new AnewluvResponse();

                try
                {
                    var anewluvmessages = _anewluvmembershipprovider.recoveractivationcode(model);

                    if (anewluvmessages.errormessages.Count() == 0)
                    {
                        //get the profile info to return
                        //Shell.MVC2.Domain.Entities.Anewluv.profile profile = _memberservice.getpro(model.username);
                      //  response.profileid1 = model.profileid.ToString();//profile.id.ToString();
                        response.email = model.emailaddress;//profile.emailaddress;
                        ResponseMessage reponsemessage = new ResponseMessage("", anewluvmessages.message, "");
                        response.ResponseMessages.Add(reponsemessage);
                        //send the email vai service

                    }
                    else
                    {
                        ResponseMessage reponsemessage = new ResponseMessage("", "There was a problem sending your activation code please try again later", anewluvmessages.errormessages.First());
                        response.ResponseMessages.Add(reponsemessage);
                    }

                 

                    return response;
                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error activating profile : authenticantion  service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }

            #endregion









        }
    
    
}
