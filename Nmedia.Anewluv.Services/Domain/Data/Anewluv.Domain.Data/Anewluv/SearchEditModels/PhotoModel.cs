using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Anewluv.Domain.Data.ViewModels;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class PhotoModel
    {

        

        //search stuff for photos
        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public string screenname { get; set; }
        //**********/
        [DataMember]
        public int? photoalbumid { get; set; }
        [DataMember]
        public int? phototstatusid { get; set; }
        [DataMember]
        public int? photoformatid { get; set; }
        [DataMember]
        public int? photosecuritylevel { get; set; }
        [DataMember]
        public PhotosUploadModel photosuploadmodel { get; set; }
        [DataMember]
        public PhotoUploadModel singlephotouploadmodel { get; set; }
        [DataMember]
        public Guid? photoid { get; set; }
        [DataMember]
        public List<Guid> photoids { get; set; }
        [DataMember]
        public string photocaption { get; set; }
        [DataMember]
        public string imageUrl { get; set; }
        [DataMember]
        public string inmagesource { get; set; }
        [DataMember]
        public PhotoAlbumViewModel photoalbum { get; set; }

        //paging variables
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? numberperpage { get; set; }
        [DataMember]
        public int? currentpage { get; set; }
    }
}
