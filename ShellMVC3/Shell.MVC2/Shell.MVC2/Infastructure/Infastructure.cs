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


    public class ActivityLoggingController : Controller
    { 
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
           //code to store the current page class as a viewbag item so it is accesible for backgroudnds etc
            string pageClass = filterContext.RouteData.Values["controller"].ToString() + "_" + filterContext.RouteData.Values["action"].ToString();
            ViewBag.PageClass = pageClass.ToLower();

            //get the data to be logged 
             var actionDescriptor= filterContext.ActionDescriptor;
             string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;
             string actionName = actionDescriptor.ActionName;
             string userName = filterContext.HttpContext.User.Identity.Name.ToString();
             DateTime timeStamp = filterContext.HttpContext.Timestamp;
             string routeId=string.Empty;
             if (filterContext.RouteData.Values["id"] != null)
             {
                 routeId = filterContext.RouteData.Values["id"].ToString();
             } 
             //connect to the logging service and log it to the GEOdatalogging table

            }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // ... log stuff after execution
        }

 
    }


    /// <summary>
    /// Validation to ensure that a date is within the specified range
    /// At least on of Min and Max must be specified, but the validator
    /// does work with open ranges
    /// By default the attribute also marks the property with DataType.Date - this can be 
    /// disabled with SuppressDataTypeUpdate
    /// </summary> 
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

 
    



    


}