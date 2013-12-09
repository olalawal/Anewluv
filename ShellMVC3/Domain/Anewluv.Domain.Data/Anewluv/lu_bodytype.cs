using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_bodytype
    {
        public lu_bodytype()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_bodytype = new List<searchsetting_bodytype>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_bodytype> searchsetting_bodytype { get; set; }
    }
}
