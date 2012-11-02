using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum LogSeverityEnum : int
    {
        [Description("NotSet")][EnumMember]
        Information = 1,
        [Description("NotSet")][EnumMember]
        Warning = 2,
        [Description("NotSet")][EnumMember]
        CriticalError = 3,
        [Description("NotSet")][EnumMember]
        MaxSeverity = 4
    }

}

