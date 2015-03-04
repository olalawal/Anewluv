using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_gender : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> gender_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_gender lu_gender { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
