using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class MembersInRole
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<System.DateTime> RoleStartDate { get; set; }
        public Nullable<System.DateTime> RoleExpireDate { get; set; }
        public Nullable<bool> Active { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual profile profile { get; set; }
        public virtual Role Role { get; set; }
    }
}
