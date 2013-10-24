using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class searchsetting_haircolor
    {
        
        [Key]
        public int? id { get; set; }
        public virtual lu_haircolor haircolor { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; } 

    
    }

}
