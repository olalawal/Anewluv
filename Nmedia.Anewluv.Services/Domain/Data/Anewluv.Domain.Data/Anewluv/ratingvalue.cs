using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class ratingvalue
    {
        public int id { get; set; }
        public int rating_id { get; set; }
        public int profile_id { get; set; }
        public int rateeprofile_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<double> value { get; set; }
        public virtual profilemetadata raterprofilemetadata { get; set; }
        public virtual profilemetadata rateeprofilemetadata { get; set; }
      
        public virtual rating rating { get; set; }
    }
}
