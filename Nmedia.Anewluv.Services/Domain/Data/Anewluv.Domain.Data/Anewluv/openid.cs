using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class openid
    {
        public int id { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public string openididentifier { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> openidprovider_id { get; set; }
        public virtual lu_openidprovider lu_openidprovider { get; set; }
        public virtual profile profile { get; set; }
    }
}
