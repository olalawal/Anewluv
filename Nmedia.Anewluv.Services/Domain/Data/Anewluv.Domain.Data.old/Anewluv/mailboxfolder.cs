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
        public int profile_id { get; set; }
        public Nullable<int> active { get; set; }
        public string displayname { get; set; }      
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<System.DateTime> deleteddate { get; set; }
        public Nullable<int> maxsizeinbytes { get; set; }
        public Nullable<int> defaultfolder_id { get; set; }
        public virtual lu_defaultmailboxfolder lu_defaultmailboxfolder { get; set; }       
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual ICollection<mailboxmessagefolder> mailboxmessagefolders { get; set; }

      
       
        
    }
}
