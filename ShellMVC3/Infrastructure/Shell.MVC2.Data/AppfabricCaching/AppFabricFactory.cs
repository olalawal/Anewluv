using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models;
using Microsoft.ApplicationServer.Caching;
using System.Collections;
using System.Threading;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;




namespace Shell.MVC2.AppFabric
{

    public class CachingFactory
    {
        // public  LoggingServiceClient  svcloggingService;
        public static DataCacheFactory _cacheFactory;
        private static DataCache _cache;
        private static string _sessionStateCacheName = "AnewLuv";
        private static string _persistantCacheName = "AnewLuvPersistantCache";
        private static string _LeadCacheHost = "NmediaApps01"; // add NemdiaAPPs IP address to hostnames file on production webserver
        static readonly object _locker = new object();

        //public  CachingFactory(LoggingServiceClient _loggingService)
        //{
        //    IKernel kernel = new StandardKernel();
        //    //Get these initalized
        //    svcloggingService = kernel.Get<LoggingServiceClient>();

        //}




        #region "Cache Initializers"

        /// <summary>
        /// Summary description for Cache
        /// </summary>
        private static DataCache GetCache
        {


            get
            {
#if DISCONECTED
                return null;

#else
        if (_cacheFactory == null)
        {           
            {

                if (Monitor.TryEnter(_locker, 20000))	{

                
                if (_cacheFactory == null)
                {
                    try
                    {
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
                    
                    }
                    catch (DataCacheException ex)
                    {
                        //throw ex;
                        // log the execption message
                        return null;
                    }

                    finally		
                    {			
                        Monitor.Exit(_locker);
                    }
                }
                }


            }


        }

        DataCache cache = null;

        if (_cacheFactory != null)
        {
            cache = _cacheFactory.GetCache(_sessionStateCacheName);
        }
        
        //make sure that any regions needed exist as well
        return CreateAewLuvRegions(cache);
#endif

            }
        }

        private static DataCache GetPersistantCache
        {






            get
            {
                //added the code to handle disconnected clients
#if DISCONECTED
                return null;

#else
                                                                                                                                                                                                                                                                                                                                                                                                                                                                           if (_cacheFactory == null)
               {
                   {

                       if (Monitor.TryEnter(_locker, 20000))
                       {
                           if (_cacheFactory == null)
                           {
                               try
                               {



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




                               }
                               catch (DataCacheException ex)
                               {
                                   //log cache error
                                   throw ex;
                                   // log the execption message
                                   //return false;
                                   return null;
                               }

                               finally
                               {
                                   Monitor.Exit(_locker);
                               }
                           }
                       }
                   }
               }
               //reset cache values ?
               DataCache cache = null;

               if (_cacheFactory != null)
               {
                   cache = _cacheFactory.GetCache(_persistantCacheName);
               }

               //make sure that any regions needed exist as well
               return CreateAewLuvRegions(cache);

#endif
            }

        }

        public static bool ClearCurrentSessionCache(string _ProfileID, HttpContextBase context)
        {

            //clear out guest data
            MembersViewModelHelper.RemoveGuestData(context);
            ProfileBrowseModelsHelper.RemoveGuestResults(context);
            //clear out member data if member is authenticated
            //TO DO worry about maybe to deleted this to wonder about peristing it?
            //Due to speed considerations

            if (_ProfileID != null)
            {
                MembersViewModelHelper.RemoveMemberData(_ProfileID);
                ProfileBrowseModelsHelper.RemoveMemberResults(_ProfileID);
            }

            return true;

        }



        //only creating a region for guests since we want scalablituy for members
        //named caches in version 1.0 are limited to cache servers cannot be on clusters
        public static DataCache CreateAewLuvRegions(DataCache NamedCache)
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
                if (dcex.ErrorCode != DataCacheErrorCode.RegionAlreadyExists)
                    throw dcex;
            }

            return NamedCache;


        }

        #endregion

        #region "Save and Retreival of Common strings

        //TO do dpreciate this and test using the Session ID as in below
        //TO Do combine both these functions to rturn a string array of profileID and screenName
        //sowe can pick?
        public static int? getprofileidbyusername(string username, AnewluvContext _datingcontext)
        {
            //handle case of null username
            if (username == null || username == "") return null;

            DataCache dataCache;
            //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
            //create the guests region if it does not exists


            int? _profileid = null;
            try { if (dataCache != null)  _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + username)); }
            catch (DataCacheException)
            {
                throw new InvalidOperationException();
            }
            if (_profileid == null | _profileid ==0)
            {
                //MembersRepository membersrepository = new MembersRepository();
                //get the correct value from DB
                _profileid = _datingcontext.profiles.Where(p => p.username == username).FirstOrDefault().id;
                    
                //if we have an active cache we store the current value 
                if (dataCache != null)
                {
                  //store it in the database
                    if (_profileid != null) dataCache.Put("ProfileID" + username, _profileid);
                }
                else
                {  //just return the value if no cahce
                    return _profileid;

                }


            }
            //value came from cahce return it
            return _profileid ;

        }
        //TO do dpreciate this and test using the Session ID as in below
        public static int? getprofileidbyscreenname(string screenname, AnewluvContext _datingcontext)
        {
            //handle case of null screenname
            if (screenname == null || screenname == "") return null;

            DataCache dataCache;
            //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
            //create the guests region if it does not exists


            int? _profileid = null;
            try { if (dataCache != null)  _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + screenname)); }
            catch (DataCacheException)
            {
                throw new InvalidOperationException();
            }
            if (_profileid == null | _profileid == 0)
            {
                //MembersRepository membersrepository = new MembersRepository();
                //get the correct value from DB
                _profileid = _datingcontext.profiles.Where(p => p.screenname == screenname).FirstOrDefault().id;

                //if we have an active cache we store the current value 
                if (dataCache != null)
                {
                    //store it in the database
                    if (_profileid != null) dataCache.Put("ProfileID" + screenname, _profileid);
                }
                else
                {  //just return the value if no cahce
                    return _profileid;

                }


            }
            //value came from cahce return it
            return _profileid;

        }

        //TO do dpreciate this and test using the Session ID as in below
        public static int? getprofileidbysessionid(string sessionid)
        {

            DataCache dataCache;
            //DataCacheFactory dataCacheFactory = new DataCacheFactory();
            dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();
            //create the guests region if it does not exists


            int? _profileid = null;
            try { _profileid = Convert.ToInt16(dataCache.Get("ProfileID" + sessionid); }
            catch (DataCacheException)
            {
                throw new InvalidOperationException();
            }
            if (_profileid == null)
            {
                return null;

            }

            return _profileid;

        }


        #endregion


        //Converted to use AppFabric
        public static class MembersViewModelHelper
        {

            public static MembersViewModel getmemberdata(int profileid,IMembersMapperRepository membersmapperepository)
            {
                

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();



                MembersViewModel model = new MembersViewModel();
                //var mm = MembersMapperRepository();

                try
                {

                    if (dataCache != null) model = dataCache.Get("MembersViewModel" + profileid) as MembersViewModel;

                    //TO do check if the model has changed since the last time it was loaded 
                    //create a method in members repo that checks to see if something has been updated since last activity?
                    if (model == null )
                    {
                        if (dataCache != null)
                        {
                            //remap the user data if cache is empty

                            model = membersmapperepository.mapmember(profileid);
                            // Datings context = new modelContext();
                            // model = context.models.Single(c => c.Id == id);
                            dataCache.Put("MembersViewModel" + profileid, model);
                        }
                        else
                        {
                            model =membersmapperepository.mapguest();

                        }
                    } return model;
                }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
            }
            //Items for guests are just dumped in the session area not the members region
            //and they are id'd by session ID
            public static MembersViewModel getguestdata(string sessionid, IMembersMapperRepository membersmapperepository)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                MembersViewModel model = null;
                try { 
                    if (dataCache != null)  model = dataCache.Get("MembersViewModel" +sessionid, "Guests") as MembersViewModel;

                    //generate your fictional exception
                    //int x = 1;
                    //int y = 0;
                    //int z = x / y;

                    
                    if (model == null)
                    {
                        //not sure what needs to be initialized for guests , if anything initlize it here
                        // map the registration thing since 
                        // GuestRepository guestrepo = new GuestRepository();
                        // model = guestrepo.MapGuest();
                        //also map the guest members viewmodel
                       // var mm = new ViewModelMapper();
                       
                        model = membersmapperepository.mapguest();

                        //poulate the model from the view model mapper
                       // ViewModelMapper Mapper = new ViewModelMapper();
#if DEBUG
                                                Console.WriteLine("Debug version");
                                                model.register = membersmapperepository.mapregistrationtest();
                                               //model.Register = Mapper.MapRegistration();
#else
                        model.Register = Mapper.MapRegistration(model);
#endif

                        if (dataCache != null)
                        {
                            //store the model if null
                            dataCache.Put("MembersViewModel" +sessionid, model, "Guests");

                        }
                    } return model;

                }
                // try { if (dataCache != null)  model = dataCache.Get("MembersViewModel" + context.Session.SessionID, "Guests") as MembersViewModel; }

                catch (DataCacheException)
                {
                    throw new CacheingException("A problem occured accessing the Appfabric Cache", model);
                    //log the datachae type of excpetion here and mark it handled and logged
                }
                catch (Exception ex)
                {
                    var message = String.Format("Something went wrong with the cache method GetGuestData with this object or user :  {0}", context.User.Identity.Name );
                    throw new AccountException(model, message, ex);
 
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

                dataCache.Put("ProfileID" + context.Session.SessionID, ProfileID);

                return true;

            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            public static MembersViewModel updatememberdata(MembersViewModel p,IMembersMapperRepository  membersmapperepository)
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
                        //  try { oldMembersViewModel = dataCache.Get("MembersViewModel" + _ProfileID) as MembersViewModel; }
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
                            p = membersmapperepository.mapmember(p.profile.id);

                        }
                        else if (p != null && (p.profile.id != null | p.profile == null ))
                        {
                            dataCache.Put("MembersViewModel" + p.profile.id, p);
                        }

                    //handle cases were we are just updating bits for the viewmodel. QuickSearch and Account right now would be what is 
                    //being updated from what
                    //if city is empty we probbaly have empty data so reslove it 
                    // oldMembersViewModel.MyQuickSearch = (p.MyQuickSearch.MySelectedCity != null) ? p.MyQuickSearch : oldMembersViewModel.MyQuickSearch;
                    //check if account model is empty as well if it is refresh it
                    //  oldMembersViewModel.Account = (p.Account.BirthDate  != null) ? p.Account  : oldMembersViewModel.Account;

                    
                }
                catch (DataCacheException)
                {
                    throw new CacheingException("A problem occured accessing the Appfabric Cache", p);
                    //log the datachae type of excpetion here and mark it handled and logged
                }
                catch (Exception ex)
                {
                    var message = String.Format("Something went wrong with the cache method UpdateMemberData with this object or user :  {0}", ProfileID);
                    throw new AccountException(p, message, ex);

                    // throw new Exception(message, ex); 

                }
                
                return p;




            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            public static MembersViewModel updatememberprofiledatabyprofile(profile profilemodel, IMembersMapperRepository membersmapperepository)
            {
                MembersViewModel model = new MembersViewModel();
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();

                //get the current prodile data
                model = membersmapperepository.getmemberdata(profilemodel.id);
                model.profiledata = model.profile.profiledata ;

                //  MembersViewModel oldMembersViewModel = new MembersViewModel();
                //  try { oldMembersViewModel = dataCache.Get("MembersViewModel" + _ProfileID) as MembersViewModel; }
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

                dataCache.Put("MembersViewModel" + profilemodel.id, model);

                return model;




            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID

            public static MembersViewModel updateguestdata(MembersViewModel p, IMembersMapperRepository membersmapperrepository)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();


                //MembersViewModel oldMembersViewModel = null;
                // try { oldMembersViewModel = dataCache.Get("MembersViewModel" + context.Session.SessionID, "Guests") as MembersViewModel; }
                //catch (DataCacheException)
                //{
                //    //Log error
                //    //throw new InvalidOperationException();
                //    return null;
                //}
                if (p == null)
                {
                    //remap the user data if cache is empty
                   // var mm = new ViewModelMapper();
                    //TO DO update map guest to scrape UI data
                    //Mapguest will probbaly get geo data like what country city state thier IP matches and any cookie data we can scrap or facebook data
                    // oldMembersViewModel = mm.MapGuest(); //default values probbaly just for test don't map anything in this case since 
                    //we do not want to populate data yet
                    p = membersmapperrepository.mapguest();
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
                // model = context.models.Single(c => c.Id == id);
                dataCache.Put("MembersViewModel" + p.sessionid , p, "Guests");

                return p;




            }

            public static bool removememberdata(int profileid)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();



                try { dataCache.Remove("MembersViewModel" + profileid); }
                catch (DataCacheException)
                {
                    return false;
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                return true;



            }

            public static bool RemoveGuestData(HttpContextBase context)
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetCache;  // dataCacheFactory.GetDefaultCache();


                try { dataCache.Remove("MembersViewModel" + context.Session.SessionID, "Guests"); }
                catch (DataCacheException)
                {
                    return false;
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                return true;



            }




        }

        public static class ProfileBrowseModelsHelper
        {

            public static List<ProfileBrowseModel> getmembercurrentsearchresults(string profileid, ISearchRepository  searchrepository)
            {
                DataCache dataCache;
                dataCache = GetCache;

                List<ProfileBrowseModel> model = null;
                try { model = dataCache.Get("ProfileBrowseModel" + profileid) as List<ProfileBrowseModel>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (model == null)
                {

                    //remap the user data if cache is empty
                    model = new List<ProfileBrowseModel>();
                    //return memberactionsrepository.
                    //No need to put empty data
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    // dataCache.Put("ProfileBrowseModel" + ProfileID, model);


                } return model;
            }

            //Items for guests are just dumped in the session area not the members region
            //and they are id'd by session ID
            public static List<ProfileBrowseModel> getguestresults(string sessionid)
            {

                DataCache dataCache;
                dataCache = GetCache;

                List<ProfileBrowseModel> model = null;
                try { model = dataCache.Get("ProfileBrowseModel" +sessionid, "Guests") as List<ProfileBrowseModel>; }
                catch (DataCacheException)
                {
                    //load from DB or something
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (model == null)
                {


                    //remap the user data if cache is empty
                    model = new List<ProfileBrowseModel>();

                    //No need to put empty data
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    // dataCache.Put("ProfileBrowseModel" + ProfileID, model);
                } return model;


            }

            //updates the model in session with any new data
            //TO DO verify that the P contians profile ID
            //Basically it is a save
            public static bool addmembersearchresults(List<ProfileBrowseModel> p, string profileid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Put("ProfileBrowseModel" + profileid, p); }
                catch (DataCacheException)
                {
                    //Log error
                    //throw new InvalidOperationException();
                    return false;
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }

                return true;




            }

            public static bool addguestsearchresults(List<ProfileBrowseModel> p, string sessionid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Put("ProfileBrowseModel" + sessionid, "Guests"); }
                catch (DataCacheException)
                {
                    //Log error
                    //throw new InvalidOperationException();
                    return false;
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }

                return true;




            }

            public static bool removemembersearchresults(string profileid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Remove("ProfileBrowseModel" + ProfileID); }
                catch (DataCacheException)
                {
                    return false;
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }

                return true;



            }
            public static bool removeguestsearchresults(string sessionid)
            {
                DataCache dataCache;
                dataCache = GetCache;

                try { dataCache.Remove("ProfileBrowseModel" + sessionid, "Guests"); }
                catch (DataCacheException)
                {
                    return false;
                    throw new InvalidOperationException();

                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                return true;



            }

        }

        //only runs on application start, if the objects are not found then we will want to 
        //add all thier getters and setters here 
        public static class SharedObjectHelper
        {

            public static List<string> getagelist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<string> Ages = null;



                //if we still have no datacahe do tis
                try { if (dataCache != null) Ages = dataCache.Get("AgeList") as List<string>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Ages == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Ages = sharedrepository.AgesSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);

                    //if we still have no datacahe no need to do the put
                    if (dataCache != null)
                        dataCache.Put("AgeList", Ages);

                } return Ages;
            }

            public static List<lu_gender> GetGenderList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();




                List<lu_gender> Genders = null;


                try { if (dataCache != null) Genders = dataCache.Get("GenderList") as List<lu_gender>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Genders == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the Genders list from the repositry and exit
                    Genders = sharedrepository.GendersSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);


                    //if we still have no datacahe do tis
                    if (dataCache != null)
                        dataCache.Put("GenderList", Genders);

                } return Genders;
            }

            public static List<SelectListItem> GetCountrysList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Countrys = null;




                try { if (dataCache != null) Countrys = dataCache.Get("CountriesList") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Countrys == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the Countrys list from the repositry and exit

                    Countrys = sharedrepository.CountrySelectList();

                    //if we still have no datacahe do tis

                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    if (dataCache != null)
                        dataCache.Put("CountriesList", Countrys);

                } return Countrys;
            }

            public static List<SelectListItem> GetSecurityQuestionsList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();              

                List<SelectListItem> SecurityQuestionsList = null;

                try { if (dataCache != null)  SecurityQuestionsList = dataCache.Get("SecurityQuestionsList") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (SecurityQuestionsList == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the SecurityQuestions s list from the repositry and exit

                    SecurityQuestionsList = sharedrepository.SecurityQuestionSelectList;

                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    if (dataCache != null)
                        dataCache.Put("SecurityQuestionsList", SecurityQuestionsList);

                }
                return SecurityQuestionsList;
            }


            #region "Creiteria Apperance lists cached here"

            public static List<SelectListItem> GetMetricHeightList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Heights = null;
                try { Heights = dataCache.Get("MetricHeightList") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Heights == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Heights = sharedrepository.HeightMetricSelectList();
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("MetricHeightList", Heights);

                } return Heights;
            }
            public static List<SelectListItem> Getbodytypeslist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> BodyTypes = null;
                try { BodyTypes = dataCache.Get("bodytypeslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (BodyTypes == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    BodyTypes = sharedrepository.BodyTypesSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("bodytypeslist", BodyTypes);

                } return BodyTypes;
            }
            public static List<SelectListItem> Geteyecolorlist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> EyeColor = null;
                try { EyeColor = dataCache.Get("eyecolorlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (EyeColor == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    EyeColor = sharedrepository.EyeColorSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("eyecolorlist", EyeColor);

                } return EyeColor;
            }
            public static List<SelectListItem> Gethaircolorlist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> HairColor = null;
                try { HairColor = dataCache.Get("haircolorlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (HairColor == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    HairColor = sharedrepository.HairColorSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("haircolorlist", HairColor);

                } return HairColor;
            }

            #endregion

            #region "CriteriaCharacter lists cached here"
            public static List<SelectListItem> Getdietlist()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Diet = null;
                try { Diet = dataCache.Get("dietlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Diet == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Diet = sharedrepository.DietSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("dietlist", Diet);

                } return Diet;
            }
            public static List<SelectListItem> GetDrinksList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Drinks = null;
                try { Drinks = dataCache.Get("drinkslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Drinks == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Drinks = sharedrepository.DrinksSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("drinkslist", Drinks);

                } return Drinks;
            }
            public static List<SelectListItem> GetExerciseList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Exercise = null;
                try { Exercise = dataCache.Get("exerciselist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Exercise == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Exercise = sharedrepository.ExerciseSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("exerciselist", Exercise);

                } return Exercise;
            }
            public static List<SelectListItem> GetHobbyList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Hobby = null;
                try { Hobby = dataCache.Get("hobbylist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Hobby == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Hobby = sharedrepository.HobbySelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("hobbylist", Hobby);

                } return Hobby;
            }
            public static List<SelectListItem> GetHumorList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Humor = null;
                try { Humor = dataCache.Get("humorlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Humor == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Humor = sharedrepository.HumorSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("humorlist", Humor);

                } return Humor;
            }
            public static List<SelectListItem> GetPoliticalViewList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> PoliticalView = null;
                try { PoliticalView = dataCache.Get("politicalviewlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (PoliticalView == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    PoliticalView = sharedrepository.PoliticalViewSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("politicalviewlist", PoliticalView);

                } return PoliticalView;
            }
            public static List<SelectListItem> GetReligionList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Religion = null;
                try { Religion = dataCache.Get("religionlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Religion == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Religion = sharedrepository.ReligionSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("religionlist", Religion);

                } return Religion;
            }
            public static List<SelectListItem> GetReligiousAttendanceList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> ReligiousAttendance = null;
                try { ReligiousAttendance = dataCache.Get("religiousattendancelist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (ReligiousAttendance == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    ReligiousAttendance = sharedrepository.ReligiousAttendanceSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("religiousattendancelist", ReligiousAttendance);

                } return ReligiousAttendance;
            }
            public static List<SelectListItem> GetSignList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Sign = null;
                try { Sign = dataCache.Get("signlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Sign == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Sign = sharedrepository.SignSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("signlist", Sign);

                } return Sign;
            }
            public static List<SelectListItem> GetSmokesList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Smokes = null;
                try { Smokes = dataCache.Get("smokeslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
                if (Smokes == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Smokes = sharedrepository.SmokesSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("smokeslist", Smokes);

                } return Smokes;
            }

            #endregion

            #region "Criteria Lifestyle lists cached here"
            public static List<SelectListItem> GetEducationLevelList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> EducationLevel = null;
                try
                {

                    EducationLevel = dataCache.Get("educationlevellist") as List<SelectListItem>;

                    if (EducationLevel == null)
                    {
                        SharedRepository sharedrepository = new SharedRepository();
                        //remafill the ages list from the repositry and exit
                        EducationLevel = sharedrepository.EducationLevelSelectList;
                        // Datings context = new modelContext();
                        // model = context.models.Single(c => c.Id == id);
                        dataCache.Put("educationlevellist", EducationLevel);
                      

                    }
                }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                  
                }
                return EducationLevel;

            }
            public static List<SelectListItem> GetEmploymentStatusList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> EmploymentStatus = null;
                try { EmploymentStatus = dataCache.Get("employmentstatuslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (EmploymentStatus == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    EmploymentStatus = sharedrepository.EmploymentStatusSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("employmentstatuslist", EmploymentStatus);

                } return EmploymentStatus;
            }
            public static List<SelectListItem> GetHaveKidsList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> HaveKids = null;
                try { HaveKids = dataCache.Get("havekidslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (HaveKids == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    HaveKids = sharedrepository.HaveKidsSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("havekidslist", HaveKids);

                } return HaveKids;
            }
            public static List<SelectListItem> GetIncomeLevelList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> IncomeLevel = null;
                try { IncomeLevel = dataCache.Get("incomelevellist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (IncomeLevel == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    IncomeLevel = sharedrepository.IncomeLevelSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("incomelevellist", IncomeLevel);

                } return IncomeLevel;
            }
            public static List<SelectListItem> GetLivingSituationList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> LivingSituation = null;
                try { LivingSituation = dataCache.Get("livingsituationlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (LivingSituation == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    LivingSituation = sharedrepository.LivingSituationSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("livingsituationlist", LivingSituation);

                } return LivingSituation;
            }

            public static List<SelectListItem> GetLookingForList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> LookingFor = null;
                try { LookingFor = dataCache.Get("lookingforlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (LookingFor == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    LookingFor = sharedrepository.LookingForSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("lookingforlist", LookingFor);

                } return LookingFor;
            }
            public static List<SelectListItem> GetMaritalStatusList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> MaritalStatus = null;
                try { MaritalStatus = dataCache.Get("maritalstatuslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (MaritalStatus == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    MaritalStatus = sharedrepository.MaritalStatusSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("maritalstatuslist", MaritalStatus);

                } return MaritalStatus;
            }
            public static List<SelectListItem> GetProfessionList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> Profession = null;
                try { Profession = dataCache.Get("professionlist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (Profession == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    Profession = sharedrepository.ProfessionSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("professionlist", Profession);

                } return Profession;
            }
            public static List<SelectListItem> GetWantsKidsList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> WantsKids = null;
                try { WantsKids = dataCache.Get("wantskidslist") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (WantsKids == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    WantsKids = sharedrepository.WantsKidsSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    dataCache.Put("wantskidslist", WantsKids);

                } return WantsKids;
            }
            #endregion

            #region "lists for visibily settings that can be used else where"

            public static List<SelectListItem> GetVisibilityMailSettingsList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> VisibilityMailSettings = null;
                try { if (dataCache != null) VisibilityMailSettings = dataCache.Get("VisibilityMailSettingsList") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (VisibilityMailSettings == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    VisibilityMailSettings = sharedrepository.VisibilityMailSettingsList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    if (dataCache != null)
                        dataCache.Put("VisibilityMailSettingsList", VisibilityMailSettings);

                } return VisibilityMailSettings;
            }

            public static List<SelectListItem> GetVisibilityStealthSettingsList()
            {
                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                List<SelectListItem> VisibilityStealthSettings = null;
                try { if (dataCache != null) VisibilityStealthSettings = dataCache.Get("VisibilityStealthSettingsList") as List<SelectListItem>; }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                if (VisibilityStealthSettings == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                    VisibilityStealthSettings = sharedrepository.VisibilityStealthSettingsList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);
                    if (dataCache != null)
                        dataCache.Put("VisibilityStealthSettingsList", VisibilityStealthSettings);

                } return VisibilityStealthSettings;
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


                    dataCache.Remove("AgeList");
                    dataCache.Remove("GenderList");
                    dataCache.Remove("CountriesList");
                    dataCache.Remove("SecurityQuestionsList");
                    dataCache.Remove("bodytypeslist");
                    dataCache.Remove("eyecolorlist");
                    dataCache.Remove("haircolorlist");
                    dataCache.Remove("MetricHeightList");
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
                    dataCache.Remove("HaveKidstList");
                    dataCache.Remove("incomelevellist");
                    dataCache.Remove("livingsituationlist");
                    dataCache.Remove("AgeList");
                    dataCache.Remove("maritalstatuslist");
                    dataCache.Remove("professionlist");
                    dataCache.Remove("lookingforlist");
                    dataCache.Remove("VisibilityMailSettingsList");
                    dataCache.Remove("VisibilityStealthSettingsList");
                }

                catch (DataCacheException)
                {
                    return false;
                    throw new InvalidOperationException();

                }

                return true;



            }


        }


        /// <summary>
        /// This class helps us determine what pages use what styles etc - comes from a appsettings file
        /// </summary>
        public static class CssStyleSelector
        {

            public static string GetBodyCssByPageName(string pagename)
            {
               

                DataCache dataCache;
                //DataCacheFactory dataCacheFactory = new DataCacheFactory();
                dataCache = GetPersistantCache;  // dataCacheFactory.GetDefaultCache();

                //TO DO put in cache or something ? or return from shared object deal
                string CssSyle = "StandardWhiteBackground";  //default
                List<SystemPageSetting> pages = null;


                //if we still have no datacahe do tis
                try 
                { 
                    
                if (dataCache != null) pages = dataCache.Get("SystemPageSettingsList") as List<SystemPageSetting>; 
              

                if (pages == null)
                {
                    SharedRepository sharedrepository = new SharedRepository();
                    //remafill the ages list from the repositry and exit
                   // Ages = sharedrepository.AgesSelectList;
                    // Datings context = new modelContext();
                    // model = context.models.Single(c => c.Id == id);

                    //if we still have no datacahe no need to do the put
                   // if (dataCache == null) return Ages;
                    pages = sharedrepository.GetSystemPageSettingList;
                    if (dataCache != null)
                        dataCache.Put("SystemPageSettingsList", pages);
                }


                //TO DO 
                //finde the matchv

                var results = from item in pages 
                              where (item.Titile ==pagename.Trim()) 
                select item;

                //return the default white background if none found
                if (results.Count()  == 0) return CssSyle;
                         
                //else return the value from cache or database
                return results.FirstOrDefault().BodyCssSyleName.Trim()  ;
                }
                catch (DataCacheException)
                {
                    throw new InvalidOperationException();
                }
                catch (Exception ex)
                {
                    //put cleanup code here
                    throw (ex);
                }
            
              

        }
        }
    }
    
}