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

using System.Configuration;
using Nmedia.Infrastructure;

using Nmedia.Infrastructure.Domain;
using System.Threading.Tasks;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.Mvc;
using Repository.Pattern.UnitOfWork;



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


        public NotificationService(IUnitOfWorkAsync unitOfWork)
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
        // public IAsyncResult 
        public async Task  senderrormessage(log error, string systemaddresstype)
        {

            EmailModel emailmodels = new EmailModel();

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

                        var systemaddresstypeenum = (systemaddresstypeenum)Enum.Parse(typeof(systemaddresstypeenum), systemaddresstype);
                        //Id's messed up in DB use the first 
                        dynamic systemsenderaddress = (from x in (_unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList()) select x).FirstOrDefault(); 
                        lu_template template = (from x in (_unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(f => f.id == 1)) select x).FirstOrDefault(); 

                        lu_messagetype messagetype = (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id == (int)(messagetypeenum.DeveloperError))) select x).FirstOrDefault(); 
                        var recipientemailaddresss = (from x in (_unitOfWorkAsync.Repository<address>().Queryable().ToList().Where(f => f.addresstype.id == (int)(addresstypeenum.Developer))) select x).ToList();

                        //build the recipient address objects
                        EmailModel returnmodel = new EmailModel();
                        returnmodel = getemailbytemplateid(templateenum.GenericErrorMessage, _unitOfWorkAsync);
                        //fill in the rest of the email model values 
                        returnmodel.subject = String.Format(returnmodel.subject, error.profileid);
                        returnmodel.body = String.Format(returnmodel.body, error.profileid, error.message);


                        //Now build the message e
                        // recipeints = context.MessageSystemAddresses.Where(Function(c) c.SystemAddressType = CInt(AddressType))
                        //Perform validation on the updated order before applying the changes.
                        message message = new message();
                        //use create method it like this 

                        //11-29-2013 get the template path from web config
                        var TemplatePath = ConfigurationManager.AppSettings["razortemplatefilelocation"];

                        message = (message.Create(c =>
                        {

                            c.id = (int)templateenum.GenericErrorMessage;
                            c.template = template;
                            c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                            c.body = TemplateParser.RazorFileTemplate(template.filename.description, ref returnmodel, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                            c.subject = returnmodel.subject;
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


        public async Task sendmessagebytemplate(EmailViewModel model)
        {

           // EmailViewModel viewmodel = new EmailViewModel();

          //    using (var db = _unitOfWork)
            {
               // db.IsAuditEnabled = false; //do not audit on adds
                //db.DisableProxyCreation = true;
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        //parse the address type

                        //determine if we are sending a To or From email.


                        //Task that returns nothing
                        await Task.Factory.StartNew(() =>
                        {

                           // var templateenum = (templateenum)Enum.Parse(typeof(templateenum), model.templateid);


                            //Id's messed up in DB use the first 
                            dynamic systemsenderaddress = (from x in (_unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList()) select x).FirstOrDefault();
                            message message = new message();
                            //11-29-2013 get the template path from web config
                            var TemplatePath = ConfigurationManager.AppSettings["razortemplatefilelocation"];

                            //member part
                            //************************************************************************
                            lu_template template = (from x in (_unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(f => f.id == model.userEmailViewModel.templateid)) select x).FirstOrDefault();                             
                            lu_messagetype messagetype = (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id ==  model.userEmailViewModel.messagetypeid )) select x).FirstOrDefault();
                            lu_addresstype addresstype = (from x in (_unitOfWorkAsync.Repository<lu_addresstype>().Queryable().ToList().Where(f => f.id == model.userEmailViewModel.addresstypeid)) select x).FirstOrDefault(); 

                         
                            ICollection<address> addresses = new List<address>();

                            //create the user address
                            addresses.Add(getorcreateaddaddress(model,_unitOfWorkAsync));
                        

                            //the member message created and sent here
                            message = (message.Create(c =>
                            {

                                c.id = model.userEmailViewModel.templateid;
                                c.template = template;
                                c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                                c.body = TemplateParser.RazorFileTemplate(template.filename.description, ref model, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                                c.subject = model.userEmailViewModel.subject;
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
                         
                            //send admin email here
                            //************************************************************
                             template = (from x in (_unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(f => f.id == model.adminEmailViewModel.templateid)) select x).FirstOrDefault();
                             messagetype = (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id == model.adminEmailViewModel.messagetypeid)) select x).FirstOrDefault();
                             addresstype = (from x in (_unitOfWorkAsync.Repository<lu_addresstype>().Queryable().ToList().Where(f => f.id == model.adminEmailViewModel.addresstypeid)) select x).FirstOrDefault(); 

                             //get the addresss for all admins for now down the line filter based on message type ?
                             addresses = _unitOfWorkAsync.Repository<address>().Query(z => z.addresstype_id ==  (int)addresstypeenum.SystemAdmin ).Select().ToList();

                             //the member message created and sent here
                             message = (message.Create(c =>
                             {

                                 c.id = model.adminEmailViewModel.templateid;
                                 c.template = template;
                                 c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
                                 c.body = TemplateParser.RazorFileTemplate(template.filename.description, ref model, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
                                 c.subject = model.adminEmailViewModel.subject;
                                 c.recipients = addresses;
                                 c.sendingapplication = "NotificationService";
                                 c.systemaddress = systemsenderaddress;
                             }));

                             message.sent = message.body != null ? sendemail(message) : false;//attempt to send the message
                             message.sendattempts = message.body != null ? 1 : 0;
                             //  db.Add(message);
                             // int j = db.Commit();

                             _unitOfWorkAsync.Repository<message>().Insert(message);
                              j = _unitOfWorkAsync.SaveChanges();


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


        ////Send generic message using the noted email viewmodel to determine the template etc and everything else requires
        //public async Task sendmessage(EmailViewModel model, string systemaddresstype)
        //{

        //    EmailModel emailmodels = new EmailModel();

        //    using (var db = new NotificationContext())
        //    {
        //       // db.IsAuditEnabled = false; //do not audit on adds
        //        //db.DisableProxyCreation = true;
        //     //   using (var transaction = db.BeginTransaction())
        //        {

        //            try
        //            {


        //                  //Task that returns nothing
        //                await Task.Factory.StartNew(() =>
        //                {


        //                //parse the address type
        //                var systemaddresstypeenum = (systemaddresstypeenum)Enum.Parse(typeof(systemaddresstypeenum), systemaddresstype);
        //                int value = (int)systemaddresstypeenum;
        //                // var list = _unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList();
        //                var addresstypes = _unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList();

        //                //  dynamic systemsenderaddress = (from x in (_unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList()) select x).First();

        //                systemaddress systemsenderaddress = (from x in (_unitOfWorkAsync.Repository<systemaddress>().Queryable().ToList().Where(f => f.systemaddresstype_id == value)) select x).FirstOrDefault();
        //                lu_template template = (from x in (_unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(f => f.id == (int)(model.template.id))) select x).First();
        //                //verify message type, if meessage type is empty just send the user update
        //                var messagetype = model.messagetype != null ? (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id == model.messagetype.id)) select x).First() :
        //                                                          (from x in (_unitOfWorkAsync.Repository<lu_messagetype>().Queryable().ToList().Where(f => f.id == (int)(messagetypeenum.UserUpdate))) select x).First();
        //                //check to see if the recipeint address already exists if not add i
        //                //took out filtering for now , if an email address was passed we will just send it , trusting the sending app.
        //                address current = _unitOfWorkAsync.Repository<address>().Queryable().ToList().Where(p => p.emailaddress.ToUpper() == model.userEmailViewModel.to.ToUpper()).FirstOrDefault(); //&& (p.addresstype_id == (int)addresstypeenum.PromotionUser | p.addresstype.description.ToUpper() == "DEVELOPER")  ).FirstOrDefault();

        //                if (current == null)
        //                {
        //                    current = new address();
        //                    // var addresstype = new lu_addresstype();
        //                    //   current.addresstype = addresstype;
        //                    //add the email address                         
        //                    current.addresstype_id = model.userEmailViewModel.addresstype != null ? model.userEmailViewModel.addresstype.GetValueOrDefault() : (int)addresstypeenum.PromotionUser;
        //                    current.username = model.userEmailViewModel.username;
        //                    current.otheridentifer = model.adminEmailViewModel.userlogon;
        //                    current.emailaddress = model.userEmailViewModel.to;
        //                    current.active = true;
        //                    current.creationdate = DateTime.Now;
        //                    db.Add(current);
        //                    int i = db.Commit();
        //                }

        //                //get the ID since it is required either way , got to be a betetr way to do this than to query twice
        //                // var currentaddress = _unitOfWorkAsync.Repository<address>().Queryable().Where(p => p.emailaddress.ToUpper() == model.to.ToUpper() && p.addresstype_id == (int)addresstypeenum.PromotionUser).FirstOrDefault();

        //                ICollection<address> addresses = new List<address>();
        //                //add the address 
        //                addresses.Add(current);


        //                message message = new message();
        //                //use create method it like this 

        //                //11-29-2013 get the template path from web config
        //                var TemplatePath = ConfigurationManager.AppSettings["razortemplatefilelocation"];

        //                message = (message.Create(c =>
        //                {
        //                    //c.id = (int)templateenum.;
        //                    c.template = template;
        //                    c.messagetype = messagetype; //(int)messagetypeenum.DeveloperError;
        //                    c.body = TemplateParser.RazorFileTemplate(template.filename.description, ref model, TemplatePath); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
        //                    c.subject = model.userEmailViewModel.subject;
        //                    c.recipients = addresses;
        //                    c.sendingapplication = "NotificationService";
        //                    c.systemaddress = systemsenderaddress;
        //                }));


        //                message.sent = message.body != null ? sendemail(message) : false;//attempt to send the message
        //                message.sendattempts = message.body != null ? 1 : 0;
        //                db.Add(message);
        //                int j = db.Commit();
        //               // transaction.Commit();
        //                });
        //                // return new CompletedAsyncResult<EmailModel>(model);
        //            }
        //            catch (Exception ex)
        //            {
        //                //// transaction.Rollback();
        //                //can parse the error to build a more custom error mssage and populate fualt faultreason
        //                FaultReason faultreason = new FaultReason("Generic Error");
        //                //string ErrorMessage = "";
        //                string ErrorDetail = "ErrorMessage: " + ex.Message;
        //                //log the error but dont notifiy
        //                new Logging(applicationEnum.notificationservice).WriteSingleEntry(logseverityEnum.Warning, LogenviromentEnum.dev, ex, null, null, false);
        //                //throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //                //log error mesasge
        //                // new Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning , ex, null, null,false);

        //            }

        //        }

        //    }
        //}

        
        ////TO Do find a way to differentiate between user and Member
        //public EmailViewModel sendcontactusemail(ContactUsModel model)
        //{

        //    try
        //    {
        //        // var testmembertemplate =(int)(templateenum.MemberContactUsMemberMesage );
        //        //var testadmintemplate = (int)(templateenum.MemberContactUsAdminMessage);

        //        dynamic systemsenderaddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
        //        lu_template membertemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberContactUsMemberMesage))) select x).First();
        //        lu_template admintemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberContactUsAdminMessage))) select x).First();

        //        //build the recipeint addresses from contract us model i.f they dont exist create new ones 
        //        //First get system addresses for admin
        //        dynamic adminrecipientemailaddresss = (from x in (_notificationcontext.address.Where(f => f.id == (int)(addresstypeenum.SiteSupportAdmin))) select x);

        //        //create new addres for user who already has one otherwise just add to new item
        //        var memberrecipientaddress = (from x in (_notificationcontext.address.Where(f => f.emailaddress == model.Email)) select x);
        //        if (!(memberrecipientaddress.Count() > 0))
        //        {
        //            var address = new address
        //            {
        //                addresstype = _notificationcontext.lu_addresstype.Where(f => f.id == (int)addresstypeenum.SiteUser).FirstOrDefault(),
        //                emailaddress = model.Email,
        //                otheridentifer = model.Name,  //use this for chat notifications maybe
        //                active = true,
        //                creationdate = DateTime.Now
        //            };
        //            saveaddress(address);   //TO DO maybe remove this                      
        //        }

        //        //TO DO remove these large models after testing is complete
        //        EmailViewModel returnmodel = new EmailViewModel();

        //        //12-19-2012 olawal Here we added this  , so we can populate the screen name
        //        MembersViewModel membersviewmodel = new MembersViewModel();
        //        profile profile = new profile { screenname = model.Name, emailaddress = model.Email };
        //        membersviewmodel.profile = profile;
        //        //add it
        //        returnmodel.MembersViewModel = membersviewmodel;

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsMemberMesage);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsAdminMessage);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.Name, model.Subject, model.Message);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.Name, model.Email, model.Subject, model.Message);

        //        //Build and send the user Message first               

        //        message message = new message();
        //        message.template = _notificationcontext.lu_template.Where(f => f.id == (int)templateenum.MemberContactUsMemberMesage).First();
        //        message.messagetype = _notificationcontext.lu_messagetype.Where(f => f.id == (int)messagetypeenum.UserContactUsnotification).First();
        //        message.content = TemplateParser.RazorFileTemplate(membertemplate.filename, ref returnmodel);
        //        message.body = returnmodel.memberEmailViewModel.body;
        //        message.subject = returnmodel.memberEmailViewModel.subject;
        //        message.recipients = memberrecipientaddress.ToList();
        //        message.sendingapplication = "InfoNotificationService";
        //        message.creationdate = DateTime.Now;
        //        message.sendattempts = 0;
        //        message.systemaddress = systemsenderaddress;


        //        message.sent = sendemail(message); //attempt to send the message
        //        savemessage(message);  //save the message into Initial Catalog= 

        //        ////now send the admin message
        //        ////use create method it like this 
        //        message = (message.Create(c =>
        //        {
        //            //c.id = (int)templateenum.GenericErrorMessage;
        //            c.template.id = (int)templateenum.MemberContactUsAdminMessage;
        //            c.messagetype.id = (int)messagetypeenum.UserContactUsnotification;
        //            c.content = TemplateParser.RazorFileTemplate(admintemplate.filename, ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
        //            c.body = returnmodel.adminEmailViewModel.body;
        //            c.subject = returnmodel.adminEmailViewModel.subject;
        //            c.recipients = adminrecipientemailaddresss.ToList();
        //            c.sendingapplication = "InfoNotificationService";
        //            c.systemaddress = systemsenderaddress;
        //        }));

        //        message.sent = sendemail(message); //attempt to send the message
        //        savemessage(message);  //save the message into Initial Catalog= 

        //        //TO DO remove this after testing is complete;
        //        return returnmodel;

        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
        //        throw;
        //    }
        //    // return null;
        //}

        ////TO DO revisit this to send back the matches here
        //public EmailViewModel sendemailmatchesemailbyprofileid(int profileid)
        //{


        //    try
        //    {
        //        MembersViewModel model = _membersmapperrepository.getmemberdata(profileid);
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.EmailMatches = _membersmapperrepository.getemailmatches(new ProfileModel { profileid = profileid });
        //        //get featured member
        //        returnmodel.FeaturedMember = _membersmapperrepository.getmembersearchviewmodel(profileid, featuredmemberid, false);
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberMatchesSentMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberMatchestSentAdminNotificaton);
        //        //fill in the rest of the email model values 
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.profile.screenname, returnmodel.EmailMatches.Count(), DateTime.Now, model.profile.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.profile.username, model.profile.emailaddress, returnmodel.EmailMatches.Count(), DateTime.Now);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return

        //        returnmodel.HasMatches = (returnmodel.EmailMatches.Count > 0) ? true : false;

        //        dynamic systemsenderaddress = (from x in (_notificationcontext.systemaddress.Where(f => f.id == (int)(systemaddresstypeenum.DoNotReplyAddress))) select x).First();
        //        lu_template membertemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberMatchesSentMemberNotification))) select x).First();
        //        lu_template admintemplate = (from x in (_notificationcontext.lu_template.Where(f => f.id == (int)(templateenum.MemberMatchestSentAdminNotificaton))) select x).First();

        //        //build the recipeint addresses from contract us model i.f they dont exist create new ones 
        //        //First get system addresses for admin
        //        dynamic adminrecipientemailaddresss = (from x in (_notificationcontext.address.Where(f => f.id == (int)(addresstypeenum.SiteSupportAdmin))) select x);

        //        //create new addres for user who already has one otherwise just add to new item
        //        var memberrecipientaddress = (from x in (_notificationcontext.address.Where(f => f.emailaddress == model.profile.emailaddress)) select x);
        //        if (!(memberrecipientaddress.Count() > 0))
        //        {
        //            var address = new address
        //            {
        //                addresstype = _notificationcontext.lu_addresstype.Where(f => f.id == (int)addresstypeenum.SiteUser).FirstOrDefault(),
        //                emailaddress = model.profile.emailaddress,
        //                otheridentifer = model.profile.username,  //use this for chat notifications maybe
        //                active = true,
        //                creationdate = DateTime.Now
        //            };
        //            saveaddress(address);   //TO DO maybe remove this                      
        //        }


        //        //Build and send the user Message first               

        //        message message = new message();
        //        message.template = _notificationcontext.lu_template.Where(f => f.id == (int)templateenum.MemberMatchesSentMemberNotification).First();
        //        message.messagetype = _notificationcontext.lu_messagetype.Where(f => f.id == (int)messagetypeenum.UserUpdate).First();
        //        message.content = TemplateParser.RazorFileTemplate(membertemplate.filename, ref returnmodel);
        //        message.body = returnmodel.memberEmailViewModel.body;
        //        message.subject = returnmodel.memberEmailViewModel.subject;
        //        message.recipients = memberrecipientaddress.ToList();
        //        message.sendingapplication = "InfoNotificationService";
        //        message.creationdate = DateTime.Now;
        //        message.sendattempts = 0;
        //        message.systemaddress = systemsenderaddress;


        //        message.sent = sendemail(message); //attempt to send the message
        //        savemessage(message);  //save the message into Initial Catalog= 

        //        ////now send the admin message
        //        ////use create method it like this 
        //        message = (message.Create(c =>
        //        {
        //            //c.id = (int)templateenum.GenericErrorMessage;
        //            c.template.id = (int)templateenum.MemberMatchestSentAdminNotificaton;
        //            c.messagetype.id = (int)messagetypeenum.SysAdminUpdate;
        //            c.content = TemplateParser.RazorFileTemplate(admintemplate.filename, ref returnmodel); // c.template == null ? TemplateParser.RazorFileTemplate("", ref error) :                                                            
        //            c.body = returnmodel.adminEmailViewModel.body;
        //            c.subject = returnmodel.adminEmailViewModel.subject;
        //            c.recipients = adminrecipientemailaddresss.ToList();
        //            c.sendingapplication = "InfoNotificationService";
        //            c.systemaddress = systemsenderaddress;
        //        }));

        //        message.sent = sendemail(message); //attempt to send the message
        //        savemessage(message);  //save the message into Initial Catalog= 

        //        //TO DO remove this after testing is complete;
        //        return returnmodel;

        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
        //        throw;
        //    }





        //}

        //public EmailViewModel sendmembercreatedemail(registermodel model)
        //{
        //    try
        //    {
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username, model.activationcode);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.screenname, model.emailaddress);
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
        //        throw;
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmembercreatedopenidemail(registermodel model)
        //{


        //    try
        //    {

        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, model.openidprovider);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.openidprovider, model.username);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.screenname, model.emailaddress, model.openidprovider);

        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
        //        throw;
        //    }
        //    return null;

        //}

        //public EmailViewModel sendmemberpasswordchangedemail(LogonViewModel model)
        //{

        //    try
        //    {
        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangeMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangedAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.LogOnModel.ScreenName, model.LogOnModel.UserName, model.LogOnModel.Password, model.LogOnModel.ProfileID);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.LogOnModel.UserName, model.LostAccountInfoModel.Email);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log error mesasge
        //        new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
        //        throw;
        //    }
        //    return null;

        //}

        //public EmailViewModel sendmemberprofileactivatedemailbyprofileid(int profileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile model = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmemberactivationcoderecoveredemailbyprofileid(int profileid)
        //{

        //    try
        //    {
        //        //get the profile info
        //        profile model = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username, model.activationcode);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmemberemailmemssagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmemberpeekreceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmemberlikereceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{



        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}

        //public EmailViewModel sendmemberinterestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{



        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });

        //        EmailViewModel returnmodel = new EmailViewModel();

        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendmemberchatrequestreceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestAdminNotification);

        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return


        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}

        //public EmailViewModel sendmemberofflinechatmessagereceivedemailbyprofileid(int recipientprofileid, int senderprofileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = recipientprofileid });
        //        profile sender = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = senderprofileid });
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;


        //}

        //public EmailViewModel sendmemberphotorejectedemailbyprofileid(int profileid, int adminprofileid, photorejectionreasonEnum reason)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });
        //        profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
        //        //get rejecttion reason
        //        string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, reasondesc);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress, admin.username);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}

        //public EmailViewModel sendmemberphotouploadedemailbyprofileid(int profileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = profileid });
        //        // profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
        //        //get rejecttion reason
        //        // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}

        //public EmailViewModel sendadminspamblockedemailbyprofileid(int blockedprofileid)
        //{

        //    try
        //    {
        //        //get the profile info
        //        profile recipient = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockedprofileid });
        //        // profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
        //        //get rejecttion reason
        //        // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;

        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        //public EmailViewModel sendadminmemberspamblockedemailbyprofileid(int spamblockedprofileid, string reason, string blockedby)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile blocked = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = spamblockedprofileid });
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, blockedby);
        //        //Body
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.username, blocked.emailaddress, reason);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}

        //public EmailViewModel sendadminmemberblockedemailbyprofileid(int blockedprofileid, int blockerprofileid)
        //{


        //    try
        //    {
        //        //get the profile info
        //        profile blocked = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockedprofileid });
        //        profile blocker = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = blockerprofileid });

        //        //get the notes since the block was created first before email was sent
        //        var blockreason = _datingcontext.blocks.Where(p => p.blockprofile_id == blockedprofileid && p.id == blockedprofileid).FirstOrDefault();
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.emailaddress, blocker.emailaddress, blockreason.notes.FirstOrDefault());
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;
        //}

        ////There is no message for Members to know when photos are approved btw
        //public EmailViewModel sendadminmemberphotoapprovedemailbyprofileid(int approvedprofileid, int adminprofileid)
        //{

        //    try
        //    {
        //        //get the profile info
        //        profile profile = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = approvedprofileid });
        //        profile admin = _membersrepository.getprofilebyprofileid(new ProfileModel { profileid = adminprofileid });
        //        EmailViewModel returnmodel = new EmailViewModel();
        //        returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoApprovedAdminNotification);
        //        //fill in the rest of the email model values i.e format the subject and body
        //        //subject
        //        returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
        //        //Body
        //        returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, profile.username, profile.emailaddress, admin.screenname);
        //        //send the emails here i think trhough the service
        //        //once things are finally done there will he only a boolean return
        //        return returnmodel;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;


        //}


        //public EmailModel getemailbytemplateid(templateenum template)
        //{
        //    EmailModel emaildetail = new EmailModel();

        //    try
        //    {
        //        emaildetail.body = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().bodystring.description;
        //        emaildetail.subject = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().subjectstring.description;
        //        //TO DO figure out if we will populate other values here
        //        return emaildetail;
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle logging here
        //    }
        //    return null;

        //}





        #region "Private methods"
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
       private address getorcreateaddaddress(EmailViewModel Model, IUnitOfWorkAsync db)
       {
       
         //check to see if the recipeint address already exists if not add i
                        address current =
                        _unitOfWorkAsync.Repository<address>().Queryable().ToList()
                        .Where(p => p.emailaddress.ToUpper() == Model.userEmailViewModel.emailaddress && p.addresstype_id == Model.userEmailViewModel.addresstypeid).FirstOrDefault();
                            //_unitOfWorkAsync.Repository<address>().Queryable().ToList().Where(p => p.emailaddress.ToUpper() == Model.EmailModel.to && Model.EmailModel.addresstypefrom == addresstypeenum.SiteUser).FirstOrDefault();

                         
                        if (current == null)
                        {
                            current = new address();
                           // var addresstype = new lu_addresstype();
                         //   current.addresstype = addresstype;
                            //add the email address                         
                            current.addresstype_id = Model.userEmailViewModel.addresstypeid;//:  (int)Model.EmailModel.addresstypefrom;
                            current.username = Model.userEmailViewModel.username;
                            current.emailaddress = Model.userEmailViewModel.emailaddress;  //IsToAddress == true? Model.EmailModel.to:Model.EmailModel.from;
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
       
       
       private EmailModel getemailbytemplateid(templateenum template, IUnitOfWorkAsync db)
        {
            EmailModel emaildetail = new EmailModel();

            try
            {
                emaildetail.body = _unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(p => p.id == (int)template).FirstOrDefault().body.description;
                emaildetail.subject = _unitOfWorkAsync.Repository<lu_template>().Queryable().ToList().Where(p => p.id == (int)template).FirstOrDefault().subject.description;
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
