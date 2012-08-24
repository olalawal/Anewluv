using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
   
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
}

