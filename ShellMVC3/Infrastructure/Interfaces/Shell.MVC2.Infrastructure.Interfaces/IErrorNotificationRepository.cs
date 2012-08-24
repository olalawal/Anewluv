using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;


namespace Shell.MVC2.Infrastructure.Interfaces
{
   public interface IErrorNotificationRepository    {

      
        int SendErrorMessageToDevelopers(CustomErrorLog customerror);       

        // TODO: Add your service operations here

    }
}
