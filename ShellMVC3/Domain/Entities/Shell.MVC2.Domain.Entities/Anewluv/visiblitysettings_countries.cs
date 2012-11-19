using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract ]
    public class visiblitysettings_country
    {

        [Key]
        [DataMember ]
        public int id { get; set; }
          [DataMember]
        public string  countryId { get; set; }
          [DataMember]
        public string  countryname { get; set; }
          [DataMember]
        public int visiblitysetting_id { get; set; }
          [DataMember]
        public virtual visiblitysetting visiblitysetting { get; set; }
        
    }
}
