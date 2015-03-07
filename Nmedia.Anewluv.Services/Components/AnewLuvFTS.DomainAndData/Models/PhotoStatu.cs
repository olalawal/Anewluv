using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class PhotoStatu
    {
        public PhotoStatu()
        {
            this.photos = new List<photo>();
        }

        public int PhotoStatusID { get; set; }
        public string PhotoStatusValue { get; set; }
        public string Description { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
