using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_hobby
    {
        public int id { get; set; }
        public Nullable<int> hobby_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_hobby lu_hobby { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
