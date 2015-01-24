using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
          public virtual ICollection<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
    }
}
