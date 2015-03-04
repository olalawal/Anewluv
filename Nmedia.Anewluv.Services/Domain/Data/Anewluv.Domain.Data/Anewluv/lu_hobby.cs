using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_hobby : Entity
    {
        public lu_hobby()
        {
            this.profiledata_hobby = new List<profiledata_hobby>();
            this.searchsetting_hobby = new List<searchsetting_hobby>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
     public virtual ICollection<profiledata_hobby> profiledata_hobby { get; set; }
        public virtual ICollection<searchsetting_hobby> searchsetting_hobby { get; set; }
    }
}
