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
        public List<mailmessageviewmodel> replyemail1(int? id, int mailboxMsgFldId)
        {
            var allMail =
                  from m in db.mailboxmessages.Where(x => x.id == id)
                    select new mailmessageviewmodel  
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
            return allMail.ToList();
        }
// tobe deleted
        public List<mailmessageviewmodel> replyemail(int? id)
        {
            var allMail =
                  from m in db.mailboxmessages.Where(x => x.id  == id)
                  select new mailmessageviewmodel
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
            return allMail.ToList();
        }
        public int getmailboxmessagefoldersid(int mailboxMsgId ) {
            return (from p in db.mailboxmessagefolders
                    where p.mailboxmessage.id  == mailboxMsgId
                    && p.mailboxfolder.foldertype.defaultfolder.id == (int)defaultmailboxfoldertypeEnum.Inbox 
                    select p.mailboxfolder_id).FirstOrDefault();  
        }

        public int getmailboxfolderid(string mailboxFolderTypeName, int profileId)
        {
            return (from i in db.mailboxfolders
                    where i.foldertype.name == mailboxFolderTypeName && i.profiled_id == profileId
                    select i.id).FirstOrDefault();
        }

        public mailboxmessagefolder newmailboxmessagefolderobject(string mailboxFolderTypeName, int profileId)
        {
            int fldId = getmailboxfolderid(mailboxFolderTypeName, profileId); // Retrieve specific mailboxfolderId by its mailboxFolderTypename and ProfileID

            //create mailbox folders if we have a null mailbox folders for a user
            if (fldId == 0)
            {
                   //TO do move this to a mail repop
                   membersrepository.createmailboxfolders (profileId);
                   return newmailboxmessagefolderobject(mailboxFolderTypeName, profileId);
                

            }



            
            mailboxmessagefolder fld_mailboxmessagefolder = new mailboxmessagefolder() // Create a new mailboxmessagefolder object
            {
                mailboxfolder_id  = fldId,
                 readdate  = null,
                 replieddate  = null,
                 flaggeddate  = null,
                 deleteddate  = null,
                draftdate   =  null,
                recent   = false
                };
            return fld_mailboxmessagefolder;
        }

        public void add(mailboxmessage mailboxmessage)
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

        public string getuserid(string User)
        {

            return (from p in db.profiles
                    where p.username  == User
                    select p.id).FirstOrDefault().ToString();
        }

        public int getallmailcountbyfolderid(int folderid, int profile_id)
        {
            IEnumerable<mailviewmodel > models = null;

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
                       select new mailviewmodel 
                     {

                         sender_id = m.sender_id,
                         recipient_id = m.recipient_id,
                          mailboxmessagefolder_id     = f.mailboxfolder_id ,
                         mailboxfolder_id  = f.mailboxfolder.id,
                         senderstatus_id    = m.sender.profile.status.id , //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                         recipientstatus_id = m.recipeint.profile.status.id,
                         blockstatus =  (db.blocks.Where(i=>i.profile_id  == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id !=null) ? true: false,
                         creationdate = m.creationdate,
                         senderscreenname =  m.sender.profile.screenname  , //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                         recipientscreenname = m.recipeint.profile.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                     });

            


            return filtermailmodels(models).Count();

        }

        public int getnewmailcountbyfolderid(int folderid, int profile_id)
        {
            IEnumerable<mailviewmodel> models = null;

            models = (from m in db.mailboxmessages
                      join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id
                          && u.mailboxfolder.profiled_id == profile_id && u.readdate == null)
                      on m.id equals f.mailboxmessage_id
                      select new mailviewmodel
                      {

                          sender_id = m.sender_id,
                          recipient_id = m.recipient_id,
                          mailboxmessagefolder_id = f.mailboxfolder_id,
                          mailboxfolder_id = f.mailboxfolder.id,
                          senderstatus_id = m.sender.profile.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.profile.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,
                          creationdate = m.creationdate,
                          senderscreenname = m.sender.profile.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.profile.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });
            
            return filtermailmodels(models).Count();

        }

        //TO DO find a way to use ENUM for these names 
        //TO DO re-wite code based on EF code first composite table queries
        public List<mailviewmodel> getallmailbydefaultmailboxfoldertypemail(string foldertypename, int profile_id)
        {

            
            IEnumerable<mailviewmodel> models = null;

            models = (from m in db.mailboxmessages
                     join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id 
                         && u.mailboxfolder.foldertype.name == foldertypename && u.mailboxfolder.profiled_id == profile_id)
                      on m.id equals f.mailboxmessage_id
                     orderby m.creationdate descending
                      select new mailviewmodel
                      {

                          mailboxfoldername  = f.mailboxfolder.foldertype.name ,
                          mailboxmessageid  = m.id ,
                          sender_id = m.sender_id,
                          body = m.body,
                          subject = m.subject,                         
                          mailboxfolder_id = f.mailboxfolder.id ,
                          age  = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                          city = m.sender.profiledata.city ,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                          state = m.sender.profiledata.stateprovince  , // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                          creationdate = m.creationdate,
                          recipient_id = m.recipient_id,
                          readdate    = f.readdate ,
                          replieddate = f.replieddate ,   
                          senderstatus_id = m.sender.profile.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.profile.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,                        
                          senderscreenname = m.sender.profile.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.profile.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });

            return filtermailmodels(models).ToList();


        }

        public List<mailviewmodel> getallmailbyfolder(int folderid, int profile_id)
        {


         var   models = (from m in db.mailboxmessages
                      join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder_id == u.mailboxfolder.id
                          && u.mailboxfolder.profiled_id == profile_id && u.readdate == null && u.mailboxfolder.id == folderid )
                      on m.id equals f.mailboxmessage_id
                      select new mailviewmodel
                      {

                          mailboxfoldername  = f.mailboxfolder.foldertype.name ,
                          mailboxmessageid  = m.id ,
                          sender_id = m.sender_id,
                          body = m.body,
                          subject = m.subject,                        
                          mailboxfolder_id = f.mailboxfolder.id ,
                          age  = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                          city = m.sender.profiledata.city ,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                          state = m.sender.profiledata.stateprovince  , // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                          creationdate = m.creationdate,
                          recipient_id = m.recipient_id,
                          readdate    = f.readdate ,
                          replieddate = f.replieddate ,   
                          senderstatus_id = m.sender.profile.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                          recipientstatus_id = m.recipeint.profile.status.id,
                          blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,                        
                          senderscreenname = m.sender.profile.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                          recipientscreenname = m.recipeint.profile.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()

                      });

            return filtermailmodels(models);
        }

        //TO DO read out the description feild from enum using sample code
        public List<mailviewmodel> getmailmsgthreadbyuserid(int uniqueId, int profile_id)
        {

            IEnumerable<mailviewmodel> model = null;

            model = (from m in db.mailboxmessages.Where(x => x.uniqueid == uniqueId)
                     join f in db.mailboxmessagefolders.Where(u => u.mailboxfolder.foldertype.name != "Deleted"
                     && u.mailboxfolder.profiled_id == profile_id)
                       on m.id equals f.mailboxmessage_id
                     orderby m.creationdate ascending
                     select new mailviewmodel
                     {

                         mailboxfoldername = f.mailboxfolder.foldertype.name,
                         mailboxmessageid = m.id,
                         sender_id = m.sender_id,
                         body = m.body,
                         subject = m.subject,
                         mailboxfolder_id = f.mailboxfolder.id,
                         age = m.sender.profiledata.birthdate, //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.Birthdate).FirstOrDefault(),
                         city = m.sender.profiledata.city,   //(from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.City).FirstOrDefault(),
                         state = m.sender.profiledata.stateprovince, // (from p in db.ProfileDatas where p.ProfileID == m.sender_id select p.State_Province).FirstOrDefault(),
                         creationdate = m.creationdate,
                         recipient_id = m.recipient_id,
                         readdate = f.readdate,
                         replieddate = f.replieddate,
                         senderstatus_id = m.sender.profile.status.id, //(from p in db.profiles where p.id  == m.sender_id select p.status.id ).FirstOrDefault(),
                         recipientstatus_id = m.recipeint.profile.status.id,
                         blockstatus = (db.blocks.Where(i => i.profile_id == profile_id && i.blockprofile_id == m.sender_id && i.removedate == null).FirstOrDefault().id != null) ? true : false,
                         senderscreenname = m.sender.profile.screenname, //(from p in db.profiles where (p.id == m.sender_id) select p.screenname  ).FirstOrDefault(),
                         recipientscreenname = m.recipeint.profile.screenname // (from p in db.profiles where (p.id  == m.recipient_id) select p.screenname ).FirstOrDefault()
                     });



            return filtermailmodels(model);

        }
    
    
        /// <summary>
        /// added a filter function so it is shared maybe do this client side
        /// </summary>
        /// <param name="mailmodels"></param>
        /// <returns></returns>
         private List<mailviewmodel> filtermailmodels (IEnumerable<mailviewmodel> mailmodels)
        {
            return mailmodels.Where(p => (p.senderstatus_id != (int)profilestatusEnum.Banned | p.senderstatus_id != (int)profilestatusEnum.Inactive))
                            .Where(p => p.senderscreenname != null)
                            .Where(p => p.recipientscreenname != null)
                            .Where(p => p.blockstatus != true).ToList();
                           

        }




    }
    
       
}
