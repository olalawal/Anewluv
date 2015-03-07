using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileView
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public string ProfileViewerID { get; set; }
        public Nullable<System.DateTime> ProfileViewDate { get; set; }
        public Nullable<int> MutualViews { get; set; }
        public Nullable<bool> ProfileViewViewed { get; set; }
        public Nullable<System.DateTime> ProfileViewViewedDate { get; set; }
        public Nullable<bool> DeletedByProfileID { get; set; }
        public Nullable<System.DateTime> DeletedByProfileIDDate { get; set; }
        public Nullable<bool> DeletedByProfileViewerID { get; set; }
        public Nullable<System.DateTime> DeletedByProfileViewerIDDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
