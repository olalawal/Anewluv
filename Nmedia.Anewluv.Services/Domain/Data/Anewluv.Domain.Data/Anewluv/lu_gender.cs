using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_gender
    {
        public lu_gender()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_gender = new List<searchsetting_gender>();
            this.visiblitysettings_gender = new List<visiblitysettings_gender>();
        }

        [DataMember]   public int id { get; set; }
        [NotMapped, DataMember]
        public bool? isselected { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_gender> searchsetting_gender { get; set; }
        [IgnoreDataMember]  public virtual ICollection<visiblitysettings_gender> visiblitysettings_gender { get; set; }
    }
}
