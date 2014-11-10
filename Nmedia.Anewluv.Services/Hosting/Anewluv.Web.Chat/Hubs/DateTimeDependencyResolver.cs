using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anewluv.Web.Chat
{
    public class DateTimeDependencyResolver : IDateTimeDependencyResolver
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
