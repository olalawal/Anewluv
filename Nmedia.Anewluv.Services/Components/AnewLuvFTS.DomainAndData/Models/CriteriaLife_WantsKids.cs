using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_WantsKids
    {
        public CriteriaLife_WantsKids()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_WantKids = new List<SearchSettings_WantKids>();
        }

        public int WantsKidsID { get; set; }
        public string WantsKidsName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_WantKids> SearchSettings_WantKids { get; set; }
    }
}
