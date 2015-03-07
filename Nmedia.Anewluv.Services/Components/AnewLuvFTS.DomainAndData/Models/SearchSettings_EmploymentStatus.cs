using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_EmploymentStatus
    {
        public int SearchSettings_EmploymentStatusID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> EmploymentStatusID { get; set; }
        public virtual CriteriaLife_EmploymentStatus CriteriaLife_EmploymentStatus { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
