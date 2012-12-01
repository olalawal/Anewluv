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
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public virtual lu_abusetype abusetype { get; set; }
        [DataMember]
        public int abusereporter_id { get; set; }
        [DataMember]
        public int abuser_id { get; set; }
        [DataMember]
        public virtual profilemetadata abuser { get; set; }
        [DataMember]
        public virtual profilemetadata abusereporter { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public ICollection<abusereportnotes> notes { get; set; }



    }
}

      