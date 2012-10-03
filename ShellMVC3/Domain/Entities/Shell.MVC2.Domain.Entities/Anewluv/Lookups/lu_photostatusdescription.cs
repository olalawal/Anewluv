using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class lu_photostatusdescription
    {
       [Key]
        public int id { get; set; }
        public string description { get; set; }
        public lu_photostatus photostatus { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public bool selected { get; set; }
      
        //use the ID
        //public string value { get; set; }

    }
}
