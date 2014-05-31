using System;
using System.Collections.Generic;

namespace GeoData.Domain.Models
{
    public partial class CustomRegion
    {
        public int RegionID { get; set; }
        public string CustomRegionName { get; set; }
        public string CustomRegionDescription { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
