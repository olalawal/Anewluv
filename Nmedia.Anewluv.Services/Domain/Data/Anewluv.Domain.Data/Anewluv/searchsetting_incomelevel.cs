using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_incomelevel : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> incomelevel_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_incomelevel lu_incomelevel { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
