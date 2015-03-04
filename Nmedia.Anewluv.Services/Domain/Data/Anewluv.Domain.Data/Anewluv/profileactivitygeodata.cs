using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class profileactivitygeodata :Entity
    {
        public profileactivitygeodata()
        {
            this.activity = new profileactivity();
        }

        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string regionname { get; set; }
        [DataMember]
        public string continent { get; set; }
        [DataMember]
        public Nullable<int> countryId { get; set; }
        [DataMember]
        public string countrycode { get; set; }
        [DataMember]
        public string countryname { get; set; }
        [DataMember]
        public Nullable<System.DateTime> creationdate { get; set; }
        [DataMember]
        public Nullable<double> lattitude { get; set; }
        [DataMember]
        public Nullable<double> longitude { get; set; }

        [DataMember]
        public int activity_id { get; set; }
        
        public virtual profileactivity activity { get; set; }
    }
}
