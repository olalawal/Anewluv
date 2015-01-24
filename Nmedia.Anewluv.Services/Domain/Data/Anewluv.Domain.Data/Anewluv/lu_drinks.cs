using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_drinks
    {
        public lu_drinks()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_drink = new List<searchsetting_drink>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
         public virtual ICollection<searchsetting_drink> searchsetting_drink { get; set; }
    }
}
