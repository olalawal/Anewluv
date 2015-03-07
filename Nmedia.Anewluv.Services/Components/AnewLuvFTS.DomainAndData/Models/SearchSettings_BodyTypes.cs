using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_BodyTypes
    {
        public int SearchSettings_BodyTypeID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> BodyTypesID { get; set; }
        public virtual CriteriaAppearance_Bodytypes CriteriaAppearance_Bodytypes { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
