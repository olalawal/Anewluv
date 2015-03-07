using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Humor
    {
        public int SearchSettings_HumorID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> HumorID { get; set; }
        public virtual CriteriaCharacter_Humor CriteriaCharacter_Humor { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
