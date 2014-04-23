using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract] public partial class lu_religiousattendance
    {
        public lu_religiousattendance()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_religiousattendance = new List<searchsetting_religiousattendance>();
        }

        [DataMember]   public int id { get; set; }
        [DataMember]   public string description { get; set; }
        [IgnoreDataMember]  public virtual ICollection<profiledata> profiledatas { get; set; }
        [IgnoreDataMember]  public virtual ICollection<searchsetting_religiousattendance> searchsetting_religiousattendance { get; set; }
    }
}
