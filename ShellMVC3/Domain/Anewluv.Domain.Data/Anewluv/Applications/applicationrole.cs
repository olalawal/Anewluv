using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Anewluv.Domain.Data
{
    [DataContract] 
    public class applicationrole
    {

        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool? active { get; set; }
        [DataMember]
        public int application_id { get; set; }
        [DataMember]
        public virtual application application { get; set; }
        [DataMember]
        public virtual lu_role role { get; set; }
        [DataMember]
        public DateTime? roleexpiredate { get; set; }
        //public int? roleID { get; set; }
        [DataMember]
        public DateTime? rolestartdate { get; set; }
        public DateTime? deactivationdate { get; set; }
        public DateTime? creationdate { get; set; }
    }
}
