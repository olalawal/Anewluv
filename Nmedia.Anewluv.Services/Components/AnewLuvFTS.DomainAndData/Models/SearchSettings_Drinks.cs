using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Drinks
    {
        public int SearchSettings_DrinksID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> DrinksID { get; set; }
        public virtual CriteriaCharacter_Drinks CriteriaCharacter_Drinks { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
