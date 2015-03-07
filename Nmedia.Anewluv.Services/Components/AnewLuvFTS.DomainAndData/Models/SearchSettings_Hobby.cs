using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Hobby
    {
        public int SearchSettings_HobbyID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> HobbyID { get; set; }
        public virtual CriteriaCharacter_Hobby CriteriaCharacter_Hobby { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
