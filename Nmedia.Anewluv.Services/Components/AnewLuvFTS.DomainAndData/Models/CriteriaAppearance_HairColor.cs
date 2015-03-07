using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaAppearance_HairColor
    {
        public CriteriaAppearance_HairColor()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_HairColor = new List<SearchSettings_HairColor>();
        }

        public int HairColorID { get; set; }
        public string HairColorName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_HairColor> SearchSettings_HairColor { get; set; }
    }
}
