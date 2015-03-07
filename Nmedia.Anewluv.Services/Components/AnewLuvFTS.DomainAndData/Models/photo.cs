using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class photo
    {
        public System.Guid PhotoID { get; set; }
        public string ProfileID { get; set; }
        public string ImageCaption { get; set; }
        public string ProfileImageType { get; set; }
        public int PhotoStatusID { get; set; }
        public string Aproved { get; set; }
        public Nullable<int> PhotoSize { get; set; }
        public Nullable<System.DateTime> PhotoDate { get; set; }
        public Nullable<int> PhotoAlbumID { get; set; }
        public Nullable<int> PhotoReviewStatusID { get; set; }
        public string PhotoReviewerID { get; set; }
        public Nullable<System.DateTime> PhotoReviewDate { get; set; }
        public byte[] ProfileImage { get; set; }
        public Nullable<int> PhotoRejectionReasonID { get; set; }
        public Nullable<System.Guid> PhotoUniqueID { get; set; }
        public Nullable<int> PhotoTypeID { get; set; }
        public string PhotoProviderName { get; set; }
        public virtual PhotoRejectionReason PhotoRejectionReason { get; set; }
        public virtual PhotoType PhotoType { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual ProfileData ProfileData1 { get; set; }
        public virtual PhotoAlbum PhotoAlbum { get; set; }
        public virtual PhotoReviewStatu PhotoReviewStatu { get; set; }
        public virtual PhotoStatu PhotoStatu { get; set; }
    }
}
