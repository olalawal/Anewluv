using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class applicationrole
    {
        public int id { get; set; }
        public Nullable<bool> active { get; set; }
        public int application_id { get; set; }
        public Nullable<System.DateTime> roleexpiredate { get; set; }
        public Nullable<System.DateTime> rolestartdate { get; set; }
        public Nullable<System.DateTime> deactivationdate { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<int> application_id1 { get; set; }
        public Nullable<int> role_id { get; set; }
        public virtual application application { get; set; }
        public virtual lu_role lu_role { get; set; }
    }
}
