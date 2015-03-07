using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Rating
    {
        public Rating()
        {
            this.ProfileRatings = new List<ProfileRating>();
            this.ProfileRatings1 = new List<ProfileRating>();
        }

        public int RatingID { get; set; }
        public string RatingDescription { get; set; }
        public Nullable<int> RatingMaxValue { get; set; }
        public Nullable<int> RatingWeight { get; set; }
        public virtual ICollection<ProfileRating> ProfileRatings { get; set; }
        public virtual ICollection<ProfileRating> ProfileRatings1 { get; set; }
    }
}
