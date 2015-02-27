using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace GeoData.Domain.Models
{
    public partial class Country_PostalCode_List :Entity
    {
        public byte CountryID { get; set; }
        public string Country_Code { get; set; }
        public string CountryName { get; set; }
        public string Country_Region { get; set; }
        public Nullable<byte> PostalCodes { get; set; }
        public Nullable<int> CountryCustomRegionID { get; set; }
    }
}
