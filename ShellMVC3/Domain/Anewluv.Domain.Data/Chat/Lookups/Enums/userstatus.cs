using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.Chat
{
     /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum userstatusEnum : int
    {
        [Description("Active")]
        [EnumMember]
        Active,
        [Description("Inactive")]
        [EnumMember]
        Inactive,
        [Description("Offline")]
        [EnumMember]
        Offline       
    }
}