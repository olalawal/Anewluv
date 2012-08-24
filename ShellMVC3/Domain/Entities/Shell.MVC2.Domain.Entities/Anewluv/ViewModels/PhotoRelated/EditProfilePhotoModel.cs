using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //10-29-2011 moved the  edit photo stuff here but it is not edit only, probably should be in photomodel
   
   [DataContract ]
    public class EditProfilePhotoModel
    {
          [DataMember]
        public Guid PhotoID { get; set; }
        [DataMember]
        public string ScreenName { get; set; }
        [DataMember]
        public string ProfileID { get; set; }
        [DataMember]
        public string Aproved { get; set; }
        [DataMember]
        public string ProfileImageType { get; set; }
        [DataMember]
        public bool checkedPrimary { get; set; }
        [DataMember]
        public DateTime? PhotoDate { get; set; }
        [DataMember]
        public string ImageCaption { get; set; }
        [DataMember]
        public int PhotoStatusID { get; set; }

        public bool BoolImageType(string ProfileImageType)
        {
            bool check = false;

            if (ProfileImageType == "Gallery")//"NoStatus vs Gallery"
            {
                check = true;
            }

            return check;
        }

    }  
}
