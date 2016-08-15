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
    public enum templatebodyenum : int
    {
        [Description("The Member <b>{0}</b> has generated the following error </br> Error: </br> {1} ")]
        [EnumMember]
        GenericErrorMessage = 1,
      
        //contact us
        [Description("Dear <b>{0}</b> ,<p></p> Thank you for contacting us at Anewluv.com, your message has been added to the support ticket list and you will get a response from our support staff within 24 hours <p></p> The Anewluv.com Support Team")]
        [EnumMember]
        MemberContactUsMemberMesage = 2,
      
        [Description("The user {0} with email address : {1} sent the following message: <p></p> <b> </br>  subject:  {1}  </b> </br> Message : </br> {2}")]
        [EnumMember]
        MemberContactUsAdminMessage = 3,
       
        //profile created
        [Description("Thank you for creating a profile at <a href=\"www.anewluv.com\">Anewluv.com </a> <p></p><a href=\"www.anewluv.com\">Anewluv.com </a>   provides a new and refreshing way to  meet people from all walks of life.  <p></p> You have <b> FREE </b> access to our exclusive geo-targeting system to link up with members from all around the world! <p></p>  you can easily search for members any distance from you and you will always get acurate REAL results unlike some other websites.  <p></p> Our system spans  hundreds of countries all around the world.  <p></p> We are also adding new features every month <p></p> So get started today! Please click on the link below to activate your profile. <b>If you are unable to activate your profile with the provided link, try copying the activation code directly from this email and pasting it into the activation code box on our activation page</br> Your username is : <b>{0}</>  and  Your activation code is:  <b>  {1} </b> <p></p><a href='http://www.AnewLuv.com/Account/LandingSPA/?username={0}?activationcode={1}'>Activiate your account on www.Anewluv.com</a><p></p> ")]
        [EnumMember]
        MemberCreatedMemberNotification = 4,

        [Description("A new profile for the user <b>{0}</b> with email  <b>{1}</b>  has been created on AnewLuv.com")]
        [EnumMember]
        MemberCreatedAdminNotification = 5,

        //open id or jainrain profile created
        [Description("Dear <b>{0}</b> ,<p></p> Thank you for creating a profile at <a href=\"www.anewluv.com\">Anewluv.com </a> <p></p><a href=\"www.anewluv.com\">Anewluv.com </a>   provides a new and refreshing way to  meet people from all walks of life.  <p></p> You have <b> FREE </b> access to our exclusive geo-targeting system to link up with members from all around the world! <p></p>  you can easily search for members any distance from you and you will always get acurate REAL results unlike some other websites.  <p></p> Our system spans  hundreds of countries all around the world.  <p></p> We are also adding new features every month <p></p> You profile has already been activated since you signed in with {1} , please make sure you information was correctly uploaded by checking your profile details on our website  </b> </br> Your user name is: <b> {2} </b><p></p>  - The AnewLuv.com Team <p></p>  ")]
        [EnumMember]
        MemberCreatedJianRainOrOPenIDMemberNotification = 6,
        [Description("A new profile for the user <b>{0}</b> with email  <b>{1}</b>  has been created on AnewLuv.com via OpenID or JainRain using: {2} ")]
        [EnumMember]
        MemberCreatedJainRanOrOpenIDAdminNotification = 7,

        //member password change notificaion
        [Description( "Dear {0} ,<p></p> You have initated an Account recovery on AnewLuv.com , Your Username is <b> {1} </b> You must reset your password within the next 30 minutes by clicking this reset link <br><a href='http://www.AnewLuv.com/Account/LandingSPA/?username={1}?token={2}'>Reset your password on www.Anewluv.com</a>")]
        [EnumMember]
        MemberPasswordResetMemberNotification = 8,
        [Description("An Account has been recovered on Anewluv.com by the user <b>{0}</b> with email :<b>{1}</b>")]
        [EnumMember]
        MemberPasswordResetAdminNotification = 9,

        //profile activated 
        [Description("Dear {0} ,<p></p>  Your account has been activated on AnewLuv.com , You can now log in to your account at <p></p> <a href='http://www.AnewLuv.com/Members/Home '>Login to AnewLuv.com</a>  <p></p><a href=\"http://www.anewluv.com \">Anewluv.com </a>")]
        [EnumMember]
        MemberActivatedMemberNotification = 10,

        [Description("The  profile for the user {0} with email {1} has been <b> Activated </b> on AnewLuv.com")]
        [EnumMember]
        MemberActivatedAdminNotification = 11,


        //Activation code recovered 
        [Description("Dear {0} ,<p></p>You have recovered you activation code on AnewLuv.com </br>  Please click on the link below to activate your profile. <b>If you are unable to activate your profile with the provided link, try copying the activation code directly from this email and pasting it into the activation code box on our activation page</br><p></p><a href=\'http://www.AnewLuv.com/Account/LandingSPA/?activationcode={1}'>Activiate your account on www.Anewluv.com</a><p></p>")]
        [EnumMember]
        MemberActivationCodeRecoveredMemberNotification = 12,
        [Description("A Profile ActivationCode for the user {0} with email address : {1} has been recovered on AnewLuv.com")]
        [EnumMember]
        MemberActivationCodeRecoveredAdminNotification = 13,

        //Sent member message 
        [Description("Dear {10}, <p></p>The member {1} has sent you an Email message on AnewLuv.com, Please log into your account to view {1}'s Email message </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home'>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedEmailMessageMemberNotification = 14,
        [Description("The Member {0} has received an Email Message from the member {1}")]
        [EnumMember]
        MemberRecivedEmailMessageAdminNotification = 15,

        //Peek recived message
        [Description("Dear {0}, <p></p> <p></p>The member {1} has peeked at your profile on AnewLuv.com, Please log into your account to view {2}'s details </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home'>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedPeekMemberNotification = 16,
        [Description("The  Member {0} has received a Peek from the member {1}")]
        [EnumMember]
        MemberRecivedPeekAdminNotification = 17,


        [Description("Dear {0}, <p></p>The member {1} has likes you on AnewLuv.com, Please log into your account to view {2}'s details </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home '>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedLikeMemberNotification = 18,
        [Description("The Member {0} has received a Like from the member {1}")]
        [EnumMember]
        MemberRecivedLikeAdminNotification = 19,
        [Description("Dear {0}, <p></p>The member {1} has expressed interest your profile on AnewLuv.com, Please log into your account and to view {2}'s details </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home  '>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedInterestMemberNotification = 20,
        [Description("The Member {0} has received an Interest from the Member, {1}")]
        [EnumMember]
        MemberRecivedInterestAdminNotification = 21,
        [Description("Dear {0}, <p></p>The member {1} has wants to chat with you on AnewLuv.com, Please log into your account and check your chat requests to respond</br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home'>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedChatRequestMemberNotification = 22,
        [Description("The Member {0} has received a chat request from the Member, {1}")]
        [EnumMember]
        MemberRecivedChatRequestAdminNotification = 23,
        [Description("Dear {0}, <p></p>The member {1} has sent you an offline Chat message on AnewLuv.com, Please log into your account to view {2}'s Chat message </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home  '>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberRecivedOfflineChatMessageMemberNotification = 24,
        [Description("The Member {0} has received an offline chat request from the Member, {1}")]
        [EnumMember]
        MemberRecivedOfflineChatMessageAdminNotification = 25, //Photo
        [Description("Dear {0} ,</br> Thank you for uploading your photo to  Anewluv.com </br>Unfortunately your photo did not meed our terms of service, please read the terms of service for acceptable photo formats and content. <p></p> This is the reason why : <p></p> <b>  {1} </b>")]
        [EnumMember]
        MemberPhotoRejectedMemberNotification = 26,
        [Description("The member {0} with the email address {1} has had thier photo rejected by the admin user with username {2}")]
        [EnumMember]
        MemberPhotoRejectedAdminNotification = 27,
        [Description("Dear {0} ,</br> Thank you for uploading your photo to  Anewluv.com Please allow up to 24 hours for your photo to be aproved.")]
        [EnumMember]
        MemberPhotoUploadedMemberNotification = 28,
        [Description("A new photo for the user {0} with email address : {1}  has been uploaded to AnewLuv.com")]
        [EnumMember]
        MemberPhotoUploadedAdminNotification = 29,
        [Description("Dear {0} , <p></p>Here are your {1} new matches for date : {2}  </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home'>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberMatchesSentMemberNotification = 30,
        [Description("The member {0} with email address {1} has received {2} matches for the date : {2}")]
        [EnumMember]
        MemberMatchestSentAdminNotificaton = 31,      
        [Description("A message from the user {0} with email address {1) was blocked becase of the reason: {1}")]
        [EnumMember]
        MemberSpamBlockedAdminNotification = 32,
        [Description("The  Member {0} has been blocked by the member {1}, </br> block notes :  {2}")]
        MemberBlockedAdminNotification = 33,

        [Description("A photo from the user {0} with email address {1} has been approved by: {1} on AnewLuv")]
        MemberPhotoApprovedAdminNotification = 34,

        //friend request recived
        [Description("Dear {0}, <p></p>The member {1} has sent you a friend request on AnewLuv.com, Please log into your account and to view {1}'s details </br><p></p> <b><a href='http://www.AnewLuv.com/Members/Home '>Log on to your Account on www.Anewluv.com</a></b>")]
        [EnumMember]
        MemberFriendRequestRecivedMemberNotification = 35,

        [Description("The Member {0} has received an Interest from the Member, {1}")]
        [EnumMember]
        MemberFriendRequestRecivedAdminNotification = 36,

         //member password change notificaion
        [Description("Dear {0} ,<p></p> Your password has been changed on AnewLuv.com, if this is an error or you did not initiate this activity please contact us imediately at  <b><a href='http://www.AnewLuv.com/ContactUs'>Contact Us</a></b>")]
        [EnumMember]
        MemberPasswordChangeMemberNotification = 37,
        [Description("An Password has been changed for on Anewluv.com by the user <b>{0}</b> with email :<b>{1}</b>")]
        [EnumMember]
        MemberPasswordChangedAdminNotification = 38,

    }



}
