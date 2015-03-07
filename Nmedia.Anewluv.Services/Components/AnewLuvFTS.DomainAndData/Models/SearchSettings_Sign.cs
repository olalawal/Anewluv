using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Sign
    {
        public int SearchSettings_SignID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> SignID { get; set; }
        public virtual CriteriaCharacter_Sign CriteriaCharacter_Sign { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
