using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Nmedia.Services.Contracts;
using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure.Domain.Data.Errorlog;
using Nmedia.Infrastructure.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Domain.Data.Notification;
using Shell.MVC2.Infrastructure;
using System.Net.Mail;

namespace Nmedia.Services.Notification
{


    //TO do with this new patter either find a way to make extention methods use the generic repo dict in EFUnitOfWork or 
    //MOve the remaining ext method methods into this service layer that uses the EF unit of work
    //TO do also make a single Update method generic and implement that for the rest of the Repo saves.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class NotificationService : INotificationService
    {
        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;


        public NotificationService(IUnitOfWork unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

            //promotionrepository = _promotionrepository;
            _unitOfWork = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        }




        public EmailModel senderrormessage(Errorlog error, string addresstype)
        {



            EmailModel emailmodels = new EmailModel();


            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                      
                        dynamic systemsenderaddress = (from x in (db.GetRepository<systemaddress>().Find().Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
                        lu_template template = (from x in (db.GetRepository<lu_template>().Find().Where(f => f.id == (int)(templateenum.GenericErrorMessage))) select x).First();
                        lu_messagetype messagetype = (from x in (db.GetRepository<lu_messagetype>().Find().Where(f => f.id == (int)(messagetypeenum.DeveloperError))) select x).First();
                        var recipientemailaddresss = (from x in (db.GetRepository<address>().Find().Where(f => f.id == (int)(((addresstypeenum)Enum.Parse(typeof(addresstypeenum), addresstype))))) select x).ToList();

                        //build the recipient address objects
                        EmailModel returnmodel = new EmailModel();
                        returnmodel = getemailbytemplateid(templateenum.GenericErrorMessage, db);
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

                            c.id = (int)templateenum.GenericErrorMessage;
                            c.template = template;
                            c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                            c.body = TemplateParser.RazorFileTemplate(template.filename, ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                            c.subject = returnmodel.subject;
                            c.recipients = recipientemailaddresss;
                            c.sendingapplication = "NotificationService";
                            c.systemaddress = systemsenderaddress;
                        }));

                        db.Add(message);
                        int i = db.Commit();
                        transaction.Commit();

                        message.sent = sendemail(message); //attempt to send the message


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Generic Error");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        //log error mesasge
                        // new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning , ex, null, null,false);
                       
                    }
                }
            }
            return emailmodels;

        }

        //Private reusable internal functions  
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

        private EmailModel getemailbytemplateid(templateenum template, IUnitOfWork db)
        {
            EmailModel emaildetail = new EmailModel();

            try
            {
                emaildetail.body = db.GetRepository<lu_template>().Find().Where(p => p.id == (int)template).FirstOrDefault().bodystring.description;
                emaildetail.subject = db.GetRepository<lu_template>().Find().Where(p => p.id == (int)template).FirstOrDefault().subjectstring.description;
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
        private message sendemailtemplateinfo(templateenum template, IUnitOfWork db)
        {
            message newmessagedetail = new message();


            try
            {
                newmessagedetail.template = db.GetRepository<lu_template>().Find().Where(p => p.id == (int)template).FirstOrDefault();
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
