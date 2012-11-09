using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

namespace Shell.MVC2.Infrastructure.Interfaces
{
   public  interface IInfoNotificationRepository
    {

        //[OperationContract]
        //int WriteLogEntry(CustomErrorLog logEntry);

        //temporary method for use by designer to get the message information formated for them
        message  sendemailtemplateinfo(messagetypeenum messagetype);


       

    }
}
