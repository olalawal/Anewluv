using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
      [DataContract(IsReference = true)]
    public class photoalbum
    {
      
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public virtual ICollection<photo> photos { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public virtual ICollection<photoalbum_securitylevel> albumsecuritylevels { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public profilemetadata profilemetadata { get; set; }
        

    }
}
