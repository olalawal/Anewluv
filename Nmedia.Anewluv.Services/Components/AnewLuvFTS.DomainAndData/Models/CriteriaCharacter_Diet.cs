using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Diet
    {
        public CriteriaCharacter_Diet()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Diet = new List<SearchSettings_Diet>();
        }

        public int DietID { get; set; }
        public string DietName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Diet> SearchSettings_Diet { get; set; }
    }
}
