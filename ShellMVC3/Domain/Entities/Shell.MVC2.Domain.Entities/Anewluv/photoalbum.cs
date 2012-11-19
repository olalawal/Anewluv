using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class photoalbum
    {
      
        [Key]
        public int id { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public string description { get; set; }
        public virtual ICollection<photoalbum_securitylevel> albumsecuritylevels { get; set; }
        public int profile_id { get; set; }
        public profilemetadata profilemetadata { get; set; }
        

    }
}
