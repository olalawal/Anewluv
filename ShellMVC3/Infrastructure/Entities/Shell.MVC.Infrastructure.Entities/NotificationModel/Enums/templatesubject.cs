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

        [Description("AnewLuv.com * Action Required * : Your new profile needs to be activated")
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,

        [Description("A Profile ActivationCode has been Recovered!")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        [Description("You have a new Email message on Anewluv.com!")]
        [EnumMember]
        MemberRecivedEmailMessageNotification = 14,

        [Description("Members have Communicated via AnewLuv Email! ")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        [Description("Dear {0}, <p></p> <p></p>The member {1} has peeked at your profile on AnewLuv.com, Please log into your account to view {1}'s details </br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={1}\">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,

        [Description("The  Member {0} has received a Peek from the member {1}")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,

        [Description("Dear {0}, <p></p>The member {1} has likes you on AnewLuv.com, Please log into your account to view {1}'s details </br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={1}\">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,
        [Description("The Member {0} has received a Like from the member {1}")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,

        [Description("Dear {0}, <p></p>The member {1} has expressed interest your profile on AnewLuv.com, Please log into your account and to view {1}'s details </br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={1} \">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,

        [Description("The Member {0} has received an Interest from the Member, {1}")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,

        [Description("Dear {0}, <p></p>The member {1} has wants to chat with you on AnewLuv.com, Please log into your account and check your chat requests to respond</br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={1} \">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,

        [Description("The Member {0} has received a chat request from the Member, {1}")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,

        [Description("Dear {0}, <p></p>The member {1} has sent you an offline Chat message on AnewLuv.com, Please log into your account to view {1}'s Chat message </br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={1} \">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,

        [Description("The Member {0} has received an offline chat request from the Member, {1}")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo

        [Description("Dear {0} ,</br> Thank you for uploading your photo to  Anewluv.com </br>Unfortunately your photo did not meed our terms of service, please read the terms of service for acceptable photo formats and content. <p></p> This is the reason why : <p></p> <b>  {1} </b>")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,

        [Description("The member {0} has had thier photo rejected by the admin user with username {1}")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,

        [Description("Dear {0} ,</br> Thank you for uploading your photo to  Anewluv.com Please allow up to 24 hours for your photo to be aproved.")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,

        [Description("A new photo for the user {0} has been uploaded to AnewLuv.com")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,

        [Description("The member {0} has had thier photo rejected by the admin user with username {1}")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 30,

        [Description("Dear {0} , <p></p>Here are you {1} new matches for date : {2}  </br><p></p> <b><a href=\"http://www.AnewLuv.com/Account/LogOn/?ProfileID={0} \">Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 31,

        [Description("The member {0} has recived {1} matches for the date : {2}")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 32,

        [Description("A message from the user {0} was blocked becase of the reason: {1}")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 33,

        [Description("The  Member {0} has been blocked by the member {1}")]
        MemberBlockedAdminNotification = 34,


    }



}
