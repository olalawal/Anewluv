using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.ViewModels;
using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anewluv.DataExtentionMethods
{


    public static class mailextentions
    {

      

        //INTEREST methods
        ////////////////////////////////////////
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public  static int getmailcountbyfolderid(ProfileModel model, int mailboxfolderid, IUnitOfWork db)
        {


            try
            {
                IEnumerable<mailviewmodel> models = null;

                //return (from i in db.mailboxmessagefolders
                //             .Where(u => u.mailboxfolderID == u.mailboxfolder.mailboxfolderID
                //            && u.mailboxfolder.foldertype.name == MailType && u.mailboxfolder.ProfileID == User)
                //        select i.MessageRead).Count();


                //join f in _datingcontext.profiledatas on p.blockprofile_id  equals f.id 
                //get a model of the messages that match this mail type

                //get a model of the messages that match this mail type
                models = (from m in db.GetRepository<mailboxmessage>().Find(p=>p.profilemetadata.profile_id == model.profileid)
                          join f in db.GetRepository<mailboxmessagefolder>().Find(u => u.mailboxfolder_id == mailboxfolderid
                              && u.mailboxfolder.profiled_id == model.profileid)
                          on m.id equals f.mailboxmessage_id
                          select new mailviewmodel
                          {
                              sender_id = m.sender_id,
                              recipient_id = m.recipient_id                             

                          });

                return models.Count();

            }

          
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int getnewmailcountbyfolderid(ProfileModel model,int mailboxfolderid, IUnitOfWork db)
        {


            try
            {
                IEnumerable<mailviewmodel> models = null;


                models = (from m in db.GetRepository<mailboxmessage>().Find(p => p.profilemetadata.profile_id == model.profileid )
                          join f in db.GetRepository<mailboxmessagefolder>().Find(u => u.mailboxfolder_id == mailboxfolderid
                              && u.readdate == null)
                          on m.id equals f.mailboxmessage_id
                          select new mailviewmodel
                          {

                              sender_id = m.sender_id,
                              recipient_id = m.recipient_id,
                             

                          });

                //  return filtermailmodels(models).Count();
                return models.Count();

            }


            catch (Exception ex)
            {
                throw ex;
            }

        }
   
    }

}