using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_incomelevel
    {
        public lu_incomelevel()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_incomelevel = new List<searchsetting_incomelevel>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_incomelevel> searchsetting_incomelevel { get; set; }
    }
}
