using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_wantskids
    {
        public lu_wantskids()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_wantkids = new List<searchsetting_wantkids>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_wantkids> searchsetting_wantkids { get; set; }
    }
}
