using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class User_Logtime
    {
        public int Id { get; set; }
        public string ProfileID { get; set; }
        public string SessionID { get; set; }
        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }
        public byte Offline { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
