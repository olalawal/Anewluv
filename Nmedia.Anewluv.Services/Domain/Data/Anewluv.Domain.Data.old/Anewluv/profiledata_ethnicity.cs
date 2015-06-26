using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profiledata_ethnicity : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> ethnicty_id { get; set; }
        public virtual lu_ethnicity lu_ethnicity { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
