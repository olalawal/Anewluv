using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{

    [DataContract]
    public class lu_gender
    {
        [Key]
        [DataMember]
        public int id { get; set; }
      [DataMember]
        public string description { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
     
    }
}
