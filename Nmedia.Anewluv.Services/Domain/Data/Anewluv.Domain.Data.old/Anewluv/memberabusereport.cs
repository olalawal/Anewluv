using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class memberabusereport
    {
        [Key]
        public int id { get; set; }
        public virtual abusereport abusereport { get; set; }
        public virtual profiledata abuser { get; set; }
        public virtual profiledata abusereporter { get; set; }
       


    }
}
