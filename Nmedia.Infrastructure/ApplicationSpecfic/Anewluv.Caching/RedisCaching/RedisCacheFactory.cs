using Anewluv.Domain.Data;
using GeoData.Domain.Models;
using GeoData.Domain.Models.ViewModels;
using Nmedia.Infrastructure.DTOs;
using Nmedia.Infrastructure.Helpers;
using Repository.Pattern.UnitOfWork;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Anewluv.Caching.RedisCaching
{
    

    public class RedisCacheFactory
    {
        private static ConnectionMultiplexer _connection;
        /// </summary>
        private static IDatabase GetCache()
        {
            try
            {
                if (_connection == null ||_connection.IsConnected == false)
                    _connection = ConnectionMultiplexer.Connect("olawaltest.redis.cache.windows.net,ssl=true,password=Cn86u9582SQqoMhZwQmwJutG+5Mptmp5x0b8drivILU=,ConnectTimeout=10000");

                IDatabase cache = _connection.GetDatabase();

                return cache;
            }
            catch (Exception ex)
            { 
            //TO DO log
                return null;
            
            }
        }

        



        public static class SharedObjectHelper
        {

            /// <summary>
            /// This class helps us determine what pages use what styles etc - comes from a appsettings file
            /// </summary>
            public static class CssStyleSelector
            {

                public static string getbodycssbypagename(string pagename, IUnitOfWorkAsync context)
                {


                    IDatabase dataCache;
                    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                    //dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();
                    dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                    //TO DO put in cache or something ? or return from shared object deal
                    string CssSyle = "StandardWhiteBackground";  //default
                    List<systempagesetting> pages = null;


                    //if we still have no datacahe do tis
                    try
                    {

                        if (dataCache != null) pages = dataCache.Get("SystemPageSettingsList") as List<systempagesetting>;


                        if (pages == null)
                        {
                            //context context = new context();
                            //remafill the ages list from the repositry and exit
                            // Ages =  context.Repository<AgesSelectList;
                            // Datings context = new modelContext();
                            // model =  context.Repository<models.Single(c => c.Id == id);

                            //if we still have no datacahe no need to do the put
                            // if (dataCache == null) return Ages;
                            //pages =  context.systempagesettings.Where(p => p.bodycssstylename != "").ToList();
                            if (dataCache != null)
                                dataCache.Set("SystemPageSettingsList", pages);
                        }


                        //TO DO 
                        //finde the matchv

                        var results = from item in pages
                                      where (item.title == pagename.Trim())
                                      select item;

                        //return the default white background if none found
                        if (results.Count() == 0) return CssSyle;

                        //else return the value from cache or database
                        return results.FirstOrDefault().bodycssstylename.Trim();
                    }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                        //Log error
                        //logger = new Logging(applicationEnum.AppfabricCaching);
                        // logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        //throw new InvalidOperationException();

                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }

                    return CssSyle;

                }

                public static List<systempagesetting> getsystempagesettingslist(IUnitOfWorkAsync context)
                {


                    IDatabase dataCache;
                    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                    //dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();
                    dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                    //TO DO put in cache or something ? or return from shared object deal

                    List<systempagesetting> pages = null;


                    //if we still have no datacahe do tis
                    try
                    {

                        if (dataCache != null) pages = dataCache.Get("SystemPageSettingsList") as List<systempagesetting>;


                        if (pages == null)
                        {
                            //context context = new context();
                            //remafill the ages list from the repositry and exit
                            // Ages =  context.Repository<AgesSelectList;
                            // Datings context = new modelContext();
                            // model =  context.Repository<models.Single(c => c.Id == id);

                            //if we still have no datacahe no need to do the put
                            // if (dataCache == null) return Ages;
                            pages = context.Repository<systempagesetting>().Queryable().Where(p => p.bodycssstylename != "").ToList();
                            if (dataCache != null)
                                dataCache.Set("SystemPageSettingsList", pages);
                        }


                        return pages;
                    }              
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                        //Log error
                        //logger = new Logging(applicationEnum.AppfabricCaching);
                       // logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                        //throw new InvalidOperationException();

                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }

                    return pages;



                }
            }

            //Photo based functions 
            public static List<lu_photoformat> getphotoformatlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
               dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<lu_photoformat> photoformat = null;
                try
                {

                    try { if (dataCache != null) photoformat = dataCache.Get<List<lu_photoformat>>("photoformatlist"); } //as List<lu_photoformat>; }

                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photoformat == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoformat = context.Repository<lu_photoformat>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Set("photoformatlist", photoformat);

                    } return photoformat;
                }               
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }

                return photoformat;
            }
            public static List<listitem> getphotoapprovalstatuslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoapprovalstatus = null;
                try
                {

                    try { if (dataCache != null) photoapprovalstatus = dataCache.Get("photoapprovalstatuslist") as List<listitem>; }
                   
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photoapprovalstatus == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoapprovalstatus = (from o in context.Repository<lu_photoapprovalstatus>().Queryable().ToList()
                                               select new listitem
                                               {
                                                   id = o.id,
                                                   description = o.description,
                                                   selected = false
                                               }).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photoapprovalstatuslist", photoapprovalstatus);

                    } return photoapprovalstatus;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }

                return photoapprovalstatus;

            }
            public static List<lu_photorejectionreason> getphotorejectionreasonlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<lu_photorejectionreason> photorejectionreason = null;
                try
                {

                    try { if (dataCache != null) photorejectionreason = dataCache.Get("photorejectionreasonlist") as List<lu_photorejectionreason>; }                   
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photorejectionreason == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photorejectionreason = context.Repository<lu_photorejectionreason>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photorejectionreasonlist", photorejectionreason);

                    } return photorejectionreason;
                }              
               
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }

                return photorejectionreason;

            }
            public static List<listitem> getphotostatuslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> photostatus = null;
                try
                {

                    try { if (dataCache != null) photostatus = dataCache.Get("photostatuslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photostatus == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photostatus = (from o in context.Repository<lu_photostatus>().Queryable().ToList()
                                       select new listitem
                                       {
                                           id = o.id,
                                           description = o.description,
                                           selected = false
                                       }).ToList();


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photostatuslist", photostatus);

                    } return photostatus;
                }               
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return photostatus;
            }
            public static List<listitem> getphotoimagetypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoimagetype = null;
                try
                {

                    try { if (dataCache != null) photoimagetype = dataCache.Get("photoimagetypelist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photoimagetype == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoimagetype = (from o in context.Repository<lu_photoimagetype>().Queryable().ToList()
                                          select new listitem
                                          {
                                              id = o.id,
                                              description = o.description,
                                              selected = false
                                          }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photoimagetypelist", photoimagetype);

                    } return photoimagetype;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return photoimagetype;
            }


            public static List<listitem> getphotosecurityleveltypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> getphotosecurityleveltypelist = null;
                try
                {

                    try { if (dataCache != null) getphotosecurityleveltypelist = dataCache.Get("getphotosecurityleveltypelist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (getphotosecurityleveltypelist == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        getphotosecurityleveltypelist = (from o in context.Repository<lu_securityleveltype>().Queryable().ToList()
                                                         select new listitem
                                                         {
                                                             id = o.id,
                                                             description = o.description,
                                                             selected = false
                                                         }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("getphotosecurityleveltypelist", getphotosecurityleveltypelist);

                    } return getphotosecurityleveltypelist;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return getphotosecurityleveltypelist;
            }
            //olawal 3-13-2013 other functions added after fact
            public static List<lu_photostatusdescription> getphotostatusdescriptionlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<lu_photostatusdescription> photostatusdescription = null;
                try
                {

                    try { if (dataCache != null) photostatusdescription = dataCache.Get("photostatusdescriptionlist") as List<lu_photostatusdescription>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photostatusdescription == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photostatusdescription = context.Repository<lu_photostatusdescription>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photostatusdescriptionlist", photostatusdescription);

                    } 
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return photostatusdescription;
            }
            public static List<listitem> getabusetypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> abusetype = null;
                try
                {

                    try { if (dataCache != null) abusetype = dataCache.Get("abusetypelist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (abusetype == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        abusetype = (from o in context.Repository<lu_abusetype>().Queryable().ToList()
                                     select new listitem
                                     {
                                         id = o.id,
                                         description = o.description,
                                         selected = false
                                     }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("abusetypelist", abusetype);

                    } return abusetype;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return abusetype;
            }
            public static List<listitem> getprofilestatuslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> profilestatus = null;
                try
                {

                    try { if (dataCache != null) profilestatus = dataCache.Get("profilestatuslist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (profilestatus == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        profilestatus = (from o in context.Repository<lu_profilestatus>().Queryable().ToList()
                                         select new listitem
                                         {
                                             id = o.id,
                                             description = o.description,
                                             selected = false
                                         }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("profilestatuslist", profilestatus);

                    } return profilestatus;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return profilestatus;
            }
            public static List<listitem> getphotoImagersizerformatlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoImagersizerformat = null;
                try
                {

                    try { if (dataCache != null) photoImagersizerformat = dataCache.Get("getphotoImagersizerformat") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (photoImagersizerformat == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoImagersizerformat = (from o in context.Repository<lu_photoImagersizerformat>().Queryable().ToList()
                                                  select new listitem
                                                  {
                                                      id = o.id,
                                                      description = o.description,
                                                      selected = false
                                                  }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("photoImagersizerformatlist", photoImagersizerformat);

                    } return photoImagersizerformat;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return photoImagersizerformat;
            }
            public static List<lu_role> getrolelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<lu_role> role = null;
                try
                {

                    try { if (dataCache != null) role = dataCache.Get("rolelist") as List<lu_role>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (role == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        role = context.Repository<lu_role>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("rolelist", role);

                    } return role;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return role;
            }
            public static List<listitem> getsecurityleveltypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> securityleveltype = null;
                try
                {

                    try { if (dataCache != null) securityleveltype = dataCache.Get("securityleveltypelist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (securityleveltype == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        securityleveltype = (from o in context.Repository<lu_securityleveltype>().Queryable().ToList()
                                             select new listitem
                                             {
                                                 id = o.id,
                                                 description = o.description,
                                                 selected = false
                                             }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("securityleveltypelist", securityleveltype);

                    } return securityleveltype;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return securityleveltype;
            }
            public static List<listitem> getshowmelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> showme = null;
                try
                {

                    try { if (dataCache != null) showme = dataCache.Get("showmelist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       //// throw;
                    }

                    if (showme == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        showme = (from o in context.Repository<lu_showme>().Queryable().ToList()
                                  select new listitem
                                  {
                                      id = o.id,
                                      description = o.description,
                                      selected = false
                                  }).ToList();


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("showmelist", showme);

                    } return showme;
                }
                catch (RedisCommandException ex)
                {
                   //// throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    //throw;
                }
                return showme;

            }
            public static List<listitem> getsortbytypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> sortbytype = null;
                try
                {

                    try { if (dataCache != null) sortbytype = dataCache.Get("sortbytypelist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (sortbytype == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        sortbytype = (from o in context.Repository<lu_sortbytype>().Queryable().ToList()
                                      select new listitem
                                      {
                                          id = o.id,
                                          description = o.description,
                                          selected = false
                                      }).ToList();


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("sortbytypelist", sortbytype);

                    } return sortbytype;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return sortbytype;
            }
            public static List<listitem> getsecurityquestionlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> securityquestion = null;
                try
                {

                    try { if (dataCache != null) securityquestion = dataCache.Get("securityquestionlist") as List<listitem>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (securityquestion == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        securityquestion = (from o in context.Repository<lu_securityquestion>().Queryable().ToList()
                                            select new listitem
                                            {
                                                id = o.id,
                                                description = o.description,
                                                selected = false
                                            }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("securityquestionlist", securityquestion);

                    } return securityquestion;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return securityquestion;
            }
            public static List<lu_flagyesno> getflagyesnolist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<lu_flagyesno> flagyesno = null;
                try
                {

                    try { if (dataCache != null) flagyesno = dataCache.Get("flagyesnolist") as List<lu_flagyesno>; }
                  catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (flagyesno == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        flagyesno = context.Repository<lu_flagyesno>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("flagyesnolist", flagyesno);

                    } return flagyesno;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return flagyesno;
            }
            public static List<listitem> getprofilefiltertypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> profilefiltertype = null;
                try
                {

                    try { if (dataCache != null) profilefiltertype = dataCache.Get("profilefiltertypelist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }

                    if (profilefiltertype == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        profilefiltertype = (from o in context.Repository<lu_profilefiltertype>().Queryable().ToList()
                                             select new listitem
                                             {
                                                 id = o.id,
                                                 description = o.description,
                                                 selected = false
                                             }).ToList();


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                           dataCache.Set("profilefiltertypelist", profilefiltertype);

                    } return profilefiltertype;
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return profilefiltertype;
            }


            //generic functions
            public static List<listitem> getgenderlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> genders = null;

                try
                {
                    try { if (dataCache != null) genders = dataCache.Get("genderlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (genders == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        genders = (from o in context.Repository<lu_gender>().Queryable().ToList()
                                   select new listitem
                                   {
                                       id = o.id,
                                       description = o.description,
                                       selected = false
                                   }).ToList();

                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Set("genderlist", genders);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return genders;
            }
            public static List<age> getagelist()
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<age> ageslist = null;


                try {
                //if we still have no datacahe do tis
                try { if (dataCache != null) ageslist = dataCache.Get("agelist") as List<age>; }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                if (ageslist == null)
                {
                    ageslist = generatedlists.ageslist();
                    //if we still have no datacahe no need to do the put
                    if (dataCache != null)
                       dataCache.Set("agelist", ageslist);

                }}
                 catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                
                return ageslist;
            }
            public static List<metricheight> getmetricheightlist()
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<metricheight> heights = null;
                try{
                try { if (dataCache != null) heights = dataCache.Get("metricheightlist") as List<metricheight>; }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                if (heights == null)
                {
                    // context context = new context();
                    //remafill the ages list from the repositry and exit
                    heights = generatedlists.metricheights();
                    // Datings context = new modelContext();
                    // model =  context.Repository<models.Single(c => c.Id == id);
                    if (dataCache != null)
                       dataCache.Set("metricheightlist", heights);

                } }
                 catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return heights;
            }


            #region "Creiteria Apperance lists cached here"

            public static List<listitem> getbodytypelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();
                List<listitem> bodytype = null;
                try
                {
                    try { if (dataCache != null) bodytype = dataCache.Get("bodytypelist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (bodytype == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        bodytype = (from o in context.Repository<lu_bodytype>().Queryable().ToList()
                                    select new listitem
                                    {
                                        id = o.id,
                                        description = o.description,
                                        selected = false
                                    }).ToList();

                        //put this into the cache since it was not in here already 
                        if (dataCache != null)
                            dataCache.Set("bodytypelist", bodytype);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return bodytype;
            }
            public static List<listitem> getethnicitylist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> ethnicity = null;
                try
                {
                    try { if (dataCache != null) ethnicity = dataCache.Get("ethnicitylist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (ethnicity == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        ethnicity = (from o in context.Repository<lu_ethnicity>().Queryable().ToList()
                                     select new listitem
                                     {
                                         id = o.id,
                                         description = o.description,
                                         selected = false
                                     }).ToList();

                        if (dataCache != null)
                            dataCache.Set("ethnicitylist", ethnicity);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return ethnicity;
            }
            public static List<listitem> geteyecolorlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> eyecolor = null;
                try
                {
                    try { if (dataCache != null) eyecolor = dataCache.Get("eyecolorlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (eyecolor == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        eyecolor = (from o in context.Repository<lu_eyecolor>().Queryable().ToList()
                                    select new listitem
                                    {
                                        id = o.id,
                                        description = o.description,
                                        selected = false
                                    }).ToList();
                        if (dataCache != null) dataCache.Set("eyecolorlist", eyecolor);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return eyecolor;
            }
            public static List<listitem> gethaircolorlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> haircolor = null;
                try
                {
                    try { if (dataCache != null)haircolor = dataCache.Get("haircolorlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (haircolor == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        haircolor = (from o in context.Repository<lu_haircolor>().Queryable().ToList()
                                     select new listitem
                                     {
                                         id = o.id,
                                         description = o.description,
                                         selected = false
                                     }).ToList();

                        if (dataCache != null) dataCache.Set("haircolorlist", haircolor);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return haircolor;
            }
            public static List<listitem> gethotfeaturelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> hotfeature = null;
                try
                {
                    try { if (dataCache != null)hotfeature = dataCache.Get("hotfeaturelist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (hotfeature == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        hotfeature = (from o in context.Repository<lu_hotfeature>().Queryable().ToList()
                                      select new listitem
                                      {
                                          id = o.id,
                                          description = o.description,
                                          selected = false
                                      }).ToList();

                        if (dataCache != null) dataCache.Set("hotfeaturelist", hotfeature);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return hotfeature;
            }

            #endregion

            #region "CriteriaCharacter lists cached here"
            public static List<listitem> getdietlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> diet = null;
                try
                {
                    try { if (dataCache != null) diet = dataCache.Get("dietlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (diet == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        diet = (from o in context.Repository<lu_diet>().Queryable().ToList()
                                select new listitem
                                {
                                    id = o.id,
                                    description = o.description,
                                    selected = false
                                }).ToList();



                        if (dataCache != null) dataCache.Set("dietlist", diet);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return diet;
            }
            public static List<listitem> getdrinkslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> drinks = null;
                try
                {
                    try { if (dataCache != null) drinks = dataCache.Get("drinkslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (drinks == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        drinks = (from o in context.Repository<lu_drinks>().Queryable().ToList()
                                  select new listitem
                                  {
                                      id = o.id,
                                      description = o.description,
                                      selected = false
                                  }).ToList();

                        if (dataCache != null) dataCache.Set("drinkslist", drinks);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return drinks;
            }
            public static List<listitem> getexerciselist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> exercise = null;
                try
                {
                    try { if (dataCache != null) exercise = dataCache.Get("exerciselist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (exercise == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        exercise = (from o in context.Repository<lu_exercise>().Queryable().ToList()
                                    select new listitem
                                    {
                                        id = o.id,
                                        description = o.description,
                                        selected = false
                                    }).ToList();

                        if (dataCache != null) dataCache.Set("exerciselist", exercise);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return exercise;
            }
            public static List<listitem> gethobbylist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> hobby = null;
                try
                {
                    try { if (dataCache != null) hobby = dataCache.Get("hobbylist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (hobby == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        hobby = (from o in context.Repository<lu_hobby>().Queryable().ToList()
                                 select new listitem
                                 {
                                     id = o.id,
                                     description = o.description,
                                     selected = false
                                 }).ToList();//.OrderBy(p=>p.id);

                        //insert value for ANY
                        var anytime = new listitem { id = 0, description = "Any", selected = false };

                        //put it at top
                        hobby.Insert(0, anytime);


                        if (dataCache != null) dataCache.Set("hobbylist", hobby);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return hobby;
            }
            public static List<listitem> gethumorlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> humor = null;
                try
                {
                    try { if (dataCache != null) humor = dataCache.Get("humorlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (humor == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        humor = (from o in context.Repository<lu_humor>().Queryable().ToList()
                                 select new listitem
                                 {
                                     id = o.id,
                                     description = o.description,
                                     selected = false
                                 }).ToList();

                        if (dataCache != null) dataCache.Set("humorlist", humor);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return humor;
            }
            public static List<listitem> getpoliticalviewlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> politicalview = null;
                try
                {
                    try { if (dataCache != null) politicalview = dataCache.Get("politicalviewlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (politicalview == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        politicalview = (from o in context.Repository<lu_politicalview>().Queryable().ToList()
                                         select new listitem
                                         {
                                             id = o.id,
                                             description = o.description,
                                             selected = false
                                         }).ToList();


                        if (dataCache != null) dataCache.Set("politicalviewlist", politicalview);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return politicalview;
            }
            public static List<listitem> getreligionlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> religion = null;
                try
                {
                    try { if (dataCache != null)  religion = dataCache.Get("religionlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (religion == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        religion = (from o in context.Repository<lu_religion>().Queryable().ToList()
                                    select new listitem
                                    {
                                        id = o.id,
                                        description = o.description,
                                        selected = false
                                    }).ToList();


                        if (dataCache != null) dataCache.Set("religionlist", religion);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return religion;
            }
            public static List<listitem> getreligiousattendancelist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> religiousattendance = null;
                try
                {
                    try { if (dataCache != null) religiousattendance = dataCache.Get("religiousattendancelist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (religiousattendance == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        religiousattendance = (from o in context.Repository<lu_religiousattendance>().Queryable().ToList()
                                               select new listitem
                                               {
                                                   id = o.id,
                                                   description = o.description,
                                                   selected = false
                                               }).ToList();


                        if (dataCache != null) dataCache.Set("religiousattendancelist", religiousattendance);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return religiousattendance;
            }
            public static List<listitem> getsignlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> sign = null;
                try
                {
                    try { if (dataCache != null) sign = dataCache.Get("signlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (sign == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        sign = (from o in context.Repository<lu_sign>().Queryable().ToList()
                                select new listitem
                                {
                                    id = o.id,
                                    description = o.description,
                                    selected = false
                                }).ToList();

                        if (dataCache != null) dataCache.Set("signlist", sign);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return sign;
            }
            public static List<listitem> getsmokeslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> smokes = null;
                try
                {
                    try { if (dataCache != null)  smokes = dataCache.Get("smokeslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (smokes == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        smokes = (from o in context.Repository<lu_smokes>().Queryable().ToList()
                                  select new listitem
                                  {
                                      id = o.id,
                                      description = o.description,
                                      selected = false
                                  }).ToList();

                        if (dataCache != null) dataCache.Set("smokeslist", smokes);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return smokes;
            }

            #endregion

            #region "Criteria Lifestyle lists cached here"

            public static List<listitem> geteducationlevellist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> educationlevel = null;
                try
                {
                    try { if (dataCache != null) educationlevel = dataCache.Get("educationlevellist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (educationlevel == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        educationlevel = (from o in context.Repository<lu_educationlevel>().Queryable().ToList()
                                          select new listitem
                                          {
                                              id = o.id,
                                              description = o.description,
                                              selected = false
                                          }).ToList();

                        if (dataCache != null) dataCache.Set("educationlevellist", educationlevel);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return educationlevel;
            }
            public static List<listitem> getemploymentstatuslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> employmentstatus = null;
                try
                {
                    try { if (dataCache != null) employmentstatus = dataCache.Get("employmentstatuslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (employmentstatus == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        employmentstatus = (from o in context.Repository<lu_employmentstatus>().Queryable().ToList()
                                            select new listitem
                                            {
                                                id = o.id,
                                                description = o.description,
                                                selected = false
                                            }).ToList();

                        if (dataCache != null) dataCache.Set("employmentstatuslist", employmentstatus);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                
                return employmentstatus;
            }
            public static List<listitem> gethavekidslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> havekids = null;
                try
                {
                    try { if (dataCache != null) havekids = dataCache.Get("havekidslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (havekids == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        havekids =
                            (from o in context.Repository<lu_havekids>().Queryable().ToList()
                             select new listitem
                             {
                                 id = o.id,
                                 description = o.description,
                                 selected = false
                             }).ToList();

                        if (dataCache != null) dataCache.Set("havekidslist", havekids);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return havekids;
            }
            public static List<listitem> getincomelevellist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> incomelevel = null;
                try
                {
                    try { if (dataCache != null) incomelevel = dataCache.Get("incomelevellist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (incomelevel == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        incomelevel = (from o in context.Repository<lu_incomelevel>().Queryable().ToList()
                                       select new listitem
                                       {
                                           id = o.id,
                                           description = o.description,
                                           selected = false
                                       }).ToList();

                        if (dataCache != null) dataCache.Set("incomelevellist", incomelevel);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return incomelevel;
            }
            public static List<listitem> getlivingsituationlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> livingsituation = null;
                try
                {
                    try { if (dataCache != null) livingsituation = dataCache.Get("livingsituationlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (livingsituation == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        livingsituation = (from o in context.Repository<lu_livingsituation>().Queryable().ToList()
                                           select new listitem
                                           {
                                               id = o.id,
                                               description = o.description,
                                               selected = false
                                           }).ToList();

                        if (dataCache != null) dataCache.Set("livingsituationlist", livingsituation);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return livingsituation;
            }
            public static List<listitem> getlookingforlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> lookingfor = null;
                try
                {
                    try { if (dataCache != null) lookingfor = dataCache.Get("lookingforlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (lookingfor == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        lookingfor = (from o in context.Repository<lu_lookingfor>().Queryable().ToList()
                                      select new listitem
                                      {
                                          id = o.id,
                                          description = o.description,
                                          selected = false
                                      }).ToList();


                        if (dataCache != null) dataCache.Set("lookingforlist", lookingfor);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return lookingfor;
            }
            public static List<listitem> getmaritalstatuslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> maritalstatus = null;
                try
                {
                    try { if (dataCache != null) maritalstatus = dataCache.Get("maritalstatuslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (maritalstatus == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        maritalstatus = (from o in context.Repository<lu_maritalstatus>().Queryable().ToList()
                                         select new listitem
                                         {
                                             id = o.id,
                                             description = o.description,
                                             selected = false
                                         }).ToList();


                        if (dataCache != null) dataCache.Set("maritalstatuslist", maritalstatus);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return maritalstatus;
            }
            public static List<listitem> getprofessionlist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> profession = null;
                try
                {
                    try { if (dataCache != null) profession = dataCache.Get("professionlist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (profession == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        profession = (from o in context.Repository<lu_profession>().Queryable().ToList()
                                      select new listitem
                                      {
                                          id = o.id,
                                          description = o.description,
                                          selected = false
                                      }).ToList();

                        if (dataCache != null) dataCache.Set("professionlist", profession);

                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return profession;
            }
            public static List<listitem> getwantskidslist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<listitem> wantskids = null;
                try
                {
                    try { if (dataCache != null) wantskids = dataCache.Get("wantskidslist") as List<listitem>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (wantskids == null)
                    {
                        //context context = new context();
                        //remafill the ages list from the repositry and exit
                        wantskids = (from o in context.Repository<lu_wantskids>().Queryable().ToList()
                                     select new listitem
                                     {
                                         id = o.id,
                                         description = o.description,
                                         selected = false
                                     }).ToList();


                    }
                }
                catch (RedisCommandException ex)
                {
                    // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    // throw;
                }
                return wantskids;
            }

            #endregion



            #region "Geodata lists"

            public static List<country> getcountrylist(IUnitOfWorkAsync context)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                 List<country> countries = new List<country>();

                try
                {


                    try { if (dataCache != null) countries = dataCache.Get("countrieslist") as List<country>; }
                  catch (RedisCommandException ex)
                    {
                       // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                       // throw;
                    }
                    if (countries.Count() == 0)
                    {
                        // context context = new context();
                        //remafill the Countrys list from the repositry and exit

                        //    List<country> tmplist = new List<country>();
                        // Loop over the int List and modify it.
                        //insert the first one as ANY
                       // countrys.Add(new country { id = "0", name = "Any" });


                        countries.Add(new country { id = "0", name = "Any" });
                        foreach (Country_PostalCode_List item in context.Repository<Country_PostalCode_List>().Query().Select().OrderBy(p => p.CountryName))
                        {
                            var currentcountry = new country { id = item.CountryID.ToString(), name = item.CountryName };
                            countries.Add(currentcountry);
                        }

                        //return countries;


                        //foreach (countrypostalcode item in Api.AsyncCalls.getcountryandpostalcodestatuslistasync().Result)
                        //{
                        //    var currentcountry = new country { id = item.id.ToString(), name = item.name };
                        //    countrys.Add(currentcountry);
                        //}


                        //foreach (Country_PostalCode_List item in _postal context.Repository<GetCountry_PostalCode_List().ToList().OrderBy(p => p.CountryName))
                        //{
                        //    var currentcountry = new country { id = item.CountryID.ToString(), name = item.CountryName  };
                        //    countrys.Add(currentcountry);
                        //}
                        //return tmplist;

                        //if we still have no datacahe do tis

                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);
                        if (dataCache != null)
                            dataCache.Set("countrieslist", countries);

                    } 
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }
                return countries;
            }

            public static List<countrypostalcode> getcountryandpostalcodestatuslist(IGeoDataStoredProcedures _storedProcedures)
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

                List<countrypostalcode> countryandpostalcodes = null;

                try
                {

                    try { if (dataCache != null) countryandpostalcodes = dataCache.Get("countryandpostalcodestatuslist") as List<countrypostalcode>; }
                    catch (RedisCommandException ex)
                    {
                        // throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                        // throw;
                    }
                    if (countryandpostalcodes == null)
                    {
                        var Query = _storedProcedures.GetCountryPostalCodeList();
                       // var Query = _unitOfWorkAsync.Repository<Country_PostalCode_List>().Query(p => p.CountryName != "").Select().ToList().OrderBy(p => p.CountryName);

                        countryandpostalcodes =(from s in Query
                                select new countrypostalcode
                                {
                                    name = s.CountryName,
                                    id = s.CountryID.ToString(),
                                    code = s.Country_Code,
                                    customregionid = s.CountryCustomRegionID,
                                    region = s.Country_Region,
                                    haspostalcode = Convert.ToBoolean(s.PostalCodes)
                                }).ToList();


                    }



                        //   }




                        //countryandpostalcodes = georepository.getcountrylist();

                        //if we still have no datacahe do tis

                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);
                        if (dataCache != null)
                           dataCache.Set("countryandpostalcodestatuslist", countryandpostalcodes);

                    
                }
              catch (RedisCommandException ex)
                {
                   // throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                   // throw;
                }

                return countryandpostalcodes;
            }

            #endregion

            public static bool RemoveAllLists()
            {
               IDatabase dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                 dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();


                //TO DO find a way to just flush the persistant cache
                try
                {

                    dataCache.KeyDelete("agelist");
                    dataCache.KeyDelete("genderlist");
                    dataCache.KeyDelete("countrieslist");
                    dataCache.KeyDelete("securityquestionslist");
                    dataCache.KeyDelete("bodytypeslist");
                    dataCache.KeyDelete("eyecolorlist");
                    dataCache.KeyDelete("haircolorlist");
                    dataCache.KeyDelete("metricheightlist");
                    dataCache.KeyDelete("dietlist");
                    dataCache.KeyDelete("drinkslist");
                    dataCache.KeyDelete("exerciselist");
                    dataCache.KeyDelete("hobbylist");
                    dataCache.KeyDelete("humorlist");
                    dataCache.KeyDelete("religionlist");
                    dataCache.KeyDelete("religiousattendancelist");
                    dataCache.KeyDelete("signlist");
                    dataCache.KeyDelete("smokeslist");
                    dataCache.KeyDelete("educationlevellist");
                    dataCache.KeyDelete("employmentstatuslist");
                    dataCache.KeyDelete("havekidstlist");
                    dataCache.KeyDelete("incomelevellist");
                    dataCache.KeyDelete("livingsituationlist");
                    dataCache.KeyDelete("maritalstatuslist");
                    dataCache.KeyDelete("professionlist");
                    dataCache.KeyDelete("lookingforlist");
                    //  dataCache.Remove("VisibilityMailSettingsList");
                    //  dataCache.Remove("VisibilityStealthSettingsList");
                }

              catch (RedisCommandException ex)
                {
                    return false;
                   // throw new InvalidOperationException();

                }

                return true;



            }

            //these shoul just be regular lookups
            #region "lists for visibily settings that can be used else where"

            //public static List<visiblitysetting> getvisibilitymailsettingslist(IUnitOfWorkAsync context)
            //{
            //   IDatabase dataCache;
            //    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            //     dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

            //    List<visiblitysetting> visibilitymailsettings = null;
            //    try { if (dataCache != null) visibilitymailsettings = dataCache.Get("visibilitymailsettingslist") as List<visiblitysetting>; }
            // catch (RedisCommandException ex)
            //    {
            //       // throw new InvalidOperationException();
            //    }
            //    if (visibilitymailsettings == null)
            //    {
            //        //context context = new context();
            //        //remafill the ages list from the repositry and exit
            //        visibilitymailsettings =  context.Repository<visibilitysettings.Where(p=>p.OrderBy(x => x.description).ToList();
            //        // Datings context = new modelContext();
            //        // model =  context.Repository<models.Single(c => c.Id == id);
            //        if (dataCache != null)
            //           dataCache.Set("visibilitymailsettingslist", visibilitymailsettings);

            //    } return VisibilityMailSettings;
            //}
            //public static List<visiblitysetting> getvisibilitystealthsettingslist()
            //{
            //   IDatabase dataCache;
            //    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            //     dataCache = GetCache();  // dataCacheFactory.GetDefaultCache();

            //    List<SelectListItem> VisibilityStealthSettings = null;
            //    try { if (dataCache != null) VisibilityStealthSettings = dataCache.Get("VisibilityStealthSettingsList") as List<SelectListItem>; }
            // catch (RedisCommandException ex)
            //    {
            //       // throw new InvalidOperationException();
            //    }
            //    if (VisibilityStealthSettings == null)
            //    {
            //        context context = new context();
            //        //remafill the ages list from the repositry and exit
            //        VisibilityStealthSettings =  context.Repository<VisibilityStealthSettingsList;
            //        // Datings context = new modelContext();
            //        // model =  context.Repository<models.Single(c => c.Id == id);
            //        if (dataCache != null)
            //           dataCache.Set("VisibilityStealthSettingsList", VisibilityStealthSettings);

            //    } return VisibilityStealthSettings;
            //}

            #endregion
        }

    }
}
