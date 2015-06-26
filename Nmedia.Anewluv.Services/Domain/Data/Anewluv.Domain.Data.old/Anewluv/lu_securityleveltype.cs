using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_securityleveltype :Entity
    {
        public lu_securityleveltype()
        {
            this.photo_securitylevel = new List<photo_securitylevel>();
            this.photoalbum_securitylevel = new List<photoalbum_securitylevel>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]  public bool selected { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<photo_securitylevel> photo_securitylevel { get; set; }
        [IgnoreDataMember]  public virtual ICollection<photoalbum_securitylevel> photoalbum_securitylevel { get; set; }
    }
}
