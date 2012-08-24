using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class lu_photoapprovalstatus
    {
        public string value { get; set; }       
        [Key]
        public int id { get; set; }
        //use the ID
        //public string value { get; set; }

    }
}
