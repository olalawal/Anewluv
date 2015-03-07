using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Hotlist
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public string HotlistID { get; set; }
        public Nullable<int> MutualHotlist { get; set; }
        public Nullable<System.DateTime> HotlistDate { get; set; }
        public Nullable<bool> HotlistViewed { get; set; }
        public Nullable<System.DateTime> HotlistViewedDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
