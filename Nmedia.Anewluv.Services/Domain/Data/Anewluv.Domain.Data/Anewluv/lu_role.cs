using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_role
    {
        public lu_role()
        {
            this.applicationroles = new List<applicationrole>();
            this.membersinroles = new List<membersinrole>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<applicationrole> applicationroles { get; set; }
        [IgnoreDataMember]  public virtual ICollection<membersinrole> membersinroles { get; set; }
    }
}
