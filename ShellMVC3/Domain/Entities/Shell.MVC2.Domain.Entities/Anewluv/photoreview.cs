using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class photoreview
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        //public string photoreviewtype_id { get; set; }
        //public lu_photoreviewtype photoreviewtype  { get; set; }
        [DataMember]
        public string notes { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public int reviewerprofile_id { get; set; }
        [DataMember]
        public virtual profiledata reviewerprofiledata { get; set; }
        [DataMember]
        public Guid photo_id { get; set; }
        [DataMember]
        public virtual photo photo { get; set; }
    }
}
