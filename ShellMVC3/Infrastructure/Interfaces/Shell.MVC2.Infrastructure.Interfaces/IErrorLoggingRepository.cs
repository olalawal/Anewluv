using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Infrastructure.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    public interface IErrorLoggingRepository
    {

      
        string GetData(int value);
       
        //[OperationContract]
        //int WriteLogEntry(CustomErrorLog logEntry);

      
        int WriteCompleteLogEntry(CustomErrorLog logEntry);

     
        //void WriteValue(int logEntryID, string name, object value);

      
        //void WriteLogValue(int logEntryID);


        //void WriteLogValues(int logEntryID);

        //void WriteMessage(int logEntryID, string message);

     
        //void WriteLogMessages(int logEntryID);


        //void WriteLogMessage(int logEntryID);

   
        int TranslateLogSeverity(LogSeverityEnum LogSeverityValue);
    }
}
