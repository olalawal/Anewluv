using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_humor
    {
        public lu_humor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_humor = new List<searchsetting_humor>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_humor> searchsetting_humor { get; set; }
    }
}
