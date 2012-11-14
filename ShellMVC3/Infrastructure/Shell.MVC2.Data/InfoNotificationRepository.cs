using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Infrastructure.Interfaces ;
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;
using Shell.MVC2.Interfaces;

namespace Shell.MVC2.Data
{
    public class InfoNotificationRepository : MemberRepositoryBase , IInfoNotificationRepository
    {

        private NotificationContext _notificationcontext;
        private IMemberRepository _membersrepository;
        private IMembersMapperRepository _membersmapperrepository;


        public InfoNotificationRepository(NotificationContext notificationcontext,
            AnewluvContext datingcontext, IMemberRepository membersrepository,
            IMembersMapperRepository membersmapperrepository)
            : base(datingcontext)
        {
            _notificationcontext = notificationcontext;
        }


        public EmailViewModel getgenericemailviewmodel()
        {

            EmailViewModel returnmodel = new EmailViewModel();
               
            returnmodel.adminEmailViewModel  = getemailviewmodelbytemplateid(templateenum.MemberMatchestSentAdminNotificaton);
            //fill in the rest of the email model values 

            return returnmodel;

        }

        public EmailViewModel getcontactusemailviewmodel(string from)
        {

            EmailViewModel returnmodel = new EmailViewModel();
            returnmodel.memberEmailViewModel  = getemailviewmodelbytemplateid(templateenum.MemberContactUsMemberMesage );
            returnmodel.adminEmailViewModel  = getemailviewmodelbytemplateid(templateenum.MemberCreatedAdminNotification);
            //fill in the rest of the email model values 

            return returnmodel;

        }

        public EmailViewModel getemailmatchesviewmodelbyprofileid(int profileid)
       {

           MembersViewModel model = _membersmapperrepository.getmemberdata(profileid);

           EmailViewModel returnmodel = new EmailViewModel();
           returnmodel.EmailMatches = _membersrepository.getemailmatches(model);
           returnmodel.memberEmailViewModel  = getemailviewmodelbytemplateid(templateenum.MemberMatchesSentMemberNotification);
           returnmodel.adminEmailViewModel  = getemailviewmodelbytemplateid(templateenum.MemberMatchestSentAdminNotificaton);
           //fill in the rest of the email model values 

           return returnmodel;

       }
              
        public EmailModel getemailviewmodelbytemplateid(templateenum template)
       {
           EmailModel emaildetail = new EmailModel();
           emaildetail.body = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().bodystring.description;
           emaildetail.subject = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault().subjectstring.description;
           //TO DO figure out if we will populate other values here

           return emaildetail;
       }
     
        //Private reusable internal functions  
        private message sendemailtemplateinfo(templateenum template)
       {
           message newmessagedetail = new message();
           newmessagedetail.template = _notificationcontext.lu_template.Where(p => p.id == (int)template).FirstOrDefault();
           newmessagedetail.body = newmessagedetail.template.bodystring.description;
           newmessagedetail.subject = newmessagedetail.template.subjectstring.description;
           return newmessagedetail;
       }

    }
}
