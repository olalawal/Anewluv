using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class application
    {
     [DataMember ][Key]
        public int id { get; set; }
        public lu_applicationtype applicationtype { get; set; }
        [DataMember]
        public DateTime creationdate { get; set; }
        [DataMember]
        public bool active { get; set; }
        [DataMember]
        public DateTime? deactivationdate { get; set; }
        public virtual ICollection<applicationiconconversions> icons { get; set; }
        public virtual ICollection<applicationitem> items { get; set; }
    }
}
