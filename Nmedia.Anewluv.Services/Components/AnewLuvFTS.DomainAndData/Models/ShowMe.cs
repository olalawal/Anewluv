using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ShowMe
    {
        public ShowMe()
        {
            this.SearchSettings_ShowMe = new List<SearchSettings_ShowMe>();
        }

        public int ShowMeID { get; set; }
        public string ShowMeName { get; set; }
        public virtual ICollection<SearchSettings_ShowMe> SearchSettings_ShowMe { get; set; }
    }
}
