using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{

    public class profileactivitygeodata
    {
        [Key]
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string regionname { get; set; }
        [DataMember]
        public string continent { get; set; }
        [DataMember]
        public int? countryId { get; set; }
        [DataMember]
        public string countrycode { get; set; }
        [DataMember]
        public string countryname { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public Nullable<double> lattitude { get; set; }
        [DataMember]
        public Nullable<double> longitude { get; set; }
      
      
    }
}
