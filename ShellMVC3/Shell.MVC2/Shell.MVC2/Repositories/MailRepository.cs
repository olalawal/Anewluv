using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;

using RiaServicesContrib.Mvc;
using RiaServicesContrib.Mvc.Services;

namespace Shell.MVC2.Models
{
    //public class MailRepository
    //{

    //    private AnewLuvFTSEntities db = new AnewLuvFTSEntities(); 

    //        // Properties
    //    public int MailboxMessageID { get; set; }
    //    public string SenderID { get; set; }
    //    public string Body { get; set; }
    //    public string Subject { get; set; }
    //    public DateTime Age { get; set; }
    //      //  Sender = (screenname)
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public DateTime? CreationDate { get; set; }
    //    public string RecipientID { get; set; }
    //    public int? MailboxFolderID { get; set; }
    //    public int? MessageRead { get; set; }
    //    public int? MessageReplied { get; set; }
    //    public int Page { get; set; }
    //    public int dob { 
    //        get { 
    //            return (CalculateAge(Age));
    //        }
    //        set {
    //            ;
    //        }
    //    }
    //    public string SenderName
    //                            {
    //                                get
    //                                {
    //                                    return (from p in db.profiles
    //                                            where p.ProfileID == SenderID 
    //                                            select p.UserName).FirstOrDefault();
    //                                }
    //                                set
    //                                {
    //                                    ;
    //                                }

    //                            }
    //    public string RecipientName
    //    {
    //        get
    //        {
    //            return (from p in db.profiles
    //                    where p.ProfileID == RecipientID 
    //                    select p.UserName).FirstOrDefault();
    //        }
    //        set
    //        {
    //            ;
    //        }

    //    }



    //    public static int CalculateAge(DateTime BirthDate)
    //    {
    //        DateTime Now = DateTime.Today;

    //        int years = Now.Year - BirthDate.Year;

    //        if (Now.Month < BirthDate.Month || (Now.Month == BirthDate.Month && Now.Day < BirthDate.Day))
    //        {
    //            --years;
    //        }

    //        return years;
    //    }

    //    public string getUser(string User)
    //    {
            
    //        return ( from p in db.profiles
    //                 where p.UserName == User 
    //                  select p.ProfileID).FirstOrDefault();
    //    }

    //    public int MailCount(string MailType, string User)
    //    {
    //        return (from i in db.MailboxMessagesFolders
    //                     .Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
    //                    && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
    //                select i.MessageRead).Count();
    //    }
    //    public IEnumerable<MailRepository> GetAllMail( string MailType, string User)
    //    {
    //        return (from m in db.MailboxMessages
    //                join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
    //                    && u.MailboxFolder.MailboxFolderTypeName == MailType && u.MailboxFolder.ProfileID == User)
    //                on m.MailboxMessageID equals f.MailBoxMessageID
    //                select new MailRepository
    //                {

    //                    MailboxMessageID = m.MailboxMessageID,
    //                    SenderID = m.SenderID,
    //                    Body = m.Body,
    //                    Subject = m.Subject,


    //                    Age = f.MailboxFolder.ProfileData.Birthdate,
    //                    City = f.MailboxFolder.ProfileData.City,
    //                    State = f.MailboxFolder.ProfileData.State_Province,
    //                    CreationDate = m.CreationDate,
    //                    RecipientID = m.RecipientID,
    //                    MailboxFolderID = f.MailboxFolderID,
    //                    MessageRead = f.MessageRead,
    //                    MessageReplied = f.MessageReplied
                        
    //                }).ToList();
    //    }

    //    public IEnumerable<MailRepository> GetMailFld(int Mailfld, string User)
    //    {
    //        return (from m in db.MailboxMessages
    //                join f in db.MailboxMessagesFolders.Where(u => u.MailboxFolderID == u.MailboxFolder.MailboxFolderID
    //                    && u.MailboxFolder.MailboxFolderID == Mailfld && u.MailboxFolder.ProfileID == User)
    //                on m.MailboxMessageID equals f.MailBoxMessageID
    //                select new MailRepository
    //                {

    //                    MailboxMessageID = m.MailboxMessageID,
    //                    SenderID = m.SenderID,
    //                    Body = m.Body,
    //                    Subject = m.Subject,


    //                    Age = f.MailboxFolder.ProfileData.Birthdate,
    //                    City = f.MailboxFolder.ProfileData.City,
    //                    State = f.MailboxFolder.ProfileData.State_Province,
    //                    CreationDate = m.CreationDate,
    //                    RecipientID = m.RecipientID,
    //                    MailboxFolderID = f.MailboxFolderID,
    //                    MessageRead = f.MessageRead,
    //                    MessageReplied = f.MessageReplied

    //                }).ToList();
    //    } 
    //    //
    //    //Query Methods

    //  //  public IQueryable<MailRepository> InboxMails()
    //   // {
    //  //      return db.MailboxMessages.ToList;
    //   // }
         

    //    //Insert/Delete
    //  //  public void Add();
    //}
}