using Anewluv.Domain.Data;
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

        public static  IQueryable<action> getmyactionbyprofileidandactiontype(this IRepository<action> repo, int profileid, int action)
        {
            return repo.Query(p => (p.actiontype_id == (int)action & p.active == true & p.deletedbycreatordate == null)
                  && p.creator_profile_id == profileid).Include(z => z.targetprofilemetadata.profile).Select().AsQueryable();
        }

        public static IQueryable<action> getmyactionbyprofileid(this IRepository<action> repo, int profileid)
        {
            return repo.Query(p => (p.active == true & p.deletedbycreatordate == null)
                  && p.creator_profile_id == profileid).Include(z => z.targetprofilemetadata.profile).Select().AsQueryable();
        }


        public static IQueryable<action> getothersactiontomebyprofileidandactiontype(this IRepository<action> repo, int profileid, int action)
        {
            return repo.Query(p => (p.actiontype_id == (int)action & p.active == true & p.deletedbycreatordate == null)
                   && p.target_profile_id == profileid).Include(z => z.creatorprofilemetadata.profile).Select().AsQueryable();
        }

        public static IQueryable<action> getothersactiontomebyprofileid(this IRepository<action> repo, int profileid)
        {
            return repo.Query(p => (p.active == true & p.deletedbycreatordate == null)
                   && p.target_profile_id == profileid).Include(z => z.creatorprofilemetadata.profile).Select().AsQueryable();
        }

        #endregion



        #region "methods for reuuse"

        //INTEREST methods
        ////////////////////////////////////////
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
     public static List<profile> getmyactionbyprofileidandactiontype(ProfileModel model, IUnitOfWorkAsync db,int actionid)
        {
         
            try
            {

                var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value,(int) actiontypeEnum.Block).ToList();
                var myactions = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, actionid).ToList();                 
              

                //filter out blocked profiles 
                var MyActiveblocks = from c in blocks
                                     select new
                                     {
                                         ProfilesBlockedId = c.target_profile_id
                 
                                     };
                if (myactions.Count == 0) new List<profile>();

                var query =
                     from p in myactions 
                     where (p.targetprofilemetadata.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.targetprofilemetadata.profile.id)) //filter out banned profiles or deleted profiles            
                     select p.targetprofilemetadata.profile;

             
                return query.ToList();

              
          
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


     public static List<profile> getotheractionbyprofileidandactiontype(ProfileModel model, IUnitOfWorkAsync db, int actionid, bool? unviewed = false)
     {


         try
         {


             var blocks = db.Repository<action>().getmyactionbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block);

             List<action> othersactionstome = null;

             if (unviewed.GetValueOrDefault())
             {
              othersactionstome=   db.Repository<action>().getothersactiontomebyprofileidandactiontype(model.profileid.Value, actionid).ToList();
             }
             else
             {
              othersactionstome=    db.Repository<action>().getothersactiontomebyprofileidandactiontype(model.profileid.Value, actionid).Where(p=>p.viewdate !=null).ToList();
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
                     where (p.creatorprofilemetadata.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == p.creatorprofilemetadata.profile.id)) //filter out banned profiles or deleted profiles            
                     select p.creatorprofilemetadata.profile;

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