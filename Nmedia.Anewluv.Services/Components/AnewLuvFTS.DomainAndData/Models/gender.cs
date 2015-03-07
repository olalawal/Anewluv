using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class gender
    {
        public gender()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.ProfileVisiblitySettings = new List<ProfileVisiblitySetting>();
            this.SearchSettings_Genders = new List<SearchSettings_Genders>();
        }

        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<ProfileVisiblitySetting> ProfileVisiblitySettings { get; set; }
        public virtual ICollection<SearchSettings_Genders> SearchSettings_Genders { get; set; }
    }
}
