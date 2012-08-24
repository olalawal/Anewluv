using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;


//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;
using Shell.MVC2.Models;
using MvcContrib.Pagination;

using Shell.MVC2.AppFabric;

using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;
using Shell.MVC2.Models.Chat;



using Omu.Awesome.Core;
using Shell.MVC2.Filters;
using Shell.MVC2.Infrastructure;
//_mailrepository.GetMailType(MailType, UserID).Where(p=>p.SenderName != null & p.RecipientName !=null);
namespace Shell.MVC2.Controllers
{
    public partial class MailController : Controller
    {
        private AnewLuvFTSEntities _db;
        private DatingService _context;
        private MailMessageRepository _mailmessagerepositiory; 
        private MailModelRepository _mailrepository;  
        private MembersRepository _membersrepository;        
        private PaginatedList<MailModel> _pageMail = new PaginatedList<MailModel>();
        
        //ninject contructor
        public MailController(MembersRepository membersrepositiory,AnewLuvFTSEntities db ,DatingService context,
                             MailModelRepository mailmodelrepository, MailMessageRepository mailmessagerepositiory)
        {
          _db = db;     
         _membersrepository = membersrepositiory;
         _mailrepository = mailmodelrepository;
         _mailmessagerepositiory = mailmessagerepositiory;
         _context = context;
                     
        }

        //
        // GET: Mail/MailHome
        [Authorize ]
        [GetChatUsersData]
        public virtual ActionResult MailHome(int? page)
        {
            
            string MailType = "Inbox";
            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            ViewData["inbox"] = _mailrepository.MailCount("Inbox", UserID);
            ViewData["sent"] = _mailrepository.MailCount("Sent", UserID);
            ViewData["trash"] = _mailrepository.MailCount("Trash", UserID);

            //filter out blocks and banns as well as bad data i,e deleted profiles with emails stil out there
            var getMails = _mailrepository.GetMailType(MailType, UserID);

            const int pageSize = 7;

            var paginatedMails = _pageMail.GetPageable(getMails, page ?? 0, pageSize);
            return View(paginatedMails);

        }

        // GET: Mail/Inbox
        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult Inbox(int? page)
        {

            string MailType = "Inbox";
         
            string User = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            ViewData["inbox"] = _mailrepository.MailCount("Inbox", User);
            ViewData["sent"] = _mailrepository.MailCount("Sent", User);
            ViewData["trash"] = _mailrepository.MailCount("Trash", User);


            // var getMails = _repository. GetMailType(MailType, User);
            var getMails = _mailrepository.GetMailType(MailType, User);
            const int pageSize = 7;

            var paginatedMails = _pageMail.GetPageable(getMails, page ?? 0, pageSize);
            return View(paginatedMails);


        }

        // GET: Mail/Sent
        [HttpGet]
        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult Sent(int? page)
        {
            string MailType = "Sent";
            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            ViewData["inbox"] = _mailrepository.MailCount("Inbox", UserID);
            ViewData["sent"] = _mailrepository.MailCount("Sent", UserID);
            ViewData["trash"] = _mailrepository.MailCount("Trash", UserID);


           // var getMails = _mailrepository.GetMailType(MailType, UserID);
            var getMails = _mailrepository.GetMailType(MailType, UserID);

            const int pageSize = 7;

            var paginatedMails = _pageMail.GetPageable(getMails, page ?? 0, pageSize);
            return View(paginatedMails);

        }


        // GET: Mail/Trash
        [HttpGet]
        [Authorize]
        public virtual ActionResult Trash(int? page)
        {
            string MailType = "Trash";
            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            ViewData["inbox"] = _mailrepository.MailCount("Inbox", UserID);
            ViewData["sent"] = _mailrepository.MailCount("Sent", UserID);
            ViewData["trash"] = _mailrepository.MailCount("Trash", UserID);


            //var getMails = _mailrepository.GetMailType(MailType, UserID);
            var getMails = _mailrepository.GetMailType(MailType, UserID);

            const int pageSize = 7;

            var paginatedMails = _pageMail.GetPageable(getMails, page ?? 0, pageSize);
            return View(paginatedMails);

        }
           [Authorize]
        public virtual ActionResult NewReadMsg3(int? page, int MailboxFolderID, int MailboxMessagesFoldersID)
        {

            //Update msgread
            MailboxMessagesFolder fld = _db.MailboxMessagesFolders.First(f => f.MailboxMessagesFoldersID == MailboxMessagesFoldersID);
            fld.MessageRead = 1; //update dinner properties             
            _db.SaveChanges(); //persist changes

            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);


            var getMail = _mailrepository.GetAllMail(MailboxFolderID, UserID);

            const int pageSize = 1;

            var model = _pageMail.GetPageable(getMail, page ?? 1, pageSize); //paginatedMails 

            //return View(paginatedMails); //works

            // return PartialView("NewReadMsgForm",model);
            //  return PartialView(model); //works
            if (Request.IsAjaxRequest())
            {
                //return View("NewReadMsgForm", model);
                return PartialView("NewReadMsgForm", model);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        [GetChatUsersData]
        [AkismetCheck(ParameterName = "MailMessageModel")]      
        public virtual ActionResult NewReadMsg(int? page, int MailboxFolderID, int MailboxMessagesFoldersID)
        {

            //Update msgread
            MailboxMessagesFolder fld = _db.MailboxMessagesFolders.First(f => f.MailboxMessagesFoldersID == MailboxMessagesFoldersID);
            fld.MessageRead = 1; //update dinner properties             
            _db.SaveChanges(); //persist changes

            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            var getMail = _mailrepository.GetAllMail(MailboxFolderID, UserID);

            const int pageSize = 1;

            var model = _pageMail.GetPageable(getMail, page ?? 1, pageSize); //paginatedMails 

            //return View(paginatedMails); //works

            // return PartialView("NewReadMsgForm",model);
            //  return PartialView(model); //works
            //if (Request.IsAjaxRequest())
            //{
                return View(model);
            //}
            //else
            //{
            //    return View(model);
            //}
        }
        [Authorize]
        [GetChatUsersData]
        public virtual ActionResult NewReadMsgThread(int? page, int mailboxMessageID)
        {

            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            ViewData["inbox"] = _mailrepository.MailCount("Inbox", UserID);
            ViewData["sent"] = _mailrepository.MailCount("Sent", UserID);
            ViewData["trash"] = _mailrepository.MailCount("Trash", UserID);

            int uniqueId = (from u in _db.MailboxMessages.Where(p => p.MailboxMessageID == mailboxMessageID) select u.uniqueID).FirstOrDefault().Value;

                var getMail = (from m in _db.MailboxMessages
                               where m.uniqueID == uniqueId
                               select m);

                var Thread = _mailrepository.GetMailMsgThreadByUserID(uniqueId, UserID);
                return View(Thread.ToList());

            //return View(getMail.ToList()); //works with ienumrable header instead of ipagation
        }

        //[HttpPost]
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult DeleteMail(int id, int? PageIndex, int fldId)
        {
            //Delete from inbox/sent should move the item to trash and 
            //delete from trash should move it to perm deleted
            //Delete mail. Move to trash or delete folder.



            //Get folder type 
            string folderType = _db.MailboxMessagesFolders
                .Single(item => item.MailboxMessagesFoldersID == id).MailboxFolder.MailboxFolderTypeName;

            // Get profile id
            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            //Get the mail
            MailboxMessagesFolder mailItem = (from m in _db.MailboxMessagesFolders
                                              where m.MailboxMessagesFoldersID == id
                                              select m).Single();

            if (folderType != "Deleted")
            {
                if (folderType != "Trash")
                {
                    //Current folder is Sent, Inbox or etc.
                    //Get Trash folder
                    int Trashfld = _db.MailboxFolders
                        .Single(t => t.ProfileID == UserID
                            && t.MailboxFolderTypeName == "Trash").MailboxFolderID;

                    //Update folder to the Trash id
                    mailItem.MailboxFolderID = Trashfld;
                }
                else
                {   // Current folder is Trash
                    // Get Deleted folder
                    int Deletedfld = _db.MailboxFolders
                        .Single(t => t.ProfileID == UserID
                            && t.MailboxFolderTypeName == "Deleted").MailboxFolderID;

                    // Update folder to the Trash id
                    mailItem.MailboxFolderID = Deletedfld;

                }
                _db.SaveChanges(); //persist changes
            }


            //Update msgdeleted flag
            MailboxMessagesFolder fld = _db.MailboxMessagesFolders.First(f => f.MailboxMessagesFoldersID == id);
            fld.MessageDeleted = 1; //update dinner properties             
            _db.SaveChanges(); //persist changes

     

            var getMail = _mailrepository.GetAllMail(fldId, UserID);

            const int pageSize = 1;

            var model = _pageMail.GetPageable(getMail, PageIndex ?? 1, pageSize);

            return PartialView("NewReadMsgForm", model);


        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult DeleteThreadMail(int id)
        {
            //Delete from inbox/sent should move the item to trash and 
            //delete from trash should move it to perm deleted
            //Delete mail. Move to trash or delete folder.



            //Get folder type 
            string folderType = _db.MailboxMessagesFolders
                .Single(item => item.MailboxMessagesFoldersID == id).MailboxFolder.MailboxFolderTypeName;

            // Get profile id
            string UserID = _mailrepository.getUserID(HttpContext.User.Identity.Name);

            //Get the mail
            MailboxMessagesFolder mailItem = (from m in _db.MailboxMessagesFolders
                                              where m.MailboxMessagesFoldersID == id
                                              select m).Single();

            if (folderType != "Deleted")
            {
                if (folderType != "Trash")
                {
                    //Current folder is Sent, Inbox or etc.
                    //Get Trash folder
                    int Trashfld = _db.MailboxFolders
                        .Single(t => t.ProfileID == UserID
                            && t.MailboxFolderTypeName == "Trash").MailboxFolderID;

                    //Update folder to the Trash id
                    mailItem.MailboxFolderID = Trashfld;
                }
                else
                {   // Current folder is Trash
                    // Get Deleted folder
                    int Deletedfld = _db.MailboxFolders
                        .Single(t => t.ProfileID == UserID
                            && t.MailboxFolderTypeName == "Deleted").MailboxFolderID;

                    // Update folder to the Trash id
                    mailItem.MailboxFolderID = Deletedfld;

                }
                _db.SaveChanges(); //persist changes
            }


            //Update msgdeleted flag
            MailboxMessagesFolder fld = _db.MailboxMessagesFolders.First(f => f.MailboxMessagesFoldersID == id);
            fld.MessageDeleted = 1; //update dinner properties             
            _db.SaveChanges(); //persist changes

            return Content(Boolean.TrueString); //sucess


        }

        // GET: Mail/Reply
        [Authorize]
        [HttpGet]
        public virtual ActionResult NewReply(int id, int mailboxMsgFldId)
        {




            //the model has been converted to use screen names not usernames and profileIDs
            MailMessageModel m = new MailMessageModel();
            var mail = _mailmessagerepositiory.ReplyEmail(id).SingleOrDefault();
            m.uniqueID = mail.uniqueID; // value for mail thread
            m.RecipientName = mail.RecipientName;
            m.RecipientID = mail.RecipientID;
            m.Subject = mail.Subject;
            m.SenderID = mail.SenderID;
            m.SenderName = mail.SenderName;


            return View(m);
        }

        //
        // POST: Mail/Reply
        [HttpPost]
        [Authorize]
        [AkismetCheck(ParameterName = "mailToCreate")]  
        public virtual ActionResult NewReply(int id, int mailboxMsgFldId, MailMessageModel mailToCreate)
        {

            var Email = new EmailModel();
            try {

                    if (_context.CheckIfQuoutaReachedAndUpdate(mailToCreate.SenderID) == true)
                    {
                        ModelState.AddModelError("", "You have reached your daily email limit, Free members are limited to 15 emails a day, please contact support");
                       // return View(mailToCreate);
                    }
                   // AnewLuvMembershipProvider membership = new AnewLuvMembershipProvider();
                   // if (membership.ValidateUser(mailToCreate.SenderID) == false)
                   //      ModelState.AddModelError("", "Unable to Reply to this message");
             }
            catch 
            {
                 ModelState.AddModelError("", "Unable to Reply to this message");
            }            
            //build the email packege as well as validate the user

            if (ModelState.IsValid)
            {
                //build the email model here
               Email.ScreenName = mailToCreate.RecipientName;
                Email.ProfileID = mailToCreate.RecipientID;
                Email.SenderProfileID = mailToCreate.SenderID;
               Email.UserName = _context.GetUserNamebyProfileID(mailToCreate.RecipientID);


                // update mail has being read
                MailboxMessagesFolder fld = _db.MailboxMessagesFolders.First(f => f.MailboxMessagesFoldersID == mailboxMsgFldId);
                fld.MessageReplied = 1; //update dinner properties            
                _db.SaveChanges(); //persist changes

                // Create a new MailboxMessage object to populate MailViewModel values
                MailboxMessage mailbox = new MailboxMessage();
                mailbox.CreationDate = DateTime.Now;
                mailbox.RecipientID = mailToCreate.RecipientID;
                mailbox.Subject = mailToCreate.Subject;
                mailbox.Body = mailToCreate.Body;
                mailbox.SenderID = mailToCreate.SenderID;
                mailbox.uniqueID = mailToCreate.uniqueID;

                // Create a new MailboxMessagesFolder object by its mailboxFolderTypename and ProfileID
                MailboxMessagesFolder sent_MailboxMessagesFolder = _mailmessagerepositiory.NewMailboxMessagesFolderObject("Sent", mailToCreate.SenderID);
                MailboxMessagesFolder inbox_MailboxMessagesFolder = _mailmessagerepositiory.NewMailboxMessagesFolderObject("Inbox", mailToCreate.RecipientID);

                //Add MailboxMessagesFolder to the mailbox(MailboxMessage)'s MailboxMessagesFolder Collection
                mailbox.MailboxMessagesFolders.Add(sent_MailboxMessagesFolder);
                mailbox.MailboxMessagesFolders.Add(inbox_MailboxMessagesFolder);

                //send the email to the recipient here
                //send the mails out 
                
             
              
                //declae a new instance of LocalEmailService
                var localEmailService = new LocalEmailService();
                Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileReceivedMailMessage);
                localEmailService.SendEmailMessage(Email);

              
             


                _mailmessagerepositiory.Add(mailbox);
                _mailmessagerepositiory.Save();



                return RedirectToAction("MailHome");
            }

            return View(mailToCreate);
            // return RedirectToAction("NewReadMsg", new {MailboxFolderID =4, MailboxMessagesFoldersID=12}); 
        }

        //
        // GET: Mail/Send

       
        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public virtual ActionResult SendMsg(string ScreenName)
        {
                        

            string RecipientID = "";
           // Create Message template
           MailMessageModel model = new MailMessageModel();
          //initialize the dating service here
          // Get profileId's for sender 
          //use datingservice
          // var SenderID = _mailrepository.getUserID(User.Identity.Name);
          //convert sender name to sender ID 
          //  var SenderID = datingservicecontext.GetProfileIdbyUsername(User.Identity.Name);
          // get sender's info from session
           MembersViewModel _model = new MembersViewModel();
           MembersRepository membersrepository = new MembersRepository();
           //gets most current copy , or populates the sub models in the viewmodel

           
           _model = CachingFactory.MembersViewModelHelper.GetMemberData ( CachingFactory.GetProfileIDByUserName(this.HttpContext.User.Identity.Name));

            //Validation for members who are not activated or active
           if (_model.Profile.ProfileStatusID != 2)
           {
               ModelState.AddModelError("", "Your profile status does not allow you you send messages!, please contact support");
               return View(model);
           }

            using (_context)
            {      
               //make sure sender has not gone over message qouta
                //get profileID
                RecipientID = _context.getprofileidbyscreenname(ScreenName);  
                model.RecipientID = RecipientID;
                model.RecipientName = ScreenName;
                model.SenderID = _model.Profile.ProfileID;
                model.SenderName = _model.Profile.ScreenName;
            }

            

            return View(model);


        }

        //
        // POST: Mail/Send

        //[AcceptVerbs(HttpVerbs.Post), Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        [AkismetCheck(ParameterName = "mailToCreate")]   
        public virtual ActionResult SendMsg(MailMessageModel mailToCreate)
        {
            MailboxMessage mailbox = new MailboxMessage();
            var Email = new EmailModel();
         
            try
            {
                //Create a new MailboxMessage object to populate MailViewModel values            
                mailbox.CreationDate = DateTime.Now;
                mailbox.RecipientID = mailToCreate.RecipientID;
                mailbox.Subject = mailToCreate.Subject;
                mailbox.Body = mailToCreate.Body;
                mailbox.SenderID = mailToCreate.SenderID;
                mailbox.uniqueID = mailbox.MailboxMessageID;
                
                //build the email packege as well as validate the user
                    if (_context.CheckIfQuoutaReachedAndUpdate(mailToCreate.SenderID) == true)
                    {
                        ModelState.AddModelError("", "You have reached your daily email limit, Free members are limited to 5 emails a day, please contact support");
                     //   return View(mailToCreate);
                    }
                  
                    //AnewLuvMembershipProvider membership = new AnewLuvMembershipProvider();
                    //if( membership.ValidateUser(Email.UserName) == false ) //make sure i
                    // ModelState.AddModelError("", "Unable to send this message");
               }            
            catch
            {
                ModelState.AddModelError("", "Unable to send this message, please try again later");
            }


            if (ModelState.IsValid)
            {

                //build the email model
                Email.ScreenName = mailToCreate.RecipientName;
                Email.ProfileID = mailToCreate.RecipientID;
                Email.SenderProfileID = mailToCreate.SenderID;
                Email.UserName = _context.GetUserNamebyProfileID(mailToCreate.RecipientID);               

                // Create a new MailboxMessagesFolder object by its mailboxFolderTypename and ProfileID                
                MailboxMessagesFolder sent_MailboxMessagesFolder = _mailmessagerepositiory.NewMailboxMessagesFolderObject("Sent", mailToCreate.SenderID);
                MailboxMessagesFolder inbox_MailboxMessagesFolder = _mailmessagerepositiory.NewMailboxMessagesFolderObject("Inbox", mailToCreate.RecipientID);

                //Add MailboxMessagesFolder to the mailbox(MailboxMessage)'s MailboxMessagesFolder Collection
                mailbox.MailboxMessagesFolders.Add(sent_MailboxMessagesFolder);
                mailbox.MailboxMessagesFolders.Add(inbox_MailboxMessagesFolder);

                //Add and Persist changes to mail
                _mailmessagerepositiory.Add(mailbox);
                _mailmessagerepositiory.Save();
                                
                // Update ***** Unique Id =MailboxMessageID for msg Threading and Persists changes to mail
                int id = mailbox.MailboxMessageID;  // id contains the newly inserted object's id
                mailbox.uniqueID = id;
                _mailmessagerepositiory.Save();

                 //send the email out                     
                 //declae a new instance of LocalEmailService
                 var localEmailService = new LocalEmailService();
                 Email = LocalEmailService.CreateEmails(Email, EMailtype.ProfileReceivedMailMessage);
                 localEmailService.SendEmailMessage(Email);
              



                return RedirectToAction("MailHome");
            }

            return View(mailToCreate);
            // return RedirectToAction("NewReadMsg", new {MailboxFolderID =4, MailboxMessagesFoldersID=12}); 
        }


    }
}
