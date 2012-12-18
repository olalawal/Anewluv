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

    
           try
           {
               return _InfoNotificationRepository.senderrormessage(error, ((addresstypeenum)Enum.Parse(typeof(addresstypeenum), addresstype)));

           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }

       }

       public EmailViewModel sendcontactusemail(ContactUsModel model)
       {
           try
           {
               return _InfoNotificationRepository.sendcontactusemail(model);
           }
           catch (Exception ex)
           {     
             //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail),faultreason );
           }
       }

       public EmailViewModel getmembercreatedemail(RegisterModel model)
       {
        
           try
           {
               return _InfoNotificationRepository.getmembercreatedemail(model);
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmembercreatedopenidemail(RegisterModel model)
       {
        
           try
           {
               return _InfoNotificationRepository.getmembercreatedopenidemail(model);
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberpasswordchangedemail(LogonViewModel model)
       {

          try
           {
               return _InfoNotificationRepository.getmemberpasswordchangedemail(model);          
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberprofileactivatedemailbyprofileid(string profileid)
       {
        
           try
           {
               return _InfoNotificationRepository.getmemberprofileactivatedemailbyprofileid(Convert.ToInt32(profileid));
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberactivationcoderecoveredemailbyprofileid(string profileid)
       {
        
           try
           {
               return _InfoNotificationRepository.getmemberactivationcoderecoveredemailbyprofileid(Convert.ToInt32(profileid));
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberemailmemssagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
          try
           {
               return _InfoNotificationRepository.getmemberemailmemssagereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
        
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberpeekreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
         
              try
              {
                  return _InfoNotificationRepository.getmemberpeekreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));

              }
              catch (Exception ex)
              {
                  //can parse the error to build a more custom error mssage and populate fualt faultreason
                  FaultReason faultreason = new FaultReason("Generic Error");
                  string ErrorMessage = "";
                  string ErrorDetail = "ErrorMessage: " + ex.Message;
                  throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
              }
       }

       public EmailViewModel getmemberlikereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
       
           try
           {
               return _InfoNotificationRepository.getmemberlikereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));

           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberinterestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {

        
           try
           {
               return _InfoNotificationRepository.getmemberinterestreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getmemberchatrequestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           try
           {
               return _InfoNotificationRepository.getmemberchatrequestreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
     
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
      }

       public EmailViewModel getmemberofflinechatmessagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           try
           {
               return _InfoNotificationRepository.getmemberofflinechatmessagereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
   
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
      }

       public EmailViewModel getmemberphotorejectedemailbyprofileid(string profileid, string adminprofileid, photorejectionreasonEnum reason)
       {
           try
           {

               return _InfoNotificationRepository.getmemberphotorejectedemailbyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(adminprofileid), reason);
   
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
      }

       public EmailViewModel getmemberphotouploadedemailbyprofileid(string profileid)
       {
           try
           {

               return _InfoNotificationRepository.getmemberphotouploadedemailbyprofileid(Convert.ToInt32(profileid));
     
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       public EmailViewModel getadminspamblockedemailbyprofileid(string blockedprofileid)
       {
           try
           {
                return _InfoNotificationRepository.getadminspamblockedemailbyprofileid(Convert.ToInt32(blockedprofileid));
    
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
       }

       //TO DO revisit this to send back the matches here
       public EmailViewModel getemailmatchesemailbyprofileid(string profileid)
       {
           try
           {
               return _InfoNotificationRepository.getemailmatchesemailbyprofileid(Convert.ToInt32(profileid));
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
         

       }

       public EmailViewModel getadminmemberspamblockedemailbyprofileid(string spamblockedprofileid, string reason, string blockedby)
       {
           try
           {
               return _InfoNotificationRepository.getadminmemberspamblockedemailbyprofileid(Convert.ToInt32(spamblockedprofileid), reason, blockedby);
   
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
      }
       public EmailViewModel getadminmemberblockedemailbyprofileid(string blockedprofileid, string blockerprofileid)
       {
           try
           {
               return _InfoNotificationRepository.getadminmemberblockedemailbyprofileid(Convert.ToInt32(blockedprofileid), Convert.ToInt32(blockerprofileid));
     
           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
  }

       //There is no message for Members to know when photos are approved btw
       public EmailViewModel getadminmemberphotoapprovedemailbyprofileid(string approvedprofileid, string adminprofileid)
       {
           try
           {

               return _InfoNotificationRepository.getadminmemberphotoapprovedemailbyprofileid(Convert.ToInt32(approvedprofileid), Convert.ToInt32(adminprofileid));
     

           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }
  }

       public EmailModel getemailbytemplateid(templateenum template)
       {

           try
           {
               return _InfoNotificationRepository.getemailbytemplateid(template);

           }
           catch (Exception ex)
           {
               //can parse the error to build a more custom error mssage and populate fualt faultreason
               FaultReason faultreason = new FaultReason("Generic Error");
               string ErrorMessage = "";
               string ErrorDetail = "ErrorMessage: " + ex.Message;
               throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
           }

       }
     

    }
}
