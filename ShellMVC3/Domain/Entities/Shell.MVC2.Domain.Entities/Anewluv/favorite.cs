using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class favorite
    {

        [Key]
        public int id { get; set; }
        public string profile_id { get; set; }
        public string favoriteprofile_id { get; set; }
       public virtual profiledata profiledata { get; set; }
       public virtual profiledata favoriteprofiledata { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? modificationdate { get; set; }    
        public DateTime? viewdate { get; set; }      
        public DateTime? deletedbymemberdate { get; set; }
        public DateTime? deletedbyfavoritedate { get; set; }    
        public int mutual { get; set; }

    }
}
