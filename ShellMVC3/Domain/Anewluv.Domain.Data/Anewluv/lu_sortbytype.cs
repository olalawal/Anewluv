using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_sortbytype
    {
        public lu_sortbytype()
        {
            this.searchsetting_sortbytype = new List<searchsetting_sortbytype>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<searchsetting_sortbytype> searchsetting_sortbytype { get; set; }
    }
}
