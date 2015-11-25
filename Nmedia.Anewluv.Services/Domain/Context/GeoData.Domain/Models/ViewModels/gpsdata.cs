using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
 [DataContract]
    public class gpsdata
    {
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string state_province { get; set; }
        [DataMember]
        public string postalcode { get; set; }       
        [DataMember]
        public bool selected { get; set; }
    }
}
