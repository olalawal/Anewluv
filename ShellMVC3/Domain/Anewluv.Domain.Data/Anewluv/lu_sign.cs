using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_sign
    {
        public lu_sign()
        {
            this.profiledatas = new List<profiledata>();
            this.searchsetting_sign = new List<searchsetting_sign>();
        }

        public int id { get; set; }
        public string month { get; set; }
        public string description { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual ICollection<searchsetting_sign> searchsetting_sign { get; set; }
    }
}
