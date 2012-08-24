using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class lu_photorejectionreason
    {
               
        [Key]
        public int id { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public string description { get; set; }
        public string userMessage { get; set; }
    }
}
