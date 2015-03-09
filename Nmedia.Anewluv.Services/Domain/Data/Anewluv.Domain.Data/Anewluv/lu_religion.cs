using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_religion : Entity
    {
        public lu_religion()
        {
            this.profiledatas = new List<profiledata>();
        //    this.searchsetting_religion = new List<searchsetting_religion>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
       public virtual ICollection<profiledata> profiledatas { get; set; }
     //    public virtual ICollection<searchsetting_religion> searchsetting_religion { get; set; }
    }
}
