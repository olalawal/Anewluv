using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_securityquestion
    {
        public lu_securityquestion()
        {
            this.profiles = new List<profile>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profile> profiles { get; set; }
    }
}
