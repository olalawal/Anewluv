using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SortByType
    {
        public SortByType()
        {
            this.SearchSettings_SortByType = new List<SearchSettings_SortByType>();
        }

        public int SortByTypeID { get; set; }
        public string SortByName { get; set; }
        public virtual ICollection<SearchSettings_SortByType> SearchSettings_SortByType { get; set; }
    }
}
