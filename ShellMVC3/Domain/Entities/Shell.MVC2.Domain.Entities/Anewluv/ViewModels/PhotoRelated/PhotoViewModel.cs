using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
 
    [DataContract]
    public class PhotoViewModel
    {
        public PhotoViewModel()
        {


        }
          [DataMember]
        public List<photo> Photos { get; set; }
          [DataMember]
        public string ProfileID { get; set; }

        //determines if the users have a photo situation has been handled i,e ither the user refuses or agrees in registration
        //in activation photo must me uploaded so it must be true at some point
          [DataMember]
        public bool PhotoStatus { get; set; }
          [DataMember]
        public string ProfileImageType { get; set; }

        //new properties for the telerik autoupload
          [DataMember]
        public bool autoUpload { get; set; }
          [DataMember]
        public bool multiple { get; set; }

        //  ViewData["autoUpload"] = autoUpload ?? true;
        //  ViewData["multiple"] = multiple ?? true;



  


    }
}
