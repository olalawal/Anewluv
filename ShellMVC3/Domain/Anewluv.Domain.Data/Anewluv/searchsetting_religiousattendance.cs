using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_religiousattendance
    {
        public int id { get; set; }
        public Nullable<int> religiousattendance_id { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual lu_religiousattendance lu_religiousattendance { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
