using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Ethnicity
    {
        public int SearchSettings_EthnicitiesID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> EthicityID { get; set; }
        public virtual CriteriaAppearance_Ethnicity CriteriaAppearance_Ethnicity { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
