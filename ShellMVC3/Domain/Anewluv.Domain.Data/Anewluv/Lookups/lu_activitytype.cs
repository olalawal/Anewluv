using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    //appearance
    public class lu_activitytype
    {
        public string description { get; set; }
        [Key]
        public int id { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
     
    }
}
