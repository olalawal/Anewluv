using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_sortbytype
    {
        public int id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public Nullable<int> sortbytype_id { get; set; }
        public virtual lu_sortbytype lu_sortbytype { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
