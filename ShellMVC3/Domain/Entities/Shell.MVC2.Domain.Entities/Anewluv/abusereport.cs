using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class abusereport
    {
        [Key]
        public int id { get; set; }
        public virtual lu_abusetype abusetype { get; set; }      
        public int abusereporter_id { get; set; }
        public int abuser_id { get; set; }
        public virtual profilemetadata abuser { get; set; }
        public virtual profilemetadata abusereporter { get; set; }    
        public DateTime creationdate { get; set; }
        public ICollection<abusereportnotes> notes { get; set; }



    }
}

      