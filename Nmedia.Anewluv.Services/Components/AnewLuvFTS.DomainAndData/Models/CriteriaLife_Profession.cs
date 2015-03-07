using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_Profession
    {
        public CriteriaLife_Profession()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Profession = new List<SearchSettings_Profession>();
        }

        public int ProfessionID { get; set; }
        public string ProfiessionName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Profession> SearchSettings_Profession { get; set; }
    }
}
