using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Services.Contracts;



namespace Shell.MVC2.Services.Notification
{
    public class ErrorNotificationService : IErrorNotificationService
    {
       private IErrorNotificationRepository _errornotificationrepository ;

       public ErrorNotificationService(IErrorNotificationRepository errornotificationrepository)
       {
           _errornotificationrepository = errornotificationrepository;
       }
      
        public int SendErrorMessageToDevelopers(CustomErrorLog customerror)
       {
           return _errornotificationrepository.SendErrorMessageToDevelopers(customerror);
       }
    }
}
