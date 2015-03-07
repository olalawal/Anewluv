using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class MailboxMessagesFolder
    {
        public int MailboxMessagesFoldersID { get; set; }
        public Nullable<int> MailboxFolderID { get; set; }
        public Nullable<int> MailBoxMessageID { get; set; }
        public Nullable<int> MessageRead { get; set; }
        public Nullable<int> MessageReplied { get; set; }
        public Nullable<int> MessageFlagged { get; set; }
        public Nullable<int> MessageDraft { get; set; }
        public Nullable<int> MessageDeleted { get; set; }
        public Nullable<int> MessageRecent { get; set; }
        public virtual MailboxFolder MailboxFolder { get; set; }
        public virtual MailboxMessage MailboxMessage { get; set; }
    }
}
