using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AnewLuvFTS.DomainAndData.Models
{
    [DataContract]
    public partial class photo
    {
        public System.Guid PhotoID { get; set; }
        public string ProfileID { get; set; }
        [DataMember]
        public string ImageCaption { get; set; }
        [DataMember]
        public string ProfileImageType { get; set; }
        [DataMember]
        public int PhotoStatusID { get; set; }
        [DataMember]
        public string Aproved { get; set; }
         [DataMember]
        public Nullable<int> PhotoSize { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PhotoDate { get; set; }
      [DataMember]  public Nullable<int> PhotoAlbumID { get; set; }
      [DataMember]
      public Nullable<int> PhotoReviewStatusID { get; set; }
      [DataMember]
      public string PhotoReviewerID { get; set; }
      [DataMember]
      public Nullable<System.DateTime> PhotoReviewDate { get; set; }
      [DataMember]
      public byte[] ProfileImage { get; set; }
      [DataMember]
      public Nullable<int> PhotoRejectionReasonID { get; set; }
      [DataMember]
      public Nullable<System.Guid> PhotoUniqueID { get; set; }
      [DataMember]
      public Nullable<int> PhotoTypeID { get; set; }
      [DataMember]
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
