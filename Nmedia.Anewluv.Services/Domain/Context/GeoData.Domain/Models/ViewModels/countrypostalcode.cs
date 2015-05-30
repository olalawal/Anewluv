using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
    [DataContract]
    [Serializable]
    public class countrypostalcode
    {


        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public int? customregionid { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public bool? haspostalcode { get; set; }
    }
}
