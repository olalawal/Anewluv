using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_livingsituation
    {
        public lu_livingsituation()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_livingstituation = new List<searchsetting_livingstituation>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
    }
}
