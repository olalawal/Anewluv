using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class abuser
    {
        public string ProfileID { get; set; }
        public Nullable<System.DateTime> SuspensionDate { get; set; }
        public byte[] DeletionDate { get; set; }
        public Nullable<byte> FlagTypeID { get; set; }
        public string AbuserUserName { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
