using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_abusetype : Entity
    {
        public lu_abusetype()
        {
           // this.abusereports = new List<abusereport>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<note> abusereports { get; set; }
    }
}
