using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailboxfolder :Entity
    {
        public mailboxfolder()
        {
            this.mailboxmessagefolders = new List<mailboxmessagefolder>();
        }

        public int id { get; set; }
        public int profiled_id { get; set; }
        public Nullable<int> active { get; set; }
        public Nullable<int> foldertype_id { get; set; }
        public virtual mailboxfoldertype mailboxfoldertype { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual ICollection<mailboxmessagefolder> mailboxmessagefolders { get; set; }
    }
}
