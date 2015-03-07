using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class NigerianState
    {
        public NigerianState()
        {
            this.SearchSettings_NigerianState = new List<SearchSettings_NigerianState>();
        }

        public int NigerianStateID { get; set; }
        public string StateName { get; set; }
        public virtual ICollection<SearchSettings_NigerianState> SearchSettings_NigerianState { get; set; }
    }
}
