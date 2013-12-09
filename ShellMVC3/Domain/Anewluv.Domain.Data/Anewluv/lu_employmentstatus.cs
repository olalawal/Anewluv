using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_employmentstatus
    {
        public lu_employmentstatus()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_employmentstatus = new List<searchsetting_employmentstatus>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_employmentstatus> searchsetting_employmentstatus { get; set; }
    }
}
