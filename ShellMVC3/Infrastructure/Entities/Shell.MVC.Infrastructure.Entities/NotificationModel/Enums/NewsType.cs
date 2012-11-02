using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum MessageAddressTypeEnum : int
    {
        [EnumMember]
        Developer = 1,
        [EnumMember]
        SystemAdmin = 2,
        [EnumMember]
        ProjectLead = 3,
        [EnumMember]
        QualityAsurance = 4
    }


  
}
