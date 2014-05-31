using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class membersinrole
    {
        public int id { get; set; }
        public Nullable<bool> active { get; set; }
        public int profile_id { get; set; }
        public Nullable<System.DateTime> roleexpiredate { get; set; }
        public Nullable<System.DateTime> rolestartdate { get; set; }
        public int role_id { get; set; }
        public virtual lu_role lu_role { get; set; }
        public virtual profile profile { get; set; }
    }
}
