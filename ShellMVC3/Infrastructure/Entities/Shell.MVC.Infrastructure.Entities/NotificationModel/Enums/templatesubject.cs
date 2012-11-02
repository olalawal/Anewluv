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
        [Description("The Member {0} has generated the following error </br> Error: </br> {1} ")]
        [EnumMember]
        GenericErrorMessage = 1,
        [Description("Dear {0} ,<p></p> Thank you for contacting us at Anewluv.com, your message has been added to the support ticket list and you will get a response from our support staff within 24 hours <p></p> The Anewluv.com Support Team")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,
        [Description("The Member {0} sent the following message: <p></p> <b> </br>  subject:  {1}  </b> </br> Message : </br> {2}")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,
        [Description("NotSet")]
        [EnumMember]
        MemberCreatedMemberConfirmation = 4,
        [Description("NotSet")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,
        [Description("NotSet")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberConfirmation = 6,
        [Description("NotSet")]
        [EnumMember]
        UseCreatedJainRanOrOpenIDAdminNotification = 7,
        [Description("NotSet")]
        [EnumMember]
        MemberPasswordChangeMemberConfirmation = 8,
        [Description("NotSet")]
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
        [Description("NotSet")]
        [EnumMember]
        MemberChatMessageRecivedMemberConfirmation = 26,
        [Description("NotSet")]
        [EnumMember]
        MemberChatMessageRecivedAdminNotification = 27,
    }



}
