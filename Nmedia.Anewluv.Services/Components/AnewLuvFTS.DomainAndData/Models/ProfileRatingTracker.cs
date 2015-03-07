using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileRatingTracker
    {
        public int ProfileRatingTrackerID { get; set; }
        public Nullable<int> ProfileRatingID { get; set; }
        public string ProfileID { get; set; }
        public Nullable<double> RatingValue { get; set; }
        public Nullable<System.DateTime> ProfileRatingTrackerDate { get; set; }
        public virtual ProfileRating ProfileRating { get; set; }
        public virtual ProfileRating ProfileRating1 { get; set; }
        public virtual ProfileRating ProfileRating2 { get; set; }
    }
}
