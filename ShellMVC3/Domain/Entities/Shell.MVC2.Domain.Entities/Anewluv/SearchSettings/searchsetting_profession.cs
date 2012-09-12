using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class searchsetting_profession
    {

        [Key]
        public int? id { get; set; }
        public virtual lu_profession profession { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; } 
       
    }
}
