using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_PoliticalView
    {
        public CriteriaCharacter_PoliticalView()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_PoliticalView = new List<SearchSettings_PoliticalView>();
        }

        public int PoliticalViewID { get; set; }
        public string PoliticalViewName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_PoliticalView> SearchSettings_PoliticalView { get; set; }
    }
}
