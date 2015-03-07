using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class PhotoReviewStatu
    {
        public PhotoReviewStatu()
        {
            this.photos = new List<photo>();
        }

        public int PhotoReviewStatusID { get; set; }
        public string PhotoReviewStatusValue { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
