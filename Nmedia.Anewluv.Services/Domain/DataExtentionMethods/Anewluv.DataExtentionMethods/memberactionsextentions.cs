﻿using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anewluv.DataExtentionMethods
{


    public static class memberactionsextentions
    {


        #region " standard queryable extentions actions to me and actions i made to others i.e whoipeekedat would be actiontype peek and creator would me me"


        //My actions
        public static  IQueryable<action> getmyactionsbyprofileidandactiontype(this IRepository<action> repo, int profileid, int action,bool? active = true)
        {
            return repo.Query(p => (p.actiontype_id == (int)action & p.active == active & p.deletedbycreatordate == null)
                  && p.creator_profile_id == profileid)               
                  .Include(z => z.targetprofilemetadata.profile).Select().AsQueryable();
        }

        public static IQueryable<action> getmyactionsbyprofileid(this IRepository<action> repo, int profileid, bool? active = true)
        {
            return repo.Query(p => (p.active == active & p.deletedbycreatordate == null)
                  && p.creator_profile_id == profileid).Include(z => z.targetprofilemetadata.profile).Select().AsQueryable();
        }

        public static IQueryable<action> getallmyactionsbyprofileid(this IRepository<action> repo, int profileid)
        {
            return repo.Query(p=>p.creator_profile_id == profileid)
                .Include(z => z.targetprofilemetadata.profile).Select().AsQueryable();
        }


        //others actions 
        //this query should skipp items that were deleted so we can keep track of how many times a member added/removed others
        /// TO DO maybe we ignore deleteted by creator for others actions ? i.e its like sending an email and deleting after the fact
        /// maybe only premiums can do this
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="profileid"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IQueryable<action> getothersactionsbyprofileidandactiontype(this IRepository<action> repo, int profileid, int action, bool? active = true)
        {
            return repo.Query(p => (p.actiontype_id == (int)action & p.active == active & p.deletedbycreatordate == null & p.deletedbytargetdate == null)
                   && p.target_profile_id == profileid).Include(z => z.creatorprofilemetadata.profile).Select().AsQueryable();
        }

        /// <summary>
        /// TO DO maybe we ignore deleteted by creator for others actions ? i.e its like sending an email and deleting after the fact
        /// maybe only premiums can do this
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="profileid"></param>
        /// <returns></returns>
        public static IQueryable<action> getothersctionsbyprofileid(this IRepository<action> repo, int profileid, bool? active = true)
        {
            return repo.Query(p => (p.active == true & p.deletedbycreatordate == null & p.deletedbytargetdate == null)
                   && p.target_profile_id == profileid).Include(z => z.creatorprofilemetadata.profile).Select().AsQueryable();
        }

        public static IQueryable<action> getallothersactionbyprofileid(this IRepository<action> repo, int profileid)
        {
            return repo.Query(p => 
                    p.target_profile_id == profileid).Include(z => z.creatorprofilemetadata.profile).Select().AsQueryable();
        }


        //all actions involving this Profile


        #endregion



        #region "methods for reuuse"

        //INTEREST methods
        ////////////////////////////////////////
        //count methods first
        /// <summary>
        /// count all total interests ,disctinc by profile
        /// </summary>       
        public static List<profile> getmyactiveactionprofilesbyprofileidandactiontype(ProfileModel model, IUnitOfWorkAsync db, int actionid, bool? unviewedactions = false)
        {
            try
            {
                var blocks = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block).ToList();
                List<action> myactions = null;

                if (!unviewedactions.GetValueOrDefault())
                {
                    myactions = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, actionid).GroupBy(x => x.target_profile_id).Select(y => y.FirstOrDefault()).Distinct().ToList();                    
                }
                else
                {
                    myactions = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, actionid).Where(p => p.viewdate != null).GroupBy(x => x.target_profile_id).Select(y => y.FirstOrDefault()).Distinct().ToList();
                }

                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks
                                     select new
                                     {
                                         ProfilesBlockedId = c.target_profile_id
                 
                                     };
                if (myactions.Count == 0) new List<profile>();

                var query =
                     from p in myactions
                     join f in db.Repository<profilemetadata>().Queryable() on p.target_profile_id equals f.profile_id
                     join z in db.Repository<profile>().Queryable() on p.target_profile_id equals z.id
                     where (z.status_id != (int)profilestatusEnum.Banned | z.status_id != (int)profilestatusEnum.Inactive && !MyActiveblocks.Any(b => b.ProfilesBlockedId == z.id)) //filter out banned profiles or deleted profiles            
                     select z;
             
                return query.ToList();
          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<profile> getactiveotheractionprofilesbyprofileidandactiontype(ProfileModel model, IUnitOfWorkAsync db, int actionid, bool? unviewedactions = false)
       {

         try
         {
             var blocks = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);

             List<action> othersactionstome = null;
             if (!unviewedactions.GetValueOrDefault())
             {
                 othersactionstome = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, actionid).GroupBy(x => x.creator_profile_id).Select(y => y.FirstOrDefault()).Distinct().ToList();   
             }
             else
             {
                 othersactionstome = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, actionid).Where(p => p.viewdate != null).GroupBy(x => x.creator_profile_id).Select(y => y.FirstOrDefault()).Distinct().ToList();
             }
             //filter out blocked profiles 
             var MyActiveblocks = from c in blocks
                                  select new
                                  {
                                      ProfilesBlockedId = c.target_profile_id
                                  };

             if (othersactionstome.Count == 0) new List<profile>();

             var query =
                     from p in othersactionstome
                     join f in db.Repository<profilemetadata>().Queryable() on p.creator_profile_id equals f.profile_id
                     join z in db.Repository<profile>().Queryable() on p.creator_profile_id equals z.id
                     where (z.status_id != (int)profilestatusEnum.Banned | z.status_id != (int)profilestatusEnum.Inactive && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.id)) //filter out banned profiles or deleted profiles            
                     select z;

             return query.ToList();
             // count =          db.Repository<interest>().Count(f => f.profile_id == model.profileid.Value && f.deletedbymemberdate == null);

         }
         catch (Exception ex)
         {
             throw ex;
         }


     }
   
   

        #endregion

    }

}