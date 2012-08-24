using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Services.Contracts;

namespace Shell.MVC2.Services.Notification
{
   public  class InfoNotificationService  : IInfoNotificationService 
    {
       private IInfoNotificationRepository _infonotificationrepository ;

       public InfoNotificationService(IInfoNotificationRepository infonotificationrepository)
       {
           _infonotificationrepository = infonotificationrepository;
       }
      
       
    }
}
