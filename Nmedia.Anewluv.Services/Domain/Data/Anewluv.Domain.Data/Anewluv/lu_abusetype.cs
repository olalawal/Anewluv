using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_abusetype
    {
        public lu_abusetype()
        {
            this.abusereports = new List<abusereport>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<abusereport> abusereports { get; set; }
    }
}
