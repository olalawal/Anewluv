using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_profession : Repository.Pattern.Ef6.Entity
    {
        public lu_profession()
        {
            this.profiledatas = new List<profiledata>();
        //    this.searchsetting_profession = new List<searchsetting_profession>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
         public virtual ICollection<profiledata> profiledatas { get; set; }
     //  public virtual ICollection<searchsetting_profession> searchsetting_profession { get; set; }
    }
}
