using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class abusereport
    {
        public int RecordID { get; set; }
        public byte[] RecordDate { get; set; }
        public string AbuserID { get; set; }
        public string ProfileID { get; set; }
        public string ReporterComments { get; set; }
        public byte AbuseTypeID { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual abusetype abusetype { get; set; }
    }
}
