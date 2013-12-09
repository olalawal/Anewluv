using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_securityleveltype
    {
        public lu_securityleveltype()
        {
            this.photo_securitylevel = new List<photo_securitylevel>();
            this.photoalbum_securitylevel = new List<photoalbum_securitylevel>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<photo_securitylevel> photo_securitylevel { get; set; }
        public virtual ICollection<photoalbum_securitylevel> photoalbum_securitylevel { get; set; }
    }
}
