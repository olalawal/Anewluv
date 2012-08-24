using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Infrastructure.Interfaces ;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

namespace Shell.MVC2.Data
{
   public class InfoNotificationRepository : IInfoNotificationRepository
    {

        private NotificationContext _notificationcontext;

        public InfoNotificationRepository(NotificationContext notificationcontext)
        {
            _notificationcontext = notificationcontext;
        }


    }
}
