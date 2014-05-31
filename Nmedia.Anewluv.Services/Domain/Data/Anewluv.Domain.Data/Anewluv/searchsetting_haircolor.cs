using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_haircolor
    {
        public int id { get; set; }
        public Nullable<int> haircolor_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_haircolor lu_haircolor { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
