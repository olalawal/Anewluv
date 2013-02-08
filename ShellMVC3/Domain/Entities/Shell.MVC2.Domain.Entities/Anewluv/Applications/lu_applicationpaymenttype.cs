using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class lu_applicationpaymenttype
    {     
        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public DateTime? activateddate { get; set; }
        [DataMember]
        public DateTime? deactivateddate { get; set; }
        [DataMember]
        public bool? active { get; set; }
             
    }
}
