using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profiledata_hobby
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> hobby_id { get; set; }
        public virtual lu_hobby lu_hobby { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
