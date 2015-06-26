using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_iconImagersizerformat : Repository.Pattern.Ef6.Entity
    {
        public lu_iconImagersizerformat()
        {
            this.lu_iconformat = new List<lu_iconformat>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<lu_iconformat> lu_iconformat { get; set; }
    }
}
