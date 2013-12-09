using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_bodytype
    {
        public int id { get; set; }
        public Nullable<int> bodytype_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_bodytype lu_bodytype { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
