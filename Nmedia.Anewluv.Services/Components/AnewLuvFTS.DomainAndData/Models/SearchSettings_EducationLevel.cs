using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_EducationLevel
    {
        public int SearchSettings_EducationLevelID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> EducationLevelID { get; set; }
        public virtual CriteriaLife_EducationLevel CriteriaLife_EducationLevel { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
