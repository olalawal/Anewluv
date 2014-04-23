using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_livingsituation
    {
        public lu_livingsituation()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_livingstituation = new List<searchsetting_livingstituation>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
    }
}
