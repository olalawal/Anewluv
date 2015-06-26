using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_applicationtype : Repository.Pattern.Ef6.Entity
    {
        
         public lu_applicationtype()
        {
            this.applications = new List<application>();
        }

        public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        public string description { get; set; }
        public virtual ICollection<application> applications { get; set; }
    }
}
