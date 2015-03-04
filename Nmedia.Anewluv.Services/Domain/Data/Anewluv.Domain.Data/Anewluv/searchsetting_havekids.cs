using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_havekids : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> havekids_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_havekids lu_havekids { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
