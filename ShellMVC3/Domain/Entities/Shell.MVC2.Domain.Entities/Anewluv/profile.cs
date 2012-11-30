using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]  
    public class profile
    {
             [DataMember]
        public virtual profiledata profiledata { get; set; }
           [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }

           [DataMember]
        public int id { get; set; }
       [DataMember]
        public string username { get; set; }
           [DataMember]
        public string emailaddress { get; set; }
           [DataMember]          
        public string screenname { get; set; }
           [DataMember]
        public string activationcode { get; set; }
        [DataMember]
        public int? dailsentmessagequota { get; set; }
           [DataMember]
        public int? dailysentemailquota { get; set; }
           [DataMember]
        public byte? forwardmessages { get; set; }
           [DataMember]
        public DateTime? logindate { get; set; }
           [DataMember]
        public DateTime? modificationdate { get; set; }
           [DataMember]
        public DateTime? creationdate { get; set; }
  
        public virtual lu_profilestatus status { get; set; }
           [DataMember]        
        public bool? readprivacystatement { get; set; }
           [DataMember]
        public bool? readtemsofuse { get; set; }
         
        public string password { get; set; }
           [DataMember]
        public DateTime? passwordChangeddate { get; set; }
           [DataMember]
        public int? passwordchangecount { get; set; }
           [DataMember]
        public DateTime? failedpasswordchangedate { get; set; }
           [DataMember]
        public int? failedpasswordchangeattemptcount { get; set; }
               [DataMember]
        public string salt { get; set; }
              [DataMember]
        public string securityanswer { get; set; }

       
        public virtual lu_securityquestion securityquestion { get; set; } 
        //Anti spam stuff might do away with
          [DataMember]
        public int? sentemailquotahitcount { get; set; }
            [DataMember]
        public int? sentmessagequotahitcount { get; set; }
      
        //linked collections
        public virtual ICollection<membersinrole > memberroles { get; set; }
        public virtual ICollection<profileactivity> profileactivity { get; set; }
        public virtual ICollection<openid> openids { get; set; }
        public virtual ICollection<userlogtime> logontimes { get; set; }

      



    }
}
