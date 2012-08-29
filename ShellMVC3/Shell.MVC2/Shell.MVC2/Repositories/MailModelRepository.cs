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
    public class MailModelRepository
    {

        private AnewLuvFTSEntities db = new AnewLuvFTSEntities(); 

        //
        // Query Methods

        public string getUserID(string User)
        {
            
            return ( from p in db.profiles
                     where p.UserName == User 
                      select p.ProfileID).FirstOrDefault();
        }

        public int MailCount(string MailType, string User)
        {
            IEnumerable<MailModel> model = null; 

            //return (from i in db.MailboxMessagesFolders
            //             .Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
            //            && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
            //        select i.MessageRead).Count();

            //get a model of the messages that match this mail type
          model =  (from m in db.MailboxMessages
                    join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
                        && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
                    on m.MailboxMessageID equals f.MailBoxMessageID 
                    select new  MailModel 
                    {
                       
                        SenderID = m.SenderID,
                        RecipientID = m.RecipientID,

                        MailboxMessagesFoldersID = f.MailboxMessagesFoldersID,
                        MailboxFolderID = f.MailboxFolder.MailboxFolderID,                        
                        Status = (from p in db.profiles where p.ProfileID == m.SenderID select p.ProfileStatusID).FirstOrDefault(),
                        BlockStatus = (from p in db.Mailboxblocks where (p.ProfileID == User && p.BlockID == m.SenderID) select p.RecordID).FirstOrDefault(),
                        CreationDate = m.CreationDate,
                        SenderName = (from p in db.profiles where (p.ProfileID == m.SenderID) select p.UserName).FirstOrDefault(),
                        RecipientName = (from p in db.profiles where (p.ProfileID == m.RecipientID) select p.ScreenName).FirstOrDefault()

                    });

          //trim out the messages of banned members added 6/2/2011
          //updated to filter blacnk sender or recipient and inactive status's 
          var messages = from q in model.Where(p => (p.Status != 3 | p.Status != 4))
                          .Where(p => p.SenderName != null)
                          .Where(p => p.RecipientName != null)
                          .Where(p => p.BlockStatus == 0)
                         select q;
      



          return messages.Count();
      
        }

        public int NewMailCount(string MailType, string User)
        {
            IEnumerable<MailModel> model = null;

            //return (from i in db.MailboxMessagesFolders
            //             .Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
            //            && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
            //        select i.MessageRead).Count();

            //get a model of the messages that match this mail type
            model = (from m in db.MailboxMessages
                     join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
                         && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User && u.MessageRead == 0 )
                     on m.MailboxMessageID equals f.MailBoxMessageID
                     select new MailModel
                     {

                         SenderID = m.SenderID,
                         RecipientID = m.RecipientID,

                         MailboxMessagesFoldersID = f.MailboxMessagesFoldersID,
                         MailboxFolderID = f.MailboxFolder.MailboxFolderID,
                         Status = (from p in db.profiles where p.ProfileID == m.SenderID select p.ProfileStatusID).FirstOrDefault(),
                         BlockStatus = (from p in db.Mailboxblocks where (p.ProfileID == User && p.BlockID == m.SenderID) select p.RecordID).FirstOrDefault(),
                         CreationDate = m.CreationDate,
                         SenderName =  (from p in db.profiles where (p.ProfileID == m.SenderID) select p.UserName).FirstOrDefault(),
                         RecipientName = (from p in db.profiles where (p.ProfileID == m.RecipientID) select p.ScreenName).FirstOrDefault()

                     });

            //trim out the messages of banned members added 6/2/2011
            //updated to filter blacnk sender or recipient and inactive status's 
            var messages = from q in model.Where(p => (p.Status != 3 | p.Status != 4))
                            .Where(p => p.SenderName != null)
                            .Where(p => p.RecipientName != null)
                            .Where(p => p.BlockStatus == 0)
                           select q;

            return messages.Count();

        }

        public IEnumerable<MailModel> GetMailType(string MailType, string User)
        {

           IEnumerable<MailModel> model = null ;

         model =  (from m in db.MailboxMessages
                    join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
                        && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
                    on m.MailboxMessageID equals f.MailBoxMessageID
                    orderby m.CreationDate descending
                    select new MailModel
                    {
                      
                        uniqueID = m.uniqueID,
                        FolderName = f.MailboxFolder.MailboxFolderTypeName,
                        FolderProfileID = f.MailboxFolder.ProfileID,

                        MailboxMessageID = m.MailboxMessageID,

                        Body = m.Body,
                        Subject = m.Subject,

                        SenderID = m.SenderID,
                        RecipientID = m.RecipientID,

                        MailboxMessagesFoldersID = f.MailboxMessagesFoldersID,
                        MailboxFolderID = f.MailboxFolder.MailboxFolderID,
                        Age = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.Birthdate).FirstOrDefault(),
                        City = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.City).FirstOrDefault(),
                        State = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.State_Province).FirstOrDefault(),
                        Status = (from p in db.profiles  where p.ProfileID == m.SenderID select p.ProfileStatusID).FirstOrDefault(),
                        BlockStatus = (from p in db.Mailboxblocks   where( p.ProfileID == User && p.BlockID == m.SenderID)  select p.RecordID  ).FirstOrDefault(),
                        CreationDate = m.CreationDate,

                        MessageRead = f.MessageRead,
                        MessageReplied = f.MessageReplied,
                        SenderName =  (from p in db.profiles where( p.ProfileID == m.SenderID ) select p.ScreenName).FirstOrDefault(),          
                        RecipientName =  (from p in db.profiles  where (p.ProfileID == m.RecipientID)  select p.ScreenName).FirstOrDefault()

                    });

         //trim out the messages of banned members added 6/2/2011
         //updated to filter blacnk sender or recipient and inactive status's 
         var messages = from q in model.Where(p => (p.Status != 3 | p.Status != 4))
                         .Where(p => p.SenderName != null)
                         .Where(p => p.RecipientName != null)
                         .Where(p => p.BlockStatus == 0)
                        select q;
         
          return messages;

        }

        public IEnumerable<MailModel> GetAllMail(int Mailfld, string User)
        {

            IEnumerable<MailModel> model = null;

           model = (from m in db.MailboxMessages
                    join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
                        && u.MailboxFolder.MailboxFolderID == Mailfld && u.MailboxFolder.ProfileID == User)
                    on m.MailboxMessageID equals f.MailBoxMessageID
                    orderby m.CreationDate descending
                    select new MailModel
                    {
                         
                        FolderName = f.MailboxFolder.MailboxFolderTypeName,
                        MailboxMessageID = m.MailboxMessageID,
                        SenderID = m.SenderID,
                        Body = m.Body,
                        Subject = m.Subject,

                        MailboxMessagesFoldersID = f.MailboxMessagesFoldersID,
                        MailboxFolderID = f.MailboxFolder.MailboxFolderID,
                        Age = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.Birthdate).FirstOrDefault(),
                        City = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.City).FirstOrDefault(),
                        State = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.State_Province).FirstOrDefault(),
                        CreationDate = m.CreationDate,
                        RecipientID = m.RecipientID,
                        MessageRead = f.MessageRead,
                        MessageReplied = f.MessageReplied,

                        Status = (from p in db.profiles where p.ProfileID == m.SenderID select p.ProfileStatusID).FirstOrDefault(),
                        BlockStatus = (from p in db.Mailboxblocks where (p.ProfileID == User && p.BlockID == m.SenderID) select p.RecordID).FirstOrDefault(),
                        SenderName = (from p in db.profiles where (p.ProfileID == m.SenderID) select p.ScreenName).FirstOrDefault(),
                        RecipientName = (from p in db.profiles where (p.ProfileID == m.RecipientID) select p.ScreenName).FirstOrDefault()
                                                       

                    });

            //trim out the messages of banned members added 6/2/2011
            //updated to filter blacnk sender or recipient and inactive status's 
           var messages = from q in model.Where(p => (p.Status != 3 | p.Status != 4))
                           .Where(p => p.SenderName != null)
                           .Where(p => p.RecipientName != null)
                           .Where(p => p.BlockStatus == 0)
                          select q;

            return messages;
        }

        public IEnumerable<MailModel> GetMailMsgThreadByUserID(int uniqueId, string UserID)
        {

            IEnumerable<MailModel> model = null;

            model =  (from m in db.MailboxMessages.Where(x => x.uniqueID == uniqueId)
                    join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolder.MailboxFolderTypeName != "Deleted"
                    && u.MailboxFolder.ProfileID == UserID)
                    on m.MailboxMessageID equals f.MailBoxMessageID
                    orderby m.CreationDate ascending
                    select new MailModel
                    {
                         
                        uniqueID = m.uniqueID,
                        FolderName = f.MailboxFolder.MailboxFolderTypeName,
                        FolderProfileID = f.MailboxFolder.ProfileID,

                        MailboxMessageID = m.MailboxMessageID,
                        SenderID = m.SenderID,
                        Body = m.Body,
                        Subject = m.Subject,

                        MailboxMessagesFoldersID = f.MailboxMessagesFoldersID,
                        MailboxFolderID = f.MailboxFolder.MailboxFolderID,
                        Age = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.Birthdate).FirstOrDefault(),
                        City = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.City).FirstOrDefault(),
                        State = (from p in db.ProfileDatas where p.ProfileID == m.SenderID select p.State_Province).FirstOrDefault(),


                        CreationDate = m.CreationDate,
                        RecipientID = m.RecipientID,
                        MessageRead = f.MessageRead,
                        MessageReplied = f.MessageReplied,
                        
                        Status = (from p in db.profiles where p.ProfileID == m.SenderID select p.ProfileStatusID).FirstOrDefault(),
                        BlockStatus = (from p in db.Mailboxblocks where (p.ProfileID == UserID  && p.BlockID == m.SenderID) select p.RecordID).FirstOrDefault(),
                        SenderName = (from p in db.profiles where (p.ProfileID == m.SenderID) select p.ScreenName).FirstOrDefault(),
                        RecipientName = (from p in db.profiles where (p.ProfileID == m.RecipientID) select p.ScreenName).FirstOrDefault()

                    });

            //trim out the messages of banned members added 6/2/2011
            //updated to filter blacnk sender or recipient and inactive status's 
            var messages = from q in model.Where(p => (p.Status != 3 | p.Status != 4))
                            .Where(p => p.SenderName != null)
                            .Where(p => p.RecipientName != null)
                            .Where(p => p.BlockStatus == 0)
                           select q;

            return messages;

        }

 
    }
}