using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_EducationLevel
    {
        public CriteriaLife_EducationLevel()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_EducationLevel = new List<SearchSettings_EducationLevel>();
        }

        public int EducationLevelID { get; set; }
        public string EducationLevelName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_EducationLevel> SearchSettings_EducationLevel { get; set; }
    }
}
