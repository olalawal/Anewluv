using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class userlogtime
    {
        public int id { get; set; }
        public Nullable<System.DateTime> logintime { get; set; }
        public Nullable<System.DateTime> logouttime { get; set; }
        public Nullable<bool> offline { get; set; }
        public int profile_id { get; set; }
        public string sessionid { get; set; }
        public virtual profile profile { get; set; }
    }
}
