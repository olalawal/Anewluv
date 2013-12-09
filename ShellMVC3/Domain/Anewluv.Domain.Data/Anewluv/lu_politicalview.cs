using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_politicalview
    {
        public lu_politicalview()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_politicalview = new List<searchsetting_politicalview>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_politicalview> searchsetting_politicalview { get; set; }
    }
}
