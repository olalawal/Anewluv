using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_drink
    {
        public int id { get; set; }
        public Nullable<int> drink_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_drinks lu_drinks { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
