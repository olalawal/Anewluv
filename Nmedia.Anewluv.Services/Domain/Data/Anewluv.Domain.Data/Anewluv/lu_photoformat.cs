using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract]
    public partial class lu_photoformat
    {
       
        public lu_photoformat()
        {
            this.photoconversions = new List<photoconversion>();
        }

      
        [DataMember]  public int id { get; set; }
        [NotMapped, DataMember] public bool? isselected { get; set; }
        [DataMember]  public string description { get; set; }
        [DataMember]  public int photoImagersizerformat_id { get; set; }
        public virtual lu_photoImagersizerformat lu_photoImagersizerformat { get; set; }
        public virtual ICollection<photoconversion> photoconversions { get; set; }
    }
}
