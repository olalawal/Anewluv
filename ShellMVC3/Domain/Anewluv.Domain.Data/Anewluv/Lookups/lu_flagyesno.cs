using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class lu_flagyesno
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
     
    }
}
