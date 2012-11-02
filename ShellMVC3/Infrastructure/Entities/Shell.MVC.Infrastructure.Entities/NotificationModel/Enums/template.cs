using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


 

    /// <summary>
    /// Enums that will be converted to lookup table indexs for Message Templates (templates should all be razor with an underlying model?)
    /// </summary>
    /// 
    [DataContract]
    public enum templateenum : int
    {
        [Description("GenericErrorMessage")]
        [EnumMember]
        GenericErrorMessage = 1,
        [Description("MemberContactUsMemberMesage")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,
        [Description("MemberContactUsAdminMessage")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,
        [Description("MemberCreatedMemberConfirmation")]
        [EnumMember]
        MemberCreatedMemberConfirmation = 4,
        [Description("MemberCreatedAdminNotification")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,
        [Description("MemberCreatedJianRainOrOPenIDMemberConfirmation")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberConfirmation = 6,
        [Description("MemberCreatedJainRanOrOpenIDAdminNotification")]
        [EnumMember]
        MemberCreatedJainRanOrOpenIDAdminNotification = 7,
        [Description("MemberPasswordChangeMemberConfirmation")]
        [EnumMember]
        MemberPasswordChangeMemberConfirmation = 8,
        [Description("MemberPasswordChangedAdminNotification")]
        [EnumMember]
        MemberPasswordChangedAdminNotification = 9,
        [Description("NotSet")]
        [EnumMember]
        MemberActivatedMemberConfirmation = 10,
        [Description("NotSet")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,
        [Description("NotSet")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberConfirmation = 12,
        [Description("NotSet")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedEmailMessageConfirmation = 14,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedPeekMemberConfirmation = 16,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedLikeMemberConfirmation = 18,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedInterestMemberConfirmation = 20,
        [Description("NotSet")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,
        [Description("NotSet")]
        [EnumMember]
        MemberMatchesSentMemberConfirmation = 22,
        [Description("NotSet")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 23,
        [Description("NotSet")]
        [EnumMember]
        MemberChatRequestRecivedMemberConfirmation = 24,
        [Description("NotSet")]
        [EnumMember]
        MemberChatRequestRecivedAdminNotification = 25,
        [Description("MemberChatMessageRecivedMemberConfirmation")]
        [EnumMember]
        MemberChatMessageRecivedMemberConfirmation = 26,
        [Description("MemberChatMessageRecivedAdminNotification")]
        [EnumMember]
        MemberChatMessageRecivedAdminNotification = 27,


    }



}
