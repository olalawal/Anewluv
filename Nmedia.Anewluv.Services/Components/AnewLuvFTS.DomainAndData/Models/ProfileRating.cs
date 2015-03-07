using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileRating
    {
        public ProfileRating()
        {
            this.ProfileRatingTrackers = new List<ProfileRatingTracker>();
            this.ProfileRatingTrackers1 = new List<ProfileRatingTracker>();
            this.ProfileRatingTrackers2 = new List<ProfileRatingTracker>();
        }

        public int ProfileRatingID { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> RatingID { get; set; }
        public double AverageRating { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual Rating Rating { get; set; }
        public virtual Rating Rating1 { get; set; }
        public virtual ICollection<ProfileRatingTracker> ProfileRatingTrackers { get; set; }
        public virtual ICollection<ProfileRatingTracker> ProfileRatingTrackers1 { get; set; }
        public virtual ICollection<ProfileRatingTracker> ProfileRatingTrackers2 { get; set; }
    }
}
