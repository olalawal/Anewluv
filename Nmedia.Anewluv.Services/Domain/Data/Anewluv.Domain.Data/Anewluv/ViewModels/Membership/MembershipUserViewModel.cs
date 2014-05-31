using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
   public class MembershipUserViewModel
    {
       [DataMember]
       public  string username { get; set; } 
       [DataMember]
       public   string password { get; set; }
       [DataMember]
       public string openidIdentifer { get; set; }
       [DataMember]
       public string openidProvidername { get; set; }
       [DataMember]
       public string email { get; set; }
       [DataMember]
       public string securityQuestion { get; set; }
       [DataMember]
       public string securityAnswer { get; set; }
       [DataMember]
       public DateTime birthdate { get; set; }
       [DataMember]
       public string gender { get; set; }
       [DataMember]
       public string country { get; set; }
       [DataMember]
       public string city { get; set; }
       [DataMember]
       public string stateprovince { get; set; } 
         [DataMember]
       public string longitude { get; set; }
         [DataMember]
       public string lattitude { get; set; } 
         [DataMember]
       public string screenname { get; set; }
         [DataMember]
       public string zippostalcode { get; set; } 
       public string activationcode { get; set; }
          [DataMember]
       public bool isApproved { get; set; }
          [DataMember]
       public object providerUserKey { get; set; }
          [DataMember]
       public MembershipCreateStatus status { get; set; }

    }
}

