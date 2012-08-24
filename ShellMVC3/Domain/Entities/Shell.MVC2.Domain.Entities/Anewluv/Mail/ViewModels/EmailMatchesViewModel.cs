using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shell.MVC2.Models;


using System.Web.Mvc;
using System.Reflection;
using Shell.MVC2.Repositories;


using Ninject;
using Ninject.Web.Mvc;

namespace Shell.MVC2.ViewModels.Email
{
    public class EmailMatchesViewModel
    {
        [Inject]        
        public MembersRepository _membersrepository  { get; set; }

        public EmailMatchesViewModel(string ProfileID)
            {

                _membersrepository = new MembersRepository();

                this.EmailModel = new EmailModel();            
                this.EmailMatches = new List<MemberSearchViewModel>();
             
                //get the weekly matches for this user 
                //get the membersviewmodel for this person
                MembersViewModel model = new MembersViewModel();
                var mm = new ViewModelMapper();
                model = mm.MapMember(ProfileID);
                this.EmailModel.ScreenName = model.Profile.ScreenName;
                this.EmailModel.UserName = model.Profile.UserName;
                this.EmailMatches = _membersrepository.GetEmailMatches(model);
                //determine if we have matches to show
                this.HasMatches = ( this.EmailMatches.Count > 0) ? true : false;
                
            }

            public List<MemberSearchViewModel> EmailMatches { get;  set; }
            public EmailModel EmailModel { get;  set; }
            public bool HasMatches { get; set; }
          
    }
}