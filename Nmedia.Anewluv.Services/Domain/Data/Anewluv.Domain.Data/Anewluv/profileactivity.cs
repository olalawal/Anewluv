using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    //added guid tracking
   [DataContract]
    public partial class profileactivity : Entity
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public Nullable<System.DateTime> creationdate { get; set; }
        [DataMember]
        public string ipaddress { get; set; }
        [DataMember]
        public int? profile_id { get; set; }
        [DataMember]
        public string sessionid { get; set; }
        [DataMember]
        public Guid? apikey { get; set; }
        [DataMember]
        public string useragent { get; set; }
        [DataMember]
        public string routeurl { get; set; }
        [DataMember]
        public string actionname { get; set; }
        [DataMember]
        public int? profileactivitygeodata_id { get; set; }
        [DataMember]
        public int? activitytype_id { get; set; }
        [DataMember]
        public virtual lu_activitytype lu_activitytype { get; set; }      
        public virtual profileactivitygeodata profileactivitygeodata { get; set; }
        public virtual profile profile { get; set; }
    }
}
