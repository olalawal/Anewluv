using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_maritalstatus : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> maritalstatus_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_maritalstatus lu_maritalstatus { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
