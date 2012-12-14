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
using Shell.MVC2.Domain.Entities.Anewluv;
using System.ServiceModel.Activation;
using System.ServiceModel;



namespace Shell.MVC2.Services.Notification
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
   public  class InfoNotificationService  : IInfoNotificationService 
    {
       private IInfoNotificationRepository _InfoNotificationRepository ;
       //private string _apikey;


       public InfoNotificationService(IInfoNotificationRepository InfoNotificationRepository)
            {
                _InfoNotificationRepository = InfoNotificationRepository;
       //         _apikey  = HttpContext.Current.Request.QueryString["apikey"];
              
              //TO implement API KEY validation
              //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
               
            }


       public EmailModel senderrormessage(CustomErrorLog error, string addresstype)
       {

           return _InfoNotificationRepository.senderrormessage(error,((addresstypeenum)Enum.Parse(typeof(addresstypeenum),addresstype)));

       }

       public EmailViewModel sendcontactusemail(ContactUsModel model)
       {

           return _InfoNotificationRepository.sendcontactusemail(model);
       }

       public EmailViewModel getmembercreatedemail(RegisterModel model)
       {
           return _InfoNotificationRepository.getmembercreatedemail(model);
       }

       public EmailViewModel getmembercreatedopenidemail(RegisterModel model)
       {
           return _InfoNotificationRepository.getmembercreatedopenidemail(model);

       }

       public EmailViewModel getmemberpasswordchangedemail(LogonViewModel model)
       {

           return _InfoNotificationRepository.getmemberpasswordchangedemail(model);
       }

       public EmailViewModel getmemberprofileactivatedemailbyprofileid(string profileid)
       {
           return _InfoNotificationRepository.getmemberprofileactivatedemailbyprofileid(Convert.ToInt32(profileid));
       }

       public EmailViewModel getmemberactivationcoderecoveredemailbyprofileid(string profileid)
       {

           return _InfoNotificationRepository.getmemberactivationcoderecoveredemailbyprofileid(Convert.ToInt32(profileid));
       }

       public EmailViewModel getmemberemailmemssagerecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           return _InfoNotificationRepository.getmemberemailmemssagerecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
       }

       public EmailViewModel getmemberpeekrecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
              return _InfoNotificationRepository.getmemberpeekrecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
       }

       public EmailViewModel getmemberlikerecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           return _InfoNotificationRepository.getmemberlikerecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));

       }

       public EmailViewModel getmemberinterestrecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {

           return _InfoNotificationRepository.getmemberinterestrecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
       }

       public EmailViewModel getmemberchatrequestrecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {

           return _InfoNotificationRepository.getmemberchatrequestrecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
       }

       public EmailViewModel getmemberofflinechatmessagerecivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {

           return _InfoNotificationRepository.getmemberofflinechatmessagerecivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
       }

       public EmailViewModel getmemberphotorejectedemailbyprofileid(string profileid, string adminprofileid, photorejectionreasonEnum reason)
       {

           return _InfoNotificationRepository.getmemberphotorejectedemailbyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(adminprofileid), reason);
       }

       public EmailViewModel getmemberphotouploadedemailbyprofileid(string profileid)
       {
           return _InfoNotificationRepository.getmemberphotouploadedemailbyprofileid(Convert.ToInt32(profileid));
       }

       public EmailViewModel getadminspamblockedemailbyprofileid(string blockedprofileid)
       {
           return _InfoNotificationRepository.getadminspamblockedemailbyprofileid(Convert.ToInt32(blockedprofileid));
       }

       //TO DO revisit this to send back the matches here
       public EmailViewModel getemailmatchesemailbyprofileid(string profileid)
       {
           return _InfoNotificationRepository.getemailmatchesemailbyprofileid(Convert.ToInt32(profileid));

       }

       public EmailViewModel getadminmemberspamblockedemailbyprofileid(string spamblockedprofileid, string reason, string blockedby)
       {
           return _InfoNotificationRepository.getadminmemberspamblockedemailbyprofileid(Convert.ToInt32(spamblockedprofileid),reason ,blockedby );
       }
       public EmailViewModel getadminmemberblockedemailbyprofileid(string blockedprofileid, string blockerprofileid)
       {
           return _InfoNotificationRepository.getadminmemberblockedemailbyprofileid(Convert.ToInt32(blockedprofileid), Convert.ToInt32(blockerprofileid));
       }

       //There is no message for Members to know when photos are approved btw
       public EmailViewModel getadminmemberphotoapprovedemailbyprofileid(string approvedprofileid, string adminprofileid)
       {
           return _InfoNotificationRepository.getadminmemberphotoapprovedemailbyprofileid(Convert.ToInt32(approvedprofileid), Convert.ToInt32(adminprofileid));
       }

       public EmailModel getemailbytemplateid(templateenum template)
       {
           return _InfoNotificationRepository.getemailbytemplateid(template);
       }
     

    }
}
