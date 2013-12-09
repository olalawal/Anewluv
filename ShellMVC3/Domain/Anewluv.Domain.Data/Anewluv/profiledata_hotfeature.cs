using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profiledata_hotfeature
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> hotfeature_id { get; set; }
        public virtual lu_hotfeature lu_hotfeature { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
