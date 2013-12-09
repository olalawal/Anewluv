using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_abusetype
    {
        public lu_abusetype()
        {
            this.abusereports = new List<abusereport>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<abusereport> abusereports { get; set; }
    }
}
