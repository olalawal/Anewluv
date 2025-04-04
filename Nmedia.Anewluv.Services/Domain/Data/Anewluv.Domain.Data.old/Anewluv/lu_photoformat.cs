using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract]
     [Serializable]
    public partial class lu_photoformat :Entity
    {
       
        public lu_photoformat()
        {
            this.photoconversions = new List<photoconversion>();
        }

      
        [DataMember]  public int id { get; set; }
        [NotMapped, DataMember] public bool selected { get; set; }
        [DataMember]  public string description { get; set; }
        [DataMember]  public int photoImagersizerformat_id { get; set; }
        public virtual lu_photoImagersizerformat lu_photoImagersizerformat { get; set; }
        public virtual ICollection<photoconversion> photoconversions { get; set; }
    }
}
