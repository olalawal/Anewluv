using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
 
    [DataContract]
    public class EditProfilePhotosViewModel
    {

        // properties

        [DataMember]
        public EditProfilePhotoModel SingleProfilePhoto { get; set; }

        [DataMember]
        public List<EditProfileViewPhotoModel> ProfilePhotosApproved { get; set; }

        [DataMember]
        public List<EditProfileViewPhotoModel> ProfilePhotosNotApproved { get; set; }

        [DataMember]
        public List<EditProfileViewPhotoModel> ProfilePhotosPrivate { get; set; }

        [DataMember]
        public PhotoViewModel Photo { get; set; }
         
        [DataMember]
        public bool IsUploading { get; set; }
        //model for quick status about me
    }
}
