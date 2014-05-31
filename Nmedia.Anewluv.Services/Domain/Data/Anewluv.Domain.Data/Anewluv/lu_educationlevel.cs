using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_educationlevel
    {
        public lu_educationlevel()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_educationlevel = new List<searchsetting_educationlevel>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_educationlevel> searchsetting_educationlevel { get; set; }
    }
}
