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
        public PhotoModel()
        {
            this.photostoupload = new List<PhotoUploadModel>();
        }
        

        //search stuff for photos
        [DataMember]
        public int? profileid { get; set; }
        [DataMember]
        public string screenname { get; set; }
        //**********/
        [DataMember]
        public int? photoalbumid { get; set; }
        [DataMember]
        public int? phototstatusid { get; set; }
        [DataMember]
        public int? phototapprovalstatusid { get; set; }
        [DataMember]
        public int? photoformatid { get; set; }

        //secruity level changing
        [DataMember]
        public bool? makepublic { get; set; }
        [DataMember]
        public int? photosecuritylevelid { get; set; }

        [DataMember]
        public List<PhotoUploadModel> photostoupload { get; set; }
        [DataMember]
        public bool autoupload { get; set; }
        [DataMember]
        public bool multiple { get; set; }

        [DataMember]
        public PhotoUploadModel singlephototoupload { get; set; }
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
        [DataMember]
        public byte[] image { get; set; }

        //paging variables
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? numberperpage { get; set; }
        [DataMember]
        public int? currentpage { get; set; }

        //search stuff for photos
        [DataMember]
        public int? viewerprofileid { get; set; }

        //admin otions for rejecting phtoos
        public bool? photoapproved { get; set; }
        public int? photorejectionreasonid  { get; set; }
        public string photoapprovalrejectnote { get; set; }
    }
}
