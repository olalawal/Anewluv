using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationServer.Caching;
using System.Collections;
using System.Threading;

//link to old postal code model
//using Dating.Server.Data.Services;
using LoggingLibrary;



//using Dating.Server.Data.Models;
//using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Domain.Data.log;

using Anewluv.Domain.Data;
using Anewluv.Domain;
using GeoData.Domain.Models.ViewModels;
//using Anewluv.Services.Mapping;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.Models;
//using Anewluv.Services.Spatial;
//using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.ExceptionHandling;
using Nmedia.Infrastructure.Helpers;
using Repository.Pattern.UnitOfWork;
using Nmedia.Infrastructure.DTOs;






namespace Anewluv.Caching
{

    public static class CachingFactory
    {
        private static  Logging logger;
        // public  LoggingServiceClient  svcLoggingService;
        public static DataCacheFactory _cacheFactory;
        private static DataCache _cache;
        private static string _sessionStateCacheName = "AnewLuv";
        private static string _persistantCacheName = "AnewLuvPersistantCache";
        private static string _LeadCacheHost = "NmediaApps01"; // add NemdiaAPPs IP address to hostnames file on production webserver
        static readonly object _locker = new object();

        //public  CachingFactory(LoggingServiceClient _LoggingService)
        //{
        //    IKernel kernel = new StandardKernel();
        //    //Get these initalized
        //    svcLoggingService = kernel.Get<LoggingServiceClient>();

        //}




        #region "Cache Initializers"

        /// <summary>
        /// Summary description for Cache
        /// this cache is used to store session state data so we can use acrcoss mobile etc
        /// i.e its decoupled from the server?
        ///  2-1-2013 olawal : modfied the code to exit gracefully when no cache server is active
        /// most functions require it , but now we still need to notify admin when the cache servers are down 
        /// withoute destorying the app.
        /// </summary>
        private static DataCache GetCache
        {
           

            get
            {

                return null;
#if DISCONECTED
                return null;
#else
                bool lockTaken = false;
        try
            {
                if (_cacheFactory == null)
                {


                    if (Monitor.TryEnter(_locker, 20000))
                    {
                        lockTaken = true;
                        _cacheFactory = new DataCacheFactory();
                        //-------------------------
                        // Configure Cache Client 
                        //-------------------------
                        //Define Array for 1 Cache Host
                        List<DataCacheServerEndpoint> servers = new List<DataCacheServerEndpoint>(1);
                        //Specify Cache Host Details 
                        //  Parameter 1 = host name
                        //  Parameter 2 = cache port number
                        servers.Add(new DataCacheServerEndpoint(_LeadCacheHost, 22233));
                        //Create cache configuration
                        DataCacheFactoryConfiguration configuration = new DataCacheFactoryConfiguration();
                        //Set the cache host(s)
                        configuration.Servers = servers;
                        //Set default properties for local cache (local cache disabled)
                        configuration.LocalCacheProperties = new DataCacheLocalCacheProperties();
                        //Disable tracing to avoid informational/verbose messages on the web page
                        DataCacheClientLogManager.ChangeLogLevel(System.Diagnostics.TraceLevel.Off);
                        //Pass configuration settings to cacheFactory constructor
                        _cacheFactory = new DataCacheFactory(configuration);
                        DataCache cache = null;
                        if (_cacheFactory != null)
                        {
                            cache = _cacheFactory.GetCache(_sessionStateCacheName);
                        }
                        //make sure that any regions needed exist as well
                        return createaewluvregions(cache);
                    }
                   
                }
                else
                {
                    //handle case where cache already exists 
                    return _cacheFactory.GetCache(_sessionStateCacheName);
                }
            }
        catch (DataCacheException ex)
        {
            //TO DO NOTIFY AND LOG!!!
            // log the execption message     
            //throw ex;
        }
        catch (Exception ex)
        {
            //log andmessage the exception moveon  
          throw;
        }
        finally
        {
            if (lockTaken)
            Monitor.Exit(_locker);
        }
        return null;
        }
           
#endif

   }
 
        /// <summary>
        /// 2-1-2013 olawal : modfied the code to exit gracefully when no cache server is active
        /// most functions require it , but now we still need to notify admin when the cache servers are down 
        /// withoute destorying the app.
        /// Persistant cache is never cleared and used for things such as lookup lists 
        /// and persistant user data that has to get wiped manually if it is needed to be removed
        /// </summary>
        private static DataCache GetPersistantCache
        {

            get
            {
                return null;
                //added the code to handle disconnected clients
#if DISCONECTED
                return null;

#else
                bool lockTaken = false;
                DataCache cache = null;
                try                    
                {
                    //makes sure the lock is active if we try and kill it when done
                  

                    if (_cacheFactory == null)
                    {
                       // if (Monitor.TryEnter(_locker, 20000))
                       // {
                            lockTaken =true;
                            _cacheFactory = new DataCacheFactory();
                            //-------------------------
                            // Configure Cache Client 
                            //-------------------------
                            //Define Array for 1 Cache Host
                            List<DataCacheServerEndpoint> servers = new List<DataCacheServerEndpoint>(1);
                            //Specify Cache Host Details 
                            //  Parameter 1 = host name
                            //  Parameter 2 = cache port number
                            servers.Add(new DataCacheServerEndpoint(_LeadCacheHost, 22233));
                            //Create cache configuration
                            DataCacheFactoryConfiguration configuration = new DataCacheFactoryConfiguration();
                            //Set the cache host(s)
                            configuration.Servers = servers;
                            //Set default properties for local cache (local cache disabled)
                            configuration.LocalCacheProperties = new DataCacheLocalCacheProperties();
                            //Disable tracing to avoid informational/verbose messages on the web page
                            DataCacheClientLogManager.ChangeLogLevel(System.Diagnostics.TraceLevel.Off);
                            //Pass configuration settings to cacheFactory constructor
                            _cacheFactory = new DataCacheFactory(configuration);
                            //reset cache values ?
                           
                            if (_cacheFactory != null)
                            {
                                cache = _cacheFactory.GetCache(_persistantCacheName);
                            }
                            //make sure that any regions needed exist as well
                            return createaewluvregions(cache);
                       // }

                    }
                    else
                    {
                        //return the  cache if it already was peristed
                     return  _cacheFactory.GetCache(_persistantCacheName);
                    }
             }
            catch (DataCacheException ex)
            {
                //TO DO NOTIFY AND LOG!!!
                logger = new  Logging(applicationEnum.AppfabricCaching);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                //log cache error
                //log the execption message
            //    throw ;
            }
            catch (Exception ex)
            {
                logger = new  Logging(applicationEnum.AppfabricCaching);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                //log cache error
                // log the execption message
             // throw;
            }
            finally
            {
                if (lockTaken)
                Monitor.Exit(_locker);
            }
                //just needed for the getter accsor
                //;
                return cache;
#endif
            }

        }


        //only creating a region for guests since we want scalablituy for members
        //named caches in version 1.0 are limited to cache servers cannot be on clusters
        public static DataCache createaewluvregions(DataCache NamedCache)
        {
            // Always test if region exists;
            try
            {
                NamedCache.CreateRegion("Guests");
            }
            catch (DataCacheException dcex)
            {
                // if region already exists it's ok, 
                // otherwise rethrow the exception
                //12-27-2012 olawal not throwingg the error anymore justneedto logand send theemail
                //TO DO log error andsend email toadmin
                if (dcex.ErrorCode != DataCacheErrorCode.RegionAlreadyExists)
                    return null;
                throw ;
            }
            return NamedCache;

        }

        public static bool clearcurrentsessioncache(string _ProfileID, string sessionid)
        {
            try
            {
                //clear out guest data
                MembersViewModelHelper.removeguestdata(sessionid);
                //ProfileBrowseModelsHelper.removeguestresults(context);
                //clear out member data if member is authenticated
                //TO DO worry about maybe to deleted this to wonder about peristing it?
                //Due to speed considerations

                if (_ProfileID != null)
                {
                    //   MembersViewModelHelper.RemoveMemberData(_ProfileID);
                    //   ProfileBrowseModelsHelper.RemoveMemberResults(_ProfileID);
                }
            }
            catch (Exception ex)
            {

                logger = new  Logging(applicationEnum.AppfabricCaching);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                throw;
            }

            return true;

        }
        
        #endregion

        #region "Save and Retreival of Common strings

        //TO do dpreciate this and test using the Session ID as in below
        //TO Do combine both these functions to rturn a string array of profileID and screenName
        //sowe can pick?
        public static int? getprofileidbyusername(string username, IUnitOfWorkAsync context)
        {

            try
            {
                //handle case of null username
                if (username == null || username == "") return null;

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
                //create the guests region if it does not exists


                int? _profileid = null;
                try { if (dataCache != null)  _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + username)); }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                if (_profileid == null | _profileid == 0)
                {
                    //MembersRepository membersrepository = new MembersRepository();
                    //get the correct value from DB
                    var profile = context.Repository<profile>().Queryable().Where(p => p.username == username).FirstOrDefault();
                    _profileid = (profile != null) ? profile.id : 0;
                    //if we have an active cache we store the current value 
                    if (dataCache != null && _profileid != 0)
                    {
                        //store it in the database
                        if (_profileid != null) dataCache.Put("ProfileID" + username, _profileid);
                       //  }
                       // else if (_profileid != 0)
                       //  {  //just return the value if no cahce
                       //      return _profileid;
                    }


                }
                //value came from cahce return it
                return _profileid != 0 ? _profileid : null;
            }
           catch (DataCacheException ex)
            {
                logger = new  Logging(applicationEnum.AppfabricCaching);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);               
                throw new InvalidOperationException();

            }
            catch (Exception ex)
            {
                logger = new  Logging(applicationEnum.AppfabricCaching);
                logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);   
                //put cleanup code here
                throw ;
            }

        }
        //TO do dpreciate this and test using the Session ID as in below
        public static int? getprofileidbyscreenname(string screenname, IUnitOfWorkAsync context)
        {

            try
            {
                //handle case of null screenname
                if (screenname == null || screenname == "") return null;

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
                //create the guests region if it does not exists


                int? _profileid = null;
                try { if (dataCache != null)  _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + screenname)); }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                if (_profileid == null | _profileid == 0)
                {
                    //MembersRepository membersrepository = new MembersRepository();
                    //get the correct value from DB
                     var profile = context.Repository<profile>().Queryable().Where(p => p.screenname == screenname).FirstOrDefault();
                    _profileid = (profile != null)? profile.id : 0  ;

                    //if we have an active cache we store the current value 
                    if (dataCache != null && _profileid != 0  )
                    {
                        //store it in the database
                        if (_profileid != null) dataCache.Put("ProfileID" + screenname, _profileid);
                    }
                 //   else if (_profileid != 0  )
                   // {  //just return the value if no cahce
                 //       return _profileid;
//
                 //   }


                }
                //value came from cahce return it
                return _profileid !=0? _profileid:null;
            }
           catch (DataCacheException ex)
            {
                throw new InvalidOperationException();

            }
            catch (Exception ex)
            {
                //put cleanup code here
              throw;
            }
        }

        //TO do dpreciate this and test using the Session ID as in below
        public static int? getprofileidbyopenid(ProfileModel model, IUnitOfWorkAsync context)
        {

            try
            {
                //handle case of null screenname
                if (model.email == null || model.email == "") return null;

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
                //create the guests region if it does not exists


                int? _profileid = null;
                try { if (dataCache != null)  _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + model.email + model.openidprovider)); }
                catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                if (_profileid == null | _profileid == 0)
                {
                    //MembersRepository membersrepository = new MembersRepository();
                    //get the correct value from DB
                    var profile = context.Repository<profile>().Queryable().Where(p => p.emailaddress  == model.email).FirstOrDefault();
                    _profileid = (profile != null) ? profile.id : 0;

                    //if we have an active cache we store the current value 
                    if (dataCache != null && _profileid != 0)
                    {
                        //store it in the database
                        if (_profileid != null) dataCache.Put("ProfileID" + model.email + model.openidprovider, _profileid);
                    }
                    //   else if (_profileid != 0  )
                    // {  //just return the value if no cahce
                    //       return _profileid;
                    //
                    //   }


                }
                //value came from cahce return it
                return _profileid != 0 ? _profileid : null;
            }
            catch (DataCacheException ex)
            {
                throw new InvalidOperationException();

            }
            catch (Exception ex)
            {
                //put cleanup code here
                throw;
            }
        }
        //TO do dpreciate this and test using the Session ID as in below
        public static int? getprofileidbysessionid(string sessionid)
        {
            try
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
                //create the guests region if it does not exists


                int? _profileid = null;
                try { _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + sessionid)); }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                if (_profileid == null)
                {
                    return null;

                }

                return _profileid;
            }
           catch (DataCacheException ex)
            {
                throw new InvalidOperationException();

            }
            catch (Exception ex)
            {
                //put cleanup code here
                throw ;
            }

        }


        #endregion


        //Converted to use AppFabric
        public static class MembersViewModelHelper
        {

            public static MembersViewModel getmemberdata(int profileid)
            {
                

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();



                MembersViewModel model = new MembersViewModel();               
                //var mm = MembersMapperRepository();

                try
                {

                    if (dataCache != null) model = dataCache.Get("membersviewmodel" + profileid) as MembersViewModel;

                    //TO do check if the model has changed since the last time it was loaded 
                    //create a method in members repo that checks to see if something has been updated since last activity?
                    if (model.profile_id == null | model.profile_id == 0)
                    {
                        if (dataCache != null)
                        {
                            //remap the user data if cache is empty
                            

                             AnewluvContext AnewluvContext  = new AnewluvContext();
                             using (var tempdb = AnewluvContext)
                             {
                                // MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                               //  model = MemberMapperService.mapmember(profileid.ToString());
                             }
                            // Datings context = new modelContext();
                            // model =  context.Repository<models.Single(c => c.Id == id);
                            dataCache.Put("membersviewmodel" + profileid, model);
                        }
                        else
                        {
                            //TO DO map to gest i think
                            

                         AnewluvContext AnewluvContext  = new AnewluvContext();
                         using (var tempdb = AnewluvContext)
                         {
                            // MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                           //  model = MemberMapperService.mapmember(profileid.ToString());
                         }
                                                    //model =Api.MemberMapperService.mapguest();

                        }
                    } return model;
                }
                catch (DataCacheException ex)
                {
                    //instantiate logger here so it does not break anything else.
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null,false);
                    // throw new InvalidOperationException();
                    throw new CustomExceptionTypes.CacheingException("A problem occured accessing the Appfabric Cache", model);


                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null);
                    //put cleanup code here
                    throw;
                }
                finally
                {
                   // Api.DisposeMemberMapperService();
                }
            }
            //Items for guests are just dumped in the session area not the members region
            //and they are id'd by session ID
            public static MembersViewModel getguestdata(string sessionid)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                MembersViewModel model = null;
                try { 
                    if (dataCache != null)  model = dataCache.Get("membersviewmodel" +sessionid, "Guests") as MembersViewModel;

                    //generate your fictional exception
                    //int x = 1;
                    //int y = 0;
                    //int z = x / y;


                    if (model.profile_id == null | model.profile_id == 0)
                    {
                        //not sure what needs to be initialized for guests , if anything initlize it here
                        // map the registration thing since 
                        // GuestRepository guestrepo = new GuestRepository();
                        // model = guestrepo.MapGuest();
                        //also map the guest members viewmodel
                       // var mm = new ViewModelMapper();
                       

                         AnewluvContext AnewluvContext  = new AnewluvContext();
                         using (var tempdb = AnewluvContext)
                         {
                            // MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                           //  model = MemberMapperService.mapguest();
                         }

                        //poulate the model from the view model mapper
                       // ViewModelMapper Mapper = new ViewModelMapper();
#if DEBUG
                                                Console.WriteLine("Debug version");
                        

                      AnewluvContext  = new AnewluvContext();
                     using (var tempdb = AnewluvContext)
                     {
                       //  MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                      //   model.register = MemberMapperService.getregistermodeltest();
                     }
                                               //model.Register = Mapper.MapRegistration();
#else
                        model = Api.MemberMapperService.mapmember (model.profile_id);
#endif

                        if (dataCache != null)
                        {
                            //store the model if null
                            dataCache.Put("membersviewmodel" +sessionid, model, "Guests");

                        }
                    } return model;

                }
                // try { if (dataCache != null)  model = dataCache.Get("membersviewmodel" +  context.Repository<Session.SessionID, "Guests") as MembersViewModel; }

               catch (DataCacheException ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new CustomExceptionTypes.CacheingException("A problem occured accessing the Appfabric Cache", model);
                    //log the datachae type of excpetion here and mark it handled and logged
                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    var message = String.Format("Something went wrong with the cache method GetGuestData with this sessionid :  {0}", sessionid );
                    throw new CustomExceptionTypes.AccountException(model, message, ex);
 
                   // throw new Exception(message, ex); 
                   
                }
               


            }

            //11-1-2011 added this so we can retive profileID that was was not tied to a sign in action
            //TO do proabbaly we should use this instead going forward since it works both ways
            public static bool saveprofileidbysessionid(string ProfileID, HttpContextBase context)
            {


                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();


                if (ProfileID == null)
                {
                    return false;
                }

                //handle cases were we are just updating bits for the viewmodel. QuickSearch and Account right now would be what is 
                //being updated from what
                //if city is empty we probbaly have empty data so reslove it 
                // oldMembersViewModel.MyQuickSearch = (p.MyQuickSearch.MySelectedCity != null) ? p.MyQuickSearch : oldMembersViewModel.MyQuickSearch;
                //check if account model is empty as well if it is refresh it
                //  oldMembersViewModel.Account = (p.Account.BirthDate  != null) ? p.Account  : oldMembersViewModel.Account;

                dataCache.Put("ProfileID" +  context.Session.SessionID, ProfileID);

                return true;

            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            public static MembersViewModel updatememberdata(MembersViewModel p)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                try
                {
                    if (dataCache != null)

                        //generate your fictional exception
                        //int x = 1;
                        //int y = 0;
                        //  MembersViewModel oldMembersViewModel = new MembersViewModel();
                        //  try { oldMembersViewModel = dataCache.Get("membersviewmodel" + _ProfileID) as MembersViewModel; }
                        //catch (DataCacheException)
                        //{
                        //    //Log error
                        //    //throw new InvalidOperationException();
                        //    return null;
                        //}
                        if (p == null && (p.profile.id != null | p.profile == null))
                        {

                            //remap the user data if cache is empty
                            //var mm = new ViewModelMapper();
                            

                         AnewluvContext AnewluvContext  = new AnewluvContext();
                         using (var tempdb = AnewluvContext)
                         {
                            // MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                            // p = MemberMapperService.mapmember(p.profile.id.ToString());
                         }

                        }
                        else if (p != null && (p.profile.id != null | p.profile == null ))
                        {
                            dataCache.Put("membersviewmodel" + p.profile.id, p);
                        }

                    //handle cases were we are just updating bits for the viewmodel. QuickSearch and Account right now would be what is 
                    //being updated from what
                    //if city is empty we probbaly have empty data so reslove it 
                    // oldMembersViewModel.MyQuickSearch = (p.MyQuickSearch.MySelectedCity != null) ? p.MyQuickSearch : oldMembersViewModel.MyQuickSearch;
                    //check if account model is empty as well if it is refresh it
                    //  oldMembersViewModel.Account = (p.Account.BirthDate  != null) ? p.Account  : oldMembersViewModel.Account;

                    
                }
               catch (DataCacheException ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new CustomExceptionTypes.CacheingException("A problem occured accessing the Appfabric Cache", p);
                    //log the datachae type of excpetion here and mark it handled and logged
                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    var message = String.Format("Something went wrong with the cache method UpdateMemberData with this object or user :  {0}", p.profile.id);
                    throw new CustomExceptionTypes.AccountException(p, message, ex);

                    // throw new Exception(message, ex); 

                }
                
                return p;




            }

            //updates the model in catche with any new data i.e after a save
            //TO DO verify that the P contians profile ID
            public static MembersViewModel updatememberprofiledatabyprofile(int profileid)
            {
                MembersViewModel model = new MembersViewModel();
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                //get the current prodile data

                

                 AnewluvContext AnewluvContext  = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                    // MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                   //  model = MemberMapperService.mapmember(profileid.ToString());
                 }
                model.profiledata = model.profile.profiledata ;

                //  MembersViewModel oldMembersViewModel = new MembersViewModel();
                //  try { oldMembersViewModel = dataCache.Get("membersviewmodel" + _ProfileID) as MembersViewModel; }
                //catch (DataCacheException)
                //{
                //    //Log error
                //    //throw new InvalidOperationException();
                //    return null;
                //}


                //handle cases were we are just updating bits for the viewmodel. QuickSearch and Account right now would be what is 
                //being updated from what
                //if city is empty we probbaly have empty data so reslove it 
                // oldMembersViewModel.MyQuickSearch = (p.MyQuickSearch.MySelectedCity != null) ? p.MyQuickSearch : oldMembersViewModel.MyQuickSearch;
                //check if account model is empty as well if it is refresh it
                //  oldMembersViewModel.Account = (p.Account.BirthDate  != null) ? p.Account  : oldMembersViewModel.Account;

                dataCache.Put("membersviewmodel" + model.profile.id, model);

                return model;




            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            public static bool updateguestdata(MembersViewModel p)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                try
                {
                    //MembersViewModel oldMembersViewModel = null;
                    // try { oldMembersViewModel = dataCache.Get("membersviewmodel" +  context.Repository<Session.SessionID, "Guests") as MembersViewModel; }
                    //catch (DataCacheException)
                    //{
                    //    //Log error
                    //    //throw new InvalidOperationException();
                    //    return null;
                    //}
                    if (p.profile_id == null | p.profile_id == 0)
                    {
                        //remap the user data if cache is empty
                        // var mm = new ViewModelMapper();
                        //TO DO update map guest to scrape UI data
                        //Mapguest will probbaly get geo data like what country city state thier IP matches and any cookie data we can scrap or facebook data
                        // oldMembersViewModel = mm.MapGuest(); //default values probbaly just for test don't map anything in this case since 
                        //we do not want to populate data yet


                        AnewluvContext AnewluvContext = new AnewluvContext();
                        using (var tempdb = AnewluvContext)
                        {
                          //  MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                         //   p = MemberMapperService.mapguest();
                        }
                    }

                    //now update and save

                    //now data is remapped update it
                    // p.MyQuickSearch = (p.MyQuickSearch != null) ? p.MyQuickSearch : oldMembersViewModel.MyQuickSearch;
                    // p.Register = (p.Register != null) ? p.Register : oldMembersViewModel.Register;
                    //get the profile and profile data and quick matches etc maybe from sproc

                    //oldmodel.Register.RegistrationPhotos = (p.Register.RegistrationPhotos != null) ? p.Register.RegistrationPhotos : oldmodel.Register.RegistrationPhotos;
                    //now update session  
                    //update the oldmodel with the values we are keeping
                    //oldMembersViewModel.MyQuickSearch = p.MyQuickSearch;
                    //oldMembersViewModel.Register = p.Register;

                    // Datings context = new modelContext();
                    // model =  context.Repository<models.Single(c => c.Id == id);
                    dataCache.Put("membersviewmodel" + p.sessionid, p, "Guests");

                    return true;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                   // Api.DisposeMemberMapperService();
                }

                return false;

            }
           
            public static bool removeguestdata(string sessionid)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();


                try { dataCache.Remove("membersviewmodel" + sessionid, "Guests"); }
               catch (DataCacheException ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw;
                }
                return true;



            }

        }
        public static class ProfileBrowseModelsHelper
        {
            public static List<ProfileBrowseModel> getmembercurrentsearchresults(string profileid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                List<ProfileBrowseModel> model = null;
                try { model = dataCache.Get("profilebrowsemodel" + profileid) as List<ProfileBrowseModel>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                  throw;
                }
                if (model.Count == 0)
                {

                    //remap the user data if cache is empty
                    model = new List<ProfileBrowseModel>();
                    //return memberactionsrepository.
                    //No need to put empty data
                    // Datings context = new modelContext();
                    // model =  context.Repository<models.Single(c => c.Id == id);
                    // dataCache.Put("profilebrowsemodel" + ProfileID, model);


                } return model;
            }
            //Items for guests are just dumped in the session area not the members region
            //and they are id'd by session ID
            public static List<ProfileBrowseModel> getguestresults(string sessionid)
            {

                DataCache dataCache;
                dataCache = GetCache;

                List<ProfileBrowseModel> model = null;
                try { model = dataCache.Get("profilebrowsemodel" +sessionid, "Guests") as List<ProfileBrowseModel>; }
               catch (DataCacheException ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //load from DB or something
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }
                if (model == null)
                {


                    //remap the user data if cache is empty
                    model = new List<ProfileBrowseModel>();

                    //No need to put empty data
                    // Datings context = new modelContext();
                    // model =  context.Repository<models.Single(c => c.Id == id);
                    // dataCache.Put("profilebrowsemodel" + ProfileID, model);
                } return model;


            }
            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            //Basically it is a save
            public static bool addmembersearchresults(List<ProfileBrowseModel> p, string profileid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Put("profilebrowsemodel" + profileid, p); }
                catch (DataCacheException ex)
                {

                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //throw new InvalidOperationException();
                    return false;
                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }

                return true;




            }
            public static bool addguestsearchresults(List<ProfileBrowseModel> p, string sessionid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Put("profilebrowsemodel" + sessionid, "Guests"); }
               catch (DataCacheException ex)
                {
                    //Log error
                    //throw new InvalidOperationException();
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    return false;
                }
                catch (Exception ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }

                return true;




            }
            public static bool removemembersearchresults(string profileid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Remove("profilebrowsemodel" + profileid); }
               catch (DataCacheException ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);                   
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }

                return true;



            }
            public static bool removeguestsearchresults(string sessionid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Remove("profilebrowsemodel" + sessionid, "Guests"); }
               catch (DataCacheException ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {  //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }
                return true;



            }
        }


        /// <summary>
        /// This class helps us determine what pages use what styles etc - comes from a appsettings file
        /// </summary>
        public static class CssStyleSelector
        {

            public static string getbodycssbypagename(string pagename,IUnitOfWorkAsync context)
            {


                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

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
                            dataCache.Put("SystemPageSettingsList", pages);
                    }


                    //TO DO 
                    //finde the matchv

                    var results = from item in pages
                                  where (item.title  == pagename.Trim())
                                  select item;

                    //return the default white background if none found
                    if (results.Count() == 0) return CssSyle;

                    //else return the value from cache or database
                    return results.FirstOrDefault().bodycssstylename.Trim();
                }
               catch (DataCacheException ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {  //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }



            }

            public static List<systempagesetting> getsystempagesettingslist(IUnitOfWorkAsync context)
            {


                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

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
                        pages =  context.Repository<systempagesetting>().Queryable().Where(p => p.bodycssstylename != "").ToList();
                        if (dataCache != null)
                            dataCache.Put("SystemPageSettingsList", pages);
                    }


                    return pages;
                }
               catch (DataCacheException ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //Log error
                    logger = new  Logging(applicationEnum.AppfabricCaching);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null, null);
                    //put cleanup code here
                  throw;
                }



            }
        }

        //only runs on application start, if the objects are not found then we will want to 
        //add all thier getters and setters here 
        public static class SharedObjectHelper
        {
            //Photo based functions 
            public static List<lu_photoformat> getphotoformatlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<lu_photoformat> photoformat = null;
                try
                {

                    try { if (dataCache != null) photoformat = dataCache.Get("photoformatlist") as List<lu_photoformat>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (photoformat == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoformat =  context.Repository<lu_photoformat>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Put("photoformatlist", photoformat);

                    } return photoformat;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getphotoapprovalstatuslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoapprovalstatus = null;
                try
                {

                    try { if (dataCache != null) photoapprovalstatus = dataCache.Get("photoapprovalstatuslist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (photoapprovalstatus == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photoapprovalstatus = ( from o in context.Repository<lu_photoapprovalstatus>().Queryable().ToList()
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
                            dataCache.Put("photoapprovalstatuslist", photoapprovalstatus);

                    } return photoapprovalstatus;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<lu_photorejectionreason> getphotorejectionreasonlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<lu_photorejectionreason> photorejectionreason = null;
                try
                {

                    try { if (dataCache != null) photorejectionreason = dataCache.Get("photorejectionreasonlist") as List<lu_photorejectionreason>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (photorejectionreason == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photorejectionreason =  context.Repository<lu_photorejectionreason>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Put("photorejectionreasonlist", photorejectionreason);

                    } return photorejectionreason;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getphotostatuslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> photostatus = null;
                try
                {

                    try { if (dataCache != null) photostatus = dataCache.Get("photostatuslist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("photostatuslist", photostatus);

                    } return photostatus;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getphotoimagetypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoimagetype = null;
                try
                {

                    try { if (dataCache != null) photoimagetype = dataCache.Get("photoimagetypelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("photoimagetypelist", photoimagetype);

                    } return photoimagetype;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }

            //olawal 3-13-2013 other functions added after fact
            public static List<lu_photostatusdescription> getphotostatusdescriptionlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<lu_photostatusdescription> photostatusdescription = null;
                try
                {

                    try { if (dataCache != null) photostatusdescription = dataCache.Get("photostatusdescriptionlist") as List<lu_photostatusdescription>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (photostatusdescription == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        photostatusdescription =  context.Repository<lu_photostatusdescription>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Put("photostatusdescriptionlist", photostatusdescription);

                    } return photostatusdescription;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getabusetypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> abusetype = null;
                try
                {

                    try { if (dataCache != null) abusetype = dataCache.Get("abusetypelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("abusetypelist", abusetype);

                    } return abusetype;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getprofilestatuslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> profilestatus = null;
                try
                {

                    try { if (dataCache != null) profilestatus = dataCache.Get("profilestatuslist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("profilestatuslist", profilestatus);

                    } return profilestatus;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getphotoImagersizerformatlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> photoImagersizerformat = null;
                try
                {

                    try { if (dataCache != null) photoImagersizerformat = dataCache.Get("getphotoImagersizerformat") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("photoImagersizerformatlist", photoImagersizerformat);

                    } return photoImagersizerformat;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<lu_role> getrolelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<lu_role> role = null;
                try
                {

                    try { if (dataCache != null) role = dataCache.Get("rolelist") as List<lu_role>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (role == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        role =  context.Repository<lu_role>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Put("rolelist", role);

                    } return role;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getsecurityleveltypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> securityleveltype = null;
                try
                {

                    try { if (dataCache != null) securityleveltype = dataCache.Get("securityleveltypelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("securityleveltypelist", securityleveltype);

                    } return securityleveltype;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getshowmelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> showme = null;
                try
                {

                    try { if (dataCache != null) showme = dataCache.Get("showmelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("showmelist", showme);

                    } return showme;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getsortbytypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> sortbytype = null;
                try
                {

                    try { if (dataCache != null) sortbytype = dataCache.Get("sortbytypelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("sortbytypelist", sortbytype);

                    } return sortbytype;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getsecurityquestionlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> securityquestion = null;
                try
                {

                    try { if (dataCache != null) securityquestion = dataCache.Get("securityquestionlist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("securityquestionlist", securityquestion);

                    } return securityquestion;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<lu_flagyesno> getflagyesnolist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<lu_flagyesno> flagyesno = null;
                try
                {

                    try { if (dataCache != null) flagyesno = dataCache.Get("flagyesnolist") as List<lu_flagyesno>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }

                    if (flagyesno == null)
                    {
                        // context context = new context();
                        //remafill the Genders list from the repositry and exit
                        flagyesno =  context.Repository<lu_flagyesno>().Queryable().OrderBy(x => x.description).ToList();
                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);


                        //if we still have no datacahe do tis
                        if (dataCache != null)
                            dataCache.Put("flagyesnolist", flagyesno);

                    } return flagyesno;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }
            public static List<listitem> getprofilefiltertypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> profilefiltertype = null;
                try
                {

                    try { if (dataCache != null) profilefiltertype = dataCache.Get("profilefiltertypelist") as List<listitem>; }
                   catch (DataCacheException ex)
                    {
                        //TO DO LOG and NOTIFY HERE
                        //throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
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
                            dataCache.Put("profilefiltertypelist", profilefiltertype);

                    } return profilefiltertype;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }

            }


            //generic functions
            public static List<listitem> getgenderlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> genders = null;
                try { if (dataCache != null) genders = dataCache.Get("genderlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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
                        dataCache.Put("genderlist", genders);

                }
                
                return genders;
            }
            public static List<age> getagelist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<age> ageslist = null;



                //if we still have no datacahe do tis
                try { if (dataCache != null) ageslist = dataCache.Get("agelist") as List<age>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
                if (ageslist == null)
                {
                    ageslist = generatedlists.ageslist();    
                    //if we still have no datacahe no need to do the put
                    if (dataCache != null)
                        dataCache.Put("agelist", ageslist);

                } return ageslist;
            }
            public static List<metricheight> getmetricheightlist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<metricheight> heights = null;
                try { if (dataCache != null) heights = dataCache.Get("metricheightlist") as List<metricheight>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
                if (heights == null)
                {
                   // context context = new context();
                    //remafill the ages list from the repositry and exit
                    heights = generatedlists.metricheights();
                    // Datings context = new modelContext();
                    // model =  context.Repository<models.Single(c => c.Id == id);
                    if (dataCache != null)                      
                    dataCache.Put("metricheightlist", heights);

                } return heights;
            }           
           

            #region "Creiteria Apperance lists cached here"

            public static List<listitem> getbodytypelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();
                List<listitem> bodytype = null;

                try { if (dataCache != null) bodytype = dataCache.Get("bodytypelist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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
                        dataCache.Put("bodytypelist", bodytype);

                } return bodytype;
            }
            public static List<listitem> getethnicitylist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> ethnicity = null;
                try { if (dataCache != null) ethnicity = dataCache.Get("ethnicitylist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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
                    dataCache.Put("ethnicitylist", ethnicity);

                } return ethnicity;
            }
            public static List<listitem> geteyecolorlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> eyecolor = null;
                try { if (dataCache != null) eyecolor = dataCache.Get("eyecolorlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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
                    if (dataCache != null) dataCache.Put("eyecolorlist", eyecolor);

                } return eyecolor;
            }
            public static List<listitem> gethaircolorlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> haircolor = null;
                try { if (dataCache != null)haircolor = dataCache.Get("haircolorlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("haircolorlist", haircolor);

                } return haircolor;
            }
            public static List<listitem> gethotfeaturelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> hotfeature = null;
                try { if (dataCache != null)hotfeature = dataCache.Get("hotfeaturelist") as List<listitem>; }
                catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw;
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

                    if (dataCache != null) dataCache.Put("hotfeaturelist", hotfeature);

                } return hotfeature;
            }

            #endregion

            #region "CriteriaCharacter lists cached here"
            public static List<listitem> getdietlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> diet = null;
                try { if (dataCache != null) diet = dataCache.Get("dietlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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



                    if (dataCache != null) dataCache.Put("dietlist", diet);

                } return diet;
            }
            public static List<listitem> getdrinkslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> drinks = null;
                try { if (dataCache != null) drinks = dataCache.Get("drinkslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("drinkslist", drinks);

                } return drinks;
            }
            public static List<listitem> getexerciselist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> exercise = null;
                try { if (dataCache != null) exercise = dataCache.Get("exerciselist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
                if (exercise == null)
                {
                    //context context = new context();
                    //remafill the ages list from the repositry and exit
                    exercise =(from o in context.Repository<lu_exercise>().Queryable().ToList()
                                 select new listitem
                                 {
                                     id = o.id,
                                     description = o.description,
                                     selected = false
                                 }).ToList();

                    if (dataCache != null) dataCache.Put("exerciselist", exercise);

                } return exercise;
            }
            public static List<listitem> gethobbylist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> hobby = null;
                try { if (dataCache != null) hobby = dataCache.Get("hobbylist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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
                             }).ToList();

                    if (dataCache != null) dataCache.Put("hobbylist", hobby);

                } return hobby;
            }
            public static List<listitem> gethumorlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> humor = null;
                try { if (dataCache != null) humor = dataCache.Get("humorlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null)  dataCache.Put("humorlist", humor);

                } return humor;
            }
            public static List<listitem> getpoliticalviewlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> politicalview = null;
                try { if (dataCache != null) politicalview = dataCache.Get("politicalviewlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                    if (dataCache != null)  dataCache.Put("politicalviewlist", politicalview);

                } return politicalview;
            }
            public static List<listitem> getreligionlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> religion = null;
                try { if (dataCache != null)  religion = dataCache.Get("religionlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                    if (dataCache != null) dataCache.Put("religionlist", religion);

                } return religion;
            }
            public static List<listitem> getreligiousattendancelist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> religiousattendance = null;
                try { if (dataCache != null) religiousattendance = dataCache.Get("religiousattendancelist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                    if (dataCache != null) dataCache.Put("religiousattendancelist", religiousattendance);

                } return religiousattendance;
            }
            public static List<listitem> getsignlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> sign = null;
                try { if (dataCache != null) sign = dataCache.Get("signlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null)  dataCache.Put("signlist", sign);

                } return sign;
            }
            public static List<listitem> getsmokeslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> smokes = null;
                try { if (dataCache != null)  smokes = dataCache.Get("smokeslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
                if (smokes == null)
                {
                    //context context = new context();
                    //remafill the ages list from the repositry and exit
                    smokes = (from o in context.Repository<lu_smokes().Queryable().ToList()
                              select new listitem
                              {
                                  id = o.id,
                                  description = o.description,
                                  selected = false
                              }).ToList();

                    if (dataCache != null) dataCache.Put("smokeslist", smokes);

                } return smokes;
            }

            #endregion

            #region "Criteria Lifestyle lists cached here"

            public static List<listitem> geteducationlevellist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> educationlevel = null;
                try { if (dataCache != null) educationlevel = dataCache.Get("educationlevellist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("educationlevellist", educationlevel);

                } return educationlevel;
            }
            public static List<listitem> getemploymentstatuslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> employmentstatus = null;
                try { if (dataCache != null) employmentstatus = dataCache.Get("employmentstatuslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("employmentstatuslist", employmentstatus);

                } return employmentstatus;
            }
            public static List<listitem> gethavekidslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> havekids = null;
                try { if (dataCache != null) havekids = dataCache.Get("havekidslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("havekidslist", havekids);

                } return havekids;
            }
            public static List<listitem> getincomelevellist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> incomelevel = null;
                try { if (dataCache != null) incomelevel = dataCache.Get("incomelevellist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("incomelevellist", incomelevel);

                } return incomelevel;
            }
            public static List<listitem> getlivingsituationlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> livingsituation = null;
                try { if (dataCache != null) livingsituation = dataCache.Get("livingsituationlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null)  dataCache.Put("livingsituationlist", livingsituation);

                } return livingsituation;
            }
            public static List<listitem> getlookingforlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> lookingfor = null;
                try { if (dataCache != null) lookingfor = dataCache.Get("lookingforlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                    if (dataCache != null) dataCache.Put("lookingforlist", lookingfor);

                } return lookingfor;
            }
            public static List<listitem> getmaritalstatuslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> maritalstatus = null;
                try { if (dataCache != null) maritalstatus = dataCache.Get("maritalstatuslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                    if (dataCache != null)  dataCache.Put("maritalstatuslist", maritalstatus);

                } return maritalstatus;
            }
            public static List<listitem> getprofessionlist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> profession = null;
                try { if (dataCache != null) profession = dataCache.Get("professionlist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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

                    if (dataCache != null) dataCache.Put("professionlist", profession);

                } return profession;
            }
            public static List<listitem> getwantskidslist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<listitem> wantskids = null;
                try { if (dataCache != null) wantskids = dataCache.Get("wantskidslist") as List<listitem>; }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
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


                } return wantskids;
            }

            #endregion
            
         

            #region "Geodata lists"

            public static List<country> getcountrylist(IUnitOfWorkAsync context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<country> countrys = new List<country>();

                try
                {
                  

                    try { if (dataCache != null) countrys = dataCache.Get("countrieslist") as List<country>; }
                    catch (DataCacheException ex)
                    {
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }
                    if (countrys.Count() == 0)
                    {
                        // context context = new context();
                        //remafill the Countrys list from the repositry and exit

                    //    List<country> tmplist = new List<country>();
                        // Loop over the int List and modify it.
                        //insert the first one as ANY
                        countrys.Add(new country { id = "0", name = "Any" });


                        foreach (countrypostalcode item in Api.AsyncCalls.getcountryandpostalcodestatuslistasync().Result)
                            {
                                var currentcountry = new country { id = item.id.ToString(), name = item.name };
                                countrys.Add(currentcountry);
                            }
                        

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
                            dataCache.Put("countrieslist", countrys);

                    } return countrys;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
            }

            public static List<countrypostalcode> getcountryandpostalcodestatuslist(IGeoDataStoredProcedures  _storedProcedures)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<countrypostalcode > countryandpostalcodes = null;

                try
                {

                    try { if (dataCache != null) countryandpostalcodes = dataCache.Get("countryandpostalcodestatuslist") as List<countrypostalcode>; }
                   catch (DataCacheException ex)
                    {
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                        //put cleanup code here
                      throw;
                    }
                    if (countryandpostalcodes == null)
                    {
                        // context context = new context();
                        //remafill the Countrys list from the repositry and exit


                       // List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                       // myQuery = _postal context.Repository<GetCountry_PostalCode_List().ToList().Where(p => p.CountryName != "").OrderBy(p => p.CountryName).ToList();

                       // PostalData2Context PostalDataContext = new PostalData2Context();
                       // using (var tempdb = PostalDataContext)
                      //  {
                          //  GeoService GeoService = new GeoService(tempdb);
                     
                          // return  spatialextentions.getcountryandpostalcodestatuslist(_storedProcedures);
                        return null;

                          

                     //   }


                        

                        //countryandpostalcodes = georepository.getcountrylist();

                        //if we still have no datacahe do tis

                        // Datings context = new modelContext();
                        // model =  context.Repository<models.Single(c => c.Id == id);
                        if (dataCache != null)
                            dataCache.Put("countryandpostalcodestatuslist", countryandpostalcodes);

                    } return countryandpostalcodes;
                }
               catch (DataCacheException ex)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                  throw;
                }
            }

            #endregion

            public static bool RemoveAllLists()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();


                //TO DO find a way to just flush the persistant cache
                try
                {

                    dataCache.Remove("agelist");
                    dataCache.Remove("genderlist");
                    dataCache.Remove("countrieslist");
                    dataCache.Remove("securityquestionslist");
                    dataCache.Remove("bodytypeslist");
                    dataCache.Remove("eyecolorlist");
                    dataCache.Remove("haircolorlist");
                    dataCache.Remove("metricheightlist");
                    dataCache.Remove("dietlist");
                    dataCache.Remove("drinkslist");
                    dataCache.Remove("exerciselist");
                    dataCache.Remove("hobbylist");
                    dataCache.Remove("humorlist");
                    dataCache.Remove("religionlist");
                    dataCache.Remove("religiousattendancelist");
                    dataCache.Remove("signlist");
                    dataCache.Remove("smokeslist");
                    dataCache.Remove("educationlevellist");
                    dataCache.Remove("employmentstatuslist");
                    dataCache.Remove("havekidstlist");
                    dataCache.Remove("incomelevellist");
                    dataCache.Remove("livingsituationlist");                 
                    dataCache.Remove("maritalstatuslist");
                    dataCache.Remove("professionlist");
                    dataCache.Remove("lookingforlist");
                  //  dataCache.Remove("VisibilityMailSettingsList");
                  //  dataCache.Remove("VisibilityStealthSettingsList");
                }

               catch (DataCacheException ex)
                {
                    return false;
                    throw new InvalidOperationException();

                }

                return true;



            }

            //these shoul just be regular lookups
            #region "lists for visibily settings that can be used else where"

            //public static List<visiblitysetting> getvisibilitymailsettingslist(IUnitOfWorkAsync context)
            //{
            //    DataCache dataCache;
            //    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            //    dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

            //    List<visiblitysetting> visibilitymailsettings = null;
            //    try { if (dataCache != null) visibilitymailsettings = dataCache.Get("visibilitymailsettingslist") as List<visiblitysetting>; }
            //   catch (DataCacheException ex)
            //    {
            //        throw new InvalidOperationException();
            //    }
            //    if (visibilitymailsettings == null)
            //    {
            //        //context context = new context();
            //        //remafill the ages list from the repositry and exit
            //        visibilitymailsettings =  context.Repository<visibilitysettings.Where(p=>p.OrderBy(x => x.description).ToList();
            //        // Datings context = new modelContext();
            //        // model =  context.Repository<models.Single(c => c.Id == id);
            //        if (dataCache != null)
            //            dataCache.Put("visibilitymailsettingslist", visibilitymailsettings);

            //    } return VisibilityMailSettings;
            //}
            //public static List<visiblitysetting> getvisibilitystealthsettingslist()
            //{
            //    DataCache dataCache;
            //    //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            //    dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

            //    List<SelectListItem> VisibilityStealthSettings = null;
            //    try { if (dataCache != null) VisibilityStealthSettings = dataCache.Get("VisibilityStealthSettingsList") as List<SelectListItem>; }
            //   catch (DataCacheException ex)
            //    {
            //        throw new InvalidOperationException();
            //    }
            //    if (VisibilityStealthSettings == null)
            //    {
            //        context context = new context();
            //        //remafill the ages list from the repositry and exit
            //        VisibilityStealthSettings =  context.Repository<VisibilityStealthSettingsList;
            //        // Datings context = new modelContext();
            //        // model =  context.Repository<models.Single(c => c.Id == id);
            //        if (dataCache != null)
            //            dataCache.Put("VisibilityStealthSettingsList", VisibilityStealthSettings);

            //    } return VisibilityStealthSettings;
            //}

            #endregion
        }


       
    }
    
}