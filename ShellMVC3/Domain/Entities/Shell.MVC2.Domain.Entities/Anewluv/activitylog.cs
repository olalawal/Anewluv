using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class activitylog
    {
        [Key]
        public int id { get; set; }       
        public DateTime? creationdate { get; set; }
        public string ipaddress { get; set; }     
       public int profile_id { get; set; }     
        public virtual profile profile { get; set; }       
        public string regionname { get; set; }
        public string sessionid { get; set; }
        public string useragent { get; set; }
        public string routeurl { get; set; }
        public string actionname { get; set; } //MVC type action name
        public string timestamp { get; set; }
        public virtual  ICollection<activityloggeodata> loggedgeodata { get; set; }
    }
}
