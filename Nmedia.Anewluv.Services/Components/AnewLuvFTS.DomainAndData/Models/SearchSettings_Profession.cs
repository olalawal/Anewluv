using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Profession
    {
        public int SearchSettings_ProfessionID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ProfessionID { get; set; }
        public virtual CriteriaLife_Profession CriteriaLife_Profession { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
