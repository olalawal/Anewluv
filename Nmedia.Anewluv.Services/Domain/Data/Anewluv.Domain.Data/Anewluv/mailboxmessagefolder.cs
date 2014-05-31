using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailboxmessagefolder
    {
        public int mailboxfolder_id { get; set; }
        public int mailboxmessage_id { get; set; }
        public Nullable<System.DateTime> deleteddate { get; set; }
        public Nullable<System.DateTime> draftdate { get; set; }
        public Nullable<System.DateTime> flaggeddate { get; set; }
        public Nullable<System.DateTime> readdate { get; set; }
        public Nullable<bool> recent { get; set; }
        public Nullable<System.DateTime> replieddate { get; set; }
        public virtual mailboxfolder mailboxfolder { get; set; }
        public virtual mailboxmessage mailboxmessage { get; set; }
    }
}
