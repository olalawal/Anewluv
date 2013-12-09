using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_applicationitempaymenttype
    {
        public lu_applicationitempaymenttype()
        {
            this.applicationitems = new List<applicationitem>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<applicationitem> applicationitems { get; set; }
    }
}
