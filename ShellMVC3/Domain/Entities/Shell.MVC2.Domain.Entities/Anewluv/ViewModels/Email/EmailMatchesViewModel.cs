using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email
{
    public class EmailViewModel
    {

            
       // public MembersRepository _membersrepository  { get; set; }
        //no contructor here will be populated in Notification service
        public EmailViewModel()
            {

               // _membersrepository = new MembersRepository();
                //this.MailModel = new MailModel();            
                //this.EmailMatches = new List<MemberSearchViewModel>();             
                ////get the weekly matches for this user 
                ////get the membersviewmodel for this person
                //MembersViewModel model = new MembersViewModel();
                //var mm = new ViewModelMapper();
                //model = mm.MapMember(ProfileID);
                //this.MailModel.ScreenName = model.Profile.ScreenName;
                //this.MailModel.UserName = model.Profile.UserName;
                //this.EmailMatches = _membersrepository.GetEmailMatches(model);
                ////determine if we have matches to show
                //this.HasMatches = ( this.EmailMatches.Count > 0) ? true : false;                
            }

            public List<MemberSearchViewModel> EmailMatches { get;  set; }
            public MemberSearchViewModel FeaturedMember { get; set; } //Stores the featured member that applys to this user 
            public MembersViewModel MembersViewModel { get; set; }  //lits this members data
            public EmailModel memberEmailViewModel { get; set; }
            public EmailModel adminEmailViewModel { get; set; }
            public bool HasMatches { get; set; }  //tels you if there are any matches 
    }
}
