using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
   public class communicationquota
    {
       [Key]
       public int id { get; set; }
        public bool? active { get; set; }    
        public string quotadescription { get; set; } 
        public string quotaname { get; set; }
        public int? quotaroleid { get; set; }
        public int? quotavalue { get; set; }
        public string updaterprofile_id { get; set; }
        public virtual profiledata updaterprofiledata { get; set; }
        public DateTime? updatedate { get; set; }
    }
}
