using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class blocknotes
    {
        [Key]
        public int id { get; set; }
        public int block_id { get; set; }
        public int profile_id { get; set; }
        public virtual block block { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual lu_notetype notetype { get; set; }
        public string note { get; set; }
        public DateTime creationdate { get; set; }
        public DateTime? reviewdate { get; set; }



    }
}
