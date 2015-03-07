using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaAppearance_EyeColor
    {
        public CriteriaAppearance_EyeColor()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_EyeColor = new List<SearchSettings_EyeColor>();
        }

        public int EyeColorID { get; set; }
        public string EyeColorName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_EyeColor> SearchSettings_EyeColor { get; set; }
    }
}
