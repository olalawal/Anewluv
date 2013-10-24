using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract(IsReference = true)]
    public class profiledata_ethnicity
    {

        [DataMember ]
        public virtual lu_ethnicity ethnicty { get; set; }
        [Key]
        [DataMember]
        public int id { get; set; }
          [DataMember]
        public int profile_id { get; set; }
          [DataMember]
       public virtual profilemetadata  profilemetadata { get; set; }
        
    }
}
