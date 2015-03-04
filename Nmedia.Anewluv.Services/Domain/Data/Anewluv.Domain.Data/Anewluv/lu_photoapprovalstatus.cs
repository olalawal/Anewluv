using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    public partial class lu_photoapprovalstatus :Entity
    {
        public lu_photoapprovalstatus()
        {
            this.photos = new List<photo>();
        }
        [NotMapped, DataMember] public bool selected { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
