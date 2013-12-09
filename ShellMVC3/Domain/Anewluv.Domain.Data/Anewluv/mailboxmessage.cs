using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailboxmessage
    {
        public mailboxmessage()
        {
            this.mailboxmessagefolders = new List<mailboxmessagefolder>();
        }

        public int id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public int recipient_id { get; set; }
        public int sender_id { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public Nullable<int> uniqueid { get; set; }
        public virtual ICollection<mailboxmessagefolder> mailboxmessagefolders { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
