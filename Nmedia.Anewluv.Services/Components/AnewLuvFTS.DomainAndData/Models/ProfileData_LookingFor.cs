using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileData_LookingFor
    {
        public int ProfileData_LookingFor1 { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> LookingForID { get; set; }
        public virtual CriteriaLife_LookingFor CriteriaLife_LookingFor { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
