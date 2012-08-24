using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
 
    [DataContract]
    public class EditProfileViewPhotoModel
    {
       [DataMember]
        public Guid PhotoID { get; set; }
         [DataMember]
        public string ScreenName { get; set; }
         [DataMember]
        public bool CheckedPhoto { get; set; }
         [DataMember]
    public DateTime? PhotoDate { get; set; }
         [DataMember]
        public string ProfileId { get; set; }
    }
}
