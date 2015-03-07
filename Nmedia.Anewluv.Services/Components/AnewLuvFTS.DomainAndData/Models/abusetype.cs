using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class abusetype
    {
        public abusetype()
        {
            this.abusereports = new List<abusereport>();
        }

        public byte AbuseTypeID { get; set; }
        public string AbuseTypeName { get; set; }
        public virtual ICollection<abusereport> abusereports { get; set; }
    }
}
