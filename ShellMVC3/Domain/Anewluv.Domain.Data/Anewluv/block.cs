using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    //modified to use block notes to handle reviewing as well since you need to profile Id to review anyways
    [DataContract(IsReference = true)]
    public class block
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public int blockprofile_id { get; set; }
        [DataMember]
        public virtual profilemetadata profilemetadata { get; set; }
        [DataMember]
        public virtual profilemetadata blockedprofilemetadata { get; set; }
        [DataMember]
        public virtual ICollection<blocknotes> notes { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public DateTime? modificationdate { get; set; }
        [DataMember]
        public DateTime? removedate { get; set; }
        [DataMember]
        public int? mutual { get; set; }
     
    }
}
