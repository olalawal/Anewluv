using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_MaritalStatus
    {
        public CriteriaLife_MaritalStatus()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_MaritalStatus = new List<SearchSettings_MaritalStatus>();
        }

        public int MaritalStatusID { get; set; }
        public string MaritalStatusName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_MaritalStatus> SearchSettings_MaritalStatus { get; set; }
    }
}
