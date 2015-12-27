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
using Nmedia.Infrastructure.Helpers;
using GeoData.Domain.Models;


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
                //var myblocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
                var otherblocks = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
                //added roles

                IQueryable<mailboxmessagefolder> mailboxmessagefolderlist = repo.Query(z => z.mailboxfolder.profile_id  == model.profileid.Value )
                    .Include(p => p.mailboxfolder.profilemetadata.profile)
                    .Include(p => p.mailboxfolder.profilemetadata.profile.membersinroles.Select(z => z.lu_role))
                    .Include(m=>m.mailboxmessage.recipientprofilemetadata.profile.profiledata)
                    .Include(m=>m.mailboxmessage.senderprofilemetadata.profile.profiledata)
                    .Select().AsQueryable();


                //remove profiles that blocked me  .i,e should be invlisble to me


                //return mailboxmessagefolderlist;
                
                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //folder id
                if (model.mailboxfolderid != null )
                    mailboxmessagefolderlist = mailboxmessagefolderlist.Where(p => p.mailboxfolder.id == model.mailboxfolderid.Value);
                //folder name filter
                if (model.mailboxfoldername != null && model.mailboxfoldername != "")
                    mailboxmessagefolderlist = mailboxmessagefolderlist.Where(p => p.mailboxfolder.displayname == model.mailboxfoldername);
                
                   //filter out blockedp messages here
                //remove profiles that blocked me  .i,e should be invlisble to me
                // mailboxmessagefolderlist = (from m in mailboxmessagefolderlist.Where(a => a.mailboxmessage.sender_id ==  model.profileid.Value)
                //                          where ( !otherblocks.Any(f => f.target_profile_id != m.mailboxmessage.sender_id  )) select m ).AsQueryable();


                return mailboxmessagefolderlist;


            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

        /// <summary>
        /// filters by folderid or foldername and profileid by default
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IQueryable<mailboxfolder> filtermailboxfolders(this IRepository<mailboxfolder> repo, MailModel model,IUnitOfWorkAsync db)
        {

            try
            {
                //TO DO figure out if we will add stuff where the the profile id  blocked members , maybe add to profile visiblity setings
                //get blocked profiles
                var otherblocks = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
                


                //added roles
                IQueryable<mailboxfolder> mailboxfolderlist = repo.Query(z => z.profile_id == model.profileid.Value
                    )
                    .Include(p => p.profilemetadata.profile).Include(p => p.profilemetadata.profile.membersinroles.Select(z =>z.lu_role))
                    .Include(p => p.mailboxmessagefolders.Select(z => z.mailboxmessage)).Select().AsQueryable();
                  
                 
                //TO DO filter out the blocks
                //  mailboxfolderlist = mailboxfolderlist.Where(z => !otherblocks.Any(d => d.target_profile_id == z.)
                    

                //remove profiles that blocked me  .i,e should be invlisble to me
                //to do roles ? allowing what photos they can view i.e the high rez stuff or more than 2 -3 etc

                //folder id
                if (model.mailboxfolderid != null)
                    mailboxfolderlist = mailboxfolderlist.Where(a => a.mailboxmessagefolders.Any((p => p.mailboxfolder.id == model.mailboxfolderid)));
                //folder name filter
                if (model.mailboxfoldername != null && model.mailboxfoldername != "")
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
        /// <summary>
        /// gets the mail for a folder using the folder id or folder name
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static MailSearchResultsViewModel getmailfilteredandpaged(this IRepository<mailboxmessagefolder> repo, MailModel model,IUnitOfWorkAsync db,IGeoDataStoredProcedures _storedProcedures)
        {

            try
            {
                var dd = filtermailboxmessagefolders(repo, model,db).OrderByDescending(z=>z.mailboxmessage.creationdate).ThenBy(f=>f.read );
                return pagemail(dd.ToList(),model, db,_storedProcedures);


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
                var dd = new MailFoldersViewModel();

                var d2 = filtermailboxfolders(repo, model, db).ToList();

                foreach (mailboxfolder folder in d2)
                {
                    var viewmodel = new MailFolderViewModel();
                    viewmodel.totalmessagecount = folder.mailboxmessagefolders.Count();
                    viewmodel.undreadmessagecount = folder.mailboxmessagefolders.Where(m => m.readdate == null).Count();
                    viewmodel.active = folder.active == 1 ? true : false;
                    viewmodel.readmessagecount = folder.mailboxmessagefolders.Where(m => m.readdate != null).Count();
                    viewmodel.folderid = folder.id;
                    viewmodel.foldername = folder.displayname;
                    dd.folders.Add(viewmodel);
                }



                //var folders = filtermailboxfolders(repo, model,db).Select(p => new MailFolderViewModel
                //{
                //    active = p.active ==1? true:false, folderid = p.id, foldername = p.displayname ,
                //    totalmessagecount = p.mailboxmessagefolders.Select(z=>z.mailboxmessage).Count(), 
                //    readmessagecount = p.mailboxmessagefolders.Where(m=>m.read == true).Count(),
                //    undreadmessagecount = p.mailboxmessagefolders.Where(m=>m.read == false ).Count()
                //}).ToList();

                //return new MailFoldersViewModel { folders = folders };

                return dd;
            }
            catch (Exception ex)
            {
                //eath the eception
                // throw ex;
            }

            return null;
        }

      

        //TO DO determine what side we are looking at sender or recipeint so we dont always double load both sides 
        public static MailSearchResultsViewModel pagemail(List<mailboxmessagefolder> source, MailModel model,
                                                                 IUnitOfWorkAsync db, IGeoDataStoredProcedures _storedProcedures)
        {


            try
            {
                // int? totalrecordcount = MemberSearchViewmodels.Count;
                //handle zero and null paging values
                if (model.page == null ||model.page == 0) model.page = 1;
                if (model.numberperpage == null || model.numberperpage == 0) model.numberperpage = 4;
                bool allowpaging = (source.Count() > model.numberperpage ? true : false);
                var pageData = model.page >= 1 & allowpaging ?
                    new PaginatedList<mailboxmessagefolder>().GetCurrentPages(source.ToList(),model.page ?? 1, model.numberperpage ?? 5) : source.Take(model.numberperpage.GetValueOrDefault());

           
                var results = pageData.Select(p => new MailViewModel
                {
                    senderprofile_id = p.mailboxmessage.senderprofilemetadata.profile_id,
                    senderscreenname = p.mailboxmessage.senderprofilemetadata.profile.screenname,
                    recipientprofile_id = p.mailboxmessage.recipientprofilemetadata.profile_id,
                    recipientscreenname = p.mailboxmessage.recipientprofilemetadata.profile.screenname,

                    senderstatus_id = p.mailboxmessage.senderprofilemetadata.profile.status_id,
                    recipientstatus_id = p.mailboxmessage.recipientprofilemetadata.profile.status_id,
                    body = p.mailboxmessage.body,
                    subject = p.mailboxmessage.subject,
                    mailboxmessageid = p.mailboxmessage_id,
                    mailboxfoldername = p.mailboxfolder.displayname,
                    mailboxfolder_id = p.mailboxfolder_id,
                    recipientage = DataFormatingExtensions.CalculateAge(p.mailboxmessage.senderprofilemetadata.profile.profiledata.birthdate.Value),
                    senderage = DataFormatingExtensions.CalculateAge(p.mailboxmessage.recipientprofilemetadata.profile.profiledata.birthdate.Value),

                    sendercity = p.mailboxmessage.senderprofilemetadata.profile.profiledata.city,
                    senderstate = p.mailboxmessage.senderprofilemetadata.profile.profiledata.stateprovince,
                    //TOD DO hard code country list from common and get it from cached version to filter , skipping for now
                    sendercountry =  spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = p.mailboxmessage.senderprofilemetadata.profile.profiledata.countryid.ToString() }, _storedProcedures),
                    recipientcity = p.mailboxmessage.recipientprofilemetadata.profile.profiledata.city,
                    recipientstate = p.mailboxmessage.recipientprofilemetadata.profile.profiledata.stateprovince,
                    //TOD DO hard code country list from common and get it from cached version to filter , skipping for now
                    recipientcountry = spatialextentions.getcountrynamebycountryid(new GeoModel { countryid = p.mailboxmessage.recipientprofilemetadata.profile.profiledata.countryid.ToString()}, _storedProcedures),

                    creationdate = p.mailboxmessage.creationdate,
                    //for paid members after a while
                    readbyrecipient = p.mailboxmessage.sender_id == model.profileid && p.read == true ? true : false,
                    readdate = p.readdate,
                    read = p.read,
                    replieddate = p.replieddate,
                    sendergenderid = p.mailboxmessage.senderprofilemetadata.profile.profiledata.gender_id.Value,
                    sendergalleryphoto = db.Repository<photoconversion>()
                    .getgalleryphotomodelbyprofileid(p.mailboxmessage.senderprofilemetadata.profile_id, (int)photoformatEnum.Medium),
                    recipientgenderid = p.mailboxmessage.recipientprofilemetadata.profile.profiledata.gender_id.Value,
                    recipientgalleryphoto = db.Repository<photoconversion>()
                    .getgalleryphotomodelbyprofileid(p.mailboxmessage.recipientprofilemetadata.profile_id, (int)photoformatEnum.Medium)



                }).ToList();

                return new MailSearchResultsViewModel { results = results, totalresults = source.Count() };
            }
            catch (Exception ex)
            { 
            //log error
                throw ex;
            
            }

        }


        //public static List<mailviewmodel> getallmailbyprofileid(ProfileModel model, IUnitOfWorkAsync db)
        //{

        //    try
        //    {
        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>()
        //        .Query(p => p.mailboxmessage.recipient_id == model.profileid.Value | p.mailboxmessage.sender_id == model.profileid.Value)
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

        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>().Query(p => p.mailboxmessage.recipient_id == model.profileid.Value).Include(z => z.mailboxmessage).Select().AsQueryable();
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

        //        var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);
        //        var messages = db.Repository<mailboxmessagefolder>().Query(p => p.mailboxmessage.recipient_id == model.profileid.Value).Include(z => z.mailboxmessage).Select().AsQueryable();
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