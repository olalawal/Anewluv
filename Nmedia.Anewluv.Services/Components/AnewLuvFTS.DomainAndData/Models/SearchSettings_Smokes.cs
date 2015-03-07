using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Smokes
    {
        public int SearchSettings_SmokesID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> SmokesID { get; set; }
        public virtual CriteriaCharacter_Smokes CriteriaCharacter_Smokes { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
