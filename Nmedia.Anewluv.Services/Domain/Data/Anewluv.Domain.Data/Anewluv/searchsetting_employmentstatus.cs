using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_employmentstatus : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> employmentstatus_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_employmentstatus lu_employmentstatus { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
