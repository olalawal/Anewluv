using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_incomelevel
    {
        public int id { get; set; }
        public Nullable<int> incomelevel_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_incomelevel lu_incomelevel { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
