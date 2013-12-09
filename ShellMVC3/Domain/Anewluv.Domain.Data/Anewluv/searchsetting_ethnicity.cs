using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_ethnicity
    {
        public int id { get; set; }
        public Nullable<int> ethnicity_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_ethnicity lu_ethnicity { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
