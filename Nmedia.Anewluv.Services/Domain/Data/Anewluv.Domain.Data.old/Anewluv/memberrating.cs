using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class memberrating
    {
        [Key]
        public int id { get; set; }
        public double averagerating { get; set; }
        public virtual profiledata memberprofileata { get; set; }     
        public virtual ICollection<ratingtracker> ratingtracked { get; set; }  
        public rating rating { get; set; }    


    }
}
