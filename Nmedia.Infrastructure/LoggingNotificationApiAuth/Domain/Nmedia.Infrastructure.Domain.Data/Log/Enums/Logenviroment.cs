using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace  Nmedia.Infrastructure.Domain.Data.log
{

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum enviromentEnum : int
    {
        [Description("DEV")]
        [EnumMember]
        dev = 1,
        [Description("UAT")]
        [EnumMember]
        uat = 2,
        [Description("PROD")]
        [EnumMember]
        production = 3

    }
    
    

}

