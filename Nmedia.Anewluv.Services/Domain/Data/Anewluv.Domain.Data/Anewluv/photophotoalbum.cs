using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photophotoalbum
    {
        public photophotoalbum()
        {
            this.photo = new photo();
            this.photoalbum = new  photoalbum();
        }

        public int photophotoalbumid {get;set;}
        public System.Guid photo_id { get; set; }
        public int photoalbum_id { get; set; } 
        public virtual photo photo { get; set; }
        public virtual photoalbum photoalbum { get; set; }

    }
}
