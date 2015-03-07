using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_MaritalStatus
    {
        public int SearchSettings_MaritalStatusID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> MaritalStatusID { get; set; }
        public virtual CriteriaLife_MaritalStatus CriteriaLife_MaritalStatus { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
