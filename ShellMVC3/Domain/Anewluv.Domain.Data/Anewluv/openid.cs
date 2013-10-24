using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract(IsReference = true)]
    public class openid
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool? active { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public string openididentifier { get; set; }          
        [DataMember]
        public virtual lu_openidprovider openidprovider { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual profile profile { get; set; }
       
    }
}
