using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_HotFeature
    {
        public CriteriaCharacter_HotFeature()
        {
            this.ProfileData_HotFeature = new List<ProfileData_HotFeature>();
            this.SearchSettings_HotFeature = new List<SearchSettings_HotFeature>();
        }

        public int HotFeatureID { get; set; }
        public string HotFeatureName { get; set; }
        public virtual ICollection<ProfileData_HotFeature> ProfileData_HotFeature { get; set; }
        public virtual ICollection<SearchSettings_HotFeature> SearchSettings_HotFeature { get; set; }
    }
}
