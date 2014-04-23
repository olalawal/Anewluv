using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_diet
    {
        public lu_diet()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_diet = new List<searchsetting_diet>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_diet> searchsetting_diet { get; set; }
    }
}
