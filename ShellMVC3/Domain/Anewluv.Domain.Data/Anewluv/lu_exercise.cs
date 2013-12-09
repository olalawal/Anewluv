using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_exercise
    {
        public lu_exercise()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_exercise = new List<searchsetting_exercise>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_exercise> searchsetting_exercise { get; set; }
    }
}
