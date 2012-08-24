using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class friend
    {
        [Key]
        public int id { get; set; }
        public string profile_id { get; set; }
        public string friendprofile_id { get; set; }
       public virtual profiledata profiledata { get; set; }
       public virtual profiledata friendprofiledata { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? viewdate { get; set; }
        public DateTime? modificationdate { get; set; }  
        public DateTime? deletedbymemberdate { get; set; }
        public DateTime? deletedbyfrienddate { get; set; }
        public int mutual { get; set; }
    }
}
