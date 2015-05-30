using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
     [DataContract]
    [Serializable]
    public class country
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }
}
