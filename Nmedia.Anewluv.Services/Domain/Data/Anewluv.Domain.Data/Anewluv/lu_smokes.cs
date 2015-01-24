using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_smokes
    {
        public lu_smokes()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_smokes = new List<searchsetting_smokes>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
          public virtual ICollection<profiledata> profiledatas { get; set; }
       public virtual ICollection<searchsetting_smokes> searchsetting_smokes { get; set; }
    }
}
