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
              //  this.adminEmailViewModel = new EmailModel();
                this.messagetype = new lu_messagetype();
                this.emailmatches = new List<MemberSearchViewModel>();
                this.FeaturedMember = new MemberSearchViewModel();
                this.MembersViewModel = new MembersViewModel();
                this.contact = new MemberSearchViewModel();
                this.template = new lu_template();
            }
            catch (Exception ex)
            {
            
            }

        }

        [DataMember]
        public string templateid { get; set; }   
        [DataMember]
        public EmailModel EmailModel { get; set; }
       // [DataMember]
     //   public EmailModel adminEmailViewModel { get; set; }
        [DataMember]
        public MembersViewModel MembersViewModel { get; set; }  //lits this members data    
        [DataMember]
        public string SysteMessages { get; set; } //12-19-2012 olawal added for error message logging       
        [DataMember]
        public lu_messagetype messagetype { get; set; }
        [DataMember]
        public lu_template template { get; set; }
        public List<MemberSearchViewModel> emailmatches { get; set; }
        [DataMember]
        public MemberSearchViewModel FeaturedMember { get; set; } //Stores the featured member that applys to this user        
        [DataMember]     
        public bool HasMatches { get; set; }  //tels you if there are any matches 
        [DataMember]
        public MemberSearchViewModel contact { get; set; } //Stores the current members info that contacted or was contacted by the email recipient
        
    }
}
