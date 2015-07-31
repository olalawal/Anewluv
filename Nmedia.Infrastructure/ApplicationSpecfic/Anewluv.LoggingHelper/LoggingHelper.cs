using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.LoggingHelper
{
    public class LoggingHelper
    {

        //internal enum LogSeverity
        //{
        //    Information = 1,
        //    Warning = 2,
        //    CriticalError = 3,
        //    MaxSeverity = 4
        //}

        //internal static class LoggingHelper
        //{
        //    internal static void LogMessage(LogSeverity severity, string message)
        //    {
        //        bool notify = true;

        //        logseverityEnum severityEnum;
        //        switch (severity)
        //        {
        //            case (LogSeverity.Information):
        //                severityEnum = logseverityEnum.Information;
        //                break;
        //            case (LogSeverity.Warning):
        //                severityEnum = logseverityEnum.Warning;
        //                break;
        //            case (LogSeverity.CriticalError):
        //                severityEnum = logseverityEnum.CriticalError;
        //                notify = true;
        //                break;
        //            default:
        //                severityEnum = logseverityEnum.MaxSeverity;
        //                notify = true;
        //                break;
        //        }

        //        CustomExceptionTypes.DebugException debugEx = new CustomExceptionTypes.DebugException(message);
        //        MvcApplication.logger.WriteSingleEntry(severityEnum, MvcApplication.logEnvironment, debugEx, System.Web.HttpContext.Current.User.Identity.Name, System.Web.HttpContext.Current, notify);
        //    }

        //    internal static void LogInfoMessage(string message)
        //    {
        //        bool notify = true;

        //        MvcApplication.logger.WriteInfoentry(logseverityEnum.Information, MvcApplication.logEnvironment, message, System.Web.HttpContext.Current.User.Identity.Name, System.Web.HttpContext.Current, notify);
        //    }

        //    internal static void LogTimingEvent(long duration, TimingGranularity granularity, string className = "", string methodName = "", string sessionId = "", string userName = "")
        //    {
        //        TimingEvent te = new TimingEvent(MvcApplication.AppName, duration, granularity);
        //        te.Environment = MvcApplication.TimingEnvironment;
        //        te.AssemblyName = MvcApplication.AssemblyName;
        //        te.ClassName = className;
        //        te.MethodName = methodName;
        //        te.SessionId = sessionId;
        //        te.UserName = userName;
        //        MvcApplication.timingLogger.LogTimingEvent(te);
        //    }
        //}
    }
}
