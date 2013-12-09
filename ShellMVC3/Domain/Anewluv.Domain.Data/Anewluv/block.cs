using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class block
    {
        public block()
        {
            this.blocknotes = new List<blocknote>();
        }

        public int id { get; set; }
        public int profile_id { get; set; }
        public int blockprofile_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<System.DateTime> modificationdate { get; set; }
        public Nullable<System.DateTime> removedate { get; set; }
        public Nullable<int> mutual { get; set; }
        public virtual ICollection<blocknote> blocknotes { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
