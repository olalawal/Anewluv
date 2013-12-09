using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_maritalstatus
    {
        public lu_maritalstatus()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_maritalstatus = new List<searchsetting_maritalstatus>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
    }
}
