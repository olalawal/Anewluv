using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_haircolor
    {
        public lu_haircolor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_haircolor = new List<searchsetting_haircolor>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_haircolor> searchsetting_haircolor { get; set; }
    }
}
