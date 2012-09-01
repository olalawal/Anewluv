using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class mailupdatefreqency
    {
        [Key]
        public int id { get; set; }
        public int? updatefreqency { get; set; }
        public string profile_id { get; set; } 
        public virtual profiledata profiledata { get; set; }       
      
    }
}
