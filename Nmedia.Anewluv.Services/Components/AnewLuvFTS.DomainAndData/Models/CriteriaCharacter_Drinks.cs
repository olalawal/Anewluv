using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Drinks
    {
        public CriteriaCharacter_Drinks()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Drinks = new List<SearchSettings_Drinks>();
        }

        public int DrinksID { get; set; }
        public string DrinksName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Drinks> SearchSettings_Drinks { get; set; }
    }
}
