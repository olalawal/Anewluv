using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
     [DataContract]
    public class distance
    {
        [DataMember]
        public int distancevalue;
        [DataMember]
        public int distanceindex;
        [DataMember]
        public bool selected { get; set; }

    }
}
