using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_profession
    {
        public lu_profession()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_profession = new List<searchsetting_profession>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_profession> searchsetting_profession { get; set; }
    }
}
