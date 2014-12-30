using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    //added guid tracking
    public partial class profileactivity
    {
        public int id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public string ipaddress { get; set; }
        public int profile_id { get; set; }
        public string sessionid { get; set; }
        public Guid apikey { get; set; }
        public string useragent { get; set; }
        public string routeurl { get; set; }
        public string actionname { get; set; }
        public int profileactivitygeodata_id { get; set; }
        public Nullable<int> activitytype_id { get; set; }
        public virtual lu_activitytype lu_activitytype { get; set; }
        public virtual profileactivitygeodata profileactivitygeodata { get; set; }
        public virtual profile profile { get; set; }
    }
}
