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
using CustomLoggingModel;

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
    Warning =2,
    [EnumMember]
    CriticalError = 3,
    [EnumMember]
    MaxSeverity = 4
}


public class LogSeverityLookup
{
    //we generate this manually from enums for now
    [DatabaseGenerated(DatabaseGeneratedOption.None)]    
    public int LogSeverityLookupID { get; set; }
    [DataMember]
    public string Description { get; set; }
    

}

//TO DO these are objects for wrapping enums

//public class LogSeverityWrapper : EnumWrapper<LogSeverityEnum>
//{
//    public static implicit operator LogSeverityWrapper(LogSeverityEnum e)
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
//     public LogSeverity() : this(LogSeverityEnum.Warning ) 
//     {}
//     public LogSeverity(LogSeverityEnum value)
//     {
//         LogSeverityLookupID = (int)value;       
//     }       
//     // implicit operators for auto casting the complex type to enum val   
//     public static implicit operator LogSeverityEnum(LogSeverity type)
//     {
//         return (LogSeverityEnum)type.LogSeverityLookupID;  
 
//     }

//     public static implicit operator LogSeverity(LogSeverityEnum type)    
//     {
//         return new LogSeverity(type);        
//     }     
// }




/// <summary>
/// This is used to convert .NET log severity levels to our values
/// </summary>
public enum LogSeverityInternal : byte
{
    Information = 1,
    Warning = 2,
    CriticalError = 3,
    MaxSeverity = 4
}

/// <summary>
///  converts the internal .NET log severity values to ours
/// </summary>
public class LogSeverityUtil
{
    public static LogSeverityInternal TranslateLogSeverity(LogSeverityEnum LogLevel)
    {
        if (LogLevel == LogSeverityEnum.Information)
        {
            return LogSeverityInternal.Information;
        }
        else if (LogLevel == LogSeverityEnum.Warning)
        {
            return LogSeverityInternal.Warning;
        }
        else if (LogLevel == LogSeverityEnum.CriticalError)
        {
            return LogSeverityInternal.CriticalError;
        }
        else if (LogLevel == LogSeverityEnum.MaxSeverity)
        {
            return LogSeverityInternal.MaxSeverity;
        }
        else
        {
            return LogSeverityInternal.MaxSeverity;
        }
    }

}

