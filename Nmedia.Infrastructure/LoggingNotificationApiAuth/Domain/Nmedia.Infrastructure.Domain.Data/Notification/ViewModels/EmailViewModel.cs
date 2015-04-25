using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using Anewluv.Domain.Data.ViewModels;



namespace Nmedia.Infrastructure.Domain.Data.Notification
{


   
    [DataContract(Name = "EmailViewModel")]
    public class EmailViewModel
    {

            
   
        //no contructor here will be populated in Notification service
        public EmailViewModel()
        {
            try
            {
               // this.promotionobjects = new List<promotionobject>();
                this.EmailModel = new EmailModel();
               // this.adminEmailViewModel = new EmailModel();
                
             //   this.messagetype = new lu_messagetype();
             //   this.emailmatches = new List<MemberSearchViewModel>();
             //   this.FeaturedMember = new MemberSearchViewModel();
             //   this.MembersViewModel = new MembersViewModel();
             //   this.contact = new MemberSearchViewModel();
             //   this.template = new lu_template();
            }
            catch (Exception ex)
            {
            
            }

        }

        //Models for actual email data , body username etc
        //[DataMember]
        //public List<EmailModel> IncomingEmailViewModel { get; set; }
        [DataMember]
        public EmailModel EmailModel { get; set; }  
  
       
        //global notifications
        [DataMember]
        public string SysteMessages { get; set; } //12-19-2012 olawal added for error message logging 
        [DataMember]
        public string News { get; set; }  //Lists news  
        
        //Viewmodels for detailed profile data only needed for featured member or who emailed or sent chat
        [DataMember]
        public MembersViewModel MemberDetail { get; set; }  //lists the deatil for the user who did the original action 
        [DataMember]
        public MemberActionsModel MemberActions { get; set; }
        [DataMember]
        public MemberSearchViewModel FeaturedMember { get; set; } //Stores the featured member that applys to this user
       


        //macthes stuff
        [DataMember]
        public bool HasMatches { get; set; }  //tels you if there are any matches 
        [DataMember]
        public List<MemberSearchViewModel> emailmatches { get; set; }
      

    }
}
