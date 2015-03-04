using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_haircolor : Entity
    {
        public lu_haircolor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_haircolor = new List<searchsetting_haircolor>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_haircolor> searchsetting_haircolor { get; set; }
    }
}
