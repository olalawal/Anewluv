using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class searchsetting_religion
    {
       
        [Key]
        public int? id { get; set; }
        public virtual lu_religion religion { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; } 
    
    }
}
