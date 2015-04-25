using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


using System.ServiceModel.Activation;
using Nmedia.Services.Contracts;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data.Notification;

using System.Net.Mail;
using SendGrid;

using System.Configuration;
using Nmedia.Infrastructure;

using Nmedia.Infrastructure.Domain;
using System.Threading.Tasks;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.Mvc;
using Repository.Pattern.UnitOfWork;
using Nmedia.Infrastructure.DependencyInjection;



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

          private readonly IUnitOfWorkAsync _unitOfWorkAsync;
          private readonly IUnitOfWorkAsync _unitOfWorkAsyncAnewluv;

          public NotificationService([INotificationEntitiesScope]IUnitOfWorkAsync unitOfWork, [IAnewluvEntitesScope]IUnitOfWorkAsync unitofWorkAnewluv)
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
            _unitOfWorkAsync = unitOfWork;
            _unitOfWorkAsyncAnewluv = unitofWorkAnewluv;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        }


       /// <summary>
       /// TO DO cache these as well similar to lookup service
       /// </summary>
       /// <returns></returns>
        #region "Lookups"

        public async Task<List<lu_template>> gettemplatelist()
        {

            var task = Task.Factory.StartNew(() =>
            {
                var list = new List<lu_template>();
              //    using (var db = _unitOfWork)
                {
                   // db.IsAuditEnabled = false; //do not audit on adds
                  //   db.DisableProxyCreation = true;
                   
                        try
                        {
                            list = _unitOfWorkAsync.Repository<lu_template>().Queryable().OrderBy(x => x.id).ToList();
                        }
                        catch (Exception ex)
                        {
                            //// transaction.Rollback();
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Generic Error");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            //log the error but dont notifiy
                            new Logging(applicationEnum.NotificationService).WriteSingleEntry(logseverityEnum.Warning,enviromentEnum.dev, ex, null, null, false);
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }

                        return list;
                    
                }
            });
            return await task.ConfigureAwait(false);


        }

        public async Task<List<systemaddress>> getsystemaddresslist()
        {

            var task = Task.Factory.StartNew(() =>
            {
                var list = new List<systemaddress>();
              //    using (var db = _unitOfWork)
                {
                   // db.IsAuditEnabled = false; //do not audit on adds
                  //   db.DisableProxyCreation = true;

                    try
                    {
                        list = _unitOfWorkAsync.Repository<systemaddress>().Queryable().OrderBy(x => x.id).ToList();
                    }
                    catch (Exception ex)
                    {
                        //// transaction.Rollback();
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Generic Error");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        //log the error but dont notifiy
                        new Logging(applicationEnum.NotificationService).WriteSingleEntry(logseverityEnum.Warning, enviromentEnum.dev, ex, null, null, false);
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                    return list;

                }
            });
            return await task.ConfigureAwait(false);


        }

        public async Task<List<lu_messagetype>> getmessagetypelist()
        {

            var task = Task.Factory.StartNew(() =>
            {
                var list = new List<lu_messagetype>();
              //    using (var db = _unitOfWork)
                {
                   // db.IsAuditEnabled = false; //do not audit on adds
                  //   db.DisableProxyCreation = true;

                    try
                    {
                        list = _unitOfWorkAsync.Repository<lu_messagetype>().Queryable().OrderBy(x => x.id).ToList();
                    }
                    catch (Exception ex)
                    {
                        //// transaction.Rollback();
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Generic Error");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        //log the error but dont notifiy
                        new Logging(applicationEnum.NotificationService).WriteSingleEntry(logseverityEnum.Warning, enviromentEnum.dev, ex, null, null, false);
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                    return list;

                }
            });
            return await task.ConfigureAwait(false);


        }

        #endregion


        /// <summary>
        /// addresses are hard coded for developers so need for the emial model to be passed
        /// </summary>
        /// <param name="error"></param>
        /// <param name="addresstype"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>     
        public async Task  senderrormessage(log error, string systemaddresstype)
        {

            

          //    using (var db = _unitOfWork)
            {
               // db.IsAuditEnabled = false; //do not audit on adds
                //db.DisableProxyCreation = true;
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        //parse the address type
                       
                        //Task that returns nothing
                        await Task.Factory.StartNew (() =>
                        {
                            
                        //Id's messed up in DB use the first 
                        dynamic systemsenderaddress = (from x in (_unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList()) select x).FirstOrDefault();
                        message message = new message();
                        //11-29-2013 get the template path from web config
                        var TemplatePath = ConfigurationManager.AppSettings["razortemplatefilelocation"];
                           
                      
                        lu_template template = (from x in (_unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(f => f.id == 1)) select x).FirstOrDefault(); 

                        lu_messagetype messagetype = (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id == (int)(messagetypeenum.DeveloperError))) select x).FirstOrDefault(); 
                        var recipientemailaddresss = (from x in (_unitOfWorkAsync.Repository<address>().Queryable().ToList().Where(f => f.addresstype.id == (int)(addresstypeenum.Developer))) select x).ToList();

                            //TO DO show the profile id 
                        EmailViewModel returnmodel = new EmailViewModel
                        {
                            EmailModel  = new EmailModel { subject = template.subject.description, body = template.body.description }
                        };
                       

                        message = (message.Create(c =>
                        {

                            c.id = (int)templateenum.GenericErrorMessage;
                            c.template = template;
                            c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                            c.body = TemplateParser.RazorFileTemplate(template.filename.description, ref returnmodel, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                            c.subject = returnmodel.EmailModel.subject;
                            c.recipients = recipientemailaddresss;
                            c.sendingapplication = "NotificationService";
                            c.systemaddress = systemsenderaddress;
                        }));

                        message.sent = message.body != null ? sendemail(message) : false;//attempt to send the message
                        message.sendattempts = message.body != null ? 1 : 0;
                        _unitOfWorkAsync.Repository<message>().Insert(message);
                        var j = _unitOfWorkAsync.SaveChanges();
                       // transaction.Commit();
                        });
                       // return task ;

                        //return new CompletedAsyncResult<EmailModel>(returnmodel);
                    }
                    catch (Exception ex)
                    {
                        //// transaction.Rollback();
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Generic Error");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        //log the error but dont notifiy
                        // new Logging(applicationEnum.notificationservice).WriteSingleEntry(logseverityEnum.Warning, LogenviromentEnum.dev, ex, null, null, false);
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        //log error mesasge
                        // new Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning , ex, null, null,false);

                    }
                }
            }




        }


        public async Task<string> sendmessagesbytemplate(List<EmailModel> models)
        {
            //parse the address type
            //determine if we are sending a To or From email.

                 try
                 {
                        // var templateenum = (templateenum)Enum.Parse(typeof(templateenum), model.templateid);
                        //Id's messed up in DB use the first 
                        //6	1	Anewluv@sendmail.com	NULL	smtp.sendgrid.net	olawal	azure_b5dd8d41de89841c3093bbb13c07425d@azure.com	kw8LHWnK9rnH7zQ	True	2015-04-20 00:00:00.000	NULL               
                        dynamic systemsenderaddress = _unitOfWorkAsync.Repository<systemaddress>().Query(a=>a.id == (int)systemaddresseenum.SendGridSMTPrelay).Select().FirstOrDefault();
                        //11-29-2013 get the template path from web config
                        var TemplatePath = ConfigurationManager.AppSettings["razortemplatefilelocation"];

                        //Task that returns nothing
                        await Task.Factory.StartNew(() =>
                        {
                             foreach(EmailModel model in models)
                             {
                                try
                                 {
                                   

                                    if (model != null)
                                    {
                                        EmailViewModel currentEmailViewModel = new EmailViewModel();
                                        message message = new message();
                                        ICollection<address> addresses = new List<address>();

                                       //TO DO load from cache
                                        var template = _unitOfWorkAsync.Repository<lu_template>().Query(f => f.id == model.templateid)
                                            .Include(z => z.filename).Include(z => z.body).Include(z => z.subject).Select().FirstOrDefault();

                                        currentEmailViewModel = getemailVMbyEmailModel(model, template);
                                        //model.memberEmailViewModel = currentEmailModel;
                                        //create the user address
                                        addresses.Add(getorcreateaddaddress(model, _unitOfWorkAsync));

                                        //the member message created and sent here
                                        message = (message.Create(c =>
                                        {
                                            c.template_id = template.id;
                                            c.messagetype_id = currentEmailViewModel.EmailModel.messagetypeid;
                                            c.body = TemplateParser.RazorFileTemplate(template.filename.description + ".cshtml", ref currentEmailViewModel, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                                            c.subject = currentEmailViewModel.EmailModel.subject;
                                            c.recipients = addresses;
                                            c.sendingapplication = "NotificationService";
                                            c.systemaddress = systemsenderaddress;
                                        }));

                                        message.sent = message.body != null ? sendemail(message) : false;//attempt to send the message
                                        message.sendattempts = message.body != null ? 1 : 0;
                                        //  db.Add(message);
                                        // int j = db.Commit();

                                        _unitOfWorkAsync.Repository<message>().Insert(message);
                                        var j = _unitOfWorkAsync.SaveChanges();
                                    }                         

                                 }
                                 catch (Exception ex)
                                 {
                                     //log individual error for this email and move to next
                                 }
                             }

                             return "true";
                        });
                    }
                    catch (Exception ex)
                    {
                        //to do log error
                        //thow service error

                    }

                 return "false";
      }
       



        #region "Private methods"

        private EmailViewModel getemailVMbyEmailModel(EmailModel model, lu_template template)
        {
            EmailViewModel emaildetail = new EmailViewModel();

            try
            {

                string subject = template.subject.description;
                string body = template.body.description;
                templateenum selectedtemplate = (templateenum)model.templateid;


                switch (selectedtemplate)
                {
                    case templateenum.GenericErrorMessage:
                        // Console.WriteLine("Case 1");
                        break;
                    case templateenum.MemberPasswordChangeMemberNotification:
                        model.subject = subject;
                        model.body = string.Format(body, model.screenname, model.username, model.passwordtoken);
                        emaildetail.EmailModel = model;  //add the new updated model
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }



                //TO DO figure out if we will populate other values here
                return emaildetail;
            }
            catch (Exception ex)
            {
                //handle logging here
                using (var logger = new Logging(applicationEnum.NotificationService))
                {
                    logger.WriteSingleEntry(logseverityEnum.Warning, enviromentEnum.dev, ex, null, null, false);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason              
                }
            }
            return null;

        }

            
       //Private reusable internal functions 
        //TO DO this should be handled as a separate send for each so we can update the susccess individually
        //Private reusable internal functions  
        //TO DO this should be handled as a separate send for each so we can update the susccess individually
        private bool sendemail(message message)
        {
            bool isEmailSendSuccessfully = false;


            try
            {
                //SmtpClient oSmtpClient = new SmtpClient();
                //MailMessage oMailMessage = new MailMessage();
                var FromAddress = "noreply@anewluv.com"; // (message.systemaddress == null | message.systemaddress.emailaddress == null | message.systemaddress.emailaddress == "") ? "MISReporting@wellsfargo.com" : message.systemaddress.emailaddress;

                foreach (address recip_loopVariable in message.recipients)
                {
                    // Create the email object first, then add the properties.
                    var myMessage = new SendGridMessage();

                    var recip = recip_loopVariable;
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(FromAddress, recip_loopVariable.emailaddress);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = message.subject;
                    mailMessage.Body = message.body;
                   // SmtpClient smtp = new SmtpClient();
                    //using GO Daddy btw from address should be a godaddy address too
                    var smtp = new SmtpClient("relay-hosting.secureserver.net");
                   /// http://stackoverflow.com/questions/8554567/godaddy-send-email
                   /// 
                    smtp.Host = !string.IsNullOrEmpty(message.systemaddress.hostname) ? message.systemaddress.hostname : message.systemaddress.hostip;
                    //smtp.Credentials()
                    //TO DO no credentials required i think
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

                //// transaction.Rollback();
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Generic Error");
                //string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                //log the error but dont notifiy
                using (var logger = new  Logging(applicationEnum.LoggingService))
                {
                    logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex,null, null, false);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason              
                }
                return isEmailSendSuccessfully;
            }

            return isEmailSendSuccessfully;
        }
       
       //TO do might want to be able to send multiple emails at once instead instead of verifiying each 
        private address getorcreateaddaddress(EmailModel Model, IUnitOfWorkAsync db)
       {
       
         //check to see if the recipeint address already exists if not add i
                        address current =
                        _unitOfWorkAsync.Repository<address>().Queryable().ToList()
                        .Where(p => p.emailaddress.ToUpper() == Model.emailaddress.ToUpper() && p.addresstype_id == Model.addresstypeid).FirstOrDefault();
                            //_unitOfWorkAsync.Repository<address>().Queryable().ToList().Where(p => p.emailaddress.ToUpper() == Model.EmailModel.to && Model.EmailModel.addresstypefrom == addresstypeenum.SiteUser).FirstOrDefault();

                         
                        if (current == null)
                        {
                            current = new address();
                           // var addresstype = new lu_addresstype();
                         //   current.addresstype = addresstype;
                            //add the email address                         
                            current.addresstype_id = Model.addresstypeid;//:  (int)Model.EmailModel.addresstypefrom;
                            current.username = Model.username;
                            current.emailaddress = Model.emailaddress;  //IsToAddress == true? Model.EmailModel.to:Model.EmailModel.from;
                            current.active = true;
                            current.creationdate = DateTime.Now;
                            _unitOfWorkAsync.Repository<address>().Insert(current); 
                            var i = db.SaveChanges();
                        }
                      
                        //get the ID since it is required either way , got to be a betetr way to do this than to query twice
                       // var currentaddress = _unitOfWorkAsync.Repository<address>().Queryable().Where(p => p.emailaddress.ToUpper() == model.to.ToUpper() && p.addresstype_id == (int)addresstypeenum.PromotionUser).FirstOrDefault();

                       //ICollection<address> addresses = new List<address>();
                        //add the address 
                       // addresses.Add(current);

                    
                        message message = new message();
                        return current;
       
       }              
       
        //Private reusable internal functions  
        private message sendemailtemplateinfo(templateenum template, IUnitOfWorkAsync db)
        {
            message newmessagedetail = new message();


            try
            {
                newmessagedetail.template = _unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(p => p.id == (int)template).FirstOrDefault();
                newmessagedetail.body = newmessagedetail.template.body.description;
                newmessagedetail.subject = newmessagedetail.template.subject.description;
                return newmessagedetail;
            }
            catch (Exception ex)
            {
                //handle logging here
            }
            return null;


        }
        #endregion
       
    }
}


   //if (model.adminEmailViewModel != null)
   //                         {
   //                             //send admin email here
   //                             //************************************************************

   //                             EmailModel currentEmailModel = new EmailModel();
   //                             message message = new message();
   //                             ICollection<address> addresses = new List<address>();
   //                             //get the template 
   //                             var template = _unitOfWorkAsync.Repository<lu_template>().Query(f => f.id == model.adminEmailViewModel.templateid)
   //                                 .Include(z => z.filename).Include(z => z.body).Include(z => z.subject).Select().FirstOrDefault();
                               
   //                              //get the body and subject formatted
   //                             currentEmailModel = getemailbyEmailViewModel(model.adminEmailViewModel, template);
   //                             model.adminEmailViewModel = currentEmailModel;
   //                             //get the addresss for all admins for now down the line filter based on message type ?
   //                             addresses = _unitOfWorkAsync.Repository<address>().Query(z => z.addresstype_id == (int)addresstypeenum.SystemAdmin).Select().ToList();

   //                             //the member message created and sent here
   //                             message = (message.Create(c =>
   //                             {
   //                                 c.template_id = template.id;
   //                                 c.messagetype_id = currentEmailModel.messagetypeid;
   //                                 c.body = TemplateParser.RazorFileTemplate(template.filename.description + ".cshtml", ref model, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
   //                                 c.subject = currentEmailModel.subject;
   //                                 c.recipients = addresses;
   //                                 c.sendingapplication = "NotificationService";
   //                                 c.systemaddress = systemsenderaddress;
   //                             }));


   //                             currentEmailModel = getemailbyEmailViewModel(model.memberEmailViewModel, template, _unitOfWorkAsync);
   //                             message.sendattempts = message.body != null ? 1 : 0;
   //                             //  db.Add(message);
   //                             // int j = db.Commit();

   //                             _unitOfWorkAsync.Repository<message>().Insert(message);
   //                            var j = _unitOfWorkAsync.SaveChanges();
   //                         }