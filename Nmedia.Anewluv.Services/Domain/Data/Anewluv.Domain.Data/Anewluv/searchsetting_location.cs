using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting_location
    {
        public int id { get; set; }
        public string city { get; set; }
        public Nullable<int> countryid { get; set; }
        public string postalcode { get; set; }
        public Nullable<int> searchsetting_id { get; set; }
        public virtual searchsetting searchsetting { get; set; }
    }
}
