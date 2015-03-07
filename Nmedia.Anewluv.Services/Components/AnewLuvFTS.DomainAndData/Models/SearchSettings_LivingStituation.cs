using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_LivingStituation
    {
        public int SearchSettings_LivingStituationID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> LivingStituationID { get; set; }
        public virtual CriteriaLife_LivingSituation CriteriaLife_LivingSituation { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
