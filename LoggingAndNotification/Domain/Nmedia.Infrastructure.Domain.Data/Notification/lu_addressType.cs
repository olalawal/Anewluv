using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Nmedia.Infrastructure.Domain.Data.Notification
{


     [DataContract(Namespace = "")]
    public class lu_addresstype
    {
        //we generate this manually from enums for now
        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }
        [DataMember()]
        public bool? active { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public DateTime? removaldate { get; set; }

    }
}
