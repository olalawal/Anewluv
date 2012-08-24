using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class block
    {
        [Key]
        public int id { get; set; }
        public string profile_id { get; set; }
        public string blockprofile_id { get; set; }
        public string reviewerprofile_id { get; set; }
        public virtual profiledata profiledata { get; set; }
        public virtual profiledata blockedprofiledata { get; set; }
       public virtual profiledata reviewerprofiledata { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? modificationdate { get; set; }  
        public DateTime? reviewdate { get; set; }
        public DateTime? removedate { get; set; }      
        public int? mutual { get; set; }
     
    }
}
