using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_defaultmailboxfolder : Repository.Pattern.Ef6.Entity
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
