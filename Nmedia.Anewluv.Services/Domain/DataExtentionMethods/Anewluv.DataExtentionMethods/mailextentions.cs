using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure;
using Repository.Pattern.UnitOfWork;
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
        public  static int getmailcountbyfolderid(ProfileModel model, int mailboxfolderid, IUnitOfWorkAsync db)
        {


            try
            {
               
                var blocks = db.Repository<block>().Queryable();
                var messages = db.Repository<mailboxmessage>().Queryable();
                var messagefolders = db.Repository<mailboxmessagefolder>().Queryable();


                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks.Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                var query =                    
(from p in messages.Where(p => p.profilemetadata.profile_id == model.profileid)
                    join f in messagefolders on new { a = p.id} equals new { a = f.mailboxmessage_id}
                    where (f.mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id)) //filter out banned profiles or deleted profiles            
                     select new mailviewmodel
                          {
                              sender_id = p.sender_id,
                              recipient_id = p.recipient_id                             

                          });

                return query.Count();


                //IEnumerable<mailviewmodel> models = null;
                ////get a model of the messages that match this mail type
                //models = (from m in db.Repository<mailboxmessage>().Query(p=>p.profilemetadata.profile_id == model.profileid).Select()
                //          join f in db.Repository<mailboxmessagefolder>().Query(u => u.mailboxfolder_id == mailboxfolderid
                //              && u.mailboxfolder.profiled_id == model.profileid)
                //          on m.id equals f.mailboxmessage_id
                //          select new mailviewmodel
                //          {
                //              sender_id = m.sender_id,
                //              recipient_id = m.recipient_id                             

                //          });

                //return models.Count();

            }

          
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int getnewmailcountbyfolderid(ProfileModel model,int mailboxfolderid, IUnitOfWorkAsync db)
        {


            try
            {

                var blocks = db.Repository<block>().Queryable();
                var messages = db.Repository<mailboxmessage>().Queryable();
                var messagefolders = db.Repository<mailboxmessagefolder>().Queryable();


                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks.Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                                var query =
                (from p in messages.Where(p => p.profilemetadata.profile_id == model.profileid)
                 join f in messagefolders on new { a = p.id } equals new { a = f.mailboxmessage_id }
                 where (f.mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id) && f.readdate == null) //filter out banned profiles or deleted profiles            
                 select new mailviewmodel
                 {
                     sender_id = p.sender_id,
                     recipient_id = p.recipient_id

                 });

                return query.Count();



                //IEnumerable<mailviewmodel> models = null;


                //models = (from m in db.Repository<mailboxmessage>().Query(p => p.profilemetadata.profile_id == model.profileid ).Select()
                //          join f in db.Repository<mailboxmessagefolder>().Query(u => u.mailboxfolder_id == mailboxfolderid
                //              && u.readdate == null)
                //          on m.id equals f.mailboxmessage_id
                //          select new mailviewmodel
                //          {

                //              sender_id = m.sender_id,
                //              recipient_id = m.recipient_id,
                             

                //          });

                ////  return filtermailmodels(models).Count();
                //return models.Count();

            }


            catch (Exception ex)
            {
                throw ex;
            }

        }
   
    }

}