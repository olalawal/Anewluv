using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class applicationicons
    {

        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public application application { get; set; }
        [DataMember]
        public lu_applicationicontype imagetype { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember ]
        public byte? icon { get; set; } 
    }
}
