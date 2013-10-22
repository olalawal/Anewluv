using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract(IsReference = true)]
    public class like
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public int likeprofile_id { get; set; }
        [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }
        [DataMember]
        public virtual profilemetadata likeprofilemetadata { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public DateTime? viewdate { get; set; }
        [DataMember]
        public DateTime? modificationdate { get; set; }
        [DataMember]
        public DateTime? deletedbymemberdate { get; set; }
                  [DataMember]public DateTime? deletedbylikedate { get; set; }
        public bool? mutual { get; set; }
    }
}
