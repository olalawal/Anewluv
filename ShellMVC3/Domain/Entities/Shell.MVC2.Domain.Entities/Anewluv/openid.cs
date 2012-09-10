using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class openid
    {
        [Key]
        public int id { get; set; }
        public bool? active { get; set; }
        public DateTime? creationdate { get; set; }      
        public string openididentifier { get; set; }
        public string openidprovidername { get; set; }
       public int profile_id { get; set; }       
        public virtual profile profile { get; set; }
       
    }
}
