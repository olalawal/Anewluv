using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_diet
    {
        public int id { get; set; }
        public Nullable<int> diet_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_diet lu_diet { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
