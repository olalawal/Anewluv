using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_wantskids
    {
        public lu_wantskids()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_wantkids = new List<searchsetting_wantkids>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_wantkids> searchsetting_wantkids { get; set; }
    }
}
