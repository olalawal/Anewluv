using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Nmedia.Infrastructure.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Domain.Data.errorlog;

namespace Nmedia.Services.Contracts
{
    [ServiceContract]
    public interface IAnewluvNotificationService
    {



        //[OperationContract]
        //string WriteLogEntry(errorlog logEntry);


        ////temporary method for use by designer to get the message information formated for them
        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[WebInvoke(UriTemplate = "/senderrormessage/{addresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //EmailModel senderrormessage(errorlog error, string addresstype);


        //temporary method for use by designer to get the message information formated for them
        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/senderrormessage/{addresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IAsyncResult Beginsenderrormessage(errorlog error, string addresstype, AsyncCallback callback,
                           object state);

        EmailModel Endsenderrormessage(IAsyncResult result);


    }
}
