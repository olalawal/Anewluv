using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_havekids : Entity
    {
        public lu_havekids()
        {
            this.profiledatas = new List<profiledata>();
          //  this.searchsetting_havekids = new List<searchsetting_havekids>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
       public virtual ICollection<profiledata> profiledatas { get; set; }
         //public virtual ICollection<searchsetting_havekids> searchsetting_havekids { get; set; }
    }
}
