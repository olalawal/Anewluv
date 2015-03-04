using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_educationlevel : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> educationlevel_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_educationlevel lu_educationlevel { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
