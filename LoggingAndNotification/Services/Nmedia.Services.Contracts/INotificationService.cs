using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Nmedia.Infrastructure.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Domain.Data.Errorlog;

namespace Nmedia.Services.Contracts
{
    [ServiceContract]
    public interface INotificationService
    {



        //[OperationContract]
        //string WriteLogEntry(Errorlog logEntry);


        //temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/senderrormessage/{addresstype}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        EmailModel senderrormessage(Errorlog error, string addresstype);


    }
}
