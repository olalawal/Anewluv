using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailupdatefreqency
    {
        public int id { get; set; }
        public Nullable<int> updatefreqency { get; set; }
        public int profile_id { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
