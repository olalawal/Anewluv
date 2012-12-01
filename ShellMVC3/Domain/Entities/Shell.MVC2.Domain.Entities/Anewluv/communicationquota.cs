using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
   public class communicationquota
    {
       [Key]
       [DataMember]
       public int id { get; set; }
       [DataMember]
       public bool? active { get; set; }
       [DataMember]
       public string quotadescription { get; set; }
       [DataMember]
       public string quotaname { get; set; }
       [DataMember]
       public int? quotaroleid { get; set; }
       [DataMember]
       public int? quotavalue { get; set; }
       [DataMember]
       public string updaterprofile_id { get; set; }
       [DataMember]
       public virtual profiledata updaterprofiledata { get; set; }
       [DataMember]
       public DateTime? updatedate { get; set; }
    }
}
