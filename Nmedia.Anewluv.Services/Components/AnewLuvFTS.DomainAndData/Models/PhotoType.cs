using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class PhotoType
    {
        public PhotoType()
        {
            this.photos = new List<photo>();
        }

        public int PhotoTypeID { get; set; }
        public string PhotoTypeName { get; set; }
        public string PhotoTypeDescription { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
