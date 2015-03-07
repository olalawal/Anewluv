using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_SortByType
    {
        public int SearchSettings_SortByType1 { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> SortByTypeID { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
        public virtual SortByType SortByType { get; set; }
    }
}
