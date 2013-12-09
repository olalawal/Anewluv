using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_photoformat
    {
        public lu_photoformat()
        {
            this.photoconversions = new List<photoconversion>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public int photoImagersizerformat_id { get; set; }
        public virtual lu_photoImagersizerformat lu_photoImagersizerformat { get; set; }
        public virtual ICollection<photoconversion> photoconversions { get; set; }
    }
}
