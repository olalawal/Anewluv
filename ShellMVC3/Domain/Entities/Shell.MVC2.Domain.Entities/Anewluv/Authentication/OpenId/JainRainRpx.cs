
using System;
using System.Collections;
using System.Collections.Generic;


using System.Linq;
using System.Web;
//using System.Web.Mvc;
//using System.Web.Script.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.Authentication.rpx
{


    /// <summary>
    /// RPX Profile
    /// </summary>
    public class RpxProfile
    {
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Preferred username
        /// </summary>
        public string PreferredUsername { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Identifier
        /// </summary>
        public string Identifier { get; set; }


        /// <summary>
        /// verifiedEmail
        /// </summary>
        public string verifiedEmail { get; set; }

        /// <summary>
        /// photo
        /// </summary>
        public string photo { get; set; }


        /// <summary>
        /// gender
        /// </summary>
        public string gender { get; set; }


        /// <summary>
        /// Birthday
        /// </summary>
        public string birthday { get; set; }



    }
    /// <summary>
    /// RPX Authentication Info
    /// </summary>
    public class RpxAuthInfo
    {
        /// <summary>
        /// RPX Profile
        /// </summary>
        public RpxProfile Profile { get; set; }

        /// <summary>
        /// RPX Status
        /// </summary>
        public string Stat { get; set; }
    }

    /// <summary>
    /// Rpx Exception
    /// </summary>
    public class RpxException : Exception
    {
        /// <summary>
        /// Rpx Exception
        /// </summary>
        public RpxException() : base() { }

        /// <summary>
        /// Rpx Exception
        /// </summary>
        /// <param name="message">Message</param>
        public RpxException(string message) : base(message) { }

        /// <summary>
        /// Rpx Exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner exception</param>
        public RpxException(string message, Exception innerException) : base(message, innerException) { }
    }
}
