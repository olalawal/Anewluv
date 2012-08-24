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
        public string description { get; set; }
        public DateTime? reviewdate { get; set; }
        public string reviewerprofile_id { get; set; }
        public virtual profiledata reviewerprofiledata { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
