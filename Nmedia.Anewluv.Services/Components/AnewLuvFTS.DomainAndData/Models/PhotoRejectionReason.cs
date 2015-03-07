using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class PhotoRejectionReason
    {
        public PhotoRejectionReason()
        {
            this.photos = new List<photo>();
        }

        public int PhotoRejectionReasonID { get; set; }
        public string Description { get; set; }
        public string UserMessage { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
