using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Infrastructure.Interfaces ;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using Shell.MVC2.Interfaces;

namespace Shell.MVC2.Data
{
    public class InfoNotificationRepository : MemberRepositoryBase , IInfoNotificationRepository
    {
        private int featuredmemberid = 0;
        private NotificationContext _notificationcontext;
        private IMemberRepository _membersrepository;
        private IMembersMapperRepository _membersmapperrepository;
      //  private IAPIkeyRepository _apikeyrepository;
      

        public InfoNotificationRepository(NotificationContext notificationcontext, AnewluvContext datingcontext, IMemberRepository membersrepository,IMembersMapperRepository membersmapperrepository)
            : base(datingcontext)
        {
            _notificationcontext = notificationcontext;
            _membersrepository = membersrepository;
            _membersmapperrepository = membersmapperrepository;
            _datingcontext = datingcontext;
           //_apikeyrepository = apikeyrepository;
            featuredmemberid = 67;
            
        }
               
              
   

      public  EmailViewModel getgenericerroremail(CustomErrorLog error)
        {

            try
            {
                EmailViewModel returnmodel = new EmailViewModel();
                returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.GenericErrorMessage);
                //fill in the rest of the email model values 
                returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, error.ProfileID);
                returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, error.ProfileID, error.Message);

                return returnmodel;
            }
            catch (Exception ex)
            {
                //handle logging here
            }
            return null;

        }

      public  EmailViewModel getcontactusemail(ContactUsModel model)
         {
          
             try
             {
                 EmailViewModel returnmodel = new EmailViewModel();
                 returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsMemberMesage);
                 returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberContactUsAdminMessage);
                 //fill in the rest of the email model values i.e format the subject and body
                 //subject
                 returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
                 returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
                 //Body
                 returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.Email, model.Subject, model.Message);
                 returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.Name);
                 return returnmodel;
             }
             catch (Exception ex)
             {
                 //handle logging here
             }
             return null;
         } 

      public  EmailViewModel getmembercreatedemail(RegisterModel model)
      {           

             try
             {
                 EmailViewModel returnmodel = new EmailViewModel();
                 returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
                 returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

                 //fill in the rest of the email model values i.e format the subject and body
                 //subject
                 returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
                 returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

                 //Body
                 returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.ScreenName, model.UserName, model.ActivationCode);
                 returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.ScreenName, model.Email);
                 return returnmodel;
             }
             catch (Exception ex)
             {
                 //handle logging here
             }
             return null;
      }

      public  EmailViewModel getmembercreatedopenidemail(RegisterModel model) 
      {


          try
          {

              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberCreatedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, model.openidProvider);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.ScreenName, model.openidProvider, model.UserName);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.ScreenName, model.Email, model.openidProvider);

              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      
      }

      public  EmailViewModel getmemberpasswordchangedemail(LogonViewModel model) 
      {                

          try
          {
              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangeMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPasswordChangedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.LogOnModel.ScreenName, model.LogOnModel.UserName, model.LogOnModel.Password, model.LogOnModel.ProfileID);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.LogOnModel.UserName, model.LostAccountInfoModel.Email);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public EmailViewModel getmemberprofileactivatedemailbyprofileid(int profileid)
      {
      

          try
          {
              //get the profile info
              profile model = _membersrepository.getprofilebyprofileid(profileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivatedAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel getmemberactivationcoderecoveredemailbyprofileid(int profileid) 
      {             

          try
          {
              //get the profile info
              profile model = _membersrepository.getprofilebyprofileid(profileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.screenname, model.username, model.activationcode);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.username, model.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel getmemberemailmemssagerecivedemailbyprofileid(int recipientprofileid, int senderprofileid) 
      {
         

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberActivationCodeRecoveredAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel getmemberpeekrecivedemailbyprofileid(int recipientprofileid, int senderprofileid)
      {
       

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedPeekAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel getmemberlikerecivedemailbyprofileid(int recipientprofileid, int senderprofileid) {

 

           try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedLikeAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }         
          return null;
      
      }

      public  EmailViewModel getmemberinterestrecivedemailbyprofileid(int recipientprofileid, int senderprofileid) {
       
    

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);

              EmailViewModel returnmodel = new EmailViewModel();

              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedInterestAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      public  EmailViewModel getmemberchatrequestrecivedemailbyprofileid(int recipientprofileid, int senderprofileid) {

         
          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedChatRequestAdminNotification);

              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);

              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);

              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return


              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public  EmailViewModel getmemberofflinechatmessagerecivedemailbyprofileid(int recipientprofileid, int senderprofileid) {
            

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(recipientprofileid);
              profile sender = _membersrepository.getprofilebyprofileid(senderprofileid);
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberRecivedOfflineChatMessageAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, sender.screenname, sender.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.emailaddress, sender.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;


      }

      public  EmailViewModel getmemberphotorejectedemailbyprofileid(int profileid,int adminprofileid, photorejectionreasonEnum reason) {
              

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(profileid);
              profile admin = _membersrepository.getprofilebyprofileid(adminprofileid);
              //get rejecttion reason
              string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoRejectedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname, reasondesc);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress, admin.username);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public  EmailViewModel getmemberphotouploadedemailbyprofileid(int profileid) {

   
          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(profileid);
              // profile admin = _membersrepository.getprofilebyprofileid(adminprofileid);
              //get rejecttion reason
              // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      
      }

      public EmailViewModel getadminspamblockedemailbyprofileid(int blockedprofileid)
      {    

          try
          {
              //get the profile info
              profile recipient = _membersrepository.getprofilebyprofileid(blockedprofileid);
              // profile admin = _membersrepository.getprofilebyprofileid(adminprofileid);
              //get rejecttion reason
              // string reasondesc = _datingcontext.lu_photorejectionreason.Where(p => p.id == (int)reason).FirstOrDefault().description;

              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoUploadedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, recipient.screenname);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, recipient.username, recipient.emailaddress);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

      //TO DO revisit this to send back the matches here
      public EmailViewModel getemailmatchesemailbyprofileid(int profileid)
      {    

          try
          {
              MembersViewModel model = _membersmapperrepository.getmemberdata(profileid);
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.EmailMatches = _membersrepository.getemailmatches(model);
              //get featured member
              returnmodel.FeaturedMember = _membersmapperrepository.getmembersearchviewmodel(featuredmemberid);
              returnmodel.memberEmailViewModel = getemailbytemplateid(templateenum.MemberMatchesSentMemberNotification);
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberMatchestSentAdminNotificaton);
              //fill in the rest of the email model values 
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.memberEmailViewModel.subject = String.Format(returnmodel.memberEmailViewModel.subject);
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.memberEmailViewModel.body = String.Format(returnmodel.memberEmailViewModel.body, model.profile.screenname, returnmodel.EmailMatches.Count(), DateTime.Now, model.profile.id);
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, model.profile.username, model.profile.emailaddress, returnmodel.EmailMatches.Count(), DateTime.Now);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
            
              returnmodel.HasMatches = (returnmodel.EmailMatches.Count > 0) ? true : false;
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }

      public EmailViewModel getadminmemberspamblockedemailbyprofileid(int spamblockedprofileid,string reason,string blockedby)
      {
   

          try
          {
              //get the profile info
              profile blocked = _membersrepository.getprofilebyprofileid(spamblockedprofileid);
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject, blockedby);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.username, blocked.emailaddress, reason);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;

      }
      public EmailViewModel getadminmemberblockedemailbyprofileid(int blockedprofileid, int blockerprofileid)
      {
       

          try
          {
              //get the profile info
              profile blocked = _membersrepository.getprofilebyprofileid(blockedprofileid);
              profile blocker = _membersrepository.getprofilebyprofileid(blockerprofileid);

              //get the notes since the block was created first before email was sent
              var blockreason = _datingcontext.blocks.Where(p => p.blockprofile_id == blockedprofileid && p.id == blockedprofileid).FirstOrDefault();
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberSpamBlockedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, blocked.emailaddress, blocker.emailaddress, blockreason.notes.FirstOrDefault());
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null;
      }

        //There is no message for Members to know when photos are approved btw
      public EmailViewModel getadminmemberphotoapprovedemailbyprofileid(int approvedprofileid, int adminprofileid)
      {

          try
          {
              //get the profile info
              profile profile = _membersrepository.getprofilebyprofileid(approvedprofileid);
              profile admin = _membersrepository.getprofilebyprofileid(adminprofileid);
              EmailViewModel returnmodel = new EmailViewModel();
              returnmodel.adminEmailViewModel = getemailbytemplateid(templateenum.MemberPhotoApprovedAdminNotification);
              //fill in the rest of the email model values i.e format the subject and body
              //subject
              returnmodel.adminEmailViewModel.subject = String.Format(returnmodel.adminEmailViewModel.subject);
              //Body
              returnmodel.adminEmailViewModel.body = String.Format(returnmodel.adminEmailViewModel.body, profile.username, profile.emailaddress, admin.screenname);
              //send the emails here i think trhough the service
              //once things are finally done there will he only a boolean return
              return returnmodel;
          }
          catch (Exception ex)
          {
              //handle logging here
          }
          return null; 

     
      }

      public EmailModel getemailbytemplateid(templateenum template)
      {
          EmailModel emaildetail = new EmailModel();

          try
          {
              emaildetail.body = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().bodystring.description;
              emaildetail.subject = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().subjectstring.description;
              //TO DO figure out if we will populate other values here
              return emaildetail;
          }
          catch (Exception ex)
          {
              //handle logging here
          }         
          return null;
         
      }
        //Private reusable internal functions  
        private message sendemailtemplateinfo(templateenum template)
       {
           message newmessagedetail = new message();


           try
           {
               newmessagedetail.template = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault();
               newmessagedetail.body = newmessagedetail.template.bodystring.description;
               newmessagedetail.subject = newmessagedetail.template.subjectstring.description;
               return newmessagedetail;
           }
           catch (Exception ex)
           {
               //handle logging here
           }
           return null; 
            
      
       }


    }
}
