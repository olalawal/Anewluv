using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class visiblitysettings_country
    {
        public int id { get; set; }
        public string countryId { get; set; }
        public string countryname { get; set; }
        public int visiblitysetting_id { get; set; }
        public virtual visiblitysetting visiblitysetting { get; set; }
    }
}
