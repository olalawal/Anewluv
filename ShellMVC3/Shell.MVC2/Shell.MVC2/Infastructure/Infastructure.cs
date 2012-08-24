using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
//using Microsoft.Web.Mvc;

using Microsoft.VisualBasic;

using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Text;
using Shell.MVC2.Models;





namespace Shell.MVC2.Helpers
{


    public class CustomBaseController : Controller
    { 
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string pageClass = filterContext.RouteData.Values["controller"].ToString() + "_" + filterContext.RouteData.Values["action"].ToString();
            ViewBag.PageClass = pageClass.ToLower();
 } 
    }




    
     


    /// <summary>
    /// Validation to ensure that a date is within the specified range
    /// At least on of Min and Max must be specified, but the validator
    /// does work with open ranges
    /// By default the attribute also marks the property with DataType.Date - this can be 
    /// disabled with SuppressDataTypeUpdate
    /// </summary>
    /// 

   // public class DateAttribute : RangeAttribute {
    //    public DateAttribute() : base(typeof(DateTime), DateTime.Now.AddYears(-20).ToShortDateString(), DateTime.Now.AddYears(2).ToShortDateString()) { } } 


 

    public class DateRangeAttribute : ValidationAttribute, IClientValidatable, IMetadataAware
    {
        private const string DateFormat = "yyyy/MM/dd";
        private static class DefaultErrorMessages
        {
            public const string Range = "'{0}' must be a date between {1:d} and {2:d}.";
            public const string Min = "'{0}' must be a date after {1:d}";
            public const string Max = "'{0}' must be a date before {2:d}.";
        }
        private DateTime _minDate = DateTime.MinValue;
        private DateTime _maxDate = DateTime.MaxValue;

        /// <summary>
        /// String representation of the Min Date (yyyy/MM/dd)
        /// </summary>
        public string Min
        {
            get { return FormatDate(_minDate, DateTime.MinValue); }
            set { _minDate = ParseDate(value, DateTime.MinValue); }
        }
        /// <summary>
        /// String representation of the Max Date (yyyy/MM/dd)
        /// </summary>
        public string Max
        {
            get { return FormatDate(_maxDate, DateTime.MaxValue); }
            set { _maxDate = ParseDate(value, DateTime.MaxValue); }
        }

        public bool SuppressDataTypeUpdate { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }
            DateTime dateValue = (DateTime)value;
            return _minDate <= dateValue && dateValue <= _maxDate;
        }
        public override string FormatErrorMessage(string name)
        {
            EnsureErrorMessage();
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, _minDate, _maxDate);
        }

        private void EnsureErrorMessage()
        {
            //  normally we'd pass the default error message in the constructor
            // but here the default message depends on whether we have one or both of the range ends
            // This method is used to inject a default error message if none has been set
            if (string.IsNullOrEmpty(ErrorMessage)
                && string.IsNullOrEmpty(ErrorMessageResourceName)
                && ErrorMessageResourceType == null)
            {
                string message;
                if (_minDate == DateTime.MinValue)
                {
                    if (_maxDate == DateTime.MaxValue)
                    {
                        throw new ArgumentException("Must set at least one of Min and Max");
                    }
                    message = DefaultErrorMessages.Max;
                }
                else
                {
                    if (_maxDate == DateTime.MaxValue)
                    {
                        message = DefaultErrorMessages.Min;
                    }
                    else
                    {
                        message = DefaultErrorMessages.Range;
                    }
                }
                ErrorMessage = message;
            }
        }

        private static DateTime ParseDate(string dateValue, DateTime defaultValue)
        {
            if (string.IsNullOrWhiteSpace(dateValue))
            {
                return defaultValue;
            }
            return DateTime.ParseExact(dateValue, DateFormat, CultureInfo.InvariantCulture);
        }
        private static string FormatDate(DateTime dateTime, DateTime defaultValue)
        {
            if (dateTime == defaultValue)
            {
                return "";
            }
            return dateTime.ToString(DateFormat);
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]
                       {
                           new ModelClientValidationRangeDateRule(FormatErrorMessage(metadata.GetDisplayName()), _minDate,
                                                              _maxDate)
                       };
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            if (!SuppressDataTypeUpdate)
            {
                metadata.DataTypeName = "Date";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]

    public class EmailAddressAttribute : DataTypeAttribute
    {

        private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);



        public EmailAddressAttribute()
            : base(DataType.EmailAddress)
        {

        }



        public override bool IsValid(object value)
        {

            string str = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(str))

                return true;



            Match match = regex.Match(str);

            return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));

        }

    }

 
    ////TO DO
    ////utils that should be moved to Common when its converted to C#
    //    public class Utils
    //    {

          


            
    //        #region "Logging"
    //        public static void LogInfo(string message)
    //        {
    //            LogMessage(message, TraceLevel.Info);
    //        }

    //        public static void LogMessage(MethodBase method, string message)
    //        {
    //            LogMessage("ERROR IN " + method.DeclaringType.ToString() + ":" + method.Name + ":" + message, TraceLevel.Error);
    //        }
    //        public static void LogMessage(string application, MethodBase method, string message)
    //        {
    //            LogMessage(application, "ERROR IN " + method.DeclaringType.ToString() + ":" + method.Name + ":" + message, TraceLevel.Error);
    //        }
    //        public static void LogMessage(string message, TraceLevel level)
    //        {
    //            try
    //            {
    //                switch (level)
    //                {
    //                    case TraceLevel.Error:
    //                        LogEventLog("Webtox", message, EventLogEntryType.Error);
    //                        break;
    //                    case TraceLevel.Info:
    //                        LogEventLog("Webtox", message, EventLogEntryType.Information);
    //                        break;
    //                    case TraceLevel.Warning:
    //                        LogEventLog("Webtox", message, EventLogEntryType.Warning);
    //                        break;
    //                }
    //            }
    //            catch
    //            {
    //            }
    //        }

    //        public static void LogMessage(string application, string message, TraceLevel level)
    //        {
    //            try
    //            {
    //                switch (level)
    //                {
    //                    case TraceLevel.Error:
    //                        LogEventLog(application, message, EventLogEntryType.Error);
    //                        break;
    //                    case TraceLevel.Info:
    //                        LogEventLog(application, message, EventLogEntryType.Information);
    //                        break;
    //                    case TraceLevel.Warning:
    //                        LogEventLog(application, message, EventLogEntryType.Warning);
    //                        break;
    //                }
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        public static bool LogEventLog(string sApp, string logMessage, EventLogEntryType EventType = EventLogEntryType.Error)
    //        {

    //            EventLog objEventLog = new EventLog();

    //            try
    //            {
    //                if (!EventLog.SourceExists(sApp))
    //                {
    //                    EventLog.CreateEventSource(sApp, "Application");
    //                }
    //                objEventLog.Source = sApp;
    //                objEventLog.WriteEntry(logMessage, EventType);
    //                return true;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //                // log the execption message
    //                //return false;
    //            }

    //        }

    //         public static bool LogEmailAndEventLog(string sApp, string logMessage, EventLogEntryType EventType = EventLogEntryType.Error)
    //        {

    //            EventLog objEventLog = new EventLog();

    //            try
    //            {
    //                if (!EventLog.SourceExists(sApp))
    //                {
    //                    EventLog.CreateEventSource(sApp, "Application");
    //                }
    //                objEventLog.Source = sApp;
    //                objEventLog.WriteEntry(logMessage, EventType);

    //                return true;
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //                // log the execption message
    //                //return false;
    //            }

    //        }

    //        #endregion

    //        #region "Miscellanous"


    //        public static string FormatException(Exception ex)
    //        {

    //            string sTemp = "Message: " + ex.Message + "  Stack: " + ex.StackTrace;
    //            return sTemp;

    //        }
    //        //public static string FormatSQLException(SqlException ex)
    //        //{

    //        //    string sTemp = "Message: " + ex.Message + "  Stack: " + ex.StackTrace;
    //        //    return sTemp;

    //        //}


    //        #endregion


    //    }



    


}