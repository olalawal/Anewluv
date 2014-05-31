using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_photoimagetype
    {
        public lu_photoimagetype()
        {
            this.photos = new List<photo>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
