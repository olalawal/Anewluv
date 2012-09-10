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
        public int profile_id { get; set; }
        public int blockprofile_id { get; set; }
        public int reviewerprofile_id { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profilemetadata blockedprofilemetadata { get; set; }
        public virtual profilemetadata reviewerprofilemetadata { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? modificationdate { get; set; }  
        public DateTime? reviewdate { get; set; }
        public DateTime? removedate { get; set; }      
        public int? mutual { get; set; }
     
    }
}
