using System;
using System.Collections.Generic;

namespace GeoData.Domain.Models
{
    public partial class Country_PostalCode_Region
    {
        public byte Country_PostalCode_RegionID { get; set; }
        public string Country_Region { get; set; }
        public string Country_Region_Description { get; set; }
    }
}
