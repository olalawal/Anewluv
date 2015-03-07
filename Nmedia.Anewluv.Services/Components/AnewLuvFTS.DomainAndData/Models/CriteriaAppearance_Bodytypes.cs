using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaAppearance_Bodytypes
    {
        public CriteriaAppearance_Bodytypes()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_BodyTypes = new List<SearchSettings_BodyTypes>();
        }

        public int BodyTypesID { get; set; }
        public string BodyTypeName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_BodyTypes> SearchSettings_BodyTypes { get; set; }
    }
}
