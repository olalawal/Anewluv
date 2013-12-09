using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_livingstituation
    {
        public int id { get; set; }
        public Nullable<int> livingsituation_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_livingsituation lu_livingsituation { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
