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
        public string Latitude { get; set; }
         [DataMember]
        public string Longitude { get; set; }
         [DataMember]
        public string State_Province { get; set; }
        [DataMember]
        public string postalcode { get; set; }       
        [DataMember]
        public bool selected { get; set; }
    }
}
