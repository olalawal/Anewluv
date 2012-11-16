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

namespace Shell.MVC2.Services.Contracts
{
    [ServiceContract]
    public interface IInfoNotificationService 
    {



        //[OperationContract]
        //int WriteLogEntry(CustomErrorLog logEntry);

        //temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgenericemailviewmodel", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getgenericemailviewmodel();

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcontactusemailviewmodel/{from}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getcontactusemailviewmodel(string from);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getemailmatchesviewmodelbyprofileid/{profileid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailViewModel getemailmatchesviewmodelbyprofileid(string profileid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/getemailviewmodelbytemplateid", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]	
        EmailModel getemailviewmodelbytemplateid(templateenum template);

    }
}
