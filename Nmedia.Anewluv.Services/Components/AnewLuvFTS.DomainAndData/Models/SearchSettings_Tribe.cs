using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Tribe
    {
        public int SearchSettings_TribeID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> TribeID { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
