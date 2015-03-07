using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileGeoDataLogger
    {
        public int id { get; set; }
        public string ProfileID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Lattitude { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string UserAgent { get; set; }
        public string Continent { get; set; }
        public string IPaddress { get; set; }
        public string SessionID { get; set; }
        public virtual profile profile { get; set; }
    }
}
