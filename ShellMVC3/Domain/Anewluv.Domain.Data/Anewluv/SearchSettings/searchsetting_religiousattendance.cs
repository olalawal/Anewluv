using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class searchsetting_religiousattendance
    {
        [Key]
        public int? id { get; set; }
        public virtual lu_religiousattendance religiousattendance { get; set; }
     
        public virtual searchsetting  searchsetting { get; set; }        
     
    }
}
