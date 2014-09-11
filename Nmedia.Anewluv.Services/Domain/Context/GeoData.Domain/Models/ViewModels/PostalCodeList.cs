using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GeoData.Domain.Models.ViewModels
{
    //Matches the reuturn vars from the databse
     [DataContract]
   public class PostalCodeList
    {
         
         [DataMember]
        public string PostalCode { get; set; }
         [DataMember]
         public string LATITUDE { get; set; }
         [DataMember]
         public string LONGITUDE { get; set; }
    }
}
