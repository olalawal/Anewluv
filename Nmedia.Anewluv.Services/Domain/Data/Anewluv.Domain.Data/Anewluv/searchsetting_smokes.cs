using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_smokes : Repository.Pattern.Ef6.Entity
    {
        public int id { get; set; }
        public Nullable<int> smoke_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_smokes lu_smokes { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
