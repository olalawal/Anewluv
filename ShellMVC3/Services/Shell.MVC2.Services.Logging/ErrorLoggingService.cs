using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel ;
using Shell.MVC2.Services.Contracts;

namespace Shell.MVC2.Services.Logging
{

    public class ErrorLoggingService : IErrorLoggingService
    {

        private  IErrorLoggingRepository _errorloggingrepository;

        public ErrorLoggingService(IErrorLoggingRepository errorloggingrepository)
        {
            _errorloggingrepository = errorloggingrepository;
        }

        
        public string GetData(int value)
        {
           return _errorloggingrepository.GetData(value);
        }


 
        public int WriteCompleteLogEntry(CustomErrorLog logEntry)
        {
            return _errorloggingrepository.WriteCompleteLogEntry(logEntry);
        }



        public int TranslateLogSeverity(LogSeverityEnum LogSeverityValue)
        {
            return _errorloggingrepository.TranslateLogSeverity(LogSeverityValue);
        }


    }
}
