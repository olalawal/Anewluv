using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Genders
    {
        public int SearchSettings_GenderID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> GenderID { get; set; }
        public virtual gender gender { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
