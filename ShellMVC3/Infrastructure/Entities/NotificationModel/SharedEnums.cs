using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NotificationModel
{


    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>

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


    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>    
    public enum MessageSystemAddressTypeEnum : int
    {
        [EnumMember]
        DoNotReplyAddress = 1,
        [EnumMember]
        ExternalSenderAddress = 2,
        [EnumMember]
        SupportSenderAddress = 3,

    }

    /// <summary>
    /// Enums that will be converted to lookup table indexs for Message Templates (templates should all be razor with an underlying model?)
    /// </summary>
    public enum MessageTemplateEnum : int
    {
        [EnumMember]
        GenericErrorMessage = 1,
        [EnumMember]
        ContactUsUserConfirmation = 2,
        [EnumMember]
        ContactUserAdminMessage = 3,
        [EnumMember]
        UserCreatedUserConfirmation = 4,
        [EnumMember]
        UserCreatedAdminNotification = 5,
        [EnumMember]
        UserCreatedJianRainOrOPenIDUserConfirmation = 6,
        [EnumMember]
        UseCreatedJainRanOrOpenIDAdminNotification = 7,
        [EnumMember]
        UserPasswordChangeUserConfirmation = 8,
        [EnumMember]
        UserPasswordChangedAdminNotification = 9,
        [EnumMember]
        UserActivatedUserConfirmation = 10,
        [EnumMember]
        UserActivatedAdminNotification = 11,
        [EnumMember]
        UserActivationCodeRecoveredUserConfirmation = 12,
        [EnumMember]
        UserActivationCodeRecoveredAdminNotification = 13,
        [EnumMember]
        UserRecivedEmailMessageConfirmation = 14,
        [EnumMember]
        UserRecivedEmailMessageAdminNotification = 15,
        [EnumMember]
        UserRecivedPeekUserConfirmation = 16,
        [EnumMember]
        UserRecivedPeekAdminNotification = 17,
        [EnumMember]
        UserRecivedLikeUserConfirmation = 18,
        [EnumMember]
        UserRecivedLikeAdminNotification = 19,
        [EnumMember]
        UserRecivedInterestUserConfirmation = 20,
        [EnumMember]
        UserRecivedInterestAdminNotification = 21,
        [EnumMember]
        UserMatchesSentUserConfirmation = 22,
        [EnumMember]
        UserMatchestSentAdminNotificaton = 23,
        [EnumMember]
        UserChatRequestRecivedUserConfirmation = 24,
        [EnumMember]
        UserChatRequestRecivedAdminNotification = 25,
        [EnumMember]
        UserChatMessageRecivedUserConfirmation = 26,
        [EnumMember]
        UserChatMessageRecivedAdminNotification = 27,


    }

    /// <summary>
    /// Enums that will be converted to lookup table data for messageTypes
    /// </summary>
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
