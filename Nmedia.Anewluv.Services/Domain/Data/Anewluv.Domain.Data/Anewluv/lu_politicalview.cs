using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_politicalview
    {
        public lu_politicalview()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_politicalview = new List<searchsetting_politicalview>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_politicalview> searchsetting_politicalview { get; set; }
    }
}
