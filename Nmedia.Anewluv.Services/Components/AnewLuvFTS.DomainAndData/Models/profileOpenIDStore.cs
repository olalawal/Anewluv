using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class profileOpenIDStore
    {
        public int Id { get; set; }
        public string ProfileID { get; set; }
        public string openidIdentifier { get; set; }
        public string openidProviderName { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<bool> active { get; set; }
        public virtual profile profile { get; set; }
    }
}
