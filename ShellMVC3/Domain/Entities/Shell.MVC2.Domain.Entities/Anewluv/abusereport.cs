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
        //public virtual abuser abuser { get; set; }

        public string abusereporter_id { get; set; }
        public string abuser_id { get; set; }

        public virtual profiledata abuser { get; set; }
        public virtual profiledata abusereporter { get; set; }
        public virtual ICollection<profiledata> reviewers { get; set; }
        public DateTime creationdate { get; set; }
        public ICollection<abusereportnotes> notes { get; set; }



    }
}

      