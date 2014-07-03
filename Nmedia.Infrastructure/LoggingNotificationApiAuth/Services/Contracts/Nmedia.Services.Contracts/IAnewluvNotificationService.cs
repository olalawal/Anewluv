using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data.Notification;
using System.Threading.Tasks;

namespace Nmedia.Services.Contracts
{
    [ServiceContract]
    public interface IAnewluvNotificationService
    {



        //[OperationContract]
        //string WriteLogEntry(log logEntry);


        ////temporary method for use by designer to get the message information formated for them
        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[WebInvoke(UriTemplate = "/senderrormessage/{addresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //EmailModel senderrormessage(log error, string addresstype);


        //temporary method for use by designer to get the message information formated for them
        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/senderrormessage/{systemaddresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Task senderrormessage(log error, string systemaddresstype);

       


    }
}
