using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.SignalR.Hubs
{
    public interface IDateTimeDependencyResolver
    {
        DateTime GetDateTime();
    }
}
