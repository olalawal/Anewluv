using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using System.Web;
using System.Net;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Services.Notification
{
   public  class InfoNotificationService  : IInfoNotificationService 
    {
       private IInfoNotificationRepository _InfoNotificationRepository ;
       private string _apikey;


       public InfoNotificationService(IInfoNotificationRepository InfoNotificationRepository)
            {
                _InfoNotificationRepository = InfoNotificationRepository;
                _apikey  = HttpContext.Current.Request.QueryString["apikey"];
              
              //TO implement API KEY validation
              //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }


       public EmailViewModel getgenericemailviewmodel()
       {
           return _InfoNotificationRepository.getgenericemailviewmodel();
       }

       public EmailViewModel getcontactusemailviewmodel(string from)
       {
           return _InfoNotificationRepository.getcontactusemailviewmodel( from);
       }

       public EmailViewModel getemailmatchesviewmodelbyprofileid(int profileid)
       {
           return _InfoNotificationRepository.getemailmatchesviewmodelbyprofileid(profileid);
       }

       public EmailModel getemailviewmodelbytemplateid(templateenum template)
       {
           return _InfoNotificationRepository.getemailviewmodelbytemplateid(template);
       }

     

    }
}
