using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_sign
    {
        public lu_sign()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_sign = new List<searchsetting_sign>();
        }

        [NotMapped, DataMember]  public bool? isselected { get; set; }
        [DataMember]   public int id { get; set; }
        public string month { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_sign> searchsetting_sign { get; set; }
    }
}
