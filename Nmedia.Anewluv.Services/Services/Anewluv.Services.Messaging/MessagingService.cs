using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;
using System.Web;
using System.Net;

using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using System.ServiceModel.Activation;
using LoggingLibrary;

using Anewluv.Services.Contracts.ServiceResponse;

using System.IO;

//using Nmedia.DataAccess.Interfaces;
using System.Drawing;

using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;
using Anewluv.Domain;
using Repository.Pattern.UnitOfWork;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.Domain.Data.Notification;

using Nmedia.Infrastructure;

namespace Anewluv.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
    public class MessagingService : IMessagingService 
    {


        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MessagingService(IUnitOfWorkAsync unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

         
            _unitOfWorkAsync = unitOfWork;
         

        }



        public async Task<MailFoldersViewModel> getmailfolderdetails(MailModel model)
           {
               var task = Task.Factory.StartNew(() =>
               {
                   var repo = _unitOfWorkAsync.Repository<mailboxfolder>();
                   var dd = mailextentions.getmailfolderdetails(repo, model);

                   return dd;


               });
               return await task.ConfigureAwait(false);
           }
        public async Task<MailSearchResultsViewModel> getmailfilteredandpaged(MailModel model)
            {
                var task = Task.Factory.StartNew(() =>
                {
                    var repo = _unitOfWorkAsync.Repository<mailboxmessagefolder>();
                    var dd = mailextentions.getmailfilteredandpaged(repo, model,_unitOfWorkAsync);
                    return dd;


                });
                return await task.ConfigureAwait(false);
            }

        


        #region "update methods"

            public async Task<AnewluvMessages> updatemessage(MailModel model)
        {

            //do not audit on adds
            AnewluvMessages AnewluvMessages = new AnewluvMessages();
            //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
            {

                try
                {


                    var task = Task.Factory.StartNew(() =>
                    {

                        // var  model.photoformat = model. model.photoformat);
                        //var photoid = model.photoid;
                        //var convertedprofileid = Convert.ToInt32(model.profileid);
                        // var convertedstatus =  model.photostatus);

                        //first check the sent qotat for this user
                        var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = model.profileid });

                        //add daily read message quota as well
                        if (profile.dailsentmessagequota.Value > 3)
                        {
                            AnewluvMessages.errormessages.Add("Daily Message Quota Exceeded please upgrade your memembership to read more than 3 messages a day");
                            return AnewluvMessages;
                        }

                        // get the folderr details
                        mailboxfolder mailboxfolder = _unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(u => u.id == model.mailboxfolderid && u.profile_id == model.profileid).FirstOrDefault();
                        if (mailboxfolder != null)
                        {
                            //create the message and save it
                            var newmailboxmessage = new mailboxmessage
                            {
                                body = model.body,
                                subject = model.subject,
                                sizeinbtyes = model.body.Length + model.subject.Length,
                                recipient_id = model.recipeintprofileid.Value,
                                sender_id = model.profileid
                            };
                            _unitOfWorkAsync.Repository<mailboxmessage>().Insert(newmailboxmessage);

                            //create the message folder and save it 
                            var newmailboxmessagesfolder = new mailboxmessagefolder
                            {
                                mailboxmessage_id = newmailboxmessage.id,
                                mailboxfolder_id = mailboxfolder.id

                            };
                            _unitOfWorkAsync.Repository<mailboxmessagefolder>().Insert(newmailboxmessagesfolder);


                            // Update database
                            // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                            var i = _unitOfWorkAsync.SaveChanges();


                            //TO DO call the Notification Service to QUUE up this message. For now 
                            // All messages are sent on the fly but long term we will add it to the QUUE and then the sevice will send peroidoically.

                            //member 
                            var EmailViewModel = new EmailViewModel
                            {
                                userEmailViewModel = new EmailModel
                                {
                                    templateid = (int)templateenum.MemberRecivedEmailMessageMemberNotification,
                                    messagetypeid = (int)messagetypeenum.UserUpdate,
                                    addresstypeid = (int)addresstypeenum.SiteUser,
                                    emailaddress = newmailboxmessage.recipientprofilemetadata.profile.emailaddress,
                                    username = newmailboxmessage.recipientprofilemetadata.profile.username,
                                    subject = templatesubjectenum.MemberRecivedEmailMessageMemberNotification.ToDescription()
                                },
                                adminEmailViewModel = new EmailModel
                                {
                                    templateid = (int)templateenum.MemberRecivedEmailMessageAdminNotification,
                                    messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                    addresstypeid = (int)addresstypeenum.SystemAdmin,
                                    subject = templatesubjectenum.MemberRecivedEmailMessageAdminNotification.ToDescription()
                                }
                            };

                            //this sends both admin and user emails  
                            Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);


                            AnewluvMessages.messages.Add("Email was sent Succesfully");
                        }
                        else
                        {
                            AnewluvMessages.errormessages.Add("Invalid Mailbox folder or profile both values are required to delete messages");
                        }






                        return AnewluvMessages;
                    });
                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {
                    //TO DO track the transaction types only rollback on DB connections
                    //rollback transaction
                    // transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in Messaging Service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }

        }

            public async Task<AnewluvMessages> sendmessage(MailModel model)
            {
                
                    //do not audit on adds
                    AnewluvMessages AnewluvMessages = new AnewluvMessages();
                    //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                    {

                        try
                        {


                            var task = Task.Factory.StartNew(() =>
                            {
                                
                                    // var  model.photoformat = model. model.photoformat);
                                    //var photoid = model.photoid;
                                    //var convertedprofileid = Convert.ToInt32(model.profileid);
                                    // var convertedstatus =  model.photostatus);

                                   //first check the sent qotat for this user
                                    var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = model.profileid});
                                   
                                     if (profile.dailsentmessagequota.Value > 5)
                                     {
                                         AnewluvMessages.errormessages.Add("Daily Message Quota Exceeded please upgrade your memembership to send more than 5 messages a day");
                                         return AnewluvMessages;
                                     }

                                    // get the folderr details
                                     mailboxfolder mailboxfolder  = _unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(u => u.id == model.mailboxfolderid && u.profile_id == model.profileid).FirstOrDefault();
                                    if (mailboxfolder !=null)
                                    {
                                        //create the message and save it
                                        var newmailboxmessage = new mailboxmessage
                                        {
                                            body = model.body,
                                            subject = model.subject,
                                            sizeinbtyes = model.body.Length + model.subject.Length,
                                            recipient_id = model.recipeintprofileid.Value,
                                            sender_id = model.profileid
                                        };
                                       _unitOfWorkAsync.Repository<mailboxmessage>().Insert(newmailboxmessage);

                                        //create the message folder and save it 
                                       var newmailboxmessagesfolder = new mailboxmessagefolder
                                       {
                                           mailboxmessage_id = newmailboxmessage.id,
                                           mailboxfolder_id = mailboxfolder.id

                                       };
                                       _unitOfWorkAsync.Repository<mailboxmessagefolder>().Insert(newmailboxmessagesfolder);


                                       // Update database
                                       // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                                       var i = _unitOfWorkAsync.SaveChanges();
                                                                        

                                        //TO DO call the Notification Service to QUUE up this message. For now 
                                       // All messages are sent on the fly but long term we will add it to the QUUE and then the sevice will send peroidoically.

                                        //member 
                                       var EmailViewModel = new EmailViewModel
                                       {
                                           userEmailViewModel = new EmailModel
                                           {
                                               templateid = (int)templateenum.MemberRecivedEmailMessageMemberNotification,
                                               messagetypeid = (int)messagetypeenum.UserUpdate,                                           
                                               addresstypeid = (int)addresstypeenum.SiteUser,
                                               emailaddress = newmailboxmessage.recipientprofilemetadata.profile.emailaddress,
                                               username = newmailboxmessage.recipientprofilemetadata.profile.username,
                                               subject = templatesubjectenum.MemberRecivedEmailMessageMemberNotification.ToDescription()
                                           },
                                               adminEmailViewModel = new EmailModel {
                                               templateid = (int)templateenum.MemberRecivedEmailMessageAdminNotification,
                                               messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                               addresstypeid = (int)addresstypeenum.SystemAdmin,
                                               subject =templatesubjectenum.MemberRecivedEmailMessageAdminNotification.ToDescription()
                                           }
                                       };
                                      
                                       //this sends both admin and user emails  
                                       Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);

    
                                       AnewluvMessages.messages.Add("Email was sent Succesfully");
                                    }
                                    else
                                    {
                                        AnewluvMessages.errormessages.Add("Invalid Mailbox folder or profile both values are required to delete messages");
                                    }


                               
                                 

                                
                                return AnewluvMessages;
                            });
                            return await task.ConfigureAwait(false);


                        }
                        catch (Exception ex)
                        {
                            //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                            // transaction.Rollback();
                            //instantiate logger here so it does not break anything else.
                            logger = new Logging(applicationEnum.MediaService);
                            logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in Messaging Service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                        }

                    }
                
            }

            public async Task<AnewluvMessages> deletemessagesfromfolder(MailModel model)
            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();                   

                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {
                           
                            
                            foreach (int deletemailboxmessagesid in model.deletemailboxmessagesids)
                            {
                               
                                     // get the folderr details
                                     mailboxfolder mailboxfolder  = _unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(u => u.id == model.mailboxfolderid && u.profile_id == model.profileid).FirstOrDefault();
                                     mailboxmessage mailboxmessage = _unitOfWorkAsync.Repository<mailboxmessage>().Queryable().Where(u => u.id == deletemailboxmessagesid).FirstOrDefault();
                                    if (mailboxfolder !=null && mailboxmessage !=null)
                                    {

                                        //find the mailboxfoldermessages and update it to deleted
                                        var mailboxmessagefolder = _unitOfWorkAsync.Repository<mailboxmessagefolder>().Query(p => p.mailboxfolder_id == mailboxfolder.id && p.mailboxmessage_id == mailboxmessage.id).Select().FirstOrDefault();

                                        if (mailboxmessagefolder !=null)
                                        {
                                            mailboxmessagefolder.deleted = true;
                                            mailboxmessagefolder.deleteddate = DateTime.Now;
                                            _unitOfWorkAsync.Repository<mailboxmessagefolder>().Update(mailboxmessagefolder);
                                            var i = _unitOfWorkAsync.SaveChanges();
                                            AnewluvMessages.messages.Add("Message deleted Succesfully");
                                        }
                                     
                                    }
                                    else
                                    {
                                        AnewluvMessages.errormessages.Add("Invalid Mailbox folder or profile both values are required to delete messages");
                                    }


                            }


                            return AnewluvMessages;


                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                        //TO DO track the transaction types only rollback on DB connections
                        //rollback transaction
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in Messaging Service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                
                   
            }

            public async Task<AnewluvMessages> movemessages(MailModel model)
            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();

                try
                {


                    var task = Task.Factory.StartNew(() =>
                    {


                        foreach (int movemailboxmessagesid in model.movemailboxmessagesids)
                        {

                            // get the folderr details
                            mailboxfolder mailboxfolder = _unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(u => u.id == model.mailboxfolderid && u.profile_id == model.profileid).FirstOrDefault();
                            mailboxmessage mailboxmessage = _unitOfWorkAsync.Repository<mailboxmessage>().Queryable().Where(u => u.id == movemailboxmessagesid).FirstOrDefault();
                            if (mailboxfolder != null && mailboxmessage != null && model.destinationmailboxfolderid !=null)
                            {

                                //find the mailboxfoldermessages and update it to deleted
                                var mailboxmessagefolder = _unitOfWorkAsync.Repository<mailboxmessagefolder>().Query(p => p.mailboxfolder_id == mailboxfolder.id && p.mailboxmessage_id == mailboxmessage.id).Select().FirstOrDefault();

                                if (mailboxmessagefolder != null)
                                {
                                    mailboxmessagefolder.moved = true;
                                    mailboxmessagefolder.movedate = DateTime.Now;
                                    mailboxmessagefolder.mailboxfolder_id = model.destinationmailboxfolderid.GetValueOrDefault();
                                    _unitOfWorkAsync.Repository<mailboxmessagefolder>().Update(mailboxmessagefolder);
                                    var i = _unitOfWorkAsync.SaveChanges();
                                    AnewluvMessages.messages.Add("Message moved Succesfully");
                                }

                            }
                            else
                            {
                                AnewluvMessages.errormessages.Add("Invalid Mailbox folder or profile both values are required to move messages, destination folder id is also required");
                            }


                        }


                        return AnewluvMessages;


                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {
                    //TO DO track the transaction types only rollback on DB connections
                    //rollback transaction
                    // transaction.Rollback();
                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MediaService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in Messaging Service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            public async Task<AnewluvMessages> addmailfoxfolder(MailModel model)
            {
                //do not audit on adds
                AnewluvMessages AnewluvMessages = new AnewluvMessages();
                //   using (var transaction = _unitOfWorkAsync.BeginTransaction())
                {

                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {

                            // var  model.photoformat = model. model.photoformat);
                            //var photoid = model.photoid;
                            //var convertedprofileid = Convert.ToInt32(model.profileid);
                            // var convertedstatus =  model.photostatus);

                            //first check the sent qotat for this user
                            var profile = _unitOfWorkAsync.Repository<profile>().getprofilebyprofileid(new ProfileModel { profileid = model.profileid });

                            if (profile.dailsentmessagequota.Value > 5)
                            {
                                AnewluvMessages.errormessages.Add("Daily Message Quota Exceeded please upgrade your memembership to send more than 5 messages a day");
                                return AnewluvMessages;
                            }

                            // get the folderr details
                            mailboxfolder mailboxfolder = _unitOfWorkAsync.Repository<mailboxfolder>().Queryable().Where(u => u.id == model.mailboxfolderid && u.profile_id == model.profileid).FirstOrDefault();
                            if (mailboxfolder != null)
                            {
                                //create the message and save it
                                var newmailboxmessage = new mailboxmessage
                                {
                                    body = model.body,
                                    subject = model.subject,
                                    sizeinbtyes = model.body.Length + model.subject.Length,
                                    recipient_id = model.recipeintprofileid.Value,
                                    sender_id = model.profileid
                                };
                                _unitOfWorkAsync.Repository<mailboxmessage>().Insert(newmailboxmessage);

                                //create the message folder and save it 
                                var newmailboxmessagesfolder = new mailboxmessagefolder
                                {
                                    mailboxmessage_id = newmailboxmessage.id,
                                    mailboxfolder_id = mailboxfolder.id

                                };
                                _unitOfWorkAsync.Repository<mailboxmessagefolder>().Insert(newmailboxmessagesfolder);

                                //TO DO call the Notification Service to QUUE up this message. For now 
                                // All messages are sent on the fly but long term we will add it to the QUUE and then the sevice will send peroidoically.

                                //member 
                                var EmailViewModel = new EmailViewModel
                                {
                                    userEmailViewModel = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberRecivedEmailMessageMemberNotification,
                                        messagetypeid = (int)messagetypeenum.UserUpdate,
                                        addresstypeid = (int)addresstypeenum.SiteUser,
                                        emailaddress = newmailboxmessage.recipientprofilemetadata.profile.emailaddress,
                                        username = newmailboxmessage.recipientprofilemetadata.profile.username,
                                        subject = templatesubjectenum.MemberRecivedEmailMessageMemberNotification.ToDescription()
                                    },
                                    adminEmailViewModel = new EmailModel
                                    {
                                        templateid = (int)templateenum.MemberRecivedEmailMessageAdminNotification,
                                        messagetypeid = (int)messagetypeenum.SysAdminUpdate,
                                        addresstypeid = (int)addresstypeenum.SystemAdmin,
                                        subject = templatesubjectenum.MemberRecivedEmailMessageAdminNotification.ToDescription()
                                    }
                                };

                                //this sends both admin and user emails  
                                Api.AsyncCalls.sendmessagebytemplate(EmailViewModel);


                                AnewluvMessages.messages.Add("Email was sent Succesfully");
                            }



                            // Update database
                            // _datingcontext.ObjectStateManager.ChangeObjectState(PhotoModify, EntityState.Modified);
                            var i = _unitOfWorkAsync.SaveChanges();
                            // transaction.Commit();
                            AnewluvMessages.messages.Add("Email was sent Succesfully");



                            return AnewluvMessages;
                        });
                        return await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                        //TO DO track the transaction types only rollback on DB connections
                        //rollback transaction
                        // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MediaService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in Messaging Service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }

            }

        #endregion









    }


}

