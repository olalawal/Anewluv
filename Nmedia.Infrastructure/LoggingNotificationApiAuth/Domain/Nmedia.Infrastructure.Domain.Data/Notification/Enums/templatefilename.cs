using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{


 

    /// <summary>
    /// Enums that will be converted to lookup table indexs for Message Templates (templates should all be razor with an underlying model?)
    /// </summary>
    /// 
    [DataContract]
    public enum templatefilenameenum : int
    {

        [Description("CustomErrorMessage")]
        [EnumMember]
        GenericErrorMessage = 1,

        [Description("MemberContactUsMemberMesage")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,

        [Description("MemberContactUsAdminMessage")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,

        [Description("MemberCreatedMemberNotification")]
        [EnumMember]
        MemberCreatedMemberNotification = 4,

        [Description("MemberCreatedAdminNotification")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,

        [Description("MemberCreatedOPenIDMemberNotification")]
        [EnumMember]
        MemberCreatedOPenIDMemberNotification = 6,

        [Description("MemberCreatedOpenIDAdminNotification")]
        [EnumMember]
        MemberCreatedOpenIDAdminNotification = 7,

        [Description("MemberAccountRecoveredMemberNotification")]
        [EnumMember]
        MemberAccountRecoveredMemberNotification = 8,

        [Description("MemberAccountRecoveredAdminNotification")]
        [EnumMember]
        MemberAccountRecoveredAdminNotification = 9,

        [Description("MemberActivatedMemberNotification")]
        [EnumMember]
        MemberActivatedMemberNotification = 10,

        [Description("MemberActivatedAdminNotification")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,

        [Description("MemberActivationCodeRecoveredMemberNotification")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,

        [Description("MemberActivationCodeRecoveredAdminNotification")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        [Description("MemberRecivedEmailMessageMemberNotification")]
        [EnumMember]
        MemberRecivedEmailMessageMemberNotification = 14,

        [Description("MemberRecivedEmailMessageAdminNotification")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        [Description("MemberRecivedPeekMemberNotification")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,

        [Description("MemberRecivedPeekAdminNotification")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,

        [Description("MemberRecivedLikeMemberNotification")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,

        [Description("MemberRecivedLikeAdminNotification")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,

        [Description("MemberRecivedInterestMemberNotification")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,

        [Description("MemberRecivedInterestAdminNotification")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,

        [Description("MemberRecivedChatRequestMemberNotification")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,

        [Description("MemberRecivedChatRequestAdminNotification")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,

        [Description("MemberRecivedOfflineChatMessageMemberNotification")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,

        [Description("MemberRecivedOfflineChatMessageAdminNotification")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo

        [Description("MemberPhotoRejectedMemberNotification")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,

        [Description("MemberPhotoRejectedAdminNotification")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,

        [Description("MemberPhotoUploadedMemberNotification")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,

        [Description("MemberPhotoUploadedAdminNotification")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,

        [Description("MemberMatchesSentMemberNotification")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 30,

        [Description("MemberMatchestSentAdminNotificaton")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 31,

        [Description("MemberSpamBlockedAdminNotification")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 32,

        [Description("MemberBlockedAdminNotification")]
        MemberBlockedAdminNotification = 33,

        [Description("MemberPhotoApprovedAdminNotification")]
        MemberPhotoApprovedAdminNotification = 34,

        [Description("MemberFriendRequestRecivedMemberNotification")]
        [EnumMember]
        MemberFriendRequestRecivedMemberNotification = 35,

        [Description("MemberFriendRequestRecivedAdminNotification")]
        [EnumMember]
        MemberFriendRequestRecivedAdminNotification = 36

    }



}
