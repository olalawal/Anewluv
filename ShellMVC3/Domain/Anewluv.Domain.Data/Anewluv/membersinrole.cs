using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
     [DataContract(IsReference = true)]
    public class membersinrole
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool? active { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual profile profile { get; set; }
        [DataMember]
        public virtual lu_role role { get; set; }
        [DataMember]
        public DateTime? roleexpiredate { get; set; }
        //public int? roleID { get; set; }
        [DataMember]
        public DateTime? rolestartdate { get; set; }
       
    }
}
