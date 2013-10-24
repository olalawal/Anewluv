using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract(IsReference = true)]
    public class rating
    {
        [Key]
         [DataMember]
         public int id { get; set; }
       // public virtual ICollection<ratingvalue> ratingvalues { get; set; }       
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int? ratingmaxvalue { get; set; }
        [DataMember]
        public int? ratingweight { get; set; }
        [DataMember]
        public long? increment { get; set; }
    }
}
