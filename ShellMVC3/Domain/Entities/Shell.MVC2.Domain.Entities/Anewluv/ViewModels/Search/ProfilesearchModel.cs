using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{

    [DataContract]
    public class GeoSearchModel
    {
        [DataMember]
        public string countryname { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string filter { get; set; }      
        [DataMember]
        public string stateprovince { get; set; }
        [DataMember]
        public string postalcode { get; set; }
       
    }
}
