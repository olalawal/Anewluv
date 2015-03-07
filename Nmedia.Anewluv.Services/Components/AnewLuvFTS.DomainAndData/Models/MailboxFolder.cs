using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class MailboxFolder
    {
        public MailboxFolder()
        {
            this.MailboxMessagesFolders = new List<MailboxMessagesFolder>();
        }

        public int MailboxFolderID { get; set; }
        public Nullable<int> MailboxFolderTypeID { get; set; }
        public string MailboxFolderTypeName { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> Active { get; set; }
        public virtual MailboxFolderType MailboxFolderType { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual ICollection<MailboxMessagesFolder> MailboxMessagesFolders { get; set; }
    }
}
