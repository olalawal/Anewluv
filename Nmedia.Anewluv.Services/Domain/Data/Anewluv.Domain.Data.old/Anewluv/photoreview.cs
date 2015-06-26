using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photoreview
    {
        public int id { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public int reviewerprofile_id { get; set; }
        public System.Guid photo_id { get; set; }
        public virtual photo photo { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
     
    }
}
