using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;



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
                this.userEmailViewModel = new EmailModel();
                this.adminEmailViewModel = new EmailModel();
                this.messagetype = new lu_messagetype();
                this.template = new lu_template();
            }
            catch (Exception ex)
            {
            
            }

        }

   
     
        [DataMember]    
        public EmailModel userEmailViewModel { get; set; }      
        [DataMember]    
        public EmailModel adminEmailViewModel { get; set; } 
        [DataMember]
        public string SysteMessages { get; set; } //12-19-2012 olawal added for error message logging
       
        [DataMember]
        public lu_messagetype messagetype { get; set; }
        [DataMember]
        public lu_template template { get; set; }

    }
}
