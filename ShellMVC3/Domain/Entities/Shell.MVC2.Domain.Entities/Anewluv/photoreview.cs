using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class photoreview
    {
        [Key]
        public int id { get; set; }
        //public string photoreviewtype_id { get; set; }
        //public lu_photoreviewtype photoreviewtype  { get; set; }
        public string notes { get; set; }
        public DateTime? creationdate { get; set; }
        public int reviewerprofile_id { get; set; }
        public virtual profiledata reviewerprofiledata { get; set; }
        public Guid photo_id { get; set; }
        public virtual photo photo { get; set; }
    }
}
