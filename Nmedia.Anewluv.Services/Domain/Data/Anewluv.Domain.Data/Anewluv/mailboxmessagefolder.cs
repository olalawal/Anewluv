using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class mailboxmessagefolder :Entity
    {
        public mailboxmessagefolder()
        {
           

        }


        public int mailboxfolder_id { get; set; }
        public int mailboxmessage_id { get; set; }
        public bool? deleted { get; set; }
        public Nullable<System.DateTime> deleteddate { get; set; }
        public bool? moved { get; set; }
        public Nullable<System.DateTime> movedate { get; set; }
        public bool? draft { get; set; }
        public Nullable<System.DateTime> draftdate { get; set; }
        public bool flagged { get; set; }
        public Nullable<System.DateTime> flaggeddate { get; set; }
        public bool? read { get; set; }
        public Nullable<System.DateTime> readdate { get; set; }
        public Nullable<bool> recent { get; set; }
        public bool? replied { get; set; }
        public Nullable<System.DateTime> replieddate { get; set; }
        public virtual mailboxfolder mailboxfolder { get; set; }
        public virtual mailboxmessage mailboxmessage { get; set; }
    }
}
