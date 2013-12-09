using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_havekids
    {
        public lu_havekids()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_havekids = new List<searchsetting_havekids>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_havekids> searchsetting_havekids { get; set; }
    }
}
