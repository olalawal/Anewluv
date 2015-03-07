using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_ReligiousAttendance
    {
        public CriteriaCharacter_ReligiousAttendance()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_ReligiousAttendance = new List<SearchSettings_ReligiousAttendance>();
        }

        public int ReligiousAttendanceID { get; set; }
        public string ReligiousAttendanceName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance { get; set; }
    }
}
