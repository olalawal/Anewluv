using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Anewluv.Domain.Data
{
    public class lu_photostatus
    {
        public string description{ get; set; }       
        [Key]
        public int id { get; set; }
        //use the ID
        //public string value { get; set; }
       //[NotMapped]
        public bool selected { get; set; }

    }
}
