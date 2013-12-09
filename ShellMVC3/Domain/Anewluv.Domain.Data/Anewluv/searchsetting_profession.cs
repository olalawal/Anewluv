using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_profession
    {
        public int id { get; set; }
        public Nullable<int> profession_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_profession lu_profession { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
