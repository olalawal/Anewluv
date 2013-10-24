using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels.Email
{


    [Serializable]
    [DataContract(Name = "EmailViewModel")]
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

        [DataMember ]
       public List<MemberSearchViewModel> EmailMatches { get;  set; }
       [DataMember]
        public MemberSearchViewModel FeaturedMember { get; set; } //Stores the featured member that applys to this user 
       [DataMember]    
        public MembersViewModel MembersViewModel { get; set; }  //lits this members data
        [DataMember]    
        public EmailModel memberEmailViewModel { get; set; }
        [DataMember]    
        public EmailModel adminEmailViewModel { get; set; }
        [DataMember]   
        public bool HasMatches { get; set; }  //tels you if there are any matches 
         [DataMember]
        public string SysteMessages { get; set; } //12-19-2012 olawal added for error message logging

    }
}
