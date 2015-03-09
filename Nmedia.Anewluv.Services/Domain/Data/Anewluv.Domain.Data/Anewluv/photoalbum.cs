using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photoalbum
    {
        public photoalbum()
        {
            this.photoalbum_securitylevel = new List<photoalbum_securitylevel>();
            this.photophotoalbums = new List<photophotoalbum>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public int profile_id { get; set; }
        public virtual ICollection<photoalbum_securitylevel> photoalbum_securitylevel { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
       // public virtual ICollection<photo> photos { get; set; }
        public virtual ICollection<photophotoalbum> photophotoalbums { get; set; }
    }
}
