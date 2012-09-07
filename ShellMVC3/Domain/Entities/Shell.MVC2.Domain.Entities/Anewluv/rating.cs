using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class rating
    {
        [Key]
        public int id { get; set; }
        public virtual ICollection<ratingvalue> ratingvalues { get; set; } 
        public string description { get; set; }       
        public int? ratingmaxvalue { get; set; }
        public int? ratingweight { get; set; }
        public long? increment { get; set; }
    }
}
