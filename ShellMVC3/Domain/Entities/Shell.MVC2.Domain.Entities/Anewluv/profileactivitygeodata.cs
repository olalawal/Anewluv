using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profileactivitygeodata
    {
        [Key]
        public int id { get; set; }        
        public string city { get; set; }
        public string regionname { get; set; }
        public string continent { get; set; }
        public int? countryId { get; set; }
        public string countrycode { get; set; }
        public string countryname { get; set; }
        public DateTime? creationdate { get; set; }       
        public Nullable<double> lattitude { get; set; }
        public Nullable<double> longitude { get; set; }
      
      
    }
}
