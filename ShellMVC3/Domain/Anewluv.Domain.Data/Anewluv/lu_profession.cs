using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_profession
    {
        public lu_profession()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_profession = new List<searchsetting_profession>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_profession> searchsetting_profession { get; set; }
    }
}
