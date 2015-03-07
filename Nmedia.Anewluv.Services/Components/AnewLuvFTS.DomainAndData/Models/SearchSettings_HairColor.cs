using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_HairColor
    {
        public int SearchSettings_HairColorID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> HairColorID { get; set; }
        public virtual CriteriaAppearance_HairColor CriteriaAppearance_HairColor { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
