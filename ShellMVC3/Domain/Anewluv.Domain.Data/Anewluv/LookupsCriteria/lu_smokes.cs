using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class lu_smokes
    {
    
    [Key]
        public int id { get; set; }
        public string description { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
    }
}
