using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_applicationtype
    {
        public lu_applicationtype()
        {
            this.applications = new List<application>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<application> applications { get; set; }
    }
}
