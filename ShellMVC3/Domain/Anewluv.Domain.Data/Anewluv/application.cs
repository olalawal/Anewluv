using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class application
    {
        public application()
        {
            this.applicationiconconversions = new List<applicationiconconversion>();
            this.applicationitems = new List<applicationitem>();
            this.applicationroles = new List<applicationrole>();
        }

        public int id { get; set; }
        public System.DateTime creationdate { get; set; }
        public bool active { get; set; }
        public Nullable<System.DateTime> deactivationdate { get; set; }
        public Nullable<int> applicationtype_id { get; set; }
        public virtual ICollection<applicationiconconversion> applicationiconconversions { get; set; }
        public virtual ICollection<applicationitem> applicationitems { get; set; }
        public virtual ICollection<applicationrole> applicationroles { get; set; }
        public virtual lu_applicationtype lu_applicationtype { get; set; }
    }
}
