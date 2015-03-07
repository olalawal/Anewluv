using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class favorite
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public string FavoriteID { get; set; }
        public Nullable<int> MutualFavorite { get; set; }
        public Nullable<System.DateTime> FavoriteDate { get; set; }
        public Nullable<bool> FavoriteViewed { get; set; }
        public Nullable<System.DateTime> FavoriteViewedDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
