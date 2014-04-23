using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_humor
    {
        public lu_humor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_humor = new List<searchsetting_humor>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_humor> searchsetting_humor { get; set; }
    }
}
