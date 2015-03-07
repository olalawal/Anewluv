using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_HaveKids
    {
        public int SearchSettings_HaveKidsID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> HaveKidsID { get; set; }
        public virtual CriteriaLife_HaveKids CriteriaLife_HaveKids { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
