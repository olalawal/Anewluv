using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_humor : Entity
    {
        public lu_humor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_humor = new List<searchsetting_humor>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
       public virtual ICollection<searchsetting_humor> searchsetting_humor { get; set; }
    }
}
