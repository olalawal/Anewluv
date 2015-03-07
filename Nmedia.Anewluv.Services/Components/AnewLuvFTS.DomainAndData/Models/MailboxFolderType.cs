using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class MailboxFolderType
    {
        public MailboxFolderType()
        {
            this.MailboxFolders = new List<MailboxFolder>();
        }

        public int MailboxFolderTypeID { get; set; }
        public string MailboxFolderTypeName { get; set; }
        public string MailboxFolderTypeDescription { get; set; }
        public string FolderType { get; set; }
        public virtual ICollection<MailboxFolder> MailboxFolders { get; set; }
    }
}
