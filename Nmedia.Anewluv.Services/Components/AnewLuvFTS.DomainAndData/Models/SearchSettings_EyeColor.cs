using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_EyeColor
    {
        public int SearchSettings_EyeColorID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> EyeColorID { get; set; }
        public virtual CriteriaAppearance_EyeColor CriteriaAppearance_EyeColor { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
