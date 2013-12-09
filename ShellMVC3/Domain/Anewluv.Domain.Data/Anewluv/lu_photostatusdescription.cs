using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_photostatusdescription
    {
        public int id { get; set; }
        public string description { get; set; }
        public Nullable<int> photostatus_id { get; set; }
        public virtual lu_photostatus lu_photostatus { get; set; }
    }
}
