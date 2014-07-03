using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{
    [DataContract]
    public class EmailModel
    {
     [DataMember]  public string to { get; set; }
     [DataMember]
     public string from { get; set; }  //only used in contact us
     [DataMember]
     public string subject { get; set; }
     [DataMember]
     public string body { get; set; }
     [DataMember]
     public string News { get; set; }  //Lists news 
     [DataMember]
     public string Messages { get; set; }  //Personal user updates strored here
     [DataMember]
     public string username { get; set; }  //Personal user updates strored here
     [DataMember]
     public string userlogon { get; set; }  //Personal user updates strored here
    }
}
