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
using Shell.MVC2.Web.Common.ServiceHostFactories;

namespace Shell.MVC2.Web.Chat
{
   
    public class Global : System.Web.HttpApplication
    {

        // Background task info , timer if fo chat sweeping nad cleaning
        private static Timer _timer;
        private static readonly TimeSpan _sweepInterval = TimeSpan.FromMinutes(15);


        protected void Application_Start(object sender, EventArgs e)
        {

            var resolver = new SignalR.Ninject.NinjectDependencyResolver(Kernel);

            GlobalHost.ConnectionManager.GetHubContext<Shell.MVC2.Services.Chat.Chat>();
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