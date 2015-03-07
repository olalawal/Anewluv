using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_LookingFor
    {
        public int SearchSettings_LookingFor1 { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> LookingForID { get; set; }
        public virtual CriteriaLife_LookingFor CriteriaLife_LookingFor { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
