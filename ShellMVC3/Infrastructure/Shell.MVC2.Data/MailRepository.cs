using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Data
{
    public class MailRepository : MemberRepositoryBase, IMailRepository
    {



        private AnewluvContext db; // = new AnewluvContext();
        private IMemberRepository membersrepository;
        //private  PostalData2Entities postaldb; //= new PostalData2Entities();




        public MailRepository(AnewluvContext datingcontext, IMemberRepository _membersrepository)
            : base(datingcontext)
        {
            membersrepository = _membersrepository;
        }
        // Query Methods
        public IQueryable<mailmessagemodel> ReplyEmail1(int? id, int mailboxMsgFldId)
        {
            var allMail =
                  from m in db.mailboxmessages.Where(x => x.id == id)
                    select new mailmessagemodel  
                    {
                        
                         mailboxmessage_id  = m.id,
                        //mailboxmessagefoldersID  = mailboxMsgFldId,
                         uniqueid = m.uniqueid,
                        sender_id = m.recipient_id ,
                        recipient_id = m.sender_id ,
                        body = m.body,
                        subject = m.subject,
                         creationdate  = m.creationdate,
                        // Sender and Recipient are flipped in a reply
                        senderscreenname  = (from p in db.profiles where (p.id  == m.recipient_id ) select p.screenname ).FirstOrDefault(),
                        recipientscreenname  = (from p in db.profiles where (p.id  == m.sender_id ) select p.screenname ).FirstOrDefault()
                    };
            return allMail;
        }
// tobe deleted
        public IQueryable<mailmessagemodel> ReplyEmail(int? id)
        {
            var allMail =
                  from m in db.mailboxmessages.Where(x => x.id  == id)
                  select new mailmessagemodel
                  {

                       mailboxmessage_id  = m.id ,
                      //mailboxmessagefoldersID = mailboxMsgFldId,
                      uniqueid = m.uniqueid,
                      sender_id = m.recipient_id,
                      body = m.body,
                      subject = m.subject,
                      creationdate = m.creationdate,
                      recipient_id = m.sender_id,
                      senderscreenname  = (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname).FirstOrDefault(),
                      recipientscreenname = (from p in db.profiles where (p.id == m.sender_id ) select p.screenname).FirstOrDefault()
                  };
            return allMail;
        }
        public int GetmailboxmessagefoldersID(int mailboxMsgId ) {
            return (from p in db.mailboxmessagefolders
                    where p.mailboxmessage.id  == mailboxMsgId
                    && p.mailboxfolder.foldertype.name == "Inbox"
                    select p.id).FirstOrDefault();  
        }

        public int GetmailboxfolderID(string mailboxFolderTypeName, string profileId)
        {
            return (from i in db.mailboxfolders
                    where i.foldertype.name == mailboxFolderTypeName && i.profiled_id == profileId
                    select i.id).FirstOrDefault();
        }

        public mailboxmessagefolder NewmailboxmessagefolderObject(string mailboxFolderTypeName, string profileId)
        {
            int fldId = GetmailboxfolderID(mailboxFolderTypeName, profileId); // Retrieve specific mailboxfolderId by its mailboxFolderTypename and ProfileID

            //create mailbox folders if we have a null mailbox folders for a user
            if (fldId == 0)
            {
                   //TO do move this to a mail repop
                   membersrepository.createmailboxfolders (profileId);
                   return NewmailboxmessagefolderObject(mailboxFolderTypeName, profileId);
                

            }



            
            mailboxmessagefolder fld_mailboxmessagefolder = new mailboxmessagefolder() // Create a new mailboxmessagefolder object
            {
                id = fldId,
                 readdate  = null,
                 replieddate  = null,
                 flaggeddate  = null,
                 deleteddate  = null,
                draftdate   =  null,
                recent   = false
                };
            return fld_mailboxmessagefolder;
        }

        public void Add(mailboxmessage mailboxmessage)
        {
            db.mailboxmessages.Add (mailboxmessage); //Updating the context
        }

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


        //
        // Query Methods

        public string getUserID(string User)
        {

            return (from p in db.profiles
                    where p.username  == User
                    select p.id).FirstOrDefault();
        }

        public int getAllMailCountbyfolderid(int folderid, string profile_id)
        {
            IEnumerable<mailmodel > models = null;

            //return (from i in db.mailboxmessagefolders
            //             .Where(u => u.mailboxfolderID == u.mailboxfolder.mailboxfolderID
            //            && u.mailboxfolder.foldertype.name == MailType && u.mailboxfolder.ProfileID == User)
            //        select i.MessageRead).Count();


            //join f in _datingcontext.profiledatas on p.blockprofile_id  equals f.id 
            //get a model of the messages that match this mail type

            //get a model of the messages that match this mail type
            models = (from m in db.mailboxmessages
                     join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id  == u.mailboxfolder.id
                         && u.mailboxfolder.profiled_id == profile_id )
                     on m.id  equals f.mailboxmessage_id
                       select new mailmodel 
                     {

                         sender_id = m.sender_id,
                         recipient_id = m.recipient_id,
                          mailboxmessagefolder_id     = f.id ,
                         mailboxfolder_id  = f.mailboxfolder.id,
                         senderstatus_id    = m.sender.status.id , //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.status.id,
                         blockstatus =  (db.blocks.Where(i=>i.profile_id  == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id !=null) ? true: false,
                         creationdate = m.creationdate,
                         senderscreenname =  m.sender.screenname  , //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                         recipientscreenname = m.recipeint.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                     });

            


            return filtermailmodels(models).Count();

        }

        public int GetNewMailCountbyfolderid(int folderid, string profile_id)
        {
            IEnumerable<mailmodel> models = null;

            models = (from m in db.mailboxmessages
                      join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id
                          && u.mailboxfolder.profiled_id == profile_id && u.readdate == null)
                      on m.id equals f.mailboxmessage_id
                      select new mailmodel
                      {

                          sender_id = m.sender_id,
                          recipient_id = m.recipient_id,
                           mailboxmessagefolder_id  = f.id,
                          mailboxfolder_id = f.mailboxfolder.id,
                          senderstatus_id = m.sender.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,
                          creationdate = m.creationdate,
                          senderscreenname = m.sender.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });
            
            return filtermailmodels(models).Count();

        }

        //TO DO find a way to use ENUM for these names 
        public IEnumerable<mailmodel> GetAllMailbydefaultmailboxfoldertypeMail(string foldertypename, string profile_id)
        {

            
            IEnumerable<mailmodel> models = null;

            models = (from m in db.mailboxmessages
                     join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id
                         && u.mailboxfolder.foldertype.name == foldertypename && u.mailboxfolder.profiled_id == profile_id)
                      on m.id equals f.mailboxmessage_id
                     orderby m.creationdate descending
                      select new mailmodel
                      {

                          mailboxfoldername  = f.mailboxfolder.foldertype.name ,
                          mailboxmessageid  = m.id ,
                          sender_id = m.sender_id,
                          body = m.body,
                          subject = m.subject,
                          mailboxmessagefolder_id  = f.id ,
                          mailboxfolder_id = f.mailboxfolder.id ,
                          age  = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                          city = m.sender.profiledata.city ,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                          state = m.sender.profiledata.stateprovince  , // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                          creationdate = m.creationdate,
                          recipient_id = m.recipient_id,
                          readdate    = f.readdate ,
                          replieddate = f.replieddate ,   
                          senderstatus_id = m.sender.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,                        
                          senderscreenname = m.sender.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });

            return filtermailmodels(models);


        }

        public IEnumerable<mailmodel> GetAllMailbyfolder(int folderid, string profile_id)
        {


         var   models = (from m in db.mailboxmessages
                      join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id
                          && u.mailboxfolder.profiled_id == profile_id && u.readdate == null && u.mailboxfolder.id == folderid )
                      on m.id equals f.mailboxmessage_id
                      select new mailmodel
                      {

                          mailboxfoldername  = f.mailboxfolder.foldertype.name ,
                          mailboxmessageid  = m.id ,
                          sender_id = m.sender_id,
                          body = m.body,
                          subject = m.subject,
                          mailboxmessagefolder_id  = f.id ,
                          mailboxfolder_id = f.mailboxfolder.id ,
                          age  = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                          city = m.sender.profiledata.city ,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                          state = m.sender.profiledata.stateprovince  , // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                          creationdate = m.creationdate,
                          recipient_id = m.recipient_id,
                          readdate    = f.readdate ,
                          replieddate = f.replieddate ,   
                          senderstatus_id = m.sender.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,                        
                          senderscreenname = m.sender.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });

            return filtermailmodels(models);
        }

        //TO DO read out the description feild from enum using sample code
        public IEnumerable<mailmodel> GetMailMsgThreadByUserID(int uniqueId, string profile_id)
        {

            IEnumerable<mailmodel> model = null;

            model = (from m in db.mailboxmessages.Where(x => x.uniqueid == uniqueId)
                     join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder.foldertype.name != "Deleted"
                     && u.mailboxfolder.profiled_id == profile_id)
                       on m.id equals f.mailboxmessage_id
                     orderby m.creationdate ascending
                     select new mailmodel
                     {

                         mailboxfoldername = f.mailboxfolder.foldertype.name,
                         mailboxmessageid = m.id,
                         sender_id = m.sender_id,
                         body = m.body,
                         subject = m.subject,
                         mailboxmessagefolder_id = f.id,
                         mailboxfolder_id = f.mailboxfolder.id,
                         age = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                         city = m.sender.profiledata.city,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                         state = m.sender.profiledata.stateprovince, // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                         creationdate = m.creationdate,
                         recipient_id = m.recipient_id,
                         readdate = f.readdate,
                         replieddate = f.replieddate,
                         senderstatus_id = m.sender.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                         recipientstatus_id = m.recipeint.status.id,
                         blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,
                         senderscreenname = m.sender.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                         recipientscreenname = m.recipeint.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                     });



            return filtermailmodels(model);

        }
    
    
        /// <summary>
        /// added a filter function so it is shared maybe do this client side
        /// </summary>
        /// <param name="mailmodels"></param>
        /// <returns></returns>
         private List<mailmodel> filtermailmodels (IEnumerable<mailmodel> mailmodels)
        {
            return mailmodels.Where(p => (p.senderstatus_id != (int)profilestatusEnum.Banned | p.senderstatus_id != (int)profilestatusEnum.Inactive))
                            .Where(p => p.senderscreenname != null)
                            .Where(p => p.recipientscreenname != null)
                            .Where(p => p.blockstatus != true).ToList();
                           

        }
    }
    
       
}
