﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{


    
    /// <summary>
    /// Enums that will be converted to lookup table data for messageTypes
    /// </summary>
    /// 
    [DataContract]
    public enum messagetypeenum : int
    {
        [Description("System Update")][EnumMember]
        GlobalSystemUpdate = 1,
        [Description("Admin Update")][EnumMember]
        SysAdminUpdate = 2,
        [Description("Admin Error ")][EnumMember]
        SysAdminError = 3,
        [Description("Developer Update")][EnumMember]
        DeveloperUpdate = 4,
        [Description("Developer Error")][EnumMember]
        DeveloperError = 5,
        [Description("Global Error")][EnumMember]
        GlobalError = 6,
        [Description("Project Manager Update")][EnumMember]
        ProjectManagerUpdate = 7,
        [Description("QA Update")][EnumMember]
        QAUpdate = 8,
        [Description("Members Update")][EnumMember]
        UserUpdate = 9,
        [Description("Member Global Update")][EnumMember]
        GlobalUserUpdate = 10,
        [Description("Member News Update")][EnumMember]
        NewsUpdate = 11,
        [Description("User Contract us")]
        [EnumMember]
        UserContactUsnotification = 12,
        [Description("Member Contract us")]
        [EnumMember]
         MemberContactUsNotification = 13,

    }


}
