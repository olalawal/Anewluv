using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_maritalstatus : Entity
    {
        public lu_maritalstatus()
        {
            this.profiledatas = new List<profiledata>();
           // this.searchsetting_maritalstatus = new List<searchsetting_maritalstatus>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
       // public virtual ICollection<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
    }
}
