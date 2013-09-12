using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


//using Shell.MVC2.Web.Common.ServiceHostFactories;
using Shell.MVC2.DependencyResolution.Ninject.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using NinjectModules = Shell.MVC2.DependencyResolution.Ninject.Modules;
using System.Web.Routing;
using Shell.MVC2.SignalR.Hubs;



namespace Anewluv.Web.Chat
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            //  GlobalHost.ConnectionManager.GetHubContext<Shell.MVC2.Services.Chat.Chat>();
            RouteTable.Routes.MapHubs();

            GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            GlobalHost.ConnectionManager.GetHubContext<ExternalHub>();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
