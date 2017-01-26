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
    public enum templatesubjectenum : int
    {

        [Description("The Member <b>{0}</b> has generated an error on {1}")]
        [EnumMember]
        GenericErrorMessage = 1,

        [Description("You Question or Comment has been accepted on {0}! (No Action Required)")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,

        [Description("A new Question from a user has been received at {0}! Please respond ")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,

        [Description("{1} * Action Required * : Your new profile needs to be activated")]
        [EnumMember]
        MemberCreatedMemberNotification = 4,

        [Description("A new Profile has been created on {0} !")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,

        [Description("Thank you for creating a profile on {0}")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberNotification = 6,

        [Description("A new Profile has been created via Jainrain or OpenId using {0} !")]
        [EnumMember]
        MemberCreatedJainRanOrOpenIDAdminNotification = 7,

        [Description("Your Account has been Recovered on {0}, *Action required*")]
        [EnumMember]
        MemberPasswordResetMemberNotification = 8,

        [Description("An Account has been recovered on {0}")]
        [EnumMember]
        MemberPasswordResetAdminNotification = 9,

        [Description("Your Profile has been Activated on {0}")]
        [EnumMember]
        MemberActivatedMemberNotification = 10,

        [Description("A Profile has been Activated on {0} !")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,

        [Description("{0} * Action Required * : Your new profile needs to be activated")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,

        [Description("A Profile ActivationCode has been Recovered! on {0}")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        [Description("You have a new Email message on {0}!")]
        [EnumMember]
        MemberRecivedEmailMessageMemberNotification = 14,

        [Description("Members have Communicated via {0} Email! ")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        [Description("You have a new peek  on {0}!")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,

        [Description("A Member has Peeked at another Member! on {0}")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,

        [Description("You have a new Like on {0}!")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,

        [Description("A Member has sent a Like to another Member! on {0}")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,

        [Description("You have a new Interest on {0} ! ")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,

        [Description("A Member has sent an Interest to another member on {0} !")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,

        [Description("You have a new Chat Request on {0} !")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,

        [Description("A member has sent a Chat Request to Another member on {0}")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,

        [Description("You have new Chat message on {0}")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,

        [Description("A member has sent a Chat Request to another member on {0}")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo

        [Description("Your photo was not Approved on {0}! **Action May Be Required**")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,

        [Description("A photo was rejected by an AnewLuv Admin on {0}")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,

        [Description("{0} *Your Photo has been uploaded on {1} *")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,

        [Description("A new Photo has been uploaded on {0}! Please Approve this Photo.")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,        

        [Description("Your latest compatibility matches on {0}</a></b>")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 30,

        [Description("Member compatibility matches sent on {0}")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 31,

        [Description("Message was blocked on {1} by {0}")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 32,

        [Description("A Member has blocked another Member on {0}")]
        MemberBlockedAdminNotification = 33,

        [Description("A photo has been approved by an Admin on {0}")]
        MemberPhotoApprovedAdminNotification = 34,

        [Description("You have a new Friend Request on {0}")]
        [EnumMember]
        MemberFriendRequestRecivedMemberNotification = 35,

        [Description("A member has sent a Friend Request to another member on {0}")]
        [EnumMember]
        MemberFriendRequestRecivedAdminNotification = 36,

        [Description("Your password has been successfully changed! on {0}")]
        [EnumMember]
        MemberPasswordChangeMemberNotification = 37,

        [Description("A member has changed their password! on {0}")]
        [EnumMember]
        MemberPasswordChangeAdminNotification = 38
    }



}
