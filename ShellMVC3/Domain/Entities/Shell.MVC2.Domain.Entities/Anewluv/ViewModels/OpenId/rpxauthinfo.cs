//C# Helper Class for Janrain Engage
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;

using System.Linq;
using System.Web;


namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    /// <summary>
    /// RPX Authentication Info
    /// </summary>
    public class rpxauthinfo
    {
        /// <summary>
        /// RPX Profile
        /// </summary>
        public rpxprofile Profile { get; set; }

        /// <summary>
        /// RPX Status
        /// </summary>
        public string Stat { get; set; }
    }

    //TO DO move this to custom exceptions
    /// <summary>
    /// Rpx Exception
    /// </summary>
    public class rpxexception : Exception
    {
        /// <summary>
        /// Rpx Exception
        /// </summary>
        public rpxexception() : base() { }

        /// <summary>
        /// Rpx Exception
        /// </summary>
        /// <param name="message">Message</param>
        public rpxexception(string message) : base(message) { }

        /// <summary>
        /// Rpx Exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner exception</param>
        public rpxexception(string message, Exception innerException) : base(message, innerException) { }
    }

   




}
