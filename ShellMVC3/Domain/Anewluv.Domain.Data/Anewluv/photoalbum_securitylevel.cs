using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photoalbum_securitylevel
    {
        public int id { get; set; }
        public int photoalbum_id { get; set; }
        public Nullable<int> securityleveltype_id { get; set; }
        public virtual lu_securityleveltype lu_securityleveltype { get; set; }
        public virtual photoalbum photoalbum { get; set; }
    }
}
