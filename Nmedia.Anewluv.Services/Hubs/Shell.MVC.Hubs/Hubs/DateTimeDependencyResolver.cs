using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.SignalR.Hubs
{
    public class DateTimeDependencyResolver : IDateTimeDependencyResolver
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
