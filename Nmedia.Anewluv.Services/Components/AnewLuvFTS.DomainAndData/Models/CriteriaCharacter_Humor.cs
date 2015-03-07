using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Humor
    {
        public CriteriaCharacter_Humor()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Humor = new List<SearchSettings_Humor>();
        }

        public int HumorID { get; set; }
        public string HumorName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Humor> SearchSettings_Humor { get; set; }
    }
}
