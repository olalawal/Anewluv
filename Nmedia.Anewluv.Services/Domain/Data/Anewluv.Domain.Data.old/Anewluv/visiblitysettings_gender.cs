using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class visiblitysettings_gender
    {
        public int id { get; set; }
        public int visiblitysetting_id { get; set; }
        public Nullable<int> gender_id { get; set; }
        public virtual lu_gender lu_gender { get; set; }
        public virtual visiblitysetting visiblitysetting { get; set; }
    }
}
