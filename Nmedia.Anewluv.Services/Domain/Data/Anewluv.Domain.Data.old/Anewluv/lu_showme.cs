using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_showme : Entity
    {
        public lu_showme()
        {
         //   this.searchsetting_showme = new List<searchsetting_showme>();
        }

        [DataMember] public int id { get; set; }
        [NotMapped,DataMember]  public bool selected { get; set; }
        [DataMember] public string description { get; set; }
        //public virtual ICollection<searchsetting_showme> searchsetting_showme { get; set; }
    }
}
