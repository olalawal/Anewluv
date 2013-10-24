using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public class lu_abusetype
    {
      
        [Key]
        public int id { get; set; }
        public string description { get; set; }
       //[NotMapped]
        public bool selected { get; set; }
    }
}
