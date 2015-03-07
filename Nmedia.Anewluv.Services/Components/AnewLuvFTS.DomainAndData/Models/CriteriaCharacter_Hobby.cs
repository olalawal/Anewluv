using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Hobby
    {
        public CriteriaCharacter_Hobby()
        {
            this.ProfileData_Hobby = new List<ProfileData_Hobby>();
            this.SearchSettings_Hobby = new List<SearchSettings_Hobby>();
        }

        public int HobbyID { get; set; }
        public string HobbyName { get; set; }
        public virtual ICollection<ProfileData_Hobby> ProfileData_Hobby { get; set; }
        public virtual ICollection<SearchSettings_Hobby> SearchSettings_Hobby { get; set; }
    }
}
