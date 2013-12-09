using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_defaultmailboxfolder
    {
        public lu_defaultmailboxfolder()
        {
            this.mailboxfoldertypes = new List<mailboxfoldertype>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<mailboxfoldertype> mailboxfoldertypes { get; set; }
    }
}
