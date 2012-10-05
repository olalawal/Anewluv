using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //10-5-2012 looks similar toe mailboxmessage model in DB 
   public class mailmessageviewmodel
    {
  
            // Properties
            public int mailboxmessage_id { get; set; }
            public int? uniqueid { get; set; }
            public int sender_id { get; set; }
            [Required(ErrorMessage = "Message is required")]
            public string body { get; set; }
            [Required(ErrorMessage = "Subject is required")]
            public string subject { get; set; }
            public DateTime? creationdate { get; set; }
            public int recipient_id { get; set; }
            public string senderscreenname { get; set; }
            public string recipientscreenname { get; set; }
        
    }
}
