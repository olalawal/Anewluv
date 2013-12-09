using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_eyecolor
    {
        public lu_eyecolor()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_eyecolor = new List<searchsetting_eyecolor>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_eyecolor> searchsetting_eyecolor { get; set; }
    }
}
