using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_showme : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public Nullable<int> showme_id { get; set; }
        public virtual lu_showme lu_showme { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
