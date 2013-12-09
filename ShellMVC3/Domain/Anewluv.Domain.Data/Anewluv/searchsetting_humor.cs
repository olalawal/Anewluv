using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_humor
    {
        public int id { get; set; }
        public Nullable<int> humor_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_humor lu_humor { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
