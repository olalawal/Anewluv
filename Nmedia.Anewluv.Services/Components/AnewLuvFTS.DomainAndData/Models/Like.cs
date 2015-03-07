using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Like
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public string LikeID { get; set; }
        public Nullable<System.DateTime> LikeDate { get; set; }
        public Nullable<int> MutuaLikes { get; set; }
        public Nullable<bool> LikeViewed { get; set; }
        public Nullable<System.DateTime> LikeViewedDate { get; set; }
        public Nullable<bool> DeletedByProfileID { get; set; }
        public Nullable<System.DateTime> DeletedByProfileIDDate { get; set; }
        public Nullable<bool> DeletedByLikeID { get; set; }
        public Nullable<System.DateTime> DeletedByLikeIDDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
