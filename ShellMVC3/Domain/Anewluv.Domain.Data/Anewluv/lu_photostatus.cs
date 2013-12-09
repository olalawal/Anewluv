using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_photostatus
    {
        public lu_photostatus()
        {
            this.lu_photostatusdescription = new List<lu_photostatusdescription>();
            this.photos = new List<photo>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<lu_photostatusdescription> lu_photostatusdescription { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
