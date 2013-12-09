using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_profilestatus
    {
        public lu_profilestatus()
        {
            this.profiles = new List<profile>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profile> profiles { get; set; }
    }
}
