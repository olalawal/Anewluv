using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Diet
    {
        public int SearchSettings_DietID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> DietID { get; set; }
        public virtual CriteriaCharacter_Diet CriteriaCharacter_Diet { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
