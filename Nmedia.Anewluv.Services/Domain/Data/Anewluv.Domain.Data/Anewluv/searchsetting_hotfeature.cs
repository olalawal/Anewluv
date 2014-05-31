using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_hotfeature
    {
        public int id { get; set; }
        public Nullable<int> hotfeature_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_hotfeature lu_hotfeature { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
