using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Religion
    {
        public CriteriaCharacter_Religion()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Religion = new List<SearchSettings_Religion>();
        }

        public int religionID { get; set; }
        public string religionName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Religion> SearchSettings_Religion { get; set; }
    }
}
