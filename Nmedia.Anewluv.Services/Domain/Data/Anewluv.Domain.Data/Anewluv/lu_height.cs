using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    public partial class lu_height
    {
        public int id { get; set; }
        [NotMapped, DataMember]
        public bool? isselected { get; set; }
        public string description { get; set; }
    }
}
