using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileData_Hobby
    {
        public int ProfileData_Hobby1 { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> HobbyID { get; set; }
        public virtual CriteriaCharacter_Hobby CriteriaCharacter_Hobby { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
