using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Friend
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public string FriendID { get; set; }
        public Nullable<int> MutualFriend { get; set; }
        public Nullable<System.DateTime> FriendDate { get; set; }
        public Nullable<bool> FriendViewed { get; set; }
        public Nullable<System.DateTime> FriendViewedDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
