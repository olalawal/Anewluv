using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{

    [DataContract]
    public class lu_openidprovider
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
