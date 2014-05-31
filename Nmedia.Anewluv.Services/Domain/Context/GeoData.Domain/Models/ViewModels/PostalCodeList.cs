using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
     [DataContract]
   public class PostalCodeList
    {
         [DataMember]
        public string PostalCode { get; set; }
    }
}
