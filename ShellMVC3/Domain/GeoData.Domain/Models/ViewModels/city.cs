using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
     [DataContract]
    public class city
    {
        [DataMember]
        public string cityvalue { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }
}
