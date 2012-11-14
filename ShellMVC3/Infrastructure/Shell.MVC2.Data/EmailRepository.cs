using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email;

//added 11/2/2012 
using Shell.MVC2.Infrastructure.Entities.NotificationModel;

using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;

using Shell.MVC2.AppFabric;

namespace Shell.MVC2.Data
{
    public class EmailRepository : MemberRepositoryBase, IEmailRepository
    {



        //TO DO do this a different way I think
        //private AnewluvContext  _datingcontext;
        private IMemberRepository _membersrepository;
        private IMembersMapperRepository _membersmapperrepository;

        public EmailRepository(AnewluvContext datingcontext, IMemberRepository membersrepository, IMembersMapperRepository membersmapperrepository)
            : base(datingcontext)
        {
            _membersrepository = membersrepository;
            _membersmapperrepository = membersmapperrepository;
        }

       public  EmailViewModel getemailmatches(int profileid)
        {

           MembersViewModel model = _membersmapperrepository.getmemberdata(profileid);
          
           EmailViewModel returnmodel = new EmailViewModel();
           returnmodel.EmailMatches = _membersrepository.getemailmatches(model);
           //fill in the rest of the email model values 

            return returnmodel;

        }

       private EmailViewModel createemailviewmodel(MembersViewModel model,messagetypeenum messagetype)
       {
EmailViewModel dd = new EmailViewModel();
return dd;
       }

    }
}
