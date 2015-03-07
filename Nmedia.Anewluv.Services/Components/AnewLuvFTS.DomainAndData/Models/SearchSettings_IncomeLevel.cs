using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_IncomeLevel
    {
        public int SearchSettings_IncomeLevelID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ImcomeLevelID { get; set; }
        public virtual CriteriaLife_IncomeLevel CriteriaLife_IncomeLevel { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
