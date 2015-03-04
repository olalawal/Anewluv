using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_wantkids : Entity
    {
        public int id { get; set; }
        public Nullable<int> wantskids_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_wantskids lu_wantskids { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
