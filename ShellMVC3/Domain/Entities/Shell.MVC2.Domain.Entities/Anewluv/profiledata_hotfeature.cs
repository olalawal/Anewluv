using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract(IsReference = true)]
    public class profiledata_hotfeature
    {

        
        public virtual lu_hotfeature hotfeature { get; set; }
        [Key ]
        public int id { get; set; }
        public int profile_id { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
       
    }
}
