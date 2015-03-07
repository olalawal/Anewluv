using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Smokes
    {
        public CriteriaCharacter_Smokes()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Smokes = new List<SearchSettings_Smokes>();
        }

        public int SmokesID { get; set; }
        public string SmokesName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Smokes> SearchSettings_Smokes { get; set; }
    }
}
