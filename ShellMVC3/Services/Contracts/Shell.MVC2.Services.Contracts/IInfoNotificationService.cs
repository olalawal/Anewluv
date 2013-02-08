using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;
using System.ServiceModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using System.ServiceModel.Web;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.ServiceModel.Activation;

namespace Shell.MVC2.Services.Contracts
{
    [ServiceContract]
    
    public interface IInfoNotificationService 
    {



        //[OperationContract]
        //string WriteLogEntry(CustomErrorLog logEntry);

        
        //temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/senderrormessage/{addresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        EmailModel senderrormessage(errorlog error, string addresstype);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/sendcontactusemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	      
        EmailViewModel sendcontactusemail(ContactUsModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/sendmembercreatedemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel sendmembercreatedemail(registermodel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/sendmembercreatedopenidemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel sendmembercreatedopenidemail(registermodel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/sendmemberpasswordchangedemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel sendmemberpasswordchangedemail(LogonViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberprofileactivatedemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberprofileactivatedemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberactivationcoderecoveredemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberactivationcoderecoveredemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberemailmemssagereceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberemailmemssagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberpeekreceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberpeekreceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberlikereceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberlikereceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberinterestreceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberinterestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberchatrequestreceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberchatrequestreceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberofflinechatmessagereceivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberofflinechatmessagereceivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/sendmemberphotorejectedemailbyprofileid/{profileid}/{adminprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel sendmemberphotorejectedemailbyprofileid(string profileid, string adminprofileid, photorejectionreasonEnum reason);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendmemberphotouploadedemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendmemberphotouploadedemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendadminspamblockedemailbyprofileid/{blockedprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendadminspamblockedemailbyprofileid(string blockedprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendemailmatchesemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	     
        EmailViewModel sendemailmatchesemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendadminmemberspamblockedemailbyprofileid/{spamblockedprofileid}/{reason}/{blockedby}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendadminmemberspamblockedemailbyprofileid(string spamblockedprofileid, string reason, string blockedby);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendadminmemberblockedemailbyprofileid/{blockedprofileid}/{blockerprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendadminmemberblockedemailbyprofileid(string blockedprofileid, string blockerprofileid);
        //There is no message for Members to know when photos are approved btw

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/sendadminmemberphotoapprovedemailbyprofileid/{approvedprofileid}/{adminprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel sendadminmemberphotoapprovedemailbyprofileid(string approvedprofileid, string adminprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getemailbytemplateid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailModel getemailbytemplateid(templateenum template);
       
    }
}
