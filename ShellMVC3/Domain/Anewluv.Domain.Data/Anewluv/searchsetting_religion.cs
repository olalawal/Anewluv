using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_religion
    {
        public int id { get; set; }
        public Nullable<int> religion_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_religion lu_religion { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
