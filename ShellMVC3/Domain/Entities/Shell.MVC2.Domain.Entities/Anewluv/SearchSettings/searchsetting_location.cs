using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class searchsetting_location
    {

        [Key]
        public int? id { get; set; }
        public string city { get; set; }
        public int? countryid { get; set; }
        public string postalcode { get; set; }
        public virtual searchsetting  searchsetting { get; set; } 


       
    }
}
