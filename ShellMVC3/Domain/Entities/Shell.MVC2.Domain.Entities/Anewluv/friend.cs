using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract(IsReference = true)]
    public class friend
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
       public int profile_id { get; set; }
        [DataMember]
        public int friendprofile_id { get; set; }
        [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }
        [DataMember]
        public virtual profilemetadata friendprofilemetadata { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public DateTime? viewdate { get; set; }
        [DataMember]
        public DateTime? modificationdate { get; set; }
        [DataMember]
        public DateTime? deletedbymemberdate { get; set; }
        [DataMember]
        public DateTime? deletedbyfrienddate { get; set; }
        [DataMember]
        public bool? mutual { get; set; }
    }
}
