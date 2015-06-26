using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class photo : Entity
    {
        public photo()
        {
            this.photo_securitylevel = new List<photo_securitylevel>();
            this.photoconversions = new List<photoconversion>();
            this.photoreviews = new List<photoreview>();
            this.photophotoalbums = new List<photophotoalbum>();
        }

        public System.Guid id { get; set; }
        public long size { get; set; }
        public int profile_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public string imagecaption { get; set; }
        public string imagename { get; set; }
        public string providername { get; set; }
        public Nullable<int> rejectionreason_id { get; set; }
        public Nullable<int> photostatus_id { get; set; }
        public Nullable<int> approvalstatus_id { get; set; }
        public Nullable<int> imagetype_id { get; set; }
        public virtual lu_photoapprovalstatus lu_photoapprovalstatus { get; set; }
        public virtual lu_photoimagetype lu_photoimagetype { get; set; }
        public virtual lu_photorejectionreason lu_photorejectionreason { get; set; }
        public virtual lu_photostatus lu_photostatus { get; set; }
        public virtual ICollection<photo_securitylevel> photo_securitylevel { get; set; }
        public virtual ICollection<photoconversion> photoconversions { get; set; }
        public virtual ICollection<photoreview> photoreviews { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
      //  public virtual ICollection<photoalbum> photoalbums { get; set; }
        public virtual ICollection<photophotoalbum> photophotoalbums { get; set; }

    }
}
