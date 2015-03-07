using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileData_Ethnicity
    {
        public int ProfileData_Ethnicity1 { get; set; }
        public string ProfileID { get; set; }
        public Nullable<int> EthnicityID { get; set; }
        public virtual CriteriaAppearance_Ethnicity CriteriaAppearance_Ethnicity { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
