using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    //10-5-2012 looks similar toe mailboxmessage model in DB 
 [DataContract] public class mailmessageviewmodel
    {
  
            // Properties
            [DataMember]  public int mailboxmessage_id { get; set; }
            [DataMember]  public int? uniqueid { get; set; }
            [DataMember]  public int sender_id { get; set; }
            [Required(ErrorMessage = "Message is required")]
            [DataMember]  public string body { get; set; }
            [Required(ErrorMessage = "Subject is required")]
            [DataMember]  public string subject { get; set; }
           [DataMember]    public DateTime? creationdate { get; set; }
            [DataMember]  public int recipient_id { get; set; }
            [DataMember]  public string senderscreenname { get; set; }
            [DataMember]  public string recipientscreenname { get; set; }
        
    }
}
