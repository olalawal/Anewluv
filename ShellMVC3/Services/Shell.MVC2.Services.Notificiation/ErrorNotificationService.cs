using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Services.Contracts;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;



namespace Shell.MVC2.Services.Notification
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
    public class ErrorNotificationService : IErrorNotificationService
    {
       private IErrorNotificationRepository _errornotificationrepository ;
       //private string _apikey;

       public ErrorNotificationService(IErrorNotificationRepository errornotificationrepository)
       {
           _errornotificationrepository = errornotificationrepository;
        //   _apikey = HttpContext.Current.Request.QueryString["apikey"];

           //TO implement API KEY validation
           //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
       }
      
        public int SendErrorMessageToDevelopers(CustomErrorLog customerror)
       {
           return _errornotificationrepository.SendErrorMessageToDevelopers(customerror);
       }
    }
}
