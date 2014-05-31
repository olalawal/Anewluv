using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_maritalstatus
    {
        public lu_maritalstatus()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_maritalstatus = new List<searchsetting_maritalstatus>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
    }
}
