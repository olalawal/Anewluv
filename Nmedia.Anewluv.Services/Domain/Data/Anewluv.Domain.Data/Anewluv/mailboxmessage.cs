using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailboxmessage : Entity
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
        public Nullable<int> sizeinbtyes { get; set; }
        public virtual ICollection<mailboxmessagefolder> mailboxmessagefolders { get; set; }
        public virtual profilemetadata recipientprofilemetadata { get; set; }
        public virtual profilemetadata senderprofilemetadata { get; set; }
    }
}
