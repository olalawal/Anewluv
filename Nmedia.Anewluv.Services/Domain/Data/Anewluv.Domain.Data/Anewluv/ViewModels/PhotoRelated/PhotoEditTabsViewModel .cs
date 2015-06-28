using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class PhotosSortedCountsViewModel
    {

        // properties

     
        [DataMember]
        public int? PublicPhotoCount { get; set; }

        [DataMember]
        public int? PeeksOnlyPhotoCount { get; set; }

        [DataMember]      
        public int ?PrivatePhotoCount { get; set; }

        [DataMember]
        public int? LikesOnlyPhotoCount { get; set; }

        [DataMember]
        public int? InterestsOnlyPhotoCount { get; set; }

        [DataMember]
        public int? NotApprovedPhotoCount { get; set; }

        [DataMember]
        public int? RejectedPhotoCount { get; set; }
       
        //model for quick status about me
    }
}
