using System;
using System.Collections.Generic;

namespace GeoData.Domain.Models
{
    public partial class Tanzania
    {
        public int RecordID { get; set; }
        public string Country_Code { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State_Province { get; set; }
        public string State_Province_Code { get; set; }
        public string County_Province { get; set; }
        public string Empty1 { get; set; }
        public string Empty2 { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string Empty3 { get; set; }
        public string Country_Region { get; set; }
    }
}
