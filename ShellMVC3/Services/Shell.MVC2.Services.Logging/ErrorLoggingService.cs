using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure.Interfaces;
using Nmedia.Infrastructure.Domain.Errorlog ;
using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace Shell.MVC2.Services.Logging
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ErrorLoggingService : IErrorLoggingService
    {

        private  IErrorLoggingRepository _Errorloggingrepository;

        public ErrorLoggingService(IErrorLoggingRepository Errorloggingrepository)
        {
            _Errorloggingrepository = Errorloggingrepository;
        }

        
     


 
        public int WriteCompleteLogEntry(Errorlog logEntry)
        {
            return _Errorloggingrepository.WriteCompleteLogEntry(logEntry);
        }



        public int TranslateLogSeverity(logseverityEnum LogSeverityValue)
        {
            return _Errorloggingrepository.TranslateLogSeverity(LogSeverityValue);
        }


    }
}
