using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models;



using System.Reflection;





namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class EmailMatchesViewModel
    {
           
       // public MembersRepository _membersrepository  { get; set; }

        public EmailMatchesViewModel(string ProfileID)
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
            public MailModel MailModel { get;  set; }
            public bool HasMatches { get; set; }
          
    }
}