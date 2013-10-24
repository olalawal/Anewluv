using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract(IsReference = true)]
    public class profileactivity
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public string ipaddress { get; set; }
        [DataMember]
        public int profile_id { get; set; }
        [DataMember]
        public virtual profile profile { get; set; }
        [DataMember]
        public string sessionid { get; set; }
        [DataMember]
        public string useragent { get; set; }
        [DataMember]
        public string routeurl { get; set; }
        [DataMember]
        public string actionname { get; set; } //MVC type action name
        //public string timestamp { get; set; }
        [DataMember]
        public virtual lu_activitytype activitytype { get; set; } //TO DO convert to object and lookup
        [DataMember]
        public int profileactivitygeodata_id { get; set; }
        [DataMember]
        public virtual profileactivitygeodata profileactivitygeodata { get; set; }
    }
}
