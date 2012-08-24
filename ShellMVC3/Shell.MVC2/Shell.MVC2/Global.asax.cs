using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SignalR.Infrastructure; 
using SignalR.Ninject;
using Ninject;
using SignalR;
using Ninject.Web.Mvc;


using Dating.Server.Data;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;
using System.Security.Principal;


using System.Web.Security;

using Shell.MVC2;
using Shell.MVC2.Models;
using Shell.MVC2.Repositories;


using Shell.MVC2.Models.Chat;
using Shell.MVC2.Services.Chat;
using Shell.MVC2.Repositories.Chat;
using Shell.MVC2.Infastructure.Chat;

using System.Diagnostics;


using Omu.Awesome.Mvc;
using System.Reflection;

using System.Threading.Tasks;
using System.Threading;

using Shell.MVC2.Infastructure;
using Shell.MVC2.Helpers;

using LoggingLibrary;

namespace Shell.MVC2
{




    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        // Background task info , timer if fo chat sweeping nad cleaning
        private static Timer _timer;
        private static readonly TimeSpan _sweepInterval = TimeSpan.FromMinutes(15);
        
        //standard MVC routes and filters registration
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");           
            //routes.IgnoreRoute("{*allaxd}", new { allaxd = @".*\.axd(/.*)?" });  //added for mango chat

            //Toggle for start page in different enviroments
#if DISCONECTED

           // routes.MapRoute(
                  //  "Default", // Route name
                  //  "{controller}/{action}/{id}", // URL with parameters
                  //  new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                  //);
            routes.MapRoute(
                    "Default", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Home", action = "SplashPage", id = UrlParameter.Optional } // Parameter defaults
                  );

#else
    //  TO Do toggle between at work and home
           routes.MapRoute(
              "Default",
              "{controller}/{action}/{id}",
              new { controller = "Home", action = "SplashPage", id = UrlParameter.Optional });
            // code to toggle for diff enviroments ends
            //*****************************
#endif


           routes.MapRoute(
               "Admin",
               "Admin/{controller}/{action}/{id}",
               new { controller = "Admin", action = "AdminHome" });
       

           //routes.MapRoute(
           //    "SilverlightSplash",
           //    "{controller}/{action}/{id}",
           //    new { controller = "Home", action = "ShellTestPage", id = UrlParameter.Optional });



            //ROute for jainraid token request
            routes.MapRoute(
           "Authenticate", // Route name
           "{controller}/{action}/{id}", // URL with parameters
           new { controller = "Account", action = "Authenticate", token = UrlParameter.Optional } // Parameter defaults
            );

        

            routes.MapRoute(
                "AboutUs",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "AboutUs" });

            routes.MapRoute(
                "Members",
                "{controller}/{action}/{id}",
                new { controller = "Members", action = "MembersHome", id = UrlParameter.Optional });


            routes.MapRoute(
            "Account",
            "{controller}/{action}/{id}",
            new { controller = "Account", action = "LogOn", id = UrlParameter.Optional });


            routes.MapRoute(
         "Register",
         "{controller}/{action}/{id}",
         new { controller = "Account", action = "Registration", id = UrlParameter.Optional });



            routes.MapRoute(
         "Registration",
         "{controller}/{action}/{id}",
         new { controller = "Account", action = "Registration", id = UrlParameter.Optional });


      //      routes.MapRoute(
      //"EditProfileBasicSettings",
      //"{controller}/{action}/{id}",
      //new { controller = "EditProfile", action = "EditProfileBasicSettings", id = UrlParameter.Optional });


            routes.MapRoute(
              "LogOn",
              "{controller}/{action}/{id}",
              new { controller = "Account", action = "LogOn" });


            routes.MapRoute(
               "ActivateProfile",
               "{controller}/{action}/{id}",
               new { controller = "Account", action = "ActivateProfile", ProfileId = UrlParameter.Optional, ActivationCode = UrlParameter.Optional, ProfileActivated = UrlParameter.Optional });


            routes.MapRoute(
            "QuickProfileMembers",
            "{controller}/{action}/{id}",
            new { controller = "Members", action = "QuickProfileMembers", Paqe = UrlParameter.Optional, ReturnUrl = UrlParameter.Optional, Model = UrlParameter.Optional });


            routes.MapRoute(
             "ActivateProfileCompleted",
             "{controller}/{action}/{id}",
             new { controller = "Account", action = "ActivateProfileCompleted" });



        }

          //public override void Init()
          //  {
          //      this.AuthenticateRequest += new EventHandler(MvcApplication_AuthenticateRequest);
          //      this.PostAuthenticateRequest += new EventHandler(MvcApplication_PostAuthenticateRequest);
          //      base.Init();
          //  }


      #region "Background tasks for entire application"



      #endregion


      #region "Ninject stuff for dependancy Injection

      /// <summary>
      /// Creates the kernel that will manage your application.
      /// </summary>
      /// <returns>The created kernel.</returns>
      protected override IKernel CreateKernel()
      { 
          var kernel = new StandardKernel();
         // kernel.Load(Assembly.GetExecutingAssembly());
          RegisterServices(kernel);

        

         

          return kernel; 
      }

      /// <summary>
      /// Load your modules or register your services here!
      /// </summary>
      /// <param name="kernel">The kernel.</param>
      private static void RegisterServices(IKernel kernel)
      {


          kernel.Bind<UserIdClientIdFactory>()
           .To<UserIdClientIdFactory>()
             .InRequestScope();         

             ////Rest of the other stuff to inject
             //    kernel.Bind<DatingService>()
             //         .To<DatingService >()
             //         .InRequestScope();

             //    kernel.Bind<AnewLuvFTSEntities>()
             //         .To<AnewLuvFTSEntities>()
             //         .InRequestScope();
             //           kernel.Bind<PostalDataService>()
             //        .To<PostalDataService>()
             //        .InRequestScope();

             //kernel.Bind<PostalData2Entities>()
             //         .To<PostalData2Entities>()
             //         .InRequestScope();

             //5/24/2012 added binds for logging for geodata


             //kernel.Bind<LoggerService>()
             //       .To<LoggerService>()
             //       .InRequestScope();

             //kernel.Bind<AnewLuvLogEntities>()
             //         .To<AnewLuvLogEntities>()
             //         .InRequestScope();


                 kernel.Bind<IChatService>()
                       .To<ChatService>()
                       .InRequestScope();

                 //TO DO try and bind it to your custom db here and inject for now try the memmory one
                 kernel.Bind<IChatRepository >()
                     .To<InMemoryChatRepository>()
                    .InSingletonScope();

            //kernal binds for my repostiories which probbaly need to be interfaced as well
                 //kernel.Bind<MembersRepository>()
                 //        .To<MembersRepository>()
                 //        .InRequestScope();


                 //kernel.Bind<GeoRepository>()
                 //           .To<GeoRepository>()
                 //          .InSingletonScope();

                 kernel.Bind<EditProfileRepository>()
                         .To<EditProfileRepository>()
                        .InRequestScope();

                 //kernel.Bind<MailMessageRepository >()
                 //        .To<MailMessageRepository>()
                 //       .InRequestScope();

                 //kernel.Bind<MailModelRepository>()
                 //           .To<MailModelRepository>()
                 //           .InRequestScope();

                 //kernel.Bind<ProfileRepository >()
                 //        .To<ProfileRepository>()
                 //       .InRequestScope();

                 //kernel.Bind<SharedRepository>()
                 //        .To<SharedRepository>()
                 //     .InRequestScope();

          
                 //kernel.Bind<SearchProfileRepository>()
                 //        .To<SearchProfileRepository>()
                 //       .InRequestScope();

           //for local email service

                 kernel.Bind<LocalEmailService>()
                               .To<LocalEmailService>()
                              .InRequestScope();

          //Binding for WCF services
          //****************************************************
                 
            

                 //kernel.Bind<ICryptoService>()
                 //    .To<CryptoService>()
                 //    .InSingletonScope();

                 //kernel.Bind<IResourceProcessor>()
                 //    .To<ResourceProcessor>()
                 //    .InSingletonScope();

                 //kernel.Bind<IApplicationSettings>()
                 //      .To<ApplicationSettings>()
                 //      .InSingletonScope();



        //         return kernel;
        // SignalR.Hosting.AspNet.AspNetHost.DependencyResolver.Register(typeof(IConnectionIdFactory), () => new UserIdClientIdFactory());



             SignalR.Hosting.AspNet.AspNetHost.SetResolver(new SignalR.Ninject.NinjectDependencyResolver(kernel));

          //now all the services are regiseters reslove the kernal and access it
             var resolver = new SignalR.Ninject.NinjectDependencyResolver(kernel);

           // Start the sweeper
           var repositoryFactory = new Func<IChatRepository>(() => kernel.Get<IChatRepository>());
           _timer = new Timer(_ => ChatInfrastructure.Sweep(repositoryFactory, resolver), null, _sweepInterval, _sweepInterval);

           
      }

      #endregion

      protected override void OnApplicationStarted() 
      {
                
          base.OnApplicationStarted();
          //for project awesome
          ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();
          AreaRegistration.RegisterAllAreas(); 
          RegisterGlobalFilters(GlobalFilters.Filters);
          RegisterRoutes(RouteTable.Routes);
          //3-30-2012 added paths for razor partials
          ViewEngines.Engines.Clear();

          ViewEngines.Engines.Add (new MyViewEngineWithPartialPaths());
          //5-8-2012 added custom elmah error handling controller base 
          //6-7-2012 updated error handling
          //ControllerBuilder.Current.SetControllerFactory(new ErrorHandlingControllerFactory());
      }



      //protected override void OnApplicationStarted()

      //{
      //    //for project awesome
      //    ModelMetadataProviders.Current = new AwesomeModelMetadataProvider();

      //    AreaRegistration.RegisterAllAreas();
      //    RegisterGlobalFilters(GlobalFilters.Filters);
      //    RegisterRoutes(RouteTable.Routes);
      
      //}


      //protected void Application_Start()
      //  {
           

          

      //    //ninject calls to create the kernal etc
      //  //    IKernel kernel = CreateKernel();

      //    //TO DO using signal IR resolver
      //   //   var resolver = new SignalR.Ninject.NinjectDependencyResolver(kernel);
      //  //    SignalR.Hosting.AspNet.AspNetHost.SetResolver(resolver);

      //    //TO DO i think we would be using the SignalIR dependency reslover for everything or 
      //    //maybe have a separate resolver it seems for the web repostioreis
      //     // DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

      //  }
        
        public void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
       
        }

        //TO DO fix the session end deal
        public void Session_End(object sender, EventArgs e)
        {


            try
            {


                Session.Abandon();


                //initialize the dating service here
              //  var datingservice = new DatingService().Initialize();
              //  var datingservicecontext = new AnewLuvFTSEntities();
           // string username = HttpContext.Current.User.ToString();

               // HttpContext contextsession = ((HttpApplication)sender).Context; 

          //  MembersViewModel model = new MembersViewModel();

         //   contextsession.Request.QueryString.ToString();
              //  model =(MembersViewModel)contextsession.Session["MembersViewModel"] ;

              //  using (DatingService   context = new DatingService())
              //  {
              //      //get the profile ID i.e email from the user context username
              //  //    string profileID = context.GetProfileIdbyUsername(username);

              //      //now you can log it
              ////    context.UpdateUserLogoutTime(username, HttpContext.Current.Session.SessionID);

                 
              //  }

               // FormsAuth.SignOut(); 
            }

                //sent error email
            catch { }
            // Code that runs when a session ends.           

        }
                
        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
                  //  NerdIdentity id = new NerdIdentity(ticket);
                   // System.Web.Security.FormsAuthenticationTicket ticket;
                   // GenericPrincipal prin = new GenericPrincipal(principal.Identity , null);
                  //  HttpContext.Current.User = prin;
                }
            }
        }

        void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
        }
	
        void Application_Error(object sender, EventArgs e)
        {
       
         
            //get reference to the source of the exception chain
        Exception ex = Server.GetLastError().GetBaseException();
            

   //log the details of the exception and page state to the
   //Windows 2000 Event Log

        var temp = ex.ToString();
        // Get stack trace for the exception with source file information 
    //    var st = new StackTrace(ex, true);
        // Get the top stack frame 
    //    var frame = st.GetFrame(0);
        // Get the line number from the stack frame 
     //   var line = frame.GetFileLineNumber();

       // string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7); 

 //  EventLog.WriteEntry("Test Web",
   //  "MESSAGE: " + ex.Message.ToString() + 
  //   "\nSOURCE: " + ex.Source +
  //   "\nFORM: " + Request.Form.ToString() + 
  //   "\nQUERYSTRING: " + Request.QueryString.ToString() +
   //  "\nTARGETSITE: " + ex.TargetSite +
   //  "\nSTACKTRACE: " + ex.StackTrace, 
    // EventLogEntryType.Error);

   //Insert optional email notification here...
}




    }
}