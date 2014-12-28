using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_sortbytype
    {
        public lu_sortbytype()
        {
            this.searchsetting_sortbytype = new List<searchsetting_sortbytype>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<searchsetting_sortbytype> searchsetting_sortbytype { get; set; }
    }
}
