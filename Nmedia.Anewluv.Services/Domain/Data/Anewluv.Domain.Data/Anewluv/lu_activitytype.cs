using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
   [DataContract] public partial class lu_activitytype
    {
        public lu_activitytype()
        {
            this.profileactivities = new List<profileactivity>();
        }

        public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        public string description { get; set; }
        public virtual ICollection<profileactivity> profileactivities { get; set; }
    }
}
