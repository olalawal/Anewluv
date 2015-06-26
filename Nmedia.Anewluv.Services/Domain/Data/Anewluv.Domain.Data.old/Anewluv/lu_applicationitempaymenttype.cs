using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_applicationpaymenttype : Repository.Pattern.Ef6.Entity
    {
        public lu_applicationpaymenttype()
        {
            this.applications = new List<application>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<application> applications { get; set; }
    }
}
