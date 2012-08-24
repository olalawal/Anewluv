using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class abusereportnotes
    {
        [Key]
        public int id { get; set; }
        public int abusereport_id { get; set; }
        public string profile_id { get; set; }

        public virtual abusereport abusereport { get; set; }
        public virtual profiledata profiledata { get; set; }
        public string note { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? reviewdate { get; set; }



    }
}
