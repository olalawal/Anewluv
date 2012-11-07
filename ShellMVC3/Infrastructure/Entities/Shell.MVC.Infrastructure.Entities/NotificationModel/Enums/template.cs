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
        [Description("Generic error message")]
        [EnumMember]
        GenericErrorMessage = 1,

        [Description("ContactUs Member Mesage")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,

        [Description("ContactUs Admin Message ")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,

        [Description("Profile Created Member Notification")]
        [EnumMember]
        MemberCreatedMemberNotification = 4,

        [Description("Profile Created Admin Notification")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,

        [Description("Profile Created via OpenID or JainRain Member Notification")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberNotification = 6,

        [Description("Profile Created via OpenID or JainRain Admin Notification")]
        [EnumMember]
        MemberCreatedJainRanOrOpenIDAdminNotification = 7,

        [Description("Account Recovered/Password Change Member Notification")]
        [EnumMember]
        MemberPasswordChangeMemberNotification = 8,

        [Description("Account Recovered/Password Change Admin Notification")]
        [EnumMember]
        MemberPasswordChangedAdminNotification = 9,

        [Description("Profile Activated Member Notification")]
        [EnumMember]
        MemberActivatedMemberNotification = 10,

        [Description("Profile Activated Admin Notification")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,

        [Description("ActivationCode Recovered Member Notification")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,

        [Description("ActivationCode Recovered Admin Notification")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        [Description("Email Message Recived Member Notification")]
        [EnumMember]
        MemberRecivedEmailMessageMemberNotification = 14,

        [Description("Email Message Recived Admin Notification")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        [Description("Peek Recived Member Notification")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,

        [Description("Peek Recived Admin Notification")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,

        [Description("Like Recived Member Notification")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,

        [Description("Like Recived Admin Notification")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,

        [Description("Interest Recived Member Notification")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,

        [Description("Interest Recived Admin Notification")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,

        [Description("Chat Request Recived Member Notification")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,

        [Description("Chat Request Recived Admin Notification")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,

        [Description("Chat Message Recived Member Notification")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,

        [Description("Chat Message Recived Admin Notification")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo

        [Description("Photo Rejection Member Notification")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,

        [Description("Photo Rejection Admin Notification")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,

        [Description("Photo Uploaded Member Notification")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,

        [Description("Photo Uploaded Admin Notification")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,

        [Description("Matches Sent Member Notification")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 30,

        [Description("Matches Sent Admin Notification")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 31,

        [Description("Spam blocked Admin Notification")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 32,

        [Description("Member Blocked Admin Notification")]
        MemberBlockedAdminNotification = 33,

        [Description("Photo Approved Admin Notification")]
        MemberPhotoApprovedAdminNotification = 34,

    }



}
