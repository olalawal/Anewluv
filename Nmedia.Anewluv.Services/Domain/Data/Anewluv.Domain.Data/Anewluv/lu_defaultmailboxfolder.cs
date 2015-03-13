using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_defaultmailboxfolder : Repository.Pattern.Ef6.Entity
    {
        public lu_defaultmailboxfolder()
        {
            this.mailboxfolders = new List<mailboxfolder>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }
    }
}
