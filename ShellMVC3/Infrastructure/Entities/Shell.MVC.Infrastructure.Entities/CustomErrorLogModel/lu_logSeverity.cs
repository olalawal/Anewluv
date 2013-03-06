using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualBasic;

using System.Collections;

using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    [DataContract(Namespace = "")]
    public class lu_logseverity
    {
        //we generate this manually from enums for now
       [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }



    }

    //TO DO these are objects for wrapping enums

    //public class LogSeverityWrapper : EnumWrapper<logseverityEnum>
    //{
    //    public static implicit operator LogSeverityWrapper(logseverityEnum e)
    //    {
    //        return new LogSeverityWrapper() { Enum = e };
    //    }
    //}


    //[ComplexType]
    //public class LogSeverity    
    // {         
    //     // FK + navigation property (must be virtual)  
    //     [ForeignKey("LogSeverityLookup")]    
    //     public int LogSeverityLookupID { get; set; }
    //     public virtual LogSeverityLookup LogSeverityLookup { get; set; }

    //     // actors        
    //     public LogSeverity() : this(logseverityEnum.Warning ) 
    //     {}
    //     public LogSeverity(logseverityEnum value)
    //     {
    //         LogSeverityLookupID = (int)value;       
    //     }       
    //     // implicit operators for auto casting the complex type to enum val   
    //     public static implicit operator logseverityEnum(LogSeverity type)
    //     {
    //         return (logseverityEnum)type.LogSeverityLookupID;  

    //     }

    //     public static implicit operator LogSeverity(logseverityEnum type)    
    //     {
    //         return new LogSeverity(type);        
    //     }     
    // }


    /// <summary>
    ///  converts the internal .NET log severity values to ours
    /// </summary>
    public class LogSeverityUtil
    {
        public static logseverityinternalEnum TranslateLogSeverity(logseverityEnum LogLevel)
        {
            if (LogLevel == logseverityEnum.Information)
            {
                return logseverityinternalEnum.Information;
            }
            else if (LogLevel == logseverityEnum.Warning)
            {
                return logseverityinternalEnum.Warning;
            }
            else if (LogLevel == logseverityEnum.CriticalError)
            {
                return logseverityinternalEnum.CriticalError;
            }
            else if (LogLevel == logseverityEnum.MaxSeverity)
            {
                return logseverityinternalEnum.MaxSeverity;
            }
            else
            {
                return logseverityinternalEnum.MaxSeverity;
            }
        }

    }
}
