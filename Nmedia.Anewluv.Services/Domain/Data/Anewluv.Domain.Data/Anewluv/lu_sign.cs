using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_sign : Entity
    {
        public lu_sign()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_sign = new List<searchsetting_sign>();
        }

        [NotMapped, DataMember]  public bool selected { get; set; }
        [DataMember]   public int id { get; set; }
        public string month { get; set; }
        [DataMember]   public string description { get; set; }
       public virtual ICollection<profiledata> profiledatas { get; set; }
         public virtual ICollection<searchsetting_sign> searchsetting_sign { get; set; }
    }
}
