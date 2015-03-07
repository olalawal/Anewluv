using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_EmploymentStatus
    {
        public CriteriaLife_EmploymentStatus()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_EmploymentStatus = new List<SearchSettings_EmploymentStatus>();
        }

        public int EmploymentSatusID { get; set; }
        public string EmploymentStatusName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_EmploymentStatus> SearchSettings_EmploymentStatus { get; set; }
    }
}
