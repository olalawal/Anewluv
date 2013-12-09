using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_smokes
    {
        public lu_smokes()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_smokes = new List<searchsetting_smokes>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_smokes> searchsetting_smokes { get; set; }
    }
}
