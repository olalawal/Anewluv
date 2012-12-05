using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [Serializable]
    [DataContract]
    public class PhotoViewModel
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


    }
}
