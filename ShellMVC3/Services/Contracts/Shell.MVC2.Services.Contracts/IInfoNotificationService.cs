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

namespace Shell.MVC2.Services.Contracts
{
    [ServiceContract]
    public interface IInfoNotificationService 
    {



        //[OperationContract]
        //string WriteLogEntry(CustomErrorLog logEntry);

  
        //temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getgenericerroremail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        EmailViewModel getgenericerroremail(CustomErrorLog error);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getcontactusemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	      
        EmailViewModel getcontactusemail(ContactUsModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmembercreatedemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel getmembercreatedemail(RegisterModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmembercreatedopenidemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel getmembercreatedopenidemail(RegisterModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmemberpasswordchangedemail", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel getmemberpasswordchangedemail(LogonViewModel model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberprofileactivatedemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberprofileactivatedemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberactivationcoderecoveredemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberactivationcoderecoveredemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberemailmemssagerecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberemailmemssagerecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberpeekrecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberpeekrecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberlikerecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberlikerecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberinterestrecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberinterestrecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberchatrequestrecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberchatrequestrecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberofflinechatmessagerecivedemailbyprofileid/{recipientprofileid}/{senderprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberofflinechatmessagerecivedemailbyprofileid(string recipientprofileid, string senderprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getmemberphotorejectedemailbyprofileid/{profileid}/{adminprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	 
        EmailViewModel getmemberphotorejectedemailbyprofileid(string profileid, string adminprofileid, photorejectionreasonEnum reason);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getmemberphotouploadedemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getmemberphotouploadedemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getadminspamblockedemailbyprofileid/{blockedprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getadminspamblockedemailbyprofileid(string blockedprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getemailmatchesemailbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	     
        EmailViewModel getemailmatchesemailbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getadminmemberspamblockedemailbyprofileid/{spamblockedprofileid}/{reason}/{blockedby}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getadminmemberspamblockedemailbyprofileid(string spamblockedprofileid, string reason, string blockedby);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getadminmemberblockedemailbyprofileid/{blockedprofileid}/{blockerprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getadminmemberblockedemailbyprofileid(string blockedprofileid, string blockerprofileid);
        //There is no message for Members to know when photos are approved btw

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getadminmemberphotoapprovedemailbyprofileid/{approvedprofileid}/{adminprofileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getadminmemberphotoapprovedemailbyprofileid(string approvedprofileid, string adminprofileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getemailbytemplateid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailModel getemailbytemplateid(templateenum template);
       
    }
}
