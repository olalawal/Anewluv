using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{




    [DataContract(Namespace = "")]
    public class lu_systemaddresstype
    {
        //we generate this manually from enums for now
        [Key]
        public int id { get; set; }    
        public string description { get; set; }
        public bool? active { get; set; }
        public DateTime? creationdate { get; set; }
        public DateTime? removaldate { get; set; }

    }
}
