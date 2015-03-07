using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_NigerianState
    {
        public int SearchSettings_NigerianStateID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> NigerianStateID { get; set; }
        public virtual NigerianState NigerianState { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
