using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class hotlist
    {

        [Key]
        public int id { get; set; }
       public int profile_id { get; set; }
        public int hotlistprofile_id { get; set; }
       public virtual profilemetadata profilemetadata { get; set; }
       public virtual profilemetadata hotlistprofilemetadata { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? viewdate { get; set; }
        public DateTime? modificationdate { get; set; }  
        public DateTime? deletedbymemberdate { get; set; }
        public DateTime? deletedbyhotlistdate { get; set; }
        public int mutual { get; set; }
    }
}
