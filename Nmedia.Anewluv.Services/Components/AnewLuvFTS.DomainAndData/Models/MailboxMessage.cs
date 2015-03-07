using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class MailboxMessage
    {
        public MailboxMessage()
        {
            this.MailboxMessagesFolders = new List<MailboxMessagesFolder>();
        }

        public int MailboxMessageID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> uniqueID { get; set; }
        public virtual ICollection<MailboxMessagesFolder> MailboxMessagesFolders { get; set; }
    }
}
