using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel ;
using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Shell.MVC2.Services.Logging
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ErrorLoggingService : IErrorLoggingService
    {

        private  IErrorLoggingRepository _errorloggingrepository;

        public ErrorLoggingService(IErrorLoggingRepository errorloggingrepository)
        {
            _errorloggingrepository = errorloggingrepository;
        }

        
        public string GetData(string value)
        {
           return _errorloggingrepository.GetData(Convert.ToInt32(value));
        }


 
        public int WriteCompleteLogEntry(errorlog logEntry)
        {
            return _errorloggingrepository.WriteCompleteLogEntry(logEntry);
        }



        public int TranslateLogSeverity(logseverityEnum LogSeverityValue)
        {
            return _errorloggingrepository.TranslateLogSeverity(LogSeverityValue);
        }


    }
}
