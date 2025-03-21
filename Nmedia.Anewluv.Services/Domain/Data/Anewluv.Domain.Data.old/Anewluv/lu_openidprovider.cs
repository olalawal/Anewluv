using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_openidprovider : Entity
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
