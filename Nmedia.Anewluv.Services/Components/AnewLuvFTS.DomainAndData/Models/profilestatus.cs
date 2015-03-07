using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class profilestatus
    {
        public profilestatus()
        {
            this.profiles = new List<profile>();
        }

        public int ProfileStatusID { get; set; }
        public string ProfileStatusName { get; set; }
        public virtual ICollection<profile> profiles { get; set; }
    }
}
