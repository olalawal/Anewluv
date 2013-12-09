using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_photoImagersizerformat
    {
        public lu_photoImagersizerformat()
        {
            this.lu_photoformat = new List<lu_photoformat>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<lu_photoformat> lu_photoformat { get; set; }
    }
}
