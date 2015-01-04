using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

using Nmedia.Infrastructure.Domain.Data.log;
using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain.Data.ApiKey;

namespace Nmedia.Services.Contracts
{
    [ServiceContract]
    public interface IApiKeyService
    {



      
         
        ////temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/IsValidAPIKey", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> IsValidAPIKey(apikey apikeymodel);

            ////temporary method for use by designer to get the message information formated for them
        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/IsValidAPIKeyAndUser", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<bool> IsValidAPIKeyAndUser(apikey model);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/ValidateOrGenerateNewApiKey", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Guid> ValidateOrGenerateNewApiKey(string service, string username, int useridentifier, string application, Guid key);
   

        //temporary method for use by designer to get the message information formated for them
        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/generateAPIkey/{service}/{username}/{application}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Guid> GenerateAPIkey(string service, string username, int useridentifier, string application);

      
    }
}
