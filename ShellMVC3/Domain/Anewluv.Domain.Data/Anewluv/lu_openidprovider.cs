using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_openidprovider
    {
        public lu_openidprovider()
        {
            this.openids = new List<openid>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<openid> openids { get; set; }
    }
}
