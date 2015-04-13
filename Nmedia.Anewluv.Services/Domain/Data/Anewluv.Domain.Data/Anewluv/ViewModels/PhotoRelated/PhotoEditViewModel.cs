using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class PhotoEditViewModel
    {

        // properties

        [DataMember]
        public PhotoModel SingleProfilePhoto { get; set; }

        [DataMember]
       // public List<EditProfileViewPhotoModel> ProfilePhotosApproved { get; set; }
        public List<PhotoModel> ProfilePhotosApproved { get; set; }

        [DataMember]
        //public List<EditProfileViewPhotoModel> ProfilePhotosNotApproved { get; set; }
        public List<PhotoModel> ProfilePhotosNotApproved { get; set; }

        [DataMember]
       // public List<EditProfileViewPhotoModel> ProfilePhotosPrivate { get; set; }
        public List<PhotoModel> ProfilePhotosPrivate { get; set; }


        [DataMember]
        public PhotoUploadViewModel PhotosUploading { get; set; }
         
        [DataMember]
        public bool IsUploading { get; set; }
        //model for quick status about me
    }
}
