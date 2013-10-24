using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    //appearance
    public class lu_eyecolor
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
     
    }
}
