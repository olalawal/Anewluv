using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class photoreviewstatus
    {
        [Key]
        public int id { get; set; }
        //public string photoreviewstatustype_id { get; set; }
        //public lu_photoreviewstatustype photoreviewstatustype  { get; set; }
        public string notes { get; set; }
        public DateTime? reviewdate { get; set; }
        public int reviewerprofile_id { get; set; }
        public virtual profiledata reviewerprofiledata { get; set; }
        public Guid photo_id { get; set; }
        public virtual photo photo { get; set; }
    }
}
