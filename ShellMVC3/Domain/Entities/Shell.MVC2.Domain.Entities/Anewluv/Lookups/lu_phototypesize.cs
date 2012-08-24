using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //Gallery, Detail,larger, Full <- might do away with full to save memory or 
    //at least compress and rescan it
    public class lu_phototypesize
    {

       // public virtual ICollection<photoconversions> converted { get; set; }
        [Key]
        public string id { get; set; }
        public virtual lu_phototype imagetype { get; set; }  
        public string size { get; set; }
    }
}
