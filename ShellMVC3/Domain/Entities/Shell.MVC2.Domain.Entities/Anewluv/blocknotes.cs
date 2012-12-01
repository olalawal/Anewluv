using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class blocknotes
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int block_id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual block block { get; set; }
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
