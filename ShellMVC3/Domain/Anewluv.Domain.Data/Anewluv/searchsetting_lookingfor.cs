using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_lookingfor
    {
        public int id { get; set; }
        public Nullable<int> lookingfor_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_lookingfor lu_lookingfor { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
