using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_PoliticalView
    {
        public int SearchSettings_PoliticalViewID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> PoliticalViewID { get; set; }
        public virtual CriteriaCharacter_PoliticalView CriteriaCharacter_PoliticalView { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
