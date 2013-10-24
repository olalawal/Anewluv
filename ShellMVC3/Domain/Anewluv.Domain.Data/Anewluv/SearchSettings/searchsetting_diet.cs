using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;


namespace Anewluv.Domain.Data
{
    public class searchsetting_diet
    {
        [Key]
        public int? id { get; set; }
        public virtual lu_diet diet { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; }   
       
    }
}
