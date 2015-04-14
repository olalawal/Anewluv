using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
 
    [DataContract]  
    public class PhotosUploadModel
    {

        public PhotosUploadModel()
        {
            this.photosuploaded = new List<PhotoUploadModel>();  
        }
       
        [DataMember]
        public List<PhotoUploadModel> photosuploaded { get; set; }
        [DataMember]
        public int profileid { get; set; }
      
        [DataMember]
        public bool autoupload { get; set; }
        [DataMember]
        public bool multiple { get; set; }

      

    }
}
