using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Religion
    {
        public int SearchSettings_ReligionID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ReligionID { get; set; }
        public virtual CriteriaCharacter_Religion CriteriaCharacter_Religion { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
