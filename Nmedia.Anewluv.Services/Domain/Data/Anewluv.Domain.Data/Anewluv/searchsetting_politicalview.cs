
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_politicalview : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> politicalview_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_politicalview lu_politicalview { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
