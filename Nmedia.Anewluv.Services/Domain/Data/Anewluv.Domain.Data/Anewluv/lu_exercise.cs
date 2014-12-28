using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_exercise
    {
        public lu_exercise()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_exercise = new List<searchsetting_exercise>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
       public virtual ICollection<profiledata> profiledatas { get; set; }
          public virtual ICollection<searchsetting_exercise> searchsetting_exercise { get; set; }
    }
}
