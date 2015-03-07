using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_LivingSituation
    {
        public CriteriaLife_LivingSituation()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_LivingStituation = new List<SearchSettings_LivingStituation>();
        }

        public int LivingSituationID { get; set; }
        public string LivingSituationName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_LivingStituation> SearchSettings_LivingStituation { get; set; }
    }
}
