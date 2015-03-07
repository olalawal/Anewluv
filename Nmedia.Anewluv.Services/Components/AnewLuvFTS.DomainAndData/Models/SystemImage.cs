using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SystemImage
    {
        public int ImageID { get; set; }
        public string ProfileID { get; set; }
        public string ImageCaption { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        public string ImageStatus { get; set; }
        public string AproveStatus { get; set; }
    }
}
