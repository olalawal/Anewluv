using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profileactivitygeodata
    {
        public profileactivitygeodata()
        {
            this.activity = new profileactivity();
        }

        public int id { get; set; }
        public string city { get; set; }
        public string regionname { get; set; }
        public string continent { get; set; }
        public Nullable<int> countryId { get; set; }
        public string countrycode { get; set; }
        public string countryname { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<double> lattitude { get; set; }
        public Nullable<double> longitude { get; set; }

        public int activity_id { get; set; }
        public virtual profileactivity activity { get; set; }
    }
}
