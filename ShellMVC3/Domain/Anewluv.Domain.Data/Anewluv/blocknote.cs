using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class blocknote
    {
        public int id { get; set; }
        public int block_id { get; set; }
        public int profile_id { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<System.DateTime> reviewdate { get; set; }
        public Nullable<int> notetype_id { get; set; }
        public virtual block block { get; set; }
        public virtual lu_notetype lu_notetype { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
    }
}
