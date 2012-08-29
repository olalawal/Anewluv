using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

namespace Shell.MVC2.Models
{
    public class MailMessageRepository
    {
        private AnewLuvFTSEntities db = new AnewLuvFTSEntities(); 
        
        //
        // Query Methods

        public IQueryable<MailMessageModel> ReplyEmail1(int? id, int mailboxMsgFldId)
        {
            var allMail =
                  from m in db.MailboxMessages.Where(x => x.MailboxMessageID == id)
                    select new MailMessageModel
                    {
                        
                        MailboxMessageID = m.MailboxMessageID,
                        //MailboxMessagesFoldersID  = mailboxMsgFldId,
                        uniqueID = m.uniqueID,
                        SenderID = m.RecipientID,
                        Body = m.Body,
                        Subject = m.Subject,
                        CreationDate = m.CreationDate,
                        RecipientID =m.SenderID, // Sender and Recipient are flipped in a reply
                        SenderName = (from p in db.profiles where (p.ProfileID == m.RecipientID ) select p.ScreenName).FirstOrDefault(),
                        RecipientName = (from p in db.profiles where (p.ProfileID == m.SenderID) select p.ScreenName).FirstOrDefault()
                    };
            return allMail;
        }
// tobe deleted
        public IQueryable<MailMessageModel> ReplyEmail(int? id)
        {
            var allMail =
                  from m in db.MailboxMessages.Where(x => x.MailboxMessageID == id)
                  select new MailMessageModel
                  {

                      MailboxMessageID = m.MailboxMessageID,
                      //MailboxMessagesFoldersID = mailboxMsgFldId,
                      uniqueID = m.uniqueID,
                      SenderID = m.RecipientID,
                      Body = m.Body,
                      Subject = m.Subject,
                      CreationDate = m.CreationDate,
                      RecipientID = m.SenderID,
                      SenderName = (from p in db.profiles where (p.ProfileID == m.RecipientID) select p.ScreenName).FirstOrDefault(),
                      RecipientName = (from p in db.profiles where (p.ProfileID == m.SenderID) select p.ScreenName).FirstOrDefault()
                  };
            return allMail;
        }
        public int GetMailboxMessagesFoldersID(int mailboxMsgId ) {
            return (from p in db.MailboxMessagesFolders
                    where p.MailboxMessage.MailboxMessageID == mailboxMsgId
                    && p.MailboxFolder.MailboxFolderTypeName == "Inbox"
                    select p.MailboxMessagesFoldersID).FirstOrDefault();  
        }


        public int GetMailboxFolderID(string mailboxFolderTypeName, string profileId)
        {
            return (from i in db.MailboxFolders
                    where i.MailboxFolderTypeName == mailboxFolderTypeName && i.ProfileID == profileId
                    select i.MailboxFolderID).FirstOrDefault();
        }

        public MailboxMessagesFolder NewMailboxMessagesFolderObject(string mailboxFolderTypeName, string profileId)
        {
            int fldId = GetMailboxFolderID(mailboxFolderTypeName, profileId); // Retrieve specific mailboxfolderId by its mailboxFolderTypename and ProfileID

            //create mailbox folders if we have a null mailbox folders for a user
            if (fldId == 0)
            {
                using (DatingService context = new DatingService())
                {
                    context.CreateMailBoxFolders(profileId);
                    return NewMailboxMessagesFolderObject(mailboxFolderTypeName, profileId);
                }

            }



            
            MailboxMessagesFolder fld_MailboxMessagesFolder = new MailboxMessagesFolder() // Create a new MailboxMessagesFolder object
            {
                MailboxFolderID = fldId,
                MessageRead = 0,
                MessageReplied = 0,
                MessageFlagged = 0,
                MessageDeleted = 0,
                MessageDraft = 0,
                MessageRecent = 0
                };
            return fld_MailboxMessagesFolder;
        }


        //
        // Insert/Delete Methods

        public void Add(MailboxMessage mailboxmessage)
        {
            db.MailboxMessages.AddObject(mailboxmessage); //Updating the context
        }

        //
        // Persistence
        public void Save()
        {
            try
            {
                db.SaveChanges(); //Save to Database
            }
            catch { 
            
            
            }
        }
    }
}