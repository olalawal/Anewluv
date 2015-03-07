using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileData_HotFeature
    {
        public int ProfileData_HotFeature1 { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> HotFeatureID { get; set; }
        public virtual CriteriaCharacter_HotFeature CriteriaCharacter_HotFeature { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
