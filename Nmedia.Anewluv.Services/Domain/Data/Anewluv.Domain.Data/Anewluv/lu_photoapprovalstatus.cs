using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    public partial class lu_photoapprovalstatus
    {
        public lu_photoapprovalstatus()
        {
            this.photos = new List<photo>();
        }
        [NotMapped, DataMember] public bool? isselected { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
