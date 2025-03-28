using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_employmentstatus : Repository.Pattern.Ef6.Entity
    {
        public lu_employmentstatus()
        {
            this.profiledatas = new List<profiledata>();
           // this.searchsetting_employmentstatus = new List<searchsetting_employmentstatus>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
      //  public virtual ICollection<searchsetting_employmentstatus> searchsetting_employmentstatus { get; set; }
    }
}
