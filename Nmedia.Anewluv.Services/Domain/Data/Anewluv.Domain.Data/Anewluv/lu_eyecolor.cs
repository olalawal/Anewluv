using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_eyecolor
    {
        public lu_eyecolor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_eyecolor = new List<searchsetting_eyecolor>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
         public virtual ICollection<searchsetting_eyecolor> searchsetting_eyecolor { get; set; }
    }
}
