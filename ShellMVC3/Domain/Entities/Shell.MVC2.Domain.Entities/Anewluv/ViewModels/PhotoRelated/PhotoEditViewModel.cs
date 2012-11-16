using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [Serializable]
    [DataContract]
    public class PhotoEditViewModel
    {

        // properties

        [DataMember]
        public PhotoEditModel SingleProfilePhoto { get; set; }

        [DataMember]
       // public List<EditProfileViewPhotoModel> ProfilePhotosApproved { get; set; }
        public List<PhotoEditModel> ProfilePhotosApproved { get; set; }

        [DataMember]
        //public List<EditProfileViewPhotoModel> ProfilePhotosNotApproved { get; set; }
        public List<PhotoEditModel> ProfilePhotosNotApproved { get; set; }

        [DataMember]
       // public List<EditProfileViewPhotoModel> ProfilePhotosPrivate { get; set; }
        public List<PhotoEditModel> ProfilePhotosPrivate { get; set; }


        [DataMember]
        public PhotoUploadViewModel PhotosUploading { get; set; }
         
        [DataMember]
        public bool IsUploading { get; set; }
        //model for quick status about me
    }
}
