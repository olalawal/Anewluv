using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
        [DataContract(IsReference = true)]
    public class abusereportnotes
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int abusereport_id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual abusereport abusereport { get; set; }
        [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }
        [DataMember]
        public virtual lu_notetype notetype { get; set; }
        [DataMember]
        public string note { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public DateTime? reviewdate { get; set; }



    }
}
