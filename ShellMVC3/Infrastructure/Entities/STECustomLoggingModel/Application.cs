using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CustomLoggingModel
{
   

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum ApplicationEnum : int
    {
        [EnumMember]
        Echain = 1,
        [EnumMember]
        WebApp = 2,
        [EnumMember]
        LoggingService = 3,
        [EnumMember]
        NotificationService = 4
    }


    public class ApplicationLookup
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationLookupID { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }


    }
}
