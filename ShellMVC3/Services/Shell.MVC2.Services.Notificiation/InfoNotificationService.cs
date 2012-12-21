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

       public EmailViewModel sendmembercreatedemail(RegisterModel model)
       {
        
           try
           {
               return _InfoNotificationRepository.sendmembercreatedemail(model);
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

       public EmailViewModel sendmembercreatedopenidemail(RegisterModel model)
       {
        
           try
           {
               return _InfoNotificationRepository.sendmembercreatedopenidemail(model);
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

       public EmailViewModel sendmemberpasswordchangedemail(LogonViewModel model)
       {

          try
           {
               return _InfoNotificationRepository.sendmemberpasswordchangedemail(model);          
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

       public EmailViewModel sendmemberprofileactivatedemailbyprofileid(string profileid)
       {
        
           try
           {
               return _InfoNotificationRepository.sendmemberprofileactivatedemailbyprofileid(Convert.ToInt32(profileid));
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

       public EmailViewModel sendmemberactivationcoderecoveredemailbyprofileid(string profileid)
       {
        
           try
           {
               return _InfoNotificationRepository.sendmemberactivationcoderecoveredemailbyprofileid(Convert.ToInt32(profileid));
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

       public EmailViewModel sendmemberemailmemssagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
          try
           {
               return _InfoNotificationRepository.sendmemberemailmemssagereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
        
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

       public EmailViewModel sendmemberpeekreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
         
              try
              {
                  return _InfoNotificationRepository.sendmemberpeekreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));

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

       public EmailViewModel sendmemberlikereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
       
           try
           {
               return _InfoNotificationRepository.sendmemberlikereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));

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

       public EmailViewModel sendmemberinterestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {

        
           try
           {
               return _InfoNotificationRepository.sendmemberinterestreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
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

       public EmailViewModel sendmemberchatrequestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           try
           {
               return _InfoNotificationRepository.sendmemberchatrequestreceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
     
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

       public EmailViewModel sendmemberofflinechatmessagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid)
       {
           try
           {
               return _InfoNotificationRepository.sendmemberofflinechatmessagereceivedemailbyprofileid(Convert.ToInt32(recipientprofileid), Convert.ToInt32(senderprofileid));
   
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

       public EmailViewModel sendmemberphotorejectedemailbyprofileid(string profileid, string adminprofileid, photorejectionreasonEnum reason)
       {
           try
           {

               return _InfoNotificationRepository.sendmemberphotorejectedemailbyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(adminprofileid), reason);
   
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

       public EmailViewModel sendmemberphotouploadedemailbyprofileid(string profileid)
       {
           try
           {

               return _InfoNotificationRepository.sendmemberphotouploadedemailbyprofileid(Convert.ToInt32(profileid));
     
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

       public EmailViewModel sendadminspamblockedemailbyprofileid(string blockedprofileid)
       {
           try
           {
                return _InfoNotificationRepository.sendadminspamblockedemailbyprofileid(Convert.ToInt32(blockedprofileid));
    
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
       public EmailViewModel sendemailmatchesemailbyprofileid(string profileid)
       {
           try
           {
               return _InfoNotificationRepository.sendemailmatchesemailbyprofileid(Convert.ToInt32(profileid));
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

       public EmailViewModel sendadminmemberspamblockedemailbyprofileid(string spamblockedprofileid, string reason, string blockedby)
       {
           try
           {
               return _InfoNotificationRepository.sendadminmemberspamblockedemailbyprofileid(Convert.ToInt32(spamblockedprofileid), reason, blockedby);
   
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
       public EmailViewModel sendadminmemberblockedemailbyprofileid(string blockedprofileid, string blockerprofileid)
       {
           try
           {
               return _InfoNotificationRepository.sendadminmemberblockedemailbyprofileid(Convert.ToInt32(blockedprofileid), Convert.ToInt32(blockerprofileid));
     
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
       public EmailViewModel sendadminmemberphotoapprovedemailbyprofileid(string approvedprofileid, string adminprofileid)
       {
           try
           {

               return _InfoNotificationRepository.sendadminmemberphotoapprovedemailbyprofileid(Convert.ToInt32(approvedprofileid), Convert.ToInt32(adminprofileid));
     

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
