using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class PhotoAlbum
    {
        public PhotoAlbum()
        {
            this.photos = new List<photo>();
        }

        public int PhotoAlbumID { get; set; }
        public string ProfileID { get; set; }
        public string PhotoAlbumDescription { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
