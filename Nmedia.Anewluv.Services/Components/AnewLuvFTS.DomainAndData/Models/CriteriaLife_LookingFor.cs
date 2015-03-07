using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaLife_LookingFor
    {
        public CriteriaLife_LookingFor()
        {
            this.ProfileData_LookingFor = new List<ProfileData_LookingFor>();
            this.SearchSettings_LookingFor = new List<SearchSettings_LookingFor>();
        }

        public int LookingForID { get; set; }
        public string LookingForName { get; set; }
        public virtual ICollection<ProfileData_LookingFor> ProfileData_LookingFor { get; set; }
        public virtual ICollection<SearchSettings_LookingFor> SearchSettings_LookingFor { get; set; }
    }
}
