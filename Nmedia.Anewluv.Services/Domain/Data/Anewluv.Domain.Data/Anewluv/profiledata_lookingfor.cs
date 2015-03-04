using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profiledata_lookingfor : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> lookingfor_id { get; set; }
        public virtual lu_lookingfor lu_lookingfor { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
