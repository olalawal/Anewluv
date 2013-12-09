using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_showme
    {
        public lu_showme()
        {
            this.searchsetting_showme = new List<searchsetting_showme>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<searchsetting_showme> searchsetting_showme { get; set; }
    }
}
