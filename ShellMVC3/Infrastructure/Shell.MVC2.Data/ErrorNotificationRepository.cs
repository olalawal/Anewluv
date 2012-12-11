using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces ;

namespace Shell.MVC2.Data
{
 


    using System.Net.Mail;

    // NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
    public class ErrorNotificationRepository : IErrorNotificationRepository
    {

        private NotificationContext _notificationcontext;

        public ErrorNotificationRepository(NotificationContext notificationcontext)
        {
            _notificationcontext = notificationcontext;
        }


 

      

    }

}
