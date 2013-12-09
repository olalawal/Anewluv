using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_religion
    {
        public lu_religion()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_religion = new List<searchsetting_religion>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_religion> searchsetting_religion { get; set; }
    }
}
