using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract(IsReference = true)]
    public class userlogtime
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime? logintime { get; set; }
        [DataMember]
        public DateTime? logouttime { get; set; }
        [DataMember]
        public Boolean? offline { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual profile profile { get; set; }
        [DataMember]
        public string sessionid { get; set; }
    }
}
