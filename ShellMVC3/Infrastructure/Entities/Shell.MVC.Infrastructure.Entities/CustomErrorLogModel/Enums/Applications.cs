using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum applicationEnum : int
    {
        [Description("MemberActionsService")]
        [EnumMember]
        MemberActionsService = 1,
        [Description("WebClient")]
        [EnumMember]
        WebClient = 2,
        [Description("MemberService")]
        [EnumMember]
        MemberService = 3,
        [Description("EditMemberService")]
        [EnumMember]
        EditMemberService = 4,
        [Description("EditSearchService")]
        [EnumMember]
        EditSearchService = 5,
        [Description("SearchService")]
        [EnumMember]
        SearchService = 6,
        [Description("UserAuthorizationService")]
        [EnumMember]
        UserAuthorizationService = 7,
        [Description("Logging Service")][EnumMember]
        LoggingService = 8,
        [Description("Notification Service")][EnumMember]
        NotificationService = 9,
        [Description("General Application Error")]
        [EnumMember]
        GeneralApplicationError = 11,
         [Description("Lookup Service Error")]
        [EnumMember]
        LookupService = 11
    }

}

