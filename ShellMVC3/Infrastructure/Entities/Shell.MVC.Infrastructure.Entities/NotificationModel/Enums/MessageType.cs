using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


    
    /// <summary>
    /// Enums that will be converted to lookup table data for messageTypes
    /// </summary>
    /// 
    [DataContract]
    public enum MessageTypeEnum : int
    {
        [EnumMember]
        GlobalSystemUpdate = 1,
        [EnumMember]
        SysAdminUpdate = 2,
        [EnumMember]
        SysAdminError = 3,
        [EnumMember]
        DeveloperUpdate = 4,
        [EnumMember]
        DeveloperError = 5,
        [EnumMember]
        GlobalError = 6,
        [EnumMember]
        ProjectManagerUpdate = 7,
        [EnumMember]
        QAUpdate = 8,
        [EnumMember]
        UserUpdate = 9,
        [EnumMember]
        GlobalUserUpdate = 10,

    }


}
