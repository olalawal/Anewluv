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


    public static class memberactionsextentions
    {

        #region "private  count methods for reuuse"

        //INTEREST methods
        ////////////////////////////////////////
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
     public static int getwhoiaminterestedincount(ProfileModel model, IUnitOfWork db)
        {


            try
            {



                int? count = null;
                int defaultvalue = 0;


                count =          db.GetRepository<interest>().Count(f => f.profile_id == model.profileid && f.deletedbymemberdate == null);
          
                // ?? operator example.
                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;







            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
     public static int getwhoisinterestedinmecount(ProfileModel model, IUnitOfWork db)
        {
            try
            {
                int? count = null;
                int defaultvalue = 0;
                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<interest>().Find()
                   where (p.interestprofile_id == model.profileid)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.

                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
     public static int getwhoisinterestedinmenewcount(ProfileModel model, IUnitOfWork db)
        {



            try
            {



                int? count = null;
                int defaultvalue = 0;


                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<interest>().Find()
                   where (p.interestprofile_id == model.profileid && p.viewdate == null)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.


                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;

                return defaultvalue;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //PEEK methods
        ////////////////////////

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
     public static int getwhoipeekedatcount(ProfileModel model, IUnitOfWork db)
        {



            try
            {



                int? count = null;
                int defaultvalue = 0;


                count = (
           from f in db.GetRepository<peek>().Find()
           where (f.profile_id == model.profileid && f.deletedbymemberdate == null)
           select f).Count();
                // ?? operator example.
                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
     public static int getwhopeekedatmecount(ProfileModel model, IUnitOfWork db)
        {




            try
            {

                int? count = null;
                int defaultvalue = 0;


                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<peek>().Find()
                   where (p.peekprofile_id == model.profileid)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.


                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;

                return defaultvalue;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
     public static int getwhopeekedatmenewcount(ProfileModel model, IUnitOfWork db)
        {

            try
            {

                int? count = null;
                int defaultvalue = 0;


                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<peek>().Find()
                   where (p.peekprofile_id == model.profileid && p.viewdate == null)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.


                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;

                return defaultvalue;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //BLock methods
        ///////////////////////////////////////// 

     public static int getwhoiblockedcount(ProfileModel model, IUnitOfWork db)
        {

            try
            {
                int? count = null;
                int defaultvalue = 0;


                count = (
                  from f in db.GetRepository<block>().Find()
                  where (f.profile_id == model.profileid && f.removedate == null)
                  select f).Count();

                // ?? operator example.
                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //LIke methods
        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
     public static int getwhoilikecount(ProfileModel model, IUnitOfWork db)
        {



            try
            {


                int? count = null;
                int defaultvalue = 0;


                count = (
           from f in db.GetRepository<like>().Find()
           where (f.profile_id == model.profileid && f.deletedbymemberdate == null)
           select f).Count();
                // ?? operator example.
                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;
                return defaultvalue;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
     public static int getwholikesmecount(ProfileModel model, IUnitOfWork db)
        {
            try
            {

                int? count = null;
                int defaultvalue = 0;


                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<like>().Find()
                   where (p.likeprofile_id == model.profileid)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.


                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;

                return defaultvalue;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
     public static int getwhoislikesmenewcount(ProfileModel model, IUnitOfWork db)
        {




            try
            {

                int? count = null;
                int defaultvalue = 0;


                //filter out blocked profiles 
                var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == model.profileid && p.removedate != null)
                                     select new
                                     {
                                         ProfilesBlockedId = c.blockprofile_id
                                     };

                count = (
                   from p in db.GetRepository<like>().Find()
                   where (p.likeprofile_id == model.profileid && p.viewdate == null)
                   join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                   where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                   select f).Count();

                // ?? operator example.


                // y = x, unless x is null, in which case y = -1.
                defaultvalue = count ?? 0;

                return defaultvalue;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




        #endregion

    }

}