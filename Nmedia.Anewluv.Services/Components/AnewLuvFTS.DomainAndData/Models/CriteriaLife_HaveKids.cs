using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_HaveKids
    {
        public CriteriaLife_HaveKids()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_HaveKids = new List<SearchSettings_HaveKids>();
        }

        public int HaveKidsId { get; set; }
        public string HaveKidsName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_HaveKids> SearchSettings_HaveKids { get; set; }
    }
}
