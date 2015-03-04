using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_photostatusdescription : Repository.Pattern.Ef6.Entity
    {
        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
        public Nullable<int> photostatus_id { get; set; }
        [IgnoreDataMember]  public virtual lu_photostatus lu_photostatus { get; set; }
    }
}
