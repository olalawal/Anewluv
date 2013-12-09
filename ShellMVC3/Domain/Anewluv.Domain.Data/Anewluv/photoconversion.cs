using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photoconversion
    {
        public int id { get; set; }
        public System.Guid photo_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }
        public long size { get; set; }
        public int formattype_id { get; set; }
        public virtual lu_photoformat lu_photoformat { get; set; }
        public virtual photo photo { get; set; }
    }
}
