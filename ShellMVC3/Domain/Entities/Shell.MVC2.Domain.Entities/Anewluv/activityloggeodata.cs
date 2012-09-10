using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class activityloggeodata
    {
        [Key]
        public int id { get; set; }
        public string city { get; set; }
        public string continent { get; set; }
        public string country { get; set; }
        public string countryname { get; set; }
        public DateTime? creationdate { get; set; }
        public string ipaddress { get; set; }
        public Nullable<double> lattitude { get; set; }
        public Nullable<double> longitude { get; set; }        
        public string regionname { get; set; }
        public int activitylog_id { get; set; }
        public virtual activitylog loggedactivity { get; set; } 
      
    }
}
