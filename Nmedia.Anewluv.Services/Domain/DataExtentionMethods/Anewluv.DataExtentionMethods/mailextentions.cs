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

                var blocks = db.Repository<action>().get(model.profileid, actiontypeEnum.Block);
                var messages = db.Repository<mailboxmessage>().Queryable();
                var messagefolders = db.Repository<mailboxmessagefolder>().Queryable();


                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks
                                     select new
                                     {
                                         ProfilesBlockedId = c.target_profile_id
                                     };

                var query =                    
                    (from p in messages.Where(p => p.recipientprofilemetadata.profile_id == model.profileid)
                    join f in messagefolders on new { a = p.id} equals new { a = f.mailboxmessage_id}
                    where (f.mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id)) //filter out banned profiles or deleted profiles            
                     select new mailviewmodel
                          {
                              sender_id = p.sender_id,
                              recipient_id = p.recipient_id                             

                          });

                return query.Count();


           

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

                var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
                var messages = db.Repository<mailboxmessage>().Queryable();
                var messagefolders = db.Repository<mailboxmessagefolder>().Queryable();


                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks
                                     select new
                                     {
                                         ProfilesBlockedId = c.target_profile_id
                                     };

                                var query =
                (from p in messages.Where(p => p.recipientprofilemetadata.profile_id == model.profileid)
                 join f in messagefolders on new { a = p.id } equals new { a = f.mailboxmessage_id }
                 where (f.mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id) && f.readdate == null) //filter out banned profiles or deleted profiles            
                 select new mailviewmodel
                 {
                     sender_id = p.sender_id,
                     recipient_id = p.recipient_id

                 });

                return query.Count();




            }


            catch (Exception ex)
            {
                throw ex;
            }

        }
   
    }

}