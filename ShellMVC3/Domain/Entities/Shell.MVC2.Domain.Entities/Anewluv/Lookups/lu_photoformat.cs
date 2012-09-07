using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //Gallery, Detail,larger, Full <- might do away with full to save memory or 
    //at least compress and rescan it
    public class lu_photoformat
    {

       // public virtual ICollection<photoconversions> converted { get; set; }
        [Key]
        public int id { get; set; }
        public string description { get; set; }
    }
}
