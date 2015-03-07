using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_WantKids
    {
        public int SearchSettings_WantKidsID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> WantKidsID { get; set; }
        public virtual CriteriaLife_WantsKids CriteriaLife_WantsKids { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
