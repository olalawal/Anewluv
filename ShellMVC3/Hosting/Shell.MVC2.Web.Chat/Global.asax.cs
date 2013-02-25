using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading.Tasks ;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Shell.MVC2.Services.Chat;
//using Shell.MVC2.Web.Common.ServiceHostFactories;
using Shell.MVC2.DependencyResolution.Ninject.Infrastructure;
using Ninject;
using Microsoft.AspNet.SignalR.Infrastructure;
using NinjectModules = Shell.MVC2.DependencyResolution.Ninject.Modules;
using System.Web.Routing;

namespace Shell.MVC2.Web.Chat
{
   
    public class Global : System.Web.HttpApplication
    {

        // Background task info , timer if fo chat sweeping nad cleaning
        private static Timer _timer;
        private static readonly TimeSpan _sweepInterval = TimeSpan.FromMinutes(15);


        protected void Application_Start(object sender, EventArgs e)
        {                       

         //  GlobalHost.ConnectionManager.GetHubContext<Shell.MVC2.Services.Chat.Chat>();
            RouteTable.Routes.MapHubs();
            
            GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

        }

        public void Configuration()
        {
            //var settings = new ApplicationSettings();

            //if (settings.MigrateDatabase)
            //{
            //    // Perform the required migrations
            //    DoMigrations();
            //}

            // var kernel = new StandardKernel(new[] { new FactoryModule() });
            //var kernel = new StandardKernel();

           
            // We're doing this manually since we want the chat repository to be shared
            // between the chat service and the chat hub itself
            
           // SetupSignalR(kernel);
            //SetupWebApi(kernel, app);
            //SetupMiddleware(app);
           // SetupNancy(kernel, app);

           // SetupErrorHandling();
        }

        private static void SetupSignalR(IKernel kernel)
        {
            //var resolver = new NinjectSignalRDependencyResolver(kernel);
            //var connectionManager = resolver.Resolve<IConnectionManager>();

            //kernel.Bind<IConnectionManager>()
            //      .ToConstant(connectionManager);

            //var config = new HubConfiguration
            //{
            //    Resolver = resolver,
            //    EnableDetailedErrors = true
            //};

            //app.MapHubs(config);

            //Statr back gorund work stuff
            //StartBackgroundWork(kernel, resolver);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}