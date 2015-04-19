using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photo_securitylevel :Entity
    {
        public int id { get; set; }
        public System.Guid photo_id { get; set; }
        public Nullable<int> securityleveltype_id { get; set; }
        public virtual lu_securityleveltype lu_securityleveltype { get; set; }
        public virtual photo photo { get; set; }
    }
}
