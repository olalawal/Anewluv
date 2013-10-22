using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
      [DataContract(IsReference = true)]
    public class mailupdatefreqency
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int? updatefreqency { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }       
      
    }
}
