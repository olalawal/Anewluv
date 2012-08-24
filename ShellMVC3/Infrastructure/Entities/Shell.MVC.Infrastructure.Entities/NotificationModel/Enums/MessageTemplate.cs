using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


 

    /// <summary>
    /// Enums that will be converted to lookup table indexs for Message Templates (templates should all be razor with an underlying model?)
    /// </summary>
    /// 
    [DataContract]
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



}
