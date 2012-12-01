using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
       [DataContract(IsReference = true)]
    public class systempagesetting
    {
        [Key]
      [DataMember]  public int id { get; set; }
        [DataMember]
        public string bodycssstylename { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int? hitCount { get; set; }
        [DataMember]
        public bool? ismasterpage { get; set; }
        [DataMember]
        public string path { get; set; }
        [DataMember]
        public string title { get; set; }
    }
}
