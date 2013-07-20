using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Infrastructure.Interfaces ;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Interfaces;
using System.Data;
using System.Net.Mail;
using Shell.MVC2.Infrastructure;
using LoggingLibrary;

namespace Shell.MVC2.Data
{
    public class InfoNotificationRepository : MemberRepositoryBase , IInfoNotificationRepository
    {
        private int featuredmemberid = 0;
        private NotificationContext _notificationcontext;
        private IMemberRepository _membersrepository;
        private IMembersMapperRepository _membersmapperrepository;
      //  private IAPIkeyRepository _apikeyrepository;
      

        public InfoNotificationRepository(NotificationContext notificationcontext, AnewluvContext datingcontext, IMemberRepository membersrepository,IMembersMapperRepository membersmapperrepository)
            : base(datingcontext)
        {
            _notificationcontext = notificationcontext;
            _membersrepository = membersrepository;
            _membersmapperrepository = membersmapperrepository;
            _datingcontext = datingcontext;
           //_apikeyrepository = apikeyrepository;
            featuredmemberid = 67;
            
        }



        #region "private  functions which actually creates message in databse and sends the email"    


        private bool savemessage(message message)
        {
           //save the message object first
            try
            {
                //now that the sent flag has been updated we can add and save the message 
                //same thing similar would be possible for a chat based service I imagnge
                _notificationcontext.messages.Add(message);
                _notificationcontext.SaveChanges();
               
            }
            catch (UpdateException ex)
            {
                //log message
                string actualmessage = ex.Message;
                throw new InvalidOperationException("Failed to send a mail message. Try your request again.");
                //TO Do log this , sort of recursive
                // return false ;
            }
            return true;
        }
        private bool saveaddress(address  address)
        {
            //save the message object first
            try
            {
                //now that the sent flag has been updated we can add and save the message 
                //same thing similar would be possible for a chat based service I imagnge
                _notificationcontext.address.Add(address);
                _notificationcontext.SaveChanges();

            }
            catch (UpdateException ex)
            {
                //log message
                string actualmessage = ex.Message;
                throw new InvalidOperationException("Failed to send a mail message. Try your request again.");
                //TO Do log this , sort of recursive
                // return false ;
            }
            return true;
        }
        private bool sendemail(message message)
        {
            bool isEmailSendSuccessfully = false;
            try
            {
                foreach (address recip_loopVariable in message.recipients)
                {
                    var recip = recip_loopVariable;
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(message.systemaddress.emailaddress, recip_loopVariable.emailaddress);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = message.subject;
                    mailMessage.Body = message.body;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = !string.IsNullOrEmpty(message.systemaddress.hostname) ? mailMessage.Sender.Host : message.systemaddress.hostip;
                    //smtp.Credentials()
                    smtp.Credentials = new System.Net.NetworkCredential(message.systemaddress.credentialusername, message.systemaddress.credentialpassword);
                    smtp.Send(mailMessage);
                    isEmailSendSuccessfully = true;
                }
                
                isEmailSendSuccessfully = true;
            }
            catch (Exception ex)
            {
                //TO DO log this
                string ErrorMessage = ex.Message;
                isEmailSendSuccessfully = false;
                throw;
            }

            return isEmailSendSuccessfully;
        }

        #endregion


        #region "error message notifications"

        //public bool senderrormessagetodevelopers(CustomErrorLog customerror)
        //{



        //    //using (_notificationcontext ) {
        //    try
        //    {
        //        //get the recipients 
        //        //Dim recipeints As New List(Of String)

        //        // recipeints = context.MessageSystemAddresses.Where(Function(c) c.SystemAddressType = CInt(AddressType))
        //        dynamic recipientEmailAddresss = (from x in (_notificationcontext.address.Where(f => f.id == Convert.ToInt32(addresstypeenum.Developer))) select x);
        //        dynamic SystemSenderAddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == Convert.ToInt32(systemaddresstypeenum.DoNotReplyAddress))) select x).First();

        //        // Perform validation on the updated order before applying the changes.
        //         message message = new message();

        //        //use create method it like this 
        //        message = (message.Create(c =>
        //        {
        //            //c.id = (int)templateenum.GenericErrorMessage;
        //            c.template.id = (int)templateenum.GenericErrorMessage;
        //            c.messagetype.id = (int)messagetypeenum.DeveloperError;
        //            c.body = c.template == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
        //                                                 TemplateParser.RazorDBTemplate(message.template.razortemplatebody, ref customerror);
        //            c.subject = string.Format("An error occured");
        //            c.recipients = recipientEmailAddresss.ToList();
        //            c.sendingapplication = "ErrorNotificationService";
        //            c.systemaddress = SystemSenderAddress;
        //        }));

        //        // c.body = c.template == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
        //        //                                      TemplateParser.RazorDBTemplate(message.template.razorTemplateBody, ref customerror);

        //        //'parse the message body from the template

        //        var messsage = new message
        //        {
        //            id = (int)templateenum.GenericErrorMessage,
        //            template = new lu_template { id = 1 },
        //            messagetype = new lu_messagetype { id = (int)messagetypeenum.DeveloperError },
        //            body = message.template.razortemplatebody == null ? TemplateParser.RazorFileTemplate("", ref customerror) : TemplateParser.RazorDBTemplate(message.template.razortemplatebody, ref customerror),
        //            subject = string.Format("An error occured"),
        //            recipients = recipientEmailAddresss.ToList(),
        //            sendingapplication = "ErrorNotificationService",
        //            systemaddress = SystemSenderAddress

        //        };


        //        // The Add method examines the change tracking information 
        //        // contained in the graph of self-tracking entities to infer the set of operations
        //        // that need to be performed to reflect the changes in the database. 
        //        //Dim ddd = New CustomErrorLog()
        //        //ddd.Message = errormessage

        //        //send the pyysicall email message here

        //        message.sent = sendemail(message); //attempt to send the message
        //        savemessage(message);  //save the message into database 
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        string mesessage  = ex.Message ;
        //        return false;
        //    }

        //    return true;
         
        //}
        #endregion


        public   EmailModel senderrormessage(errorlog error,addresstypeenum addresstype)
        {

            EmailModel emailmodels = new EmailModel();

          
            try
            {
                dynamic systemsenderaddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
                lu_template  template = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.GenericErrorMessage))) select x).First();
                lu_messagetype  messagetype = (from x in (_notificationcontext.lu_messagetype .Where(f => f.id == (int)(messagetypeenum.DeveloperError))) select x).First();
                var recipientemailaddresss = (from x in (_notificationcontext.address.Where(f => f.id == (int)(addresstype))) select x).ToList();
               
                 //build the recipient address objects
                 


                    EmailModel returnmodel = new EmailModel();
                    returnmodel = getemailbytemplateid(templateenum.GenericErrorMessage);
                    //fill in the rest of the email model values 
                    returnmodel.subject = String.Format(returnmodel.subject, error.profileid);
                    returnmodel.body = String.Format(returnmodel.body, error.profileid, error.message);                    
                

                 //Now build the message e
                 // recipeints = context.MessageSystemAddresses.Where(Function(c) c.SystemAddressType = CInt(AddressType))
                 //Perform validation on the updated order before applying the changes.
                 message message = new message();
                 //use create method it like this 
                 message = (message.Create(c =>
                 {
                     //c.id = (int)templateenum.GenericErrorMessage;
                     c.template = template;
                     c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                     c.body = TemplateParser.RazorFileTemplate(template.filename , ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                     c.subject = returnmodel.subject;
                     c.recipients = recipientemailaddresss;
                     c.sendingapplication = "InfoNotificationService";
                     c.systemaddress = systemsenderaddress;
                 }));

                 message.sent = sendemail(message); //attempt to send the message
                 savemessage(message);  //save the message into database 

               }
              catch (Exception ex)
              {
                  //log error mesasge
                  new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning , ex, null, null,false);
                throw;
              }
             
              
            return emailmodels;

        }

        //TO Do find a way to differentiate between user and Member
        public  EmailViewModel sendcontactusemail(ContactUsModel model)
         {
          
             try
             {
                // var testmembertemplate =(int)(templateenum.MemberContactUsMemberMesage );
                //var testadmintemplate = (int)(templateenum.MemberContactUsAdminMessage);

                 dynamic systemsenderaddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
                  lu_template   membertemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberContactUsMemberMesage ))) select x).First();
                 lu_template admintemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberContactUsAdminMessage ))) select x).First();
               
                 //build the recipeint addresses from contract us model i.f they dont exist create new ones 
                 //First get system addresses for admin
                 dynamic  adminrecipientemailaddresss = (from x in (_notificationcontext.address.Where(f => f.id == (int)(addresstypeenum.SiteSupportAdmin))) select x);
               
                 //create new addres for user who already has one otherwise just add to new item
                 var  memberrecipientaddress = ( from x in (_notificationcontext.address.Where(f=>f.emailaddress == model.Email)) select x) ;
                 if (!(memberrecipientaddress.Count()  > 0))
                 {
                    var address = new address 
                    {                       
                       addresstype  = _notificationcontext.lu_addresstype.Where(f=>f.id == (int)addresstypeenum.SiteUser).FirstOrDefault(),       
                       emailaddress  = model.Email ,
                        otheridentifer = model.Name ,  //use this for chat notifications maybe
                        active = true,
                      creationdate = DateTime.Now                        
                    };
                    saveaddress(address);   //TO DO maybe remove this                      
                 }
                
                 //TO DO remove these large models after testing is complete
                 EmailViewModel returnmodel = new EmailViewModel();
                 
                 //12-19-2012 olawal Here we added this  , so we can populate the screen name
                 MembersViewModel membersviewmodel = new MembersViewModel();
                 profile profile = new profile {screenname = model.Name , emailaddress = model.Email };
                 membersviewmodel.profile = profile;
                 //add it
                 returnmodel.MembersViewModel = membersviewmodel;

                 returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsMemberMesage);
                 returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsAdminMessage);
                 //fill in the rest of the email model values i.e format the subject and body
                 //subject
                 returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
                 returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
                 //Body
                 returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.Name, model.Subject, model.Message);
                 returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.Name,model.Email ,model.Subject,model.Message );
               
                 //Build and send the user Message first               
              
                  message message = new message();
                  message.template = _notificationcontext.lu_template.Where(f => f.id == (int)templateenum.MemberContactUsMemberMesage).First();
                  message.messagetype = _notificationcontext.lu_messagetype.Where(f => f.id == (int)messagetypeenum.UserContactUsnotification).First();
                  message.content  = TemplateParser.RazorFileTemplate(membertemplate.filename, ref returnmodel);
                  message.body = returnmodel.memberEmailViewModel.body;
                  message.subject = returnmodel.memberEmailViewModel.subject;
                  message.recipients = memberrecipientaddress.ToList();
                  message.sendingapplication = "InfoNotificationService";
                  message.creationdate = DateTime.Now;
                  message.sendattempts = 0;
                  message.systemaddress = systemsenderaddress;


                  message.sent = sendemail(message); //attempt to send the message
                  savemessage(message);  //save the message into database 

                 ////now send the admin message
                 ////use create method it like this 
                  message = (message.Create(c =>
                  {
                      //c.id = (int)templateenum.GenericErrorMessage;
                      c.template.id = (int)templateenum.MemberContactUsAdminMessage;
                      c.messagetype.id = (int)messagetypeenum.UserContactUsnotification;
                      c.content  = TemplateParser.RazorFileTemplate(admintemplate.filename, ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                      c.body = returnmodel.adminEmailViewModel.body;
                      c.subject = returnmodel.adminEmailViewModel.subject;
                      c.recipients = adminrecipientemailaddresss.ToList();
                      c.sendingapplication = "InfoNotificationService";
                      c.systemaddress = systemsenderaddress;
                  }));
                 
                 message.sent = sendemail(message); //attempt to send the message
                 savemessage(message);  //save the message into database 

                 //TO DO remove this after testing is complete;
                 return returnmodel;

             }
             catch (Exception ex)
             {
                 //log error mesasge
                 new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
             }
            // return null;
         }

        //TO DO revisit this to send back the matches here
        public EmailViewModel sendemailmatchesemailbyprofileid(int profileid)
        {


            try
            {
                MembersViewModel model = _membersmapperrepository.getmemberdata(profileid);
                EmailViewModel returnmodel = new EmailViewModel();
                returnmodel.EmailMatches = _membersmapperrepository.getemailmatches(model);
                //get featured member
                returnmodel.FeaturedMember = _membersmapperrepository.getmembersearchviewmodel(profileid, featuredmemberid,false );
                returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberMatchesSentMemberNotification);
                returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberMatchestSentAdminNotificaton);
                //fill in the rest of the email model values 
                //fill in the rest of the email model values i.e format the subject and body
                //subject
                returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
                returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
                //Body
                returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.profile.screenname, returnmodel.EmailMatches.Count(), DateTime.Now, model.profile.id);
                returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.profile.username, model.profile.emailaddress, returnmodel.EmailMatches.Count(), DateTime.Now);
                //send the emails here i think trhough the service
                //once things are finally done there will he only a boolean return

                returnmodel.HasMatches = (returnmodel.EmailMatches.Count > 0) ? true : false;

                dynamic systemsenderaddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
                lu_template membertemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberMatchesSentMemberNotification ))) select x).First();
                lu_template admintemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberMatchestSentAdminNotificaton ))) select x).First();

                //build the recipeint addresses from contract us model i.f they dont exist create new ones 
                //First get system addresses for admin
                dynamic adminrecipientemailaddresss = (from x in (_notificationcontext.address.Where(f => f.id == (int)(addresstypeenum.SiteSupportAdmin))) select x);

                //create new addres for user who already has one otherwise just add to new item
                var memberrecipientaddress = (from x in (_notificationcontext.address.Where(f => f.emailaddress == model.profile.emailaddress )) select x);
                if (!(memberrecipientaddress.Count() > 0))
                {
                    var address = new address
                    {
                        addresstype = _notificationcontext.lu_addresstype.Where(f => f.id == (int)addresstypeenum.SiteUser).FirstOrDefault(),
                        emailaddress = model.profile.emailaddress, 
                        otheridentifer = model.profile.username,  //use this for chat notifications maybe
                        active = true,
                        creationdate = DateTime.Now
                    };
                    saveaddress(address);   //TO DO maybe remove this                      
                }

            
                //Build and send the user Message first               

                message message = new message();
                message.template = _notificationcontext.lu_template.Where(f => f.id == (int)templateenum.MemberMatchesSentMemberNotification ).First();
                message.messagetype = _notificationcontext.lu_messagetype.Where(f => f.id == (int)messagetypeenum.UserUpdate ).First();
                message.content = TemplateParser.RazorFileTemplate(membertemplate.filename, ref returnmodel);
                message.body = returnmodel.memberEmailViewModel.body;
                message.subject = returnmodel.memberEmailViewModel.subject;
                message.recipients = memberrecipientaddress.ToList();
                message.sendingapplication = "InfoNotificationService";
                message.creationdate = DateTime.Now;
                message.sendattempts = 0;
                message.systemaddress = systemsenderaddress;


                message.sent = sendemail(message); //attempt to send the message
                savemessage(message);  //save the message into database 

                ////now send the admin message
                ////use create method it like this 
                message = (message.Create(c =>
                {
                    //c.id = (int)templateenum.GenericErrorMessage;
                    c.template.id = (int)templateenum.MemberMatchestSentAdminNotificaton ;
                    c.messagetype.id = (int)messagetypeenum.SysAdminUpdate ;
                    c.content = TemplateParser.RazorFileTemplate(admintemplate.filename, ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                    c.body = returnmodel.adminEmailViewModel.body;
                    c.subject = returnmodel.adminEmailViewModel.subject;
                    c.recipients = adminrecipientemailaddresss.ToList();
                    c.sendingapplication = "InfoNotificationService";
                    c.systemaddress = systemsenderaddress;
                }));

                message.sent = sendemail(message); //attempt to send the message
                savemessage(message);  //save the message into database 

                //TO DO remove this after testing is complete;
                return returnmodel;

            }
            catch (Exception ex)
            {
                //log error mesasge
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw;
            }
          

          
          

        }

      public  EmailViewModel sendmembercreatedemail(registermodel model)
      {  
             try
             {
                 EmailViewModel returnmodel = new EmailViewModel();
                 returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
                 returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

                 //fill in the rest of the email model values i.e format the subject and body
                 //subject
                 returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
                 returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

                 //Body
                 returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username, model.activationcode);
                 returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.screenname, model.emailaddress );
                 return returnmodel;
             }
             catch (Exception ex)
             {
                 //log error mesasge
                 new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
             }
             return null;
      }

      public  EmailViewModel sendmembercreatedopenidemail(registermodel model) 
      {


          try
          {

              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, model.openidprovider);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.openidprovider, model.username);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.screenname, model.emailaddress , model.openidprovider);

              return returnmodel;
          }
          catch (Exception ex)
          {
              //log error mesasge
              new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
            throw;
          }
          return null;
      
      }

      public  EmailViewModel sendmemberpasswordchangedemail(LogonViewModel model) 
      {                

          try
          {
              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangeMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.LogOnModel.ScreenName, model.LogOnModel.UserName, model.LogOnModel.Password, model.LogOnModel.ProfileID);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.LogOnModel.UserName, model.LostAccountInfoModel.Email);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //log error mesasge
              new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw ;
          }
          return null;

      }

      public EmailViewModel sendmemberprofileactivatedemailbyprofileid(int profileid)
      {
      

          try
          {
              //get the profile info
              profile model = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel sendmemberactivationcoderecoveredemailbyprofileid(int profileid) 
      {             

          try
          {
              //get the profile info
              profile model =_membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username, model.activationcode);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel sendmemberemailmemssagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid) 
      {
         

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
              profile sender = _membersrepository.getprofilebyprofileid(  new ProfileModel { profileid = senderprofileid } );

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel sendmemberpeekreceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
      {
       

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
              profile sender =_membersrepository.getprofilebyprofileid(  new ProfileModel { profileid = senderprofileid } );

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel sendmemberlikereceivedemailbyprofileid(int recipientprofileid, int senderprofileid) {

 

           try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid( new ProfileModel { profileid =  recipientprofileid} );
              profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }         
          return null;
      
      }

      public  EmailViewModel sendmemberinterestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid) {
       
    

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
              profile sender =_membersrepository.getprofilebyprofileid(  new ProfileModel { profileid = senderprofileid } );

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel sendmemberchatrequestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid) {

         
          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
              profile sender =_membersrepository.getprofilebyprofileid(  new ProfileModel { profileid = senderprofileid } );
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public  EmailViewModel sendmemberofflinechatmessagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid) {
            

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
              profile sender =_membersrepository.getprofilebyprofileid(  new ProfileModel { profileid = senderprofileid } );
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;


      }

      public  EmailViewModel sendmemberphotorejectedemailbyprofileid(int profileid,int adminprofileid, photorejectionreasonEnum reason) {
              

          try
          {
              //get the profile info
              profile recipient =_membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });
              profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
              //get rejecttion reason
              string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, reasondesc);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress, admin.username);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public  EmailViewModel sendmemberphotouploadedemailbyprofileid(int profileid) {

   
          try
          {
              //get the profile info
              profile recipient =_membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });
              // profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
              //get rejecttion reason
              // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      
      }

      public EmailViewModel sendadminspamblockedemailbyprofileid(int blockedprofileid)
      {    

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockedprofileid });
              // profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
              //get rejecttion reason
              // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;

              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

 

      public EmailViewModel sendadminmemberspamblockedemailbyprofileid(int spamblockedprofileid,string reason,string blockedby)
      {
   

          try
          {
              //get the profile info
              profile blocked = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = spamblockedprofileid });
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, blockedby);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.username, blocked.emailaddress, reason);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public EmailViewModel sendadminmemberblockedemailbyprofileid(int blockedprofileid, int blockerprofileid)
      {
       

          try
          {
              //get the profile info
              profile blocked = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockedprofileid });
              profile blocker = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockerprofileid });

              //get the notes since the block was created first before email was sent
              var blockreason = _datingcontext.blocks.Where(p => p.blockprofile_id == blockedprofileid && p.id == blockedprofileid).FirstOrDefault();
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.emailaddress, blocker.emailaddress, blockreason.notes.FirstOrDefault());
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

        //There is no message for Members to know when photos are approved btw
      public EmailViewModel sendadminmemberphotoapprovedemailbyprofileid(int approvedprofileid, int adminprofileid)
      {

          try
          {
              //get the profile info
              profile profile = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = approvedprofileid });
              profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoApprovedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, profile.username, profile.emailaddress, admin.screenname);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null; 

     
      }

      public EmailModel getemailbytemplateid(templateenum template)
      {
          EmailModel emaildetail = new EmailModel();

          try
          {
              emaildetail.body = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().bodystring.description;
              emaildetail.subject = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().subjectstring.description;
              //TO DO figure out if we will populate other values here
              return emaildetail;
          }
          catch (Exception ex)
          {
              //handle logging here
          }         
          return null;
         
      }
        //Private reusable internal functions  
        private message sendemailtemplateinfo(templateenum template)
       {
           message newmessagedetail = new message();


           try
           {
               newmessagedetail.template = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault();
               newmessagedetail.body = newmessagedetail.template.bodystring.description;
               newmessagedetail.subject = newmessagedetail.template.subjectstring.description;
               return newmessagedetail;
           }
           catch (Exception ex)
           {
               //handle logging here
           }
           return null; 
            
      
       }


    }
}
