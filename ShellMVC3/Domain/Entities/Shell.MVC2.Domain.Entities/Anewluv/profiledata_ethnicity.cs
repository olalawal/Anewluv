using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profiledata_ethnicity
    {

        
        public virtual lu_ethnicity ethnicty { get; set; }
        [Key]
        public int id { get; set; }
        public int profile_id { get; set; }
       public virtual profilemetadata  profilemetadata { get; set; }
        
    }
}
