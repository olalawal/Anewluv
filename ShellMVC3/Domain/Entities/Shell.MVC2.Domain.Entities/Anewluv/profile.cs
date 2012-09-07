using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profile
    {
       
        public int id { get; set; }    
    
        public string username { get; set; }
        public string emailaddress { get; set; }
        public string screenname { get; set; }
        public string activationcode { get; set; }
     
        public int? dailsentmessagequota { get; set; }
        public int? dailysentemailquota { get; set; }
        public byte? forwardmessages { get; set; }
        public DateTime? logindate { get; set; }  
        public DateTime? modificationdate { get; set; }
        public DateTime creationdate { get; set; }
  
        public virtual lu_profilestatus status { get; set; }     
        public bool? readprivacystatement { get; set; }
        public bool? readtemsofuse { get; set; }

        public string password { get; set; }
        public DateTime? passwordChangeddate { get; set; } 
        public int? passwordchangecount { get; set; }
        public DateTime? failedpasswordchangedate { get; set; }
        public int? failedpasswordchangeattemptcount { get; set; } 
        public string salt { get; set; }   
        public string securityanswer { get; set; }

       
        public virtual lu_securityquestion securityquestion { get; set; } 
        //Anti spam stuff might do away with
        public int? sentemailquotahitcount { get; set; }
        public int? sentmessagequotahitcount { get; set; }
      
        //linked collections
        public virtual ICollection<membersinrole > roles { get; set; }
        public virtual ICollection<activitylog> activitylogs { get; set; }
        public virtual ICollection<openid> openids { get; set; }

        public virtual profiledata profiledata { get; set; }



    }
}
