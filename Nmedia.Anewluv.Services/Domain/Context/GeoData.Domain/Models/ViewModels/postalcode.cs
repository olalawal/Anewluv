using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
     [DataContract]
    public class postalcode
    {
        [DataMember]
        public string postalcodevalue { get; set; }
           [DataMember]
        public string latitude { get; set; }
           [DataMember]
           public string longitude { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }
}
