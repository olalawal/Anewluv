using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;


using System.Web.Security;
using System.Globalization;

using System.Text.RegularExpressions;


using Shell.MVC2.Domain.Entities.Anewluv.Validation;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{





    // [ActivationCodeIsValid("ProfileId", "ActivationCode", ErrorMessage = "Invalid Activation Code or Email Address")]
    [DataContract]
    [Serializable]
    public class ActivateProfileModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        //3333 DO THIS FIRST ON STAURDA, make it give same error invalid activaiton code or email addy, so make it class wide a well
        //change this to just validate the email address, we will maually verify the phtoto status in the controller beacse  , we need to be able
        // to toggle the upload photo dialog in the controller anyways
        //  [GalleryPhotoExistsValid("ProfileId", ErrorMessage = "You must Upload a GalleryPhoto to Register")]
        //#########################################################
        [DisplayName("Email Address")]
        public string ProfileId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Activation Code")]
        public string ActivationCode { get; set; }

        [Display(Name = "Photo Status")]


        public bool PhotoStatus { get; set; }

        [DataMember]
        PhotoEditModel Photos { get; set; }


    }
}
