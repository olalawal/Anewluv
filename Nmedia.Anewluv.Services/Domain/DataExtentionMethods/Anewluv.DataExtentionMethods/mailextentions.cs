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
using Repository.Pattern.Repositories;
using Anewluv.Domain.Data.Helpers;

namespace Anewluv.DataExtentionMethods
{


    public static class mailextentions
    {


        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static IQueryable<mailboxmessagefolder> filtermailboxmessagefolders(this IRepository<mailboxmessagefolder> repo, MailModel model,IUnitOfWorkAsync db)
        {

            try
            {
                //get blocked profiles , and filter any profiles on left or right side, i.e if a member blocked me or i blocked them
                //var myblocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
                var otherblocks = db.Repository<action>().getothersactiontomebyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
                //added roles

                IQueryable<mailboxmessagefolder> mailboxmessagefolderlist = repo.Query(z => z.mailboxfolder.profile_id  == model.profileid )
                    .Include(p => p.mailboxfolder.profilemetadata.profile).Include(p => p.mailboxfolder.profilemetadata.profile.membersinroles.Select(z => z.profile_id == model.profileid))
                    .Include(m=>m.mailboxmessage.recipientprofilemetadata.profile.profiledata).Include(m=>m.mailboxmessage.senderprofilemetadata.profile.profiledata)
                    .Select().AsQueryable();


                //remove profiles that blocked me  .i,e should be invlisble to me
                mailboxmessagefolderlist = (from m in mailboxmessagefolderlist.Where(a => a.mailboxmessage.sender_id ==  model.profileid)
                                           where ( !otherblocks.Any(f => f.target_profile_id != m.mailboxmessage.sender_id  )) select m ).AsQueryable();     

               
                
                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //folder id
                if (model.mailboxfolderid != null)
                    mailboxmessagefolderlist = mailboxmessagefolderlist.Where(a => a.mailboxfolder.mailboxmessagefolders.Any((p => p.mailboxfolder.id == model.mailboxfolderid)));
                //folder name filter
                if (model.mailboxfoldername != "" | model.mailboxfoldername != null)
                    mailboxmessagefolderlist = mailboxmessagefolderlist.Where(a => a.mailboxfolder.mailboxmessagefolders.Any((p => p.mailboxfolder.displayname == model.mailboxfoldername)));
                
                return mailboxmessagefolderlist;


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

        public static IQueryable<mailboxfolder> filtermailboxfolders(this IRepository<mailboxfolder> repo, MailModel model,IUnitOfWorkAsync db)
        {

            try
            {
                //TO DO figure out if we will add stuff where the the profile id  blocked members , maybe add to profile visiblity setings
                //get blocked profiles
                var otherblocks = db.Repository<action>().getothersactiontomebyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
                


                //added roles
                IQueryable<mailboxfolder> mailboxfolderlist = repo.Query(z => z.profile_id == model.profileid
                    )
                    .Include(p => p.profilemetadata.profile).Include(p => p.profilemetadata.profile.membersinroles.Select(z =>z.lu_role))
                    .Include(p => p.mailboxmessagefolders.Select(z => z.mailboxmessage).Where(z => !otherblocks.Any(d => d.target_profile_id == z.sender_id))).Select().AsQueryable();
                    


                //remove profiles that blocked me  .i,e should be invlisble to me
               



                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //folder id
                if (model.mailboxfolderid != null)
                    mailboxfolderlist = mailboxfolderlist.Where(a => a.mailboxmessagefolders.Any((p => p.mailboxfolder.id == model.mailboxfolderid)));
                //folder name filter
                if (model.mailboxfoldername != "" | model.mailboxfoldername != null)
                    mailboxfolderlist = mailboxfolderlist.Where(a => a.mailboxmessagefolders.Any((p => p.mailboxfolder.displayname == model.mailboxfoldername)));

                return mailboxfolderlist;


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }


        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static MailSearchResultsViewModel getmailfilteredandpaged(this IRepository<mailboxmessagefolder> repo, MailModel model,IUnitOfWorkAsync db)
        {

            try
            {
                var dd = filtermailboxmessagefolders(repo, model,db);
                return pagemail(dd.ToList(), model.page, model.numberperpage, db);


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

        //TO DO Premuim roles get all
        //TO DO needs code to check roles to see how many photos can be viewd etc
        public static MailFoldersViewModel getmailfolderdetails(this IRepository<mailboxfolder> repo, MailModel model,IUnitOfWorkAsync db)
        {

            try
            {
                var folders = filtermailboxfolders(repo, model,db).Select(p => new MailFolderViewModel
                {
                    active = p.active ==1? true:false, folderid = p.id, foldername = p.displayname ,
                    totalmessagecount = p.mailboxmessagefolders.Select(z=>z.mailboxmessage).Count(),
                    undreadmessagecount = p.mailboxmessagefolders.Select(z => z.mailboxmessage.mailboxmessagefolders.Where(m=>m.readdate == null)).Count()
                }).ToList();

                return new MailFoldersViewModel { folders = folders };


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

      

        //TO DO determine what side we are looking at sender or recipeint so we dont always double load both sides 
        public static MailSearchResultsViewModel pagemail(List<mailboxmessagefolder> source,
                                                                  int? page, int? numberperpage,IUnitOfWorkAsync db)
        {


            // int? totalrecordcount = MemberSearchViewmodels.Count;
            //handle zero and null paging values
            if (page == null || page == 0) page = 1;
            if (numberperpage == null || numberperpage == 0) numberperpage = 4;
            bool allowpaging = (source.Count() >= (page * numberperpage) ? true : false);
            var pageData = page > 1 & allowpaging ?
                new PaginatedList<mailboxmessagefolder>().GetCurrentPages(source.ToList(), page ?? 1, numberperpage ?? 20) : source.Take(numberperpage.GetValueOrDefault());


            var results = pageData.Select(p => new MailViewModel
            {
                senderprofile_id = p.mailboxmessage.senderprofilemetadata.profile_id ,
                senderscreenname = p.mailboxmessage.senderprofilemetadata.profile.screenname,
                recipientprofile_id = p.mailboxmessage.recipientprofilemetadata.profile_id ,
                recipientscreenname =p.mailboxmessage.recipientprofilemetadata.profile.screenname ,

               senderstatus_id = p.mailboxmessage.senderprofilemetadata.profile.status_id,
               recipientstatus_id = p.mailboxmessage.recipientprofilemetadata.profile.status_id,   
               body = p.mailboxmessage.body,
               subject =p.mailboxmessage.subject,             
               mailboxmessageid = p.mailboxmessage_id ,
               mailboxfoldername =p.mailboxfolder.displayname,   
               mailboxfolder_id = p.mailboxfolder_id,               
               recipientage =  DataFormatingExtensions.CalculateAge( p.mailboxmessage.senderprofilemetadata.profile.profiledata.birthdate.Value),
               senderage = DataFormatingExtensions.CalculateAge( p.mailboxmessage.recipientprofilemetadata.profile.profiledata.birthdate.Value),           
                          
                sendercity = p.mailboxmessage.senderprofilemetadata.profile.profiledata.city,
                senderstate =p.mailboxmessage.senderprofilemetadata.profile.profiledata.stateprovince,
                //TOD DO hard code country list from common and get it from cached version to filter , skipping for now
                sendercountry =p.mailboxmessage.senderprofilemetadata.profile.profiledata.countryid.ToString() ,

                 recipientcity = p.mailboxmessage.recipientprofilemetadata.profile.profiledata.city,
               recipientstate =p.mailboxmessage.recipientprofilemetadata.profile.profiledata.stateprovince,
                //TOD DO hard code country list from common and get it from cached version to filter , skipping for now
                recipientcountry =p.mailboxmessage.recipientprofilemetadata.profile.profiledata.countryid.ToString() ,

                creationdate =p.mailboxmessage.creationdate,
                read = p.read,
                replieddate = p.replieddate,     
                sendergalleryphoto = db.Repository<photoconversion>()
                .getgalleryphotomodelbyprofileid(p.mailboxmessage.senderprofilemetadata.profile_id , (int)photoformatEnum.Thumbnail),     
                recipientgalleryphoto = db.Repository<photoconversion>()
                .getgalleryphotomodelbyprofileid(p.mailboxmessage.recipientprofilemetadata.profile_id , (int)photoformatEnum.Thumbnail)
                   
              
    
                 }).ToList();

            return new MailSearchResultsViewModel { results = results, totalresults = source.Count() };

        }




    

        //public static List<mailviewmodel> getallmailbyprofileid(ProfileModel model, IUnitOfWorkAsync db)
        //{

        //    try
        //    {
        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>()
        //        .Query(p => p.mailboxmessage.recipient_id == model.profileid | p.mailboxmessage.sender_id == model.profileid)
        //        .Include(z => z.mailboxmessage)
        //            //  .Include(z => z.mailboxmessage.recipientprofilemetadata.profile.profiledata)
        //            //   .Include(z => z.mailboxmessage.senderprofilemetadata.profile.profiledata)
        //        .Select().AsQueryable();
        //        //var messagefolders = db.Repository<mailboxmessagefolder>().Queryable().Where(p=>p.);


        //        var dd = (from p in messages
        //                  where (!blocks.Any(z => z.target_profile_id != p.mailboxmessage.sender_id))
        //                  select new mailviewmodel
        //                  {
        //                      sender_id = p.mailboxmessage.sender_id,
        //                      recipient_id = p.mailboxmessage.recipient_id,
        //                      body = p.mailboxmessage.body,
        //                      readdate = p.readdate
        //                      //subject = p.mailboxmessage.subject,
        //                      //  recipientscreenname = p.mailboxmessage.recipientprofilemetadata.profile.screenname,
        //                      //  senderscreenname = p.mai

        //                  });
        //        return dd.ToList();

        //        // var query =                    
        //        //     (from p in messages
        //        ////   join f in messagefolders on new { a = p.id} equals new { a = f.mailboxmessage_id}
        //        //    where (z- .mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id)) //filter out banned profiles or deleted profiles            
        //        //      select new mailviewmodel
        //        //           {
        //        //               sender_id = p.sender_id,
        //        //               recipient_id = p.recipient_id                             

        //        //           });

        //        // return query.Count();




        //    }


        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public static int getmailcountbyfolderid(ProfileModel model, int mailboxfolderid, IUnitOfWorkAsync db)
        //{


        //    try
        //    {

        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>().Query(p => p.mailboxmessage.recipient_id == model.profileid).Include(z => z.mailboxmessage).Select().AsQueryable();
        //        //var messagefolders = db.Repository<mailboxmessagefolder>().Queryable().Where(p=>p.);


        //        var dd = (from p in messages
        //                  where (!blocks.Any(z => z.target_profile_id != p.mailboxmessage.sender_id))
        //                  select new mailviewmodel
        //                  {
        //                      sender_id = p.mailboxmessage.sender_id,
        //                      recipient_id = p.mailboxmessage.recipient_id

        //                  });

        //        return dd.Count(); ;


        //        // var query =                    
        //        //     (from p in messages
        //        ////   join f in messagefolders on new { a = p.id} equals new { a = f.mailboxmessage_id}
        //        //    where (z- .mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id)) //filter out banned profiles or deleted profiles            
        //        //      select new mailviewmodel
        //        //           {
        //        //               sender_id = p.sender_id,
        //        //               recipient_id = p.recipient_id                             

        //        //           });

        //        // return query.Count();




        //    }


        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public static int getnewmailcountbyfolderid(ProfileModel model, int mailboxfolderid, IUnitOfWorkAsync db)
        //{


        //    try
        //    {

        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid, actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>().Query(p => p.mailboxmessage.recipient_id == model.profileid).Include(z => z.mailboxmessage).Select().AsQueryable();
        //        var messagefolders = db.Repository<mailboxmessagefolder>().Queryable();





        //        var query =
        //      (from p in messages
        //       where (!blocks.Any(z => z.target_profile_id != p.mailboxmessage.sender_id) && p.readdate == null) //filter out banned profiles or deleted profiles            
        //       select new mailviewmodel
        //       {
        //           sender_id = p.mailboxmessage.sender_id,
        //           recipient_id = p.mailboxmessage.recipient_id

        //       });

        //        return query.Count();


        //        //         var query =
        //        //(from p in messages
        //        // join f in messagefolders on new { a = p.id } equals new { a = f.mailboxmessage_id }
        //        // where (f.mailboxfolder_id == mailboxfolderid && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.sender_id) && f.readdate == null) //filter out banned profiles or deleted profiles            
        //        // select new mailviewmodel
        //        // {
        //        //     sender_id = p.sender_id,
        //        //     recipient_id = p.recipient_id

        //        // });

        //        //return query.Count();




        //    }


        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

    }

}