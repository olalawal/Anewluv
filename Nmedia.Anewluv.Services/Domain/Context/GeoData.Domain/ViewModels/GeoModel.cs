using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.ViewModels
{
    [DataContract]
    public class GeoModel
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string countrycode { get; set; }
        [DataMember]
        public string countryid { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string stateprovince { get; set; }
        [DataMember]
        public string filter { get; set; }
        [DataMember]
        public string postalcode  { get; set; }
        [DataMember]
        public string lattitude { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string lattitude2 { get; set; }
        [DataMember]
        public string longitude2 { get; set; }
        [DataMember]
        public string unit { get; set; }

    }



}

