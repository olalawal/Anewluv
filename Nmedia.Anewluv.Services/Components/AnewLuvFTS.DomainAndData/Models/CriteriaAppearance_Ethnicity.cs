using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaAppearance_Ethnicity
    {
        public CriteriaAppearance_Ethnicity()
        {
            this.ProfileData_Ethnicity = new List<ProfileData_Ethnicity>();
            this.SearchSettings_Ethnicity = new List<SearchSettings_Ethnicity>();
        }

        public int EthnicityID { get; set; }
        public string EthnicityName { get; set; }
        public virtual ICollection<ProfileData_Ethnicity> ProfileData_Ethnicity { get; set; }
        public virtual ICollection<SearchSettings_Ethnicity> SearchSettings_Ethnicity { get; set; }
    }
}
