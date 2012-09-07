using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //appearance
    public class lu_haircolor
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
     
    }
}
