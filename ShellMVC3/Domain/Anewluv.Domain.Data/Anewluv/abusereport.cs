using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class abusereport
    {
        public abusereport()
        {
            this.abusereportnotes = new List<abusereportnote>();
        }

        public int id { get; set; }
        public int abusereporter_id { get; set; }
        public int abuser_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public int abusetype_id { get; set; }
        public virtual ICollection<abusereportnote> abusereportnotes { get; set; }
        public virtual lu_abusetype lu_abusetype { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
