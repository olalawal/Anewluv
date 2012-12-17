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
    public enum templatesubjectenum : int
    {

        [Description("The Member <b>{0}</b> has generated an error on AnewLuv.com")]
        [EnumMember]
        GenericErrorMessage = 1,

        [Description("You Question or Comment has been accepted on Anewluv.com! (No Action Required)")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,

        [Description("A new Question from a user has been recived at Anewluv.com! Please respond ")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,

        [Description("AnewLuv.com * Action Required * : Your new profile needs to be activated")]
        [EnumMember]
        MemberCreatedMemberNotification = 4,

        [Description("A new Profile has been created on AnewLuv.com !")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,

        [Description("Thank you for creating a profile on AnewLuv.com")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberNotification = 6,

        [Description("A new Profile has been created via Jainrain or OpenId using {0} !")]
        [EnumMember]
        MemberCreatedJainRanOrOpenIDAdminNotification = 7,

        [Description("Your Account has been Recovered on AnewLuv.com, *Action required*")]
        [EnumMember]
        MemberPasswordChangeMemberNotification = 8,

        [Description("An Account has been recovered on Anewluv.com")]
        [EnumMember]
        MemberPasswordChangedAdminNotification = 9,

        [Description("Your Profile has been Activated on AnewLuv.com")]
        [EnumMember]
        MemberActivatedMemberNotification = 10,

        [Description("A Profile has been Activated on AnewLuv.com !")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,

        [Description("AnewLuv.com * Action Required * : Your new profile needs to be activated")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,

        [Description("A Profile ActivationCode has been Recovered!")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        [Description("You have a new Email message on Anewluv.com!")]
        [EnumMember]
        MemberRecivedEmailMessageMemberNotification = 14,

        [Description("Members have Communicated via AnewLuv Email! ")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        [Description("You have a new peek  on Anewluv.com!")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,

        [Description("A Member has Peeked at another Member!")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,

        [Description("You have a new Like on Anewluv.com!")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,

        [Description("A Member has sent a Like to another Member!")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,

        [Description("You have a new Interest on AnewLuv ")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,

        [Description("A Member has sent an Interest to another member ")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,

        [Description("You have a new Chat Request on Anewluv")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,

        [Description("A member has sent a Chat Request to Another member")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,

        [Description("You have new Chat message on AnewLuv")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,

        [Description("A member has sent a Chat Request to another member")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo

        [Description("Your photo was not Approved on AnewLuv.com! **Action May Be Required**")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,

        [Description("A photo was rejected by an AnewLuv Admin")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,

        [Description("AnewLuv.com *Your Photo has been uploaded *")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,

        [Description("A new Photo has been uploaded on Anewluv.com! Please Aprove this Photo.")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,        

        [Description("Your latest compatiblity matches on AnewLuv</a></b>")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 30,

        [Description("Member compatiblity matches sent")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 31,

        [Description("Message was blocked on Anewluv.com by {0}")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 32,

        [Description("A Member has blocked another Member")]
        MemberBlockedAdminNotification = 33,

        [Description("A photo has been approved by an Admin on AnewLuv")]
        MemberPhotoApprovedAdminNotification = 34,

        [Description("You have a new Friend Request on Anewluv")]
        [EnumMember]
        MemberFriendRequestRecivedMemberNotification = 35,

        [Description("A member has sent a Friend Request to another member")]
        [EnumMember]
        MemberFriendRequestRecivedAdminNotification = 36
    }



}
