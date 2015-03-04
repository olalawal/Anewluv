using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_diet : Entity
    {
        public lu_diet()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_diet = new List<searchsetting_diet>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
    public virtual ICollection<profiledata> profiledatas { get; set; }
         public virtual ICollection<searchsetting_diet> searchsetting_diet { get; set; }
    }
}
