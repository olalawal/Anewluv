using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces ;

namespace Shell.MVC2.Data
{
 


    using System.Net.Mail;

    // NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
    public class ErrorNotificationRepository : IErrorNotificationRepository
    {

        private NotificationContext _notificationcontext;

        public ErrorNotificationRepository(NotificationContext notificationcontext)
        {
            _notificationcontext = notificationcontext;
        }


        public int SendErrorMessageToDevelopers(CustomErrorLog customerror)
	{

       

		//using (_notificationcontext ) {
			try {
				//get the recipients 
				//Dim recipeints As New List(Of String)

				// recipeints = context.MessageSystemAddresses.Where(Function(c) c.SystemAddressType = CInt(AddressType))
                dynamic recipientEmailAddresss = (from x in (_notificationcontext.address.Where(f => f.id == Convert.ToInt32(addresstypeenum.Developer))) select x);
				dynamic SystemSenderAddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == Convert.ToInt32(systemaddresstypeenum.DoNotReplyAddress))) select x).First();

				// Perform validation on the updated order before applying the changes.
				message message = new message();

                //message = (message.Create( c =>
                //{	



                //    c.MessageTemplateLookupId =  (int)MessageTemplateEnum.GenericErrorMessage;
                //c.MessageTypeLookupId = (int)MessageTypeEnum.DeveloperError;
                //    c.Body = c.MessageTemplate == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
                //                                         TemplateParser.RazorDBTemplate(message.MessageTemplate.RazorTemplateBody,ref customerror);
                //    c.Subject = string.Format("An error occured");
                //    c.Recipients = recipientEmailAddresss.ToList();
                //    c.SendingApplication = "ErrorNotificationService";
                //    c.SystemSender = SystemSenderAddress;
                //}));


                  //use create method it like this 
              message=  (message.Create (c =>
               {
                   c.id = (int)templateenum.GenericErrorMessage;
                   c.messagetype.id = (int)messagetypeenum.DeveloperError;
                   c.body = c.template == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
                                                        TemplateParser.RazorDBTemplate(message.template.razortemplatebody, ref customerror);
                   c.subject = string.Format("An error occured");
                   c.recipients = recipientEmailAddresss.ToList();
                   c.sendingapplication  = "ErrorNotificationService";
                   c.systemaddress = SystemSenderAddress;
               }));

               // c.body = c.template == null ? TemplateParser.RazorFileTemplate("", ref customerror) :
                  //                                      TemplateParser.RazorDBTemplate(message.template.razorTemplateBody, ref customerror);

				//'parse the message body from the template

              var messsage = new message 
              { 
               id = (int)templateenum.GenericErrorMessage,
                template = new lu_template { id = 1},
                     messagetype  = new lu_messagetype { id = (int)messagetypeenum.DeveloperError},
                   body  = message.template.razortemplatebody == null ? TemplateParser.RazorFileTemplate("", ref customerror) : TemplateParser.RazorDBTemplate(message.template.razortemplatebody, ref customerror),
                     subject  = string.Format("An error occured"),
                   recipients = recipientEmailAddresss.ToList(),
                   sendingapplication = "ErrorNotificationService",
                   systemaddress = SystemSenderAddress
              
              };


				// The Add method examines the change tracking information 
				// contained in the graph of self-tracking entities to infer the set of operations
				// that need to be performed to reflect the changes in the database. 
				//Dim ddd = New CustomErrorLog()
				//ddd.Message = errormessage

				//send the pyysicall email message here
				message.sent = SendEmail(message);
				//now that the sent flag has been updated we can add and save the message 
				//same thing similar would be possible for a chat based service I imagnge
				_notificationcontext.messages.Add(message);
                _notificationcontext.SaveChanges();
				return 1;
			} catch (UpdateException ex) {
				throw new InvalidOperationException("Failed to send a mail message. Try your request again.");
				//TO Do log this , sort of recursive

			}
		


	}


        private bool SendEmail(message message)
        {
            bool isEmailSendSuccessfully = false;

            try
            {
                foreach (address recip_loopVariable in message.recipients)
                {
                   var recip = recip_loopVariable;
                   System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(message.systemaddress.emailaddress, recip_loopVariable.emailaddress );
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
                string ErrorMessage = ex.Message;
                isEmailSendSuccessfully = false;

            }

            return isEmailSendSuccessfully;
        }


      

    }

}
