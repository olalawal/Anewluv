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
        GeneralApplicationError = 10,
         [Description("Lookup Service Error")]
        [EnumMember]
        LookupService = 11,
        [Description("Mapping And Quick Search Service")]
        [EnumMember]
        MemberMapperService = 12,
           [Description("Geo location services")]
           [EnumMember]
           GeoLocationService = 13,
           [Description("Mail Service")]
           [EnumMember]
           MailService = 14,
         [Description("Appfabric Cache")]
           [EnumMember]
           AppfabricCaching = 15,
         [Description("Media Service")]
           [EnumMember]
           MediaService = 16
    }
    

}

