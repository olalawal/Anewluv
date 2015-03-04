using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_lookingfor : Entity
    {
        public lu_lookingfor()
        {
            this.profiledata_lookingfor = new List<profiledata_lookingfor>();
            this.searchsetting_lookingfor = new List<searchsetting_lookingfor>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_lookingfor> searchsetting_lookingfor { get; set; }
    }
}
