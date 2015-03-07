using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_HotFeature
    {
        public int SearchSettings_HotFeatureID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> HotFeatureID { get; set; }
        public virtual CriteriaCharacter_HotFeature CriteriaCharacter_HotFeature { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
