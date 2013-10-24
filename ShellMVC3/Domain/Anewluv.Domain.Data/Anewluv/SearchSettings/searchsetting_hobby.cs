using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class searchsetting_hobby
    {
        [Key]
        public int? id { get; set; }
        public virtual lu_hobby hobby { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; } 
     
    }
}
