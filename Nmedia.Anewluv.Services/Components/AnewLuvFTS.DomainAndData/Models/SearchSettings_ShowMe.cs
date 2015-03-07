using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_ShowMe
    {
        public int SearchSettings_ShowMeID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ShowMeID { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
        public virtual ShowMe ShowMe { get; set; }
    }
}
