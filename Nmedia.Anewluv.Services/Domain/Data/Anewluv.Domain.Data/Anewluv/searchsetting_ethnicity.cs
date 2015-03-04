using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_ethnicity : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> ethnicity_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_ethnicity lu_ethnicity { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
