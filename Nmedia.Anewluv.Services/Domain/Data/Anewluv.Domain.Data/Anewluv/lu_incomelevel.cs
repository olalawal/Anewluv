using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_incomelevel
    {
        public lu_incomelevel()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_incomelevel = new List<searchsetting_incomelevel>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
     public virtual ICollection<searchsetting_incomelevel> searchsetting_incomelevel { get; set; }
    }
}
