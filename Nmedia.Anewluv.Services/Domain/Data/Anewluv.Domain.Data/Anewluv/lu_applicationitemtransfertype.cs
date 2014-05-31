using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_applicationitemtransfertype
    {
        public lu_applicationitemtransfertype()
        {
            this.applicationitems = new List<applicationitem>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<applicationitem> applicationitems { get; set; }
    }
}
