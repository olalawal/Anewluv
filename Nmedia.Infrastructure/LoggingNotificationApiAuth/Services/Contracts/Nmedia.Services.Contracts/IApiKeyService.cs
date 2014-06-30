using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Nmedia.Infrastructure.Domain.Data.ViewModels;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain.Data.ApiKey;

namespace Nmedia.Services.Contracts
{
    [ServiceContract]
    public interface IApiKeyService
    {



        //[OperationContract]
        //string WriteLogEntry(errorlog logEntry);


        ////temporary method for use by designer to get the message information formated for them
        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[WebInvoke(UriTemplate = "/IsValidAPIKey/{key}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //bool IsValidAPIKey(string key);


        ////temporary method for use by designer to get the message information formated for them
        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[WebInvoke(UriTemplate = "/generateAPIkey/{service}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //Guid generateAPIkey(string service);

      
         
        ////temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/NonAysncIsValidAPIKey", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> NonAysncIsValidAPIKey(apikey apikeymodel);

        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/IsValidAPIKey/{key}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IAsyncResult BeginIsValidAPIKey(string key, AsyncCallback callback, object asyncState);

        bool EndIsValidAPIKey(IAsyncResult result);

        //temporary method for use by designer to get the message information formated for them
        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/generateAPIkey/{service}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        IAsyncResult BegingenerateAPIkey(string service, AsyncCallback callback, object asyncState);

        Guid EndgenerateAPIkey(IAsyncResult result);

    }
}
