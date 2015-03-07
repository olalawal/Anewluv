using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Location
    {
        public int SearchSettings_Location1 { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
