using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
      [DataContract]
    public partial class lu_bodytype
    {
        public lu_bodytype()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_bodytype = new List<searchsetting_bodytype>();
        }

      
        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]   public string description { get; set; }    
        public virtual ICollection<profiledata> profiledatas { get; set; }     
         public virtual ICollection<searchsetting_bodytype> searchsetting_bodytype { get; set; }
    }
}
