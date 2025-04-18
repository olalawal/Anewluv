﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace  Nmedia.Infrastructure.Domain.Data.log
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into Initial Catalog= values when the context is created
    /// </summary>
    [DataContract]
    public enum logseverityEnum : int
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

