using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class applicationiconconversion
    {
        public int id { get; set; }
        public int application_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public byte[] image { get; set; }
        public long size { get; set; }
        public Nullable<int> application_id1 { get; set; }
        public Nullable<int> iconformat_id { get; set; }
        public virtual application application { get; set; }
        public virtual lu_iconformat lu_iconformat { get; set; }
    }
}
