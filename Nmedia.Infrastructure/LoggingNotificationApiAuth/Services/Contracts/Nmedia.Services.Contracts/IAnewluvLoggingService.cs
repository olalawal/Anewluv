using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using Nmedia.Infrastructure.Domain.Data.log;
using System.Threading.Tasks;

namespace Nmedia.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAnewluvLoggingService
    {


        //[OperationContract]
        //int WriteLogEntry(log logEntry);


        //[OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        //[ServiceKnownType(typeof(log))]
        //[WebInvoke(UriTemplate = "/WriteCompleteLogEntry", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //int WriteCompleteLogEntry(log logEntry);


        [OperationContract(AsyncPattern = true), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(log))]
        [WebInvoke(UriTemplate = "/WriteCompleteLogEntry", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Task WriteCompleteLogEntry(log logEntry);

        int EndWriteCompleteLogEntry(IAsyncResult result);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [ServiceKnownType(typeof(logseverityEnum))]
        [WebInvoke(UriTemplate = "/TranslateLogSeverity", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int TranslateLogSeverity(logseverityEnum LogSeverityValue);


        //void WriteValue(int logEntryID, string name, object value);


        //void WriteLogValue(int logEntryID);


        //void WriteLogValues(int logEntryID);

        //void WriteMessage(int logEntryID, string message);


        //void WriteLogMessages(int logEntryID);


        //void WriteLogMessage(int logEntryID);


    }
}
