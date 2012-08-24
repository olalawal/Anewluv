using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel ;
using System.ServiceModel.Web;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IErrorLoggingService
    {
         [OperationContract]
        string GetData(int value);

        //[OperationContract]
        //int WriteLogEntry(CustomErrorLog logEntry);

         [OperationContract]
        [WebInvoke]
        int WriteCompleteLogEntry(CustomErrorLog logEntry);

         [OperationContract]
         [WebInvoke]
         int TranslateLogSeverity(LogSeverityEnum LogSeverityValue);


        //void WriteValue(int logEntryID, string name, object value);


        //void WriteLogValue(int logEntryID);


        //void WriteLogValues(int logEntryID);

        //void WriteMessage(int logEntryID, string message);


        //void WriteLogMessages(int logEntryID);


        //void WriteLogMessage(int logEntryID);

       
    }
}
