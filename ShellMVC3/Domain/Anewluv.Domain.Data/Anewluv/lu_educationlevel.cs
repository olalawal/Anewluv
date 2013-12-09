using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_educationlevel
    {
        public lu_educationlevel()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_educationlevel = new List<searchsetting_educationlevel>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_educationlevel> searchsetting_educationlevel { get; set; }
    }
}
