using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_exercise
    {
        public int id { get; set; }
        public Nullable<int> exercise_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_exercise lu_exercise { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
