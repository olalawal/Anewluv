using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_iconformat
    {
        public lu_iconformat()
        {
            this.applicationiconconversions = new List<applicationiconconversion>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public int iconImagersizerformat_id { get; set; }
        public Nullable<int> iconImageresizerformat_id { get; set; }
        public virtual ICollection<applicationiconconversion> applicationiconconversions { get; set; }
        public virtual lu_iconImagersizerformat lu_iconImagersizerformat { get; set; }
    }
}
