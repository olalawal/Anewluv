using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Sign
    {
        public CriteriaCharacter_Sign()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Sign = new List<SearchSettings_Sign>();
        }

        public int SignID { get; set; }
        public string SignName { get; set; }
        public string SignBirthMonth { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Sign> SearchSettings_Sign { get; set; }
    }
}
