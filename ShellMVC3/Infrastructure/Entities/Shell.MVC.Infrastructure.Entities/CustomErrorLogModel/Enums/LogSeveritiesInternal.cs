using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Errorlog
{
   
    /// <summary>
    /// This is used to convert .NET log severity levels to our values
    /// </summary>
    /// 
    [DataContract]
    public enum logseverityinternalEnum : byte
    {
        [Description("Information")]
        [EnumMember]
        Information = 1,
        [Description("Warning")]
        [EnumMember]
        Warning = 2,
        [Description("CriticalError")]
        [EnumMember]
        CriticalError = 3,
        [Description("MaxSeverity")]
        [EnumMember]
        MaxSeverity = 4
    }
}

