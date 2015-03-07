using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileEmailUpdateFreqency
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> EmailUpdateFreqency { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
