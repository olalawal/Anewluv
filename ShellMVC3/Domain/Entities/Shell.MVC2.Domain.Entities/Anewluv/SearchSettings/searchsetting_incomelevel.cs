using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class searchsetting_incomelevel
    {
        [Key]
        public int? id { get; set; }
        public virtual lu_incomelevel incomelevel { get; set; }
        public int searchsetting_id { get; set; }
        public virtual searchsetting  searchsetting { get; set; } 
       
    }
}
