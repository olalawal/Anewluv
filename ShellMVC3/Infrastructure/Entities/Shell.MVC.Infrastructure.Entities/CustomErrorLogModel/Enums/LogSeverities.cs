using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum LogSeverityEnum : int
    {
        [EnumMember]
        Information = 1,
        [EnumMember]
        Warning = 2,
        [EnumMember]
        CriticalError = 3,
        [EnumMember]
        MaxSeverity = 4
    }

}

