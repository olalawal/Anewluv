using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_IncomeLevel
    {
        public CriteriaLife_IncomeLevel()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_IncomeLevel = new List<SearchSettings_IncomeLevel>();
        }

        public int IncomeLevelID { get; set; }
        public string IncomeLevelName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_IncomeLevel> SearchSettings_IncomeLevel { get; set; }
    }
}
