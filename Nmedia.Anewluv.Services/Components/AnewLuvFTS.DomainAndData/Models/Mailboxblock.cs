using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Mailboxblock
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public Nullable<System.DateTime> MailboxBlockDate { get; set; }
        public string BlockID { get; set; }
        public Nullable<bool> BlockRemoved { get; set; }
        public Nullable<System.DateTime> BlockRemovedDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
