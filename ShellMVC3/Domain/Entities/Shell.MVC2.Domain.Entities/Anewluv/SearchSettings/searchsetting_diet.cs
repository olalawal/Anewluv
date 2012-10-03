using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class searchsetting_diet
    {
        [Key]
        public int? id { get; set; }
        public virtual lu_diet diet { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; }   
       
    }
}
