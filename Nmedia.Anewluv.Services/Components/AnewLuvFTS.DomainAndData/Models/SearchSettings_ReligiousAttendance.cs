using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_ReligiousAttendance
    {
        public int SearchSettings_ReligiousAttendanceID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ReligiousAttendanceID { get; set; }
        public virtual CriteriaCharacter_ReligiousAttendance CriteriaCharacter_ReligiousAttendance { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
        public virtual SearchSetting SearchSetting1 { get; set; }
    }
}
